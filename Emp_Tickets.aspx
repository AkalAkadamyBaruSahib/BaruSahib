<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_Tickets.aspx.cs" Inherits="Emp_Tickets" %>

<%@ Register Src="~/Admin/UserControls/CreateTicket.ascx" TagPrefix="uc1" TagName="CreateTicket" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:CreateTicket runat="server" ID="CreateTicket" />
</asp:Content>

