using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amur.Import;
using Amur.Import.AmurServiceReference;

namespace Amur.Import.AMURDB
{
    public enum EnumStationType
    {
        MeteoStation = 1,
        HydroPost = 2,
        MorePost = 3,
        GaugingPost = 10,
        AHK = 6,
        AMK = 5
    }
    public enum EnumVariable
    {
        GageHeight = 2,
        Discharge = 14
    }
    public enum EnumLegalEntity
    {
        PUGMS = 243,
        FERHRI = 777
    }
    public enum EnumMethod
    {
        ObservationInSitu = 0,
        Operator = 5,
        /// <summary>
        /// Интерполяция с кривой связи Q=f(H)
        /// </summary>
        InterpCurve = 2,
        /// <summary>
        /// Прогноз Романского, Вербицкой, 2016
        /// </summary>
        WRFARW_HBRK15 = 100,
        /// <summary>
        /// Прогноз волнения по региону
        /// Тихий океан с шагом 0.5 град (Вражкин А.Н.)
        /// </summary>
        WAVE_VVO_PACIFIC_0p5 = 105,
        /// <summary>
        /// Прогноз GFS 0.5 grad
        /// </summary>
        GFS = 102,
        /// <summary>
        /// Прогноз синопика
        /// </summary>
        Sinopsis = 106
    }
    public enum EnumOffsetType
    {
        NoOffset = 0,
        /// <summary>
        /// Маршрут поле-лес
        /// </summary>
        MarshrutPoleLes = 1,
        /// <summary>
        /// Уровень моря
        /// </summary>
        MSL = 101,
        /// <summary>
        /// Уровень над поверхностью земли.
        /// </summary>
        HeightAboveEarth = 100,
        /// <summary>
        /// Поверхность земли или воды.
        /// </summary>
        SurfaceEarthOrWater = 102,
        /// <summary>
        /// Окрестность станции
        /// </summary>
        StationNearby = 103
    }

    public class RepositoryAmur
    {
        /// <summary>
        /// Получить гидропосты, соответствующеи постам ПУГМС.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<AmurServiceReference.Site, PUGMSServiceReference.Curve> GetSitesCurves(Dictionary<PUGMSServiceReference.Site, PUGMSServiceReference.Curve> psiteCurves)
        {
            Console.Write("RepositoryAmur.GetSitesCurves...");
            Dictionary<AmurServiceReference.Site, PUGMSServiceReference.Curve> ret = new Dictionary<AmurServiceReference.Site, PUGMSServiceReference.Curve>();
            List<PUGMSServiceReference.Site> psitesNotInAmur = new List<PUGMSServiceReference.Site>();

            List<AmurServiceReference.Station> astations = Program.aClient.GetStationsByIndices(Program.aHandle, psiteCurves.Select(x => x.Key.SiteCode).Distinct().ToList());
            foreach (KeyValuePair<PUGMSServiceReference.Site, PUGMSServiceReference.Curve> psiteCurve in psiteCurves)
            {
                if (psiteCurve.Value != null)
                {
                    List<AmurServiceReference.Site> asite = Program.aClient.GetSitesByStation(Program.aHandle, astations.First(x => x.Code == psiteCurve.Key.SiteCode).Id, (int)EnumStationType.HydroPost);
                    if (asite == null)
                    {
                        psitesNotInAmur.Add(psiteCurve.Key);
                        string msg = string.Format("В БД Амур отсутствует станция {0} {1}.", psiteCurve.Key.SiteCode, psiteCurve.Key.Name);
                        continue;
                    }
                    if (asite.Count != 1)
                    {
                        string msg = string.Format("В БД Амур для станции {0} {1} обнаружено {1} пунктов.", psiteCurve.Key.SiteCode, psiteCurve.Key.Name, asite.Count);
                        throw new Exception(msg);
                    }
                    ret.Add(asite[0], psiteCurve.Value);
                }
            }
            Console.WriteLine(" {0} sites with curves.", ret.Count);
            if (psitesNotInAmur.Count > 0)
                throw new Exception("(psitesNotInAmur.Count > 0)");
            return ret;
        }

