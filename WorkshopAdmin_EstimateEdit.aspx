<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="WorkshopAdmin_EstimateEdit.aspx.cs" Inherits="WorkshopAdmin_EstimateEdit" %>

<%@ Register Src="~/Admin/UserControls/BodyEstimateEdit.ascx" TagPrefix="uc1" TagName="BodyEstimateEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyEstimateEdit runat="server" id="BodyEstimateEdit" />
</asp:Content>


