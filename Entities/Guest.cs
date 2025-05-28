using System;

namespace Entities
{
    public class Guest
    {
        // Private fields
        private int _guestID;
        private string _fullName;
        private string _phoneNumber;
        private string _email;
        private string _guestPrivateInfo;

        // Constructor không có GuestID
        public Guest(string fullName, string phoneNumber, string email, string guestPrivateInfo)
        {
            _fullName = fullName;
            _phoneNumber = phoneNumber;
            _email = email;
            _guestPrivateInfo = guestPrivateInfo;
        }

        // Constructor có đầy đủ GuestID
        public Guest(int guestID, string fullName, string phoneNumber, string email, string guestPrivateInfo)
        {
            _guestID = guestID;
            _fullName = fullName;
            _phoneNumber = phoneNumber;
            _email = email;
            _guestPrivateInfo = guestPrivateInfo;
        }

        // Constructor chỉ có GuestID và FullName
        public Guest(int guestID, string fullName)
        {
            _guestID = guestID;
            _fullName = fullName;
        }

        // Public properties
        public int GuestID
        {
            get { return _guestID; }
            set { _guestID = value; }
        }

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string GuestPrivateInfo
        {
            get { return _guestPrivateInfo; }
            set { _guestPrivateInfo = value; }
        }

       
    }
}
