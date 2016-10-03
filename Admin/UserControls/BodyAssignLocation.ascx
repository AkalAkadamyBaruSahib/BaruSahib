<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BodyAssignLocation.ascx.cs" Inherits="Admin_UserControls_BodyAssignLocation" %>
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
<style type="text/css">
    .chkFontStyle
    {
        font-weight: bold;
    }

    .chkTxtAlign
    {
        display: inline !important;
    }
</style>


<div id="content" class="span10">
    <div class="row-fluid sortable" runat="server" id="divAllot">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i>Location Assign to Employee</h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <form class="form-horizontal">
                    <fieldset>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Department
                                        <br />
                                        <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <%--<asp:LinkButton runat="server" ID="lbSerach" OnClick="lbSerach_Click">Click To See Location</asp:LinkButton>--%><br />
                                        <asp:Panel runat="server" ID="pnlEmplyee" Visible="false">
                                            Employee
                                            <br />
                                            <asp:DropDownList ID="ddlEmpl" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlEmpl_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:Label ID="lblUser" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lblDesignation" runat="server"></asp:Label><br />
                                        </asp:Panel>

                                        <asp:Panel ID="pnlSingleSelect" runat="server" Visible="false">
                                            Assign work location
                                            <br />
                                            <asp:DropDownList ID="ddlZone" runat="server"></asp:DropDownList>
                                            <%--<asp:DropDownList ID="ddlAcademy" runat="server" ></asp:DropDownList>--%>
                                        </asp:Panel>
                                        <br />
                                        <asp:Panel ID="pnlAllZone" runat="server" Visible="false">
                                            <asp:CheckBox ID="chkAllZone" runat="server" CssClass="chkTxtAlign" />
                                            <asp:Label ID="lblChkAllZone" runat="server" Text="Assign all zone"></asp:Label>
                                        </asp:Panel>
                                        <table> 
                                            <tr style="vertical-align: top">
                                                <td>
                                                    <div style="overflow: auto; height: 600px">
                                                          <asp:Label ID="lblAssignLocation" runat="server" Visible="false" Text=" Assign work location"></asp:Label>
                                                        <br />
                                                        <asp:GridView ID="GridZone" Visible="false" runat="server" AutoGenerateColumns="false" class="table table-striped table-bordered bootstrap-datatable datatable">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Width="5">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkCtrl" runat="server" />
                                                                        <asp:HiddenField ID="hdnZoneID" runat="server" Value='<%# Eval("ZoneID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ZoneName" HeaderText="Zone" ItemStyle-Width="150" />
                                                            </Columns>
                                                        </asp:GridView>
                                                        <br />
                                                    </div>
                                                    <asp:Button runat="server" ID="btnAddAcademy" Visible="false" Text="Select Academy" CssClass="btn btn-primary" OnClick="btnAddAcademy_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                                                    <asp:Label ID="lblZoneId" runat="server" Visible="true"></asp:Label>
                                                </td>
                                                <td style="width: 50px">&nbsp;
                                                </td>
                                                <td>
                                                    <div style="overflow: auto; height: 600px">
                                                        <asp:GridView ID="GridAcademy" Visible="false" runat="server" AutoGenerateColumns="false" class="table table-striped table-bordered bootstrap-datatable datatable">
                                                            <AlternatingRowStyle />
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Width="5">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkboxSelectAll" Text="Select All" Font-Bold="true" CssClass="chkFontStyle" TextAlign="Left" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkCtrl" runat="server" OnCheckedChanged="chkCtrl_CheckedChanged" AutoPostBack="true" />
                                                                        <asp:HiddenField ID="hdnAcaId" runat="server" Value='<%# Eval("AcaID") %>' />
                                                                        <asp:HiddenField ID="hdnZoneID" runat="server" Value='<%# Eval("ZoneID") %>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="5px" />
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="AcaName" HeaderText="Academy" ItemStyle-Width="150">
                                                                    <ItemStyle Width="150px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <asp:Button runat="server" ID="btnAddAca" CssClass="btn btn-primary" Text="Load Academy" OnClick="btnAddAca_Click" Visible="false" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                                                    <asp:Label ID="lblAcaId" runat="server" Visible="true"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="form-actions">
                            <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-primary" runat="server" OnClick="btnSave_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />
                            <asp:Button ID="btnEdit" Text="Edit" CssClass="btn btn-primary" runat="server" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" Visible="false" />
                            <asp:Button ID="btnCreateIncharge" CssClass="btn btn-primary" Text="Create Incharge" runat="server" OnClick="btnCreateIncharge_Click" />
                            <%--<a href="Admin_Incharge.aspx" class="btn">Create Incharge</a>--%>
                            <a href="Default.aspx" class="btn">Cancel</a>
                        </div>
                    </fieldset>
                </form>

            </div>
        </div>
        <!--/span-->

    </div>
    <div class="row-fluid sortable" runat="server" id="divLocation">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i>Change Location</h2>
                <div class="box-icon">

                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <form class="form-horizontal">

                    <fieldset>
                        <div class="box-content">
                            <table width="100%">
                                <tr>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead" style="font-size: large;">
                                                <u style="color: darkblue; font-size: large;">Employee Name:</u>
                                                <asp:Label ID="lblEmp" runat="server"></asp:Label></label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead" style="font-size: large;">
                                                <u style="color: darkblue; font-size: large;">Current Location:</u>
                                                <asp:Label ID="lblCrtLocation" runat="server"></asp:Label></label>
                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead" style="font-size: large;">
                                                <u style="color: darkblue; font-size: large;">Department:</u>
                                                <asp:Label ID="lblDept" runat="server"></asp:Label></label>
                                        </div>
                                    </td>

                                </tr>
                                <tr>
                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead" style="font-size: large;">
                                                <u style="color: darkblue; font-size: large;">Degisnation:</u>
                                                <asp:Label ID="lblDegis" runat="server"></asp:Label></label>
                                        </div>

                                    </td>

                                </tr>

                                <tr>

                                    <td width="50%">
                                        <div class="control-group">
                                            <label class="control-label" for="typeahead" style="font-size: large;">
                                                <u style="color: darkblue; font-size: large;">Allot New Zone:</u>
                                                <asp:DropDownList runat="server" ID="ddlLocatio"></asp:DropDownList></label>
                                        </div>
                                    </td>

                                </tr>

                            </table>


                        </div>


                        <div class="form-actions">

                            <asp:Button ID="btnChnageLoc" Text="Change Location" CssClass="btn btn-primary" runat="server" OnClientClick="ClientSideClick(this)" OnClick="btnChnageLoc_Click" UseSubmitBehavior="False" />


                        </div>
                    </fieldset>
                </form>

            </div>

        </div>
        <!--/span-->

    </div>
    <div class="row-fluid sortable" runat="server" id="divLocationAssignFromAdminHome">
        <div class="box span12">
            <div class="box-header well" data-original-title>
                <h2><i class="icon-edit"></i>Assign Location</h2>
                <div class="box-icon">

                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                <form class="form-horizontal">

                    <fieldset>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="box-content">
                                    <table width="100%">
                                        <tr>
                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead" style="font-size: large;">
                                                        <u style="color: darkblue; font-size: large;">Zone Name:</u>
                                                        <asp:Label ID="lblZoneName" runat="server"></asp:Label></label>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td width="50%">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead" style="font-size: large;">
                                                        <u style="color: darkblue; font-size: large;">Department:</u>
                                                        <asp:DropDownList runat="server" ID="ddlUserTpe4Assign" AutoPostBack="true" OnSelectedIndexChanged="ddlUserTpe4Assign_SelectedIndexChanged"></asp:DropDownList></label>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td width="50%" runat="server" id="tdEmpl">
                                                <div class="control-group">
                                                    <label class="control-label" for="typeahead" style="font-size: large;">
                                                        <u style="color: darkblue; font-size: large;">Select Employee:</u>
                                                        <asp:DropDownList runat="server" ID="ddlEmp4Assign" AutoPostBack="true" OnSelectedIndexChanged="ddlEmp4Assign_SelectedIndexChanged"></asp:DropDownList></label>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td width="50%" runat="server" id="tdAcaDemy">
                                                <asp:Panel ID="pnlSelectAcademy" runat="server" Width="330px" ScrollBars="Vertical" Visible="false">
                                                    <asp:GridView ID="grdAcaFromHome" runat="server" AutoGenerateColumns="false" class="table table-striped table-bordered bootstrap-datatable datatable">
                                                        <AlternatingRowStyle />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="5">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkCtrlHome" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="AcaId" HeaderText="" ItemStyle-Width="0" />
                                                            <asp:BoundField DataField="AcaName" HeaderText="Academy" ItemStyle-Width="150" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:Label ID="Label1" runat="server" Visible="true"></asp:Label>
                                                </asp:Panel>
                                            </td>

                                        </tr>



                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="form-actions">

                            <asp:Button ID="btnAssignFrmHomePage" Text="Location Assign" CssClass="btn btn-primary" runat="server" OnClick="btnAssignFrmHomePage_Click" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False" />


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
                <h2><i class="icon-user"></i>Employee Location Details</h2>
                <div class="box-icon">
                    <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                    <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                    <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                </div>
            </div>
            <div class="box-content">
                Serach Employee For Location:
                <asp:DropDownList ID="ddlSerchEmp" runat="server" OnSelectedIndexChanged="ddlSerchEmp_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                <div id="divLocationAssign" runat="server"></div>

            </div>
        </div>
        <!--/span-->


    </div>
</div>
