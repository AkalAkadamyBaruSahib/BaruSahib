<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyEstimateEdit.ascx.cs" Inherits="Admin_UserControls_BodyEstimateEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:HiddenField ID="hdnIsApproved" runat="server" />
<div id="content" class="span10">
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i>Edit Estimate</h2>
                <div class="box-icon">
                    <%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <fieldset>
                <div class="box-content">
                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpload" />
                        </Triggers>
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td width="50%" colspan="2">
                                        <div class="control-group">
                                            <h2>
                                                <asp:Label runat="server" ID="lblWorkNameReflect" Visible="false"></asp:Label></h2>
                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                Estimate No.
								                <asp:Label ID="lblEstimateNo" runat="server"></asp:Label>
                                                <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>

                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>

                                            <div class="controls">
                                                Zone:
                                                    <asp:Label runat="server" ID="lblZoneCode"></asp:Label>
                                            </div>

                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                Academy:
                                                    <asp:Label runat="server" ID="lblAcaCode"></asp:Label>
                                            </div>

                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="2" runat="server" visible="false" id="tdWorkAllot">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                Work Allot Name:
                                                    <asp:DropDownList ID="ddlWorkType" Width="300px" runat="server"></asp:DropDownList>
                                                <asp:Label ID="lblWorkName" runat="server"></asp:Label>

                                            </div>

                                        </div>
                                    </td>
                                </tr>
                                <%-- </ContentTemplate>
                                                    </asp:UpdatePanel>--%>
                                <tr>
                                    <td width="50%" colspan="2">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                Sub Estimate:
                                                    <asp:Label runat="server" ID="lblSubEstimate"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                Type Of work:
                                                    <asp:Label runat="server" ID="lblTpeofWork"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="txtSanctionDate"></label>
                                            <div class="controls">
                                                Sanction Date:
                                                    <asp:Label ID="lblSanctiondate" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                Estimate Cost:
                                                    <asp:Label ID="lblEstimateCost" runat="server" ForeColor="Red" Text="00.00"></asp:Label>
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td width="50%">
                                        <div class="control-group" id="divuploadfile" runat="server">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                Upload File
                                                    <br />
                                                <asp:FileUpload ID="fuFile" runat="server" AllowMultiple="true" />
                                            </div>
                                        </div>
                                    </td>

                                </tr>

                                <tr>
                                    <th colspan="2" width="50%" align="left">
                                        <h4></h4>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">

                                                <asp:Label ID="lblmsg" runat="server" Font-Size="18px" ForeColor="Red" Font-Bold="true"></asp:Label>

                                            </div>
                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="3">

                                        <asp:GridView ID="gvDetails" DataKeyNames="Sno" runat="server" AutoGenerateColumns="false" CssClass="Gridview" HeaderStyle-BackColor="#61A6F8"
                                            ShowFooter="true" HeaderStyle-Font-Bold="true" Width="100%" HeaderStyle-ForeColor="White" OnRowEditing="gvDetails_RowEditing"
                                            OnRowCancelingEdit="gvDetails_RowCancelingEdit" OnRowUpdating="gvDetails_RowUpdating" OnRowCommand="gvDetails_RowCommand"
                                            OnRowDataBound="gvDetails_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="50px">
                                                    <EditItemTemplate>
                                                        <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/img/Images/update.jpg" ToolTip="Update" Height="20px" Width="20px" />
                                                        <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/img/Images/Cancel.jpg" ToolTip="Cancel" Height="20px" Width="20px" />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/img/Images/Edit.jpg" ToolTip="Edit" Height="20px" Width="20px" />
                                                        <asp:ImageButton ID="imgbtnDelete" CommandArgument='<%#Eval("Sno") %>' CommandName="Delete" runat="server" ImageUrl="~/img/Images/Delete.jpg" ToolTip="Delete" Height="20px" Width="20px" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/img/Images/AddNewitem.jpg" ValidationGroup="visitor" CommandName="AddNew" Width="30px" Height="30px" ToolTip="Add new User" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EstmateId" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblEstIdEdit" runat="server" Text='<%#Eval("EstId") %>' />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEstId" runat="server" Text='<%#Eval("EstId") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblEstIdFooter" runat="server" Text='<%#Eval("EstId") %>' />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Material Type" HeaderStyle-Width="160px">
                                                    <EditItemTemplate>
                                                        <%-- <asp:TextBox ID="txtcity" runat="server" Text='<%#Eval("MatTypeId") %>'/>--%>
                                                        <asp:DropDownList runat="server" Width="115Px" ID="ddlMatTId" OnSelectedIndexChanged="ddlMatTId_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMatT" runat="server" Text='<%#Eval("MatTypeName") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList runat="server" Width="115Px" ID="ddlMatTIdFooter" OnSelectedIndexChanged="ddlMatTIdFooter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Material">
                                                    <EditItemTemplate>
                                                        <%--<asp:TextBox ID="txtstate" runat="server" Text='<%#Eval("TAddr") %>'/>--%>
                                                        <asp:DropDownList runat="server" ID="ddlMatId" Width="115Px" OnSelectedIndexChanged="ddlMatId_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMat" runat="server" Text='<%#Eval("MatName") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlMatIdFooter" Width="115Px" OnSelectedIndexChanged="ddlMatIdFooter_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblUnitEdit" runat="server" Text='<%#Eval("UnitName") %>' />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("UnitName") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblUnitFooter" runat="server" Text='<%#Eval("UnitName") %>' />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtQty" Width="85Px" runat="server" Text='<%#Eval("Qty") %>' />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtQty" runat="server" ValidationGroup="visitor"
                                                            ControlToValidate="txtQty" ForeColor="Red" ErrorMessage="Please enter the Qty" />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Qty") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtQtyFooter" Width="85Px" runat="server" Text='<%#Eval("Qty") %>' />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtQtyFooter" runat="server" ValidationGroup="visitor"
                                                            ControlToValidate="txtQtyFooter" ForeColor="Red" ErrorMessage="Please enter the Qty" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtRate" Width="85Px" runat="server" Text='<%#Eval("Rate") %>' OnTextChanged="txtRate_TextChanged" AutoPostBack="true" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtRate" runat="server" ValidationGroup="visitor"
                                                            ControlToValidate="txtRate" ForeColor="Red" ErrorMessage="Please enter the Rate" />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Rate") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtRateFooter" Width="85Px" runat="server" Text='<%#Eval("Rate") %>' OnTextChanged="txtRateFooter_TextChanged" AutoPostBack="true" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtRateFooter" runat="server" ValidationGroup="visitor"
                                                            ControlToValidate="txtRateFooter" ForeColor="Red" ErrorMessage="Please enter the Rate" />
                                                    </FooterTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="txtAmtEdit" runat="server" Text='<%#Eval("Amount") %>' />

                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmt" runat="server" Text='<%#Eval("Amount") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblAmtFooter" runat="server" />
                                                        <%--<asp:CustomValidator runat="server" id="cusCustom"/>--%>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Source Type">
                                                    <EditItemTemplate>
                                                        <%--<asp:Label ID="txtAmtEdit" runat="server" Text='<%#Eval("Amount") %>'/>--%>
                                                        <asp:DropDownList runat="server" ID="ddlPs" Width="115Px"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPs" runat="server" Text='<%#Eval("PSName") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlPsFooter" Width="115Px"></asp:DropDownList>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditRemark" runat="server" Text='<%#Eval("remarkByPurchase") %>' />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemark" runat="server" ForeColor="Red" Text='<%#Eval("remarkByPurchase") %>' />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr id="trReivew" runat="server">
                                    <td colspan="2">
                                        <div class="control-group">
                                            <b>Remark</b>
                                            <div class="controls">
                                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="755Px" Height="50Px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <asp:Button ID="btnRejectEdit" runat="server" Text="Reject Bill" CssClass="btn btn-primary" OnClick="btnUpload_Click" />
                                                <asp:Button ID="btnUpload" CssClass="btn btn-primary" Text="Save/Approved Changes" runat="server" OnClick="btnUpload_Click" />
                                        </div>
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
