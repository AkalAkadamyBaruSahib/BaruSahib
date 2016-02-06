<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_AddEstimate.aspx.cs" Inherits="Emp_AddEstimate" %>

<%@ Register Src="~/Admin/UserControls/BodyUploadEstimate.ascx" TagPrefix="uc1" TagName="BodyUploadEstimate" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyUploadEstimate runat="server" ID="BodyUploadEstimate" />
</asp:Content>

