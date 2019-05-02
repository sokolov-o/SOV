using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.SGMO
{
    public enum EnumVaroff
    {
        WDirFcs = 1,
        WSpeedFcs = 2,
        TaFcs = 3,
        /// <summary>
        /// Относительная влажность (%)
        /// </summary>
        RH2mFcs = 4,
        PmslFcs = 5,
        WGustFcs = 6,
        RR3hFcs = 7,
        SSTFcs = 8,
        U10mFcs = 9,
        V10mFcs = 10,
        PrecipPhase = 22
    }
    public class MethvarXGrib2
    {
        /// <summary>
        /// Прогностическая переменная.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Наименование источника справочника для переменной.
        /// Например, ferhri.amur, hbr.amur, sakura, etc.
        /// </summary>
        public string SrcName { get; set; }
        public int MethodId { get; set; }
        public int VariableId { get; set; }
        public int OffsetTypeId { get; set; }
        public double OffsetValue { get; set; }

        public Grib.Grib2Filter Grib2Filter;

        public bool ExcludeFromFcs { get; set; }
        public bool IsActual { get; set; }
    }
}
