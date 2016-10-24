<%@ Page Title="" Language="C#" MasterPageFile="~/Security_AdminMaster.master" AutoEventWireup="true" CodeFile="Security_EmployeeDetail.aspx.cs" Inherits="Security_EmployeeDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
     function validateFileSize(controlID) {
            var id = controlID.files[0].size;
            if (id > 200000) {
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
                                <input id="uploadeTransferLetter" type="file" style="width: 150px; height: 18px;" class="span6 typeahead" onchange="validateFileSize(this);" />
                                <div id="dvMsg" style="color: Red; width: 250px; display: none;">Maximum size allowed Less than is 200 KB </div>
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
    <div id="divViewTransferEmployee" class="modal hide fade" style="display: none; width: 500px;">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">×</button>
        <h3>Employee Transfer Detail:-</h3>
    </div>
    <div class="modal-body" style="width: 300px;">
        <table id="grdLetter" class='table table-striped table-bordered'>
            <thead>
                <tr>
                    <th>Date Of Transfer</th>
                </tr>
            </thead>
            <tbody id="tbody">
            </tbody>
        </table>
    </div>
    <div class="modal-footer">
        <input id="btnclose" value="Close" style="width: 40px" class="btn btn-primary" data-dismiss="modal" />
    </div>
</div>
</asp:Content>

