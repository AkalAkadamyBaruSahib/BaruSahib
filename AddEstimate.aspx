<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master"   AutoEventWireup="true" CodeFile="AddEstimate.aspx.cs" Inherits="AddEstimate" %>

<%@ Register Src="~/Admin/UserControls/UploadEstimate.ascx" TagPrefix="uc1" TagName="UploadEstimate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/EstimateUpload.js"></script>
    <uc1:UploadEstimate runat="server" ID="UploadEstimate" />
</asp:Content>

