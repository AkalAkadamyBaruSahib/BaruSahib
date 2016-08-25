<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="WorkshopEmp_WorkshopMaterial.aspx.cs" Inherits="WorkshopEmp_WorkshopMaterial" %>

<%@ Register Src="~/Admin/UserControls/BodyWorkshopMaterial.ascx" TagPrefix="uc1" TagName="BodyWorkshopMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyWorkshopMaterial runat="server" ID="BodyWorkshopMaterial" />
</asp:Content>

