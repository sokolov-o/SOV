using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FERHRI.SGMO
{
    public class Varoff : IEquatable<Varoff>
    {
        public int VariableId { get; set; }
        public int OffsetTypeId { get; set; }
        public double OffsetValue { get; set; }

        public override string ToString()
        {
            return VariableId + ";" + OffsetTypeId + ";" + OffsetValue;
        }
        static public Varoff GetVaroff(Amur.Meta.Catalog catalog)
        {
            return new Varoff() { VariableId = catalog.VariableId, OffsetTypeId = catalog.OffsetTypeId, OffsetValue = catalog.OffsetValue };
        }
        public bool Equals(Varoff other)
        {

            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal. 
            return VariableId.Equals(other.VariableId) && OffsetTypeId.Equals(other.OffsetTypeId) && OffsetValue.Equals(other.OffsetValue);
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
            return VariableId.GetHashCode() ^ OffsetTypeId.GetHashCode() ^ OffsetValue.GetHashCode();
        }
    }
}
