<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_ViewEmpLocalMaterialReport.aspx.cs" Inherits="Admin_ViewEmpLocalMaterialReport" %>

<%@ Register Src="~/Admin/BodyConstructionLocalEstimateMaterialReport.ascx" TagPrefix="uc1" TagName="BodyConstructionLocalEstimateMaterialReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <uc1:BodyConstructionLocalEstimateMaterialReport runat="server" ID="BodyConstructionLocalEstimateMaterialReport" />
</asp:Content>

