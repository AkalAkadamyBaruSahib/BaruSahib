<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreateTicket.ascx.cs" Inherits="Admin_UserControls_CreateTicket" %>

<input id="txtuserID" runat="server" type="hidden" value="0" />
<input id="hdnID" runat="server" type="hidden" value="0" />
<input id="hdnUserType" runat="server" type="hidden" value="0" />
<input id="hdnLoginID" runat="server" type="hidden" value="0" />
<div id="content" class="span10">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center">
                <div class="controls">
                    <%--<asp:Button ID="btnNewTicket" Text="Create New Ticket" runat="server" />--%>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class='box span12'>
                    <div class='box-header well data-original-title'>
                        <h2><i class='icon-user'></i>Complaint Ticket Details</h2>
                        <div class='box-icon'>
                            <input id="btnNewTicket" type="button" value="Create New Ticket" />
                            <%--<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>
                <a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>--%>
                        </div>
                    </div>
                    <div class='box-content'>
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
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody id="tbody">
                            </tbody>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>

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
                <input type="text" id="txtDays" value="2" style="display: none;width:50px" />
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
                <asp:TextBox ID="txtCompletionDate" Width="100px" runat="server" CssClass="input-xlarge datepicker"></asp:TextBox>
            </td>
            <%--<td><span id="spnCompletion">Date of Completion Ticket:- </span> </td>
            <td>
                <asp:TextBox ID="txtCompletionDate" Width="100px" runat="server" CssClass="input-xlarge datepicker"></asp:TextBox>
            </td>--%>
        </tr>
        <tr id="trComments">
            <td>Comments:- </td>
            <td colspan="3" >
                <textarea id="txtComments" name="txtBody" style="width: 536px; height: 150px" rows="10" cols="100"></textarea>
            </td>
        </tr>
        <tr id="trStatus">
            <td>Status:- </td>
            <td>
                <select id="ddlStatus" class="dropdown">
                    <option value="In Progress">In Progres</option>
                    <option value="Completed">Completed</option>
                </select></td>
            <td><span id="spnApproved">Approved:- </span> </td>
            <td>
                <select id="ddlApproved" class="dropdown">
                    <option value="true">Approved</option>
                    <option value="false">Rejected</option>
                </select></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="3">
                <asp:Button ID="btnSave" runat="server" Text="Save" />
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
