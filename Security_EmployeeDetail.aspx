<%@ Page Title="" Language="C#" MasterPageFile="~/Security_AdminMaster.master" AutoEventWireup="true" CodeFile="Security_EmployeeDetail.aspx.cs" Inherits="Security_EmployeeDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="content" class="span10">
          <asp:Button ID="btnNonApproved" runat="server" Text="View InActive Employee(s)" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)"  Font-Bold="True" ForeColor="Black" title="Click this button you get InActive employee" data-rel="tooltip" OnClick="btnNonApproved_Click" Width="235px" />
    </div>
    <script src="JavaScripts/Security.js"></script>
    <asp:HiddenField ID="hdnEmpID" runat="server" />
    <asp:HiddenField ID="hdnZoneId" runat="server" />
    <asp:HiddenField ID="hdnAcaId" runat="server" />
    <asp:HiddenField ID="hdnInchargeID" runat="server" />
    <asp:HiddenField ID="hdnName" runat="server" />
    <div id="divdetail" class="span10">
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
        <div id="pnlPdf" runat="server"></div>
        <div id="divEmployeeDetails" runat="server"></div>

    </div>
    <div id="divTransferEmployee" class="modal hide fade" style="display: none; width: 780px;">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">×</button>
            <h3>Transfer Security Employee</h3>
        </div>
        <div class="modal-body" id="divtransEmp">
            <table id="tblTransferEmployee" style="width: 730px;">
                <tbody>
                    <tr>
                        <td>
                            <label class="control-label" for="typeahead">Zone:</label>
                            <div class="controls">
                                <select id="ddlZone">
                                <option value="0">-Select Zone--</option></select>
                            </div>
                        </td>
                        <td>

                            <label class="control-label" for="typeahead">Academy:</label>
                            <div class="controls">
                                <select id="ddlAcademy">
                                      <option value="0">-Select Academy--</option>
                                </select>
                            </div>
                        </td>
                        <td>

                            <label class="control-label" for="typeahead">Upload Transfer Letter:</label>
                            <div class="controls">
                                <input id="uploadeTransferLetter" type="file" style="width: 150px; height: 18px;" class="span6 typeahead" />
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="modal-footer">
            <input id="btnUploadSave" value="Save" class="btn btn-primary" style="width: 40px" />
            <input id="btncloase" value="Close" style="width: 40px" class="btn btn-primary" data-dismiss="modal" />
        </div>
    </div>
</asp:Content>

