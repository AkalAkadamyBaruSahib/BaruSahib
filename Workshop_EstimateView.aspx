<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="Workshop_EstimateView.aspx.cs" Inherits="Workshop_EstimateView" %>

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
           <div class="row-fluid sortable" runat="server" id="divExcel">	
        
       
        	
				<div class="box span12">
                    
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> Estimate </h2>
						<div class="box-icon">
							
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
                        
                         <div ID="Div1" runat="server"></div>
						<div id="divDesigDetails" runat="server">
                     
                         <table border="0" WIDTH="100%"> 
                         <tbody> 
                        <tr>
                            <td>
                                <asp:Button id="btnExecl" runat="server" Text="Estimate Statement" CssClass="btn btn-primary"  onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False" OnClick="btnExecl_Click"/>
                                <asp:Button id="btnExcel1" runat="server" Text="Estimate Material Statement" CssClass="btn btn-primary" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False" OnClick="btnExcel1_Click"/>
                            </td>
                        </tr>
             
                             
                         </tbody> 
                         </table> 
                           
						</div>            
					</div>
				</div>
			
			</div>
        <div ID="pnlPdf" runat="server"></div>
         <div id="divEstimateDetails" runat="server"></div>	
    </div>
</asp:Content>

