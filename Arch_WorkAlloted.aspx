<%@ Page Title="" Language="C#" MasterPageFile="~/ArchMaster.master" AutoEventWireup="true" CodeFile="Arch_WorkAlloted.aspx.cs" Inherits="Arch_WorkAlloted" %>

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
      <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
  <div id="Div1" class="span10">
     <div class="row-fluid sortable">		
				<div class="box span12">
                    
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> WorK Allot Search</h2>
						<div class="box-icon">
							
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
                        

						<div id="divDesigDetails" runat="server">
                        <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                          <ContentTemplate>  --%>       
                         <table border="0" WIDTH="100%">  <%--class="table table-striped table-bordered bootstrap-datatable datatable"--%>
                         <tbody> 
                        <tr>
                            <td colspan="23">
                                <asp:DropDownList runat="server" ID="ddlSelect" OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Select Serach Type</asp:ListItem>
                                    <asp:ListItem Value="1">Academy Wise</asp:ListItem>
                                    <asp:ListItem Value="2">All Work Allot</asp:ListItem>
                                </asp:DropDownList>
                                 &nbsp;&nbsp;&nbsp;
                                 <asp:Panel runat="server" ID="pnlAcademy" Visible="false">
                                <asp:DropDownList ID="ddlAcademy" AutoPostBack="true" runat="server"  OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged"></asp:DropDownList>
                                     </asp:Panel>
                                 &nbsp;&nbsp;&nbsp;
                               
                            </td>
                            <td>
                                <asp:Button id="btnExecl" runat="server" Text="Work Allot Excel Download" CssClass="btn btn-primary" OnClick="btnExecl_Click" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False" />
                            </td>
                        </tr>
             
                             
                         </tbody> 
                         </table> 
                              <%--</ContentTemplate>

                             </asp:UpdatePanel>--%>
						</div>            
					</div>
				</div><!--/span-->
			
			</div>
           <div id="divDrawingView" runat="server"></div>
			<div id="divAllDrawingView" runat="server"></div>           
         </div>
</asp:Content>

