using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.SGMO
{
    /// <summary>
    /// Соответствие методов и переменных БД Амур переменным других (внешних, ext) баз данных, методам и переменным
    /// </summary>
    public class MethVarXVar
    {
        static Dictionary<MethVaroff, object> _MethVarXVar = new Dictionary<MethVaroff, object>()
        {
            {
                new MethVaroff()
                { 
                    MethodId=105, 
                    Varoff=new Varoff(){ VariableId=1028, OffsetTypeId = (int)Amur.Meta.EnumOffsetType.MSL, OffsetValue=0}
                }, 
                0 /*VAN-file column index*/
            }
        };
        public static object GetExtMethodVar(int amurMethodId, Varoff amurVaroff)
        {
            MethVaroff mv = new MethVaroff() { MethodId = amurMethodId, Varoff = amurVaroff };
            object ret;
            _MethVarXVar.TryGetValue(mv, out ret);
            return ret;
        }
        public static object GetExtMethodVar(Amur.Meta.Catalog catalog)
        {
            return GetExtMethodVar(catalog.MethodId, Varoff.GetVaroff(catalog));
        }
        class MethVaroff : IEquatable<MethVaroff>
        {
            public int MethodId { get; set; }
            public Varoff Varoff { get; set; }
            public bool Equals(MethVaroff other)
            {

                //Check whether the compared object is null. 
                if (Object.ReferenceEquals(other, null)) return false;

                //Check whether the compared object references the same data. 
                if (Object.ReferenceEquals(this, other)) return true;

                //Check whether the products' properties are equal. 
                return MethodId.Equals(other.MethodId) && Varoff.Equals(other.Varoff);
            }

            // If Equals() returns true for a pair of objects  
            // then GetHashCode() must return the same value for these objects. 

            public override int GetHashCode()
            {

                //Get hash code for the Name field if it is not null. 
                //int hashProductName = Name == null ? 0 : Name.GetHashCode();

                //Get hash code for the Code field. 
                //int hashProductCode = Code.GetHashCode();

                //Calculate the hash code for the product. 
                return MethodId.GetHashCode() ^ Varoff.GetHashCode();
            }

        }
    }
}
