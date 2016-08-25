<%@ Page Title="" Language="C#" MasterPageFile="~/Workshop.master" AutoEventWireup="true" CodeFile="AkalWorkshop_Incharge.aspx.cs" Inherits="AkalWorkshop_Incharge" %>

<%@ Register Src="~/Admin/UserControls/BodyEmployee.ascx" TagPrefix="uc1" TagName="BodyEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyEmployee runat="server" ID="BodyEmployee" />
</asp:Content>

