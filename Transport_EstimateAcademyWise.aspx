<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_EstimateAcademyWise.aspx.cs" Inherits="Transport_EstimateAcademyWise" %>

<%@ Register Src="~/Admin/UserControls/EstimateViewAcademyWise.ascx" TagPrefix="uc1" TagName="EstimateViewAcademyWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <uc1:EstimateViewAcademyWise runat="server" ID="EstimateViewAcademyWise" />
</asp:Content>

