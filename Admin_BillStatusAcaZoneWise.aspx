<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_BillStatusAcaZoneWise.aspx.cs" Inherits="Admin_BillStatusAcaZoneWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
     <div id="content" class="span10">
         <div class="row-fluid sortable" runat="server" id="divSerch">		
				<div class="box span12" >
                    
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> Select Serach Type</h2>
						<div class="box-icon">
							<%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
                        

						<div id="div4" runat="server">
                        <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                          <ContentTemplate>  --%>       
                         <table border="0" WIDTH="100%">  <%--class="table table-striped table-bordered bootstrap-datatable datatable"--%>
                         <tbody> 
                        <tr>
                            <td >
                               <asp:LinkButton runat="server" ID="lbMothWise" OnClick="lbMothWise_Click">[Month]</asp:LinkButton>

                            </td>
                             <td >
                                <asp:LinkButton runat="server" ID="lbZoneAca" >[Zone and Academy]</asp:LinkButton>

                            </td>
                             <td >
                               <asp:LinkButton runat="server" ID="lbAlloted" >[Alloted Work]</asp:LinkButton>

                            </td>
                            <td >
                                <asp:LinkButton runat="server" ID="lblPaidBill">[Paid Bill]</asp:LinkButton>

                            </td>
                            <td >
                                <asp:LinkButton runat="server" ID="lbRejectBill">[Rejected Bill]</asp:LinkButton>

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
                            
                        </tr>
                             <tr>
                                  <td colspan="2" align="center">
                               <asp:Button id="btnAcaZonSrch" runat="server" Text="Zone/Academy Wise Search" OnClick="btnAcaZonSrch_Click"/>

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
     <div class="row-fluid sortable" runat="server" id="divWorkWise">		
				<div class="box span12" >
                    
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> Alloted Work Wise Bill Details</h2>
						<div class="box-icon">
							<%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
                        

						<div id="div3" runat="server">
                        <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                          <ContentTemplate>  --%>       
                         <table border="0" WIDTH="100%">  <%--class="table table-striped table-bordered bootstrap-datatable datatable"--%>
                         <tbody> 
                        <tr>
                            <td >
                                Select Alloted Work: <asp:DropDownList ID="ddlWorkAlloted" runat="server" Width="125px"></asp:DropDownList>

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
        <asp:Panel ID="pnlBillMonthDetails" runat="server" Visible="false">
             <div id="divAcademyDetails" runat="server"></div>
         </asp:Panel>
         <asp:Panel ID="pnlBillMonthDetails1" runat="server" Visible="false">
             <div id="divAcademyDetails1" runat="server"></div>
         </asp:Panel>
         <%-- <asp:Panel ID="pnlAcademyZoneDetails" runat="server" Visible="false">
             <div id="divAcademyZoneDetails" runat="server"></div>
         </asp:Panel>--%>
         </div>
    
</asp:Content>
