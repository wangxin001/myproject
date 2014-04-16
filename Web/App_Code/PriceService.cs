using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///PriceService 的摘要说明
/// </summary>
[WebService(Namespace = "http://ccpn.gpv/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class PriceService : System.Web.Services.WebService
{

    public PriceService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public List<PriceItem> GetStorePrice(String ScreenName)
    {
        String strSQL = @"
WITH t
AS (
	SELECT top 1 PriceDate, BatchID
	FROM StorePrice
	WHERE StoreGUID = (Select UserGUID From ScreenInfo where ScreenName = @pname)
    Order By PriceDate Desc, BatchID Desc
	)
SELECT i.ItemOrder ItemID
	,i.ItemName
	,i.ItemLevel
	,i.ItemUnit
	,s.PriceDate
	,Price03 PriceAverage
	,s.Price01 StorePrice
    ,s.Created
FROM PriceItems i
INNER JOIN (
	SELECT *
	FROM StorePrice
	WHERE PriceDate = (
			SELECT PriceDate
			FROM t
			)
		AND StoreGUID = (Select UserGUID From ScreenInfo where ScreenName = @pname)
        And BatchID = (Select BatchID From t)
	) s ON i.ItemID = s.ItemID
LEFT JOIN (
	SELECT *
	FROM PriceRecord
	WHERE PriceDate = (
			SELECT DateAdd(d, -1, PriceDate)
			FROM t
			)
	) r ON r.ItemID = i.ItemID
WHERE i.ReportStatus = 1
	AND i.STATUS = 1
ORDER BY i.ItemOrder ";

        System.Data.DataTable data = OR.DB.SQLHelper.Query(strSQL, new System.Data.SqlClient.SqlParameter("@pname", ScreenName)).Tables[0];

        List<PriceItem> priceItems = OR.Control.DAL.DataTableToModel<PriceItem>(data);

        return priceItems;
    }

    /// <summary>
    /// 调用该方法获取指定日期的今日菜价价格内容。
    /// </summary>
    /// <param name="userAccount">登陆帐号，请与管理员联系获取该帐号</param>
    /// <param name="password">密码，请与管理员联系获取该密码</param>
    /// <param name="priceDate">日期格式，需获取菜价的日期</param>
    /// <param name="priceType">获取菜价的类型。
    ///              priceType=1: 获取蔬菜的价格
    ///              priceType=2: 获取粮油的价格
    ///              priceType=3: 获取肉蛋水产的价格
    ///              priceType=0: 一次性获取所有内容的价格
    ///                         </param>
    /// <returns>
    ///     以List方式返回各项目的价格列表。
    ///     该列表中各对象的说明为：
    ///         ItemType：       价格项目类型
    ///         ItemName：       价格项目名称
    ///         ItemUnit：       价格项目规格等级
    ///         Turnover：       成交量。粮油和肉蛋水产中为0
    ///         WholesalePrice： 批发价格
    ///         RetailPrice：    农贸零售价格
    ///         SupermarketPrice：超市零售价格
    ///         PriceDate：      日期
    /// 
    /// </returns>
    [WebMethod]
    public List<PublicPriceItem> GetPublicPrice(String userAccount, String password, DateTime priceDate, int priceType)
    {
        String strPassword = Util.to_md5(password, 32);

        model.UserInfo user = OR.Control.DAL.GetModel<model.UserInfo>("UserAccount=@UserAccount",
            new System.Data.SqlClient.SqlParameter("@UserAccount", userAccount.Trim()));

        if (user == null
            || user.Status != (int)model.Status.Normal
            || (model.UserRole)user.UserRole != model.UserRole.查询菜价接口用户
            || !strPassword.Equals(user.UserPassword))
        {
            return null;
        }

        //String strPriceType = "1";

        //switch (priceType)
        //{
        //    case 0:
        //        strPriceType = "1,15,16";
        //        break;
        //    case 1:
        //        strPriceType = "1";
        //        break;
        //    case 2:
        //        strPriceType = "15";
        //        break;
        //    case 3:
        //        strPriceType = "16";
        //        break;
        //    default:
        //        break;
        //}

        //if (String.IsNullOrEmpty(strPriceType))
        //{
        //    return null;
        //}



        String strSQL = @"
select case PriceItems.ItemType when 1 then '蔬菜' when 15 then '粮油' when 16 then '肉蛋水产' else '' end ItemType, 
PriceItems.ItemName, PriceItems.ItemUnit, Price01 Turnover, Price02 WholesalePrice, 
Price03 RetailPrice, Price04 SupermarketPrice, PriceDate 
From PriceRecord inner join PriceItems on PriceRecord.ItemID=PriceItems.ItemID 
where PriceItems.ItemType=1 AND PriceItems.Status=1
and PriceRecord.PriceDate= @pdate
order by PriceItems.ItemType, PriceItems.ItemOrder";

        System.Data.DataTable data = OR.DB.SQLHelper.Query(strSQL, new System.Data.SqlClient.SqlParameter("@pdate", priceDate.Date)).Tables[0];

        List<PublicPriceItem> priceItems = OR.Control.DAL.DataTableToModel<PublicPriceItem>(data);

        return priceItems;
    }
}
