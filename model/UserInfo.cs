using System;
using System.Collections.Generic;
using System.Text;
using OR.Model;

namespace model
{
    [Serializable]
    public class UserInfo : Entity
    {
        private String _userGUID;
        [ID(GenerationType.Manually)]
        public String UserGUID
        {
            get { return _userGUID; }
            set { _userGUID = value; }
        }

        private String _userAccount;

        public String UserAccount
        {
            get { return _userAccount; }
            set { _userAccount = value; }
        }

        private String _userPassword;

        public String UserPassword
        {
            get { return _userPassword; }
            set { _userPassword = value; }
        }
        private String _userName;

        public String UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private String _contactName;

        public String ContactName
        {
            get { return _contactName; }
            set { _contactName = value; }
        }

        private String _contactTitle;

        public String ContactTitle
        {
            get { return _contactTitle; }
            set { _contactTitle = value; }
        }

        private String _contactPhone;

        public String ContactPhone
        {
            get { return _contactPhone; }
            set { _contactPhone = value; }
        }

        private String _contactMobile;

        public String ContactMobile
        {
            get { return _contactMobile; }
            set { _contactMobile = value; }
        }

        private String _contactEmail;

        public String ContactEmail
        {
            get { return _contactEmail; }
            set { _contactEmail = value; }
        }


        private int _userRole;

        public int UserRole
        {
            get { return _userRole; }
            set { _userRole = value; }
        }
        private DateTime _created;

        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }

        private int _status;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private int _filledInfor;

        public int FilledInfor
        {
            get { return _filledInfor; }
            set { _filledInfor = value; }
        }

    }
}
