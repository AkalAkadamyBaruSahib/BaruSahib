<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="Workshop_GenegerateBill.aspx.cs" Inherits="Workshop_GenegerateBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/WorkshopBill.js"></script>
    <style>
        .heading
        {
            color: #cc3300;
            background-color: #CCCCCC;
            vertical-align: middle;
            text-align: center;
        }
    </style>
    <div id="content" class="span10">
        <asp:HiddenField ID="hdnBillID" runat="server" />
        <asp:HiddenField ID="hdnEstimateID" runat="server" />
        <asp:HiddenField ID="hdnSelectedItems" runat="server" />
        <asp:HiddenField ID="hdnItemsLength" runat="server" />
        <asp:HiddenField ID="hdnUserId" runat="server" />
        <asp:HiddenField ID="hdnInchargeID" runat="server" />
        <div style="display:none;" class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>

                    <h2><i class="icon-user"></i>
                        Workshop Bill</h2>
                    <div class="box-icon">

                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <fieldset>
                        <legend></legend>
                        <div class="box-content">
                            <table style="width: 1000px;" align="right">
                                <tr>
                                    <td style="height: 21px">
                                        <table style="width: 1000px;">
                                            <tr style="display: none;">
                                                <td valign="top">
                                                    <span style="font-size: 25px; font-weight: bold; margin-left: 280px;">BILL/CHALLAN</span><br />
                                                    <br />
                                                    <span style="font-size: 30px; font-weight: bolder; margin-left: 176px;" lang="EN-US" xml:lang="EN-US">
                                                        <asp:Label ID="lblUser" runat="server"></asp:Label></span><br />
                                                    <br />
                                                    <span style="font-size: 15px; margin-left: 245px;">KALGIDHAR TRUST BARUSAHIB H.P.</span><br />
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                            </table>
                            <table id="tblbill" width="100%">
                                <tbody>
                                    <tr style="display: none;">
                                        <td style="float: left; width: 174px;">
                                            <span style="color: #cc3300; font-weight: bold;">Bill No:</span>
                                            <%-- <asp:TextBox ID="txtBillNo" runat="server" Width="125px" Style="margin-top: -29px; margin-left: 64px;"></asp:TextBox>--%>
                                        </td>
                                        <td style="float: right; width: 174px;">
                                            <span style="color: #cc3300; font-weight: bold; margin-left: -200px;">Date:</span>
                                            <asp:Label ID="lblCurrentDate" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnCurrentDate" runat="server" />

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table id="tblWorkshopBill" width="100%">
                                <tbody>

                                    <tr>
                                        <td style="float: left; display: none;">
                                            <span style="color: #cc3300; font-weight: bold;">To:</span>
                                            <asp:Label ID="lblAcademy" Style="margin-left: 42px;" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hdnAcademy" runat="server" />
                                        </td>
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
                    <h2><i class="icon-user"></i>Workshop Bill Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <table>
                        <tr>
                            <td style="float: left; width: 174px;">
                                <span style="color: #cc3300; font-weight: bold;">Estimate:</span>
                                <select id="drpEstimate" style="width: 136px; margin-top: -26px; margin-left: 64px;">
                                    <option value="0">-Select Estimate--</option>
                                </select>
                                <asp:HiddenField ID="hdnEstNo" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table id="grid" class='table table-striped table-bordered bootstrap-datatable'>
                        <thead>
                            <tr class="heading">
                                <th style="text-align: center;">SrNo</th>
                                <th style="text-align: center;">NameofItem</th>
                                <th style="text-align: center;">Qty</th>
                                <th style="text-align: center;">Pcs/Kg</th>
                                <th style="text-align: center;">Rate</th>
                                <th style="text-align: center;">Amount</th>
                            </tr>
                        </thead>
                        <tbody id="tbody">
                        </tbody>
                        <tfoot>
                            <tr>
                                <td rowspan="3" colspan="4"></td>
                                <td style="color: #cc3300;">Total:</td>
                                <td>
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnTotal" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="color: #cc3300;">Scrap 2%:</td>
                                <td>
                                    <asp:Label ID="lblScrap" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnScrap" runat="server" />
                                </td>

                            </tr>
                            <tr>
                                <td style="color: #cc3300;">Grand Total:</td>
                                <td>
                                    <asp:Label ID="lblGrandTotal" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnGrandTotal" runat="server" />
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                    <asp:Button ID="btnpdf" runat="server" OnClick="btnpdf_Click" Style="margin-left: 550px; float: left" Text="Generate PDF" CssClass="btn btn-primary" />
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

