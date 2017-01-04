<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_BillDetails.aspx.cs" Inherits="Emp_BillDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .hidden
        {
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
                                    <td>
                                        <div class="control-group">
                                            <b>Bill No.</b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblBillNo" Text="Bill Type"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <b>Chargeable To</b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblChargeableTo" Text="Bill Type"></asp:Label>
                                            </div>
                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <b>Gate Entry No.</b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblGateEntry" Text="Bill Type"></asp:Label>
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
                                            <b>Agency Bill Number</b>
                                            <div class="controls">
                                                <asp:Label runat="server" ID="lblAgencyBillNo" Text="Bill Type"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <b>Agency Bill</b>
                                            <div class="controls">
                                                <a href="#" id="aAgencyBill" runat="server" style="font-size: 13px;" target="_blank">BillCopy</a>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%" colspan="2">
                                        <div id="divBillMaterialDetails" runat="server">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                <tr>
                                    <td width="100%" colspan="2">
                                        <div class="controls">
                                            <asp:Button ID="btnPDFDownload" style="float:right;" runat="server" OnClick="btnPDFDownload_Click" CssClass="btn btn-primary" Text="Download PDF" />
                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="control-group">
                                            <b>1st Verfication Details</b>
                                            <div class="controls">
                                                Approved By:
                                                <asp:Label ID="lblHqUser" runat="server" ForeColor="Red"></asp:Label><br />
                                                Approved On:
                                                <asp:Label ID="lblHqAppDate" runat="server" ForeColor="Green"></asp:Label><br />
                                                Remarks:
                                                <asp:Label ID="lblHqRemark" runat="server" ForeColor="Blue"></asp:Label>
                                            </div>
                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="control-group">
                                            <b>2nd Verification Details</b>
                                            <div class="controls">
                                                Approved By:
                                                <asp:Label ID="lbl2ndUser" runat="server" ForeColor="Red"></asp:Label><br />
                                                Approved On:
                                                <asp:Label ID="lbl2ndAppOn" runat="server" ForeColor="Green"></asp:Label><br />
                                                Remarks:
                                                <asp:Label ID="lbl2ndRemark" runat="server" ForeColor="Blue"></asp:Label>
                                            </div>
                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="control-group">
                                            <b>Payment Details</b>
                                            <div class="controls">
                                                Approved By:
                                                <asp:Label ID="lbl3rdUser" runat="server" ForeColor="Red"></asp:Label><br />
                                                Approved On:
                                                <asp:Label ID="lbl3rdAppOn" runat="server" ForeColor="Green"></asp:Label><br />
                                                Remarks:
                                                <asp:Label ID="lblAccRemark" runat="server" ForeColor="Blue"></asp:Label><br />
                                                Payment Mode:
                                                <asp:Label ID="lbl3rdPayMode" runat="server" ForeColor="#cc66ff"></asp:Label><br />
                                                Payment Detail:
                                                <asp:Label ID="lbl3rdPayDetails" runat="server" ForeColor="#9966ff"></asp:Label>
                                            </div>
                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="control-group">
                                            <b>Reciving Details</b>
                                            <div class="controls">
                                                Approved By:
                                                <asp:Label ID="lblRecUser" runat="server" ForeColor="Red"></asp:Label><br />
                                                Approved On:
                                                <asp:Label ID="lblRecAppOn" runat="server" ForeColor="Green"></asp:Label><br />
                                                Vochor No:
                                                <asp:Label ID="lblRecVocNo" runat="server" ForeColor="Blue"></asp:Label><br />
                                                Remark:
                                                <asp:Label ID="lblRecRemark" runat="server" ForeColor="Maroon"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                            </table>
                        </fieldset>
                    </form>
                </div>
            </div>
            <!--/span-->
        </div>
</asp:Content>


