<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_Designation.aspx.cs" Inherits="Transport_Designation" %>

<%@ Register Src="~/Admin/UserControls/BodyDesignation.ascx" TagPrefix="uc1" TagName="BodyDesignation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyDesignation runat="server" ID="BodyDesignation" />
</asp:Content>

