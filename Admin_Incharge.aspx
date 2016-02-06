<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_Incharge.aspx.cs" Inherits="Admin_Incharge" %>

<%@ Register Src="~/Admin/UserControls/BodyEmployee.ascx" TagPrefix="uc1" TagName="BodyEmployee" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyEmployee runat="server" ID="BodyEmployee" />
</asp:Content>


