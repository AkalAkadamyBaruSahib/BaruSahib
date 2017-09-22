<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="Workshop_EstimateBucket.aspx.cs" Inherits="Workshop_EstimateBucket" %>

<%@ Register Src="~/Admin/UserControls/BodyEstimateBucket.ascx" TagPrefix="uc1" TagName="BodyEstimateBucket" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/EstimateBucket.js"></script>
    <uc1:BodyEstimateBucket runat="server" ID="BodyEstimateBucket" />
</asp:Content>


