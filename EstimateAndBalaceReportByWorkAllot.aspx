<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="EstimateAndBalaceReportByWorkAllot.aspx.cs" Inherits="EstimateAndBalaceReportByWorkAllot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Estimate And Balance Report Report </h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="report" />
                    <table border="0" width="100%">
                        <tbody>
                            <tr>
                                <td>Select Zone:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlZone" runat="server" Style="margin-right: 730px; float: right;">
                                        <asp:ListItem Value="0">--Select Zone--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqZone" ControlToValidate="ddlZone" Display="None" runat="server" ForeColor="Red" ValidationGroup="report" ErrorMessage="Please select the Zone." InitialValue="0"></asp:RequiredFieldValidator></td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <asp:Button ID="btnDownload" runat="server" Text="Click To Download Report in Excel Sheet" OnClick="btnDownload_Click" ValidationGroup="report" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" Width="355px" />
                    </div>
                </div>
            </div>
            <!--/span-->

        </div>
    </div>
</asp:Content>

