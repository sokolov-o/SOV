using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.Sakura.Meta
{
    public class EntityXEntityExternal
    {


        public int Id { get; set; }
        public int EntityTypeId { get; set; }
        public int OurEntityId { get; set; }
        public int ExtEntityId { get; set; }
        public int ExtDbListId { get; set; }

        public EntityXEntityExternal(int id, int entityTypeId, int ourEntityId, int extEntityId, int extDbListId)
        {
            Id = id;
            EntityTypeId = entityTypeId;
            OurEntityId = ourEntityId;
            ExtEntityId = extEntityId;
            ExtDbListId = ExtDbListId;
        }
    }
}
