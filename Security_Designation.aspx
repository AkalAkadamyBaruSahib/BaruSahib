<%@ Page Title="" Language="C#" MasterPageFile="~/Security_AdminMaster.master" AutoEventWireup="true" CodeFile="Security_Designation.aspx.cs" Inherits="Security_Designation" %>

<%@ Register Src="~/Admin/UserControls/BodyDesignation.ascx" TagPrefix="uc1" TagName="BodyDesignation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyDesignation runat="server" ID="BodyDesignation" />
</asp:Content>

