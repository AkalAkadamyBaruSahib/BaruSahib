<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="WorkshopSearchEstimate.aspx.cs" Inherits="WorkshopSearchEstimate" %>

<%@ Register Src="~/Admin/UserControls/BodyEstimateSearch.ascx" TagPrefix="uc1" TagName="BodyEstimateSearch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyEstimateSearch runat="server" ID="BodyEstimateSearch" />
</asp:Content>

