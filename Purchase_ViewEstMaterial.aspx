﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="Purchase_ViewEstMaterial.aspx.cs" EnableEventValidation="false" Inherits="Purchase_ViewEstMaterial" %>

<%@ Register Src="~/Admin/UserControls/BodyAssignPurchaseOffice.ascx" TagPrefix="uc1" TagName="BodyAssignPurchaseOffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyAssignPurchaseOffice runat="server" ID="BodyAssignPurchaseOffice" />
</asp:Content>

