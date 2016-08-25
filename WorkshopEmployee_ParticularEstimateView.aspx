<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="WorkshopEmployee_ParticularEstimateView.aspx.cs" Inherits="WorkshopEmployee_ParticularEstimateView" %>


<%@ Register Src="~/Admin/UserControls/ParticularEstimateView.ascx" TagPrefix="uc1" TagName="ParticularEstimateView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ParticularEstimateView runat="server" ID="ParticularEstimateView" />
</asp:Content>

