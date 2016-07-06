<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_Drawing.aspx.cs" Inherits="Admin_Drawing" %>

<%@ Register Src="~/Admin/UserControls/UploadDrawing.ascx" TagPrefix="uc1" TagName="UploadDrawing" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/DrawingUpload.js"></script>
    <uc1:UploadDrawing runat="server" ID="UploadDrawing" />
</asp:Content>

