using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Amur.Meta;
using SOV.Common;

namespace SOV.SGMO
{
    /// <summary>
    /// Тип текстового сообщения.
    /// </summary>
    public class MessageType : IdName
    {
        public string Description { get; set; }
    }
}
