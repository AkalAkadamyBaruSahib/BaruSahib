<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="Workshop_ViewEstMaterial.aspx.cs" Inherits="Workshop_ViewEstMaterial" %>


<%@ Register Src="~/Admin/UserControls/BodyAssignPurchaseOffice.ascx" TagPrefix="uc1" TagName="BodyAssignPurchaseOffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyAssignPurchaseOffice runat="server" ID="BodyAssignPurchaseOffice" />
</asp:Content>


