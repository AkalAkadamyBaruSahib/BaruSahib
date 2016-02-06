<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultiSelect.aspx.cs" Inherits="Example_MultiSelect" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div>
        <asp:DropDownCheckBoxes ID="ddchkCountry" runat="server" 
                    AddJQueryReference="True" UseButtons="True" UseSelectAllNode="True">
                    <Style SelectBoxWidth="200" DropDownBoxBoxWidth="200" DropDownBoxBoxHeight="130" />
                    <Texts SelectBoxCaption="Select Country" />
                </asp:DropDownCheckBoxes>
    <asp:Label ID="lblCountryID" runat="server"></asp:Label><br />
    <asp:Label ID="lblCountryName" runat="server"></asp:Label>
    <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>--%>
    </div>    
    </div>
    </form>
</body>
</html>
