<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_ParticularEstimateView.aspx.cs" Inherits="Transport_ParticularEstimateView" %>

<%@ Register Src="~/Admin/UserControls/ParticularEstimateView.ascx" TagPrefix="uc1" TagName="ParticularEstimateView" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ParticularEstimateView runat="server" ID="ParticularEstimateView" />
</asp:Content>

