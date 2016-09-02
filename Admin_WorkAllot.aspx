<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_WorkAllot.aspx.cs" Inherits="Admin_WorkAllot" %>

<%@ Register Src="~/Admin/UserControls/BodyWorkAllot.ascx" TagPrefix="uc1" TagName="BodyWorkAllot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyWorkAllot runat="server" ID="BodyWorkAllot" />
</asp:Content>
