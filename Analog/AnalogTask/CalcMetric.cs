using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FERHRI.DB;
using FERHRI.MetaDb;

namespace FERHRI.Analog.Task
{
    /// <summary>
    /// Расчёт и запись метрик аналогичности.
    /// </summary>
    public class CalcMetric
    {
        static public void Run(TaskCalcMetric task)
        {
            DateTime runStartAt = DateTime.Now;
            Console.WriteLine("CalcMetric.Run({0}) started at {1}", task.Id, runStartAt);

            try
            {
                TaskCalcMetricView taskView = TaskCalcMetricRepository.SelectView(new List<int>() { task.Id })[0];

                // GET FIELD CATALOG & REPOSITORY
                DbInterface dbi = MetaDb.DataManager.GetInstance().DbInterfaceRepository.Select(taskView.FieldGeoobsView.Field.CatalogDbInterfaceId);
                Db dbCatalog = MetaDb.DataManager.GetInstance().DbListRepository.Select(dbi.DbListId);
                Catalog catalogField = Factory.GetCatalog(dbCatalog, taskView.FieldGeoobsView.Field.CatalogId);

                object repField = Factory.GetRepository(dbCatalog, catalogField);

                // Fields data times list

                List<DateTime> dates = null;
                if (repField.GetType().GetInterface(typeof(IDataTimePeriodSF).ToString()) == null)
                    throw new Exception("База данных полей не имеет интерфейса IDataTimePeriodSF.");

                dates = ((IDataTimePeriodSF)repField).GetDataAllDateList(catalogField.Id);
                if (dates.Count == 0)
                    throw new Exception("Данные для запрошенного поля отсутствуют в базе данных.");

                DateTime[] dateSF = new DateTime[] { dates.Min(x => x), dates.Max(x => x) };
                if (dateSF == null)
                    throw new Exception("Данные для запрошенного поля отсутствуют в базе данных.");
                int? yearS = taskView.FieldGeoobsView.Field.YearS;
                if (yearS.HasValue && dateSF[0].Year < yearS) dateSF[0] = new DateTime((int)yearS, 1, 1);

                dates.RemoveAll(x => x < dateSF[0] || x > dateSF[1]);
                dates = dates.OrderBy(x => x).ToList();

                // SCAN FIELDS

                for (int i = 0; i < dates.Count; i++)
                {
                    DateTime date = dates[i];
                    Console.Write(date);

                    List<FERHRI.Field> dataFields = ((IField)repField).GetFields(catalogField.NativeCatalog, date, null, 0);
                    Console.WriteLine(" - field " + ((dataFields.Count > 0) ? "ok." : "not exists"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString(), "Ошибка при выполнении расчёта и записи метрик аналогичнсоти.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Console.WriteLine("CalcMetric.Run({0}) ended. Elapsed {1:F} minutes.", task.Id, (DateTime.Now - runStartAt).TotalMinutes);
            }
        }
    }
}
