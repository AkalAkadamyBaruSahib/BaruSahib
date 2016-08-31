<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="WorkshopMaterial.aspx.cs" Inherits="WorkshopMaterial" %>

<%@ Register Src="~/Admin/UserControls/BodyWorkshopMaterial.ascx" TagPrefix="uc1" TagName="BodyWorkshopMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/Workshop.js"></script>
    <uc1:BodyWorkshopMaterial runat="server" ID="BodyWorkshopMaterial" />
</asp:Content>

