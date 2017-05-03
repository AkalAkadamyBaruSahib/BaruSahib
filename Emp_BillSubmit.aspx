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
    <asp:HiddenField ID="hdnVandorID" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnInchargeID" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnAcaID" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnZoneID" runat="server"></asp:HiddenField>
     <asp:HiddenField ID="hdnSubBillID" runat="server"></asp:HiddenField>
     <asp:HiddenField ID="hdnTotalAmount" runat="server"></asp:HiddenField>
     <asp:HiddenField ID="hdnAmount" runat="server"></asp:HiddenField>
     <asp:HiddenField ID="hdnUpdateBillID" runat="server"></asp:HiddenField>
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
                                    <asp:ListItem Value="2">NonSanctioned</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="control-label" for="typeahead" style="font-size: medium;" runat="server" id="lblChargeable" visible="false">Chargeable To :</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBillType" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlBillType_SelectedIndexChanged"></asp:DropDownList>
                                <asp:DropDownList ID="ddlEsimate" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlEsimate_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="control-label" for="typeahead" runat="server" id="lblNameWork" visible="false">Name of Work :</label>
                            </td>
                            <td>
                                <h4>
                                    <asp:Label ID="lblNameOfWork" runat="server" Visible="false" Style="font-size: medium;"></asp:Label></h4>
                                <asp:DropDownList ID="ddlNameOfWork"  OnSelectedIndexChanged="ddlNameOfWork_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
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
                                            <asp:BoundField DataField="MatName" HeaderText="Mat Name" ItemStyle-Width="150" />
                                            <asp:BoundField DataField="UnitName" HeaderText="Unit" ItemStyle-Width="0" />
                                            <asp:BoundField DataField="Quantity" HeaderText="Required Quantity" ItemStyle-Width="0" />
                                            <asp:BoundField DataField="BillQty" HeaderText="Purchased Quantity" ItemStyle-Width="0" />
                                            <asp:BoundField DataField="EstBal" HeaderText="Estimate Balance" ItemStyle-Width="0" />
                                            <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-Width="0" />
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
                                                <div id="menu" style="position: absolute; width: 500px;"></div>
                                                <br />
                                                <br />
                                                <a href="#" id="aCreateNewVEndor" runat="server" style="float: right; margin-right: 50px; margin-top: -29px;" onclick="OpenVendorInfo();">Create New Agency</a>
                                            </td>
                                            <td>Agency Bill Number :
                                                <asp:TextBox ID="txtAgenyBillNo" runat="server" Width="100px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqAgencyName" runat="server" ControlToValidate="txtAgenyBillNo" ErrorMessage="Please enter Agency Bil Number." ForeColor="Red" ValidationGroup="civilBill"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Bill Date :
                                                <asp:TextBox ID="txtBillDate" runat="server" Width="100px" Style="float: left; margin-left: 90px; margin-right: -205px;" CssClass="input-xlarge datepicker"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqBillDate" runat="server" Style="float: right; margin-right: 145px;" ControlToValidate="txtBillDate" ErrorMessage="Please enter Bill Date." ForeColor="Red" ValidationGroup="civilBill"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>Agency Bill Upload :
                                                <asp:FileUpload ID="fileAgencyBill" AllowMultiple="true" runat="server" />
                                                  <div id="afileVendorBillPath" style="display:none;"></div>
                                                <a href="#" runat="server" id="afilePath" visible="false" style="font-size: 13px;" target="_blank">Bill Copy</a>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fileAgencyBill" ErrorMessage="Please Upload the Agency Bill." ForeColor="Red" ValidationGroup="civilBill"></asp:RequiredFieldValidator>
                                           
                                            </td>
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                            <td>Gate Entry No :
                                                <asp:TextBox ID="txtGateEntryNo" runat="server" Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="height:30px;" colspan="2">
                                              <span id="spnmsg" style="font-size:15px;color:red;"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlNonSanction"  runat="server">
                                                    <table id="tblEstimateMatDetail" style="width: 100%;" class='table table-striped table-bordered'>
                                                        <thead>
                                                            <tr>
                                                                <th style="color: #cc3300; width: 30px;">Sr No</th>
                                                                <th style="color: #cc3300; width: 140px;">Source Type</th>
                                                                <th style="color: #cc3300; width: 250px;">MaterialName</th>
                                                                <th style="color: #cc3300; width: 180px;">Material Type</th>
                                                                <th style="color: #cc3300; width: 120px;">Quantity</th>
                                                                <th style="color: #cc3300; width: 50px;">Unit</th>
                                                                <th style="color: #cc3300; width: 100px">Rate</th>
                                                                <th style="color: #cc3300; width: 200px">Total</th>
                                                                <th style="color: #cc3300; width: 75px;">Action</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tbody2">
                                                            <tr id="tr0">
                                                                <td>
                                                                    <span id="spn0">1</span>
                                                                </td>
                                                                <td>
                                                                    <select id="ddlSourceType0" onchange="SourceType_ChangeEvent(0);" style="width: 150px;">
                                                                        <option value="0">Select Source Type</option>
                                                                    </select>
                                                                </td>
                                                                <td>
                                                                    <input id="txtMaterialName0" name="txtMaterialName1" style="position: absolute; width: 200px;" onblur="MaterialTextBox_ChangeEvent(0);" type="text" class="span6 typeahead" />
                                                                    <br />
                                                                    <br />
                                                                    <div id="menu-container0" style="position: absolute; width: 500px;"></div>

                                                                </td>
                                                                <td>
                                                                    <span id="spnMaterialTypeID0"></span>
                                                                </td>
                                                                <td>
                                                                    <input id="txtQty0" type="text" onchange="Qty_ChangeEvent(0);" style="width: 80px;" />
                                                                </td>
                                                                <td>
                                                                    <label id="lblUnit0"></label>
                                                                </td>
                                                                <td>
                                                                    <input id="txtRate0" type="text" style="width: 80px;" onchange="Rate_ChangeEvent(0);" />
                                                                </td>
                                                                <td>
                                                                    <span id="txtTotal0" class="span6 typeahead" ></span>
                                                                </td>
                                                                <td>
                                                                    <a href="javascript:void(0);" id="aAddNewRow0" onclick="AddMaterialRow();"><b>Add Row</b></a>
                                                                    <a href="javascript:void(0);" id="aDeleteRow0" onclick="removeRow(0);"><b>Delete</b></a>
                                                                    <input type="hidden" id="hdnMatID0" /><input type="hidden" id="hdnMatTypeID0" /><input type="hidden" id="hdnUnitID0" /><input type="hidden" id="hdnMaterialTypeName0" />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <input type="button" id="btnEstimateCost" value="BillCost" title="BillCost" style="float: left; margin-left: 570px;" class="btn btn-success" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblAmt" ForeColor="Red" Font-Bold="true" Text="0.00" Style="float: right; margin-right: 450px;" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>


                                                </asp:Panel>
                                                <asp:Panel ID="pnlSanction" runat="server">
                                                    <asp:GridView runat="server" ID="gvAddItems2" AutoGenerateColumns="false" DataKeyNames="MatId" ShowFooter="true" class="table table-striped table-bordered bootstrap-datatable datatable">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="MatName(UnitName)" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
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
                                                                    <asp:RequiredFieldValidator ID="reqtxtQty" runat="server" ControlToValidate="txtQty" Style="float: right; margin-right: -6px; margin-top: -32px;" ErrorMessage="*" ForeColor="Red" ValidationGroup="civilBill"></asp:RequiredFieldValidator>
                                                                    <span id="errMsgQty" style="display: none; color: red;">Digit only</span>
                                                                    <span id="spnQty" style="display: none; color: red;">Qty should be less then BalQty.</span>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtRateSan" runat="server" CssClass="span6 typeahead" Width="70Px" ToolTip="Rate + Vat is not more than Estimate Rate"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="reqtxtRateSan" runat="server" ControlToValidate="txtRateSan" Style="float: right; margin-right: -6px; margin-top: -32px;" ErrorMessage="*" ForeColor="Red" ValidationGroup="civilBill"></asp:RequiredFieldValidator>
                                                                    <span id="errMsg" style="display: none; color: red;">Digit only</span>
                                                                    <span id="spnRate" style="display: none; color: red;">Rate should be less then Est. Rate (Rate+VAT)</span>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="VAT Included" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkVat" runat="server" Checked="true"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Vat" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtVat" runat="server" CssClass="span6 typeahead" Width="40Px" Enabled="false"></asp:TextBox>
                                                                    <span id="spnVat" style="display: none; color: red;">Rate should be less then Est. Rate (Rate+VAT)</span>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmountFooter" runat="server" Text="Total Amount" ForeColor="Red" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
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
                                                                    <asp:RequiredFieldValidator ID="reqtxtStockEntry" runat="server" ControlToValidate="txtStockEntry" Style="float: right; margin-right: 15px; margin-top: -32px;" ErrorMessage="*" ForeColor="Red" ValidationGroup="civilBill"></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Est Qty" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEstQty" runat="server" CssClass="span6 typeahead" Width="70Px" Text='<%# Bind("EstQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Pur Qty" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPurQty" runat="server" CssClass="span6 typeahead" Width="70Px" Text='<%# Bind("PurQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bal Qty" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBalQty" runat="server" CssClass="span6 typeahead" Width="70Px" Text='<%# Bind("BalQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Est Rate" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEstRate" runat="server" CssClass="span6 typeahead" Width="70Px" Text='<%# Bind("EstRate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr id="trRemarks" runat="server" visible="false">
                                            <td>Remark If Any :</td>
                                            <td>
                                                <asp:TextBox ID="txtRemark" runat="server" Width="550px" Style="float: left; margin-left: -360px;"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button runat="server" ID="btnAmtTotal" Text="Total Amount" CssClass="btn btn-success" Visible="false" />
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
                                    <asp:Button ID="btnSubmit" runat="server" Visible ="false" Text="Submit for Approval" CssClass="btn btn-primary" OnClick="btnSubmit_Click"  ValidationGroup="civilBill" />
                                    <input type="button" id="btnSubmitApprovel" value="Submit for Approval" title="Submit for Approval" class="btn btn-primary" style="display:none;" />
                                    <asp:Button ID="btnCl" runat="server" CssClass="btn btn-primary" Text="Cancel" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="divVendorInformation" class="modal hide fade" style="width: 950px;">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">×</button>
                <h3>Create New Agency</h3>
            </div>
            <div class="modal-body" style="width: 1000px;">
                <uc1:BodyVendorInformation runat="server" ID="BodyVendorInformation" IsOpenInPopUP="true" />
            </div>
            <div class="modal-footer">
                <a href="#" class="btn btn-primary" data-dismiss="modal">Close</a>
            </div>
        </div>
    </div>
    <div id="pnlHtml" runat="server"></div>
     <div id="progress" class="modal hide fade" style="width: 900px;">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">×</button>
                <h3>Create New Agency</h3>
            </div>
            <div class="modal-body" style="width: 1000px;">
                <img src="img/animated.gif" /><br />
                Wait while Materials is uploading....
            </div>
            <div class="modal-footer">
                <a href="#" class="btn btn-primary" data-dismiss="modal">Close</a>
            </div>
        </div>
</asp:Content>

