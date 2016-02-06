<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditEstimateaspx.aspx.cs" Inherits="Example_EditEstimateaspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="gvDetails" DataKeyNames="Sno" runat="server" 
        AutoGenerateColumns="false" CssClass="Gridview" HeaderStyle-BackColor="#61A6F8" 
ShowFooter="true" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White"  onrowediting="gvDetails_RowEditing" OnRowCancelingEdit="gvDetails_RowCancelingEdit" OnRowUpdating="gvDetails_RowUpdating" 
        >
   
     
<Columns>
<asp:TemplateField>
<EditItemTemplate>
<asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Example/Images/update.jpg" ToolTip="Update" Height="20px" Width="20px" />
<asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Example/Images/Cancel.jpg" ToolTip="Cancel" Height="20px" Width="20px" />

</EditItemTemplate>
<ItemTemplate>
<asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Example/Images/Edit.jpg" ToolTip="Edit" Height="20px" Width="20px" />
</ItemTemplate>
<%--<FooterTemplate>
<asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Example/Images/AddNewitem.jpg" CommandName="AddNew" Width="30px" Height="30px" ToolTip="Add new User" ValidationGroup="validaiton" />

</FooterTemplate>--%>
 </asp:TemplateField>
<asp:TemplateField HeaderText="EstmateId">
<EditItemTemplate>
<asp:Label ID="lblEstIdEdit" runat="server" Text='<%#Eval("EstId") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblEstId" runat="server" Text='<%#Eval("EstId") %>'/>
</ItemTemplate>

</asp:TemplateField>
 <asp:TemplateField HeaderText="Material Type">
 <EditItemTemplate>
<%-- <asp:TextBox ID="txtcity" runat="server" Text='<%#Eval("MatTypeId") %>'/>--%>
     <asp:DropDownList runat="server" ID="ddlMatTId" OnSelectedIndexChanged="ddlMatTId_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
 </EditItemTemplate>
 <ItemTemplate>
 <asp:Label ID="lblMatT" runat="server" Text='<%#Eval("MatTypeName") %>'/>
 </ItemTemplate>

 </asp:TemplateField>
 <asp:TemplateField HeaderText="Material">
 <EditItemTemplate>
 <%--<asp:TextBox ID="txtstate" runat="server" Text='<%#Eval("TAddr") %>'/>--%>
     <asp:DropDownList runat="server" ID="ddlMatId" OnSelectedIndexChanged="ddlMatId_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
 </EditItemTemplate>
 <ItemTemplate>
 <asp:Label ID="lblMat" runat="server" Text='<%#Eval("MatName") %>' />
 </ItemTemplate>
 
 </asp:TemplateField>
     <asp:TemplateField HeaderText="Unit">
 <EditItemTemplate>
 <asp:Label ID="lblUnitEdit" runat="server" Text='<%#Eval("UnitName") %>' />
 </EditItemTemplate>
 <ItemTemplate>
 <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("UnitName") %>'/>
 </ItemTemplate>
 
 </asp:TemplateField>
     <asp:TemplateField HeaderText="Qty">
 <EditItemTemplate>
 <asp:TextBox ID="txtQty" runat="server" Text='<%#Eval("Qty") %>'/>
 </EditItemTemplate>
 <ItemTemplate>
 <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Qty") %>'/>
 </ItemTemplate>
 
 </asp:TemplateField>
        <asp:TemplateField HeaderText="Rate">
 <EditItemTemplate>
 <asp:TextBox ID="txtRate" runat="server" Text='<%#Eval("Rate") %>' OnTextChanged="txtRate_TextChanged" AutoPostBack="true"/>
 </EditItemTemplate>
 <ItemTemplate>
 <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Rate") %>'/>
 </ItemTemplate>
 
 </asp:TemplateField>
         <asp:TemplateField HeaderText="Amount">
 <EditItemTemplate>
 <asp:Label ID="txtAmtEdit" runat="server" Text='<%#Eval("Amount") %>'/>
 </EditItemTemplate>
 <ItemTemplate>
 <asp:Label ID="lblAmt" runat="server" Text='<%#Eval("Amount") %>'/>
 </ItemTemplate>
 
 </asp:TemplateField>
       <asp:TemplateField HeaderText="Source Type">
 <EditItemTemplate>
 <%--<asp:Label ID="txtAmtEdit" runat="server" Text='<%#Eval("Amount") %>'/>--%>
     <asp:DropDownList runat="server" ID="ddlPs"></asp:DropDownList>
 </EditItemTemplate>
 <ItemTemplate>
 <asp:Label ID="lblPs" runat="server" Text='<%#Eval("PSName") %>'/>
 </ItemTemplate>
 
 </asp:TemplateField>
 </Columns>
</asp:GridView>
    </div>
    </form>
</body>
</html>
