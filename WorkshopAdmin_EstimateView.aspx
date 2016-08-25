<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="WorkshopAdmin_EstimateView.aspx.cs" Inherits="WorkshopAdmin_EstimateView" %>

<%@ Register Src="~/Admin/UserControls/EstimateView.ascx" TagPrefix="uc1" TagName="EstimateView" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:EstimateView runat="server" ID="EstimateView" />
</asp:Content>


