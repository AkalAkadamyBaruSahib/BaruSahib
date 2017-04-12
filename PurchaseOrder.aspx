<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="PurchaseOrder.aspx.cs" Inherits="PurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/PurchaseOrder.js"></script>
    <style>
        .headingTable
        {
            float: left;
            margin-left: -184px;
        }

        .heading
        {
            color: #cc3300;
            background-color: #CCCCCC;
            vertical-align: middle;
            text-align: center;
        }

        .footerheading
        {
            color: #cc3300;
            font-weight: bold;
        }

        .AddressDrp
        {
            width: 200px;
            margin-top: -34px;
            margin-left: 130px;
        }

        .instruction
        {
            width: 72px;
            height: 18px;
        }

        .tableSecondTrHeading
        {
            width: 35px;
            text-align: center;
            vertical-align: middle;
        }

        .HeaderSpan
        {
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">

        function Validation() {
            if (($("#txtPO").val() == '' || $("#txtcontact").val() == '' || $("#txtvat").val() == '' || $("#txtpayment").val() == '' || $("#txtAuthorised").val() == '')) {
                alert("Please Enter the value in Text Box..can not leave the Text box Empty");
            }
            if ($("#drpEstimate").val() == '0' || $("#drpVendor").val() == '0' || $("#drpDeliveryAddress").val() == '0' || $("#drpBillingAddress").val() == '0' || $("#drpFreight").val() == '0') {
                alert("Please Select the any value from DropDown");
            }
            return false;
        }
    </script>


    <div id="content" class="span10">
        <asp:HiddenField ID="hdnEstimateID" runat="server" />
        <asp:HiddenField ID="hdnSelectedItems" runat="server" />
        <asp:HiddenField ID="hdnItemsLength" runat="server" />
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>

                    <h2><i class="icon-user"></i>
                        PO </h2>
                    <div class="box-icon">

                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                    <%--OnClientClick="return Validation();" --%>
                </div>
                <div class="box-content">
                    <div class="box-content">
                        <table id="tblPO" width="100%">
                            <tbody>
                                <tr>
                                    <td style="float: left; width: 174px;">
                                        <span class="footerheading">Estimate:-</span>
                                        <select required="required" id="drpEstimate" style="width: 150px;">
                                            <option value="0">-Select Estimate--</option>
                                        </select>
                                    </td>
                                    <td>
                                        <label class="control-label" for="typeahead" style="color: #cc3300; font-weight: bold; margin-left: 828px;"><b>P.O.-</b></label>
                                        <asp:TextBox ID="txtPO" required="required" runat="server" Style="width: 150px; float: right; margin-top: -1px;"></asp:TextBox>
                                        <asp:Label ID="lblCurrentDate" runat="server" Visible="false"></asp:Label>
                                        <asp:HiddenField ID="hdnCurrentDate" runat="server" />
                                    </td>
                                </tr>
                            </tbody>

                        </table>
                        <table class='table table-striped table-bordered bootstrap-datatable datatable'>
                            <tr>
                                <td>
                                    <label class="control-label" for="typeahead" style="color: #cc3300; font-weight: bold;"><b>Vendor:</b></label>
                                </td>
                                <td style="width: 382px;">
                                    <div style="height: 65px;">
                                        M/S<asp:Label ID="lblName" runat="server" Style="margin-top: -18px; margin-left: 31px;"></asp:Label>
                                        <asp:HiddenField ID="hdnVendorName" runat="server" />
                                        <br />
                                        <asp:Label ID="lblVendorAddress" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnVendorAddress" runat="server" />
                                        <br />
                                        <asp:Label ID="lblCity" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnCity" runat="server" />
                                    </div>
                                </td>
                                <td>
                                    <label class="control-label" for="typeahead" style="color: #cc3300; font-weight: bold;"><b>SHIP TO:-</b></label>
                                    <div class="controls">
                                        <select id="drpDeliveryAddress">
                                            <option value="0">-Select Billing Address--</option>
                                        </select>
                                    </div>
                                </td>
                                <td style="width: 480px;">
                                    <div style="height: 50px;">
                                        <asp:Label ID="lblTrustName" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnTrustName" runat="server" />
                                        <br />
                                        <asp:Label ID="lblAdress" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnDeliveryAddress" runat="server" />
                                        <br />
                                        <asp:Label ID="lblPhoneNo" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdnDeliveryPhoneNo" runat="server" />
                                        <br />

                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-content">
                        <%--      <div id="divMatDetails" runat="server">--%>
                        <div id="divPurchaseOrderDetails" runat="server">
                            <table id="grid" width="100%" class='table table-striped table-bordered bootstrap-datatable datatable'>
                                <thead>
                                    <tr class="heading">
                                        <th style="text-align: center;">Sr No</th>
                                        <th style="text-align: center; width: 115px;">Qty</th>
                                        <th style="text-align: center;">Description</th>
                                        <th style="text-align: center;">Detail</th>
                                        <th style="text-align: center;">Unit Price</th>
                                        <th id="linetotal" style="text-align: center;">Line Total</th>
                                    </tr>
                                </thead>
                                <tbody id="tbody">
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <td rowspan="4" style="width: 65px;" colspan="4">
                                            <label id="lblDeliveryAddress" style="color: #cc3300"><u><b>DELIVERY ADDRESS:-</b></u></label>
                                            <div class="controls">
                                                <select id="drpBillingAddress" class="AddressDrp">
                                                    <option value="0">-Select Delivery Address--</option>
                                                </select>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblBillingName" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hdnBillingName" runat="server" />
                                                <br />
                                                <asp:Label ID="lblBillingAddres" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hdnBillingAddres" runat="server" />
                                                <br />
                                                <asp:Label ID="lblBillingPhone" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hdnBillingPhone" runat="server" />
                                                <br />
                                            </div>
                                            <br />
                                            <label id="lblContact" style="color: #cc3300"><u><b>Contact Person:-</b></u></label>
                                            <asp:TextBox required="required" ID="txtcontact" Style="margin-left: 27px; width: 117px;" runat="server"></asp:TextBox>
                                        </td>
                                        <th style="color: #cc3300;">Sub Total :</th>
                                        <td>
                                            <asp:Label ID="lblSubTotal" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnSubTotal" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="rowExcise">
                                        <th style="color: #cc3300;">Is Excise Included:</th>
                                        <td>
                                            <asp:TextBox ID="txtExcise" runat="server" Enabled="false" required="required" Style="width: 80px; display: none;"></asp:TextBox>
                                            <input type="checkbox" id="chkExcise" style="width: 10px; height: 10px;" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <th style="color: #cc3300;">VAT/CST :</th>
                                        <td>
                                            <asp:TextBox ID="txtvat" runat="server" required="required" Style="width: 80px;"></asp:TextBox>
                                    </tr>
                                    <tr>
                                        <th style="color: #cc3300;">Grand Total:</th>
                                        <td>
                                            <asp:Label ID="lblGrandTotal" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnGrandTotal" runat="server" />
                                        </td>
                                    </tr>

                                </tfoot>
                            </table>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <span class="footerheading">Mode Of Dispatch:-</span>
                                        By Road
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdnVatStatus" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 80px;">
                                        <span class="footerheading">Freight:-</span><select id="drpFreight" style="width: 130px; height: 24px; float: right; margin-right: -103px; margin-top: -21px;">
                                            <option value="0">--Select One--</option>
                                            <option value="Free On Road">Free On Road</option>
                                            <option value="Extra">Extra</option>
                                        </select>
                                        <asp:HiddenField ID="hdnFreight" runat="server" />
                                        <asp:Label ID="lblFreight" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdnExciseStatus" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 30px;">
                                        <span class="footerheading">Against Indent No:-</span><b> &nbsp;&nbsp;<asp:Label ID="lblIndentNo" runat="server"></asp:Label></b>
                                        <asp:HiddenField ID="hdnIndentNo" runat="server" />
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2" style="height: 30px;text-align:center">
                                        <asp:Button ID="btnpdf" runat="server" Width="250px" CssClass="btn btn-primary" OnClick="btnpdf_Click" Text="Click to Generate PDF" ValidationGroup="po" />
                                    </td>
                                </tr>

                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divUploadMaterial" class="modal hide fade" style="display: none; width: 500px;">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">×</button>
                <h3>Select Material Item</h3>
            </div>
            <div class="modal-body" style="width: 450px;">
                <table id="tblUploadMaterial" class='table table-striped table-bordered bootstrap-datatable datatable'>
                    <tbody id="tbody1">
                    </tbody>
                </table>
            </div>
            <div id="holder" style="-moz-column-count: 8; -moz-column-gap: 5px; -webkit-column-count: 8; -webkit-column-gap: 5px; column-count: 8; column-gap: 5px">
                <ul id="place">
                </ul>
            </div>
            <div class="modal-footer">
                <input type="button" value="Load" id="btnLoad" class="btn btn-primary" />
                <%--    <asp:Button ID="btnUploadMaterial" Text="Load" runat="server" CssClass="btn btn-primary" />--%>
                <input id="btncloase" value="Close" style="width: 40px" class="btn btn-primary" data-dismiss="modal" />
            </div>
        </div>

    </div>
    <div id="pnlHtml" runat="server"></div>
</asp:Content>

