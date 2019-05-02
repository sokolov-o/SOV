using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Analog
{
    public class DataAnalog0 : Common.IdClass
    {
        public int SelectAnalogId { get; set; }
        public DateTime DateIni { get; set; }
        public List<DataAnalog1> DateWeightAnalogs { get; set; }

        public int DateWeightAnalogsCount
        {
            get
            {
                return DateWeightAnalogs.Count;
            }
        }
        public string DateWeightAnalogsString
        {
            get
            {
                if (DateWeightAnalogs == null || DateWeightAnalogs.Count == 0)
                    return null;

                string ret = DateWeightAnalogs[0].ValueString;
                for (int i = 1; i < DateWeightAnalogs.Count; i++)
                {
                    ret += ";" + DateWeightAnalogs[i].ValueString;
                }
                return ret;
            }
        }

        public class DataAnalog1
        {
            public int DataAnalog0Id { get; set; }
            public DateTime DateAnalog { get; set; }
            public double Weight { get; set; }

            static public char WEIGHT_SPLITTER = 'w';

            public string ValueString
            {
                get
                {
                    return DateAnalog.ToString() + WEIGHT_SPLITTER + Weight;
                }
            }
            public static DataAnalog1 Parse(string value)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string[] s = value.Split(WEIGHT_SPLITTER);

                    DateTime date;
                    if (!DateTime.TryParse(s[0], out date))
                        throw new Exception("Ошибка в строке для даты аналога.");

                    double weight = Common.StrVia.ParseDouble(s[1]);
                    if (weight == double.NaN)
                        throw new Exception("Ошибка в строке для веса аналога.");

                    return new DataAnalog1() { DateAnalog = date, Weight = weight };
                }
                return null;
            }
        }
    }
}
