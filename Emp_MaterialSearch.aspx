<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_MaterialSearch.aspx.cs" Inherits="Emp_MaterialSearch" %>

<%@ Register Src="~/Admin/UserControls/BodyMaterialSearch.ascx" TagPrefix="uc1" TagName="BodyMaterialSearch" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="JavaScripts/MaterialSearch.js"></script>
    <uc1:BodyMaterialSearch runat="server" ID="BodyMaterialSearch" />
</asp:Content>

