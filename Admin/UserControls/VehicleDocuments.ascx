<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VehicleDocuments.ascx.cs" Inherits="Admin_UserControls_VehicleDocuments" %>
<span class="labelH labelH-info">Documents</span>
<div id="content" class="span10">
    <div class="row-fluid sortable">
        <div class="box span12">

            <div class="box-content">
                <table border="0">
                    <tr>
                        <td>
                            <div class="control-group">
                                <label class="control-label" for="typeahead"></label>
                                <div class="controls">
                                    Select Academy<br />
                                    <asp:DropDownList ID="drpAcademy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddrAcademy_SelectedIndexChanged"></asp:DropDownList><br />
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="control-group">
                                <label class="control-label" for="typeahead"></label>
                                <div class="controls">
                                    Vehicle<br />
                                    <asp:DropDownList ID="drpVehicle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpVehicle_SelectedIndexChanged"></asp:DropDownList><br />
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="50%" align="left">
                            <asp:GridView ID="gvDocuments" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                                CellPadding="4" Width="900px" ForeColor="#333333" OnRowDataBound="gvDocuments_RowDataBound" GridLines="None" Style="text-align: left">
                                <Columns>
                                    <asp:TemplateField HeaderText="Document Type" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocumentType" runat="server" Text='<%# Eval("DocumentName") %>' class="control-label"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Expiry Date" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="input-large datepicker">(mm/dd/yyyy)</asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View/Download" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocumentTypeID" runat="server" Text='<%# Eval("ID") %>' Visible="true" class="control-label"></asp:Label>
                                            <asp:Label ID="lblDocu" Text="-1" runat="server" Visible="true" class="control-label"></asp:Label>
                                            <asp:HyperLink ID="hypDoc" Target="_blank" runat="server" Text="No document Uploaded"></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="300px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Upload Document" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:FileUpload ID="fiupload" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="400px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Button ID="btn_Approved" Visible="false" Text="Delete" CommandArgument='<%#Eval("ID")%>' runat="server" CssClass="btn btn-primary" OnClick="btn_Approved_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#3f9fd9" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="LightGray" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</div>