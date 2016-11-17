<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_ReceivedEstimate.aspx.cs" Inherits="Emp_ReceivedEstimate" %>

<%@ Register Src="~/Admin/UserControls/BodyEstimateStatus.ascx" TagPrefix="uc1" TagName="BodyEstimateStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyEstimateStatus runat="server" id="BodyEstimateStatus" />
</asp:Content>

