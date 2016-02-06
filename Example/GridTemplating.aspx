<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GridTemplating.aspx.cs" Inherits="Example_GridTemplating" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" ShowFooter="True" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" >
          <Columns>
           <asp:TemplateField>
              <FooterTemplate>
               <asp:LinkButton ID="LkB1" runat="server" CommandName="Select">Insert</asp:LinkButton>
              </FooterTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Name">
              <ItemTemplate>
                 <asp:Label ID="Label1" runat="server" Text='<%# Eval("Tname") %>'></asp:Label>
              </ItemTemplate>
              <FooterTemplate>
                 <asp:TextBox ID="txt_Name" runat="server"></asp:TextBox>
               </FooterTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Mob">
               <ItemTemplate>
                  <asp:Label ID="Label2" runat="server" Text='<%# Eval("Tmob")%>'></asp:Label>
               </ItemTemplate>
               <FooterTemplate>
                  <asp:TextBox ID="txt_Mob" runat="server"></asp:TextBox>
               </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address">
               <ItemTemplate>
                  <asp:Label ID="Label3" runat="server" Text='<%# Eval("TAddr") %>'></asp:Label>
               </ItemTemplate>
               <FooterTemplate>
                  <asp:TextBox ID="txt_Addr" runat="server"></asp:TextBox>
               </FooterTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Email">
               <ItemTemplate>
                  <asp:Label ID="Label3" runat="server" Text='<%# Eval("Temail") %>'></asp:Label>
               </ItemTemplate>
               <FooterTemplate>
                  <asp:TextBox ID="txt_Email" runat="server"></asp:TextBox>
               </FooterTemplate>
            </asp:TemplateField>
          </Columns> 
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />       
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
