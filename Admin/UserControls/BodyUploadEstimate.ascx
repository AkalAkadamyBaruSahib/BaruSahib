<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyUploadEstimate.ascx.cs" Inherits="Admin_UserControls_BodyUploadEstimate" %>

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
                <h2><i class="icon-edit"></i>Create Estimate</h2>
                <div class="box-icon">
                    <%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <fieldset>
                <div class="box-content">
                    <%-- <asp:UpdatePanel ID="updPanel1" runat="server">
                            <ContentTemplate>--%>
                    <table width="100%">
                        <tr>
                            <td width="50%" colspan="2">
                                <div class="control-group">
                                    <h2>
                                        <asp:Label runat="server" ID="lblWorkNameReflect" Visible="false"></asp:Label></h2>
                                </div>
                            </td>

                        </tr>
                        <tr id="trEstimateNo" runat="server">
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
                        <tr id="trZone" runat="server">

                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>

                                    <div class="controls">
                                        Zone
                                                    <br />
                                        <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" ID="ddlZone_RequiredFieldValidator"
                                            ControlToValidate="ddlZone" ErrorMessage="Please Select Any Zone" ForeColor="#ff0000"></asp:RequiredFieldValidator><br />
                                        Zone Code :
                                                    <asp:Label runat="server" ID="lblZoneCode"></asp:Label>

                                    </div>

                                </div>
                            </td>
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Academy
                                                    <br />
                                        <asp:DropDownList ID="ddlAcademy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" ForeColor="#ff0000" ID="ddlAcademy_RequiredFieldValidator"
                                            ControlToValidate="ddlAcademy" ErrorMessage="Please Select Any Academy" /><br />
                                        Academy Code :
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
                                        Select Work Allot
                                                    <br />
                                        <asp:DropDownList ID="ddlWorkAllot" runat="server" Width="500px" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkAllot_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" ID="ddlWorkAllot_RequiredFieldValidator"
                                            ControlToValidate="ddlWorkAllot" ForeColor="#ff0000" ErrorMessage="Please Select Any Work" />
                                        <br />

                                    </div>

                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="50%" colspan="2">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Sub Estimate
								                <asp:TextBox ID="txtSubEstimate" runat="server" CssClass="span6 typeahead" Width="910px"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" ID="txtSubEstimate_RequiredFieldValidator"
                                            ControlToValidate="txtSubEstimate" ForeColor="#ff0000" ErrorMessage="Please Enter The Sub Estimate" />

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
                            <td colspan="2" width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Type Of work
                                                    <br />
                                        <asp:DropDownList ID="ddlTypeOfWork" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" ID="ddlTypeOfWork_RequiredFieldValidator"
                                            ControlToValidate="ddlTypeOfWork" ForeColor="#ff0000" ErrorMessage="Please Select Any Type Of Work" />
                                    </div>
                                </div>
                            </td>

                            <%-- <td width="50%">
                                    <div class="control-group">
                                        <label class="control-label" for="txtSanctionDate"></label>
                                        <div class="controls">
                                            Sanction Date
                                                    <br />
                                            <asp:TextBox ID="txtSanctionDate" runat="server" CssClass="input-xlarge datepicker"></asp:TextBox><br />
                                        </div>
                                    </div>
                                </td>--%>
                        </tr>
                    </table>
                    <%--    </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    <table width="100%">
                        <tr id="trCost" runat="server" visible="false">
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Estimate Cost
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
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        File Name
                                                    <br />
                                        <asp:TextBox runat="server" ID="txtFileName"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" ID="txtFileName_RequiredFieldValidator"
                                            ControlToValidate="txtFileName" Visible="false" ForeColor="#ff0000" ErrorMessage="Please Enter The File Name" />
                                    </div>
                                </div>
                            </td>

                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Upload File
                                                    <br />
                                        <asp:FileUpload ID="fuFile" runat="server" AllowMultiple="true" />
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="visitor" ID="fuFile_RequiredFieldValidator"
                                            ControlToValidate="fuFile" ForeColor="#ff0000" ErrorMessage="Please Upload The File" />
                                    </div>
                                </div>
                            </td>
                        </tr>

                    </table>
                    <asp:UpdatePanel ID="updpanel2" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%" border="1">
                                <tr>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                Select Default Material Type
                                                    <br />
                                                <asp:ListBox ID="lstMaterialTypes" Height="150px" Width="400px" CssClass="list-group" AutoPostBack="true" OnSelectedIndexChanged="lstMaterialTypes_SelectedIndexChanged" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                Select Default Material
                                                    <br />
                                                <asp:ListBox ID="lstMaterials" Height="150px" Width="400px" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                                <br />
                                                <asp:Button ID="btnloadMaterials" Text="Load" CssClass="btn btn-success" runat="server" OnClick="btnloadMaterials_Click" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2" width="50%" align="left">
                                        <asp:GridView ID="grvStudentDetails" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                                            CellPadding="4" Width="100px" ForeColor="#333333" GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting"
                                            Style="text-align: left" OnRowDataBound="grvStudentDetails_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="RowNumber" HeaderText="SNo" />
                                                <asp:TemplateField HeaderText="Material Type" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlMatType" Width="97Px" AutoPostBack="true" OnSelectedIndexChanged="ddlMatType_SelectedIndexChanged"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Material" ItemStyle-Width="315px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlMat" Width="300Px" AutoPostBack="true" OnSelectedIndexChanged="ddlMat_SelectedIndexChanged"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Source Type" ItemStyle-Width="105px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSourceType_SelectedIndexChanged" ID="ddlSourceType" Width="105Px"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQty" runat="server" CssClass="span6 typeahead" Width="70Px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnit" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRate" runat="server" CssClass="span6 typeahead" Width="70Px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmt" runat="server" class="control-label"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="span6 typeahead" Width="70Px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <FooterTemplate>
                                                        <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click" />
                                                    </FooterTemplate>
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
                                <tr id="trTotal" runat="server" visible="false">
                                    <td colspan="2" width="50%" align="center" style="margin-right: 80px">
                                        <asp:Button runat="server" ID="btnAmtTotal" Text="Total Amount" CssClass="btn btn-success" OnClick="btnAmtTotal_Click" />
                                        :
                                            <asp:Label ID="lblTtlAmtAfterGrid" runat="server" ForeColor="Red" Text="00.00"></asp:Label></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="form-actions" style="text-align: center">
                    <%--<asp:Button id="btnExecl" runat="server" Text="Excel Download" CssClass="btn btn-primary" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False" OnClick="btnExecl_Click"  Width="200px" Height="40px" Font-Bold="True" Font-Size="16pt" ForeColor="Black"/>--%>
                    <asp:Button ID="btnSubEstimate" Width="200px" Height="40px" Text="Submit Estimate" ValidationGroup="visitor" CssClass="btn btn-success" runat="server" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" BackColor="Green" Font-Bold="True" Font-Size="16pt" ForeColor="Black" OnClick="btnSubEstimate_Click" />
                </div>
            </fieldset>
        </div>
    </div>
</div>
