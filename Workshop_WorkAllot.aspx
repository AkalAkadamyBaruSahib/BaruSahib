<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="Workshop_WorkAllot.aspx.cs" Inherits="Workshop_WorkAllot" %>

<%@ Register Src="~/Admin/UserControls/BodyWorkAllot.ascx" TagPrefix="uc1" TagName="BodyWorkAllot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyWorkAllot runat="server" ID="BodyWorkAllot" />
</asp:Content>

