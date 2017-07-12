<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" Async="true" AutoEventWireup="true" CodeFile="RateApproved.aspx.cs" Inherits="RateApproved" %>

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
                    <h2><i class="icon-edit"></i>Approved Material Rate</h2>
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
                                <table border="0">
                                    <asp:Label ID="lblUser" Visible="false" runat="server"></asp:Label>
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="grvNonApprovedRateDetails" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Style="text-align: left">
                                                <Columns>
                                                    <%--  <asp:BoundField DataField="RowNumber" HeaderText="SNo" />--%>
                                                    <asp:TemplateField HeaderText="Material Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtMaterialType" runat="server" Text='<%# Eval("MatTypeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtMaterialName" runat="server" Text='<%# Eval("MatName")+ "(" + Eval("UnitName") +")"%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vendor">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("VendorName")+ "<br>" + Eval("VendorAddress") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Old Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtRate" runat="server" Text='<%# Eval("MatCost") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRP">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRP" runat="server" Text='<%# Eval("MRP") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDiscount" runat="server" Text='<%# Eval("Discount")+"%" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Additional Discount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAdditionalDiscount" runat="server" Text='<%# Eval("AdditionalDiscount")+"%" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GST">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVat" runat="server" Text='<%# Eval("GST")+"%" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NetRate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNewRate" runat="server" Text='<%# Eval("NetRate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Requested On">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequestedOn" runat="server" Text='<%# Eval("CreatedOn") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Requested By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNewRateCreatedBy" runat="server" Text='<%# Eval("InName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btn_Approved" Text="Approved" CommandArgument='<%#Eval("MatId")%>' runat="server" OnClick="btn_Approved_Click" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="false" />
                                                            <asp:Button ID="btn_Rejected" Text="Rejected" CommandArgument='<%#Eval("MatId")%>' runat="server" CssClass="btn btn-info" UseSubmitBehavior="false" OnClick="btn_Rejected_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

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
                                        <td></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>

    <div style="display: none" id="progress">
        <table style="text-align: center">
            <tr>
                <td style="text-align: center">
                    <img src="img/animated.gif" />
                </td>
            </tr>
            <tr>
                <td>Wait while estimate is uploading....
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

