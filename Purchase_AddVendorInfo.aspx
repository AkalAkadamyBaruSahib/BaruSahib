<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Purchase_AddVendorInfo.aspx.cs" Inherits="Purchase_AddVendorInfo" %>

<%@ Register Src="~/Admin/UserControls/BodyVendorInformation.ascx" TagPrefix="uc1" TagName="BodyVendorInformation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/Vendor.js"></script>
    <uc1:BodyVendorInformation runat="server" ID="BodyVendorInformation" />
</asp:Content>

