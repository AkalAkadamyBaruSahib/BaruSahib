<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" Async="true" AutoEventWireup="true" CodeFile="RateUpload.aspx.cs" Inherits="RateUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ClientSideClick(myButton) {

            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }
            if (myButton.getAttribute('type') == 'button') {
                myButton.disabled = true;
                myButton.className = "btn btn-success";
                myButton.value = "Please Wait...";
            }
            return true;
        }
    </script>
    <script src="JavaScripts/RateUpload.js"></script>
    <asp:HiddenField ID="hdnInchargeID" runat="server" />
    <asp:HiddenField ID="hdnUserName" runat="server" />
    <asp:HiddenField ID="hdnVandorID" runat="server" />
    <asp:HiddenField ID="hdnMaterialID" runat="server" />
   <asp:HiddenField ID="hdnEstID" runat="server" />
    <div id="content" class="span10">

        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Upload Material Rate</h2>
                    <div class="box-icon">
                        <%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <table id="tblRateUploadDetail" style="width: 100%;" class='table table-striped table-bordered'>
                        <thead>
                            <tr>
                                <th style="color: #cc3300;">Sr No</th>
                                <th style="color: #cc3300; width: 200px;">Vendor</th>
                                <th style="color: #cc3300; width: 200px;">Material</th>
                                <th style="color: #cc3300;">Material Type</th>
                                <th style="color: #cc3300;">Unit</th>
                                <th style="color: #cc3300;">Current Rate</th>
                                <th style="color: #cc3300;">New Rate</th>
                                <th style="color: #cc3300;">Action</th>
                            </tr>
                        </thead>
                        <tbody id="tbody">
                            <tr id="tr0">
                                <td><span id="spn0">1</span></td>
                                <td>
                                    <input id="txtVendorName0" name="txtVendorName0" onblur="VendorTextBox_ChangeEvent(0);" style="position: absolute; width: 200px;" type="text" class="span6 typeahead" required />
                                    <br />
                                    <br />
                                    <div id="menu" style="position: absolute; width: 500px;"></div>
                                </td>
                                <td>
                                    <input id="txtMaterialName0" name="txtMaterialName1" style="position: absolute; width: 200px;" onblur="MaterialTextBox_ChangeEvent(0);" type="text" class="span6 typeahead" required />
                                    <br />
                                    <br />
                                    <div id="menu-container0" style="position: absolute; width: 500px;"></div>
                                </td>
                                <td>
                                    <label id="lblMaterialType0"></label>
                                </td>
                                <td>
                                    <label id="lblUnit0"></label>
                                </td>
                                <td>
                                    <table id="myNestedTableOne">
                                        <tbody id="first">
                                            <tr id="trlblMRP0">
                                                <td>MRP/Dealer Price:
                                                    <label id="lblMRP0"></label>
                                                </td>
                                            </tr>
                                            <tr id="trlblDiscount0">
                                                <td>Discount:
                                                    <label id="lblDiscount0"></label>
                                                </td>
                                            </tr>
                                            <tr id="trlblAdditionalDiscount0">
                                                <td>Additional Discount:
                                                    <label id="lblAdditionalDiscount0"></label>
                                                </td>
                                            </tr>
                                            <tr id="trlblVat0">
                                                <td>GST:
                                                    <label id="lblVat0"></label>
                                                </td>
                                            </tr>
                                            <tr id="trlblRate0">
                                                <td>Net Rate:
                                                    <label id="lblRate0"></label>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td>
                                    <table id="myNestedTableTwo">
                                        <tbody id="second">
                                            <tr id="trMRP0">
                                                <td>MRP/Dealer Price:
                                                    <input id="txtMRP0" type="text" style="width: 50px;" onblur="MRPTextBox_ChangeEvent(0);" required /></td>
                                            </tr>
                                            <tr id="trDiscount0">
                                                <td>Discount:
                                                    <input id="txtDiscount0" type="text" style="width: 50px;" onblur="DiscountTextBox_ChangeEvent(0);" required /></td>
                                            </tr>
                                            <tr id="trAdditionalDiscount0">
                                                <td>Additional Discount:
                                                    <input id="txtAdditionalDiscount0" type="text" style="width: 50px;" onblur="AdditionalDiscountTextBox_ChangeEvent(0);" required /></td>
                                            </tr>
                                            <tr id="trVat0">
                                                <td>GST:
                                                    <select id="drpGst0" required style="width: 120px;" onchange="ddlGST_ChangeEvent(0);">
                                                        <option value="-1">-Select GST-</option>
                                                        <option value="0">0%</option>
                                                        <option value="5">5%</option>
                                                        <option value="12">12%</option>
                                                        <option value="18">18%</option>
                                                        <option value="28">28%</option>
                                                    </select>
                                              </td>
                                            </tr>
                                        </tbody>
                                        <tr id="trRate0">
                                            <td>Net Rate:
                                                <label id="lblNetRate0"></label>
                                            </td>
                                    </table>
                                </td>
                                <td>
                                    <a href="javascript:void(0);" id="aAddNewRow0" onclick="AddMaterialRow();"><b>Add Row</b></a>&nbsp;&nbsp;&nbsp;
                                    <a href="javascript:void(0);" id="aDeleteRow0" onclick="removeRow(0);"><b>Delete</b></a>
                                    <input type="hidden" id="hdnMatID0" /><input type="hidden" id="hdnMatTypeID0" /><input type="hidden" id="hdnUnitID0" />
                                    <input type="hidden" id="hdnMatCost0" /><input type="hidden" id="hdnLocalCost0" /><input type="hidden" id="hdnAkalWorkshopCost0" />
                                    <input type="hidden" id="hdnVendorID0" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="form-actions" style="text-align: center">
                    <input type="button" id="btnSendforApproval" value="Send Rate for Approval" title="Send for Approval" class="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
     
</asp:Content>

