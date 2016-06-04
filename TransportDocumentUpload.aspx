<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="TransportDocumentUpload.aspx.cs" Inherits="TransportDocumentUpload" %>

<%@ Register Src="~/Admin/UserControls/VehicleDocuments.ascx" TagPrefix="uc1" TagName="VehicleDocuments" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="JavaScripts/Transport.js"></script>
    <uc1:VehicleDocuments runat="server" ID="VehicleDocuments" />
</asp:Content>

