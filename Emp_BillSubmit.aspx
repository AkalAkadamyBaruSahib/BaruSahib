<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Emp_BillSubmit.aspx.cs" Inherits="Emp_BillSubmit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Admin/UserControls/BodyVendorInformation.ascx" TagPrefix="uc1" TagName="BodyVendorInformation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .hidden
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function OpenVendorInfo() {
            $("#divVendorInformation").modal('show');
        }
    </script>
    <script src="JavaScripts/Vendor.js"></script>
    <script src="JavaScripts/CivilGenerateBill.js"></script>
    <asp:HiddenField ID="hdnAmtSan" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnBillID" runat="server"></asp:HiddenField>
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Submit Bill</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                   <%-- <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="civilBill" />--%>
                <div class="box-content">
                    <table>
                        <tr>
                            <td>Bill Type :</td>
                            <td>
                                <asp:DropDownList ID="ddlBillType1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBillType1_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Select Bill Type</asp:ListItem>
                                    <asp:ListItem Value="1">Sanctioned</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="control-label" for="typeahead" style="font-size: medium;" runat="server" id="lblChargeable" visible="false">Chargeable To :</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBillType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBillType_SelectedIndexChanged"></asp:DropDownList>
                                <asp:DropDownList ID="ddlEsimate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEsimate_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="control-label" for="typeahead" runat="server" id="lblNameWork" visible="false">Name of Work :</label>
                            </td>
                            <td>
                                <h4>
                                    <asp:Label ID="lblNameOfWork" runat="server" Visible="false" Style="font-size: medium;"></asp:Label></h4>
                                <asp:DropDownList ID="ddlNameOfWork" OnSelectedIndexChanged="ddlNameOfWork_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr runat="server" id="trMatSelect">
                            <td colspan="2">
                                <div class="control-group">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped table-bordered bootstrap-datatable datatable">
                                        <AlternatingRowStyle />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCtrl" runat="server" />
                                                    <asp:HiddenField ID="hdnMatID" Value='<%# Eval("MatId") %>' runat="server" />
                                                    <asp:HiddenField ID="hdnUnitId" Value='<%# Eval("UnitId") %>' runat="server" />
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MatName" HeaderText="Mat Name" ItemStyle-Width="150"  />
                                            <asp:BoundField DataField="UnitName" HeaderText="Unit" ItemStyle-Width="0" />
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="0" />
                                            <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-Width="0" />
                                            <asp:BoundField DataField="EstBal" HeaderText="Estimate Balance" ItemStyle-Width="0" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="btnShowData" runat="server" Text="Add Material" CssClass="btn btn-primary" OnClick="btnShowData_Click" />
                                    <asp:Label ID="lblData" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <asp:Panel runat="server" ID="pnlEstimateDetails" Visible="false">
                            <tr>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>Agency Name :
                                                <input type="text" id="txtAgencyName" style="position: absolute; width: 150px;" name="txtAgencyName" required />
                                                <div id="menu-container0" style="position: absolute; width: 500px;"></div>
                                                <br />
                                                <br />
                                                  <a href="#" id="aCreateNewVEndor" runat="server" style="float: right; margin-right: 96px; margin-top: -29px;" onclick="OpenVendorInfo();">Create New Agency</a>
                                            </td>
                                            <td>Agency Bill Number :
                                                <asp:TextBox ID="txtAgenyBillNo" runat="server" Width="100px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqAgencyName" runat="server"  ControlToValidate="txtAgenyBillNo" ErrorMessage="Please enter Agency Bil Number." ForeColor="Red" ValidationGroup="civilBill"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Bill Date :
                                                <asp:TextBox ID="txtBillDate" runat="server" Width="100px" Style="float: left; margin-left: 90px; margin-right: -205px;" CssClass="input-xlarge datepicker"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqBillDate" runat="server" style="float: right;  margin-right: 145px;" ControlToValidate="txtBillDate" ErrorMessage="Please enter Bill Date." ForeColor="Red" ValidationGroup="civilBill"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>Agency Bill Upload :
                                                <asp:FileUpload ID="fileAgencyBill" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fileAgencyBill" ErrorMessage="Please Upload the Agency Bill." ForeColor="Red" ValidationGroup="civilBill"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                            <td>Gate Entry No :
                                                <asp:TextBox ID="txtGateEntryNo" runat="server" Width="100px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqGateEntryNo" runat="server"  ControlToValidate="txtGateEntryNo" ErrorMessage="Please enter Gate Entry No." ForeColor="Red" ValidationGroup="civilBill"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlNonSanction" runat="server">
                                                    <asp:GridView runat="server" ID="gvAddItems" AutoGenerateColumns="false" OnRowDataBound="gvAddItems_RowDataBound" class="table table-striped table-bordered bootstrap-datatable datatable">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <div style="text-align: right;">
                                                                        <asp:Label ID="lblSno" runat="server" class="control-label"></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Stock Entery" ItemStyle-Width="115px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtStocEntry" runat="server" CssClass="span6 typeahead" Width="70Px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material Type" ItemStyle-Width="115px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList runat="server" ID="ddlMatType" Width="115Px" AutoPostBack="true" OnSelectedIndexChanged="ddlMatType_SelectedIndexChanged"></asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Name" ItemStyle-Width="115px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList runat="server" ID="ddlMat" Width="85Px" AutoPostBack="true" OnSelectedIndexChanged="ddlMat_SelectedIndexChanged"></asp:DropDownList>
                                                                    <asp:TextBox ID="txtItmName" runat="server" CssClass="span6 typeahead" Width="70Px" Visible="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtQty" runat="server" CssClass="span6 typeahead" Width="70Px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblNonSUnit" class="control-label"></asp:Label>
                                                                    <asp:TextBox ID="txtUnit" runat="server" CssClass="span6 typeahead" Width="70Px" Visible="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRate" runat="server" CssClass="span6 typeahead" Width="70Px" AutoPostBack="true" OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmt" runat="server" class="control-label"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTtlAmt" runat="server" class="control-label"></asp:Label>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Add More" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="ButtonAdd2" runat="server" Text="+" ToolTip="Add New Row" OnClick="ButtonAdd2_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlSanction" runat="server">
                                                    <asp:GridView runat="server" ID="gvAddItems2" AutoGenerateColumns="false" DataKeyNames="MatId" ShowFooter="true" class="table table-striped table-bordered bootstrap-datatable datatable">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="MatName(UnitName)" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdnSno" runat="server" />
                                                                    <asp:HiddenField ID="txtMatId" Value='<%# Eval("MatId") %>' runat="server" />
                                                                    <asp:HiddenField ID="txtUnitId" Value='<%# Eval("UnitId") %>' runat="server" />
                                                                    <asp:HiddenField ID="txtUnitName" Value='<%# Eval("UnitName") %>' runat="server" />
                                                                    <asp:Label ID="txtMatName" runat="server" Text='<%#Eval("MatName")+"("+ Eval("UnitName") +")" %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                       <asp:TextBox ID="txtQty" runat="server" CssClass="span6 typeahead" Width="70px" ToolTip="Qty is not more than Estimate Qty"></asp:TextBox> 
                                                                       <asp:RequiredFieldValidator ID="reqtxtQty" runat="server"  ControlToValidate="txtQty" style="float:right;margin-right: -6px; margin-top: -32px;" ErrorMessage="*"  ForeColor="Red" ValidationGroup="civilBill"></asp:RequiredFieldValidator>
                                                                       <span id="spnQty"  style="display:none; color:red;">Please enter Qty is less than Estimate Qty</span>
                                                                      </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRateSan" runat="server" CssClass="span6 typeahead" Width="70Px" ToolTip="Rate + Vat is not more than Estimate Rate" ></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="reqtxtRateSan" runat="server" ControlToValidate="txtRateSan" style="float:right;margin-right: -6px; margin-top: -32px;" ErrorMessage="*"  ForeColor="Red" ValidationGroup="civilBill"></asp:RequiredFieldValidator>
                                                                     <span id="spnRate" style="display:none;color:red;">Please enter Rate + Vat is less than Estimate Rate</span>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="VAT Included" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkVat" runat="server" Checked="true"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Vat" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtVat" runat="server" CssClass="span6 typeahead" Width="70Px" Enabled="false"></asp:TextBox>
                                                                      <span id="spnVat" style="display:none; color:red;">Please enter Rate + Vat is less than Estimate Rate</span>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmountFooter" runat="server" Text="Total Amount" ForeColor="Red" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmtSan" Text="0.00" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblTotalEstimateCost" runat="server" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Stock Entery No" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtStockEntry" runat="server" CssClass="span6 typeahead" Width="70Px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="reqtxtStockEntry" runat="server" ControlToValidate="txtStockEntry" style="float:right;margin-right: 15px; margin-top: -32px;" ErrorMessage="*"  ForeColor="Red" ValidationGroup="civilBill"></asp:RequiredFieldValidator>
                                                                 </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Est Qty" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEstQty" runat="server" CssClass="span6 typeahead" Width="70Px" Text='<%# Bind("EstQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Est Rate" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEstRate" runat="server" CssClass="span6 typeahead" Width="70Px" Text='<%# Bind("EstRate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bal Qty" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBalQty" runat="server" CssClass="span6 typeahead" Width="70Px" Text='<%# Bind("BalQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Remark If Any :</td>
                                            <td>
                                                <asp:TextBox ID="txtRemark" runat="server" Width="550px" Style="float: left; margin-left: -360px;"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button runat="server" ID="btnAmtTotal" Text="Total Amount" CssClass="btn btn-success" OnClick="btnAmtTotal_Click" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEstimateCost" runat="server" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td colspan="2">
                                <div class="form-actions" runat="server" id="divFinalButtons" visible="false">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit for Approval" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="civilBill" />
                                    <asp:Button ID="btnCl" runat="server" CssClass="btn btn-primary" Text="Cancel" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="divVendorInformation" class="modal hide fade" style="width: 900px;">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">×</button>
                <h3>Create New Agency</h3>
            </div>
            <div class="modal-body" style="width: 1000px;">
                <uc1:BodyVendorInformation runat="server" ID="BodyVendorInformation" />
            </div>
            <div class="modal-footer">
                <a href="#" class="btn btn-primary" data-dismiss="modal">Close</a>
            </div>
        </div>
    </div>
    <div id="pnlHtml" runat="server"></div>
</asp:Content>

