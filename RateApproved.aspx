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
                                <table style="width: 100%" border="0">
                                      <asp:Label ID="lblUser" Visible="false" runat="server"></asp:Label>
                                    <tr>
                                        <td width="100%" align="left">
                                            <asp:GridView ID="grvNonApprovedRateDetails" runat="server" Width="100%" ShowFooter="True" AutoGenerateColumns="False" CellPadding="4"  ForeColor="#333333" GridLines="None" Style="text-align: left">
                                                <Columns>
                                                    <%--  <asp:BoundField DataField="RowNumber" HeaderText="SNo" />--%>
                                                    <asp:TemplateField HeaderText="Material Type" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtMaterialType" Width="200px" runat="server" Text='<%# Eval("MatTypeName") %>' style="text-align:left;"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material" ItemStyle-Width="315px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtMaterialName" Width="240px" runat="server" Text='<%# Eval("MatName") %>' style="text-align:left;"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="315px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtUnitName" Width="100px" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Old Rate" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                              <asp:Label ID="txtRate" Width="100px" runat="server" Text='<%# Eval("MatCost") %>'></asp:Label>
                                                         </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="New Rate" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtNewRate" runat="server" Text='<%# Eval("Rate") %>' CssClass="span6 typeahead" Width="100Px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Requested On" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequestedOn" runat="server" Text='<%# Eval("CreatedOn") %>' CssClass="span6 typeahead" Width="200Px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="New Rate By" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNewRateCreatedBy" runat="server" Text='<%# Eval("InName") %>' CssClass="span6 typeahead" Width="200Px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btn_Approved" Text="Rate Approved" CommandArgument='<%#Eval("MatId")%>' runat="server" OnClick="btn_Approved_Click" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)"  UseSubmitBehavior="false"/>
                                                            <asp:Button ID="btn_Rejected" Text="Rate Rejected" CommandArgument='<%#Eval("MatId")%>' runat="server"  CssClass="btn btn-info"  UseSubmitBehavior="false" OnClick="btn_Rejected_Click"/>
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

