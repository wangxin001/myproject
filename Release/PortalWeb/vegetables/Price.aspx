<%@ page language="C#" autoeventwireup="true" inherits="vegetables_Price, PageCode" enableviewstate="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>北京市价格监测中心</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../css/stylesheet.css" type="text/css" />
    <script type="text/javascript" src="../javascript/home.js"></script>
    <script type="text/javascript" src='../javascript/foucus.js'></script>
    <script type="text/javascript" src='../javascript/qiehuan.js'></script>
    <script type="text/javascript" src="../javascript/calendar.js" charset="gb2312"></script>
    <script type="text/javascript">

        function Search(p, t) {
            var tvalue = document.getElementById(t).value;
            if (tvalue == "") {
                alert("请选择要查询的日期");
                return false;
            }

            location.href = "./Price.aspx?t=" + tvalue + "&PriceType=" + p;
            return false;
        }
        
    </script>
</head>
<body style="background-color: transparent;">
    <script type='text/javascript' src='../javascript/zheader.js'></script>
    <!--header end-->
    <form id="form1" runat="server">
    <div class="clear">
    </div>
    <!--middle begin-->
    <div id="middle">
        <div class="middle_slide">
            <div class="slider">
                <h3>
                    农副产品价格<span>RGRICULTURRL PRICES</span></h3>
                <div class="slider_pic">
                    <h2>
                        一周菜价</h2>
                    <p>
                        <b>蔬菜</b>
                        <asp:TextBox ID="txtDateVegatetable" runat="server" CssClass="input1"></asp:TextBox>
                        <span>
                            <img width="13" height="12" src="../images/tt.gif" alt="" onfocus="if(this.value=='YYYY-MM-DD')this.value=''"
                                onblur="if(this.value=='')this.value='YYYY-MM-DD'" onclick="displayCalendar(event,'txtDateVegatetable')" /></span><input
                                    class="input2" name="" type="image" src="../images/cx.gif" onclick="javascript:return Search(1,'txtDateVegatetable');" /></p>
                </div>
                <div class="slider_pic">
                    <h2>
                        一周粮油价格</h2>
                    <p>
                        <b>粮油</b>
                        <asp:TextBox ID="txtLiangYou" runat="server" CssClass="input1"></asp:TextBox>
                        <span>
                            <img width="13" height="12" src="../images/tt.gif" alt="" onfocus="if(this.value=='YYYY-MM-DD')this.value=''"
                                onblur="if(this.value=='')this.value='YYYY-MM-DD'" onclick="displayCalendar(event,'txtLiangYou')" />
                        </span>
                        <input class="input2" type="image" src="../images/cx.gif" onclick="javascript:return Search(15,'txtLiangYou');" /></p>
                </div>
                <div class="slider_pic">
                    <h2>
                        一周肉蛋水产价格</h2>
                    <p>
                        <b>肉蛋水产</b>
                        <asp:TextBox ID="txtRouDan" runat="server" CssClass="input1"></asp:TextBox>
                        <span>
                            <img width="13" height="12" src="../images/tt.gif" alt="" onfocus="if(this.value=='YYYY-MM-DD')this.value=''"
                                onblur="if(this.value=='')this.value='YYYY-MM-DD'" onclick="displayCalendar(event,'txtRouDan')" />
                        </span>
                        <input class="input2" name="" type="image" src="../images/cx.gif" onclick="javascript:return Search(16,'txtRouDan');" /></p>
                </div>
            </div>
        </div>
        <div class="middle_content">
            <div class="middle_place">
                <p>
                    <a title="首页" href="../index.aspx">首页</a>&nbsp;&gt;&nbsp;<a title="农副产品价格" href="#">农副产品价格</a>&nbsp;&gt;&nbsp;<span>
                        <asp:Label ID="txtCurrentProduct" runat="server" Text=""></asp:Label>
                    </span>
                </p>
            </div>
            <div class="situ">
                <div class="title">
                    <ul id="date">
                        <%=GetWeekList() %>
                    </ul>
                </div>
                <asp:Panel ID="PanelVegetable" runat="server" Visible="false">
                    <div class="date_menu">
                        <table cellpadding="0" cellspacing="0" border="0" width="607">
                            <tr>
                                <td height="43" width="61" align="center">
                                    品类
                                    <br />
                                </td>
                                <td align="center" width="100">
                                    批发成交量<br />
                                    万/公斤
                                </td>
                                <td align="center" width="100">
                                    批发价格<br />
                                    元/500克
                                </td>
                                <td align="center" width="120">
                                    农贸零售价格<br />
                                    元/500克
                                </td>
                                <td align="center" width="120">
                                    超市零售价格<br />
                                    元/500克
                                </td>
                                <td align="center" width="106">
                                    日期<br />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="date0">
                        <ul>
                            <asp:Repeater ID="rptVegetable" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <table width="607" cellpadding="0" cellspacing="0" border="0">
                                            <tr align="center">
                                                <td width="61" class="tt1">
                                                    <%# Eval("ItemName").ToString()%>
                                                </td>
                                                <td width="100">
                                                    <%# Convert.ToDecimal(Eval("Price01").ToString()).ToString("0.00;0.00;-")%>
                                                </td>
                                                <td width="100">
                                                    <%# Convert.ToDecimal(Eval("Price02").ToString()).ToString("0.00;0.00;-")%>
                                                </td>
                                                <td width="120">
                                                    <%# Convert.ToDecimal(Eval("Price03").ToString()).ToString("0.00;0.00;-")%>
                                                </td>
                                                <td width="120">
                                                    <%# Convert.ToDecimal(Eval("Price04").ToString()).ToString("0.00;0.00;-")%>
                                                </td>
                                                <td width="106" class="tt2">
                                                    <%# Convert.ToDateTime(Eval("PriceDate").ToString()).ToString("yyyy.MM.dd")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PanelLiangYou" runat="server" Visible="false">
                    <div class="date_menu">
                        <table cellpadding="0" cellspacing="0" border="0" width="607">
                            <tr>
                                <td height="43" width="61" align="center">
                                    品类<br />
                                </td>
                                <td align="center" width="100">
                                    规格等级
                                </td>
                                <td align="center" width="100">
                                    批发价格
                                </td>
                                <td align="center" width="120">
                                    农贸零售价格
                                </td>
                                <td align="center" width="120">
                                    超市零售价格
                                </td>
                                <td align="center" width="106">
                                    日期<br />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <ul>
                            <asp:Repeater ID="rptLiangYou" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <table width="607" cellpadding="0" cellspacing="0" border="0">
                                            <tr align="center">
                                                <td width="61" class="tt1">
                                                    <%# Eval("ItemName").ToString()%>
                                                </td>
                                                <td width="100">
                                                    <%# Eval("ItemUnit").ToString()%>
                                                </td>
                                                <td width="100">
                                                    <%# Convert.ToDecimal(Eval("Price02").ToString()).ToString("0.00;0.00;-")%>
                                                </td>
                                                <td width="120">
                                                    <%# Convert.ToDecimal(Eval("Price03").ToString()).ToString("0.00;0.00;-")%>
                                                </td>
                                                <td width="120">
                                                    <%# Convert.ToDecimal(Eval("Price04").ToString()).ToString("0.00;0.00;-")%>
                                                </td>
                                                <td width="106" class="tt2">
                                                    <%# Convert.ToDateTime(Eval("PriceDate").ToString()).ToString("yyyy.MM.dd")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </asp:Panel>
                <asp:Panel ID="PanelRouDan" runat="server" Visible="false">
                    <div class="date_menu">
                        <table cellpadding="0" cellspacing="0" border="0" width="607">
                            <tr>
                                <td height="43" width="61" align="center">
                                    品类<br />
                                </td>
                                <td align="center" width="100">
                                    规格等级
                                </td>
                                <td align="center" width="100">
                                    批发价格<br />
                                    元/500克
                                </td>
                                <td align="center" width="120">
                                    农贸零售价格<br />
                                    元/500克
                                </td>
                                <td align="center" width="120">
                                    超市零售价格<br />
                                    元/500克
                                </td>
                                <td align="center" width="106">
                                    日期<br />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <ul>
                            <asp:Repeater ID="rptRouDan" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <table width="607" cellpadding="0" cellspacing="0" border="0">
                                            <tr align="center">
                                                <td width="61" class="tt1">
                                                    <%# Eval("ItemName").ToString()%>
                                                </td>
                                                <td width="100">
                                                    <%# Eval("ItemUnit").ToString()%>
                                                </td>
                                                <td width="100">
                                                    <%# Convert.ToDecimal(Eval("Price02").ToString()).ToString("0.00;0.00;-")%>
                                                </td>
                                                <td width="120">
                                                    <%# Convert.ToDecimal(Eval("Price03").ToString()).ToString("0.00;0.00;-")%>
                                                </td>
                                                <td width="120">
                                                    <%# Convert.ToDecimal(Eval("Price04").ToString()).ToString("0.00;0.00;-")%>
                                                </td>
                                                <td width="106" class="tt2">
                                                    <%# Convert.ToDateTime(Eval("PriceDate").ToString()).ToString("yyyy.MM.dd")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </asp:Panel>
                <p class="situ_foot">
                    注：粮油批发统计数据来自8家批发市场，肉禽蛋及蔬菜统计数据来自8家批发市场<br />
                    &nbsp;&nbsp;&nbsp; 零售价格数据来自32家农贸市场和17家超市的平均零售价格<br />
                    <asp:Label runat="server" Text="" ID="txtMemo">
                    &nbsp;&nbsp;&nbsp; 香油批发价格的计量单位为元/500g，零售价格的计量单位为元/瓶（490mL）
                    </asp:Label>
                </p>
            </div>
        </div>
    </div>
    <!--middle end-->
    <div class="clear">
    </div>
    <!--footer begin-->
    <script type="text/javascript" src='../javascript/zfooter.js'></script>
    <!--footer end-->
    </form>
</body>
</html>
