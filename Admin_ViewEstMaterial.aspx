<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_ViewEstMaterial.aspx.cs" Inherits="Admin_ViewEstMaterial" %>

<%@ Register Src="~/Admin/UserControls/BodyAssignPurchaseOffice.ascx" TagPrefix="uc1" TagName="BodyAssignPurchaseOffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyAssignPurchaseOffice runat="server" ID="BodyAssignPurchaseOffice" />
</asp:Content>

