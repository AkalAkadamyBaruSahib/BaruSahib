<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_ViewMatDispatchStatus.aspx.cs" Inherits="Emp_ViewMatDispatchStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>   
         <div class="box span10"> 
         <div class="box-header well" data-original-title> 
         <h2><i class="icon-user"></i> Material Dispatch Status for  | <asp:Label ID="lblAcademy" runat="server"></asp:Label> | <asp:Label ID="lblEstId" runat="server"></asp:Label></h2> 
         <div class="box-icon"> 
         <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a> 
         <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a> 
         <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a> 
         </div> 
         </div> 
         <div class="box-content"> 
         <asp:GridView runat="server" AutoGenerateColumns="false" DataKeyNames="EstId" ID="gvMaterailDetailForPurchase" class="table table-striped table-bordered bootstrap-datatable datatable">
             <Columns>
                 <asp:TemplateField HeaderText="Material Name">
                     <ItemTemplate>
                         <asp:Label runat="server" ID="lblMatName" Text='<%# Bind("MatName") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderText="Unit">
                     <ItemTemplate>
                         <asp:Label runat="server" ID="lblUnitName" Text='<%# Bind("UnitName") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Quantity">
                     <ItemTemplate>
                         <asp:Label runat="server" ID="lblQty" Text='<%# Bind("Qty") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderText="Tantitive Date">
                     <ItemTemplate>
                         <asp:TextBox runat="server" ID="txtTatDate" CssClass="input-xlarge datepicker" Width="85px"></asp:TextBox>
                         <%--<a class="btn btn-setting btn-round" href="#Purchase_ViewEstMaterial.aspx?EstId='<%# Bind("EstId")%>'"><span class="label label-important" >Enter Tantitive Date</span></a>--%>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Dispatch Date">
                     <ItemTemplate>
                         <asp:TextBox runat="server" ID="txtDispatchDate" CssClass="input-xlarge datepicker" Width="85px"></asp:TextBox>
                         <%--<a class="btn btn-setting btn-round" href="#Purchase_ViewEstMaterial.aspx?EstId='<%# Bind("EstId")%>'"><span class="label label-important" >Enter Dispatch Date</span></a>--%>
                     </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderText="Remark">
                     <ItemTemplate>
                         <asp:TextBox runat="server" ID="txtRemark" ></asp:TextBox>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
         </asp:GridView>
         </div> 
         </div>  
</asp:Content>

