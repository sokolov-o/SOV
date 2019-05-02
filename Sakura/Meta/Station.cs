using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Sakura.Meta
{
    /// <summary>
    /// Гидрометеорлогическая станция, пост, пункт. Сруктура членов класса соответствует структуре таблицы Hmdic..stations.
    /// </summary>
    public class Station
    {
        /// <summary>
        /// Уникальный код/индекс станции.
        /// </summary>
        public int Id = -1;
        /// <summary>
        /// Индекс станции.
        /// </summary>
        public int Index = -1;
        /// <summary>
        /// Идентификатор типа станции
        /// </summary>
        public Enums.StationType Type;
        /// <summary>
        /// Идентификатор организации, к кторой принадлежит станция
        /// </summary>
        public int OrgId;

        /// <summary>
        /// Наименование станции.
        /// </summary>
        public string Name;
        /// <summary>
        /// Широта.
        /// </summary>
        public Nullable<float> Lat;
        /// <summary>
        /// Долгота.
        /// </summary>
        public Nullable<float> Lon;
        /// <summary>
        /// Координатный номер станции.
        /// </summary>
        public Nullable<int> CoordNum;
        /// <summary>
        /// Часовой пояс станции.
        /// </summary>
        public Nullable<int> TimeZone;
        /// <summary>
        /// Инициализация экземпляра класса.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// /// <param name="stIndex"></param>
        /// <param name="coordNum"></param>
        /// <param name="timeZone"></param>
        public Station(int id, int stIndex, Enums.StationType stnTypeId, int orgId, string name, float? lat, float? lon, int? coordNum, int? timeZone)
        {
            this.Id = id;
            this.Index = stIndex;
            this.Type = stnTypeId;
            this.OrgId = orgId;
            this.Name = name;
            this.Lat = lat;
            this.Lon = lon;
            this.CoordNum = coordNum;
            this.TimeZone = timeZone;
        }
        public Station()
        {
        }
        /// <summary>
        /// Возвращает строку, представляющую текущий экземпляр класса.
        /// </summary>
        /// <returns>Строка, представляющая текущий экземпляр класса.</returns>
        override public string ToString()
        {
            return Id + ";" + Name + ";" + Lat + ";" + Lon + ";" + CoordNum + ";" + TimeZone;
        }
    }
}
