<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="Workshop_WorkWiseBill.aspx.cs" Inherits="Workshop_WorkWiseBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
     <div id="content" class="span10">
     <div class="row-fluid sortable">		
				<div class="box span12">
                    
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> Academy Wise Bill Details</h2>
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
                            <td >
                                Select Work Allot: <asp:DropDownList runat="server" ID="ddlWork" AutoPostBack="true" OnSelectedIndexChanged="ddlWork_SelectedIndexChanged"></asp:DropDownList>

                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                <a href="Workshop_BillStatus.aspx"><span class="label label-info" style="font-size:medium;">Month & Year Wise</span></a>
                                 &nbsp;&nbsp;&nbsp;
                                <a href="Workshop_PaidBills.aspx"><span class="label label-info" style="font-size:medium;">Paid Bills</span></a>
                                 &nbsp;&nbsp;&nbsp;
                                <a href="Workshop_ZoneAcaWiseBill.aspx"><span class="label label-info" style="font-size:medium;">Academy Wise </span></a>
                               
                                
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

