<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="Transport_GenerateBill.aspx.cs" Inherits="TranspportGenerateBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/TransportGenerateBills.js"></script>
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
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqddlAcademy" InitialValue="0" runat="server" ControlToValidate="ddlAcademy" ForeColor="Red" ErrorMessage="*" ValidationGroup="bill"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    Start Date:
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="input-xlarge datepicker" Width="150px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate" ForeColor="Red" ErrorMessage="*" ValidationGroup="bill"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    End Date:
                      <asp:TextBox ID="txtEndDate" runat="server" CssClass="input-xlarge datepicker" Width="150px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate" ForeColor="Red" ErrorMessage="*" ValidationGroup="bill"></asp:RequiredFieldValidator>
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
                    <asp:GridView runat="server" AutoGenerateColumns="false" Width="100%" DataKeyNames="ID" ID="repVehicle" class="table table-striped table-bordered bootstrap-datatable datatable">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select All" HeaderStyle-Width="70px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" Text="Select All" AutoPostBack="true" TextAlign="Left" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkvehicle" runat="server" />
                                    <asp:HiddenField ID="hdnVechileId" runat="server" Value='<%# Eval("ID") %>' />
                                    <asp:HiddenField ID="hdnNumber" runat="server" Value='<%# Eval("Number") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Number" HeaderText="Vehicle Number" />
                            <asp:BoundField DataField="OwnerName" HeaderText="Owner Name" />
                            <asp:BoundField DataField="OwnerNumber" HeaderText="Owner Number" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <asp:Button ID="btnDownload" runat="server" Visible="false" Text="Generate Bill" ValidationGroup="bill"  CssClass="btn btn-primary" Font-Bold="True" ForeColor="Black" OnClick="btnDownload_Click" Width="235px" />
        </div>
    </div>
</asp:Content>

