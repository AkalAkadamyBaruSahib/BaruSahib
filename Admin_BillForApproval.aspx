<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_BillForApproval.aspx.cs" Inherits="Admin_BillForApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="span10">
        <script src="JavaScripts/BillForApproval.js"></script>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-user"></i>Bill For Approval Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    
                    <div id="divBillsDetails" runat="server">
                        <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                            <thead>
                                <tr>
                                    <th style="color: #cc3300; width: 30%;">Bill Details</th>
                                    <th style="color: #cc3300; width: 30%;">Location</th>
                                    <th style="color: #cc3300; width: 15%;">Agency Name</th>
                                    <th style="color: #cc3300; width: 10%;">Bill Amount</th>
                                    <th style="color: #cc3300; width: 15%;">Actions</th>
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

