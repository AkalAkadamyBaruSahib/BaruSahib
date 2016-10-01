<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyNewEstimateFunctionality.ascx.cs" Inherits="BodyNewEstimateFunctionality" %>

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

    function validateFileSize() {
        var uploadControl = $("input[id*='fileUploadSignedCopy']")[0].files[0].size;
        if (uploadControl > 1048576) {
            $('#dvMsg').show();
            return false;
        }
        else {
            $('#dvMsg').hide();
            return true;
        }
    }

</script>
<div id="content" class="span10">
    <asp:HiddenField ID="hdnEstimateID" runat="server" />
    <asp:HiddenField ID="hdnSelectedItems" runat="server" />
    <asp:HiddenField ID="hdnItemsLength" runat="server" />
    <asp:HiddenField ID="hdnIsAdmin" runat="server" />
    <asp:HiddenField ID="hdnInchargeID" runat="server" />
    <asp:HiddenField ID="hdnModule" runat="server" />
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i>Create Estimate</h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <fieldset>
                <legend></legend>
                <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="visitor" />
                <div class="box-content">
                    <table width="100%">
                        <tr>
                            <td width="50%" colspan="2">
                                <div class="control-group">
                                    <h2>
                                        <label id="lblWorkNameReflect"></label>
                                </div>
                            </td>

                        </tr>
                        <tr id="trEstimateNo" runat="server" visible="false">
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Estimate No.
								                <asp:Label ID="lblEstimateNo" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </td>
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr id="trZone" runat="server">
                            <td width="50%">
                                <div class="control-group">
                                    <label id="lblzone" style="display: none;" class="control-label" for="typeahead">Zone</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlZone" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" InitialValue="0" Display="None" ValidationGroup="visitor" ID="ddlZone_RequiredFieldValidator"
                                            ControlToValidate="ddlZone" ErrorMessage="Please Select Any Zone" ForeColor="#ff0000"></asp:RequiredFieldValidator><br />
                                    </div>
                                </div>
                            </td>
                            <td width="50%">
                                <div class="control-group">
                                    <label id="lblAcademy" style="display: none;" class="control-label" for="typeahead">Academy</label>
                                    <label id="lblSourceType" style="display: none;" class="control-label" for="typeahead">Select Workshop</label>
                                    <div class="controls">
                                        <asp:DropDownList ID="ddlAcademy" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" Display="None" InitialValue="0" ForeColor="#ff0000" ID="ddlAcademy_RequiredFieldValidator"
                                            ControlToValidate="ddlAcademy" ErrorMessage="Please Select Any Academy" /><br />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" id="tdWorkAllot">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Select Work Allot
                                        <br />
                                        <asp:DropDownList ID="ddlWorkAllot" runat="server" Width="500px">
                                            <asp:ListItem Value="0">--Select Academy--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" Display="None" InitialValue="0" ID="ddlWorkAllot_RequiredFieldValidator"
                                            ControlToValidate="ddlWorkAllot" ForeColor="#ff0000" ErrorMessage="Please Select Any Work" />
                                        <br />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="50%" colspan="2">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Sub Estimate
								                <asp:TextBox ID="txtSubEstimate" runat="server" CssClass="span6 typeahead" Width="910px"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" Display="None" ID="txtSubEstimate_RequiredFieldValidator"
                                            ControlToValidate="txtSubEstimate" ForeColor="#ff0000" ErrorMessage="Please Enter The Sub Estimate" />
                                    </div>
                                </div>
                            </td>
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Type Of work
                                                    <br />
                                        <asp:DropDownList ID="ddlTypeOfWork" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" Display="None" ID="ddlTypeOfWork_RequiredFieldValidator"
                                            ControlToValidate="ddlTypeOfWork" ForeColor="#ff0000" ErrorMessage="Please Select Any Type Of Work" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>

                    <table width="100%">
                        <tr id="trCost" runat="server" visible="false">
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Estimate Cost
								                <asp:Label ID="lblEstimateCost" runat="server" ForeColor="Red" Text="00.00"></asp:Label>
                                    </div>
                                </div>
                            </td>
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Signed Copy Name
                                                    <br />
                                        <asp:TextBox runat="server" ID="txtFileName"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" Display="None" ID="txtFileName_RequiredFieldValidator"
                                            ControlToValidate="txtFileName" ForeColor="#ff0000" ErrorMessage="Please Enter The File Name" />
                                        <div id="dvMsg" style="color: Red; width: 190px; float: right; margin-right: -395px; padding: 3px; display: none;">
                                            Maximum size allowed Less than is 1 MB                                   
                                        </div>
                                    </div>
                            </td>

                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Upload File
                                        <br />

                                        <asp:FileUpload ID="fileUploadSignedCopy" runat="server" AllowMultiple="true" onchange="validateFileSize();" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorphoto" ControlToValidate="fileUploadSignedCopy" runat="server" ValidationGroup="visitor" Display="None" ErrorMessage="Please Upload the File"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </td>
                        </tr>

                    </table>
                </div>
                <div class="box-content">
                    <table id="tblEstimateMatDetail" style="width: 100%;" class='table table-striped table-bordered'>
                        <thead>
                            <tr>
                                <th style="color: #cc3300; width: 30px;">Sr No</th>
                                <th style="color: #cc3300; width: 140px;">Source Type</th>
                                <th style="color: #cc3300; width: 200px;">MaterialName</th>
                                <th style="color: #cc3300; width: 180px;">Material Type</th>
                                <th style="color: #cc3300; width: 120px;">Quantity</th>
                                <th style="color: #cc3300; width: 50px;"">Unit</th>
                                <th style="color: #cc3300; width: 100px">Rate</th>
                                <th style="color: #cc3300; width: 200px">Remarks</th>
                                <th style="color: #cc3300; width: 75px;">Action</th>
                            </tr>
                        </thead>
                        <tbody id="tbody">
                            <tr id="tr0">
                                <td>
                                    <span id="spn0">1</span>
                                </td>
                                 <td>
                                    <select id="ddlSourceType0" onchange="SourceType_ChangeEvent(0);"  style="width: 150px;">
                                        <option value="0">Select Source Type</option>
                                    </select>
                                </td>
                                
                                <td>
                                    <input id="txtMaterialName0" name="txtMaterialName1" onblur="MaterialTextBox_ChangeEvent(0);" type="text" class="span6 typeahead" style="width: 210px;" />

                                </td>
                               <td>
                                     <span id="spnMaterialTypeID0"></span>
                                </td>
                                <td>
                                    <input id="txtQty0" type="text" style="width: 80px;" />
                                </td>
                                <td>
                                    <label id="lblUnit0"></label>
                                </td>
                                <td>
                                    <input id="txtRate0" type="text" style="width: 80px;" />
                                </td>
                                <td>
                                    <input id="txtRemarks0"  type="text" class="span6 typeahead" style="width: 200px;" />
                                </td>
                                <td>
                                    <a href="javascript:void(0);" id="aAddNewRow0" onclick="AddMaterialRow();"><b>Add Row</b></a>
                                    <a href="javascript:void(0);" id="aDeleteRow0" onclick="removeRow(0);"><b>Delete</b></a>
                                    <input type="hidden" id="hdnMatID0" /><input type="hidden" id="hdnMatTypeID0" /><input type="hidden" id="hdnUnitID0" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <input type="button" id="btnEstimateCost" value="EstimateCost" title="EstimateCost" style="float: left; margin-left: 570px;" class="btn btn-success" />
                            </td>
                            <td>
                                <asp:Label ID="lblAmt" ForeColor="Red" Font-Bold="true" Text="0.00" Style="float: right; margin-right: 450px;" runat="server"></asp:Label>
                            </td>
                        </tr>

                    </table>




                </div>
                <div class="form-actions" style="text-align: center">
                    <input type="button" id="btnSubEstimate" value="Submit Estimate" title="Submit Estimate" class="btn btn-success" />
                </div>
            </fieldset>
        </div>
    </div>
</div>
<div style="display: none" id="progress">
    <table style="text-align: center">
        <tr>
            <td style="text-align: center">
                <img src="img/animated.gif" />
            </td>
        </tr>
        <tr>
            <td>Wait while estimate is uploading....
            </td>
        </tr>
    </table>
</div>
