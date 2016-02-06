<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_PertrolDieselPrice.aspx.cs" Inherits="PertrolDieselPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnID" runat="server" />
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">
                    <h2><i class="icon-edit"></i>Current Diesel Price</h2>
                    <div class="box-icon">
                        <%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>

                <div class="box-content">
                    <%-- <asp:UpdatePanel ID="updPanel1" runat="server">
                            <ContentTemplate>--%>
                    <fieldset>
                        <legend>
                            <asp:Label ID="lblHeading" class="btn-danger" runat="server"></asp:Label></legend>
                        
                        <table width="70%">
                            <tr id="trZone" runat="server">
                                <td>
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"></label>
                                        <div class="controls">
                                            Zone:<br />
                                            <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList><br />
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"></label>
                                        <div class="controls">
                                            Academy:<br />
                                            <asp:DropDownList ID="ddlAcademy" runat="server"></asp:DropDownList><br />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"></label>
                                        <div class="controls">
                                            Diesel Price: 
                                            <asp:TextBox ID="txtDieselPrice" Width="50px" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"></label>
                                        <div class="controls">
                                            Petrol Price: 
                                                    
                                            <asp:TextBox ID="txtPetrolPrice" Width="50px" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </fieldset>

                </div>
                 <div class="form-actions" style="text-align: center">
                    <asp:Button ID="btnSaveChanges" Text="Save Price" CssClass="btn btn-success" runat="server" OnClick="btnSaveChanges_Click" />
                </div>
                <div class="row-fluid sortable">
                    <div class="box span12">
                        <div class="box-header well" data-original-title>
                            <h2><i class="icon-user"></i>Price Details</h2>
                            <div class="box-icon">
                                <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                                <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                                <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                            </div>
                        </div>
                        <div class="box-content">
                            <div id="divDesigDetails" runat="server"></div>
                        </div>
                    </div>
                    <!--/span-->

                </div>
               
            </div>
        </div>
    </div>
</asp:Content>

