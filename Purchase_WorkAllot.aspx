<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="Purchase_WorkAllot.aspx.cs" Inherits="Purchase_WorkAllot" %>

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
    <asp:Label runat="server" Visible="false" ID="lblUser"></asp:Label>
     <div id="content" class="span10">
        <div class="row-fluid sortable">		
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> Work Allot Details</h2>
						<div class="box-icon">
							<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<div id="divAcademyDetails" runat="server"></div>            
					</div>
				</div><!--/span-->
			
			</div>

        </div>
</asp:Content>

