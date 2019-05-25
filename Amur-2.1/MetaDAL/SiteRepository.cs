using System;
using System.Collections.Generic;
using System.Linq;
using SOV.Common;
using Npgsql;

namespace SOV.Amur.Meta
{
    public class SiteRepository : BaseRepository<Site>
    {
        internal SiteRepository(Common.ADbNpgsql db) : base(db, "meta.site_latlon_view") { }

        public static List<Site> GetCash()
        {
            return GetCash(Meta.DataManager.GetInstance().SiteRepository);
        }
        protected override object ParseData(NpgsqlDataReader rdr)
        {
            return new Site()
            {
                Id = (int)rdr["id"],
                ParentId = ADbNpgsql.GetValueInt(rdr, "parent_id"),
                TypeId = (int)rdr["site_type_id"],
                Code = rdr["code"].ToString(),
                Description = ADbNpgsql.GetValueString(rdr, "description"),
                AddrRegionId = ADbNpgsql.GetValueInt(rdr, "addr_region_id"),
                Name = rdr["name"].ToString(),
                OrgId = ADbNpgsql.GetValueInt(rdr, "org_id"),
                Lat = ADbNpgsql.GetValueDouble(rdr, "latitude"),
                Lon = ADbNpgsql.GetValueDouble(rdr, "longitude")
            };
        }

        /// <summary>
        /// Создать пункт.
        /// </summary>
        /// <param name="site"></param>
        /// <returns>New id.</returns>
        public int Insert(Site site)
        {
            string tableNameCur = TableName;
            try
            {
                TableName = "meta.site";
                var fields = new Dictionary<string, object>()
                {
                    {"parent_id", site.ParentId},
                    {"site_type_id", site.TypeId},
                    {"code", site.Code},
                    {"description", site.Description},
                    {"addr_region_id",site.AddrRegionId },
                    {"org_id" , site.OrgId},
                    {"name",site.Name }
                };
                int siteId = InsertWithReturn(fields);
                if (site.Lat.HasValue && site.Lon.HasValue)
                    UpdateLatLon(siteId, (double)site.Lat, (double)site.Lon);

                return siteId;
            }
            finally
            {
                TableName = tableNameCur;
            }
        }

        private void UpdateLatLon(int siteId, double lat, double lon)
        {
            string sql = "update meta.site set geom_latlon = ST_MakePoint(@lon,@lat)";
            var fields = new Dictionary<string, object>()
                {
                    {"id", siteId},
                    {"lat", lat},
                    {"lon", lon}
                };
            ExecSimpleQuery(sql, fields);
        }

        /// <summary>
        /// Изменить тип, привязку к станции и описание пункта.
        /// </summary>
        /// <param name="site">Изменяемый пункт.</param>
        /// <returns></returns>
        public void Update(Site site)
        {
            string tableNameCur = TableName;
            try
            {
                TableName = "meta.site";
                var fields = new Dictionary<string, object>()
            {
                {"id" , site.Id},
                {"parent_id", site.ParentId},
                {"site_type_id", site.TypeId},
                {"code", site.Code},
                {"description", site.Description},
                {"addr_region_id",site.AddrRegionId },
                {"org_id" , site.OrgId},
                {"name",site.Name }
            };
                Update(fields);

            }
            finally
            {
                TableName = tableNameCur;
            }
        }

        public List<Site> Select(SiteFilter siteFilter)
        {
            var fields = new Dictionary<string, object>()
            {
                { "site_type_id", siteFilter.TypeId },
                { "code", siteFilter.CodeLike  },
                { "name", siteFilter.NameLike },
                { "addr_region_id", siteFilter.AddrId },
                { "org_id", siteFilter.OrgId }
            };
            return Select(fields);
        }
        public List<Site> SelectByType(int siteTypeId)
        {
            return SelectByTypes(new List<int>(new int[] { siteTypeId }));
        }
        public virtual List<Site> SelectByTypes(List<int> siteTypeIds)
        {
            var fields = new Dictionary<string, object>() { { "site_type_id", siteTypeIds } };
            return Select(fields);
        }
        public List<Site> SelectExtent(double south, double north, double west, double east)
        {
            var fields = new Dictionary<string, object>() { { "south", south }, { "north", north }, { "west", west }, { "east", east } };
            string sql = "Select * from " + TableName +
                         "Where lat between :south and :north and lon between :west and :east";
            return ExecQuery<Site>(sql, fields);
        }

        public List<Site> SelectByCodes(List<string> siteCodes)
        {
            return Select(new Dictionary<string, object>()
                {
                    {"code", siteCodes}
                }
            );
        }
        public List<Site> SelectByAddrRegionIds(List<int> addrRegionIds)
        {
            return Select(new Dictionary<string, object>()
                {
                    {"addr_region_id", addrRegionIds}
                }
            );

        }
        /// <summary>
        /// Выбрать пункты, относящиеся к группе и отсортировать их в порядке, указанном в группе.
        /// </summary>
        /// <param name="siteGroupId">Код группы пунктов.</param>
        /// <returns>Список пунктов группы, отсортированные в порядке, указанном в группе.</returns>
        public List<Site> SelectSitesByGroup(int siteGroupId)
        {
            List<int[]> idOrder = DataManager.GetInstance().EntityGroupRepository.SelectEntities(siteGroupId);
            return Select(idOrder.Select(x => x[0]).ToList())
                .OrderBy(x => idOrder.First(y => y[0] == x.Id)[1])
                .ToList();
        }
        public List<Site> SelectByParent(int parentSiteId)
        {
            var fields = new Dictionary<string, object>() { { "parent_id", parentSiteId } };
            return Select(fields);
        }
        public List<Site> SelectWithoutGeoObject()
        {
            var fields = new Dictionary<string, object>();
            string sql = "select * from meta.site s"
                    + " where s.id not in (select distinct site_id from meta.site_x_geoobject)";
            return ExecQuery<Site>(sql, fields);
        }

    }
}
