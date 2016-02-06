<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="Worksho_MaterialToBeDispatch.aspx.cs" Inherits="Worksho_MaterialToBeDispatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <%--         
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>  
    
         
         <div class="box span10"> 
         <div class="box-header well" data-original-title> 
         <h2><i class="icon-user"></i> Dispatch Details</h2> 
         <div class="box-icon"> 
         <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a> 
         <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a> 
         <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a> 
         </div> 
         </div> 
         <div class="box-content"> 
             <div id="divEstimateDetails" runat="server"></div>
         </div> 
         </div>  
         --%>
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
        <div class="row-fluid sortable">		
				<div class="box span12">
                    
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> Material dispatch </h2>
						<div class="box-icon">
							
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
                        

						<div id="divDesigDetails" runat="server">
                        
                         <table border="0" WIDTH="100%"> 
                         <tbody> 
                        <tr>
                          
                            <td>
                                <asp:Button id="btnExecl" runat="server" Text="Dispatch Excel Download" CssClass="btn btn-primary"  onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False"  OnClick="btnExecl_Click"  />
                            </td>
                        </tr>
             
                             
                         </tbody> 
                         </table> 
                             
						</div>            
					</div>
				</div><!--/span-->
			
			</div>
          <div ID="pnlPdf" runat="server"></div>
        <div id="divEstimateDetails" runat="server"></div>
    </div> 
   
     
</asp:Content>
