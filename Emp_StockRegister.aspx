<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_StockRegister.aspx.cs" Inherits="Emp_StockRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div id="content" class="span10">
         <div class="row-fluid sortable">
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><%--<i class="icon-edit"></i> Create Estimate--%></h2>
						<div class="box-icon">
							<%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					
						<form class="form-horizontal">
                            <table width="100%">
                                <tr>
                                    <td width="50%">Academy Name: <asp:Label ID="lblAcaName" runat="server"></asp:Label></td>
                                    <td width="50%">Material Name: <asp:Label ID="lblMaterialName" runat="server"></asp:Label></td>
                                </tr>
                                
                            </table>
                        </form>
                    </div>
             </div>
        
    <div class="row-fluid sortable">		
				<div class="box span12">
					<div class="box-header well" data-original-title>
						<h2><i class="icon-user"></i> <asp:Label ID="lblMatName" runat="server" text="materialName"> Stock Register</asp:Label></h2>
						<div class="box-icon">
							<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
							<a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
							<a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
						</div>
					</div>
					<div class="box-content">
						<%--<table class="table table-striped table-bordered bootstrap-datatable datatable">
						  <thead>
							  <tr>
								  <th>Sno</th>
								  <th>
                                      <table width="100%">
                                        <tr>
                                            <td colspan="4" align="center" width="100%">Particulars</td>
                                            
                                        </tr>
                                        <tr>
                                            <td width="25%">Name Of Agency.</td>
                                            <td width="25%">Challen No.</td>
                                            <td width="25%">Gate Entry No.</td>
                                            <td width="25%">Amount</td>
                                        </tr>
								      </table>

								  </th>
								  <th>Recived</th>
								  <th>Issued</th>
								  <th>Balance</th>
                                  <th>Amount</th>
                                  <th>Remarks</th>
                                  <th>Action</th>
							  </tr>
						  </thead>   
						  <tbody>
						<tr>
								  <td>1.</td>
								  <td>
                                     <table width="100%">
                                       
                                        <tr>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                            
                                        </tr>
								      </table>


								  </td>
								  <td></td>
								  <td></td>
								  <td></td>
                                  <td></td>
                                  <td></td>
                                  <td></td>
							  </tr>
                              <tr>
								  <td>2.</td>
								  <td>
                                      <table width="100%">
                                       
                                        <tr>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                        </tr>
								      </table>
								  </td>
								  <td></td>
								  <td></td>
								  <td></td>
                                  <td></td>
                                  <td></td>
                                  <td></td>
							  </tr>
                              <tr>
								  <td>3.</td>
								  <td>
                                     <table width="100%">
                                       
                                        <tr>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                        </tr>
								      </table>

								  </td>
								  <td></td>
								  <td></td>
								  <td></td>
                                  <td></td>
                                  <td></td>
                                  <td></td>
							  </tr>
                              <tr>
								  <td>4.</td>
								  <td>
                                       <table width="100%">
                                        
                                        <tr>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                        </tr>
								      </table>

								  </td>
								  <td></td>
								  <td></td>
								  <td></td>
                                  <td></td>
                                  <td></td>
                                  <td></td>
							  </tr>
						  </tbody>
					  </table>      --%>   
                        <asp:GridView ID="gvdStockRegister" CssClass="box-content" runat="server" AutoGenerateColumns="false" CellPadding="4" ShowFooter="True" OnSelectedIndexChanging="gvdStockRegister_SelectedIndexChanging" >
          <Columns>
            <%--<asp:TemplateField HeaderText="Sno.">
              <ItemTemplate>
                 <asp:Label ID="lblSno" runat="server" Text='<%# Eval("SRId") %>'></asp:Label>
              </ItemTemplate>
              <FooterTemplate>
                 <asp:TextBox ID="txt_Name" runat="server"></asp:TextBox>
               </FooterTemplate>
           </asp:TemplateField>--%>
           <asp:TemplateField HeaderText="Date" >
              <ItemTemplate>
                 <asp:Label ID="lblAgencyName" runat="server" Enabled="false" Text='<%# Eval("DateOfEntry") %>'></asp:Label>
              </ItemTemplate>
              <FooterTemplate>
                 <asp:Label ID="txt_AgencyName" runat="server"  Text='<%# System.DateTime.Now.ToString("dd/MM/yyyy") %>'>'></asp:Label>
               </FooterTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Particular">
               <ItemTemplate>
                  <asp:Label ID="lblChallenNo" runat="server" Text='<%# Eval("Particluar")%>'></asp:Label>
               </ItemTemplate>
               <FooterTemplate>
                  <asp:TextBox ID="txt_ChallenNo" runat="server" Width="100PX"></asp:TextBox>
               </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Recipt/Bill No.">
               <ItemTemplate>
                  <asp:Label ID="lblGateEntryNo" runat="server" Text='<%# Eval("ReciptAndBillNo") %>'></asp:Label>
               </ItemTemplate>
               <FooterTemplate>
                  <asp:TextBox ID="txt_GateEntryNo" runat="server" Width="100PX"></asp:TextBox>
               </FooterTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Amount">
               <ItemTemplate>
                  <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
               </ItemTemplate>
               <FooterTemplate>
                  <asp:TextBox ID="txt_Amount" runat="server" Width="60PX"></asp:TextBox>
               </FooterTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Recived Qty">
               <ItemTemplate>
                  <asp:Label ID="lblReciQty" runat="server" Text='<%# Eval("ReciQty") %>'></asp:Label>
               </ItemTemplate>
               <FooterTemplate>
                  <asp:TextBox ID="txt_ReciQty" runat="server" Width="60PX"></asp:TextBox>
               </FooterTemplate>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="Issued Qty">
               <ItemTemplate>
                  <asp:Label ID="lblIssuedQty" runat="server" Text='<%# Eval("IssuedQty") %>'></asp:Label>
               </ItemTemplate>
               <FooterTemplate>
                  <asp:TextBox ID="txt_IssuedQty" runat="server" Width="60PX"></asp:TextBox>
               </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Balance Qty">
               <ItemTemplate>
                  <asp:Label ID="lblBalanceQty" runat="server" Text='<%# Eval("BalanceQty") %>'></asp:Label>
               </ItemTemplate>
               <FooterTemplate>
                  <asp:TextBox ID="txt_BalanceQty" runat="server" Width="60PX"></asp:TextBox>
               </FooterTemplate>
            </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark">
               <ItemTemplate>
                  <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
               </ItemTemplate>
               <FooterTemplate>
                  <asp:TextBox ID="txt_Remark" runat="server" Width="250PX"></asp:TextBox>
               </FooterTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Action">
              <FooterTemplate>
               <asp:LinkButton ID="LkB1" runat="server" CommandName="Select">Add Entry</asp:LinkButton>
              </FooterTemplate>
           </asp:TemplateField>
          </Columns> 
       <%-- <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />       
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />--%>
        </asp:GridView>   
					</div>
				</div><!--/span-->
			
			</div>
        </div>
</asp:Content>

