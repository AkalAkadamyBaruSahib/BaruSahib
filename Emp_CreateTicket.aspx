﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AcademicMaster.master" AutoEventWireup="true" CodeFile="Emp_CreateTicket.aspx.cs" Inherits="Emp_CreateTicket" %>

<%@ Register Src="~/Admin/UserControls/CreateTicket.ascx" TagPrefix="uc1" TagName="CreateTicket" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="JavaScripts/CreateTickets.js"></script>
     <uc1:CreateTicket runat="server" ID="CreateTicket" />
</asp:Content>

