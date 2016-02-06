<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="popUp.aspx.cs" Inherits="popUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/themes/flick/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js" type="text/javascript"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script src="Scripts/jquery-ui-1.8.20.min.js"></script>
           <script language="javascript" type="text/javascript">
               function openpopup() {
                   var a = document.getElementById('<%= DropDownList1.ClientID%>').selectedIndex;
               var b = document.getElementById('<%= DropDownList1.ClientID%>').options[a].value;
               if (b == "-1") {
                   alert('Please Select item');
               }
               if (b == "9") {
                   $("#divId").show({
                       show: "blind",
                       hide: "explode",
                       resizable: false,
                       modal: true,
                       height: 120,
                       width: 170
                   });

               }
           }
</script>
    <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <div id="divId" title="Dialog Title" style="width: 170px; height: 120px; display:none;" class="modal hide fade">
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:CheckBoxList ID="CheckBoxList1" runat="server">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
        </asp:CheckBoxList>
        <asp:LinkButton ID="Button1" runat="server" Text="click here" style="display:none" />
    </div>
</asp:Content>

