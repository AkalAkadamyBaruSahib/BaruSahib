<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyEstimateBucket.ascx.cs" Inherits="Admin_UserControls_BodyEstimateBucket" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>
<div id="content" class="span10">
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i>Create Bucket</h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <asp:HiddenField ID="hdnInchargeID" runat="server" />
                <asp:HiddenField ID="hdnBucketID" runat="server" />

                <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                <table>
                    <tr>
                        <td width="25%">
                            <label class="control-label" for="typeahead"><b>Bucket Name:</b></label>
                            <div class="controls">
                                <input type="text" id="txtBucketName" name="txtBucketName" required />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="control-label" for="typeahead"><b>Material Type:</b></label>
                            <div class="controls">
                                <select id="drpMaterialType" onchange="drpMaterialType_onchange(this);" name="drpMaterialType" required>
                                    <option value="">--Select Material Type--</option>
                                </select>
                            </div>
                        </td>
                        <td>
                            <div class="control-group" style="margin-right: 53px; float: right;">
                                <label class="control-label" for="typeahead"><b>Material Name:</b></label>
                                <div class="controls">
                                    <select id="drpMaterialName" multiple="multiple" style="height: 30px;" name="drpMaterialName" required>
                                    </select>
                                </div>
                            </div>
                        </td>
                        <td>
                            <asp:Button ID="btnLoad" runat="server" Text="Load Bucket" Style="float: left;" CssClass="btn btn-primary" />
                            <asp:Button ID="btnUpdateLoadBucket" runat="server" Text="Update Load Bucket" Style="float: left; display: none;" CssClass="btn btn-primary" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <!--/span-->
    </div>

    <div class="row-fluid sortable" id="vendorMainbox" runat="server">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-user"></i>Estimate Bucket Details</h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <div id="divEstimateBucketDetails">
                    <table id="grid" width="100%" class='table table-striped table-bordered bootstrap-datatable datatable'>
                        <thead>
                            <tr>
                                <th style="color: #cc3300; width: 10%;">Sr.No.</th>
                                <th style="color: #cc3300; width: 20%;">Bucket Name</th>
                                <th style="color: #cc3300; width: 50%;">Material Name</th>
                                <th style="color: #cc3300; width: 20%;">Action</th>
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
