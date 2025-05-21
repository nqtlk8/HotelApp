using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ServiceInfo
    {
        public string Service { get; set; }
        public string Description { get; set; }
        public ServiceInfo(string service, string description)
        {
            this.Service = service;
            this.Description = description;
        }

        public override bool Equals(object obj)
        {
            return obj is ServiceInfo info &&
                   Service == info.Service;
        }

        public override int GetHashCode()
        {
            return 1514353572 + EqualityComparer<string>.Default.GetHashCode(Service);
        }
    }
}
