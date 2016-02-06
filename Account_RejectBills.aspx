<%@ Page Title="" Language="C#" MasterPageFile="~/AccountMaster.master" AutoEventWireup="true" CodeFile="Account_RejectBills.aspx.cs" Inherits="Account_RejectBills" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
     <div id="content" class="span10">
          <asp:Panel ID="pnlBillMonthDetails" runat="server" Visible="false">
             <div id="divAcademyDetails" runat="server"></div>
         </asp:Panel>
     </div>
</asp:Content>