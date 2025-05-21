using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ServiceInfo
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string Descrip { get; set; }


        public ServiceInfo(int serviceID, string serviceName, string descrip)
        {
            ServiceID = serviceID;
            ServiceName = serviceName;
            Descrip = descrip;
        }
        /*

        public override bool Equals(object obj)
        {
            return obj is ServiceInfo info &&
                   Service == info.Service;
        }

        public override int GetHashCode()
        {
            return 1514353572 + EqualityComparer<string>.Default.GetHashCode(Service);
        }
        */
    }
}
