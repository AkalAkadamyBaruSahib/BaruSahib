<%@ Page Title="" Language="C#" MasterPageFile="~/Security_AdminMaster.master" AutoEventWireup="true" CodeFile="Security_NewEmployee.aspx.cs" Inherits="Security_NewEmployee" %>

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
                                                       <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="reqName" ForeColor="Red"  ControlToValidate="txtName" ErrorMessage="Please Enter the  Name" />
                                                    </div>
                                                </div>
                                            </td>
                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Mobile No:</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtMobileNo" runat="server" Width="200px" CssClass="span6 typeahead"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Invalid Mobile No" ValidationExpression="[0-9]{10}" ForeColor="Red"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator1" ForeColor="Red"  ControlToValidate="txtMobileNo" ErrorMessage="Please Enter the Mobile No" />
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
                                                    <label class="control-label" for="typeahead">Cutting:</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtCutting" runat="server" Width="200px" CssClass="span6 typeahead"></asp:TextBox>
                                                         <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator4" ForeColor="Red"  ControlToValidate="txtCutting" ErrorMessage="Please Enter the Cutting" />
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
                                        <tr>
                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Designation:</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlDesig" Width="200px" runat="server"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator6" ForeColor="Red" ControlToValidate="ddlDesig" ErrorMessage="Please Select the Designation" />
                                                    </div>
                                                </div>
                                            </td>
                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Department:</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlDept" Width="200px" runat="server"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator7" ForeColor="Red" ControlToValidate="ddlDept" ErrorMessage="Please Select the Department" />
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Zone:</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlZone" Width="200px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList><br />   
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator9" ForeColor="Red" ControlToValidate="ddlZone" ErrorMessage="Please Select the Zone" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Academy:</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="ddlAcademy" Width="200px" runat="server"></asp:DropDownList><br />
                                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="security" ID="RequiredFieldValidator8" ForeColor="Red" ControlToValidate="ddlAcademy" ErrorMessage="Please Select the Academy" />
                                                    </div>
                                                </div>
                                            </td>

                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                                                            <a id="afileUploadAppointment" style="font-size: 13px;"  target="_blank">Appointment Letter</a>
                                          <%--                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="fileUploadAppointment" runat="server" ValidationGroup="security" Display="None" ErrorMessage="Please Upload  Appointment"></asp:RequiredFieldValidator>
                                         --%>               </div>
                                                    </div>
                                                </td>
                                                <td width="50%">
                                                    <div class="control-group" id="div2" runat="server">
                                                        <label class="control-label" for="typeahead">Experience:</label>
                                                        <div class="controls">
                                                            <asp:FileUpload ID="fileUploadExperience" runat="server" />
                                                            <a id="afileUploadExperience" style="font-size: 13px;"  target="_blank">Experience Letter </a>
                                                 <%--           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="fileUploadExperience" runat="server" ValidationGroup="security" Display="None" ErrorMessage="Please Upload  Experience"></asp:RequiredFieldValidator>
                                                --%>        </div>
                                                    </div>
                                                </td>
                                                <td width="50%">
                                                    <div class="control-group" id="div3" runat="server">
                                                        <label class="control-label" for="typeahead">Family Ration Card:</label>
                                                        <div class="controls">
                                                            <asp:FileUpload ID="fileUploadFamilyRationCard" runat="server" />
                                                            <a id="afileUploadFamilyRationCard" style="font-size: 13px;" target="_blank">Ration Card </a>
                                                 <%--           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="fileUploadFamilyRationCard" runat="server" ValidationGroup="security" Display="None" ErrorMessage="Please Upload  Family Ration Card"></asp:RequiredFieldValidator>
                                                --%>        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    <div class="control-group" id="div4" runat="server">
                                                        <label class="control-label" for="typeahead">PCC(Verification):</label>
                                                        <div class="controls">
                                                            <asp:FileUpload ID="fileUploadPCC" runat="server" />
                                                            <a id="afileUploadPCC" style="font-size: 13px;"  target="_blank">PCC</a>
                                                   <%--         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="fileUploadPCC" runat="server" ValidationGroup="security" Display="None" ErrorMessage="Please Upload  PCC"></asp:RequiredFieldValidator>
                                               --%>         </div>
                                                    </div>
                                                </td>
                                                <td width="50%">
                                                    <div class="control-group" id="div5" runat="server">
                                                        <label class="control-label" for="typeahead">Qualification Letter:</label>
                                                        <div class="controls">
                                                            <asp:FileUpload ID="fileUploadQualification" runat="server" />
                                                            <a id="afileUploadQualification" style="font-size: 13px;"  target="_blank">Qualification Letter </a>
                                              <%--              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="fileUploadQualification" runat="server" ValidationGroup="security" Display="None" ErrorMessage="Please Upload Qualification"></asp:RequiredFieldValidator>
                                              --%>          </div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div class="control-group" id="dividphoto" runat="server">
                                                        <label class="control-label" for="typeahead">Upload Photo:</label>
                                                        <div class="controls">
                                                            <asp:FileUpload ID="fileUploadphoto" runat="server" />
                                                               <a id="afileUploadphoto" style="font-size: 13px;"  target="_blank">Photo</a>
                                                <%--            <asp:RequiredFieldValidator ID="RequiredFieldValidatorphoto" ControlToValidate="fileUploadphoto" runat="server" ValidationGroup="security" Display="None" ErrorMessage="Please Upload  Photo"></asp:RequiredFieldValidator>
                                              --%>          </div>
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
                        <div class="row-fluid sortable">
                            <div class="box span12">
                                <div class="box-header well" data-original-title>
                                    <h2 style="color: #cc3300;"><i class="icon-user"></i>Security Employee Details</h2>
                                    <div class="box-icon">
                                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                                    </div>
                                </div>
                                <div class="box-content">

                                    <div id="divMatDetails" runat="server">
                                        <div id="divVisitorDetails" runat="server">
                                            <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                                                <thead>
                                                    <tr>
                                                        <th style="color: #cc3300;">Name</th>
                                                        <th style="color: #cc3300;">Address</th>
                                                        <th style="color: #cc3300;">Contact No</th>
                                                        <th style="color: #cc3300;">Qualification</th>
                                                        <th style="color: #cc3300;">Documents</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tbody">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
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

