<%@ Page Title="" Language="C#" MasterPageFile="~/StoreMaster.master" AutoEventWireup="true" CodeFile="Store_Materials.aspx.cs" Inherits="Store_Materials" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/Store.js"></script>
    <script type="text/javascript">
        function ClientSideClick(myButton) {
            // Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }

            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                // diable the button
                myButton.disabled = true;
                myButton.className = "btn btn-primary";
                myButton.value = "Please Wait...";
            }
            return true;
        }
      
</script>
       <div id="content" class="span10">
        <asp:HiddenField ID="hdnEstID" runat="server" />
      

        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">
                    <h2><i class="icon-user"></i>Stock Register</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="divDesigDetails" runat="server">
                        <table border="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnExecl" runat="server" Text="Dispatch Excel Download" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" OnClick="btnExecl_Click" />

                                    </td>
                                    <td>Select Academy:
                                        <asp:DropDownList ID="ddlAcademy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div id="pnlPdf" runat="server"></div>
        <div id="divEstimateDetails" runat="server"></div>
    </div>

    <div id="divIsReceived" class="modal hide fade" style="display: none; width: 500px">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">×</button>
            <h3>Stock Register</h3>
        </div>
        <div class="modal-body">
            <asp:HiddenField ID="hdnEMRId" runat="server" />
            <asp:HiddenField ID="hdnIsReceived" runat="server" />
              <asp:HiddenField ID="hdnVendorID" runat="server" />
            <table>
                <tr id="trvendorname">
                    <td>Select The Vendor:
                        <select id="ddlVendorName" style="width:131px; float:right;">
                        <option value="0">--Select Vendor--</option>
                       </select>
                    </td>
                </tr>
                <tr>
                    <td>Enter Received Quantity:&nbsp&nbsp
                        <asp:TextBox ID="txtReceivedQty" Width="111px" float="right" padding-right="1px"  runat="server"></asp:TextBox>
                     </td>
                </tr>
                <tr id="trupload">
                    <td>Enter Purchased Bill No:&nbsp&nbsp&nbsp
                        <asp:TextBox ID="txtLinkBillNo" Width="113px" float="right" runat="server"></asp:TextBox>
                      </td>
                </tr>
            </table>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Received" CssClass="btn btn-primary" />
            <input id="Text1" value="Close" style="width: 100px" class="btn btn-primary" data-dismiss="modal" />
        </div>
    </div>

    <div id="divUploadBill" class="modal hide fade" style="display: none; width: 780px;">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">×</button>
            <h3>Stock Register: Upload Bill</h3>
        </div>
        <div class="modal-body">
            <table id="tblUploadBill" style="width: 730px;">
                <tbody>
                    <tr>
                        <td>
                            <label class="control-label" for="typeahead">BillNo:</label>
                            <div class="controls">
                                <input id="txtBillNo1" type="text" style="width: 150px; height: 18px;" class="span6 typeahead" />
                            </div>
                        </td>
                        <td>

                            <label class="control-label" for="typeahead">Bill Name:</label>
                            <div class="controls">
                                <input id="txtBillName1" type="text" style="width: 150px; height: 18px;" class="span6 typeahead" />
                            </div>
                        </td>
                        <td>

                            <label class="control-label" for="typeahead">Upload Purchase Bill:</label>
                            <div class="controls">
                                <input id="uploadePurchaseFile1" type="file" style="width: 150px; height: 18px;" class="span6 typeahead" />
                            </div>
                        </td>
                        <td>
                            <label class="control-label" for="typeahead"></label>
                            <div class="controls">
                                <a href="javascript:void(0);" id="aReference" onclick="addNewBill();">
                                    <input id="btnadd" class="btn btn-primary" value="+" style="width: 10px" /></a>
                            </div>
                        </td>
                        <td>
                            <label class="control-label" for="typeahead"></label>
                            <div class="controls">
                                <a href="javascript:void(0);" id="aDeleterow" onclick="removeRow();">
                                    <input id="btnremove" class="btn btn-primary" value="-" style="width: 10px" /></a>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnUploadSave" Text="Save" runat="server" CssClass="btn btn-primary" />
            <input id="btncloase" value="Close" style="width: 40px" class="btn btn-primary" data-dismiss="modal" />
        </div>
    </div>

    <div id="divViewbill" class="modal hide fade" style="display: none; width: 500px;">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">×</button>
            <h3>Bill(s) for Estimate ID:- <span id="spnEstID"></span></h3>
        </div>
        <div class="modal-body" style="width: 300px;">
            <table id="grdBills" class='table table-striped table-bordered bootstrap-datatable datatable'>
                <thead>
                    <tr>
                        <th>Bills</th>
                    </tr>
                </thead>
                <tbody id="tbody">
                </tbody>
            </table>
        </div>
        <div class="modal-footer">
            <input id="btnclose" value="Close" style="width: 40px" class="btn btn-primary" data-dismiss="modal" />
        </div>
    </div>
</asp:Content>

