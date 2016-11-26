<%@ Page Title="" Language="C#" MasterPageFile="~/Security_AdminMaster.master" AutoEventWireup="true" CodeFile="SecuritySearchEmployee.aspx.cs" Inherits="SecuritySearchEmployee" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/SecurityEmpSearch.js"></script>
    <asp:HiddenField ID="hdnEmpID" runat="server" />
    <asp:HiddenField ID="hdnZoneId" runat="server" />
    <asp:HiddenField ID="hdnAcaId" runat="server" />
    <asp:HiddenField ID="hdnInchargeID" runat="server" />
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">
                    <h2><i class="icon-edit"></i>Security Employee  Seacrh</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div class="control-group">
                        <label class="control-label" for="typeahead">Enter Employee Name</label>
                        <div class="controls">
                            <table>
                                <tr>
                                    <td>
                                        <input id="txtEmpName" name="txtEmpName" />

                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" Text="Get Employee Detail" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div id="divEmployeeDetails" runat="server"></div>
            </div>
            <!--/span-->

        </div>
        
    </div>
</asp:Content>

