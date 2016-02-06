<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ViewAdminWorkDetails.aspx.cs" Inherits="ViewAdminWorkDetails" %>

<%@ Register Src="~/Admin/UserControls/BodyViewWorkDetails.ascx" TagPrefix="uc1" TagName="BodyViewWorkDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="JavaScripts/WorkAllot.js"></script>
    <uc1:BodyViewWorkDetails runat="server" ID="BodyViewWorkDetails" />
</asp:Content>

