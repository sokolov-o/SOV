using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOV.WcfService.Field.AmurServiceReference;
using SOV.SGMO;
using SOV.Amur.Meta;

namespace SOV.WcfService.Field
{
    /// <summary>
    /// Метод с расширенными атрибутами.
    /// Используется для хранения методов и их сопутствующих атрибутов, допустимых в сервисе.
    /// </summary>
    public class MethodExt 
    {
        public Method Method { get; set; }
        public List<MethVaroffXGrib2> MethVaroffXGrib2 { get; set; }
        public MethodForecast MethodForecast { get; set; }
    }
}