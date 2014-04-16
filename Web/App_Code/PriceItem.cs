using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
///PriceItem 的摘要说明
/// </summary>
public class PriceItem : OR.Model.Entity
{
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

    private String _itemLevel;

    public String ItemLevel
    {
        get { return _itemLevel; }
        set { _itemLevel = value; }
    }

    private String _itemUnit;

    public String ItemUnit
    {
        get { return _itemUnit; }
        set { _itemUnit = value; }
    }

    private DateTime _priceDate;

    public DateTime PriceDate
    {
        get { return _priceDate; }
        set { _priceDate = value; }
    }

    private Double _priceAverage;

    public Double PriceAverage
    {
        get { return _priceAverage; }
        set { _priceAverage = value; }
    }

    private Double _storePrice;

    public Double StorePrice
    {
        get { return _storePrice; }
        set { _storePrice = value; }
    }

    private DateTime _created;

    public DateTime Created
    {
        get { return _created; }
        set { _created = value; }
    }
    
}