<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Example_Default3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem Value="-1">select</asp:ListItem>
            <asp:ListItem>abcd</asp:ListItem>
            <asp:ListItem>subh</asp:ListItem>
            <asp:ListItem>ram</asp:ListItem>

        </asp:DropDownList>
        <asp:TextBox ID="txtBillDate" runat="server" Width="100px" ></asp:TextBox><%--CssClass="input-xlarge datepicker"--%>
                                                <asp:ImageButton ID="imgbtnCalendar" runat="server" Width="20px" Height="20px" ImageUrl="~/img/calendar.png" />
                                                <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtBillDate" PopupButtonID="imgbtnCalendar" runat="server" />
    </div>
    </form>
</body>
</html>
