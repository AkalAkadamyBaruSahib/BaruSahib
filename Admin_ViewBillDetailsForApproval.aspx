<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Admin_ViewBillDetailsForApproval.aspx.cs" Inherits="Admin_ViewBillDetailsForApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/WorkAllot.js"></script>
    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
    <asp:HiddenField ID="hdnWorkAllotID" runat="server" />
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Bill Detail</h2>
                    <div class="box-icon">
                        <%-- <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
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

                                <%-- <tr>
                                            <td colspan="2">
                                                <div class="control-group">
                                                    <b>Bill Type</b>
                                                    <div class="controls">
                                                        <asp:Label runat="server" ID="lblBillType" Text="Bill Type"></asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                           
                                        </tr>--%>
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
                                <tr>
                                    <td runat="server" id="tdProceedRemark">
                                        <div class="control-group">
                                            <b>Proceed Remark</b>
                                            <div class="controls">
                                                <asp:Label ID="lblProRemark" runat="server" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="control-group">
                                            <b>Remark</b>
                                            <div class="controls">
                                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="755Px" Height="50Px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>

                                </tr>

                            </table>




                            <div class="form-actions" runat="server" id="divFinalButtons">
                                <asp:Button ID="btnSave" runat="server" Text="Verify Bill" CssClass="btn btn-primary" BorderColor="DarkGreen" OnClick="btnSave_Click" />
                                <asp:Button ID="btnAgain" runat="server" Text="Again Verify" CssClass="btn btn-primary" BorderColor="DarkGreen" OnClick="btnAgain_Click" />
                                <asp:Button ID="btnEdit" runat="server" Text="Reject Bill" CssClass="btn btn-primary" BorderColor="Red" ForeColor="Red" OnClick="btnEdit_Click" />

                            </div>
                        </fieldset>
                    </form>

                </div>
            </div>
            <!--/span-->

        </div>
        <div class="modal hide fade" id="myModal1">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">×</button>
                <h3><b>
                    <asp:Label ID="Label1" runat="server"></asp:Label></b> Reciving Details</h3>
            </div>
            <div class="modal-body">
                <div class="controls">
                    <table align="Center">
                        <tr>
                            <td>Agency Name</td>
                            <td>
                                <asp:Label ID="lblAgency" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Recipt/ Voucher No.</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtRecipTNo"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Date Of Recieving</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtDateOfRec" Width="150Px" CssClass="input-xlarge datepicker"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Remark</td>
                            <td>
                                <asp:TextBox runat="server" ID="TextBox1" TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="modal-footer">
                <a href="#" class="btn" data-dismiss="modal">Close</a>
                <asp:Button ID="Button1" Text="Submit Details" CssClass="btn btn-primary" runat="server" />
                <%--<asp:LinkButton runat="server" class='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>
            <a class='btn btn-setting btn-round' href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>--%>
            </div>
        </div>
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

        </div>
</asp:Content>
