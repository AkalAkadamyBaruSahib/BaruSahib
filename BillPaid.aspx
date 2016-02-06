<%@ Page Title="" Language="C#" MasterPageFile="~/BillStatus.master" AutoEventWireup="true" CodeFile="BillPaid.aspx.cs" Inherits="BillPaid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
     <div id="content" class="span10">
          <asp:Panel ID="pnlBillMonthDetails" runat="server" Visible="false">
             <div id="divAcademyDetails" runat="server"></div>
         </asp:Panel>
     </div>
</asp:Content>

