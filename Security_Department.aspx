<%@ Page Title="" Language="C#" MasterPageFile="~/Security_AdminMaster.master" AutoEventWireup="true" CodeFile="Security_Department.aspx.cs" Inherits="Security_Department" %>

<%@ Register Src="~/Admin/UserControls/BodyDepartment.ascx" TagPrefix="uc1" TagName="BodyDepartment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyDepartment runat="server" ID="BodyDepartment" />
</asp:Content>

