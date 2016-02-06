<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Dynamic Rows In Gridview</title>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
             <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
            <asp:GridView ID="grvStudentDetails" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting"
                Style="text-align: left" OnRowDataBound="grvStudentDetails_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="SNo" />
                    <asp:TemplateField HeaderText="Material Type" ItemStyle-Width="115px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="ddlMatType" Width="115Px" AutoPostBack="true" OnSelectedIndexChanged="ddlMatType_SelectedIndexChanged"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material" ItemStyle-Width="85px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="ddlMat" Width="85Px"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Source Type" ItemStyle-Width="105px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlSourceType" Width="105Px" ></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQty" runat="server" CssClass="span6 typeahead" Width="70Px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="ddlUnit" Width="70Px"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRate" runat="server" CssClass="span6 typeahead" Width="70Px" OnTextChanged="txtRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblAmt" runat="server" class="control-label"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="span6 typeahead" Width="70Px"></asp:TextBox>
                            </ItemTemplate>
                           <%-- <FooterStyle HorizontalAlign="Right" />
                            <FooterTemplate>
                                <asp:Button ID="ButtonAdd2" runat="server" Text="+" ToolTip="Add New Row" OnClick="ButtonAdd2_Click" />
                            </FooterTemplate>--%>
                        
                        <FooterStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
                                </ContentTemplate>
                 </asp:UpdatePanel>
            <br />
            <center>
                <asp:Button ID="btnSave" runat="server" Text="Save Data" OnClick="btnSave_Click" />
        </div>
        </center>
    </form>
</body>
</html>
