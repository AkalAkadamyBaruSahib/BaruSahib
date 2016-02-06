<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialShow.aspx.cs" Inherits="MaterialShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Please Select Material
        <asp:CheckBoxList ID="chkMaterial" runat="server" ></asp:CheckBoxList>
        <asp:Button id="btnDone" Text="Done" runat="server"/>
    </div>
    </form>
</body>
</html>
