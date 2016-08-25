<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="WorkshopEmployee_ViewEstMaterial.aspx.cs" Inherits="WorkshopEmployee_ViewEstMaterial" %>

<%@ Register Src="~/Admin/UserControls/BodyViewEstimateMaterial.ascx" TagPrefix="uc1" TagName="BodyViewEstimateMaterial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <uc1:BodyViewEstimateMaterial runat="server" ID="BodyViewEstimateMaterial" />
</asp:Content>
