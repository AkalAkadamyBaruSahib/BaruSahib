<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="TranspportGenerateBill.aspx.cs" Inherits="TranspportGenerateBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="span10">
        <div class="box-header well">
            <h2><i class="icon-user"></i>Generate Transport Bills</h2>
            <div class="box-icon">
                <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
            </div>
        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-content">
                    Select Academy:
                    <asp:DropDownList ID="ddlAcademy" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged">
                        <asp:ListItem Text="--Select Academy--" Selected="True" Value="0"></asp:ListItem>
                    </asp:DropDownList><br />
                </div>
            </div>

        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2>Vehicle Info</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>

                <div class="box-content">
                    <asp:GridView runat="server" AutoGenerateColumns="false" DataKeyNames="ID" ID="repVehicle"
                        class="table table-striped table-bordered">
                        <Columns>
                            <asp:TemplateField HeaderText="Check Vehicle">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" Text="Select All" Font-Bold="true" CssClass="chkFontStyle" TextAlign="Left" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkvehicle" runat="server" />
                                    <asp:HiddenField ID="hdnVechileId" runat="server" Value='<%# Eval("ID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Number" HeaderText="Number" />
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
            <asp:Button ID="btnGenerateBil" runat="server" Text="Generate Bill" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" Width="235px" OnClick="btnGenerateBil_Click" />
            <asp:Button ID="btnDownload" runat="server" Text="Click To Download Excel" OnClientClick="test();" CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" OnClick="btnDownload_Click" Width="235px" />
        </div>
    </div>
</asp:Content>

