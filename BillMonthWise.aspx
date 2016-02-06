<%@ Page Title="" Language="C#" MasterPageFile="~/BillStatus.master" AutoEventWireup="true" CodeFile="BillMonthWise.aspx.cs" Inherits="BillMonthWise" %>

<%-- Add content controls here --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
     <div id="content" class="span10">
         <div class="row-fluid sortable" runat="server" id="divMonthWise">		
				<div class="box span12" >
                    
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> Year and Month Wise Bill Details</h2>
						<div class="box-icon">
							<%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
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
                                Select Year: <asp:DropDownList ID="ddlYear" runat="server" Width="125px">
                                    <asp:ListItem Value="0">SELECT YEAR</asp:ListItem>
                                     <asp:ListItem Value="2013">2013</asp:ListItem>
                                     <asp:ListItem Value="2014">2014</asp:ListItem>
                                     <asp:ListItem Value="2015">2015</asp:ListItem>
                                     <asp:ListItem Value="2016">2016</asp:ListItem>
                                     <asp:ListItem Value="2017">2017</asp:ListItem>
                                     <asp:ListItem Value="2018">2018</asp:ListItem>
                                     <asp:ListItem Value="2019">2019</asp:ListItem>
                                     <asp:ListItem Value="2020">2020</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
             
                             <tr> 
                
                                 <td><asp:LinkButton runat="server" ID="lbJanBill" OnClick="lbJanBill_Click">JAN(<asp:Label ID="lblJanBillcount" runat="server" Text="10" ForeColor="Red"></asp:Label>)</asp:LinkButton></td>
                                 <td></td>
                                 <td><asp:LinkButton runat="server" ID="lbFebBill" OnClick="lbFebBill_Click">FEB(<asp:Label ID="lblFeBillCount" runat="server" Text="10" ForeColor="Red"></asp:Label>)</asp:LinkButton></td> 
                                 <td></td>
                                 <td><asp:LinkButton runat="server" ID="lbMarchBill" OnClick="lbMarchBill_Click">MARCH(<asp:Label ID="lblMarchBillCount" runat="server" Text="10" ForeColor="Red"></asp:Label>)</asp:LinkButton></td> 
                                 <td></td>
                                 <td><asp:LinkButton runat="server" ID="lbAprBill" OnClick="lbAprBill_Click">APRIL(<asp:Label ID="lblAprBillCount" runat="server" Text="10" ForeColor="Red"></asp:Label>)</asp:LinkButton></td> 
                                 <td></td>
                                 <td><asp:LinkButton runat="server" ID="lbMayBill" OnClick="lbMayBill_Click">MAY(<asp:Label ID="lblMayBillCount" runat="server" Text="10" ForeColor="Red"></asp:Label>)</asp:LinkButton></td> 
                                 <td></td>
                                 <td><asp:LinkButton runat="server" ID="lbJuneBill" OnClick="lbJuneBill_Click">JUNE(<asp:Label ID="lblJuneBillCount" runat="server" Text="10" ForeColor="Red"></asp:Label>)</asp:LinkButton></td> 
                                 <td></td>
                                 <td><asp:LinkButton runat="server" ID="lbJulyBill" OnClick="lbJulyBill_Click">JULY(<asp:Label ID="lblJulyBillCount" runat="server" Text="10" ForeColor="Red"></asp:Label>)</asp:LinkButton></td> 
                                 <td></td>
                                 <td><asp:LinkButton runat="server" ID="lbAugBill" OnClick="lbAugBill_Click">AUG(<asp:Label ID="lbAugBillCount" runat="server" Text="10" ForeColor="Red"></asp:Label>)</asp:LinkButton></td> 
                                 <td></td>             
                                 <td><asp:LinkButton runat="server" ID="LBsPEbILL" OnClick="LBsPEbILL_Click">SEP(<asp:Label ID="lblSepBillCount" runat="server" Text="10" ForeColor="Red"></asp:Label>)</asp:LinkButton></td> 
                                 <td></td>
                                 <td><asp:LinkButton runat="server" ID="lbOctBill" OnClick="lbOctBill_Click">OCT(<asp:Label ID="lblOctBillCount" runat="server" Text="10" ForeColor="Red"></asp:Label>)</asp:LinkButton></td> 
                                 <td></td>
                                 <td><asp:LinkButton runat="server" ID="lbNovBill" OnClick="lbNovBill_Click">NOV(<asp:Label ID="lblNovBillCount" runat="server" Text="10" ForeColor="Red"></asp:Label>)</asp:LinkButton></td> 
                                 <td></td>
                                 <td><asp:LinkButton runat="server" ID="lbDecBill" OnClick="lbDecBill_Click">DEC(<asp:Label ID="lblDecBillCount" runat="server" Text="10" ForeColor="Red"></asp:Label>)</asp:LinkButton></td> 

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