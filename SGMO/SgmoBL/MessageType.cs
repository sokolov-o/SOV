using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FERHRI.Amur.Meta;
using FERHRI.Common;

namespace FERHRI.SGMO
{
    /// <summary>
    /// Тип текстового сообщения.
    /// </summary>
    public class MessageType : IdName
    {
        public string Description { get; set; }
    }
}
