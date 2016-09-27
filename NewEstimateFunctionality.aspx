<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="NewEstimateFunctionality.aspx.cs" Inherits="NewEstimateFunctionality" %>

<%@ Register Src="~/Admin/UserControls/BodyNewEstimateFunctionality.ascx" TagPrefix="uc1" TagName="BodyNewEstimateFunctionality" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/NewEstimateFunctionality.js"></script>
    <uc1:BodyNewEstimateFunctionality runat="server" ID="BodyNewEstimateFunctionality" />
</asp:Content>

