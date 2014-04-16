<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/InfoList.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="agency_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <div class="middle_slide">
        <div class="slide">
            <h3>
                价格监测定点单位名单</h3>
            <ul>
                <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                     <li><a title="<%# Eval("TopicTitle").ToString().Split(',')[1] %>" href="./Default_<%# Eval("TopicID") %>.aspx"><%# Eval("TopicTitle").ToString().Split(',')[1] %></a></li>
                </ItemTemplate>
                </asp:Repeater>

            </ul>
        </div>
    </div>
    <div class="middle_content">
        
        <div class="zb_ri">
			<div class="gza">
				<span class="gz_title"><strong>
					<%=info.TopicTitle.Split(',')[0] %></strong> </span><span class="gz_td">来源：<%=info.Publisher%>&nbsp;&nbsp;&nbsp;&nbsp;发布时间：<%=info.PublishDate.ToString("yyyy-MM-dd") %>
					</span><span class="gz_tc">
						<%=info.TopicContent %></span>
			</div>
		</div>

    </div>
</asp:Content>
