using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Amur.Meta;
using FERHRI.Geo;

namespace FERHRI.SGMO
{
    /// <summary>
    /// Выборка прогнозов опасных явлений по регионам
    /// из файла формата WW.VVO.VAN.
    /// </summary>
    public partial class FcsWave
    {
        readonly int?[] AmurCatalogIdXVanWaveParam = new int?[]
        {
            // Смешанное: высота, период, длина, направление
            1028,null,null,            1035,
            // Ветровое: высота, период, длина, направление
            1033,1026,null,1032,
            // Зыбь 1-й системы: высота, период, длина, направление
            1027,1049,null,1050,
            // Зыбь 2-й системы: высота, период, длина, направление
            1111,null,null,null
        };

        public static object GetWaveDangerVAN(DateTime dateIni, List<Geo.GeoRectangle> grs, int catalogIdWaveH, double waveHWarningGE, string waveFcsFilePath)
        {
            object[/*input GeoRectangle index*/][/*GeoPoint belonged to region, DateTime, max wave height (gdheight)*/] ret = new object[grs.Count][];

            // Parse wave file (get all data from file)
            Dictionary<GeoPoint, Dictionary<TimeSpan/*FcsTime*/, double[/*WaveParams*/]>> data = FERHRI.DB.VanRepository.ReadData(dateIni);

            // GEORECT LOOP
            for (int iGeoRect = 0; iGeoRect < grs.Count; iGeoRect++)
            {
                // FCS GRID NODES LOOP
                Dictionary<int, double> danger = new Dictionary<int, double>();
                for (int iGeoPoint = 0; iGeoPoint < data.Count; iGeoPoint++)
                {
                    Geo.GeoPoint node = data.ElementAt(iGeoPoint).Key;
                    if (!grs[iGeoRect].IsPointBelong(node)) continue;

                    // FCS-TIME LOOP
                    foreach (KeyValuePair<TimeSpan, double[]> kvp in data.ElementAt(iGeoPoint).Value)
                    {
                        double waveHMix = kvp.Value[0];
                        if (waveHMix >= waveHWarningGE)
                        {
                            object[] gdheight = ret[iGeoRect] ?? new object[] { node, kvp.Key, waveHMix };
                            if ((double)gdheight[2] < waveHMix)
                            {
                                gdheight[0] = node;
                                gdheight[1] = kvp.Key;
                                gdheight[2] = waveHMix;
                            }
                        }
                    }
                }
            }
            return ret;
        }
    }
}
