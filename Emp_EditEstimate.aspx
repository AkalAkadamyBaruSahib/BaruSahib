<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_EditEstimate.aspx.cs" Inherits="Emp_EditEstimate" %>

<%@ Register Src="~/Admin/UserControls/BodyEstimateEdit.ascx" TagPrefix="uc1" TagName="BodyEstimateEdit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyEstimateEdit runat="server" ID="BodyEstimateEdit" />
</asp:Content>

