<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="AdminEstimate_Bucket.aspx.cs" Inherits="AdminEstimate_Bucket" %>
<%@ Register Src="~/Admin/UserControls/BodyEstimateBucket.ascx" TagPrefix="uc1" TagName="BodyEstimateBucket" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="JavaScripts/EstimateBucket.js"></script>
    <uc1:BodyEstimateBucket runat="server" ID="BodyEstimateBucket" />
</asp:Content>
