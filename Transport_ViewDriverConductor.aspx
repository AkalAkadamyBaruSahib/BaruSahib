<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_ViewDriverConductor.aspx.cs" Inherits="Transport_ViewDriverConductor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/ViewDriverConductor.js"></script>
    <asp:HiddenField ID="hdnUserType" runat="server" />
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">
                    <h2><i class="icon-edit"></i>Driver/Conductor Search</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div class="control-group">
                        <div class="controls">
                            <table>
                                <tr>
                                    <td>Select Transport Employee Type:</td>
                                    <td>
                                        <asp:DropDownList ID="drpEmployeeType" runat="server" Style="width: 200px; height: 25px;">
                                            <asp:ListItem Text="" Value="0">--Select One--</asp:ListItem>
                                            <asp:ListItem Text="Driver" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Conductor" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trVhiclenumber" style="display:none;">
                                    <td>Select Vehicle Number</td>
                                    <td>
                                        <asp:DropDownList ID="ddlVehicleNumber" runat="server" Style="width: 200px; height: 25px;">
                                            <asp:ListItem Text="" Value="0">--Select One--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!--/span-->
        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2 style="color: #cc3300;"><i class="icon-user"></i>Driver/Conductor Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="divMatDetails" runat="server">
                        <div id="divTransportEmpDetails" runat="server">
                            <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                                <thead>
                                    <tr>

                                        <th style="color: #cc3300;">Employee</th>
                                        <th style="color: #cc3300; width: 300px;">Driving Licence</th>
                                        <th style="color: #cc3300; width: 200px;">Contact Number<br />
                                            (In Case Of Emergency)</th>
                                        <th style="color: #cc3300;">Action</th>
                                        <%-- <th style= "color: #cc3300;">DL Scan Copy</th>--%>
                                    </tr>
                                </thead>
                                <tbody id="tbody">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <!--/span-->

            </div>
        </div>

        <div id="myModal" class="modal hide fade" style="display: none; width: 800px; height: 500px">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">×</button>
                <h3>Transport Employee <span id="spnName"></span>| <span id="spanidentityfication"></span></h3>
            </div>
            <div class="modal-body">
                <iframe id="iframeDailog" style="width: 750px; height: 500px"></iframe>
            </div>
        </div>
    </div>

    <div id="pnlHtml" runat="server"></div>
</asp:Content>

