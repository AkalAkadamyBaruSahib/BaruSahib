<%@ Page Title="" Language="C#" MasterPageFile="~/Security_AdminMaster.master" AutoEventWireup="true" CodeFile="Security_LocationAssign.aspx.cs" Inherits="Security_LocationAssign" %>

<%@ Register Src="~/Admin/UserControls/BodyAssignLocation.ascx" TagPrefix="uc1" TagName="BodyAssignLocation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:BodyAssignLocation runat="server" ID="BodyAssignLocation" />
</asp:Content>

