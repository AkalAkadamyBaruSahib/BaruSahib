<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="AkalWorkshop_MaterialToBeDispatch.aspx.cs" Inherits="AkalWorkshop_MaterialToBeDispatch" %>

<%@ Register Src="~/Admin/UserControls/BodyPurchaseMaterialDetails.ascx" TagPrefix="uc1" TagName="BodyPurchaseMaterialDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyPurchaseMaterialDetails runat="server" id="BodyPurchaseMaterialDetails" />
</asp:Content>


