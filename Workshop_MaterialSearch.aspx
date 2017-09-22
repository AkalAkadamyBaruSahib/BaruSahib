<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="Workshop_MaterialSearch.aspx.cs" Inherits="Workshop_MaterialSearch" %>

<%@ Register Src="~/Admin/UserControls/BodyMaterialSearch.ascx" TagPrefix="uc1" TagName="BodyMaterialSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/MaterialSearch.js"></script>
    <uc1:BodyMaterialSearch runat="server" ID="BodyMaterialSearch" />
</asp:Content>



