<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_LocalMaterialReort.aspx.cs" Inherits="Emp_LocalMaterialReort" %>

<%@ Register Src="~/Admin/UserControls/BodyLocalMaterialsReport.ascx" TagPrefix="uc1" TagName="BodyLocalMaterialsReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyLocalMaterialsReport runat="server" ID="BodyLocalMaterialsReport" />
</asp:Content>


