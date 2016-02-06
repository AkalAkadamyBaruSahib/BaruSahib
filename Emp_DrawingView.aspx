<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_DrawingView.aspx.cs" Inherits="Emp_DrawingView" %>
<%@ Register Src="~/Admin/UserControls/EmailTemplate.ascx" TagPrefix="uc1" TagName="EmailTemplate" %>

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
						<h2><i class="icon-user"></i> Drawing Search</h2>
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
                               Drawing Type: <asp:DropDownList ID="ddlDwgType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDwgType_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button id="btnExecl" runat="server" Text="Drawing Excel Download" CssClass="btn btn-primary"  onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False" OnClick="btnExecl_Click"/>
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
        <div id="divEmailDialog" style="display:none">
        <uc1:EmailTemplate runat="server" ID="EmailTemplate" />
    </div>
    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
    <div id="content" class="span10">
      
				            <table width="100%">
                                <tr><td></td></tr>
                                <tr>
                                    <td align="center">Drawing Type: <asp:DropDownList ID="ddlDwgType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDwgType_SelectedIndexChanged"></asp:DropDownList></td>
                                    
                                </tr>
                                 <tr><td></td></tr>
                            </table>
                                                   
    <div class="row-fluid sortable">		
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> <asp:Label ID="lblMatName" runat="server" text="materialName"> Drawing Details</asp:Label></h2>
						<div class="box-icon">
							<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
                        
					</div>
				</div><!--/span-->
			
			</div>
        </div> 

                                                </ContentTemplate>
          </asp:UpdatePanel>--%>
</asp:Content>

