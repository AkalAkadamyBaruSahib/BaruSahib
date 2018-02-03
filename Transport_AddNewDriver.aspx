<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_AddNewDriver.aspx.cs" Inherits="Transport_AddNewDriver" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
    <script src="JavaScripts/TransportValidation.js"></script>
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
    <asp:HiddenField ID="hdnvehicleEmployeeID" runat="server" />
    <asp:HiddenField ID="hdnvehicleIDExit" runat="server" />
    <asp:HiddenField ID="hdnEmployeeType" runat="server" />
    <div id="content" class="span10">
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
                        <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="driver" />
                        <div class="box-content">

                            <table id="tabledata" width="100%">
                                <tr>
                                    <td>
                                        <div class="control-group" id="divemptype">
                                            <label class="control-label" for="typeahead">Employee Type:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="drpEmployeeType" runat="server" Style="width: 200px; height: 25px;">
                                                    <asp:ListItem Text="" Value="0">--Select One--</asp:ListItem>
                                                    <asp:ListItem Text="Driver" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Conductor" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="driver" ID="RequiredFieldValidator_drpEmpLoyeeType"
                                                ControlToValidate="drpEmpLoyeeType" InitialValue="0" ErrorMessage="Please Select The  Employee Type" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group" id="div3">
                                            <label class="control-label" for="typeahead">Vehicle Number:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlVehicleNumber" runat="server" Style="width: 200px; height: 25px;">
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="driver" ID="RequiredFieldValidator2"
                                                ControlToValidate="ddlVehicleNumber" InitialValue="0" ErrorMessage="Please Select The Vehicle Number " ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group" id="div2">
                                            <label class="control-label" for="typeahead">Transport Type:</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="drpTransportType" runat="server" Style="width: 200px; height: 25px;">
                                                </asp:DropDownList>
                                            </div>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="driver" ID="RequiredFieldValidator1"
                                                ControlToValidate="drpTransportType" InitialValue="0" ErrorMessage="Please Select The  Transport Type" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </div>
                                    </td>



                                </tr>

                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Name:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="span6 typeahead" Style="width: 200px; height: 25px;"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="driver" ID="reqName"
                                                    ControlToValidate="txtName" ErrorMessage="Please Enter the Name" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Mobile No:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="span6 typeahead" Style="width: 200px; height: 25px;"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="regxnumbervalidator" runat="server" ControlToValidate="txtMobileNo" ForeColor="Red" Font-Size="13px" ErrorMessage="Please Enter only at least 10 Digit Numbers" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="driver" ID="RequiredFieldValidator_txtMobileNo"
                                                    ControlToValidate="txtMobileNo" ErrorMessage="Please Enter the Mobile No" />
                                            </div>
                                        </div>
                                    </td>

                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Date Of Birth:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="input-xlarge datepicker" Style="width: 190px; height: 18px;"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="driver" ID="RequiredFieldValidator_txtDateOfBirth" ControlToValidate="txtDateOfBirth" ErrorMessage="Please Enter The Date Of Birth" />
                                                <asp:RegularExpressionValidator ID="regPurchaseDate" runat="server" ErrorMessage="Invalid Format.Use(MM/DD/YYYY)." ForeColor="Red" ControlToValidate="txtDateOfBirth" SetFocusOnError="true" ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </td>

                                </tr>
                                <tr>


                                    <td>
                                        <div class="control-group" id="divfileUploadQualification" runat="server">
                                            <label class="control-label" for="typeahead">Qualification:</label>
                                            <div class="controls">
                                                <input type="file" id="fileUploadQualification" multiple="multiple" />
                                                <div id="aQualification"></div>
                                            </div>
                                        </div>
                                    </td>

                                    <td>
                                        <div class="control-group" id="div1" runat="server">
                                            <label class="control-label" for="typeahead">DL Scan Copy:</label>
                                            <div class="controls">
                                                <input type="file" id="fileUploadDlValidity" multiple="multiple" />
                                                <div id="afileUploadDlValidity"></div>


                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Date Of Joining:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtDateOfJoin" runat="server" CssClass="input-xlarge datepicker" Style="width: 190px; height: 18px;"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Format.Use(MM/DD/YYYY)." ForeColor="Red" ControlToValidate="txtDateOfJoin" SetFocusOnError="true" ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"></asp:RegularExpressionValidator>

                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">DL Number:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtDlNumber" runat="server" CssClass="span6 typeahead" Style="width: 200px; height: 25px;"></asp:TextBox>

                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">DL Type:</label>
                                            <div class="controls">
                                                <div class="controls">
                                                    <asp:DropDownList ID="drpDlType" runat="server" Style="width: 200px; height: 25px;">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                    </td>
                                    <td>
                                        <div class="control-group" id="divfileUploadDlValidity" runat="server">
                                            <label class="control-label" for="typeahead">DL Validity:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtdlvalidity" runat="server" CssClass="input-xlarge datepicker" Style="width: 190px; height: 18px;"></asp:TextBox><br />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Format.Use(MM/DD/YYYY)." ForeColor="Red" ControlToValidate="txtdlvalidity" SetFocusOnError="true" ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"></asp:RegularExpressionValidator>

                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Contact Number In Case Of Emergency:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtEmergencyContactNo" runat="server" CssClass="span6 typeahead" Style="width: 200px; height: 25px;"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_txtEmergencyContactNo" runat="server" ControlToValidate="txtEmergencyContactNo" ForeColor="Red" Font-Size="13px" ErrorMessage="Please Enter only at least 10 Digit Numbers" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_EmergencyContactNo" ControlToValidate="txtEmergencyContactNo" runat="server" ValidationGroup="driver" Display="None" ErrorMessage="Please Enter The Contact Number In Case Of Emergency"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group" id="divfileUploadApplicationForm" runat="server">
                                            <label class="control-label" for="typeahead">Application Form:</label>
                                            <div class="controls">
                                                <input type="file" id="fileUploadApplicationForm" multiple="multiple" />
                                                <div id="afileUploadApplicationForm"></div>

                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Address:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="span6 typeahead" Style="width: 200px;"></asp:TextBox>
                                                <%--               <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="driver" ID="RequiredFieldValidator_txtAddress" ControlToValidate="txtAddress" ErrorMessage="Please Enter the Address" />
                                                --%>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group" id="divfathername" runat="server" visible="false">
                                            <label class="control-label" for="typeahead">Father Name:</label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtFatherName" CssClass="span6 typeahead" runat="server" Style="width: 200px; height: 25px;"></asp:TextBox>

                                            </div>
                                        </div>
                                    </td>
                                </tr>



                            </table>

                            <fieldset id="fCompanyDetail">
                                <legend><span class="labelH labelH-info">Previous Company Detail</span></legend>
                                <asp:UpdatePanel ID="updatepanel2" runat="server">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td width="50%">
                                                    <div class="control-group">
                                                        <label class="control-label" for="typeahead">Experience:</label>
                                                        <div class="controls">
                                                            Year:
                                                         <asp:DropDownList ID="ddlyear" Width="65px" runat="server"></asp:DropDownList>
                                                            Month:
                                                       <asp:DropDownList ID="ddlmonth" Width="65px" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td style="float: left; width: 280px;">
                                                    <div class="control-group" id="dividphoto" runat="server">
                                                        <label class="control-label" for="typeahead">Name Of The Company:</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtNameOfTheComp" runat="server" CssClass="span6 typeahead" Style="width: 200px; height: 25px;"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </td>

                                            </tr>

                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </fieldset>
                            <fieldset id="fFamilyDetail">
                                <legend><span class="labelH labelH-info">Family Members Detail</span></legend>
                                <asp:UpdatePanel ID="updatepanel" runat="server">
                                    <ContentTemplate>
                                        <a href="javascript:void(0);" id="aAddFamilyMember" onclick="addFamilyRow();">Add Family Member</a>
                                        <table id="tblFamilyMembrDetail" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td>

                                                        <label class="control-label" for="typeahead">Name:</label>
                                                        <div class="controls">
                                                            <input id="txtFamilyName1" type="text" style="width: 200px; height: 18px;" class="span6 typeahead" />
                                                        </div>

                                                    </td>
                                                    <td style="float: left; width: 174px;">

                                                        <label class="control-label" for="typeahead">Age:</label>
                                                        <div class="controls">
                                                            <input id="txtFamilyAge1" type="text" style="width: 71px; height: 18px;" class="span6 typeahead" />
                                                        </div>

                                                    </td>
                                                    <td style="float: left; width: 83px;">

                                                        <label class="control-label" for="typeahead">Relation:</label>
                                                        <div class="controls">
                                                            <select id="ddlFamilyRelation1" style="width: 170px; height: 25px;">
                                                                <option value="0">--Select One--</option>
                                                                <option value="Father">Father</option>
                                                                <option value="Mother">Mother</option>
                                                                <option value="Wife">Wife</option>
                                                                <option value="Brother">Brother</option>
                                                                <option value="Sister">Sister</option>
                                                                <option value="Brother">Son</option>
                                                                <option value="Sister">Daughter</option>
                                                            </select>
                                                        </div>

                                                    </td>
                                                    <td style="float: right; width: 230px;">

                                                        <label class="control-label" for="typeahead">Nominee:</label>
                                                        <div class="controls">
                                                            <input type="checkbox" id="chkNominee" style="width: 10px; height: 10px;" />
                                                        </div>

                                                    </td>
                                                    <td></td>

                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </fieldset>
                            <fieldset id="fRefenceDetail">
                                <legend><span class="labelH labelH-info">Reference Detail</span></legend>
                                <asp:UpdatePanel ID="updatepanel1" runat="server">
                                    <ContentTemplate>

                                        <a href="javascript:void(0);" id="aReference" onclick="addRefernceRow();">Add Reference</a>
                                        <table width="100%" id="tblRefernceDetail">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <%--    <div class="control-group">--%>
                                                        <label class="control-label" for="typeahead">Name:</label>
                                                        <div class="controls">
                                                            <input id="txtRefName1" type="text" style="width: 200px; height: 18px;" class="span6 typeahead" />
                                                        </div>
                                                        <%--   </div>--%>
                                                    </td>
                                                    <td>
                                                        <%--    <div class="control-group">--%>
                                                        <label class="control-label" for="typeahead">Address:</label>
                                                        <div class="controls">
                                                            <input id="txtRefAddress1" type="text" style="width: 200px; height: 18px;" class="span6 typeahead" />
                                                        </div>
                                                        <%--   </div>--%>
                                                    </td>
                                                    <td>
                                                        <%-- <div class="control-group">--%>
                                                        <label class="control-label" for="typeahead">Phone No:</label>
                                                        <div class="controls">
                                                            <input id="txtRefPhoneNo1" type="text" style="width: 200px; height: 18px;" class="span6 typeahead" />
                                                        </div>
                                                        <%--  </div>--%>
                                                    </td>
                                                    <td>
                                                        <%--   <div class="control-group">--%>
                                                        <label class="control-label" for="typeahead">Relation:</label>
                                                        <div class="controls">
                                                            <input id="txtRefRelation1" type="text" style="width: 200px; height: 18px;" class="span6 typeahead" />
                                                        </div>
                                                        <%--  </div>--%>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </fieldset>
                        </div>
                        <div class="form-actions">
                            <%--<input id="btnSave" value="Save" class="btn btn-primary" />--%>
                            <input type="button" id="btnSave" value="Save" title="Save" class="btn btn-success" />
                            <%--  <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn btn-primary"  />--%>
                            <input type="button" id="btnEdit" value="Update" title="Save" class="btn btn-success" />
                            <%-- <asp:Button ID="btnEdit" Text="Update" runat="server" CssClass="btn btn-primary"/>--%>
                        </div>
                    </fieldset>

                </div>
            </div>
        </div>

        

        <div id="myModal" class="modal hide fade" style="display: none; width: 800px; height: 500px">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">×</button>
                <h3>Transport Employee <span id="spnName"></span>| <span id="spanidentityfication"></span></h3>
            </div>
            <div class="modal-body">
                <iframe id="iframeDailog" style="width: 750px; height: 500px"></iframe>
            </div>
        </div>

    </div>

</asp:Content>

