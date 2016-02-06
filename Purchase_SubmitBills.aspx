<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="Purchase_SubmitBills.aspx.cs" Inherits="Purchase_SubmitBills" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>
    <%-- --%>
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Submit Bill</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Zone</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Academy</label>
                                            <div class="controls">
                                                <asp:DropDownList ID="ddlAcademy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Bill Type</label>
                                            <div class="controls">

                                                <asp:DropDownList ID="ddlBillType1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBillType1_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select Bill Type</asp:ListItem>
                                                    <asp:ListItem Value="1">Sanctioned</asp:ListItem>
                                                    <asp:ListItem Value="2">Non-Sanctioned</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Chargeable To</label>
                                            <div class="controls">
                                                <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlBillType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBillType_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlEsimate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEsimate_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead">Name of Work</label>
                                            <div class="controls">
                                                <h4>
                                                    <asp:Label ID="lblNameOfWork" runat="server" Visible="false"></asp:Label>
                                                </h4>
                                                <asp:DropDownList ID="ddlNameOfWork" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr runat="server" id="trMatSelect">
                                    <td colspan="2">
                                        <div class="control-group">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-striped table-bordered bootstrap-datatable datatable">
                                                <AlternatingRowStyle />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkCtrl" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="MatId" HeaderText="" ItemStyle-Width="0" />
                                                    <asp:BoundField DataField="MatName" HeaderText="Mat Name" ItemStyle-Width="150" />
                                                    <asp:BoundField DataField="UnitId" HeaderText="" ItemStyle-Width="0" />
                                                    <asp:BoundField DataField="UnitName" HeaderText="" ItemStyle-Width="0" />
                                                    <asp:BoundField DataField="Qty" HeaderText="" ItemStyle-Width="0" />
                                                    <asp:BoundField DataField="Rate" HeaderText="" ItemStyle-Width="0" />
                                                    <asp:BoundField DataField="EstBal" HeaderText="" ItemStyle-Width="0" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Button ID="btnShowData" runat="server" Text="Add Material" CssClass="btn btn-primary" OnClick="btnShowData_Click" />
                                            <asp:Label ID="lblData" runat="server"></asp:Label>
                                        </div>
                                        </div>
                                    </td>
                                </tr>
                                <asp:Panel runat="server" ID="pnlEstimateDetails">
                                    <tr>
                                        <td colspan="2">
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead">Description of Bill</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtBillDes" runat="server" Width="750px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <div class="control-group">
                                                            <label class="control-label" for="typeahead">Agency Name.</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtAgencyName" runat="server" Width="200px"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="control-group">
                                                            <label class="control-label" for="typeahead">Date</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtBillDate" runat="server" Width="100px" CssClass="input-xlarge datepicker"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead">Gate Entry No.</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="txtGateEntryNo" runat="server" Width="100px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead">Add Items</label>
                                                <div class="controls">
                                                    <asp:Panel ID="pnlNonSanction" runat="server">
                                                        <asp:GridView runat="server" ID="gvAddItems" AutoGenerateColumns="false" OnRowDataBound="gvAddItems_RowDataBound" class="table table-striped table-bordered bootstrap-datatable datatable">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <div style="text-align: right;">
                                                                            <asp:Label ID="lblSno" runat="server" class="control-label"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Stock Entery" ItemStyle-Width="115px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>

                                                                        <asp:TextBox ID="txtStocEntry" runat="server" CssClass="span6 typeahead" Width="70Px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Material Type" ItemStyle-Width="115px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList runat="server" ID="ddlMatType" Width="115Px" AutoPostBack="true" OnSelectedIndexChanged="ddlMatType_SelectedIndexChanged"></asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Item Name" ItemStyle-Width="115px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList runat="server" ID="ddlMat" Width="85Px" AutoPostBack="true" OnSelectedIndexChanged="ddlMat_SelectedIndexChanged"></asp:DropDownList>
                                                                        <asp:TextBox ID="txtItmName" runat="server" CssClass="span6 typeahead" Width="70Px" Visible="false"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtQty" runat="server" CssClass="span6 typeahead" Width="70Px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <%--<asp:DropDownList runat="server" ID="ddlUnit" Width="70Px"></asp:DropDownList>--%>
                                                                        <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                                                        <asp:TextBox ID="txtUnit" runat="server" CssClass="span6 typeahead" Width="70Px" Visible="false"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRate" runat="server" CssClass="span6 typeahead" Width="70Px" AutoPostBack="true" OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmt" runat="server" class="control-label"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTtlAmt" runat="server" class="control-label"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Add More" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>

                                                                        <asp:Button ID="ButtonAdd2" runat="server" Text="+" ToolTip="Add New Row" OnClick="ButtonAdd2_Click" />

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlSanction" runat="server">
                                                        <asp:GridView runat="server" ID="gvAddItems2" AutoGenerateColumns="false" DataKeyNames="MatId" class="table table-striped table-bordered bootstrap-datatable datatable">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="MatId" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtMatId" runat="server" Text='<%# Bind("MatId") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="MatName" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtMatName" runat="server" Text='<%# Bind("MatName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtQty" runat="server" CssClass="span6 typeahead" Width="70Px" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UnitId" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtUnitId" runat="server" Text='<%# Bind("UnitId") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UnitName" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtUnitName" runat="server" Text='<%# Bind("UnitName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Rate" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRateSan" runat="server" CssClass="span6 typeahead" Width="70Px" AutoPostBack="true" OnTextChanged="txtRateSan_TextChanged"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmtSan" runat="server" class="control-label"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotlAmt" runat="server" class="control-label"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Stock Entery No" ItemStyle-Width="115px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtStockEntry" runat="server" CssClass="span6 typeahead" Width="70Px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Est Qty" ItemStyle-Width="115px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEstQty" runat="server" CssClass="span6 typeahead" Width="70Px" Text='<%# Bind("EstQty") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Est Rate" ItemStyle-Width="115px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEstRate" runat="server" CssClass="span6 typeahead" Width="70Px" Text='<%# Bind("EstRate") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Bal Qty" ItemStyle-Width="115px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBalQty" runat="server" CssClass="span6 typeahead" Width="70Px" Text='<%# Bind("BalQty") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button runat="server" ID="btnAmtTotal" Text="Total Amount" CssClass="btn btn-success" OnClick="btnAmtTotal_Click" />
                                            <asp:Button runat="server" ID="btnTtlSanctioned" Text="Total Amount" CssClass="btn btn-success" OnClick="btnTtlSanctioned_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label class="control-label" for="typeahead">Amount:</label>
                                                    </td>
                                                    <td style="padding-left:10px;">
                                                        <asp:Label ID="lblEstimateCost" runat="server" ForeColor="Red" Text="00.00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="control-group">
                                                            <label class="control-label" for="typeahead">Remark if Any: </label>
                                                        </div>
                                                    </td>
                                                    <td style="padding-left:10px;">
                                                        <div class="controls">
                                                            <asp:TextBox ID="txtRemark" runat="server" Width="550px"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="form-actions" runat="server" id="divFinalButtons">
                        <asp:Button ID="btnSave" runat="server" Text="Submit for Approval" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" Visible="false" />
                        <asp:Button ID="btnCl" runat="server" Text="Cancel" CssClass="btn" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-user"></i>Previous Bill Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="divAcademyDetails" runat="server"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
