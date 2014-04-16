using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class StorePrice : OR.Model.Entity
    {
        private int _priceID;

        [OR.Model.ID(OR.Model.GenerationType.Identity)]
        public int PriceID
        {
            get { return _priceID; }
            set { _priceID = value; }
        }

        private String _storeGUID;

        public String StoreGUID
        {
            get { return _storeGUID; }
            set { _storeGUID = value; }
        }

        private String _storeName;

        public String StoreName
        {
            get { return _storeName; }
            set { _storeName = value; }
        }

        private int _itemID;

        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        private String _itemName;

        public String ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }

        private String _itemUnit;

        public String ItemUnit
        {
            get { return _itemUnit; }
            set { _itemUnit = value; }
        }

        private String _itemLevel;

        public String ItemLevel
        {
            get { return _itemLevel; }
            set { _itemLevel = value; }
        }

        private int _itemType;

        public int ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }

        private DateTime _priceDate;

        public DateTime PriceDate
        {
            get { return _priceDate; }
            set { _priceDate = value; }
        }

        private int _batchID;

        public int BatchID
        {
            get { return _batchID; }
            set { _batchID = value; }
        }


        private Double _price01;

        public Double Price01
        {
            get { return _price01; }
            set { _price01 = value; }
        }


        private DateTime _created;

        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }

        private String _userGUID;

        public String UserGUID
        {
            get { return _userGUID; }
            set { _userGUID = value; }
        }

        private int _status;

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

    }
}
