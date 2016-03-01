<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="WorkShop_AddEstimate.aspx.cs" Inherits="WorkShop_AddEstimate" %>

<%@ Register Src="~/Admin/UserControls/BodyUploadEstimate.ascx" TagPrefix="uc1" TagName="BodyUploadEstimate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyUploadEstimate runat="server" ID="BodyUploadEstimate" />
</asp:Content>

