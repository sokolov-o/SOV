using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Sakura.Meta
{
    /// <summary>
    /// Метаданные: запись каталога г/м данных, содержащая коды атрибутов данных. 
    /// Структура класса соответствует структуре таблицы Hmdic..ctl.
    /// 
    /// ВМЕСТО null использовать int.MinValue !
    /// </summary>
    [DataContract]
    public class Catalog
    {
        /// <summary>
        /// Код записи каталога данных.
        /// </summary>
        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// Код базы данных, в которой расположены данные записи каталога.
        /// </summary>
        [DataMember]
        public int DbListId { get; set; }
        /// <summary>
        /// Код г/м параметра. Например, давление, температура, высота ниж. границы облачности.
        /// </summary>
        [DataMember]
        public int ParamId;
        /// <summary>
        /// Код типа временного интервала (временного разрешения) данных. Например, день, месяц, сезон.
        /// </summary>
        [DataMember]
        public int TimeId;
        /// <summary>
        /// Код пространственного типа данных. Например, поле, временной ряд, данные на отдельной станции.
        /// </summary>
        [DataMember]
        public int SpaceId;
        /// <summary>
        /// Код формата данных. Например, реляционный, бинарный, текстовый.
        /// </summary>
        [DataMember]
        public int FormatId;
        /// <summary>
        /// Код типа уровня. Например, стандартный горизонт, стандартная изобарическая поверхность.
        /// </summary>
        [DataMember]
        public int LevelTypeId;
        /// <summary>
        /// Значение уровня указанного типа. Например 900гПа, 50 метров.
        /// </summary>
        [DataMember]
        public int LevelValue;
        /// <summary>
        /// Код географического региона (трапеции) данных.
        /// </summary>
        [DataMember]
        public int GeoRegId;
        /// <summary>
        /// Код центра - поставщика данных. Например, EGRR, web, ДВНИГМИ.
        /// </summary>
        [DataMember]
        public int CenterId;
        /// <summary>
        /// Код действия, процедуры обработки данных. Например, значение (исходное), среднее, индекс Блиновой и др.
        /// </summary>
        [DataMember]
        public int ActionId;
        /// <summary>
        /// Заблаговременность данных (час). Например 0 (анализ), 12 (прогноз на 12 часов). Отрицательная величина означает множественные заблаговременности.
        /// </summary>
        [DataMember]
        public int PredictTime;
        /// <summary>
        /// Код регулярной сетки для полей данных. Сетка определяет границы региона, шаг по шир., долг. и др.
        /// </summary>
        [DataMember]
        public int GridId;
        /// <summary>
        /// Признак климатических данных. Например, климатических норм параметра.
        /// </summary>
        [DataMember]
        public int IsClimate;
        /// <summary>
        /// Дополнительное действиех. Достаточно произвольная величина, использующаяся в контексте задач по обработке данных.
        /// </summary>
        [DataMember]
        public string ActionSub;
        /// <summary>
        /// Номер в наборе данных для сохранения уникальности. ДОЛЖЕН БЫТЬ = 0.
        /// </summary>
        [DataMember]
        public int UnqAttr;
        /// <summary>
        /// Информация для запроса к данным. Напррмер, оператор select или путь к файлу и др. Представляет собой строку вида FIELDNAME1=FIELDVALUE1;FIELDNAME2=FIELDVALUE2;...
        /// </summary>
        [DataMember]
        public string DataCnn;
        /// <summary>
        /// Словарь, содержащий прочие коды атрибутов данных и их строковое представление, 
        /// связанные с записью каталога, но не вошедшие в неё. 
        /// Код атрибута соответствует кодам ключа из таблицы Hmdic.t_attr_dic и значениям enum TaskAttrEnum.
        /// </summary>
        [DataMember]
        public Dictionary<int, string> Attr = null;

        public bool TryGetAttrInt(Enums.TaskAttr attr, out int ret)
        {
            ret = int.MinValue;
            string s = String.Empty;

            if (Attr.TryGetValue((int)attr, out s))
            {
                if (int.TryParse(s, out ret))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Составное наименование записи каталога данных, состоящее из значений членов класса, разделённых точкой.
        /// </summary>
        [DataMember]
        public string Name;
        /// <summary>
        /// Дата вставки записи каталога в таблицу Hmdic..ctl.
        /// </summary>
        DateTime DateInsert;
        /// <summary>
        /// Инициализация экземпляра класса.
        /// </summary>
        public Catalog(int Id, int dbListId, int paramId, int levelTypeId, int actionId, int spaceId, int geoRegId, int centerId, int formatId, int timeId, int levelValue, int predictTime, int isClimate, int gridId, int unqAttr1, string dataCnn, string actionSub, string name, DateTime dateInsert)
        {

            this.Id = Id;
            this.DbListId = dbListId;
            this.ParamId = paramId;
            this.TimeId = timeId;
            this.SpaceId = spaceId;
            this.FormatId = formatId;
            this.LevelTypeId = levelTypeId;
            this.LevelValue = levelValue;
            this.GeoRegId = geoRegId;
            this.CenterId = centerId;
            this.ActionId = actionId;
            this.PredictTime = predictTime;
            this.GridId = gridId;
            this.IsClimate = isClimate;
            this.ActionSub = actionSub;
            this.UnqAttr = unqAttr1;
            this.DataCnn = dataCnn;
            this.DateInsert = dateInsert;
            this.Name = name;
        }

        /// <summary>
        /// Копирование экземпляра класса.
        /// </summary>
        static public Catalog Copy(Catalog ctlr)
        {
            return new Catalog(ctlr.Id, ctlr.DbListId, ctlr.ParamId, ctlr.LevelTypeId, ctlr.ActionId, ctlr.SpaceId, ctlr.GeoRegId, ctlr.CenterId, ctlr.FormatId, ctlr.TimeId, ctlr.LevelValue, ctlr.PredictTime, ctlr.IsClimate, ctlr.GridId, ctlr.UnqAttr, ctlr.DataCnn, ctlr.ActionSub, ctlr.Name, ctlr.DateInsert);
        }
        /// <summary>
        /// Возвращает строку, представляющую текущий экземпляр класса.
        /// </summary>
        /// <returns>Строка, представляющая текущий экземпляр класса.</returns>
        public override string ToString()
        {
            return
                Id.ToString()
                + "act " + ActionId.ToString()
                + ".par " + ParamId.ToString()
                + ".time " + TimeId.ToString()
                + ".spac " + SpaceId.ToString()
                + ".fmt " + FormatId.ToString()
                + ".lvl" + LevelTypeId.ToString()
                + ".lvl v" + LevelValue.ToString()
                + ".greg " + GeoRegId.ToString()
                + ".cnt " + CenterId.ToString()
                + ".pt " + PredictTime.ToString()
                + ".clm " + IsClimate.ToString()
                + ".asub " + ((ActionSub != null) ? ActionSub.ToString() : "")
                + ".unq " + UnqAttr.ToString()
                + ".db " + DbListId.ToString()
            ;
        }
        /// <summary>
        /// Возвращает строку, представляющую экземпляры классов указанного массива.
        /// </summary>
        /// <returns>Строка, представляющая текущий экземпляр класса.</returns>
        static public string ToString(Catalog[] ctlRec)
        {
            string ret = ctlRec[0].ToString();
            for (int i = 1; i < ctlRec.Length; i++)
                ret += "\n" + ctlRec[i].ToString();
            return ret;
        }
        /// <summary>
        /// С "пустыми" членами класса.
        /// </summary>
        /// <returns></returns>
        static public Catalog getDummyCtlRec()
        {
            return new Catalog(int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, null, null, null, DateTime.MinValue);
        }
        /// <summary>
        /// Сравнение 2-х экземпляров.
        /// </summary>
        /// <param name="other">С чем сравниваем.</param>
        /// <param name="compareType">Принцип сравнения - по уникальному ключу 
        /// </param>
        /// <returns>0 - одинаковы в соответствие с принципом; 1 - разные в соответствие с принципом;</returns>
        public int CompareTo(Catalog other, int compareType)
        {
            switch (compareType)
            {
                case 1:
                    if (
                           DbListId == other.DbListId
                        && ParamId == other.ParamId
                        && LevelTypeId == other.LevelTypeId
                        && ActionId == other.ActionId
                        && SpaceId == other.SpaceId
                        && GeoRegId == other.GeoRegId
                        && CenterId == other.CenterId
                        && FormatId == other.FormatId
                        && TimeId == other.TimeId
                        && LevelValue == other.LevelValue
                        && ActionSub == other.ActionSub
                        && PredictTime == other.PredictTime
                        && IsClimate == other.IsClimate
                        && GridId == other.GridId
                        && UnqAttr == other.UnqAttr
                    ) return 0;
                    break;
                default:
                    throw new Exception("switch (compareType) : " + compareType);
            }
            return 1;
        }
        /// <summary>
        /// Добавить атрибут или изменить значение существующего.
        /// </summary>
        /// <param name="taskAttr">Атрибут.</param>
        /// <param name="value">Значение атрибута.</param>
        public void AddReplaceAttr(Enums.TaskAttr taskAttr, object value)
        {
            if (Attr.ContainsKey((int)taskAttr))
                Attr[(int)taskAttr] = value.ToString();
            else
                Attr.Add((int)taskAttr, value.ToString());
        }
        public string ReplaceSqlAttr(string sql)
        {
            string ret = @sql.ToLower();

            ret = ret.Replace("@hmdid", Id.ToString());
            ret = ret.Replace("@id_time", TimeId.ToString());
            ret = ret.Replace("@id_hmdic_param", ParamId.ToString());
            ret = ret.Replace("@leveltype", LevelTypeId.ToString());
            ret = ret.Replace("@levelvalue", LevelValue.ToString());
            ret = ret.Replace("@action_sub", ActionSub);
            ret = ret.Replace("@action", ActionId.ToString());	// не переставлять местами с @action_sub !
            ret = ret.Replace("@id_hmdic_grid", GridId.ToString());
            ret = ret.Replace("@id_hmdic_gr", GeoRegId.ToString()); // не переставлять местами с @id_hmdic_grid !
            ret = ret.Replace("@predict_time", PredictTime.ToString());

            ret = ReplaceCtlAttr(ret, "@georegsetid", Enums.TaskAttr.ID_GEOREG_SET, null);
            ret = ReplaceCtlAttr(ret, "@count_null", Enums.TaskAttr.COUNT_NULL, "0");
            ret = ReplaceCtlAttr(ret, "@countmindatapercent", Enums.TaskAttr.COUNT_NULL_PERC, "95");
            ret = ReplaceCtlAttr(ret, "@dateclms", Enums.TaskAttr.DATE_S_CLM, null);
            ret = ReplaceCtlAttr(ret, "@dateclmf", Enums.TaskAttr.DATE_F_CLM, null);
            ret = ReplaceCtlAttr(ret, "@parent0", Enums.TaskAttr.PARENT0, null);
            ret = ReplaceCtlAttr(ret, "@parent1", Enums.TaskAttr.PARENT1, null);

            if (ret.IndexOf("@st_index") != -1)
                throw new NotImplementedException("IndexOf(\"@st_index\") != -1");
            //GeoRectangle gr = getGeoRectangle((int)ctlRec.geoRegId);
            //ret = ret.Replace("@st_index", "'" + gr.name + "'");

            return ret;
        }
        private string ReplaceCtlAttr(string str, string param, Enums.TaskAttr taskAttr, string defValue)
        {
            string s;
            if (str.IndexOf(param) != -1)
            {
                if (Attr.TryGetValue((int)taskAttr, out s))
                {
                    if (param == "@countmindatapercent" && taskAttr == Enums.TaskAttr.COUNT_NULL_PERC)
                    {
                        return str.Replace(param, (100 - float.Parse(s)).ToString());
                    }
                    return str.Replace(param, s);
                }
                else
                {
                    if (defValue != null)
                    {
                        return str.Replace(param, defValue);
                    }
                    else
                    {
                        throw new Exception("No ctl.attr = " + taskAttr);
                    }
                }
            }
            else
            {
                return str;
            }
        }
    }
}
