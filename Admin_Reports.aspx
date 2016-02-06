<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" Culture="en-US" AutoEventWireup="true" CodeFile="Admin_Reports.aspx.cs" Inherits="Admin_UserControls_Admin_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div id="content" class="span10">
        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">
                    <h2><i class="icon-user"></i>Reports Regarding Material Dispatch Status</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="divDesigDetails" runat="server">
                        <table border="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnExecl" runat="server" Text="Dispatch Excel Download" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" OnClick="btnExecl_Click" /></td>
                                    <td>
                                        <h2><i class="icon-user"></i>From:<asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                                            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" OnSelectionChanged="Calendar1_SelectionChanged" Width="330px">
                                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                                                <DayStyle BackColor="#CCCCCC" />
                                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                                                <TodayDayStyle BackColor="#999999" ForeColor="White" />
                                            </asp:Calendar>
                                            &nbsp;To:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                            <asp:Calendar ID="Calendar2" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Width="330px">
                                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                                                <DayStyle BackColor="#CCCCCC" />
                                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                                <OtherMonthDayStyle ForeColor="#999999" />
                                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                                                <TodayDayStyle BackColor="#999999" ForeColor="White" />
                                            </asp:Calendar>
                                        </h2>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div id="pnlPdf" runat="server"></div>
        <div id="divEstimateDetails" runat="server"></div>
    </div>
</asp:Content>

