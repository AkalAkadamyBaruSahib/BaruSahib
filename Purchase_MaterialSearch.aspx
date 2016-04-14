<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="Purchase_MaterialSearch.aspx.cs" Inherits="Purchase_MaterialSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/MaterialSearch.js"></script>
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">
                    <h2><i class="icon-edit"></i>Material Search</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div class="control-group">
                        <label class="control-label" for="typeahead">Material Name</label>
                        <div class="controls">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                        <input id="txtMaterial" name="txtMaterial" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"  OnClick="btnSearch_Click" />
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
                    <h2><i class="icon-user"></i>Material Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="divMaterialDetails" runat="server"></div>

                </div>
            </div>
            <!--/span-->

        </div>
    </div>
</asp:Content>

