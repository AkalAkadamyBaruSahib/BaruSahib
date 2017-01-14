<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_MaterialDetailByPurchaseSource.aspx.cs" Inherits="Admin_WorkAllotDetailByPurchaseSource" %>

<%@ Register Src="~/Admin/UserControls/BodyWorkAllot.ascx" TagPrefix="uc1" TagName="BodyWorkAllot" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="JavaScripts/WorkAllot.js"></script>
    <uc1:BodyWorkAllot runat="server" ID="BodyWorkAllot" />
</asp:Content>

