<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadEstimate.ascx.cs" Inherits="Admin_UserControls_UploadEstimate" %>

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
    <asp:HiddenField ID="hdnEstimateID" runat="server" />
    <asp:HiddenField ID="hdnSelectedItems" runat="server" />
    <asp:HiddenField ID="hdnItemsLength" runat="server" />
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


                        </tr>
                    </table>

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
                                        <input type="file" id="fileUploadSignedCopy" multiple="multiple" />


                                    </div>
                                </div>
                            </td>
                        </tr>

                    </table>

                    <table style="width: 100%" border="1">
                        <tr>
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Select  Material Type
                                        <br />
                                        <select id="drpMaterialType" multiple="multiple" style="width: 400px; height: 150px;"></select>
                                    </div>
                                </div>
                            </td>
                            <td width="50%">
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Select  Material Names
                                        <br />
                                        <select id="drpMaterialName" multiple="multiple" style="width: 400px; height: 150px;"></select>
                                        <br />
                                        <asp:Button ID="btnloadMaterials" Text="Load" CssClass="btn btn-success" runat="server" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr id="trEstimateDetail">
                            <td colspan="2">

                                <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                                    <thead>
                                        <tr>
                                            <th style="color: #cc3300;">Material Name</th>
                                            <th style="color: #cc3300;">Source Type</th>
                                            <th style="color: #cc3300;">Quantity</th>
                                            <th style="color: #cc3300;">Unit</th>
                                            <th style="color: #cc3300;">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbody">
                                    </tbody>
                                </table>

                            </td>
                        </tr>
                    </table>
                </div>

                <div class="form-actions" style="text-align: center">
                    <asp:Button ID="btnSubEstimate" Width="200px" Height="40px" Text="Submit Estimate" ValidationGroup="visitor" CssClass="btn btn-success" runat="server" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" BackColor="Green" Font-Bold="True" Font-Size="16pt" ForeColor="Black" />
                </div>
            </fieldset>
        </div>
    </div>
</div>
