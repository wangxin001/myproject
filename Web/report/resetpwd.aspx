<%@ Page Title="" Language="C#" MasterPageFile="~/report/MasterPage.master" AutoEventWireup="true" CodeFile="resetpwd.aspx.cs" Inherits="report_resetpwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
<style type="text/css">

.btnSubmit { background:url(../images/tj_03.jpg) no-repeat; width:88px; height:22px; border:0; cursor:pointer;}

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <div class="breadCrumb">
        <a href="###" title="首页">首页</a>&gt; <a href="###" title="平价菜店">平价菜店</a>&gt; <span>重置密码</span>
    </div>
    <div class="login">
        <div class="loginInfo">
            <ul class="addUser">
                <li>
                    <label class="fl">
                        用 户 名：</label><div class="fl inputBg">
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        </div>
                </li>
                <li>
                    <label class="fl">
                        邮箱地址：</label><div class="fl inputBg">
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        </div>
                </li>
                
                <li class="lastBnt">
                    <div class="bot">
                        <asp:Button class="btnSubmit" ID="btnSubmit" runat="server" Text="" 
                            onclick="btnSubmit_Click" />
                    </div>
                </li>
            </ul>
            <div class="clear">
            </div>
        </div>
    </div>
    
        </ContentTemplate>
        </asp:UpdatePanel>     

</asp:Content>

