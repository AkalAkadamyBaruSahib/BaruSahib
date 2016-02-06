<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_MaterialDepatchStatus.aspx.cs" Inherits="Emp_MaterialDepatchStatus" %>

<%@ Register Src="~/Admin/UserControls/BodyPurchaseMaterialDetails.ascx" TagPrefix="uc1" TagName="BodyPurchaseMaterialDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BodyPurchaseMaterialDetails runat="server" id="BodyPurchaseMaterialDetails" />
</asp:Content>
