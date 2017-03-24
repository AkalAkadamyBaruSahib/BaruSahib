<%@ Page Title="" Language="C#" MasterPageFile="~/Visitor_AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="AddNewStudentInfo.aspx.cs" Inherits="AddNewStudentInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"> 
     <script src="JavaScripts/StudentAdmisssionNoInfo.js"></script>
     <asp:HiddenField ID="hdnStudentID" runat="server" />
    <div id="content" class="span10">
       <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2 style="color: #cc3300;"><i class="icon-edit"></i>Students Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
              
                    <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="student" />
                     <div class="well bs-component">
                        <fieldset>
                            <legend>
                             <div>
                                <label class="control-label" for="typeahead">Fill data through Admission Number:</label>
                                <input type="checkbox" id="chkAdminsnNumber" />
                            </div>

                            </legend>
                         <div class="box-content">
                                <div id="trStudentSearch" style="display: none;">
                                <table>
                                    <tr>
                                        <td>
                                            <label class="control-label" for="typeahead">Admission Number:</label>
                                            <input type="text" id="txtAdmisnNo" class="span6 typeahead" name="txtAdmisnNo" maxlength="8" style="width: 200px" />
                                        </td>
                                        <td>
                                            <input type="button" id="btnSearch" value="Search" class="btn btn-primary" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                                <table id="tabledata" width="100%" border="0">
                                    <tr>
                                        <td style="width: 30%">
                                            <label class="control-label" for="typeahead">Admission Number</label>
                                            <asp:TextBox ID="txtAdmissionNumber" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="student" ID="reqName" ControlToValidate="txtAdmissionNumber" ErrorMessage="Please enter the Admission Number" />
                                        </td>
                                        <td style="width: 30%">
                                            <label class="control-label" for="typeahead">Class</label>
                                            <asp:TextBox ID="txtClass" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="student" ID="RequiredtxtClass" ControlToValidate="txtClass" ErrorMessage="Please enter the Class" />

                                        </td>
                                        <td style="width: 40%">
                                            <label class="control-label" for="typeahead">Student Name</label>
                                            <asp:TextBox ID="txtStudentName" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="student" ID="RequiredtxtStudentName" ControlToValidate="txtStudentName" ErrorMessage="Please enter the Student Name" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%">
                                            <label class="control-label" for="typeahead">Father Name</label>
                                            <asp:TextBox ID="txtFatherName" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="student" ID="RequiredtxtFatherName" ControlToValidate="txtFatherName" ErrorMessage="Please enter the Father Name" />

                                        </td>
                                        <td style="width: 30%">
                                            <label class="control-label" for="typeahead">Contact Number</label>
                                            <asp:TextBox ID="txtContactNumber" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="enter only numeric digit minimum length 10 and maximum 15." ForeColor="Red" ControlToValidate="txtContactNumber" ValidationExpression="^[0-9]{10,15}$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 40%">
                                            <label class="control-label" for="typeahead">Addresss </label>
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="span6 typeahead" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="student" ID="RequiredtxtAddress" ControlToValidate="txtAddress" ErrorMessage="Please enter the Address" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="width: 30%">
                                            <label class="control-label" for="typeahead">Country </label>
                                            <asp:DropDownList ID="drpCountry" runat="server" CssClass="span6 typeahead" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">--Select Country--</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator Display="None" InitialValue="0" runat="server" ValidationGroup="student" ID="RequireddrpCountry" ControlToValidate="drpCountry" ErrorMessage="Please select the Country" />

                                        </td>
                                        <td style="width: 30%">
                                            <label class="control-label" for="typeahead">Sate</label>
                                            <asp:DropDownList ID="drpState" runat="server" CssClass="span6 typeahead" AutoPostBack="true" OnSelectedIndexChanged="drpState_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--Select State--</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator Display="None" runat="server" InitialValue="0" ValidationGroup="student" ID="RequireddrpState" ControlToValidate="drpState" ErrorMessage="Please select the State" />

                                        </td>
                                        <td style="width: 40%">
                                            <label class="control-label" for="typeahead">City</label>
                                            <asp:DropDownList ID="drpCity" runat="server" CssClass="span6 typeahead">
                                                <asp:ListItem Value="0">--Select City--</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator Display="None" runat="server" InitialValue="0" ValidationGroup="student" ID="RequireddrpCity" ControlToValidate="drpCity" ErrorMessage="Please select the City" />

                                        </td>

                                    </tr>
                                </table>
                            </div>

                            <div class="form-actions">
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="student" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                            </div>
                        </fieldset>
                 </div>
            </div>
            <!--/span-->

        <%--<div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2 style="color: #cc3300;"><i class="icon-user"></i>Buildings Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="divBuildingNameDetails" runat="server"></div>

                </div>
            </div>
            <!--/span-->

        </div>--%>
    </div>
</asp:Content>

