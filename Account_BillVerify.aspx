<%@ Page Title="" Language="C#" MasterPageFile="~/AccountMaster.master" AutoEventWireup="true" CodeFile="Account_BillVerify.aspx.cs" Inherits="Account_BillVerify" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function confirmation() {
            if (confirm('ARE YOU SURE FOR THIS ACTION?')) {
                return true;
            } else {
                return false;
            }
        }
    </script> 
     
       <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>   
         <div class="box span10"> 
         <div class="box-header well" data-original-title> 
         <h2><i class="icon-user"></i> Bill Details for <b><asp:Label ID="lblAcaName" runat="server"></asp:Label></b></h2> 
         <div class="box-icon"> 
         <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a> 
         <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a> 
         <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a> 
         </div> 
         </div> 
         <div class="box-content"> 
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnlBillDetails">
         <asp:GridView runat="server" AutoGenerateColumns="false" DataKeyNames="AcaId" ID="gvBillDetails" class="table table-striped table-bordered bootstrap-datatable datatable" OnRowDataBound="gvBillDetails_RowDataBound" >
             <Columns>
                 <asp:BoundField DataField="SubBillId" HeaderText="Bill No"  />
                 <asp:BoundField DataField="AgencyName" HeaderText="Agency Name"  />
                 <asp:BoundField DataField="TotalAmount" HeaderText="Amount"  />
                 <%--<asp:TemplateField HeaderText="Bill No">
                     <ItemTemplate>
                         <asp:Label runat="server" ID="lblBillId" Text='<%# Bind("SubBillId") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Agency Name">
                     <ItemTemplate>
                         <asp:Label runat="server" ID="lblAgencyName" Text='<%# Bind("AgencyName") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderText="Amount">
                     <ItemTemplate>
                         <asp:Label runat="server" ID="lblAmount" Text='<%# Bind("TotalAmount") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>--%>
                 <asp:TemplateField HeaderText="Payment Mode">
                     <ItemTemplate>
                         <asp:DropDownList runat="server" ID="ddlPayMode" Width="130px"></asp:DropDownList>
                     </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderText="Payment Details">
                     <ItemTemplate>
                         <asp:TextBox runat="server" ID="txtPaymentDetails" Width="130px"></asp:TextBox>
                         <%--<a class="btn btn-setting btn-round" href="#Purchase_ViewEstMaterial.aspx?EstId='<%# Bind("EstId")%>'"><span class="label label-important" >Enter Tantitive Date</span></a>--%>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Remark">
                     <ItemTemplate>
                         <asp:TextBox runat="server" ID="txtRemark" Width="200px"></asp:TextBox>
                     </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderText="Action">
                     <ItemTemplate>
                        <asp:Button runat="server"  Text="Pay for Bill" CausesValidation="false" ID="btnPay" CommandArgument='<%# Bind("SubBillId") %>' CommandName="PayBtn"  OnClick="btnPay_Click1" BackColor="Green" ForeColor="White" OnClientClick="return confirmation();"/>
                          <asp:Button runat="server"  Text="Reject" CausesValidation="false" ID="btnRej" CommandArgument='<%# Bind("SubBillId") %>' CommandName="RejBtn" BackColor="Red" ForeColor="White" OnClick="btnRej_Click" OnClientClick="return confirmation();"/>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
         </asp:GridView>
                    </asp:Panel>
                <h3><asp:Label runat="server" ID="lblBillDetails" Visible="false" Text="No Bill Details of this Acadeny" ForeColor="Red"></asp:Label></h3>
            </ContentTemplate>
        </asp:UpdatePanel>
         </div> 
             <%--<div class="form-actions">
							 
                                <asp:Button id="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                                <asp:Button id="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" Visible="false"  />
                                <asp:Button id="btnCl" runat="server" Text="Cancel" CssClass="btn"  />
							</div>--%>
         </div>  
</asp:Content>

