using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;
using FERHRI.Geo;
using FERHRI.Amur.Meta;
using FERHRI.DB;

namespace FERHRI.SGMO
{
    /// <summary>
    /// Прогноз 
    ///     - от исходной даты для указанного метода прогноза 
    ///     - в точках одного трека 
    ///     - для набора прогностических переменных в терминах справочников БД Амур.
    /// </summary>
    public class TrackFcs
    {
        /// <summary>
        /// Метод прогноза
        /// </summary>
        public MethodForecast MethodForecast { get; set; }
        /// <summary>
        /// Атрибуты проностических переменных
        /// </summary>
        public List<Varoff> Varoffs { get; set; }
        public EnumPointNearestType PointNearestType = EnumPointNearestType.Nearest;
        public EnumDistanceType PointsDistanceType = EnumDistanceType.TheoremHaverSin;
        /// <summary>
        /// Прогностические значения
        /// </summary>
        public DataFcs0 DataFcs0 { get; set; }

        public TrackFcs(DateTime dateIni, int methodId, Track0 track0, bool isDeletePrevFcs = false)
        {
            MethodForecast = FERHRI.Amur.Meta.DataManager.GetInstance().MethodForecastRepository.Select(methodId);
            if (MethodForecast == null)
                throw new Exception("Нет такого метода прогноза с id=" + methodId);

            // Обработка случая уже осуществлённого прогноза
            List<DataFcs0> data0 = SGMO.DataManager.GetInstance().DataFcsRepository.SelectData0(track0.Id, dateIni);
            if (data0.Count != 0)
            {
                if (isDeletePrevFcs)
                    DataManager.GetInstance().DataFcsRepository.DeleteData1(data0[0].Id, MethodForecast.Method.Id);
                else
                    throw new Exception(string.Format("Прогноз методом {0} на дату {1} для трека [{2}] уже сделан. Останов.", methodId, dateIni, track0));
            }
            Varoffs = FERHRI.SGMO.DataManager.GetInstance().VarOffRepository.SelectVaroffs(methodId);
            Varoffs.RemoveAll(x => x.ExcludeFromFcs);

            // GET TRACK POINTS
            PointLags = track0.GetPointLags(dateIni, MethodForecast.Lags);

            // INIT DATA
            // Инициализируем для всех точек трека все лаги. Потом, в базу запишем только те, которые в PointLags.
            // Все нужны, например, для обработки осадков GFS.
            DataFcs0 = new DataFcs0() { Id = -1, DateIniUTC = dateIni, Track0Id = track0.Id, MethodId = methodId, DataFcs1List = new List<DataFcs1>(1000) };
            for (int i = 0; i < MethodForecast.Lags.Length; i++)
            {
                for (int j = 0; j < PointLags.Count; j++)
                {
                    for (int k = 0; k < Varoffs.Count; k++)
                    {
                        DataFcs0.DataFcs1List.Add(new DataFcs1()
                        {
                            Id = -1,
                            DataFcs0Id = DataFcs0.Id,
                            Lag = MethodForecast.Lags[i],
                            Point = PointLags[j].Point,
                            VaroffId = Varoffs[k].Id,
                            Value = double.NaN
                        });
                    }
                }
            }
        }
        public List<PointLag> PointLags { get; private set; }
        /// <summary>
        /// Шаг сетки прогнозов GFS по умолчанию.
        /// </summary>
        const double DEFAULT_GFS_DXDY = 0.5;
        /// <summary>
        /// Выбрать прогнозы из источников методов.
        /// </summary>
        /// <param name="Points">Прогнозопункты.</param>
        /// <param name="Variables">Структура проностической переменной.</param>
        /// <returns></returns>
        public void GetForecast(double gfsDxDy = double.NaN)
        {
            switch (MethodForecast.Method.Id)
            {
                case (int)EnumMethod.GFS: FcsGFS.GetFcs(this, gfsDxDy); break;
                case (int)EnumMethod.Sinopsis: FcsSinopsis.GetFcs(this); break;
                case (int)EnumMethod.WAVE_VVO_PACIFIC_0p5: FcsWave.GetFcs(this); break;
                default:
                    throw new Exception("Неизвестный метод прогноза " + MethodForecast.Method.Id);
            }
        }
        static public DateTime GetCurFcsDateIni(int fcsMethodId)
        {
            Console.Write("GetCurFcsDateIni started...");
            DateTime dateIni;
            switch (fcsMethodId)
            {
                case (int)EnumMethod.GFS:
                    dateIni = (new GfsRepository(DEFAULT_GFS_DXDY)).GetMaxDateIni();
                    break;
                case (int)EnumMethod.Sinopsis:
                    dateIni = DateTime.Today;
                    break;
                case (int)EnumMethod.WAVE_VVO_PACIFIC_0p5:
                    dateIni = new DateTime(2017,4,12); break;
                default:
                    throw new Exception("Неизвестный метод прогноза " + fcsMethodId);
            }
            Console.WriteLine(" -> {0}", dateIni);
            return dateIni;
        }

        public void WriteForecast()
        {
            // Удалить точки и заблаговременности, которые не надо записывать в базу

            DataFcs0.DataFcs1List.RemoveAll(item => item.Point == null);
            DataFcs0.DataFcs1List.RemoveAll(item => !PointLags.Exists(x => x.Point == item.Point && x.Lag == item.Lag));

            // Записать прогнозы

            SGMO.DataFcsRepository drep = SGMO.DataManager.GetInstance().DataFcsRepository;
            List<DataFcs0> df0 = drep.SelectData0(DataFcs0.Track0Id, DataFcs0.DateIniUTC);
            
            DataFcs0.Id = (df0.Count == 0) ? drep.InsertDataFcs0(DataFcs0) : df0[0].Id;
            drep.InsertDataFcs1(DataFcs0.Id, DataFcs0.DataFcs1List);
        }
    }
}
