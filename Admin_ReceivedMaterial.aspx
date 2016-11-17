<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_ReceivedMaterial.aspx.cs" Inherits="Admin_ReceivedMaterial" %>

<%@ Register Src="~/Admin/UserControls/BodyEstimateStatus.ascx" TagPrefix="uc1" TagName="BodyEstimateStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyEstimateStatus runat="server" ID="BodyEstimateStatus" />
</asp:Content>

