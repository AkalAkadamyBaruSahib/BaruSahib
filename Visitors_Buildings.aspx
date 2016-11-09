<%@ Page Title="" Language="C#" MasterPageFile="~/Visitor_AdminMaster.master" AutoEventWireup="true" CodeFile="Visitors_Buildings.aspx.cs" Inherits="Visitors_Buildings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2 style="color: #cc3300;"><i class="icon-edit"></i>New Buildings</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <form class="form-horizontal">
                        <fieldset>
                            <legend></legend>
                            <div class="control-group">
                                <label class="control-label"  for="typeahead">Building Name</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtBuildingName" runat="server" CssClass="span6 typeahead"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqBuildind" runat="server" ErrorMessage="Please Enter the Building Name" ValidationGroup="building" ForeColor="Red" ControlToValidate="txtBuildingName"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-actions">
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="building" CssClass="btn btn-primary" OnClick="btnSave_Click"/>
                                 <asp:Button ID="btnEdit" runat="server" Text="Edit" Visible="false" ValidationGroup="building" CssClass="btn btn-primary" OnClick="btnEdit_Click"/>
                                <asp:Button ID="btnCl" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCl_Click" />
                            </div>
                        </fieldset>
                    </form>

                </div>
            </div>
            <!--/span-->

        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2 style="color: #cc3300;"><i class="icon-user"></i>Buildings Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="divBuildingNameDetails" runat="server"></div>

                </div>
            </div>
            <!--/span-->

        </div>
    </div>
</asp:Content>

