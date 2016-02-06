<%@ Page Title="" Language="C#" MasterPageFile="~/AuditMaster.master" AutoEventWireup="true" CodeFile="Audit_BillsForApproval.aspx.cs" Inherits="Audit_BillsForApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content" class="span10">
    <div class="row-fluid sortable">	
        <asp:Label runat="server" ID="lblUser" Visible="false"></asp:Label>
        <div id="divBillsDetails" runat="server"></div>	
	</div>
    </div>
</asp:Content>