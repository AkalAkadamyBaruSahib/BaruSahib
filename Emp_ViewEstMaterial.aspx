<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_ViewEstMaterial.aspx.cs" Inherits="Emp_ViewEstMaterial" %>

<%@ Register Src="~/Admin/UserControls/BodyViewEstimateMaterial.ascx" TagPrefix="uc1" TagName="BodyViewEstimateMaterial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyViewEstimateMaterial runat="server" ID="BodyViewEstimateMaterial" />
</asp:Content>


