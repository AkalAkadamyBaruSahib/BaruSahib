<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="Purchase_Materials.aspx.cs" Inherits="Purchase_Materials" %>

<%@ Register Src="~/Admin/UserControls/BodyMaterials.ascx" TagPrefix="uc1" TagName="BodyMaterials" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyMaterials runat="server" ID="BodyMaterials" />
</asp:Content>

