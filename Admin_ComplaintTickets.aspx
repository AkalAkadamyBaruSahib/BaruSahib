﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_ComplaintTickets.aspx.cs" Inherits="Admin_ComplaintTickets" %>

<%@ Register Src="~/Admin/UserControls/CreateTicket.ascx" TagPrefix="uc1" TagName="CreateTicket" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="JavaScripts/CreateTickets.js"></script>
    <uc1:CreateTicket runat="server" ID="CreateTicket" />
</asp:Content>

