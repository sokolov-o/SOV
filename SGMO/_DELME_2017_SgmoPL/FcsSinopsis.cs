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

namespace FERHRI.SGMO
{
    public class FcsSinopsis
    {
        /// <summary>
        /// Случайный прогноз.
        /// </summary>
        /// <param name="fcs"></param>
        /// <param name="gfsDxDy"></param>
        static internal void GetFcs(TrackFcs fcs)
        {
            Random rand = new Random();
            for (int i = 0; i < fcs.DataFcs0.DataFcs1List.Count; i++)
            {
                fcs.DataFcs0.DataFcs1List[i].Value = rand.NextDouble();
            }
        }
    }
}
