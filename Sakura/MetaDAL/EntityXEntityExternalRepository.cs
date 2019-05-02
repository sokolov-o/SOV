using FERHRI.Common;
using FERHRI.Sakura.Meta;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Sakura.Meta
{
    public class EntityXEntityExternalRepository
    {
        ADbMSSql _db;

        public EntityXEntityExternalRepository(ADbMSSql db)
        {
            _db = db;
        }
        public int? SelectOurEntityId(int entityTypeId, int extDbListId, int extEntityId)
        {
            int? ret = null;
            using (SqlConnection cnn = _db.Connection)
            {
                using (SqlCommand cmd = new SqlCommand("select * from EntityXEntityExternal " +
                    " where entityTypeId = @entityTypeId AND extDbListId = @extDbListId and extEntityId=@extEntityId", cnn))
                {
                    cmd.Parameters.AddWithValue("@entityTypeId", entityTypeId);
                    cmd.Parameters.AddWithValue("@extDbListId", extDbListId);
                    cmd.Parameters.AddWithValue("@extEntityId", extEntityId);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                            ret = (int)rdr["ourEntityId"];
                    }
                }
            }
            return ret;
        }
        public List<EntityXEntityExternal> Select(int entityTypeId, int extDbListId)
        {
            List<EntityXEntityExternal> ret = new List<EntityXEntityExternal>();
            using (SqlConnection cnn = _db.Connection)
            {
                using (SqlCommand cmd = new SqlCommand("select * from EntityXEntityExternal " +
                    " where entityTypeId = @entityTypeId AND extDbListId = @extDbListId ", cnn))
                {
                    cmd.Parameters.AddWithValue("@entityTypeId", entityTypeId);
                    cmd.Parameters.AddWithValue("@extDbListId", extDbListId);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ret.Add(Parse(rdr));
                        }
                    }
                }
            }
            return ret;
        }


        EntityXEntityExternal Parse(SqlDataReader rdr)
        {
            return new EntityXEntityExternal(
                (int)rdr["id"],
                (int)rdr["entityTypeId"],
                (int)rdr["ourEntityId"],
                (int)rdr["extEntityId"],
                (int)rdr["extDbListId"]
            );
        }
    }
}
