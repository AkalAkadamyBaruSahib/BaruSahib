<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_RejectedBills.aspx.cs" Inherits="Emp_RejectedBills" %>

<%@ Register Src="~/Admin/UserControls/BodyRejectedBills.ascx" TagPrefix="uc1" TagName="BodyRejectedBills" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<style type="text/css">
        a span {
            color: #000000;
        }
    </style>--%>
   
    <uc1:BodyRejectedBills runat="server" ID="BodyRejectedBills" />
  </asp:Content>