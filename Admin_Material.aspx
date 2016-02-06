<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_Material.aspx.cs" Inherits="Admin_Material" %>

<%@ Register Src="~/Admin/UserControls/BodyMaterials.ascx" TagPrefix="uc1" TagName="BodyMaterials" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyMaterials runat="server" ID="BodyMaterials" />
</asp:Content>
