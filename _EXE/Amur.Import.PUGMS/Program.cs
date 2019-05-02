using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amur.Import.PUGMSServiceReference;
using Amur.Import.AmurServiceReference;

namespace Amur.Import
{
    /// <summary>
    /// 
    /// Импорт данных БД ПУГМС -> БД Амур
    /// http://192.168.116.90:8033/HydroService.svc
    /// 
    /// Нотация префикса наименований в коде: 
    ///     p - Hydro DB ПУГМС , 
    ///     a - Amur DB ДВНИГМИ.
    /// 
    /// OSokolov@201709
    /// </summary>
    class Program
    {
        internal static HydroServiceClient hClient = new HydroServiceClient();
        internal static ServiceClient aClient = new ServiceClient();
        internal static long aHandle = aClient.Open("OSokolov", "qq");

        static void Main(string[] args)
        {
            AmurServiceReference.Curve acurve = Program.aClient.GetCurveByCatalog(Program.aHandle, 1191420, 1191421,
                new List<DateTime> { DateTime.Parse("9.08.2016"), DateTime.Parse("11.08.2016") });

            //var site = hClient.GetSite("5160", 2, false);
            //Curve curve = hClient.GetCurve(site.SiteId, 2, 14);

            // Получить П-сайты, имеющие кривые.
            Dictionary<PUGMSServiceReference.Site, PUGMSServiceReference.Curve> psitesCurves = PUGMSDB.RepositoryPUGMS.GetSitesCurves();

            // Получить соответствующие им А-сайты
            Dictionary<AmurServiceReference.Site, PUGMSServiceReference.Curve> asitesCurves = AMURDB.RepositoryAmur.GetSitesCurves(psitesCurves);

            // Обновить кривые БД Амур
            AMURDB.RepositoryAmur.InsertUpdateDbAmur(asitesCurves);

            Console.WriteLine("Press ENTER...");
            Console.ReadLine();
        }
    }
}
