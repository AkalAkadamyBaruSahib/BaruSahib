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
                                    <b>Select Academy:</b>
                                    <asp:DropDownList ID="drpAcademy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddrAcademy_SelectedIndexChanged"></asp:DropDownList><br />
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="control-group">
                                <label class="control-label" for="typeahead"></label>
                                <div class="controls">
                                    <b>Vehicle:</b>
                                    <asp:DropDownList ID="drpVehicle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpVehicle_SelectedIndexChanged"></asp:DropDownList><br />
                                </div>
                            </div>
                        </td>
                        <td id="tdtype" runat="server" visible="false">
                            <div class="control-group">
                                <label class="control-label" for="typeahead"></label>
                                <div class="controls">
                                    <b>Vehicle Type:</b> <asp:Label ID="lblVehicleType" runat="server"></asp:Label>
                                   </div>
                            </div>
                        </td>
                    </tr>
                    <tr id="tdDrverInfo" runat="server" visible="false">
                        <td>
                            <div class="control-group">
                                <label class="control-label" for="typeahead"></label>
                                <div class="controls">
                                    <b>Driver Name:</b>
                                    <asp:Label ID="lblDriverName" runat="server"></asp:Label><br />
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="control-group">
                                <label class="control-label" for="typeahead"></label>
                                <div class="controls">
                                    <b>Driver Mobile Number:</b><asp:Label ID="lblDriverMobile" runat="server"></asp:Label><br />
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="control-group">
                                <label class="control-label" for="typeahead"></label>
                                <div class="controls">
                                    <b>DL Number:</b> <asp:Label ID="lblDLNumber" runat="server"></asp:Label></div>
                            </div>
                        </td>
                    </tr>
                    <tr id="tdDLtype" runat="server" visible="false">
                        <td>
                            <div class="control-group">
                                <label class="control-label" for="typeahead"></label>
                                <div class="controls">
                                    <b>DL Type:</b><asp:Label ID="lblDLType" runat="server" ></asp:Label><br />
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="control-group">
                                <label class="control-label" for="typeahead"></label>
                                <div class="controls">
                                    <b>Driving From Date:</b><asp:Label ID="lblDrivingDate" runat="server" ></asp:Label><br />
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="control-group">
                                <label class="control-label" for="typeahead"></label>
                                <div class="controls">
                                    <b>DL Copy:</b> <asp:Label ID="lblDlCopy" runat="server"></asp:Label></div>
                            </div>
                        </td>
                    </tr>
                    <tr id="tdConductorInfo" runat="server" visible="false">
                        <td>
                            <div class="control-group">
                                <label class="control-label" for="typeahead"></label>
                                <div class="controls">
                                    <b>Conductor Name:</b> <asp:Label ID="lblConductorName" runat="server"></asp:Label></div>
                            </div>
                        </td>
                        <td>
                            <div class="control-group">
                                <label class="control-label" for="typeahead"></label>
                                <div class="controls">
                                    <b>Conductor Mobile Number:</b>
                                    <asp:Label ID="lblCondutorMobile" runat="server"></asp:Label><br />
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="control-group">
                                <label class="control-label" for="typeahead"></label>
                                <div class="controls">
                                    <b>Experience:</b>
                                    <asp:Label ID="lblExperince" runat="server"></asp:Label><br />
                                </div>
                            </div>
                        </td>
                       
                    </tr>
                    <tr id="trconductordate" runat="server" visible="false">
                         <td>
                            <div class="control-group">
                                <label class="control-label" for="typeahead"></label>
                                <div class="controls">
                                    <b>Conductor From Date:</b><asp:Label ID="lblConductorDate" runat="server"></asp:Label>
                                   </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3"  align="left">
                            <asp:GridView ID="gvDocuments" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                                CellPadding="5" Width="1000px" ForeColor="#333333" OnRowDataBound="gvDocuments_RowDataBound" GridLines="None" Style="text-align: left">
                                <Columns>
                                    <asp:TemplateField HeaderText="Document Type"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocumentType" runat="server" Text='<%# Eval("DocumentName") %>' class="control-label"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Expiry Date"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="input-large datepicker">(mm/dd/yyyy)</asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View/Download" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocumentTypeID" runat="server" Text='<%# Eval("ID") %>' Visible="true" class="control-label"></asp:Label>
                                            <asp:Label ID="lblDocu" Text="-1" runat="server" Visible="true" class="control-label"></asp:Label>
                                            <asp:HyperLink ID="hypDoc" Target="_blank" runat="server" Text="No document Uploaded"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Upload Document" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:FileUpload ID="fiupload" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Button ID="btn_Approved" Visible="false"  Text="Delete"  CommandArgument='<%#Eval("ID")%>' runat="server" CssClass="btn btn-primary" OnClick="btn_Approved_Click" />
                                            <asp:Button ID="btnApproved" Visible="false" Enabled="false" width="100px"  Text="Approved" CommandArgument='<%#Eval("ID")%>' runat="server" CssClass="btn btn-success" OnClick="btnApproved_Click"/>
                                            <asp:Button ID="btnExpire" Visible="false" Enabled="false" width="100px"  Text="Expire" runat="server" CssClass="btn btn-primary" />
                        
                                                </ItemTemplate>
                              
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