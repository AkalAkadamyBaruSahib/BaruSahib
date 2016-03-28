<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="RateUpload.aspx.cs" Inherits="RateUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function ClientSideClick(myButton) {
            // Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }

            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                // diable the button
                myButton.disabled = true;
                myButton.className = "btn btn-success";
                myButton.value = "Please Wait...";
            }
            return true;
        }
    </script>
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Upload Material Rate</h2>
                    <div class="box-icon">
                        <%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <fieldset>
                    <div class="box-content">
                        <asp:UpdatePanel ID="updpanel2" runat="server">
                            <ContentTemplate>
                                <table style="width: 100%" border="0">
                                    <tr>
                                        <td width="50%">
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead"></label>
                                                <div class="controls">
                                                    Select Material Type
                                                    <br />
                                                    <asp:ListBox ID="lstMaterialTypes" Height="150px" Width="400px" CssClass="list-group" AutoPostBack="true" SelectionMode="Multiple" runat="server" OnSelectedIndexChanged="lstMaterialTypes_SelectedIndexChanged"></asp:ListBox>
                                                </div>
                                            </div>
                                        </td>
                                        <td width="50%">
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead"></label>
                                                <div class="controls">
                                                    Select Material Items
                                                    <br />
                                                    <asp:ListBox ID="lstMaterials" Height="150px" Width="400px" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnloadMaterials" Text="Load" Width="60px" Height="30px" CssClass="btn btn-primary" runat="server" OnClick="btnloadMaterials_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" width="100%" align="left">
                                            <asp:GridView ID="grvMaterialDetails" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="4" Width="100px" ForeColor="#333333" GridLines="None" Style="text-align: left" OnRowDataBound="grvMaterialDetails_RowDataBound" OnRowDeleting="grvMaterialDetails_RowDeleting">
                                                <Columns>
                                                    <asp:BoundField DataField="RowNumber" HeaderText="SNo" />
                                                    <asp:TemplateField HeaderText="Material Type" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlMatType" Width="200Px" AutoPostBack="true" OnSelectedIndexChanged="ddlMatType_SelectedIndexChanged"></asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material" ItemStyle-Width="315px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlMat" Width="300Px" AutoPostBack="true" OnSelectedIndexChanged="ddlMat_SelectedIndexChanged"></asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlUnit" Width="200Px" AutoPostBack="true"></asp:DropDownList>
                                                            <%-- <asp:TextBox ID="lblUnit" runat="server" CssClass="span6 typeahead" Width="200Px"></asp:TextBox>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Price/Rate" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRate" runat="server" CssClass="span6 typeahead" Width="200Px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:CommandField ShowDeleteButton="True" />
                                                </Columns>
                                                <FooterStyle BackColor="#3f9fd9" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="LightGray" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                       <td>
                                            <asp:Button ID="btnsave" Visible="false" runat="server" Text="Save" Style="float: right;" CssClass="btn btn-primary" OnClick="btnsave_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>

</asp:Content>

