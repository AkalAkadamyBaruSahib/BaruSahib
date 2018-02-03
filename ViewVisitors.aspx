<%@ Page Title="" Language="C#" MasterPageFile="~/Visitor_AdminMaster.master" AutoEventWireup="true" CodeFile="ViewVisitors.aspx.cs" Inherits="ViewVisitors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/ViewVisitors.js"></script>
    <asp:HiddenField ID="hdnUserType" runat="server" />
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">
                    <h2><i class="icon-edit"></i>Visitor Search</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div class="control-group">
                        <label class="control-label" for="typeahead">Select Visitor Type</label>
                        <div class="controls">
                            <table>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddltypeofvisitor" runat="server"></asp:DropDownList>
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
                    <h2 style="color: #cc3300;"><i class="icon-user"></i>Visitor Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">

                    <div id="divMatDetails" runat="server">
                        <div id="divVisitorDetails" runat="server">
                            <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                                <thead>
                                    <tr>
                                        <th style="color: #cc3300;">Visitor Name</th>
                                        <th style="color: #cc3300;">Room(s) Allocated</th>
                                        <th style="color: #cc3300;">Arrived On</th>
                                        <th style="color: #cc3300;">Stay Upto</th>
                                        <th style="color: #cc3300;">Identity Proof</th>
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
                <h3>Visitor <span id="spnName"></span>| <span id="spanidentityfication"></span></h3>

            </div>
            <div class="modal-body">
                <iframe id="iframeDailog" style="width: 750px; height: 500px"></iframe>
            </div>
        </div>
    </div>

    <div id="pnlHtml" runat="server"></div>
</asp:Content>

