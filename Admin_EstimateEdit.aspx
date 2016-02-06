<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_EstimateEdit.aspx.cs" Inherits="Admin_EstimateEdit" %>

<%@ Register Src="~/Admin/UserControls/BodyEstimateEdit.ascx" TagPrefix="uc1" TagName="BodyEstimateEdit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyEstimateEdit runat="server" id="BodyEstimateEdit" />
</asp:Content>

