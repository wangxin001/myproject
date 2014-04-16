<%@ page title="" language="C#" masterpagefile="~/MasterPage/InfoList.master" autoeventwireup="true" inherits="policy_index, PageCode" enableviewstate="false" %>

<%@ OutputCache CacheProfile="Cache1MinuteByPage" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <!--左侧标题 begin-->
    <div class="jgzc_top">
    </div>
    <!--左侧标题 end-->
    <div class="wei_ri">
        <a href="../index.aspx">首页</a> > <span class="we_te"><a href="index.aspx">价格政策</a></span>
    </div>
    <div class="clear">
    </div>
    <div class="zbody_b">
        <!--左侧 begin-->
        <div class="zb_le">
        </div>
        <!--左侧 end-->
        <!--右侧 begin-->
        <div class="zb_ri">
            <div class="gza">
                <ul>
                    <asp:Repeater ID="rpInfo" runat="server">
                        <ItemTemplate>
                            <li><span class="ne_le"><a href="detail_<%# Eval("TopicID").ToString()%>.aspx" target="_blank">
                                <div class="CuteTitle">
                                    <%# Eval("TopicTitle").ToString()%></div>
                            </a></span><span class="ne_ld"><%# Boolean.Parse( Eval("ShowDate").ToString())? "["+Convert.ToDateTime(Eval("PublishDate").ToString()).ToString("yyyy-MM-dd")+"]":"" %>
                            </span></li>
                            <%# (Container.ItemIndex>0 && (Container.ItemIndex+1) %5==0  )? "</ul><ul>":string.Empty  %>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="clear">
            </div>
            <div class="page">
                <webdiyer:AspNetPager ID="AspNetPager" runat="server" AlwaysShow="True" AlwaysShowFirstLastPageNumber="True"
                    CustomInfoHTML="共%PageCount%页/%RecordCount%条" ShowCustomInfoSection="Right" CustomInfoSectionWidth="95px"
                    FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList"
                    ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到"
                    EnableUrlRewriting="True" UrlRewritePattern="./index.aspx?page={0}" PageSize="10">
                </webdiyer:AspNetPager>
            </div>
        </div>
        <!--右侧 end-->
    </div>
</asp:Content>
