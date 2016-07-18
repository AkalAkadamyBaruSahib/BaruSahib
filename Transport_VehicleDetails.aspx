<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_VehicleDetails.aspx.cs" Inherits="Transport_VehicleDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        function openModelPopUp(number) {
            $("div[id*='divEstimateDetails']").hide();
            var url = "http://fleetvts.com/current_vehicle_location.php?locate_vehicle=" + number;
            $("#iframeVehicleLocation").attr("src", url);
            $("#divTransportGps").show();
            $("#divbtnapprved").hide();
        }
    </script>

    <div id="content" class="span10">
       
        <div id="divbtnapprved">
        <asp:Button ID="btnNonApproved" runat="server" Text="View Non Approved Vehicle(s)" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" Font-Bold="True" ForeColor="Black" title="Click this button you get Estimate which are uploaded by user" data-rel="tooltip" OnClick="btnNonApproved_Click" Width="235px" />
         Select Academy:
        <asp:DropDownList ID="ddlAcademy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged"></asp:DropDownList>
        Select Transport Type:
        <asp:DropDownList ID="ddlTransportType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTransportType_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div id="divTransportGps" style="width: 1100px; height: 520px; display: none;">
            <div class="box-header well" data-original-title>
                 <asp:Button ID="btnClose" Text="x" runat="server" CssClass="btn btn-primary" style="float: right;" />
                <h2><i><img src="img/abc.jpg"/></i>Vehicle Location</h2>

            </div>
            <div>
                <iframe id="iframeVehicleLocation" style="width: 1100px; height: 500px; border: 1.5pt inset; vertical-align: top;"></iframe>
            </div>
        </div>

    </div>
    <div id="divdetail" class="span10">
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
        <div id="pnlPdf" runat="server"></div>
        <div id="divEstimateDetails" runat="server"></div>

    </div>



</asp:Content>
