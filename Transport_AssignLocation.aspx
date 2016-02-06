<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_AssignLocation.aspx.cs" Inherits="Transport_AssignLocation" %>

<%@ Register Src="~/Admin/UserControls/BodyAssignLocation.ascx" TagPrefix="uc1" TagName="BodyAssignLocation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyAssignLocation runat="server" ID="BodyAssignLocation" />
</asp:Content>

