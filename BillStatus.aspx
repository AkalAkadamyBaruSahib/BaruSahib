<%@ Page Title="" Language="C#" MasterPageFile="~/BillStatus.master" AutoEventWireup="true" CodeFile="BillStatus.aspx.cs" Inherits="BillStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="span10" style="width:97%">
        <script src="JavaScripts/BillStatus.js"></script>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-user"></i>Bill Status Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="divBillsDetails" runat="server">
                        <table id="grid" style="width:100%" class='table table-striped table-bordered bootstrap-datatable datatable'>
                            <thead>
                                <tr>
                                    <th style="color: #cc3300;">Zone</th>
                                    <th style="color: #cc3300;">Academy</th>
                                    <th style="color: #cc3300;">Bill No</th>
                                    <th style="color: #cc3300;">Agency Name</th>
                                    <th style="color: #cc3300;">Bill Amount</th>
                                    <th style="color: #cc3300;">H/Q Activity</th>
                                    <th style="color: #cc3300;">Audit Activity</th>
                                    <th style="color: #cc3300;">Account Activity</th>
                                    <th style="color: #cc3300;">Receiving</th>
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

