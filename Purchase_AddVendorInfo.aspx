<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="Purchase_AddVendorInfo.aspx.cs" Inherits="Purchase_AddVendorInfo" %>

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
                myButton.className = "btn btn-primary";
                myButton.value = "Please Wait...";
            }
            return true;
        }
    </script>
    <div id="content" class="span10">
        <asp:HiddenField ID="hdnMaterialitems" runat="server" />
        <asp:HiddenField ID="hdnVendorID" runat="server" />
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Create Vendor</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <form class="form-horizontal">
                        <fieldset>
                            <legend></legend>
                            <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="vendor" />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Vendor Name</label>
                                                    <div class="controls">
                                                        <asp:Label ID="lblUser" runat="server" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtVendorName" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vendor" ID="reqName" ForeColor="Red"
                                                            ControlToValidate="txtVendorName" ErrorMessage="Please Enter The  Vendor Name" />
                                                    </div>
                                                </div>
                                            </td>
                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Contact No</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                                        <%--         <asp:RegularExpressionValidator ID="regxnumbervalidator" runat="server" ControlToValidate="txtPhone" ForeColor="Red" Font-Size="13px" ErrorMessage="Invalid Contact No" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                        --%>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vendor" ID="reqPhone" ForeColor="Red"
                                                            ControlToValidate="txtPhone" ErrorMessage="Please Enter The Contact No" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead">Address</label>
                                                    <div class="controls">
                                                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="span6 typeahead" Height="60px" Width="300px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vendor" ID="reqAddress" ForeColor="Red"
                                                            ControlToValidate="txtAddress" ErrorMessage="Please Enter The Address" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead"></label>
                                                    <div class="controls">
                                                        Select Material Type
                                                    <br />
                                                        <asp:ListBox ID="lstMaterialTypes" Height="150px" Width="400px" CssClass="list-group" AutoPostBack="true" OnSelectedIndexChanged="lstMaterialTypes_SelectedIndexChanged" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                                    </div>
                                                </div>
                                            </td>
                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead"></label>
                                                    <div class="controls">
                                                        Select  Material Items
                                                    <br />
                                                        <asp:ListBox ID="lstMaterials" Height="150px" Width="400px" SelectionMode="Multiple" runat="server"></asp:ListBox>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlActiveVendor" runat="server" Visible="false">
                                                    <asp:CheckBox ID="chkInactive" runat="server" />
                                                    <asp:Label ID="lblChkinActive" runat="server" Text="Is Active"></asp:Label>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-actions">
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vendor" CssClass="btn btn-primary" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" OnClick="btnSave_Click" />
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" ValidationGroup="vendor" CssClass="btn btn-primary" Visible="false" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" OnClick="btnEdit_Click" />
                                <asp:Button ID="btnCl" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCl_Click" />
                            </div>
                        </fieldset>
                    </form>

                </div>
            </div>
            <!--/span-->
        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-user"></i>Vendor Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
            </div>
            <div class="box-content">
                <asp:CheckBox ID="chkShowAllVendor" runat="server" OnCheckedChanged="chkShowAllVendor_CheckedChanged" AutoPostBack="true" />View All Vendors
                    <div id="divVendorDetails" runat="server"></div>
            </div>
        </div>
    </div>
</asp:Content>

