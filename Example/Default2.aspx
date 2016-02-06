<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function popup() {
            var dd = document.getElementById('<%= ddl_Names.ClientID %>').selectedIndex;
        //alert(dd);
        var yr = document.getElementById('<%= ddl_Names.ClientID %>').options[dd].value;
        //alert(yr);
        if (yr == "-1") {
            alert('Select Something');
        }
        else {
            window.open("GridViewWithCheckBox.aspx?EstId=" + yr, "List", "scrollbars=no,resizable=no,width=400,height=480");
        }
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    Select Estimate :
     <asp:DropDownList ID="ddl_Names" runat="server" Width="108px" ></asp:DropDownList>
    <a href="#" onclick="javascript:popup();return false;"> Show Details</a>
    </div>
    </form>
</body>
</html>
