<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyStoreMaterialDetail.ascx.cs" Inherits="Admin_UserControls_BodyStoreMaterialDetail" %>

<div id="content" class="span10">
    <%--  <asp:HiddenField ID="hdnEMRId" runat="server" />--%>
    <asp:HiddenField ID="hdnIsReceived" runat="server" />
    <asp:HiddenField ID="hdnEstID" runat="server" />
    <asp:HiddenField ID="hdnBillNo" runat="server" />
    <asp:HiddenField ID="hdnInchargeID" runat="server" />
    <asp:HiddenField ID="hdnUloadBill" runat="server" />
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well">
                <h2><i class="icon-edit"></i>Estimate Material Seacrh</h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <div class="control-group">
                    <label class="control-label" for="typeahead">Enter Estimate ID</label>
                    <div class="controls">
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEstID" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Get Estimate Detail" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                        </table>


                    </div>
                </div>
            </div>
        </div>
        <!--/span-->

    </div>

    <div id="divMaterialDetails" runat="server">
    </div>
    <!--/span-->



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
        <input id="btnUploadSave" value="Save" class="btn btn-primary" style="width: 40px" />
        <input id="btncloase" value="Close" style="width: 40px" class="btn btn-primary" data-dismiss="modal" />
    </div>
</div>

<div id="divViewbill" class="modal hide fade" style="display: none; width: 500px;">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">×</button>
        <h3>Bill(s) for Estimate ID:- <span id="spnEstID"></span></h3>
    </div>
    <div class="modal-body" style="width: 500px;">
        <table id="grdBills" style="width:100%" class='table table-striped table-bordered bootstrap-datatable datatable'>
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
</div>
