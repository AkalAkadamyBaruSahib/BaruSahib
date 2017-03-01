<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_Dashboard.aspx.cs" Inherits="Admin_Dashboard" %>

<%@ Register Src="~/Admin/UserControls/BodyDashboard.ascx" TagPrefix="uc1" TagName="BodyDashboard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script src="JavaScripts/Dashboard.js"></script>
    <uc1:BodyDashboard runat="server" ID="BodyDashboard" />
</asp:Content>

