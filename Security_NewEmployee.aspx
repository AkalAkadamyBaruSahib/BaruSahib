<%@ Page Title="" Language="C#" MasterPageFile="~/Security_AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Security_NewEmployee.aspx.cs" Inherits="Security_NewEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/Security.js"></script>
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
        <asp:HiddenField ID="hdnsecurityEmployeeID" runat="server" />
        <div class="row-fluid sortable" runat="server" id="divAllotment">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Create Employee</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <fieldset>
                        <legend></legend>
                        <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="security" />
                        <div class="box-content">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Name:</label>
                                                    <div class="controls">
                                                        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtName" runat="server" Width="200px" CssClass="span6 typeahead"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="reqName" ForeColor="Red" ControlToValidate="txtName" ErrorMessage="Please Enter the  Name" />
                                                    </div>
                                                </div>
                                            </td>
                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Mobile No:</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtMobileNo" runat="server" Width="200px" CssClass="span6 typeahead"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Invalid Mobile No" ValidationExpression="[0-9]{10}" ForeColor="Red"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtMobileNo" ErrorMessage="Please Enter the Mobile No" />
                                                    </div>
                                                </div>
                                            </td>
                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Address:</label>
                                                    <div class="controls">
                                                        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="200px" CssClass="span6 typeahead"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator2" ForeColor="Red" ControlToValidate="txtAddress" ErrorMessage="Please Enter the Address" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Salary:</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtSalary" runat="server" Width="200px" CssClass="span6 typeahead"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="txtSalary" ErrorMessage="Please Enter the  Salary" />
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Deduction:</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtCutting" runat="server" Width="200px" CssClass="span6 typeahead"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator4" ForeColor="Red" ControlToValidate="txtCutting" ErrorMessage="Please Enter the Cutting" />
                                                    </div>
                                                </div>
                                            </td>

                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Education:</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlEducation" Width="200px" runat="server">
                                                            <asp:ListItem Text="" Value="0">--Select One--</asp:ListItem>
                                                            <asp:ListItem Text="Below Matrix" Value="Below Matrix"></asp:ListItem>
                                                            <asp:ListItem Text="Matrix" Value="Matrix"></asp:ListItem>
                                                            <asp:ListItem Text="+2" Value="+2"></asp:ListItem>
                                                            <asp:ListItem Text="Diploma" Value="Diploma"></asp:ListItem>
                                                            <asp:ListItem Text="Graduation" Value="Graduation"></asp:ListItem>
                                                            <asp:ListItem Text="Post Graduation" Value="Post Graduation"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" InitialValue="0" ID="RequiredFieldValidator5" ForeColor="Red" ControlToValidate="ddlEducation" ErrorMessage="Please Select the Education" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>

                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <fieldset>
                                <legend><span class="labelH labelH-info">Location Assign</span></legend>
                                <asp:UpdatePanel ID="updatepanel3" runat="server">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>

                                                <td>
                                                    <div class="control-group">
                                                        <label class="control-label" for="typeahead">Zone:</label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="drpZone" Width="200px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpZone_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">--Select Zone--</asp:ListItem>
                                                            </asp:DropDownList><br />
                                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator9" ForeColor="Red" ControlToValidate="drpZone" ErrorMessage="Please Select the Zone" />
                                                        </div>
                                                    </div>
                                                </td>
                                                <td width="50%">
                                                    <div class="control-group">
                                                        <label class="control-label" for="typeahead">Academy:</label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="drpAcademy" Width="200px" runat="server">
                                                                <asp:ListItem Value="0">--Select Academy--</asp:ListItem>
                                                            </asp:DropDownList><br />
                                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator8" ForeColor="Red" ControlToValidate="drpAcademy" ErrorMessage="Please Select the Academy" />
                                                        </div>
                                                    </div>

                                                </td>
                                                <td width="50%">
                                                    <div class="control-group">
                                                        <label class="control-label" for="typeahead">Designation:</label>
                                                        <div class="controls">
                                                            <asp:DropDownList ID="ddlDesig" Width="200px" runat="server"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator6" ForeColor="Red" ControlToValidate="ddlDesig" ErrorMessage="Please Select the Designation" />
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td width="50%">
                                                    <label class="control-label" for="typeahead">Date of Joining:</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtDateofJoining" runat="server" Width="200px" CssClass="input-xlarge datepicker"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator7" ForeColor="Red" ControlToValidate="txtDateofJoining" ErrorMessage="Please Enter the Date of Joining" />
                                                    </div>
                                                </td>
                                                <td width="50%">
                                                    <div class="control-group">
                                                        <label class="control-label" for="typeahead">Date of Appraisal:</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtDateofAppraisal" runat="server" Width="200px" CssClass="input-xlarge datepicker"></asp:TextBox>
                                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator10" ForeColor="Red" ControlToValidate="txtDateofAppraisal" ErrorMessage="Please Enter the Date of Appraisal" />
                                                        </div>
                                                    </div>

                                                </td>
                                                <td>
                                                    <div class="control-group">
                                                        <label class="control-label" for="typeahead">Last Appraisal Amount:</label>
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtLastAppraisal" runat="server" Width="200px" CssClass="span6 typeahead"></asp:TextBox>
                                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator11" ForeColor="Red" ControlToValidate="txtLastAppraisal" ErrorMessage="Please Enter the Last Appraisal Amount" />
                                                        </div>
                                                    </div>
                                                </td>

                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </fieldset>
                            <fieldset>
                                <legend><span class="labelH labelH-info">Employee Detail Upload</span></legend>
                                <asp:UpdatePanel ID="updatepanel2" runat="server">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="50%">
                                                    <div class="control-group" id="div1" runat="server">
                                                        <label class="control-label" for="typeahead">Appointment:</label>
                                                        <div class="controls">
                                                            <asp:FileUpload ID="fileUploadAppointment" runat="server" />
                                                            <a id="afileUploadAppointment" runat="server" visible="false" style="font-size: 13px;" target="_blank">Appointment Letter</a>
                                                          </div>
                                                    </div>
                                                </td>
                                                <td width="50%">
                                                    <div class="control-group" id="div2" runat="server">
                                                        <label class="control-label" for="typeahead">Experience:</label>
                                                        <div class="controls">
                                                            <asp:FileUpload ID="fileUploadExperience" runat="server" />
                                                            <a id="afileUploadExperience"  runat="server" visible="false" style="font-size: 13px;" target="_blank">Experience Letter </a>
                                                           </div>
                                                    </div>
                                                </td>
                                                <td width="50%">
                                                    <div class="control-group" id="div3" runat="server">
                                                        <label class="control-label" for="typeahead">Family Ration Card:</label>
                                                        <div class="controls">
                                                            <asp:FileUpload ID="fileUploadFamilyRationCard" runat="server" />
                                                            <a id="afileUploadFamilyRationCard"  runat="server" visible="false" style="font-size: 13px;" target="_blank">Ration Card </a>
                                                           </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    <div class="control-group" id="div4" runat="server">
                                                        <label class="control-label" for="typeahead">PCC(Verification):</label>
                                                        <div class="controls">
                                                            <asp:FileUpload ID="fileUploadPCC" runat="server" />
                                                            <a id="afileUploadPCC"  runat="server" visible="false" style="font-size: 13px;" target="_blank">PCC</a>
                                                         </div>
                                                    </div>
                                                </td>
                                                <td width="50%">
                                                    <div class="control-group" id="div5" runat="server">
                                                        <label class="control-label" for="typeahead">Qualification Letter:</label>
                                                        <div class="controls">
                                                            <asp:FileUpload ID="fileUploadQualification" runat="server" />
                                                            <a id="afileUploadQualification"  runat="server" visible="false" style="font-size: 13px;" target="_blank">Qualification Letter </a>
                                                        </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="control-group" id="dividphoto" runat="server">
                                                        <label class="control-label" for="typeahead">Upload Photo:</label>
                                                        <div class="controls">
                                                            <asp:FileUpload ID="fileUploadphoto" runat="server" />
                                                            <a id="afileUploadphoto"  runat="server" visible="false" style="font-size: 13px;" target="_blank">Photo</a>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </fieldset>
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-primary" runat="server" ValidationGroup="security" OnClick="btnSave_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                            <asp:Button ID="Button1" Text="Cancel" CssClass="btn" runat="server" OnClick="Button1_Click" />
                        </div>
                        <div id="myModal" class="modal hide fade" style="display: none; width: 800px; height: 500px">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">×</button>
                                <h3>Security <span id="spnName"></span>| <span id="spanidentityfication"></span></h3>
                            </div>
                            <div class="modal-body">
                                <iframe id="iframeDailog" style="width: 750px; height: 500px"></iframe>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