        public static void InsertUpdateDbAmur(Dictionary<AmurServiceReference.Site, PUGMSServiceReference.Curve> asitesCurves)
        {
            foreach (KeyValuePair<AmurServiceReference.Site, PUGMSServiceReference.Curve> asitesCurve in asitesCurves)
            {
                // PUGMS CURVE -> AMUR CURVE
                Curve pcurve = Parse(asitesCurve.Key, asitesCurve.Value);

                // Создать или выбрать кривую из БД АМУР
                Curve acurve = Program.aClient.GetCurveByCatalog(Program.aHandle, pcurve.CatalogIdX, pcurve.CatalogIdY, null);
                if (acurve == null)
                {
                    Program.aClient.SaveCurve(Program.aHandle, pcurve);
                    acurve = Program.aClient.GetCurveByCatalog(Program.aHandle, pcurve.CatalogIdX, pcurve.CatalogIdY, null);
                }

                foreach (var pseria in pcurve.Series)
                {
                    Curve.Seria aseria = acurve.Series == null ? null : acurve.Series.FirstOrDefault(x => x.DateS == pseria.DateS);
                    if (aseria == null)
                    {
                        aseria = new Curve.Seria()
                        {
                            CurveId = acurve.Id,
                            DateS = pseria.DateS,
                            Description = pseria.Description,
                            Coefs = pseria.Coefs,
                            Points = pseria.Points
                        };
                        Program.aClient.SaveCurveSeries(Program.aHandle, new List<Curve.Seria>() { aseria });
                    }
                    else
                    {
                        Program.aClient.UpdateCurveSeria(Program.aHandle,
                            aseria.Id, pseria.DateS,
                            string.IsNullOrEmpty(pseria.Description) ? aseria.Description : pseria.Description,
                            pseria.Points, pseria.Coefs);
                    }
                }
            }
        }

        private static bool Equal(List<Curve.Seria.Point> points1, List<Curve.Seria.Point> points2)
        {
            if (points1.Count != points2.Count)
                return false;
            for (int i = 0; i < points1.Count; i++)
            {
                if (points1[i].X != points2[i].X || points1[i].Y != points2[i].Y)
                    return false;
            }
            return true;
        }

        private static bool Equal(List<Curve.Seria.Coef> coefs1, List<Curve.Seria.Coef> coefs2)
        {
            if (coefs1.Count != coefs2.Count)
                return false;
            for (int i = 0; i < coefs1.Count; i++)
            {
                if (coefs1[i].Day != coefs2[i].Day || coefs1[i].Month != coefs2[i].Month || coefs1[i].Value != coefs2[i].Value)
                    return false;
            }
            return true;
        }

