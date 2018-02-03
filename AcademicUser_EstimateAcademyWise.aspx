<%@ Page Title="" Language="C#" MasterPageFile="~/AcademicMaster.master" AutoEventWireup="true" CodeFile="AcademicUser_EstimateAcademyWise.aspx.cs" Inherits="AcademicUser_EstimateAcademyWise" %>


<%@ Register Src="~/Admin/UserControls/EstimateViewAcademyWise.ascx" TagPrefix="uc1" TagName="EstimateViewAcademyWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <uc1:EstimateViewAcademyWise runat="server" ID="EstimateViewAcademyWise" />
 </asp:Content>
