<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_UploadEstimate.aspx.cs" Inherits="Transport_UploadEstimate" %>

<%@ Register Src="~/Admin/UserControls/UploadEstimate.ascx" TagPrefix="uc1" TagName="UploadEstimate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/EstimateUpload.js"></script>
    <uc1:UploadEstimate runat="server" ID="UploadEstimate" />
</asp:Content>

