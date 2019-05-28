using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;
using System.Runtime.Serialization;

namespace SOV.Amur.Meta
{
    [DataContract]
    public class Variable : IdNameRE
    {
        [DataMember]
        public int VariableTypeId { get; set; }
        [DataMember]
        public int TimeId { get; set; }
        [DataMember]
        public int UnitId { get; set; }
        [DataMember]
        public int DataTypeId { get; set; }
        [DataMember]
        public int GeneralCategoryId { get; set; }
        [DataMember]
        public int SampleMediumId { get; set; }
        [DataMember]
        public int TimeSupport { get; set; }

        [DataMember]
        public double CodeNoData { get; set; }
        [DataMember]
        public double CodeErrData { get; set; }

        // FK
        public List<VariableAttributes> VariableAttributes { get; set; }

        public static List<IdName> ToListIdName(List<Variable> items, EnumLanguage enumLanguage)
        {
            return items.Select(x => new IdName() { Id = x.Id, Name = enumLanguage == EnumLanguage.Rus ? x.NameRus : x.NameEng }).ToList();
        }

        public Variable
        (
            int id,
            int VariableTypeId,
            int TimeId,
            int UnitId,
            int DataTypeId,
            int GeneralCategoryId,
            int SampleMediumId,
            int TimeSupport,
            string NameRus,
            string NameEng,
            double CodeNoData,
            double CodeErrData
        )
            : base(id, NameRus, NameEng)
        {
            this.VariableTypeId = VariableTypeId;
            this.TimeId = TimeId;
            this.UnitId = UnitId;
            this.DataTypeId = DataTypeId;
            this.GeneralCategoryId = GeneralCategoryId;
            this.SampleMediumId = SampleMediumId;
            this.TimeSupport = TimeSupport;
            this.CodeNoData = CodeNoData;
            this.CodeErrData = CodeErrData;
        }

        public Variable Clone()
        {
            return new Variable(
                Id,
                VariableTypeId,
                TimeId,
                UnitId,
                DataTypeId,
                GeneralCategoryId,
                SampleMediumId,
                TimeSupport,
                NameRus.Clone() as string,
                NameEng.Clone() as string,
                CodeNoData,
                CodeErrData
            );
        }
    }
}
