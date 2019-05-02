using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amur.Import;
using Amur.Import.PUGMSServiceReference;

namespace Amur.Import.PUGMSDB
{
    public enum EnumSiteType
    {
        HydroPost = 2
    }
    public enum EnumVariable
    {
        GageHeight = 2,
        Discharge = 14
    }

    public class RepositoryPUGMS
    {
        /// <summary>
        /// Получить гидропосты с кривыми.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<PUGMSServiceReference.Site, PUGMSServiceReference.Curve> GetSitesCurves()
        {
            Console.Write("RepositoryPUGMS.GetSitesCurves...");
            Dictionary<Site, PUGMSServiceReference.Curve> ret = new Dictionary<Site, PUGMSServiceReference.Curve>();

            List<PUGMSServiceReference.Site> psites = Program.hClient.GetSiteList((int)EnumSiteType.HydroPost);
            foreach (PUGMSServiceReference.Site psite in psites)
            {
                PUGMSServiceReference.Curve curve = Program.hClient.GetCurve(psite.SiteId, (int)EnumVariable.GageHeight, (int)EnumVariable.Discharge);
                if (curve != null)
                {
                    ret.Add(psite, curve);
                    // T.ODO: ATTENTION - one site for testing only.
                    //break;
                }
            }
            Console.WriteLine(" {0} sites. {1} with curves, {2} curve series.", psites.Count, ret.Count, ret.Sum(x => x.Value.Serieses.Count));
            return ret;
        }

    }
}
