<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_Department.aspx.cs" Inherits="Admin_Department" %>

<%@ Register Src="~/Admin/UserControls/BodyDepartment.ascx" TagPrefix="uc1" TagName="BodyDepartment" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyDepartment runat="server" ID="BodyDepartment" />
</asp:Content>

