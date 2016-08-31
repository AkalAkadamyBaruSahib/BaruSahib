<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyWorkshopMaterial.ascx.cs" Inherits="Admin_UserControls_BodyWorkshopMaterial" %>

<div id="content" class="span10">
   <asp:HiddenField ID="hdnInchargeID" runat="server" />
    <asp:HiddenField ID="hdnUserType" runat="server" />
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
                    <label class="control-label" for="typeahead">Select Workshop Type</label>
                    <div class="controls">
                        <table>
                            <tr>
                                <td>

                                    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                    <asp:DropDownList ID="ddlworkshop" runat="server"></asp:DropDownList>
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
            <div class="box-content">
                <div id="trEstimateDetail">

                    <table id="grid" style="width:72%;" class='table table-striped table-bordered bootstrap-datatable datatable'>
                        <thead>
                            <tr>
                                <th style="color: #cc3300;width:7%">Sr No</th>
                                <th style="color: #cc3300;width:45%">MaterialName</th>
                                <th style="color: #cc3300;width:20%">Rate Per Piece</th>
                                <th style="color: #cc3300;width:20%">Instore</th>
                                <th style="color: #cc3300;width:10%">Action</th>
                            </tr>
                        </thead>
                        <tbody id="tbody">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>
