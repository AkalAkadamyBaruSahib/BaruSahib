<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_AllBillDetails.aspx.cs" Inherits="Admin_AllBillDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Div2" class="span10">
        <script src="JavaScripts/BillDetail.js"></script>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <div><h2><i class="icon-user"></i>Bill Details</h2></div>
                    <div style="float:right;">
                        <asp:Button ID="btnExecl" runat="server" Text="Bill Details Excel" CssClass="btn btn-primary"  Font-Bold="True"  ForeColor="Black" title="Click this button you get Estimate Statement Execl." data-rel="tooltip" OnClick="btnExecl_Click" />
                    </div>
                </div>
                <div class="box-content">

                    <div id="divBillsDetails" runat="server">
                        <table id="grid" style="width:100%" class='table table-striped table-bordered bootstrap-datatable datatable'>
                            <thead>
                                <tr>
                                    <th style="color: #cc3300; width: 30%;">Bill Details</th>
                                    <th style="color: #cc3300; width: 15%;">Zone</th>
                                    <th style="color: #cc3300; width: 15%;">Academy</th>
                                    <th style="color: #cc3300; width: 15%;">Amount</th>
                                    <th style="color: #cc3300; width: 25%;">Chargable To</th>
                                </tr>
                            </thead>
                            <tbody id="tbody">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal hide fade" style="width: 400px; height: 200px;" id="divprogress">
            <div class="modal-body">
                <table style="text-align: center; width: 100%">
                    <tr>
                        <td style="text-align: center">
                            <img src="img/animated.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td><b>Wait while bills are loading....</b></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>



