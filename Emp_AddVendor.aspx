<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Emp_AddVendor.aspx.cs" Inherits="Emp_AddVendor" %>

<%@ Register Src="~/Admin/UserControls/BodyVendorInformation.ascx" TagPrefix="uc1" TagName="BodyVendorInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/Vendor.js"></script>
    <uc1:BodyVendorInformation runat="server" ID="BodyVendorInformation" />
</asp:Content>
