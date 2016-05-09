<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="PurchaseOrder.aspx.cs" Inherits="PurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/PurchaseOrder.js"></script>
    <style>
        .headingTable
        {
            float: left;
            margin-left: -47px;
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
          width: 35px; text-align: center; vertical-align: middle;
        }
        .HeaderSpan
        {
            font-weight: bold; padding-right: 317px;
        }
    </style>

    <div id="content" class="span10">
        <asp:HiddenField ID="hdnEstimateID" runat="server" />
        <asp:HiddenField ID="hdnSelectedItems" runat="server" />
      
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>

                    <h2><i class="icon-user"></i>
                        PO </h2>
                    <div class="box-icon">

                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                    <asp:Button ID="btnpdf" runat="server" OnClick="btnpdf_Click" Text="Generate PDF" />
                </div>
                <div class="box-content">
                    <fieldset>
                        <legend></legend>
                        <div class="box-content">
                            <table style="width:980px" align="right">
                                <tr>
                                    <td style="height: 21px">
                                        <div align="left">

                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 1000px;"><span>
                                                        <img src="img/Logo_Small.png" style="float: left; margin-left: -53px;" />
                                                    </span></td>
                                                    <td valign="top">
                                                        <span style="font-size: 27px; font-weight: bold; margin-left: -148px;">PURCHASE ORDER</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 2451px;">
                                                        <span style="font-size: 25px; font-weight: bolder; margin-left: 14px;" lang="EN-US" xml:lang="EN-US">THE KALGIDHAR TRUST </span>
                                                        <br />
                                                        <br />
                                                        <span style="font-size: 15px; margin-left: -151px;"><i style="margin-left: 214px;">Service to Humanity</i></span><br />
                                                        <br />
                                                    </td>
                                                    <td></td>

                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="headingTable">The Kalgidhar Trust,Baru Sahib,Via Rajgarh<br />
                                            Distt.Sirmour,Himachal Pradesh-173101<br />
                                            Phone 91-1799-276031 Fax 91-1799-276041
                                        </span>
                                    </td>
                                    <td>
                                        <span class="HeaderSpan">P.O.</span>
                                        <input type="text" id="txtPO" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <span class="HeaderSpan">Date:</span>
                                        <label id="lblCurrentDate" style="font-weight: bold; margin-top: -18px; margin-left: 37px;"></label>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                            </table>
                            <table id="tblPO" width="100%">
                                <tbody>
                                    <tr>

                                        <td style="float: left; width: 174px;">

                                            <label class="control-label" for="typeahead" style="color: #cc3300; font-weight: bold;">Estimate</label>
                                            <div class="controls">
                                                <select id="drpEstimate" style="width: 136px;">
                                                    <option value="0">-Select Estimate--</option>
                                                </select>
                                            </div>
                                        </td>
                                        <td style="float: left;">
                                            <div class="controls">
                                                <input type="button" value="Test" id="btntest" style="padding-left: 14px; padding-right: 11px; margin-left: -16px; padding-bottom: 7px; margin-top: 19px;" class="btn btn-primary" />
                                            </div>
                                        </td>
                                        <td style="float: left; width: 230px; margin-left: 69px; margin-top: 16px;">
                                            <label class="control-label" for="typeahead">Is Excise Duty Included</label>
                                            <div class="controls">
                                                <input type="checkbox" id="chkExcise" style="width: 10px; height: 10px;" />
                                            </div>

                                        </td>
                                        <td></td>
                                    </tr>
                                </tbody>

                            </table>
                            <table class='table table-striped table-bordered bootstrap-datatable datatable'>
                                <tr>
                                    <td>

                                        <label class="control-label" for="typeahead" style="color: #cc3300; font-weight: bold;"><b>Vendor:</b></label>
                                        <div class="controls">
                                            <select id="drpVendor" style="width: 134px;">
                                                <option value="0">--Select Vendor--</option>
                                            </select>
                                        </div>


                                    </td>
                                    <td style="width: 382px;">
                                        <div style="height: 65px;">
                                            M/S<label id="lblName" style="margin-top: -18px; margin-left: 31px;"></label>
                                            <label id="lblVendorAddress"></label>
                                            <label id="lblCity"></label>
                                        </div>
                                    </td>
                                    <td style="width: 177px; text-align: center; vertical-align: middle;">
                                        <div style="height: 50px; width: 7px;"><span style="color: #cc3300; font-weight: bold;">SHIP TO</span></div>
                                    </td>
                                    <td style="width: 480px;">
                                        <div style="height: 50px;">
                                            <label id="lblTrustName"></label>
                                            <label id="lblAdress"></label>
                                            <label id="lblPhoneNo"></label>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>

                </div>
            </div>
        </div>

        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-user"></i>Purchase Order Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <%--      <div id="divMatDetails" runat="server">--%>
                    <div id="divPurchaseOrderDetails" runat="server">
                        <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                            <thead>
                                <tr class="heading">
                                    <th colspan="3" style="text-align: center;" >SHIPPING METHOD</th>
                                    <th style="width: 250px; text-align: center;">SHIPPING TERMS</th>
                                    <th style="width: 35px; text-align: center;"  colspan="2">DELIVERY DATE</th>
                                </tr>
                                <tr class="tableSecondTrHeading">
                                    <th colspan="3" style="text-align:center;">By Road</th>
                                    <th style="text-align: center;">- </th>
                                    <th colspan="2"  style="text-align: center;">Within 1-2 Days</th>
                                </tr>
                                <tr class="heading">
                                    <th style="text-align: center;">Sr No</th>
                                    <th style="text-align: center; width:115px;">Qty</th>
                                    <th style="text-align: center;">Description</th>
                                    <th style="text-align: center;">Detail</th>
                                    <th style="text-align: center; width:119px;">Unit Price</th>
                                    <th id="linetotal"  style="text-align: center;">Line Total</th>
                                </tr>
                            </thead>
                            <tbody id="tbody">
                            </tbody>
                            <tfoot>

                                <tr>
                                    <td rowspan="4" style="width: 65px;" colspan="4">
                                        <label id="lblAddress" style="color: #cc3300"><u><b>BILLING ADDRESS:-</b></u></label>
                                        <div class="controls">
                                            <select id="drpDeliveryAddress" class="AddressDrp">
                                                <option value="0">-Select Billing Address--</option>
                                            </select>
                                        </div>
                                        <br />
                                        <label id="lblDeliveryAddress" style="color: #cc3300"><u><b>DELIVERY ADDRESS:-</b></u></label>
                                        <div class="controls">
                                            <select id="drpBillingAddress" class="AddressDrp">
                                                <option value="0">-Select Delivery Address--</option>
                                            </select>
                                        </div>
                                        <div>
                                            <label id="lblBillingName"></label>
                                            <label id="lblBillingAddres"></label>
                                            <label id="lblBillingPhone"></label>
                                        </div>
                                        <label id="lblContact" style="color: #cc3300"><u><b>Contact Person:-</b></u></label>
                                        <input type="text" id="txtcontact" style="margin-top: -36px; margin-left: 128px; width: 117px;" />
                                    </td>
                                    <th style="color: #cc3300;">Sub Total :</th>
                                    <td>
                                        <label id="subtotal"></label>
                                    </td>
                                </tr>
                                <tr id="rowExcise">
                                    <th style="color: #cc3300;">Excise:</th>
                                    <td>
                                        <input type="text" id="txtExcise" style="width: 80px;" />

                                    </td>
                                </tr>
                                <tr>
                                    <th style="color: #cc3300;">VAT/CST :</th>
                                    <td>
                                        <input type="text" id="txtvat" style="width: 80px;" />
                                        <input type="button" style="margin-top: -11px;" id="btnExcise" value="Total" class="btn btn-primary" /></td>
                                </tr>
                                <tr>
                                    <th style="color: #cc3300;">Grand Total:</th>
                                    <td>
                                        <label id="lblGrandTotal"></label>
                                    </td>
                                </tr>
                                <table>
                                    <tr>
                                        <td style="text-align: center; vertical-align: middle;">
                                            <strong><span style="font-size: 12pt; font-weight: bold; font-family: Arial; color: #cc3300;"><u>Please read the instruction carefully</u></span></strong>
                                        </td>
                                    </tr>
                                    <caption>
                                        <br />
                                        <tr>
                                            <td rowspan="2" style="width: 71px; vertical-align: top;"><span class="style13" style="width: 72px; height: 18px;"></span><span style="display: inline-block; height: 13px; width: 816px;">1. Please ensure to sendthe Original Bill to THE KALGIDHAR TRUST C-120, INDUSTRIAL AREA PHASE-8, MOHALI-160071 as per instructions.</span><br /> <span class="instruction">2. Material should be sent at the destination with Original Bill Only</span><br /> <span class="instruction">3.Please send two copies of invoice with material</span><br /> <span class="instruction">4. Please treat this order in accordance with prices,terms,delivery method and specification listed above.</span><br /> <span style="display: inline-block; height: 13px; width: 816px;">5. Please notify us immediately if you are unable to supply the material beyond your control. A penalty @1% per week will be charged for delayed supply of material at destination.</span><br /> <span class="instruction">6. Send all correspondence to : MOHALI 
                                                OFFICE. E-MAIL mohali@barusahib.org
                                                <br />
                                                Office Contact No:0172-5094200</span><br /> <span class="instruction">7.Delivery must be completed by .......</span><br /> <span class="instruction"><u>8.please Acknowledge the Receipt of Purchase Order in Person or by Mail:</u></span>
                                                <ul>
                                                    <li><span class="footerheading" style="color: #cc3300; font-weight: bold">Vat/CST:-</span><label id="lblVatStatus" style="margin-top: -17px; margin-left: 81px;"></label></li>
                                                    <li><span class="footerheading">Excise Duty:</span><b><label id="lblExciseStatus" style="margin-top: -17px; margin-left: 81px;"></label></b></li>
                                                    <li><span class="footerheading">Freight:-</span><select id="drpFreight" style="width: 120px; height: 24px; margin-left: 33px;">
                                                        <option value="0">--Select One--</option>
                                                        <option value="1">Free On Road</option>
                                                        <option value="2">Extra</option>
                                                        </select></li>
                                                    <li><span class="footerheading">Payment:-</span><b><input type="text" id="txtpayment" style="width: 111px; height: 15px; margin-left: 22px;" /></b><br /> <span class="footerheading">Against Indent No</span><b><label id="lblIndentNo" style="margin-top: -19px; margin-left: 118px;"></label></b></li>
                                                    <li><span class="footerheading">Mode Of Dispatch:-</span> By Road</li>
                                                    <li><span class="footerheading">Out ST Reg no:-</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Not Required</li>
                                                </ul>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                        <caption>
                                            <br />
                                            <br />
                                            <tr>
                                                <td style="font-weight: bolder; font-size: 16px; width: 300px; color: black; font-family: Arial">
                                                    <label id="lblAuthorised" style="color: #cc3300">
                                                    <u><b>Authorised By:-</b></u></label>
                                                    <input type="text" id="txtAuthorised" style="width: 111px; margin-top: -36px; margin-left: 105px;" />
                                                </td>
                                            </tr>
                                        </caption>
                                    </caption>
                                </table>
                            </tfoot>
                        </table>
                       <%-- <div id="editor"></div>
                        <div>
                           
                             <input type="button" style="margin-top: -11px;" id="btnPDF" value="Generate PDF File" class="btn btn-primary" />
                        </div>--%>
                    </div>
                    <%--  </div>--%>
                </div>
            </div>
            <!--/span-->

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

</asp:Content>

