using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using FERHRI.Common;
using Npgsql;

namespace FERHRI.Analog
{
    public class TaskCalcMetricRepository : BaseRepository<TaskCalcMetric>
    {
        internal TaskCalcMetricRepository(Common.ADbNpgsql db)
            : base(db, "task.calc_metric")
        {
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new TaskCalcMetric()
            {
                Id = (int)rdr["id"],
                Name = rdr["name"].ToString(),
                FieldIdIni = (int)rdr["field_id_ini"],
                FieldIdAng = (int)rdr["field_id_ang"],
                AngTimeQBack = (int)rdr["ang_time_q_back"],
                AngTimeQForward = (int)rdr["ang_time_q_forward"],
                ActionId = (int)rdr["action_id"],
                // TODO: ActionClimate = null
                ActionClimate = null,
                AngFieldTimeShiftWeights = (IntDouble[])rdr["field_time_shift_weights"],

                IniDateS = rdr.IsDBNull(rdr.GetOrdinal("ini_date_s")) ? null : (DateTime?)rdr["ini_date_s"],
                IniDateF = rdr.IsDBNull(rdr.GetOrdinal("ini_date_f")) ? null : (DateTime?)rdr["ini_date_f"],
                AngDateS = rdr.IsDBNull(rdr.GetOrdinal("ang_date_s")) ? null : (DateTime?)rdr["ang_date_s"],
                AngDateF = rdr.IsDBNull(rdr.GetOrdinal("ang_date_f")) ? null : (DateTime?)rdr["ang_date_f"],

                IsActual = (bool)rdr["is_actual"]
            };
        }
        public int Insert(TaskCalcMetric item)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>()
            {
                {"name", item.Name},
                {"field_id_ini", item.FieldIdIni},
                {"field_id_ang", item.FieldIdAng},
                {"ang_time_q_back", item.AngTimeQBack},
                {"ang_time_q_forward", item.AngTimeQForward},
                {"action_id", item.ActionId},
                {"action_climate", null },
                {"field_time_shift_weights", item.AngFieldTimeShiftWeights},
                {"ini_date_s", item.IniDateS},
                {"ini_date_f", item.IniDateF},
                {"ang_date_s", item.AngDateS},
                {"ang_date_f", item.AngDateF},
                {"is_actual", item.IsActual}
            };
            if (item.ActionClimate != null) fields["action_climate"] = item.ActionClimate.ToIntClimate();

            return InsertWithReturn(fields);
        }

        public void Update(TaskCalcMetric item)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>()
            {
                                {"id", item.Id},
                {"name", item.Name},
                {"field_id_ini", item.FieldIdIni},
                {"field_id_ang", item.FieldIdAng},
                {"ang_time_q_back", item.AngTimeQBack},
                {"ang_time_q_forward", item.AngTimeQForward},
                {"action_id", item.ActionId},
                {"action_climate", null },
                {"field_time_shift_weights", item.AngFieldTimeShiftWeights},
                {"ini_date_s", item.IniDateS},
                {"ini_date_f", item.IniDateF},
                {"ang_date_s", item.AngDateS},
                {"ang_date_f", item.AngDateF},
                {"is_actual", item.IsActual}
            };
            if (item.ActionClimate != null) fields["action_climate"] = item.ActionClimate.ToIntClimate();
            Update(fields);
        }
        /// <summary>
        /// Выбрать задачи с указанными в запросе исходными полями и полями аналогов.
        /// </summary>
        /// <param name="fieldIniIds">Коды исходных полей. Если null - все.</param>
        /// <param name="fieldAngIds">Коды полей аналогов. Если null - все.</param>
        /// <returns>Список задач.</returns>
        public List<TaskCalcMetric> SelectByFields(List<int> fieldIniIds, List<int> fieldAngIds)
        {
            return Select(new Dictionary<string, object>() { { "field_id_ini", fieldIniIds }, { "field_id_ang", fieldAngIds } });
        }

        static public List<TaskCalcMetricView> SelectView(List<int> taskCalcMetricIds = null)
        {
            List<TaskCalcMetricView> ret = new List<TaskCalcMetricView>();
            List<TaskCalcMetric> tasks = DataManager.GetInstance().TaskCalcMetricRepository.Select(taskCalcMetricIds);
            if (tasks.Count > 0)
            {
                List<Action> actions = DataManager.GetInstance().ActionRepository.Select(tasks.Select(x => x.ActionId).ToList());
                List<Field> fields = DataManager.GetInstance().FieldRepository.Select(
                    tasks.Select(x => x.FieldIdIni)
                    .Union(tasks.Select(x => x.FieldIdAng))
                    .Distinct()
                    .ToList()
                );

                ret = tasks.Select(x => (TaskCalcMetricView)x).ToList();
                ret.ForEach(x => x.FieldIni = fields.First(y => y.Id == x.FieldIdIni));
                ret.ForEach(x => x.FieldAng = fields.First(y => y.Id == x.FieldIdAng));
                ret.ForEach(x => x.Action = actions.First(y => y.Id == x.ActionId));
            }
            return ret;
        }
    }
}
