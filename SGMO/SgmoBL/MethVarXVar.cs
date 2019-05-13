using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOV.SGMO
{
    /// <summary>
    /// Соответствие методов и переменных БД Амур переменным других (EXTernal) баз данных
    /// </summary>
    public class MethVarXVar
    {
        /// <summary>
        /// Словарь соответствия переменных БД Амур переменным внешних баз.
        /// </summary>
        static Dictionary<MethVaroff, object> _MethVarXVar = new Dictionary<MethVaroff, object>()
        {
            // FILE_VAN2011
            // Dictionary value is VanRepository.WaveParamNames2011 index

            // Волна смеш. - высота прогн, м
            { new MethVaroff() { MethodId=105, Varoff=new Varoff(){ VariableId=1028, OffsetTypeId = (int)Amur.Meta.EnumOffsetType.MSL, OffsetValue=0} }, 0},
            // Волна ветровая - высота прогн, м
            { new MethVaroff() { MethodId=105, Varoff=new Varoff(){ VariableId=1033, OffsetTypeId = (int)Amur.Meta.EnumOffsetType.MSL, OffsetValue=0} }, 4},
            // Волна ветровая - ср. период прогн, сек
            { new MethVaroff() { MethodId=105, Varoff=new Varoff(){ VariableId=1026, OffsetTypeId = (int)Amur.Meta.EnumOffsetType.MSL, OffsetValue=0} }, 5},
            // Волна ветровая - направление прогн, град
            { new MethVaroff() { MethodId=105, Varoff=new Varoff(){ VariableId=1032, OffsetTypeId = (int)Amur.Meta.EnumOffsetType.MSL, OffsetValue=0} }, 7},
            // Волна зыбь - высота прогн, м
            { new MethVaroff() { MethodId=105, Varoff=new Varoff(){ VariableId=1027, OffsetTypeId = (int)Amur.Meta.EnumOffsetType.MSL, OffsetValue=0} }, 8},
            // Волна зыбь - ср. период прогн, сек
            { new MethVaroff() { MethodId=105, Varoff=new Varoff(){ VariableId=1049, OffsetTypeId = (int)Amur.Meta.EnumOffsetType.MSL, OffsetValue=0} }, 9},
            // Волна зыбь - направление прогн, град
            { new MethVaroff() { MethodId=105, Varoff=new Varoff(){ VariableId=1050, OffsetTypeId = (int)Amur.Meta.EnumOffsetType.MSL, OffsetValue=0} }, 11},
            /*
            // НЕИЗВЕСТНО СООТВЕТСТВИЕ
            // Волна - период пика, прогн, сек
            { new MethVaroff() { MethodId=105, Varoff=new Varoff(){ VariableId=1051, OffsetTypeId = (int)Amur.Meta.EnumOffsetType.MSL, OffsetValue=0} }, -1},
            // Волна - направление доминир. прогн, град
            { new MethVaroff() { MethodId=105, Varoff=new Varoff(){ VariableId=1035, OffsetTypeId = (int)Amur.Meta.EnumOffsetType.MSL, OffsetValue=0} }, -1},
            // Волна смеш. - высота макс 0.1% прогн, м
            { new MethVaroff() { MethodId=105, Varoff=new Varoff(){ VariableId=1048, OffsetTypeId = (int)Amur.Meta.EnumOffsetType.MSL, OffsetValue=0} }, -1}
            */
        };

        public static object GetExtMethodVar(int amurMethodId, Varoff amurVaroff)
        {
            MethVaroff mv = new MethVaroff() { MethodId = amurMethodId, Varoff = amurVaroff };
            _MethVarXVar.TryGetValue(mv, out object ret);
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
