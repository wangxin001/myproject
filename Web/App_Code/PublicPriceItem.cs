using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///PublicPriceItem 的摘要说明
/// </summary>
public class PublicPriceItem : OR.Model.Entity
{
    private String _itemType;

    public String ItemType
    {
        get { return _itemType; }
        set { _itemType = value; }
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

    private Decimal _turnover;

    public Decimal Turnover
    {
        get { return _turnover; }
        set { _turnover = value; }
    }

    private Decimal _wholesprice;

    public Decimal WholesalePrice
    {
        get { return _wholesprice; }
        set { _wholesprice = value; }
    }

    private Decimal _retailPrice;

    public Decimal RetailPrice
    {
        get { return _retailPrice; }
        set { _retailPrice = value; }
    }

    private Decimal _supermarketprice;

    public Decimal SupermarketPrice
    {
        get { return _supermarketprice; }
        set { _supermarketprice = value; }
    }

    private DateTime _priceDate;

    public DateTime PriceDate
    {
        get { return _priceDate; }
        set { _priceDate = value; }
    }

}