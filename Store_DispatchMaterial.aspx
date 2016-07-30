<%@ Page Title="" Language="C#" MasterPageFile="~/StoreMaster.master" AutoEventWireup="true" CodeFile="Store_DispatchMaterial.aspx.cs" Inherits="Store_DispatchMaterial" %>

<%@ Register Src="~/Admin/UserControls/BodyStoreMaterialDetail.ascx" TagPrefix="uc1" TagName="BodyStoreMaterialDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/Store.js"></script>
    <uc1:BodyStoreMaterialDetail runat="server" ID="BodyStoreMaterialDetail" />
</asp:Content>


