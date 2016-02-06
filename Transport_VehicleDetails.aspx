<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_VehicleDetails.aspx.cs" Inherits="Transport_VehicleDetails" %>

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
    <div id="content" class="span10">
        <%--<asp:Button id="btnExecl" runat="server" Text="Estimate Statement" CssClass="btn btn-primary" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False" OnClick="btnExecl_Click" Font-Bold="True" ForeColor="Black" title="Click this button you get Estimate Statement Execl." data-rel="tooltip" Width="165px"/>
        <asp:Button id="btnExcel2" runat="server" Text="Estimate Material Statement" CssClass="btn btn-primary" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False" Font-Bold="True" ForeColor="Black" title="Click this button you get Estimate Statement with Material Details Execl" data-rel="tooltip" OnClick="btnExcel2_Click" Width="207px"/>--%>
        <asp:Button id="btnNonApproved" runat="server" Text="View Non Approved Vehicle(s)" CssClass="btn btn-primary" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False" Font-Bold="True" ForeColor="Black" title="Click this button you get Estimate which are uploaded by user" data-rel="tooltip" OnClick="btnNonApproved_Click" Width="235px"/>
        Select Academy:
        <asp:DropDownList ID="ddlAcademy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged" ></asp:DropDownList>
    </div>
    <div class="span10">
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
        <div id="pnlPdf" runat="server"></div>
         <div id="divEstimateDetails" runat="server"></div>	
    </div>
</asp:Content>
