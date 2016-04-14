<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="PurchaseOrder.aspx.cs" Inherits="PurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/Purchase.js"></script>
    <style>
        .headingTable
        {
            background-color: #cc3300;
            color: #ffffff;
            width: 350px;
            height: 0px;
            padding-left: 10px;
            font-size: 13px;
        }
    </style>

    <div id="content" class="span10">
        <asp:HiddenField ID="hdnEstimateID" runat="server" />
        <asp:HiddenField ID="hdnSelectedItems" runat="server" />
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>

                    <h2><i class="icon-user"></i>
                        Add New Driver </h2>
                    <div class="box-icon">

                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <fieldset>
                        <legend></legend>
                        <div class="box-content">
                            <table id="tblPO" width="100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            <label class="control-label" for="typeahead">Vendor</label>
                                            <div class="controls">
                                                <select id="drpVendor">
                                                    <option value="0">Select Vendor</option>
                                                </select>
                                            </div>

                                        </td>
                                        <td style="float: left; width: 174px;">

                                            <label class="control-label" for="typeahead">Estimate</label>
                                            <div class="controls">
                                                <select id="drpEstimate">
                                                    <option value="0">Select Estimate</option>
                                                </select>
                                            </div>
                                        </td>
                                        <td style="float: left;">
                                            <div class="controls">
                                                <input type="button" value="Test" id="btntest" style="padding-left: 21px; padding-right: 18px;  margin-left: 69px;  padding-bottom: 7px; margin-top: 20px;" class="btn btn-primary" />
                                            </div>
                                        </td>
                                        <td style="float: right; width: 230px;">
                                            <label class="control-label" for="typeahead">Is Excise Duty Included</label>
                                            <div class="controls">
                                                <input type="checkbox" id="chkExcise" style="width: 10px; height: 10px;" />
                                            </div>

                                        </td>
                                        <td></td>
                                    </tr>
                                </tbody>
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
                    <div id="divMatDetails" runat="server">
                        <div id="divPurchaseOrderDetails" runat="server">
                            <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                                <thead>
                                    <tr>
                                        <th style="color: #cc3300;">Sr No</th>
                                        <th style="color: #cc3300;">Qty</th>
                                        <th style="color: #cc3300;">Description</th>
                                        <th style="color: #cc3300;">Detail</th>
                                        <th style="color: #cc3300;">Unit Price</th>
                                        <th style="color: #cc3300;">Line Total</th>
                                    </tr>
                                </thead>
                                <tbody id="tbody">
                                </tbody>
                            </table>

                        </div>
                    </div>
                </div>
            </div>
            <!--/span-->

        </div>

        <div id="divUploadMaterial" class="modal hide fade" style="display: none; width: 500px;">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">×</button>
                <h3>Select Material Item</h3>
            </div>
            <div class="modal-body">
                <table id="tblUploadMaterial" style="width: 500px;">
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

