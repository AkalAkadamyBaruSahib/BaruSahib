<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaterialToDispatch.ascx.cs" Inherits="Admin_UserControls_MaterialToDispatch" %>

<style type="text/css">
    .auto-style1
    {
        width: 546px;
    }
</style>
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
    // Changes done By nishu


</script>
<asp:HiddenField ID="hdnInchargeID" runat="server" />
<asp:HiddenField ID="hdnIsAdmin" runat="server" />
<asp:HiddenField ID="hdnModule" runat="server" />
<asp:HiddenField ID="hdnPSID" runat="server" />
<div id="content" class="span10">
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div class="row-fluid sortable">
        <div class="box span12">
            <div id="boxheader" class="box-header well">
                <h2><i class="icon-user"></i>Material dispatch status</h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <div id="divDesigDetails" runat="server">
                    <table border="0" style="width: 100%">
                        <tbody>
                            <tr>
                                <td class="auto-style1">
                                    <asp:Button ID="btnExecl" runat="server" Text="Dispatch Excel Download" CssClass="btn btn-primary"/>
                                </td>
                                <td>Select Academy:
                                        <asp:DropDownList ID="ddlAcademy" runat="server" Height="36px" Width="164px">
                                            <asp:ListItem Value="0">--Select Academy--</asp:ListItem>
                                        </asp:DropDownList>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
         
        </div>
    </div>
    
    <div id="pnlPdf" runat="server"></div>
    <div id="divEstimateDetails"></div>
    <div id="divRejectItem" class="modal hide fade">
        <div class="modal-header">
            <label id="lblestid"></label>
        </div>
        <div class="modal-body">
            <table>
                <tr>
                    <td colspan="2" style="color: red;">
                        <b>Please provide the valid comment below:</b>
                    </td>
                </tr>
                <tr>
                    <td>Comments:<asp:RequiredFieldValidator runat="server" ID="divRequiredtxtRemarks"
                        ValidationGroup="rejectitem" ControlToValidate="txtRemarks" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator></td>
                    <td style="text-align: center">
                        <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="5" Width="400px" runat="server"></asp:TextBox>

                    </td>
                </tr>
            </table>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="rejectitem" CssClass="btn-primary" />
            <a href="#" class="btn btn-primary" data-dismiss="modal">Cancel</a>
        </div>
    </div>
</div>
