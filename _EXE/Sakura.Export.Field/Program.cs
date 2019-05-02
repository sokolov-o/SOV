using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Sakura.Meta;
using FERHRI.Sakura.Data;

namespace Sakura.Export.Field
{
    class Program
    {
        static List<FERHRI.Sakura.Data.Field> _buf_fields;
        static string _dev = ";";
        static void Main(string[] args)
        {
            try
            {
                // INIT EXPORT FORMAT
                Dictionary<string, string> exportParams = FERHRI.Common.StrVia.ToDictionary(Settings.Default.ExportParams);

                // INIT REPOSITORIES
                int dbListIdField = (int)Enums.DbList.HmdGribMonth;
                GeoRegRepository dbGeoreg = FERHRI.Sakura.Meta.DataManager.GetInstance(Settings.Default.HmdicConnectionString).GeoRegRepository;
                CatalogRepository dbCatalog = FERHRI.Sakura.Meta.DataManager.GetInstance(Settings.Default.HmdicConnectionString).CatalogRepository;
                FieldCDVImageRepository dbField = FERHRI.Sakura.Data.DataManager.GetInstance(Settings.Default.FieldConnectionString, dbListIdField).FieldCDVImageRepository;
                FERHRI.Sakura.Meta.DataManager.ConnectionStringDefault = Settings.Default.HmdicConnectionString;

                // INIT FIELDS THROW THE CATALOGS MEMBERS
                Catalog[] catalogs = new Catalog[]
                {
                    new Catalog(-1,dbListIdField,(int)Enums.Parameter.GEOPOTEN_HEIGHT, (int)Enums.LevelType.ISOBARIC_LEVEL, (int)Enums.Action.AVG, (int)Enums.Space.FIELD,
                    1, 7, (int)Enums.Format.GRIB1, (int)Enums.TimePeriod.MONTH, 500, 0, 0, 70004, 0, null, "#", null, DateTime.Today),

                    new Catalog(-3,dbListIdField,(int)Enums.Parameter.T, (int)Enums.LevelType.ISOBARIC_LEVEL, (int)Enums.Action.AVG, (int)Enums.Space.FIELD,
                    1, 7, (int)Enums.Format.GRIB1, (int)Enums.TimePeriod.MONTH, 850, 0, 0, 70004, 0, null, "#", null, DateTime.Today),

                    new Catalog(-2,dbListIdField,(int)Enums.Parameter.GEOPOTEN_HEIGHT, (int)Enums.LevelType.ISOBARIC_LEVEL, (int)Enums.Action.AVG, (int)Enums.Space.FIELD,
                    1, 7, (int)Enums.Format.GRIB1, (int)Enums.TimePeriod.MONTH, 1000, 0, 0, 70004, 0, null, "#", null, DateTime.Today),

                    new Catalog(-3,dbListIdField,(int)Enums.Parameter.RELATIVE_HUMID, (int)Enums.LevelType.ISOBARIC_LEVEL, (int)Enums.Action.AVG, (int)Enums.Space.FIELD,
                    1, 7, (int)Enums.Format.GRIB1, (int)Enums.TimePeriod.MONTH, 850, 0, 0, 70004, 0, null, "#", null, DateTime.Today)
                };
                string[] catalogShortNames = new string[] { "H500", "T850", "H1000", "RH850" };
                if (catalogs.Length != catalogShortNames.Length)
                    throw new Exception("(catalogs.Length != catalogShortNames.Length)");

                Enums.TimePeriod timeId = (Enums.TimePeriod)catalogs[0].TimeId;
                GeoRectangle grExtract = Settings.Default.GeoregIdExtract < 0 ? null : dbGeoreg.SelectGeoReg(Settings.Default.GeoregIdExtract);

                // GET FIELDS CATALOGS
                for (int i = 0; i < catalogs.Length; i++)
                {
                    Catalog catalog = catalogs[i];
                    if (catalog.TimeId != (int)timeId) throw new Exception("catalog.TimeId != catalogs[0].TimeId for ctl.id=" + catalog.Id);

                    List<Catalog> ctls = dbCatalog.Select(catalog.DbListId, catalog.ParamId, catalog.LevelTypeId, catalog.ActionId, catalog.SpaceId, catalog.GeoRegId, catalog.CenterId, catalog.FormatId, catalog.TimeId, catalog.LevelValue, catalog.ActionSub, catalog.PredictTime, catalog.IsClimate, catalog.GridId, catalog.UnqAttr);
                    if (ctls.Count == 0) throw new Exception("ctls.Count == 0 for ctl.id=" + catalog.Id);
                    if (ctls.Count > 1) throw new Exception("ctls.Count > 1 for ctl.id=" + catalog.Id);

                    catalogs[i] = ctls[0];
                }

                // SCAN FIELDS BY DATE
                DateTime dateS = DateTimeVia.GetDateTimeStart(Settings.Default.YearS, 1, (Enums.TimePeriod)timeId);
                DateTime dateF = DateTimeVia.GetDateTimeStart(Settings.Default.YearF, DateTimeVia.GetTimeNumMax((Enums.TimePeriod)timeId), (Enums.TimePeriod)timeId);
                DateTimeVia date = new DateTimeVia(dateS, timeId, timeId);
                date.DateS = dateS;
                date.DateF = dateF;
                do
                {
                    Console.WriteLine(date.Date);

                    // READ FIELDS 4 DATE
                    List<FERHRI.Sakura.Data.Field> fields = new List<FERHRI.Sakura.Data.Field>();
                    for (int i = 0; i < catalogs.Length; i++)
                    {
                        List<FERHRI.Sakura.Data.Field> fields1 = dbField.SelectFields(catalogs[i], date.Date, 0, grExtract);

                        if (fields1.Count == 0)
                        {
                            Console.WriteLine("Catalog.Id=" + catalogs[i].Id + ": field is null.");
                            fields.Add(null);
                        }
                        else if (fields1.Count == 1)
                        {
                            Console.WriteLine("Catalog.Id=" + catalogs[i].Id + ": ok");
                            fields.Add(fields1[0]);
                        }
                        else
                            throw new Exception("Catalog.Id=" + catalogs[i].Id + ": fields count = " + fields1.Count);

                    }

                    // EXPORT FIELDS 4 DATE
                    string exportType = exportParams["EXPORT_TYPE"];
                    switch (exportType)
                    {
                        // *.csv: в строках точки поля, в столбцах последовательно значения полей со сдвигом на N месяцев назад
                        case "BIG_2018":
                            int monthesBack = int.Parse(exportParams["MONTHES_BACK"]);
                            if (_buf_fields == null) _buf_fields = new List<FERHRI.Sakura.Data.Field>(monthesBack * catalogs.Length);

                            string filePath = Settings.Default.OutputDir + "\\" + exportType +
                                "." + DateTimeVia.GetYear(date.Date, date.TimePeriod) +
                                "." + DateTimeVia.GetTimeNum(date.Date, date.TimePeriod) +
                                "." + monthesBack +
                                ".csv";
                            if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);

                            if (_buf_fields.Count == monthesBack * catalogs.Length && !_buf_fields.Contains(null))
                            {
                                System.IO.StreamWriter sr = null;
                                try
                                {
                                    sr = new System.IO.StreamWriter(filePath, false);

                                    // WRITE FILE HEADER: COLUMN NAMES
                                    sr.Write("#point" + _dev + "Lat" + _dev + "Lon");
                                    for (int i = 0; i < catalogs.Length; i++)
                                    {
                                        sr.Write(_dev + catalogShortNames[i] + " - 0");
                                        for (int k = 0; k < monthesBack; k++)
                                        {
                                            sr.Write(_dev + " - " + (k + 1));
                                        }
                                    }
                                    sr.WriteLine();

                                    // WRITE FILE BODY: DATA
                                    int pointsQ = fields[0].Value.Length;
                                    List<GeoPoint> geoPoints = fields[0].Grid.GetGeoPoints();

                                    for (int j = 0; j < pointsQ; j++)
                                    {
                                        sr.Write(j + _dev + geoPoints[j].LatGrd + _dev + geoPoints[j].LonGrd);
                                        for (int i = 0; i < catalogs.Length; i++)
                                        {
                                            sr.Write(_dev + fields[i].Value[j]);

                                            for (int k = 0; k < monthesBack; k++)
                                            {
                                                int ii = _buf_fields.Count - catalogs.Length + i - k * catalogs.Length;
                                                sr.Write(_dev + _buf_fields[ii].Value[j]);
                                            }
                                        }
                                        sr.Write(_dev + '\n');
                                    }
                                }
                                finally
                                {
                                    if (sr != null) sr.Close();
                                }
                            }
                            for (int i = 0; i < catalogs.Length; i++)
                            {
                                AddBufFields(fields[i], monthesBack * catalogs.Length);
                            }
                            break;
                        default:
                            throw new Exception("Unknown export type " + exportParams["export_type"]);
                    }

                } while (date.NextTimePeriod());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Press Enter...");
                Console.ReadLine();
            }
        }
        static void AddBufFields(FERHRI.Sakura.Data.Field field, int bufSize)
        {
            if (_buf_fields.Count > 0)
                if (_buf_fields[0].Value.Length != field.Value.Length)
                    throw new Exception("field0.Value.Length != pointsQ");

            _buf_fields.Add(field);
            if (_buf_fields.Count > bufSize)
                _buf_fields.RemoveAt(0);
        }
    }
}
