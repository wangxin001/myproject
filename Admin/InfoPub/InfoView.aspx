<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InfoView.aspx.cs" Inherits="InfoPub_InfoView"
    EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="InfoContainer">
        <div class="InfoTitle">
            <%= info.TopicTitle %>
        </div>
        <br />
        <div>
            <div class="InfoPublisher">
                <%=info.Publisher %>
            </div>
            <div class="InfoDate">
                <%= info.ShowDate? info.PublishDate.ToString("yyyy年MM月dd日"):"" %>
            </div>
        </div>
        <hr style="clear: both; width: 90%; border: red; background: red; height: 5px;" />
        <div class="InfoContent">
            <%=info.TopicContent %>
        </div>
    </div>
    </form>
</body>
</html>
