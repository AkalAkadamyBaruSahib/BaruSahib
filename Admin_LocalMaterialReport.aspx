<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_LocalMaterialReport.aspx.cs" Inherits="Admin_LocalMaterialReport" %>

<%@ Register Src="~/Admin/UserControls/BodyLocalMaterialsReport.ascx" TagPrefix="uc1" TagName="BodyLocalMaterialsReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyLocalMaterialsReport runat="server" ID="BodyLocalMaterialsReport" />
</asp:Content>
