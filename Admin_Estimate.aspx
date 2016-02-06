<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_Estimate.aspx.cs" Inherits="Admin_Estimate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Admin/UserControls/BodyUploadEstimate.ascx" TagPrefix="uc1" TagName="BodyUploadEstimate" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyUploadEstimate runat="server" ID="BodyUploadEstimate" />
</asp:Content>

