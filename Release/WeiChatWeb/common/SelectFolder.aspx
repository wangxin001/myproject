<%@ page language="C#" autoeventwireup="true" inherits="common_SelectFolder, WeiChatWeb" theme="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <style>
        .tree td div
        {
            height: 20px !important;
        }
        
        .tree td img
        {
            vertical-align: bottom;
        }
        
        body
        {
            color: #666666;
            font-family: "宋体";
            font-size: 12px;
        }
        img
        {
            border: 0px;
        }
        .view
        {
            border-right: #848284 1px solid;
            border-top: #848284 1px solid;
            border-left: #848284 1px solid;
            border-bottom: #848284 1px solid;
            position: relative;
            top: 0px;
            left: 0px;
            width: 248px;
            height: 370px;
            background-color: #ffffff;
            text-align: left;
            overflow-x: auto;
            overflow-y: auto;
        }
    </style>

    <script type="text/javascript">

        function SelectObj(obj) {

            if (obj.checked) {
                addObject(obj);
            }
            else {
                removeObject(obj);
            }
        }

        // 添加选中的帐号
        function addObject(obj) {

            var id = obj.id;
            var name = obj.value;

            var list = document.getElementById("lstSelectedObjs");

            var opt = new Option(name, id);
            list.options.add(opt);
        }

        function removeObject(obj) {
            var id = obj.id;

            var list = document.getElementById("lstSelectedObjs");

            for (var i = 0; i < list.options.length; i++) {
                if (id == list.options[i].value) {
                    list.options.remove(i);
                    break;
                }
            }
        }

        function GetReturnValue() {

            var list = document.getElementById("lstSelectedObjs");

            var id = new Array();
            var name = new Array();

            for (var i = 0; i < list.options.length; i++) {

                var opt = list.options[i];

                id.push(opt.value);
                name.push(opt.text);
            }

            if (parent.GetResultValue(id, name)) {
                parent.CloseFrame();
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="border: 1px solid rgb(129, 129, 129); width: 525px; height: 400px;
        padding: 5px;">
        <legend style="color: rgb(102, 102, 102); margin: 3px;">选择</legend>
        <table cellspacing="0" cellpadding="0" border="0" width="100%">
            <tr>
                <td style="padding-left: 3px; padding-top: 0px; vertical-align: top; width: 255px;">
                    <div id="div1" class="view" style="display: block;">
                        <asp:TreeView ID="treeMenu" runat="server" ShowLines="True" OnTreeNodePopulate="treeMenu_TreeNodePopulate"
                            CssClass="tree">
                        </asp:TreeView>
                    </div>
                </td>
                <td style="padding-left: 3px; padding-top: 0px; vertical-align: middle; width: 10px;
                    text-align: center;">
                </td>
                <td style="padding-left: 3px; padding-top: 0px; vertical-align: top; width: 220px;">
                    <select size="28" style="width: 100%; height: 370px; border: #848284 1px solid" id="lstSelectedObjs">
                    </select>
                </td>
            </tr>
        </table>
    </fieldset>
    <table cellspacing="0" cellpadding="0" border="0" width="100%" style="padding-top: 5px;">
        <tr style="height: 30px">
            <td align="right">
                <input type="button" onclick="javascript: GetReturnValue()" value="确定" id="btnOK" />
                &nbsp;&nbsp;
                <input type="button" onclick="parent.CloseFrame()" value="取消" id="btnCacel" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
