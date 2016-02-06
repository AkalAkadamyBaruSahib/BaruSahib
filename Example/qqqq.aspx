<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qqqq.aspx.cs" Inherits="Example_qqqq" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="Panel24" runat="server" Height="300px" Width="400px" BorderStyle="Solid" BorderWidth="1" Direction="LeftToRight" ScrollBars="Auto" BackColor="#EDECEC"
                                        CssClass="txt2">
                                       
                                       <asp:GridView ID="grvCategory" runat="server" AutoGenerateColumns="false" DataKeyNames="MatId"
			                                     CssClass="Grid" Width="400px" ShowHeader="False">
			                                    <Columns>
				                                    <asp:TemplateField ItemStyle-Width="320px">
					                                    <ItemTemplate>
						                                   <asp:CheckBox ID="chkSelect" runat="server" CssClass="skin-minimal" />
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("MatName") %>' ></asp:Label>
					                                    </ItemTemplate>
				                                    </asp:TemplateField>
				                                   <%-- <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category Name" />--%>
				
			                                    </Columns>
		                                    </asp:GridView>
                                    </asp:Panel>
        <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"/>
        <asp:Label ID="lblShow" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
