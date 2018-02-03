<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_MaterialBillQuantityReport.aspx.cs" Inherits="Admin_MaterialBillQuantutyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Bill Qty Detail Report </h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>33
                    <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="report" />
                    <table border="0" width="100%">
                        <tbody>
                            <tr>
                                <td>Select Zone:
                               </td>
                                <td>
                                    <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged" Style="margin-right: 830px; float: right;">
                                        <asp:ListItem Value="0">--Select Zone--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqZone" ControlToValidate="ddlZone" Display="None" runat="server" ForeColor="Red" ValidationGroup="report" ErrorMessage="Please select the Zone." InitialValue="0"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td>Select Academy:
                               </td>
                                <td>
                                    <asp:DropDownList ID="ddlAcademy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged" Style="margin-right: 830px; float: right;">
                                        <asp:ListItem Value="0">--Select Academy--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlAcademy" Display="None" runat="server" ValidationGroup="report" ForeColor="Red" ErrorMessage="Please select the Academy." InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Select Name Of Work:                
                             </td>
                                <td>
                                    <asp:DropDownList ID="ddlNameOfWork" runat="server" Style="margin-right: 830px; float: right;">
                                        <asp:ListItem Value="0">--Select Name Of Work--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlNameOfWork" Display="None" runat="server" ValidationGroup="report" ForeColor="Red" ErrorMessage="Please select the Name Of Work." InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <asp:Button ID="btnDownload" runat="server" Text="Click To Download Report in Excel Sheet" ValidationGroup="report" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" OnClick="btnDownload_Click" Width="355px" />
                    </div>
                </div>
            </div>
            <!--/span-->

        </div>
    </div>
</asp:Content>



