<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="EstimateSearch.aspx.cs" Inherits="EstimateSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="content" class="span10">
        <asp:HiddenField ID="hdnEstID" runat="server" />
        <asp:HiddenField ID="hdnInchargeID" runat="server" />
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">
                    <h2><i class="icon-edit"></i>Estimate  Seacrh</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div class="control-group">
                        <label class="control-label" for="typeahead">Enter Estimate ID</label>
                        <div class="controls">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtEstID" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" Text="Get Estimate Detail" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divMaterialDetails" runat="server">
        </div>
        <!--/span-->
    </div>
</asp:Content>



