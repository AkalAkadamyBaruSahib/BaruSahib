<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="Worksho_MaterialToBeDispatch.aspx.cs" Inherits="Worksho_MaterialToBeDispatch" %>

<%@ Register Src="~/Admin/UserControls/BodyPurchaseMaterialDetails.ascx" TagPrefix="uc1" TagName="BodyPurchaseMaterialDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/WorkshopDispatch.js"></script>
    <uc1:BodyPurchaseMaterialDetails runat="server" id="BodyPurchaseMaterialDetails" />
</asp:Content>

