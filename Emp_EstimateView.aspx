<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_EstimateView.aspx.cs" Inherits="Emp_EstimateView" %>

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
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
        <div ID="pnlPdf" runat="server"></div>
         <div id="divEstimateDetails" runat="server"></div>	
    </div>
</asp:Content>

