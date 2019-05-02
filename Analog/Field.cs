using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Common;

namespace FERHRI.Analog
{
    /*
        Записи полей каталога Sakura, использовавшиеся до 2018 г.
        для расчёта метрик аналогичности.
      
        18108    H.ISO.1000.HOUR.0.FIELD.Earth.0.VALUE.#.20 [18108]
        18110    H.ISO.850.HOUR.0.FIELD.Earth.0.VALUE.#.20 [18110]
        18111    H.ISO.500.HOUR.0.FIELD.Earth.0.VALUE.#.20 [18111]
        18112    TMP.ISO.850.HOUR.0.FIELD.Earth.0.VALUE.#.20 [18112]
        29282    H.ISO.700.HOUR.0.FIELD.Earth.0.VALUE.#.20 [29282]
        36669    TMP.ISO.700.HOUR.0.FIELD.Earth.0.VALUE.#.20 [36669]

        18156    H.ISO.1000.Месяц.0.FIELD.Earth.0.AVG.#.5 [18156]
        21736    TMP.ISO.1000.Месяц.0.FIELD.Earth.0.AVG.#.5 [21736]
        29292    H.ISO.700.Месяц.0.FIELD.Earth.0.AVG.#.5 [29292]
        29296    H.ISO.500.Месяц.0.FIELD.Earth.0.AVG.#.5 [29296]
        29319    TMP.ISO.850.Месяц.0.FIELD.Earth.0.AVG.#.5 [29319]
        29320    TMP.ISO.700.Месяц.0.FIELD.Earth.0.AVG.#.5 [29320]
        29322    TMP.ISO.500.Месяц.0.FIELD.Earth.0.AVG.#.5 [29322]
        29379    P.MSL.0.Месяц.0.FIELD.Earth.0.AVG.#.5 [29379]
        278777  OT.IL.500.Месяц.0.FIELD.Earth.0.VALUE.1000.5 [278777]
    */
    public class Field : IdName
    {
        //public int TimeQBack { get; set; }
        //public int TimeQForward { get; set; }
        //public int? YearS { get; set; }
        //public int ActionId { get; set; }
        public int CatalogId { get; set; }
        public int CatalogDbInterfaceId { get; set; }
        //public IntDouble[] FieldTimeShiftWeights;
        //public Climate Climate { get; set; }
    }
}
