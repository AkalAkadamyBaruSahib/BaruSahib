<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="PurchaseEmployee_ViewEstMaterial.aspx.cs" Inherits="PurchaseEmployee_ViewEstMaterial" %>

<%@ Register Src="~/Admin/UserControls/BodyViewEstimateMaterial.ascx" TagPrefix="uc1" TagName="BodyViewEstimateMaterial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <uc1:BodyViewEstimateMaterial runat="server" ID="BodyViewEstimateMaterial" />
</asp:Content>


