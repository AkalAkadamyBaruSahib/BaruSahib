<%@ Page Title="" Language="C#" MasterPageFile="~/Security_AdminMaster.master" AutoEventWireup="true" CodeFile="Security_Reports.aspx.cs" Inherits="Security_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <div id="content" class="span10">

        <div class="box-header well">
            <h2><i class="icon-user"></i>Download Transport Reports</h2>
            <div class="box-icon">
                <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
            </div>
        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-content">
                    Select Report to Download Data:
                    <asp:DropDownList ID="ddlReport" runat="server">
                        <%--  <asp:ListItem Text="-- Select One--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Working Employee" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Non Working Employee" Value="2"></asp:ListItem>--%>
                    </asp:DropDownList><br />
                </div>

            </div>

        </div>
        <asp:Button ID="btndownload" runat="server" Text="Click To Download Execl" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" OnClick="btnDownload_Click" Width="235px" />
    </div>
</asp:Content>

