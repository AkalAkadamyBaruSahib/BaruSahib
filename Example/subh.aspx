<%@ Page Language="C#" AutoEventWireup="true" CodeFile="subh.aspx.cs" Inherits="Example_subh" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/themes/flick/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js" type="text/javascript"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script src="Scripts/jquery-ui-1.8.20.min.js"></script>

    <title>PopUp window on Dropdown selection</title>
    <script language="javascript" type="text/javascript">
        function openpopup() {
            var a = document.getElementById('<%= DropDownList1.ClientID%>').selectedIndex;
            var b = document.getElementById('<%= DropDownList1.ClientID%>').options[a].value;
            if (b == "-1") {
                alert('Please Select item');
            }
            if (b == "9") {
                $("#divId").dialog({
                });
            }
        }
    </script>
   

</head>
<body>
    
    <form id="form2" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
        <div id="divId" title="Dialog Title" style="width: 170px; height: 120px; display: none; background-color: lightskyblue; resize: both;">
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
            </asp:CheckBoxList>
            <asp:LinkButton ID="Button1" runat="server" Text="click here"   />
            <asp:Button runat="server" Text="save" id="btnSave" OnClick="btnSave_Click"/>
        </div>
                                </ContentTemplate>
            </asp:UpdatePanel>
        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    </form>

</body>
</html>
