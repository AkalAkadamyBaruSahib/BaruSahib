<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_ParticularEstimateView.aspx.cs" Inherits="Emp_ParticularEstimateView" EnableEventValidation="false" %>

<%@ Register Src="~/Admin/UserControls/ParticularEstimateView.ascx" TagPrefix="uc1" TagName="ParticularEstimateView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ParticularEstimateView runat="server" ID="ParticularEstimateView" />
</asp:Content>
