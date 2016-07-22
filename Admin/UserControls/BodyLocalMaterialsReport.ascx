<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyLocalMaterialsReport.ascx.cs" Inherits="Admin_UserControls_BodyLocalMaterialsReport" %>

<div id="content" class="span10">
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well">
                <h2><i class="icon-edit"></i>Local Material Purchase Detail</h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <div id="divDesigDetails" runat="server">

                    <table border="0" width="100%">
                        <%--class="table table-striped table-bordered bootstrap-datatable datatable"--%>
                        <tbody>
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
                </div>
                <div>
                    <asp:Button ID="btnSearch" runat="server" Text="Click To Download Report in Excel Sheet" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>
        <!--/span-->

    </div>

</div>
