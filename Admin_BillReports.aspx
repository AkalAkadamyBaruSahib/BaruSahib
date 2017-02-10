﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_BillReports.aspx.cs" Inherits="Admin_BillReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <div class="box span10">
                <div class="box-header well">
                    <h2><i class="icon-user"></i>Year and Month Wise Purchase Material Reports</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <table border="0" width="100%">
                        <tbody>
                            <tr>
                                <td colspan="23">Select Zone
                            <asp:DropDownList ID="ddlZone" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                </td>
                                <td>Select Last Date:
                            <asp:DropDownList ID="ddlAcademy" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="23">Select First Date:                
                            <asp:TextBox runat="server" ID="txtfirstDate" CssClass="input-xlarge datepicker" Width="85px"></asp:TextBox>
                                </td>
                                <td>Select Last Date:
                            <asp:TextBox runat="server" ID="txtlastDate" CssClass="input-xlarge datepicker" Width="85px"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <asp:Button ID="btnDownload" runat="server" Text="Click To Download Report in Excel Sheet" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" OnClick="btnDownload_Click" Width="355px" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
