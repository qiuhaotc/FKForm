<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TestFCForm.WebForm1" EnableViewState="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Style/HighLightTool/shCoreDefault.css" rel="stylesheet" />
    <script src="Script/jquery.min.js"></script>
    <script src="Script/FCFormUtil.js"></script>
    <script src="Script/HighLightTool/shCore.js"></script>
    <script src="Script/HighLightTool/shBrushJScript.js"></script>
    <script type="text/javascript">SyntaxHighlighter.all();</script>
    <script>
        <%=ValidateFor()%>

        <%=ServerValidateScript%>
    </script>
</head>
<body>
    <form id="form1" method="post">
        <div>
            姓名：<input type="text" name="Name" id="Name" /><br />
            年龄：<input type="text" name="Ages" id="Ages" /><br />
            金额：<input type="text" name="Money" id="Money" /><br />
            简介：<input type="text" name="Memo" id="Memo" /><br />
            密码：<input type="text" name="Password" id="Password" /><br />
             重复密码：<input type="text" name="PasswordRepeat" id="PasswordRepeat" /><br />
            区域：
            <select name="Area" id="Area">
                <option value="">---请选择---</option>
                 <option value="1">---北京---</option>
                 <option value="2">---上海---</option>
                 <option value="3">---广州---</option>
            </select><br />
            电话：<input type="text" name="PhoneNum" id="PhoneNum" /><br />
            <input onclick="return validateForm();" value="提交" type="submit" />
            <input onclick="return validateAjaxForm();" value="Ajax提交" type="submit" />
        </div>
    </form>
    <div>
        <pre class="brush: js;">
var  <%=ValidateFor().Replace("},{","},\r\n{").Replace("],[","],\r\n[").Replace("=[","=[\r\n").Replace("[{","[\r\n{").Replace("}]","}\r\n]")%>
    </pre>
    </div>
    <div id="message">
    </div>
</body>
</html>
