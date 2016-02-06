<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_Department.aspx.cs" Inherits="Transport_Department" %>

<%@ Register Src="~/Admin/UserControls/BodyDepartment.ascx" TagPrefix="uc1" TagName="BodyDepartment" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyDepartment runat="server" ID="BodyDepartment" />
</asp:Content>

