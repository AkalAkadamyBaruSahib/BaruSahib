<%@ Page Title="" Language="C#" MasterPageFile="~/BillStatus.master" AutoEventWireup="true" CodeFile="BillStatusAcaZoneWise.aspx.cs" Inherits="BillStatusAcaZoneWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
     <div id="content" class="span10">
         <div class="row-fluid sortable" runat="server" id="divZoneAcaWise">		
				<div class="box span12" >
                    
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> Zone Academy Wise Bill Details</h2>
						<div class="box-icon">
							<%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
                        

						<div id="div2" runat="server">
                        <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                          <ContentTemplate>  --%>     
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>  
                         <table border="0" WIDTH="100%">  <%--class="table table-striped table-bordered bootstrap-datatable datatable"--%>
                         <tbody> 
                            
                        <tr>

                            <td >
                                Select Zone: <asp:DropDownList ID="ddlZone" runat="server" Width="175px" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>

                            </td>
                             <td >
                                Select Academy: <asp:DropDownList ID="ddlAcademy" runat="server" Width="175px" ></asp:DropDownList>

                            </td>
                            <td>
                                <%--<asp:Button id="btnAcaZonSrch" runat="server" Text="Zone/Academy Wise Search" OnClick="btnAcaZonSrch_Click" CssClass="btn btn-primary"/>--%>
                                <asp:LinkButton ID="lbAcaZonSrch" runat="server" OnClick="lbAcaZonSrch_Click">Zone/Academy Wise Search</asp:LinkButton>
                            </td>
                        </tr>
                            
                             
             
                              
                         </tbody> 
                         </table> 
                                                    </ContentTemplate>
                                </asp:UpdatePanel>
                              <%--</ContentTemplate>

                             </asp:UpdatePanel>--%>
						</div>            
					</div>
				</div><!--/span-->
			
			</div>
           <asp:Panel ID="pnlBillMonthDetails" runat="server" Visible="false">
             <div id="divAcademyDetails" runat="server"></div>
         </asp:Panel>
     </div>
</asp:Content>

