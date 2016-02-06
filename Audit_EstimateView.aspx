<%@ Page Title="" Language="C#" MasterPageFile="~/AuditMaster.master" AutoEventWireup="true" CodeFile="Audit_EstimateView.aspx.cs" Inherits="Audit_EstimateView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
     <div id="Div1" class="span10">
        <asp:Button id="btnExecl" runat="server" Text="Estimate Statement" OnClick="btnExecl_Click" CssClass="btn btn-primary" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"  Width="220px" Height="40px" Font-Bold="True" Font-Size="16pt" ForeColor="Black" title="Click this button you get Estimate Statement Execl." data-rel="tooltip"/>
        <asp:Button id="btnExcel1" runat="server" Text="Estimate Material Statement" OnClick="btnExcel1_Click" CssClass="btn btn-primary" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"   Width="320px" Height="40px" Font-Bold="True" Font-Size="16pt" ForeColor="Black" title="Click this button you get Estimate Statement with Material Details Execl" data-rel="tooltip" />
    </div>
    <div id="content" class="span10">
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
         <div ID="pnlPdf" runat="server"></div>
         <div id="divEstimateDetails" runat="server"></div>	
         <div id="divEstimateDetails1" runat="server"></div>	
    </div>
</asp:Content>
