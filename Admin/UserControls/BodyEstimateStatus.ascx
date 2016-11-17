<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyEstimateStatus.ascx.cs" Inherits="Admin_UserControls_BodyEstimateStatus" %>
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

    var _Page_tabToSelect = 0;
    $(function () {
        var $tabs = $("#tabs").tabs();
        $("#tabs").tabs({
            selected: _Page_tabToSelect
        });

    });

</script>
<div id="content" class="span10">
    <asp:HiddenField ID="hdnEstID" runat="server" />
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div class="row-fluid sortable">
        <div class="box span12">
            <div id="tabs" class="bs-component">
                <ul>
                    <li><a href="#divPendingEstimate">Pending Estimates</a></li>
                    <li><a href="#divClosedEstimate">Closed Estimates</a></li>
                </ul>
                <div id="divPendingEstimate">
                    <asp:DropDownList ID="ddlAcademy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged"></asp:DropDownList>
                    <div id="divEstimateDetails" runat="server"></div>
                </div>
                <div id="divClosedEstimate">
                    <asp:DropDownList ID="ddlClosedAcademies" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClosedAcademies_SelectedIndexChanged"></asp:DropDownList>
                    <div id="divCloseEstimateDetails" runat="server"></div>
                </div>
            </div>
        </div>
    </div>

</div>






