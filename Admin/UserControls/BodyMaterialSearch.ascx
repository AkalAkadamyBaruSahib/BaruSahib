<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyMaterialSearch.ascx.cs" Inherits="Admin_BodyMaterialSearch" %>
<div id="content" class="span10">
    <div class="row-fluid sortable" id="divMain" runat="server">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i>Material Detail Search</h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <div class="control-group">
                    <label class="control-label" for="typeahead">Material Name</label>
                    <div class="controls">
                        <table width="100%">
                            <tr>
                                <td>
                                    <input id="txtMaterial" name="txtMaterial" />
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                    <br />
                                    <div id="menu-container" style="position: absolute; width: 500px;"></div>

                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="row-fluid sortable">
        <div id="divMaterialDetails" runat="server"></div>

    </div>
</div>
