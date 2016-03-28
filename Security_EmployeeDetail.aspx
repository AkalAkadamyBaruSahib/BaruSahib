<%@ Page Title="" Language="C#" MasterPageFile="~/Security_AdminMaster.master" AutoEventWireup="true" CodeFile="Security_EmployeeDetail.aspx.cs" Inherits="Security_EmployeeDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div id="content" class="span10">
       
    </div>
    <div id="divdetail" class="span10">
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
        <div id="pnlPdf" runat="server"></div>
        <div id="divEstimateDetails" runat="server"></div>

    </div>
</asp:Content>

