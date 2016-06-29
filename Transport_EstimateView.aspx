<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_EstimateView.aspx.cs" Inherits="Transport_EstimateView" %>

<%@ Register Src="~/Admin/UserControls/EstimateView.ascx" TagPrefix="uc1" TagName="EstimateView" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:EstimateView runat="server" ID="EstimateView" />
</asp:Content>

