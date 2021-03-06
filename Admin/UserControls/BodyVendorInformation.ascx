﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyVendorInformation.ascx.cs" Inherits="Admin_UserControls_BodyVendorInformation" %>

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
    <asp:HiddenField ID="hdnMaterialitems" runat="server" />
    <asp:HiddenField ID="hdnVendorID" runat="server" />
    <asp:HiddenField ID="hdnmaterialid" runat="server" />
    <asp:HiddenField ID="hdnInchargeID" runat="server" />
    <asp:HiddenField ID="hdnVendormaterialID" runat="server" />
    <asp:HiddenField ID="hdnUserType" runat="server" />
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i>Create Vendor</h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <form class="form-horizontal">
                    <fieldset>
                        <legend></legend>
                        <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="vendor" />
                        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                        <table>
                            <tr>
                                <td width="25%">
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"><b>Vendor Name:</b></label>
                                        <div class="controls">
                                           <input type="text" id="txtAgencyName" style="display: none;" />
                                            <asp:TextBox ID="txtVendorName" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="vendor" Display="Dynamic" ControlToValidate="txtVendorName" ErrorMessage="Special Character are Invalid!!!" ForeColor="Red" ValidationExpression="^[a-zA-Z ]+$"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vendor" ID="reqName" ForeColor="Red" ControlToValidate="txtVendorName" ErrorMessage="Please enter the  Vendor Name" />
                                        </div>
                                    </div>
                                </td>
                                <td width="25%">
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"><b>Contact No:</b></label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="regxnumbervalidator" runat="server" ControlToValidate="txtPhone" ForeColor="Red" Font-Size="13px" ErrorMessage="Invalid Contact No!!!" ValidationExpression="^[0-9,]*$"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vendor" ID="reqPhone" ForeColor="Red" ControlToValidate="txtPhone" ErrorMessage="Please enter the Contact No" />
                                        </div>
                                    </div>
                                </td>
                                <td width="25%">
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"><b>Alternate No:</b></label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtAltrenatePhone" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtAltrenatePhone" ForeColor="Red" Font-Size="13px" ErrorMessage="Invalid Contact No!!!" ValidationExpression="^[0-9,]*$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </td>
                                <td width="25%">
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"><b>Address:</b></label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="span6 typeahead"  Width="300px"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vendor" ID="reqAddress" ForeColor="Red" ControlToValidate="txtAddress" ErrorMessage="Please enter the Address" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ValidationGroup="visitor" Display="Dynamic" ControlToValidate="txtAddress" ErrorMessage="Special Character are Invalid!!!" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9 #,&()-.:/\n]*$"></asp:RegularExpressionValidator>

                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>

                                <td width="25%">
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"><b>Pin Code</b></label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtZip" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtZip" ForeColor="Red" Font-Size="13px" ErrorMessage="Invalid Zip Code!!! Enter the 6 digit only" ValidationExpression="[0-9]{6}"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vendor" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtZip" ErrorMessage="Please enter the Pin Code" />
                                        </div>
                                    </div>
                                </td>

                                <td width="25%">
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"><b>State</b></label>
                                        <div class="controls">
                                            <asp:DropDownList ID="drpState" runat="server" CssClass="span6 typeahead" Width="200px">
                                                <asp:ListItem Value="0">--Select State--</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator Display="None" InitialValue="0" runat="server" ValidationGroup="vendor" ID="reqState" ForeColor="Red"
                                                ControlToValidate="drpState" ErrorMessage="Please select the  State" />
                                        </div>
                                    </div>
                                </td>
                                <td width="25%">
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"><b>City</b></label>
                                        <div class="controls">
                                            <asp:DropDownList ID="drpCity" runat="server" CssClass="span6 typeahead" Width="200px">
                                                <asp:ListItem Value="0">--Select City--</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator Display="None" InitialValue="0" runat="server" ValidationGroup="vendor" ID="reqCity" ForeColor="Red"
                                                ControlToValidate="drpCity" ErrorMessage="Please select The City" />
                                        </div>
                                    </div>
                                </td>
                                <td width="25%">
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"><b>Bank Name</b></label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vendor" ID="RequiredFieldValidator3" ForeColor="Red"
                                                ControlToValidate="txtBankName" ErrorMessage="Please enter the  Bank Name" />--%>
                                        </div>
                                    </div>
                                </td>


                            </tr>
                            <tr>
                                <td width="25%">
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"><b>Account NUmber</b></label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vendor" ID="RequiredFieldValidator5" ForeColor="Red"
                                                ControlToValidate="txtAccountNumber" ErrorMessage="Please enter the Account Number" />--%>
                                        </div>
                                    </div>
                                </td>
                                <td width="25%">
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"><b>IFSC Code</b></label>
                                        <div class="controls">

                                            <asp:TextBox ID="txtIfscCode" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vendor" ID="RequiredFieldValidator4" ForeColor="Red"
                                                ControlToValidate="txtIfscCode" ErrorMessage="Please enter the IFSC Code" />--%>
                                        </div>
                                    </div>
                                </td>
                                <td width="25%">
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"><b>PAN Number</b></label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtPanNumber" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                                <td width="25%">
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"><b>TIN Number</b></label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtTinNumber" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <%--<tr style="display: none;">
                                <td>
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"></label>
                                        <div class="controls">
                                            <b>Select  Material Items:</b>
                                            <br />
                                            <input id="txtMaterial" style="width: 340px; height: 23px;" name="txtMaterial" />

                                            <input type="button" id="btnadd" value="Add" class="btn btn-primary" />
                                            <label id="lblUserId" />
                                        </div>
                                    </div>
                                </td>
                                <td colspan="2">
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"></label>
                                        <div class="controls">
                                            <b>Select  Material Items:</b>
                                            <br />
                                            <select id="lstMaterials" multiple="multiple" style="width: 400px;"></select>
                                            <input type="button" id="btnRemove" value="Remove" style="margin-top: 36px;" class="btn btn-primary" />
                                        </div>
                                    </div>
                                </td>

                                <%--  </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </table>

                        <div class="form-actions">
                            <input type="button" id="btnSave" value="Save" class="btn btn-primary" />
                            <%-- <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vendor" OnClientClick=" return SaveVendor();" CssClass="btn btn-primary" />--%>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" ValidationGroup="vendor" CssClass="btn btn-primary" />
                        </div>
                    </fieldset>
                </form>

            </div>
        </div>
        <!--/span-->
    </div>

    <div class="row-fluid sortable" id="vendorMainbox" runat="server">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-user"></i>Vendor Details</h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <div id="divMatDetails">
                    <div id="divVendorDetails">
                        <table>
                            <tr>
                                <td style="float: right; margin-left: 830px; margin-top: -4px;">
                                    <input type="checkbox" id="chkAllVendors" style="width: 10px; height: 10px;" />
                                    <asp:Label ID="lblchkAllVendors" runat="server" Text="Show all Vendors"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                            <thead>
                                <tr>

                                    <th style="color: #cc3300; width: 215px;">Vendor Name</th>
                                    <th style="color: #cc3300; width: 298px;">Address</th>
                                    <th style="color: #cc3300; width: 216px;">Contact Number</th>
                                    <th style="color: #cc3300; width: 197px;">Status</th>
                                    <th style="color: #cc3300; width: 263px;">Action</th>
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

    <div class="modal hide fade" style="width: 900px; height: 350px;" id="divVendorMaterial">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">×</button>
            <h3>Agency Material Details</h3>
        </div>
        <div class="modal-body">
            <table id="grdMatDiscription" class='table table-striped table-bordered bootstrap-datatable datatable'>
                <thead>
                    <tr>
                        <th>Bill No</th>
                        <th>Agency Name</th>
                        <th>Work Allot Name</th>
                        <th>Mat Name</th>
                        <th>Total Amount</th>
                        <th>Created By</th>
                        <th>Created On</th>
                    </tr>
                </thead>
                <tbody id="agencymaterialtable">
                </tbody>
            </table>
        </div>
        <div class="modal-footer">
            <input id="btnclose" value="Close" style="width: 40px" class="btn btn-primary" data-dismiss="modal" />
        </div>
    </div>
    <div class="modal hide fade" style="width: 800px; height: 600px;" id="divAlreadyExitVendor">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">×</button>
            <h3>Already Exit Vendor Details</h3>
        </div>
        <div class="modal-body">
            <table id="tblVendorDetail" width="100%" class='table table-striped table-bordered bootstrap-datatable datatable'>
                <thead>
                    <tr>
                        <th style="color: #cc3300; width: 30%">Vendor Name</th>
                        <th style="color: #cc3300; width:30%">Vendor Contact No</th>
                        <th style="color: #cc3300; width:40%">Vendor Address</th>
                    </tr>
                </thead>
                <tbody id="tblAlreadySavedVendor">
                </tbody>
            </table>
        </div>
        <div class="modal-footer">
            <input id="btnSaveVendor" value="Save" style="width: 40px" class="btn btn-primary" data-dismiss="modal" />
            <input id="btnclos" value="Dont Save" style="width: 70px" class="btn btn-primary" data-dismiss="modal" />
        </div>
    </div>
</div>