        private static Curve Parse(AmurServiceReference.Site asite, PUGMSServiceReference.Curve pcurve)
        {
            // PARSE CURVE
            Catalog catalogX = new Catalog()
            {
                Id = -1,
                SiteId = asite.Id,
                VariableId = (int)EnumVariable.GageHeight,
                OffsetTypeId = (int)EnumOffsetType.NoOffset,
                MethodId = (int)EnumMethod.ObservationInSitu,
                SourceId = (int)EnumLegalEntity.PUGMS,
                OffsetValue = 0
            };
            Catalog catalogY = new Catalog()
            {
                Id = -1,
                SiteId = asite.Id,
                VariableId = (int)EnumVariable.Discharge,
                OffsetTypeId = (int)EnumOffsetType.NoOffset,
                MethodId = (int)EnumMethod.ObservationInSitu,
                SourceId = (int)EnumLegalEntity.PUGMS,
                OffsetValue = 0
            };
            Catalog catalog = Program.aClient.GetCatalog(Program.aHandle, catalogX.SiteId, catalogX.VariableId, catalogX.OffsetTypeId, catalogX.MethodId, catalogX.SourceId, catalogX.OffsetValue);
            if (catalog == null)
            {
                catalogX = Program.aClient.SaveCatalog(Program.aHandle, catalogX);
                Console.WriteLine("Создана запись каталога для сайта {0} переменной {1}", catalogX.SiteId, catalogX.VariableId);
            }
            else
                catalogX = catalog;
            catalog = Program.aClient.GetCatalog(Program.aHandle, catalogY.SiteId, catalogY.VariableId, catalogY.OffsetTypeId, catalogY.MethodId, catalogY.SourceId, catalogY.OffsetValue);
            if (catalog == null)
            {
                catalogY = Program.aClient.SaveCatalog(Program.aHandle, catalogY);
                Console.WriteLine("Создана запись каталога для сайта {0} переменной {1}", catalogY.SiteId, catalogY.VariableId);
            }
            else
                catalogY = catalog;

            // PARSE SERIES

            int dateSCount = pcurve.Serieses.GroupBy(x => x.BeginDate).Count();
            if (dateSCount != pcurve.Serieses.Count)
            {
                if (pcurve.Serieses.Count == 2)
                {
                    PUGMSServiceReference.CurveSerie pseria = (!pcurve.Serieses[1].EndDate.HasValue) ? pcurve.Serieses[1] : pcurve.Serieses[0];
                    pcurve.Serieses = new List<PUGMSServiceReference.CurveSerie>() { pseria };

                    Console.WriteLine("ОШИБКА: дубли в датах серий. Взята одна из двух серий.");
                }
                else
                    throw new Exception("ОШИБКА: дубли в датах серий.");
            }
            List<Curve.Seria> aseries = new List<Curve.Seria>();
            foreach (PUGMSServiceReference.CurveSerie pseria in pcurve.Serieses)
            {
                // CHECK PSERIA
                if (pseria.Values != null && pseria.Values.Count > 0)
                    throw new Exception("(pseria.Values != null && pseria.Values.Count > 0)");

                // ADD ASERIA
                aseries.Add(new Curve.Seria()
                {
                    Id = -1,
                    CurveId = -1,
                    DateS = (DateTime)pseria.BeginDate,
                    Coefs = Parse(pseria.Coeffs),
                    Points = Parse(pseria.Points),
                    Description = "Imported from PUGMS curve.seria.Id = " + pseria.Id + " (" + DateTime.Today + ")."
                });
            }

            // RETURN 
            return new Curve()
                {
                    Id = -1,
                    CatalogIdX = catalogX.Id,
                    CatalogIdY = catalogY.Id,
                    Series = aseries,
                    Description = "Imported from PUGMS curve.Id = " + pcurve.Id + " (" + DateTime.Today + ")."
                };
        }
        private static List<Curve.Seria.Coef> Parse(List<PUGMSServiceReference.CurveSeriesCoeff> pcoefs)
        {
            if (pcoefs != null && pcoefs.Count > 0)
            {
                List<Curve.Seria.Coef> ret = new List<Curve.Seria.Coef>();
                foreach (var pcoef in pcoefs)
                {
                    ret.Add(new Curve.Seria.Coef()
                        {
                            Day = pcoef.Day,
                            Month = pcoef.Month,
                            Value = pcoef.Value
                        });
                }
                return ret;
            }
            return null;
        }
        private static List<Curve.Seria.Point> Parse(List<PUGMSServiceReference.CurvePoint> ppoints)
        {
            if (ppoints != null && ppoints.Count > 0)
            {
                List<Curve.Seria.Point> ret = new List<Curve.Seria.Point>();
                foreach (var ppoint in ppoints)
                {
                    ret.Add(new Curve.Seria.Point()
                    {
                        X = ppoint.X,
                        Y = ppoint.Y
                    });
                }
                return ret;
            }
            return null;
        }
    }
}
