<%@ Page Title="" Language="C#" MasterPageFile="~/ArchMaster.master" AutoEventWireup="true" CodeFile="Arch_UploadDrawingTest.aspx.cs" Inherits="Arch_UploadDrawingTest" %>

<%@ Register Src="~/Admin/UserControls/UploadDrawing.ascx" TagPrefix="uc1" TagName="UploadDrawing" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="JavaScripts/DrawingUploadTest.js"></script>
    <uc1:UploadDrawing runat="server" ID="UploadDrawing" />
</asp:Content>

