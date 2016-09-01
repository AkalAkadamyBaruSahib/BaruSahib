<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="EstimateSearch.aspx.cs" Inherits="EstimateSearch" %>

<%@ Register Src="~/Admin/UserControls/BodyEstimateSearch.ascx" TagPrefix="uc1" TagName="BodyEstimateSearch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyEstimateSearch runat="server" id="BodyEstimateSearch" />
    
</asp:Content>



