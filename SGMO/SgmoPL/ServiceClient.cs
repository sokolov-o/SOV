using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOV.Common;

namespace SOV.SGMO
{
    class AmurServiceClient
    {
        public AmurServiceReference.ServiceClient client;
        public long h;

        public AmurServiceClient(User user)
        {
            client = new AmurServiceReference.ServiceClient();
            h = client.Open(user.Name, user.Password);
            if (h < 1)
                throw new Exception(string.Format("Ошибка открытия сервиса AmurServiceReference. Возврат handle {0}.", h));
        }
    }
    class FieldServiceClient
    {
        public FieldServiceReference.ServiceClient client;
        public long h;

        public FieldServiceClient(User user)
        {
            client = new FieldServiceReference.ServiceClient();
            h = client.Open(user.Name, user.Password);
            if (h < 1)
                throw new Exception(string.Format("Ошибка открытия сервиса AmurServiceReference. Возврат handle {0}.", h));
        }
    }
}
