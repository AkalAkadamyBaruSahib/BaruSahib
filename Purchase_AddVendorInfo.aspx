<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Purchase_AddVendorInfo.aspx.cs" Inherits="Purchase_AddVendorInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/Purchase.js"></script>
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
        <asp:HiddenField ID="hdnmaterialid" runat="server" />
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
                            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>--%>
                            <table>
                                <tr>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"><b>Vendor Name:</b></label>
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
                                            <label class="control-label" for="typeahead"><b>Contact No:</b></label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="regxnumbervalidator" runat="server" ControlToValidate="txtPhone" ForeColor="Red" Font-Size="13px" ErrorMessage="Invalid Contact No" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vendor" ID="reqPhone" ForeColor="Red"
                                                    ControlToValidate="txtPhone" ErrorMessage="Please Enter The Contact No" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"><b>Address:</b></label>
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
                                            <label class="control-label" for="typeahead"><b>State</b></label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtState" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vendor" ID="RequiredFieldValidator1" ForeColor="Red"
                                                    ControlToValidate="txtState" ErrorMessage="Please Enter The  State" />
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"><b>City</b></label>
                                            <div class="controls">

                                                <asp:TextBox ID="txtCity" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vendor" ID="RequiredFieldValidator2" ForeColor="Red"
                                                    ControlToValidate="txtCity" ErrorMessage="Please Enter The City" />
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"><b>Zip</b></label>
                                            <div class="controls">
                                                <asp:TextBox ID="txtZip" runat="server" CssClass="span6 typeahead" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vendor" ID="RequiredFieldValidator3" ForeColor="Red"
                                                    ControlToValidate="txtZip" ErrorMessage="Please Enter The Zip" />
                                            </div>
                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                <b>Select  Material Items:</b>
                                                <br />
                                                <input id="txtMaterial" style="width: 135px; height: 23px;" name="txtMaterial" />

                                                <input type="button" id="btnadd" value="Add" class="btn btn-primary" />
                                                <label id="lblUserId" />
                                            </div>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead"></label>
                                            <div class="controls">
                                                <b>Select  Material Items:</b>
                                                <br />
                                                <select id="lstMaterials" multiple="multiple" height="150px" width="400px"></select>
                                            </div>
                                        </div>
                                    </td>

                                    <%--  </ContentTemplate>
                            </asp:UpdatePanel>--%>
                                </tr>
                            </table>

                            <div class="form-actions">
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vendor" CssClass="btn btn-primary" />
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" ValidationGroup="vendor" CssClass="btn btn-primary" />
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
                <div class="box-content">
                    <div id="divMatDetails" runat="server">
                            <div id="divVendorDetails" runat="server">
                                <table>
                                    <tr>
                                        <td style="float: right; margin-left: 1020px; margin-top: -4px;"> 
                                             <input type="checkbox" id="chkAllVendors" style="width: 10px; height: 10px;" />
                                            <asp:Label ID="lblchkAllVendors" runat="server" Text="Show all Vendors"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                                <thead>
                                    <tr>

                                        <th style="color: #cc3300; width: 215px;">Vendor Name</th>
                                        <th style="color: #cc3300; width: 298px;">Address</th>
                                        <th style="color: #cc3300; width: 216px;">Contact Number</th>
                                        <th style="color: #cc3300; width: 197px;">Status</th>
                                        <th style="color: #cc3300; width: 263px;">Action</th>
                                    </tr>
                                </thead>
                                <tbody id="tbody">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!--/span-->

        </div>

    </div>
</asp:Content>

