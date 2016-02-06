<%@ Page Title="" Language="C#" MasterPageFile="Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_ReportDetails.aspx.cs" Inherits="Transport_ReporteDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/Transport.js"></script>

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
                    <asp:DropDownList ID="ddlReport" runat="server" onchange="test(this);">
                        <asp:ListItem Text="Daily Document Uploaded Report" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Pending Documents" Value="2"></asp:ListItem>
                    </asp:DropDownList><br />
                </div>
                <div class="box-content" id="divPendingDocumentReport" style="display: none">
                    Select Zone to Download Data:
                    <asp:DropDownList ID="ddlZone" runat="server">
                    </asp:DropDownList><br />
                </div>
                <div class="box-content" id="divDailyReport" style="display: none">
                    Date From:                
                            <asp:TextBox runat="server" ID="txtfirstDate" CssClass="input-xlarge datepicker" Width="85px"></asp:TextBox>

                    Date To:
                                   <asp:TextBox runat="server" ID="txtlastDate" CssClass="input-xlarge datepicker" Width="85px"></asp:TextBox>
                </div>
            </div>
        </div>
        <asp:Button ID="Button1" runat="server" Text="Click To Download Execl" OnClientClick="test();" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" OnClick="btnDownload_Click" Width="235px" />
    </div>
</asp:Content>

