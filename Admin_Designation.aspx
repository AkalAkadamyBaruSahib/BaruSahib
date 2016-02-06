<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_Designation.aspx.cs" Inherits="Admin_Designation" %>

<%@ Register Src="~/Admin/UserControls/BodyDesignation.ascx" TagPrefix="uc1" TagName="BodyDesignation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyDesignation runat="server" ID="BodyDesignation" />
</asp:Content>

