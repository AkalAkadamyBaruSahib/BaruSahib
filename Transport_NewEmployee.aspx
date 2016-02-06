<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_NewEmployee.aspx.cs" Inherits="Transport_NewEmployee" %>

<%@ Register Src="~/Admin/UserControls/BodyEmployee.ascx" TagPrefix="uc1" TagName="BodyEmployee" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyEmployee runat="server" ID="BodyEmployee" />
</asp:Content>

