<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Emp_AddEstimate.aspx.cs" Inherits="Emp_AddEstimate" %>

<%@ Register Src="~/Admin/UserControls/BodyNewEstimateFunctionality.ascx" TagPrefix="uc1" TagName="BodyNewEstimateFunctionality" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyNewEstimateFunctionality runat="server" ID="BodyNewEstimateFunctionality" />
    <script src="JavaScripts/NewEstimateFunctionality.js"></script>
 </asp:Content>
