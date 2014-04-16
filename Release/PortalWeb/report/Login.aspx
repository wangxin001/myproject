<%@ page title="" language="C#" masterpagefile="~/report/MasterPage.master" autoeventwireup="true" inherits="report_Login, PageCode" enableviewstate="true" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="Server">


    <script type="text/javascript">

        function ChangeAuthCode() {
            var strDate = new Date();
            document.getElementById("ImgAuthCode").src = "../common/ValidateNum.aspx?TimeStemp=" + strDate;
        }

        function ValidateInput() {

            if (CheckNull("<%=txtUserName.ClientID %>")) {
                alert("请输入用户名");
                return false;
            }

            if (CheckNull("<%=txtPassword.ClientID %>")) {
                alert("请输入密码");
                return false;
            }

            if (CheckNull("<%=txtAuthorCode.ClientID %>")) {
                alert("请输入验证码");
                return false;
            }

            return true;
        }
    
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <div class="breadCrumb">
        <a href="###" title="首页">首页</a>&gt; <a href="###" title="平价菜店">平价菜店</a>&gt; <span>登陆</span>
    </div>
    <div class="login">
        <div class="loginInfo">
            <ul class="addUser">
                <li>
                    <label class="fl">
                        用户名：</label><div class="fl inputBg">
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        </div>
                </li>
                <li>
                    <label class="fl">
                        密　码：</label><div class="fl inputBg">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                </li>
                <li>
                    <label class="fl">
                        验证码：</label><div class="fl inputBgS">
                            <asp:TextBox ID="txtAuthorCode" runat="server" MaxLength=4></asp:TextBox>
                        </div>
                    <div class="fl yzmImg">
                    <img id="ImgAuthCode" src="../common/ValidateNum.aspx" alt="点击更换图片" title="点击更换图片"
                        style="width: 50px; height: 21px; cursor: pointer;" onclick="javascript:ChangeAuthCode()" />
                   </div>
                </li>
                <li>
                    <p class="yzmTxt">
                        请输入图片中的字符<a href="javascript:ChangeAuthCode()">看不清楚？换张图片</a></p>
                </li>
                <li class="lastBnt">
                    <div class="loginBnt bnt_bg fl">
                        <asp:Button ID="btnLogin" runat="server" onclick="btnLogin_Click" OnClientClick="javascript:return ValidateInput();" />
                    </div>
                    <div class="forget bnt_bg fl">
                        <asp:Button ID="Button1" runat="server" Text="" onclick="Button1_Click" />
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
