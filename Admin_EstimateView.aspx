﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_EstimateView.aspx.cs" Inherits="Admin_EstimateView" %>

<%@ Register Src="~/Admin/UserControls/EstimateView.ascx" TagPrefix="uc1" TagName="EstimateView" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:EstimateView runat="server" ID="EstimateView" />
</asp:Content>

