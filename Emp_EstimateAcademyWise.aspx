<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_EstimateAcademyWise.aspx.cs" Inherits="Emp_EstimateAcademyWise" %>

<%@ Register Src="~/Admin/UserControls/EstimateViewAcademyWise.ascx" TagPrefix="uc1" TagName="EstimateViewAcademyWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <uc1:EstimateViewAcademyWise runat="server" ID="EstimateViewAcademyWise" />
 </asp:Content>