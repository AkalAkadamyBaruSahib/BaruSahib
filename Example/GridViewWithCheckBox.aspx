<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GridViewWithCheckBox.aspx.cs" Inherits="Example_GridViewWithCheckBox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#BFE4FF" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkCtrl" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
           <asp:BoundField DataField="MatId" HeaderText="Mat Id" ItemStyle-Width="70" />
            <asp:BoundField DataField="MatName" HeaderText="Mat Name" ItemStyle-Width="150" />
        </Columns>
    </asp:GridView>
        <asp:Button id="btnShowData" runat="server" Text="show data" OnClick="btnShowData_Click"/>
        <asp:Label ID="lblData" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
