<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_MatDispatchStatus.aspx.cs" Inherits="Admin_MatDispatchStatus" %>

<%@ Register Src="~/Admin/UserControls/BodyPurchaseMaterialDetails.ascx" TagPrefix="uc1" TagName="BodyPurchaseMaterialDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/PurchaseButtonToggle.js"></script>
    <uc1:BodyPurchaseMaterialDetails runat="server" id="BodyPurchaseMaterialDetails" />
</asp:Content>


