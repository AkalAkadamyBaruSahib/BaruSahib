<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_AllBillDetails.aspx.cs" Inherits="Admin_AllBillDetails" %>

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
        <asp:Button id="btnExecl" runat="server" Text="Bill Details Excel" CssClass="btn btn-primary" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"  Width="220px" Height="40px" Font-Bold="True" Font-Size="16pt" ForeColor="Black" title="Click this button you get Estimate Statement Execl." data-rel="tooltip" OnClick="btnExecl_Click"/>
    </div>
     <div id="content" class="span10">
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div id="divAcademyDetails" runat="server"></div>
         </div>
</asp:Content>

