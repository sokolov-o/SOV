using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Analog
{
    public class TaskCalcMetric : Common.IdName
    {
        public int FieldIdIni { get; set; }
        public int FieldIdAng { get; set; }
        public bool IsActual { get; set; }

        public int AngTimeQBack { get; set; }
        public int AngTimeQForward { get; set; }
        public IntDouble[] AngFieldTimeShiftWeights;

        public int ActionId { get; set; }
        public Climate ActionClimate { get; set; }


        public DateTime? IniDateS { get; set; }
        public DateTime? IniDateF { get; set; }
        public DateTime? AngDateS { get; set; }
        public DateTime? AngDateF { get; set; }
    }
    public class TaskCalcMetricView : TaskCalcMetric
    {
        public Field FieldIni;
        public Field FieldAng;
        public Action Action;
    }
}
