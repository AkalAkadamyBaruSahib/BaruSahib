<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Emp_AddEstimate.aspx.cs" Inherits="Emp_AddEstimate" %>

<%@ Register Src="~/Admin/UserControls/UploadEstimate.ascx" TagPrefix="uc1" TagName="UploadEstimate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/EstimateUpload.js"></script>
    <uc1:UploadEstimate runat="server" ID="UploadEstimate" />
</asp:Content>

