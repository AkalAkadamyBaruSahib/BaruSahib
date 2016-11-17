<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyPurchaseMaterialDetails.ascx.cs" Inherits="Admin_UserControls_BodyPurchaseMaterialDetails" %>

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
<asp:HiddenField ID="hidEstID" runat="server" />
<asp:HiddenField ID="hidEMRID" runat="server" />
<div id="content" class="span10">
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div id="divacademy" runat="server"  class="row-fluid sortable">
        <div class="box span12">
            <div id="divDrpHeader" runat="server" class="box-header well">
                <marquee behavior='scroll' direction='left'>WELCOME  TO AKALSEWA SOFTWARE | Material To Dispatch</marquee>
            </div>
            <div class="box-content">
                <div id="divDesigDetails" runat="server">
                    <table border="0" style="width: 100%">
                        <tbody>
                            <tr>
                                <td class="auto-style1">
                                    <asp:Button ID="btnExecl" Visible="false" runat="server" Text="Dispatch Excel Download" CssClass="btn btn-primary" OnClick="btnExecl_Click" />
                                </td>
                                <td>Select Academy:
                                        <asp:DropDownList ID="ddlAcademy" runat="server" Height="36px" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged" Width="164px" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <%--    // Changes done by Nishu--%>
        </div>
    </div>
    <div id="pnlPdf" runat="server"></div>
    <div id="divEstimateDetails" runat="server"></div>
    <div id="divRejectItem" class="modal hide fade">
        <div class="modal-header">
            <label id="lblestid"></label>
        </div>
        
        <div class="modal-body">
            <table>
                <tr>
                    <td></td>
                    <td style="color:red;">
                        <b>Are You Sure You Want To Reject This Item? Please Provide The Valid Comment Below:</b>
                        </td>
                </tr>
                <tr>
                    <td>Comment</td>
                    <td colspan="2" style="text-align: center">
                       <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="divRequiredtxtRemarks" ValidationGroup="rejectitem" ControlToValidate="txtRemarks" ErrorMessage="Please Enter The Comment" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="rejectitem" CssClass="btn-primary" />
            <a href="#" class="btn btn-primary" data-dismiss="modal">Cancel</a>
        </div>
    </div>
</div>
