<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_LocationAssignToEmployee.aspx.cs" Inherits="Admin_LocationAssignToEmployee" %>

<%@ Register Src="~/Admin/UserControls/BodyAssignLocation.ascx" TagPrefix="uc1" TagName="BodyAssignLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyAssignLocation runat="server" ID="BodyAssignLocation" />
</asp:Content>