<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="StudentWiseTransportDetail.aspx.cs" Inherits="StudentWiseTransportDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnStudentID" runat="server" />
    <asp:HiddenField ID="hdnInchargeID" runat="server" />
    <asp:HiddenField ID="hdnAcaID" runat="server" />
    <script src="JavaScripts/TransportStudentDetail.js"></script>
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


                <div class="well bs-component">
                    <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="student" />
                    <%--   <fieldset>
                        <legend>
                            <div>
                                <label class="control-label" for="typeahead">Fill data through Admission Number:</label>
                                <input type="checkbox" id="chkAdminsnNumber" />
                            </div>
                        </legend>--%>
                    <%--<div id="trStudentSearch" style="display: none;">
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
                            </div>--%>
                    <div class="box-content">
                        <table id="tabledata" width="100%" border="0">
                            <tr>
                                <td style="width: 30%">
                                    <label class="control-label" for="typeahead">Admission Number</label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="student" ID="reqName" ControlToValidate="txtAdmissionNumber" ErrorMessage="Please enter the Admission Number"><span style="color:red">*</span></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtAdmissionNumber" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                </td>
                                <td style="width: 30%">
                                    <label class="control-label" for="typeahead">Student Name</label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="student" ID="RequiredtxtStudentName" ControlToValidate="txtStudentName" ErrorMessage="Please enter the Student Name"><span style="color:red">*</span></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtStudentName" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                </td>
                                <td style="width: 40%">
                                    <label class="control-label" for="typeahead">Father Name</label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="student" ID="RequiredtxtFatherName" ControlToValidate="txtFatherName" ErrorMessage="Please enter the Father Name"><span style="color:red">*</span></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtFatherName" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label class="control-label" for="typeahead">Class</label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="student" ID="RequiredtxtClass" ControlToValidate="txtClass" ErrorMessage="Please enter the Class"><span style="color:red">*</span></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtClass" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                </td>
                                <td style="width: 30%; padding-top: 30px;">
                                    <label class="control-label" for="typeahead">Contact Number</label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="student" ID="RequiredFieldValidator1" ControlToValidate="txtContactNumber" ErrorMessage="Please enter the Contact Number"><span style="color:red">*</span></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtContactNumber" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="enter only numeric digit minimum length 10 and maximum 15." ForeColor="Red" ControlToValidate="txtContactNumber" ValidationExpression="^[0-9]{10,15}$"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 40%">
                                    <label class="control-label" for="typeahead">Name Of Village </label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="student" ID="RequiredtxtAddress" ControlToValidate="txtNameOfVillage" ErrorMessage="Please enter the Name Of Village"><span style="color:red">*</span></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtNameOfVillage" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label class="control-label" for="typeahead">Academy Name</label>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="student" ID="RequiredFieldValidator2" InitialValue="0" ControlToValidate="drpAcademy" ErrorMessage="Please select the Academy Name"><span style="color:red">*</span></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="drpAcademy" runat="server" CssClass="span6 typeahead">
                                        <asp:ListItem Value="0">--Select Academy--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                              
                            </tr>
                        </table>
                    </div>
                    <div class="form-actions">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="student" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                    </div>

                    <%-- </fieldset>--%>
                </div>
            </div>


            <div class="row-fluid sortable">
                <div class="box span12">
                    <div class="box-header well" data-original-title>
                        <h2><i class="icon-user"></i>Transport Student Details</h2>
                        <div class="box-icon">
                            <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                            <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                            <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                        </div>
                    </div>
                    <div class="box-content">
                        <div id="divTransportStudentDetails" runat="server">
                            <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                                <thead>
                                    <tr>
                                        <th style="color: #cc3300;">Admission Number</th>
                                        <th style="color: #cc3300;">Student Information</th>
                                        <th style="color: #cc3300;">Class</th>
                                        <th style="color: #cc3300;">Contact Number</th>
                                        <th style="color: #cc3300;">Name Of Village</th>
                                        <th style="color: #cc3300;">Action</th>
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
    </div>
</asp:Content>
