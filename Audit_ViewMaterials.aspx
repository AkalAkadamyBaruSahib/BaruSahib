﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AuditMaster.master" AutoEventWireup="true" CodeFile="Audit_ViewMaterials.aspx.cs" Inherits="Audit_ViewMaterials" %>

<%@ Register Src="~/Admin/UserControls/BodyMaterials.ascx" TagPrefix="uc1" TagName="BodyMaterials" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyMaterials runat="server" ID="BodyMaterials" />
</asp:Content>

