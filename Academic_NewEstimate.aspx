<%@ Page Title="" Language="C#" MasterPageFile="~/AcademicMaster.master" AutoEventWireup="true" CodeFile="Academic_NewEstimate.aspx.cs" Inherits="Academic_NewEstimate" %>

<%@ Register Src="~/Admin/UserControls/BodyNewEstimateFunctionality.ascx" TagPrefix="uc1" TagName="BodyNewEstimateFunctionality" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyNewEstimateFunctionality runat="server" ID="BodyNewEstimateFunctionality" />
    <script src="JavaScripts/NewEstimateFunctionality.js"></script>
</asp:Content>

