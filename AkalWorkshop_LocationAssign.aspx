<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="AkalWorkshop_LocationAssign.aspx.cs" Inherits="AkalWorkshop_LocationAssign" %>

<%@ Register Src="~/Admin/UserControls/BodyAssignLocation.ascx" TagPrefix="uc1" TagName="BodyAssignLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyAssignLocation runat="server" ID="BodyAssignLocation" />
</asp:Content>

