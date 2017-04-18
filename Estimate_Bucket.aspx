<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Estimate_Bucket.aspx.cs" Inherits="Estimate_Bucket" %>

<%@ Register Src="~/Admin/UserControls/BodyEstimateBucket.ascx" TagPrefix="uc1" TagName="BodyEstimateBucket" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="JavaScripts/EstimateBucket.js"></script>
    <uc1:BodyEstimateBucket runat="server" ID="BodyEstimateBucket" />
</asp:Content>

