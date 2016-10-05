<%@ Page Title="" Language="C#" MasterPageFile="~/Security_AdminMaster.master" AutoEventWireup="true" CodeFile="Security_ComplaintTickets.aspx.cs" Inherits="Security_ComplaintTickets" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/SecurityEmpSearch.js"></script>
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">
                    <h2><i class="icon-edit"></i>Employee Compliant</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <table width="100%">
                        <tr>
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead">Upload Compliant:</label>
                                    <div class="controls">
                                    <asp:FileUpload ID="fileUploadCompliant" runat="server" />
                                    </div>
                                </div>
                            </td>
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead">Date:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtDate" runat="server" Width="200px" CssClass="input-xlarge datepicker"></asp:TextBox>
                                     </div>
                                </div>
                            </td>
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead">Remarks:</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="200px" CssClass="span6 typeahead"></asp:TextBox>
                                    </div>
                                </div>
                            </td>
                            
                        </tr>
                    </table>
                       <div class="form-actions">
                            <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-primary" runat="server"/>
                            <asp:Button ID="Button1" Text="Cancel" CssClass="btn" runat="server" />
                        </div>
                </div>
                <div id="divEmployeeDetails" runat="server"></div>
            </div>
            <!--/span-->

        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2 style="color: #cc3300;"><i class="icon-user"></i>Compliants Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                       <div id="divCompliantsDetails" runat="server">
                            <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                                <thead>
                                    <tr>
                                        <th style="color: #cc3300;">Compliant</th>
                                        <th style="color: #cc3300;">Date</th>
                                        <th style="color: #cc3300;">Remarks</th>
                                    </tr>
                                </thead>
                                <tbody id="tbody">
                                </tbody>
                            </table>
                        </div>
                    </div>
             
                <!--/span-->

            </div>
        </div>
    </div>
</asp:Content>



