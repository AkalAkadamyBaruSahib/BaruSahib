<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.master" AutoEventWireup="true" CodeFile="Emp_BillSubmit.aspx.cs" Inherits="Emp_BillSubmit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .hidden {
            display: none;
        }
    </style>
    <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
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
                   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
                            <table>
                                <tr>
                                    <td>Bill Type :</td>
                                    <td>
                                        <asp:DropDownList ID="ddlBillType1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBillType1_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Select Bill Type</asp:ListItem>
                                            <asp:ListItem Value="1">Sanctioned</asp:ListItem>
                                            <%--<asp:ListItem Value="2">Non-Sanctioned</asp:ListItem>--%>
                                        </asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="control-label" for="typeahead" style="font-size: medium;" runat="server" id="lblChargeable" visible="false">Chargeable To :</label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBillType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBillType_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:DropDownList ID="ddlEsimate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEsimate_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="control-label" for="typeahead" runat="server" id="lblNameWork" visible="false">Name of Work :</label>
                                    </td>
                                    <td>
                                        <h4>
                                            <asp:Label ID="lblNameOfWork" runat="server" Visible="false" Style="font-size: medium;"></asp:Label></h4>
                                        <asp:DropDownList ID="ddlNameOfWork" OnSelectedIndexChanged="ddlNameOfWork_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
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
                                                            <asp:HiddenField ID="hdnMatID" Value='<%# Eval("MatId") %>' runat="server" />
                                                            <asp:HiddenField ID="hdnUnitId" Value='<%# Eval("UnitId") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="MatName" HeaderText="Mat Name" ItemStyle-Width="150" />
                                                    <asp:BoundField DataField="UnitName" HeaderText="Unit" ItemStyle-Width="0" />
                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="0" />
                                                    <asp:BoundField DataField="Rate" HeaderText="Rate" ItemStyle-Width="0" />
                                                    <asp:BoundField DataField="EstBal" HeaderText="Estimate Balance" ItemStyle-Width="0" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Button ID="btnShowData" runat="server" Text="Add Material" CssClass="btn btn-primary" OnClick="btnShowData_Click" />
                                            <asp:Label ID="lblData" runat="server"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <asp:Panel runat="server" ID="pnlEstimateDetails" Visible="false">
                                    <tr>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>Description of Bill :</td>
                                                    <td>
                                                        <asp:TextBox ID="txtBillDes" runat="server" Width="750px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Agency Name :</td>
                                                    <td>
                                                        <asp:TextBox ID="txtAgencyName" runat="server" Width="100px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Bill Date :</td>
                                                    <td>
                                                        <asp:TextBox ID="txtBillDate" runat="server" Width="100px" CssClass="input-xlarge datepicker"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Gate Entry No :</td>
                                                    <td>
                                                        <asp:TextBox ID="txtGateEntryNo" runat="server" Width="100px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <label class="control-label" for="typeahead" style="font-size: medium;">Add Items</label>
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
                                                                            <asp:Label runat="server" ID="lblNonSUnit" class="control-label"></asp:Label>
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
                                                                    <%--<asp:TemplateField HeaderText="MatId" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hdnSno" runat="server" />
                                                                            <asp:Label ID="txtMatId" runat="server" Text='<%# Bind("MatId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="MatName" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                        <ItemTemplate>
                                                                             <asp:HiddenField ID="hdnSno" runat="server" />
                                                                            <asp:HiddenField ID="txtMatId" Value='<%# Eval("MatId") %>' runat="server" />
                                                                            <asp:HiddenField ID="txtUnitId" Value='<%# Eval("UnitId") %>' runat="server" />
                                                                            <asp:Label ID="txtMatName" runat="server" Text='<%# Bind("MatName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtQty" runat="server" CssClass="span6 typeahead" Width="70Px" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   <%-- <asp:TemplateField HeaderText="UnitId" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtUnitId" runat="server" Text='<%# Bind("UnitId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
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
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnTtlSanctioned" Text="Total Amount" CssClass="btn btn-success" OnClick="btnTtlSanctioned_Click" /><asp:Button runat="server" ID="btnAmtTotal" Text="Total Amount" CssClass="btn btn-success" OnClick="btnAmtTotal_Click" />
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEstimateCost" runat="server" ForeColor="Red" Text="00.00"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Remark If Any :</td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemark" runat="server" Width="550px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td colspan="2">
                                        <div class="form-actions" runat="server" id="divFinalButtons">
                                            <asp:Button ID="btnSave" runat="server" Text="Submit for Approval" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCl" runat="server" Text="Cancel" CssClass="btn" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                       <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

