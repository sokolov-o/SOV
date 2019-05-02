using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Meta;

namespace SOV.SGMO
{
    /// <summary>
    /// Значение/ограничения предупреждения
    /// </summary>
    public class PileItem
    {
        public int Id { get; set; }
        public PileSet PileSet { get; set; }
        public string NameRus { get; set; }
        public string NameEng { get; set; }

        /// <summary>
        /// Период времени года, в течение которого действует значение градации.
        /// </summary>
        public int TimeId { get; set; }
        /// <summary>
        /// Сортировка градаций. 
        /// Для предупреждений, это порядок предупреждения - чем выше, тем суровее.
        /// </summary>
        public int OrderBy { get; set; }

        public string Condition1 { get; set; }
        public double Value1 { get; set; }
        public string Condition2 { get; set; }
        public double? Value2 { get; set; }

        /// <summary>
        /// Получить значения из указанных, удовлетворяющие критерию пайла.
        /// </summary>
        /// <param name="dataValues"></param>
        /// <param name="oneExtremalValueOnly">Вернуть одно, экстремальное, значение из массива значений, удовлетворяющих критерию предупреждения, или все?</param>
        /// <returns>Пустой список, если значения не попадают в градации предупреждения.</returns>
        public List<double> GetPileValues(double[] dataValues, bool oneExtremalValueOnly)
        {
            List<double> ret = new List<double>();
            foreach (var item in GetPileValuesIndeces(dataValues, oneExtremalValueOnly))
            {
                ret.Add(dataValues[(int)item]);
            }
            return ret;
        }
        public List<double> GetPileValuesIndeces(double[] dataValues, bool oneExtremalValueOnly)
        {
            List<double> ret = new List<double>();
            for (int i = 0; i < dataValues.Length; i++)
            {
                if (IsValueMeetTheCriterions(dataValues[i]))
                {
                    if (oneExtremalValueOnly)
                    {
                        if (ret.Count == 0)
                            ret.Add(i);
                        else if (IsValueMoreExtremally(dataValues[i], ret[0]))
                            ret[0] = i;
                    }
                    else
                    {
                        ret.Add(i);
                    }
                }
            }
            return ret;
        }

        public bool IsValueMoreExtremally(double value, double value2)
        {
            switch (Condition1)
            {
                case ">":
                case ">=":
                    return value > value2;
                case "<":
                case "<=":
                    return value < value2;
                case "==":
                    return false;
                default:
                    throw new Exception("Неизвестный тип условия '" + Condition1 + "' для критерия.");
            }
        }
        public bool IsValueMeetTheCriteria(string condition, double criteriaValue, double value)
        {
            switch (condition)
            {
                case ">":
                    return value > criteriaValue;
                case ">=":
                    return value >= criteriaValue;
                case "<":
                    return value < criteriaValue;
                case "<=":
                    return value <= criteriaValue;
                case "==":
                    return value == criteriaValue;
                default:
                    throw new Exception("Неизвестный тип условия '" + condition + "' для критерия.");
            }
        }
        public bool IsValueMeetTheCriterions(double value)
        {
            return double.IsNaN(value) ? false :
                Value2.HasValue
                ? IsValueMeetTheCriteria(Condition1, Value1, value) && IsValueMeetTheCriteria(Condition2, (double)Value2, value)
                : IsValueMeetTheCriteria(Condition1, Value1, value);
        }

        public string ToString(EnumLanguage lang, string unitShortNameEng = null)
        {
            return (lang == EnumLanguage.Rus ? this.NameRus : this.NameEng) +
                " (" +
                Condition1 + " " + Value1 + (string.IsNullOrEmpty(unitShortNameEng) ? "" : unitShortNameEng) +
                (Value2.HasValue ? " - " + Condition2 + " " + Value2 + (string.IsNullOrEmpty(unitShortNameEng) ? "" : unitShortNameEng) : "") +
                ")"
                ;
        }
    }
}
