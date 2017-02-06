<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_RejectedBills.aspx.cs" Inherits="Admin_RejectedBills" %>

<%@ Register Src="~/Admin/UserControls/BodyRejectedBills.ascx" TagPrefix="uc1" TagName="BodyRejectedBills" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyRejectedBills runat="server" ID="BodyRejectedBills" />
</asp:Content>

