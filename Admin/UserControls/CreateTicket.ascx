<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreateTicket.ascx.cs" Inherits="Admin_UserControls_CreateTicket" %>

<input id="hdnUserID" runat="server" type="hidden" value="0" />
<input id="hdnID" runat="server" type="hidden" value="0" />
<input id="hdnUserType" runat="server" type="hidden" value="0" />
<input id="hdnLoginID" runat="server" type="hidden" value="0" />
<input id="txtuserID" runat="server" type="hidden" value="0" />
 <script type="text/javascript">
     function ReportOnChange(control) {
         if (control.value == "1" || control.value == "2" || control.value == "3") {
             $("input[id*='btnDownload']").show();
         }
         else {
             $("input[id*='btnDownload']").hide();
         }
     }
     </script>
<div id="content" class="span10">
    <div class="row-fluid sortable" runat="server" id="divAllotment">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i>Complaint Ticket Report</h2>
                <div class='box-icon'>
                    <input id="btnNewTicket" type="button" class="btn btn-primary"  value="Create New Ticket" />
                </div>

            </div>
        <div class="box-content">
                Select Report to Download Data:
                    <asp:DropDownList ID="ddlReport" runat="server" onchange="ReportOnChange(this);">
                        <asp:ListItem Text="--Choose Report Type--" Selected="True" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="New Complaints" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Pending Complaints" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Completed Complaints" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                <asp:Button ID="btnDownload" runat="server"  Text="Download" CssClass="btn btn-primary" style="margin-bottom: 11px; display:none;" OnClick="btnDownload_Click" />
            </div>
        </div>
    </div>

    <div id="tabs" class="bs-component">
        <ul> 
            <li><a href="#divNewComplaint">New</a></li>
            <li><a href="#divInProgresComplaint">In Progress</a></li>
            <li><a href="#divCompletedComplaint">Completed</a></li>
        </ul>
        <div id="divNewComplaint">
            <table id="grdTicketDiscription" class='table table-striped table-bordered bootstrap-datatable datatable'>
                <thead>
                    <tr>
                        <th>Zone & Academy</th>
                        <th style="width: 30%">Description</th>
                        <th>CreatedOn</th>
                        <th>Tentative Date</th>
                        <th>Completion Date</th>
                        <th>Status</th>
                        <th>Feedback</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody id="tbody">
                </tbody>
            </table>
        </div>
        <div id="divInProgresComplaint">
            <table id="grdInProgressTicket" style="width:1150px;" class='table table-striped table-bordered bootstrap-datatable datatable'>
                <thead>
                    <tr>
                        <th>Zone & Academy</th>
                        <th style="width: 30%">Description</th>
                        <th>CreatedOn</th>
                        <th>Tentative Date</th>
                        <th>Completion Date</th>
                        <th>Status</th>
                        <th>Feedback</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody id="tbodyProgress">
                </tbody>
            </table>
        </div>
        
        
        <div id="divCompletedComplaint">
            <table id="grdCompletedTicket" style="width:1150px;" class='table table-striped table-bordered bootstrap-datatable datatable'>
                <thead>
                    <tr>
                        <th>Zone & Academy</th>
                        <th style="width: 30%">Description</th>
                        <th>CreatedOn</th>
                        <th>Tentative Date</th>
                        <th>Completion Date</th>
                        <th>Status</th>
                        <th>Feedback</th>
                         <th>Action</th>
                     </tr>
                </thead>
                <tbody id="tbody2">
                </tbody>
            </table>
        </div>
    </div>

</div>

<div id="divCreateTicket" class="controls" style="display: none">
    <table style="width: 100%">
        <tr>
            <td>Complaint Type: 
            </td>
            <td>
                <select id="ddlComplaintType" class="dropdown">
                    <option value="Construction Complaint">Construction Maintenance</option>
                    <option value="Electrical Complaint">Electrical Maintenance</option>
                    <option value="Construction Complaint">Plumbing Maintenance</option>
                    <option value="Electrical Complaint">Transport Maintenance</option>
                    <option value="Electrical Complaint">Other</option>
                </select></td>
            <td>Severity: 
            </td>
            <td>
                <select id="ddlSeverity" class="dropdown">
                    <option value="1">Urgent</option>
                    <option value="2">Regular</option>
                </select>
                <input type="text" id="txtDays" value="2" style="display: none; width: 50px" />
            </td>
        </tr>
        <tr>
            <td>Description:- 
            </td>
            <td colspan="3">
                <textarea id="txtBody" name="txtBody" style="width: 536px; height: 150px" rows="10" cols="100"></textarea>
            </td>
        </tr>
        <%--<tr>
            <td>Upload Image(if any):</td>
            <td>
                <input type="file" id="txtfileupload"  />
            </td>
        </tr>--%>
        <tr id="trCompletionDate">
            <td>Tentative Date:- </td>
            <td>
                <asp:TextBox ID="txtCompletionDate" Width="100px" runat="server"  CssClass="input-xlarge datepicker" Enabled="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqCompletion" runat="server" ValidationGroup="Comp" ControlToValidate="txtCompletionDate" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                 
              </td>
            <%--<td><span id="spnCompletion">Date of Completion Ticket:- </span> </td>
            <td>
                <asp:TextBox ID="txtCompletionDate" Width="100px" runat="server" CssClass="input-xlarge datepicker"></asp:TextBox>
            </td>--%>
        </tr>
        <tr id="trComments">
            <td>Comments:- </td>
            <td colspan="3">
                <textarea id="txtComments" name="txtBody" style="width: 536px; height: 150px" rows="10" cols="100"></textarea>
            </td>
        </tr>
        <tr id="trStatus">
            <td>Status:- </td>
            <td>
                <select id="ddlStatus" class="dropdown">
                    <option value="Assigned">Assigned</option>
                    <option value="In Progress">In Progres</option>
                    <option value="Completed">Completed</option>
                </select></td>
            <td><span id="spnApproved">Approved:- </span></td>
            <td>
                <select id="ddlApproved" class="dropdown">
                    <option value="true">Approved</option>
                    <option value="false">Rejected</option>
                </select></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="3">
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Comp" />
            </td>
        </tr>
    </table>
</div>

<div id="divFeedback" class="controls" style="display: none">
    <table style="width: 100%">
        <tr>
            <td>Please rate your feedback: 
            </td>
            <td>
                <select id="ddlfeedback" class="dropdown">
                    <option value="Not done">Not done</option>
                    <option value="Good">Good</option>
                    <option value="Very Good">Very Good</option>
                    <option value="Exelent">Exelent</option>
                </select></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="3">
                <asp:Button ID="btnSaveFeedback" runat="server" Text="Save" />
            </td>
        </tr>
    </table>
</div>
