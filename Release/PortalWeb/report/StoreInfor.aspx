<%@ page title="" language="C#" masterpagefile="~/report/MasterPage.master" autoeventwireup="true" inherits="report_StoreInfor, PageCode" enableviewstate="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <div class="breadCrumb">
        <a href="##" title="首页">首页</a>&gt; <a href="##" title="平价菜店">平价菜店</a>&gt; <span>用户详细信息</span>
    </div>
    <div class="login">
        <div class="formInfo">
            <h2>
                用户详细信息</h2>
            <div class="inf_tab">
                <table width="100%" border="0" cellspacing="0" cellpadding="1">
                    <tr>
                        <td width="26%">
                            用户名：
                        </td>
                        <td width="74%">
                            <asp:TextBox class="int" ID="txtUserAccount" runat="server" ReadOnly=true></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            密 码：
                        </td>
                        <td>
                            <asp:TextBox class="int" ID="txtPassword" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            平价菜店名称：
                        </td>
                        <td>
                            <asp:TextBox class="int" ID="txtUserName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            联系人姓名：
                        </td>
                        <td>
                            <asp:TextBox class="int" ID="txtContactName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            联系人职位：
                        </td>
                        <td>
                            <asp:TextBox class="int" ID="txtContactTitle" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            联系电话：
                        </td>
                        <td>
                            <asp:TextBox class="int" ID="txtContactPhone" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            手 机：
                        </td>
                        <td>
                            <asp:TextBox class="int" ID="txtContactMobile" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            邮 箱：
                        </td>
                        <td>
                            <asp:TextBox class="int" ID="txtContactEmail" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="bot">
                            <asp:Button class="int_a" ID="btnUpdate" runat="server" Text="" 
                                onclick="btnUpdate_Click" />
                            <input class="int_b" id="btnReset" type="reset" value="" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
