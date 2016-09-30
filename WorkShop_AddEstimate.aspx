<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="WorkShop_AddEstimate.aspx.cs" Inherits="WorkShop_AddEstimate" %>

<%@ Register Src="~/Admin/UserControls/BodyNewEstimateFunctionality.ascx" TagPrefix="uc1" TagName="BodyNewEstimateFunctionality" %>


<%--<%@ Register Src="~/Admin/UserControls/UploadEstimate.ascx" TagPrefix="uc1" TagName="UploadEstimate" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyNewEstimateFunctionality runat="server" ID="BodyNewEstimateFunctionality" />
    <script src="JavaScripts/NewEstimateFunctionality.js"></script>
  <%--     <script src="JavaScripts/EstimateUpload.js"></script>
    <uc1:UploadEstimate runat="server" ID="UploadEstimate" />--%>
</asp:Content>


