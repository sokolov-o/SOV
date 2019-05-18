using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.SGMO;
using FERHRI.DB;
using FERHRI.Geo;
using FERHRI.Grib;
using FERHRI.Amur.Meta;
using FERHRI.Common;

namespace FERHRI.SGMO
{
    public class FcsGFS
    {
        static internal void GetFcs(TrackFcs fcs, double gfsDxDy)
        {
            // GFS FILTER
            List<Grib2XVaroff> g2vs = DataManager.GetInstance().Grib2XVariableRepository.Select("amur", fcs.MethodForecast.Method.Id);
            if (g2vs == null) return;
            List<Grib2Filter> g2Filter = g2vs.Select(x => x.Grib2Filter).ToList();

            // SCAN LAGS

            GfsRepository gfs = new GfsRepository(gfsDxDy);
            List<GeoPoint> points = fcs.PointLags.Select(x => x.Point).ToList();

            for (int iLag = 0; iLag < fcs.MethodForecast.Lags.Length; iLag++)
            {
                double lag = fcs.MethodForecast.Lags[iLag];

                #region READ GFS
                Console.Write("Select GFS lag {0}...", fcs.MethodForecast.Lags[iLag]);
                DateTime date0 = DateTime.Now;

                double[/*g2filter*/][/*points*/] fcsValues = gfs.SelectValuesAtPoints(
                    g2Filter, fcs.DataFcs0.DateIniUTC, (int)lag, points, fcs.PointNearestType, fcs.PointsDistanceType);

                Console.WriteLine(" -> {0} seconds elapsed", (DateTime.Now - date0).TotalSeconds);
                #endregion READ GFS

                // SCAN VAROFFS
                for (int iVar = 0; iVar < fcs.Varoffs.Count; iVar++)
                {
                    EnumVaroff varoff = (EnumVaroff)fcs.Varoffs[iVar].Id;
                    double[/*iPoint*/] values;
                    var dm = FERHRI.Amur.Meta.DataManager.GetInstance();
                    var vriable = dm.VariableRepository.Select(fcs.Varoffs[iVar].VariableId);
                    switch (varoff)
                    {
                        case EnumVaroff.WSpeedFcs:
                        case EnumVaroff.WDirFcs:
                            double[/*point*/] uValues = fcsValues[g2Filter.IndexOf(GetGrib2Filter(EnumVaroff.U10mFcs, g2vs))];
                            double[] vValues = fcsValues[g2Filter.IndexOf(GetGrib2Filter(EnumVaroff.V10mFcs, g2vs))];
                            if (uValues != null && vValues != null)
                            {
                                double[] wmod = Common.Vector.uv2Module(uValues, vValues);
                                double[] wdir = Common.Vector.uv2Azimuth(uValues, vValues);

                                values = new double[points.Count];
                                for (int iPoint = 0; iPoint < points.Count; iPoint++)
                                {
                                    values[iPoint] = fcs.Varoffs[iVar].Id == (int)EnumVaroff.WSpeedFcs ? wmod[iPoint] : wdir[iPoint];
                                }
                            }
                            else
                                values = null;
                            break;
                        case EnumVaroff.WGustFcs:
                        case EnumVaroff.RH2mFcs:
                        case EnumVaroff.PmslFcs:
                        case EnumVaroff.SSTFcs:
                        case EnumVaroff.TaFcs:
                        case EnumVaroff.RR3hFcs:
                            values = fcsValues[g2Filter.IndexOf(GetGrib2Filter(varoff, g2vs))];
                            if (values != null)
                            {
                                if (varoff == EnumVaroff.TaFcs || varoff == EnumVaroff.SSTFcs) Support.Add(values, Phisics.AbsZeroInCelsius);
                                if (varoff == EnumVaroff.PmslFcs) Support.Multiply(values, 0.01);
                            }

                            Console.WriteLine("\tGFS data for varoff={0} \tis {1}", varoff, (values != null) ? "ok" : "null");
                            break;

                        default:
                            values = null; break;//throw new Exception("Неизвестная прогностическая переменная varoff=" + varoff);
                    }

                    if (values == null)
                        values = Support.Allocate(points.Count, double.NaN);
                    for (int iPoint = 0; iPoint < points.Count; iPoint++)
                    {
                        DataFcs1.SetValue(fcs.DataFcs0.DataFcs1List, fcs.DataFcs0.Id, points[iPoint], lag, (int)varoff, values[iPoint]);
                    }
                } // VAROFF
            } // LAG

            // Process precipitation
            foreach (var point in points)
            {
                if (point != null)
                {
                    List<DataFcs1> data = fcs.DataFcs0.DataFcs1List.Where(x => x.Point == point && x.VaroffId == (int)EnumVaroff.RR3hFcs).OrderBy(x => x.Lag).ToList();
                    double[] precs = data.Select(x => x.Value).ToArray();
                    double[] lags = data.Select(x => x.Lag).ToArray();
                    Console.WriteLine("GFS Precipitations by lags: {0}", StrVia.ToString(precs));
                    Console.WriteLine("Lags: {0}", StrVia.ToString(lags));

                    double[] precs4LagStep = DB.GFS.GetPrecip4LagStep(precs, lags);
                    Console.WriteLine("GFS Precipitations 4LagStep: {0}", StrVia.ToString(precs4LagStep));

                    for (int i = 0; i < data.Count(); i++)
                    {
                        data[i].Value = precs4LagStep[i];
                    }
                }
                else
                {
                    Console.WriteLine("point is null");
                }
            }
        }
        static void ProcessPrecip(TrackFcs fcs)
        {
        }
        static Grib2Filter GetGrib2Filter(EnumVaroff varoffId, List<Grib2XVaroff> g2v)
        {
            // GET
            List<Grib2XVaroff> g2v_ = g2v.FindAll(x => x.VaroffId == (int)varoffId);

            // TEST
            if (g2v_ == null || g2v_.Count == 0) throw new Exception("(g2v_ == null || g2v_.Count == 0) for variableId=" + varoffId);
            if (g2v_.Count > 1) throw new Exception("(g2v_.Count > 1) for variableId=" + varoffId);

            // RETURN
            return g2v_[0].Grib2Filter;
        }
    }
}
