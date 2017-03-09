<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="CreateCompliantTicket.aspx.cs" Inherits="CreateCompliantTicket" %>

<%@ Register Src="~/Admin/UserControls/CreateTicket.ascx" TagPrefix="uc1" TagName="CreateTicket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="JavaScripts/CreateTickets.js"></script>
     <uc1:CreateTicket runat="server" ID="CreateTicket" />
</asp:Content>
