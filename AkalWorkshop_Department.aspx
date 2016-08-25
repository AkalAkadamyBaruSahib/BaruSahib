<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="AkalWorkshop_Department.aspx.cs" Inherits="AkalWorkshop_Department" %>

<%@ Register Src="~/Admin/UserControls/BodyDepartment.ascx" TagPrefix="uc1" TagName="BodyDepartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyDepartment runat="server" ID="BodyDepartment" />
</asp:Content>

