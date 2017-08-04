<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_MaterialSearch.aspx.cs" Inherits="Admin_MaterialSearch" %>

<%@ Register Src="~/Admin/UserControls/BodyMaterialSearch.ascx" TagPrefix="uc1" TagName="BodyMaterialSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/MaterialSearch.js"></script>
    <uc1:BodyMaterialSearch runat="server" ID="BodyMaterialSearch" />
</asp:Content>

