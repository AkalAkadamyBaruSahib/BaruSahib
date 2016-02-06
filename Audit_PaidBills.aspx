<%@ Page Title="" Language="C#" MasterPageFile="~/AuditMaster.master" AutoEventWireup="true" CodeFile="Audit_PaidBills.aspx.cs" Inherits="Audit_PaidBills" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div id="content" class="span10">
     <div class="row-fluid sortable">		
				<div class="box span12">
                    
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> Paid Bills</h2>
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
                               

                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                <a href="Audit_BillStatusZYMWise.aspx"><span class="label label-info" style="font-size:medium;">Month & Year Wise</span></a>
                                 &nbsp;&nbsp;&nbsp;
                                <a href="Audit_WorkWiseBill.aspx"><span class="label label-info" style="font-size:medium;"> Work Wise Bills</span></a>
                                 &nbsp;&nbsp;&nbsp;
                                <a href="Audit_BillsZoneAcademyWise.aspx"><span class="label label-info" style="font-size:medium;">Academy Wise </span></a>
                               
                                
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
         </div>
</asp:Content>

