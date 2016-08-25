<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="Workshop_AcademiesDetails.aspx.cs" Inherits="Workshop_AcademiesDetails" %>

<%@ Register Src="~/Admin/UserControls/EstimateViewAcademyWise.ascx" TagPrefix="uc1" TagName="EstimateViewAcademyWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <uc1:EstimateViewAcademyWise runat="server" ID="EstimateViewAcademyWise" />
 </asp:Content>

