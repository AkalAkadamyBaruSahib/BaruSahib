<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="Purchase_BillDetails.aspx.cs" Inherits="Purchase_BillDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Bill Detail</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <table>
                        <tr>
                            <td>
                                <div class="control-group">

                                    <b>Zone</b>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblZone" Text="Bill Type"></asp:Label>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="control-group">

                                    <b>Academy</b>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblAca" Text="Bill Type"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="control-group">

                                    <b>Bill No.</b>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblBillNo" Text="Bill Type"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="control-group">
                                    <b>Chargeable To</b>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblChargeableTo" Text="Bill Type"></asp:Label>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="control-group">
                                    <b>Decription of Bill</b>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblBillDesc" Text="Bill Type"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="control-group">
                                    <b>Agency Name</b>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblAgencyName" Text="Bill Type"></asp:Label>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="control-group">
                                    <b>Date Of Submission</b>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblBillDate" Text="Bill Type"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="control-group">
                                    <b>Gate Entry No.</b>
                                    <div class="controls">
                                        <asp:Label runat="server" ID="lblGateEntry" Text="Bill Type"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" width="100%">
                                <div id="divBillMaterialDetails" runat="server">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
</asp:Content>