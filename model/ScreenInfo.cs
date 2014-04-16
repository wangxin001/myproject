using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class ScreenInfo : OR.Model.Entity
    {

        private Int32 _screenID;

        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public Int32 ScreenID
        {
            get { return _screenID; }
            set { _screenID = value; }
        }

        private String _screenName;

        public String ScreenName
        {
            get { return _screenName; }
            set { _screenName = value; }
        }

        private String _userGUID;

        public String UserGUID
        {
            get { return _userGUID; }
            set { _userGUID = value; }
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

    }
}
