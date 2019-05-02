using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Meta;

namespace SOV.SGMO
{
    /// <summary>
    /// Прогностическое текстовое сообщение для пункта.
    /// </summary>
    public class MessageSite
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public int MethodId { get; set; }
        public Enums.MessageType MessageType { get; set; }
        public EnumLanguage Language { get; set; }

        public DateTime DateIni { get; set; }
        public DateTime DateFcs { get; set; }
        public EnumDateType DateTypeFcs { get; set; }

        public string Text { get; set; }

        public DateTime DateInsert { get; set; }
    }
}
