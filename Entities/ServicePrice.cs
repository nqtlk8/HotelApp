using System;

namespace Entities
{
    public class ServicePrice
    {
        // Private fields
        private int _servicePriceID;
        private int _serviceID;
        private double _servicePriceValue;
        private DateTime _startDate;
        private DateTime _endDate;

        // Constructor mặc định
        public ServicePrice()
        {
        }

        // Constructor đầy đủ
        public ServicePrice(int servicePriceID, int serviceID, double servicePriceValue, DateTime startDate, DateTime endDate)
        {
            _servicePriceID = servicePriceID;
            _serviceID = serviceID;
            _servicePriceValue = servicePriceValue;
            _startDate = startDate;
            _endDate = endDate;
        }

        // Public properties
        public int ServicePriceID
        {
            get { return _servicePriceID; }
            set { _servicePriceID = value; }
        }

        public int ServiceID
        {
            get { return _serviceID; }
            set { _serviceID = value; }
        }

        public double ServicePriceValue
        {
            get { return _servicePriceValue; }
            set { _servicePriceValue = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
    }
}
