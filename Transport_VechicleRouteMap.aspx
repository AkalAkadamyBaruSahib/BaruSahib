<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_VechicleRouteMap.aspx.cs" Inherits="Transport_VechicleRouteMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/Transport.js"></script>
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>

                    <h2><i class="icon-user"></i>
                        Vehicle   Route Map </h2>
                    <div class="box-icon">

                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">

                    <fieldset>
                        <legend></legend>
                        <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="driver" />

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="box-content">
                                    <table id="tabledata" width="100%">
                                        <tr>
                                            <td>
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Zone:</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="drpZoneName" runat="server" Style="width: 200px; height: 25px;" AutoPostBack="true" OnSelectedIndexChanged="drpZoneName_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Academy:</label>
                                                    <div class="controls">
                                                        <asp:DropDownList ID="drpAcaName" runat="server" Style="width: 200px; height: 25px;" OnSelectedIndexChanged="drpAcaName_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Route No:</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtRouteNo" runat="server" CssClass="span6 typeahead" Style="width: 200px; height: 18px;"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="driver" ID="RequiredFieldValidator_txtRouteNo" ControlToValidate="txtRouteNo" ErrorMessage="Please Enter The Route No" />
                                                    </div>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead"></label>
                                                    <div class="controls">
                                                        Vehicles List:
                                                    <br />
                                                        <asp:ListBox ID="lstVehicles" Height="150px" Width="250px" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="driver" ID="RequiredFieldValidator1" ControlToValidate="lstVehicles" ErrorMessage="Please Select The Vehicles from List" />

                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="form-actions">
                                        <%--<input id="btnSave" value="Save" class="btn btn-primary" />--%>
                                        <asp:Button ID="btnSave" Text="Save" ValidationGroup="driver" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click1" />
                                        <asp:Button ID="btnEdit" Text="Update" ValidationGroup="driver" runat="server" CssClass="btn btn-primary" Visible="false" OnClick="btnEdit_Click" />
                                    </div>

                                    <div class="row-fluid sortable">
                                        <div class="box span12">
                                            <div class="box-header well" data-original-title>
                                                <h2><i class="icon-user"></i>Vehicles Route Map</h2>
                                                <div class="box-icon">
                                                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                                                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                                                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                                                </div>
                                            </div>
                                            <div class="box-content">
                                                <div id="divMatDetails" runat="server">
                                                    <div id="divRoutMapDetails" runat="server">
                                                        <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                                                            <thead>
                                                                <tr>

                                                                    <th style="color: #cc3300;">Zone Name</th>
                                                                    <th style="color: #cc3300;">Academy Name</th>
                                                                    <th style="color: #cc3300;">Route No</th>
                                                                    <th style="color: #cc3300;">Vehicles List</th>
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
                                        </div>
                                        <!--/span-->

                                    </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </fieldset>

                </div>
            </div>
        </div>
</asp:Content>

