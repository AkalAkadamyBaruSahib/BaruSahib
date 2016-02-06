<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_ComplaintTickets.aspx.cs" Inherits="Admin_ComplaintTickets" %>

<%@ Register Src="~/Admin/UserControls/CreateTicket.ascx" TagPrefix="uc1" TagName="CreateTicket" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:CreateTicket runat="server" ID="CreateTicket" />
</asp:Content>

