<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AdminEstimateSearch.aspx.cs" Inherits="AdminEstimateSearch" %>

<%@ Register Src="~/Admin/UserControls/BodyEstimateSearch.ascx" TagPrefix="uc1" TagName="BodyEstimateSearch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyEstimateSearch runat="server" id="BodyEstimateSearch" />
</asp:Content>

