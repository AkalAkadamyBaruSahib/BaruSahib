<%@ Page Title="" Language="C#" MasterPageFile="~/AuditMaster.master" AutoEventWireup="true" CodeFile="Audit_ViewBillDetailsForApproval.aspx.cs" Inherits="Audit_ViewBillDetailsForApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/WorkAllot.js"></script>
    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>
    <asp:HiddenField ID="hdnWorkAllotID" runat="server" />
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
                    <form class="form-horizontal">
                        <fieldset>
                            <table>
                                <tr>
                                    <td>
                                        <div class="control-group">

                                            <b>Zone</b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblZone"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">

                                            <b>Academy</b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblAca"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="control-group">

                                            <b>Bill No.</b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblBillNo"></asp:Label>
                                            </div>
                                        </div>
                                    </td>

                                </tr>

                                <%-- <tr>
                                            <td colspan="2">
                                                <div class="control-group">
                                                    <b>Bill Type</b>
                                                    <div class="controls">
                                                        <asp:Label runat="server" ID="lblBillType"  ></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                           
                                        </tr>--%>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <b>Chargeable To</b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblChargeableTo"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <b>Decription of Bill</b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblBillDesc"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <b>Agency Name</b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblAgencyName"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <b>Date Of Submission</b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblBillDate"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="control-group">
                                            <b>Gate Entry No.</b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblGateEntry"></asp:Label>
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
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <b>Verified By </b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblVarifiedBy"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <b>Varified Date</b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblVarifiedDate"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <b>Verified Remark </b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblVarifiedRemark"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr runat="server" id="trRemark">
                                    <td colspan="2">
                                        <div class="control-group">
                                            <b>Remark</b>
                                            <div class="controls">
                                                <h3>
                                                    <asp:Label ID="lblRemark" runat="server" ForeColor="Red"></asp:Label></h3>
                                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="755Px" Height="50Px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div class="form-actions" runat="server" id="divFinalButtons">
                                <asp:Button ID="btnSave" runat="server" Text="Verify Bill" CssClass="btn btn-primary" BorderColor="DarkGreen" OnClick="btnSave_Click" />
                                <asp:Button ID="btnEdit" runat="server" Text="Reject Bill" CssClass="btn btn-primary" BorderColor="Red" ForeColor="Red" OnClick="btnEdit_Click" />

                            </div>
                        </fieldset>
                    </form>

                </div>
            </div>
            <!--/span-->
            <div class="modal hide fade" style="width: 900px; height: 580px;" id="myModal">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">×</button>
            <h3>Work Allot Details</h3>
        </div>
        <div class="modal-body">
            <table id="grdMatDiscription" style="width: 750px" class="table table-striped table-bordered bootstrap-datatable datatable">
                <thead>
                    <tr>
                        <th>Bill No</th>
                        <th>Agency Name</th>
                        <th>Mat Name</th>
                        <th>Quantity</th>
                        <th>Rate</th>
                        <th>Stock Entry No.</th>
                        <th>Created On</th>
                    </tr>
                </thead>
                <tbody id="tbody"></tbody>
            </table>

        </div>
    </div>
    <div class="modal-footer">
        <a href="#" class="btn" data-dismiss="modal">Close</a>
    </div>
        </div>
</asp:Content>

