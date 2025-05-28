using System;

namespace Entities
{
    public class ServiceInfo
    {
        // Private fields
        private int _serviceID;
        private string _serviceName;
        private string _descrip;
        private int _isActive;

        // Constructor mặc định
        public ServiceInfo()
        {
        }

        // Constructor đầy đủ
        public ServiceInfo(int serviceID, string serviceName, string descrip, int isActive)
        {
            _serviceID = serviceID;
            _serviceName = serviceName;
            _descrip = descrip;
            _isActive = isActive;
        }

        // Constructor không có isActive
        public ServiceInfo(int serviceID, string serviceName, string descrip)
        {
            _serviceID = serviceID;
            _serviceName = serviceName;
            _descrip = descrip;
        }

        // Constructor không có serviceID
        public ServiceInfo(string serviceName, string descrip, int isActive)
        {
            _serviceName = serviceName;
            _descrip = descrip;
            _isActive = isActive;
        }

        // Public properties
        public int ServiceID
        {
            get { return _serviceID; }
            set { _serviceID = value; }
        }

        public string ServiceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }

        public string Descrip
        {
            get { return _descrip; }
            set { _descrip = value; }
        }

        public int IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
    }
}
