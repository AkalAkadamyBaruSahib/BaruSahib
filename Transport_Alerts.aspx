<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_Alerts.aspx.cs" Inherits="Transport_Alerts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function ClientSideClick(myButton) {
            // Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }

            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'input') {
                // diable the button
                myButton.disabled = true;
                myButton.className = "btn btn-success";
                myButton.value = "Please Wait...";
            }
            return true;
        }
    </script>
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div id="box" runat="server" class="box-content">
                    Please click on the button to send Alerts, It is going to take some time and once alert sent we will notify you.
                    <asp:Button ID="btnDownload" runat="server" Text="Click To Send Alerts" OnClientClick="ClientSideClick(this);" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" OnClick="btnDownload_Click" Width="235px" />
                </div>
                <asp:Label ID="msg" Visible="false" runat="server" Text="Alert has been sent successfully."  Font-Bold="true" ></asp:Label>
            </div>
            
            <div style="display:none" id="progress">
                <img src="img/progress.gif" />
            </div>
        </div>
        </div>
</asp:Content>

