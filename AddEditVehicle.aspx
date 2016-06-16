<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" AutoEventWireup="true" CodeFile="AddEditVehicle.aspx.cs" Inherits="AddVehicle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/Transport.js"></script>
    <style>
        .radio input[type="radio"],
        .radio-inline input[type="radio"],
        .checkbox input[type="checkbox"],
        .checkbox-inline input[type="checkbox"]
        {
            float: left;
            margin-left: -50px;
        }
    </style>
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

        function FileNumber() {
            var fileNumber = "Contract_";
            var No1 = $("input[id$='txtVehicleNo1']").val();
            var No2 = $("input[id$='txtVehicleNo2']").val();
            var No3 = $("input[id$='txtVehicleNo3']").val();
            var No4 = $("input[id$='txtVehicleNo4']").val();
            var $filenumber = $("input[id$='txtFileNumber']");

            if (No3 != "") {
                fileNumber = fileNumber + No1 + "-" + No2 + "-" + No3 + "-" + No4;
            }
            else {
                fileNumber = fileNumber + No1 + "-" + No2 + "-" + No4;
            }

            $filenumber.val(fileNumber);
        }



    </script>
    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">

                    <div class="box-icon">
                        <%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="vehicle" />

                <div class="box-content">
                    <%-- <asp:UpdatePanel ID="updPanel1" runat="server">
                            <ContentTemplate>--%>
                    <span class="labelH labelH-info">Add New Vehicle</span>
                    <fieldset>
                        <legend>
                            <asp:Label ID="lblHeading" CssClass="labelH labelH-info" Visible="false" runat="server"></asp:Label></legend>
                        <table width="100%">
                            <tr id="trZone" runat="server">
                                <td>
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"></label>
                                        <div class="controls">
                                            Zone<br />
                                            <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList><br />
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"></label>
                                        <div class="controls">
                                            Academy
                                                    <br />
                                            <asp:DropDownList ID="ddlAcademy" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademy_SelectedIndexChanged" runat="server"></asp:DropDownList><br />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Select Transport Type<br />
                                    <asp:DropDownList ID="ddlTransportType" OnSelectedIndexChanged="ddlTransportType_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                                </td>
                                <td>
                                    <div class="box span12">
                                        Is Vehicle on Temporary Registration? :
                                        <asp:CheckBox ID="chkTemp" runat="server" /><br />
                                        <br />
                                        Vehicle No.
                                        <asp:TextBox ID="txtVehicleNo1" Width="20px" MaxLength="2" runat="server"></asp:TextBox>-<asp:TextBox ID="txtVehicleNo2" MaxLength="2" Width="20px" runat="server"></asp:TextBox>-<asp:TextBox ID="txtVehicleNo3" MaxLength="4" Width="20px" runat="server"></asp:TextBox>-<asp:TextBox ID="txtVehicleNo4" MaxLength="5" onblur="FileNumber();" Width="50px" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vehicle" ID="reqName" ControlToValidate="txtVehicleNo1" ErrorMessage="Please enter vehicle No1!" />
                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vehicle" ID="RequiredFieldValidator1" ControlToValidate="txtVehicleNo2" ErrorMessage="Please enter vehicle No2!" />
                                        <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vehicle" ID="RequiredFieldValidator2" ControlToValidate="txtVehicleNo4" ErrorMessage="Please enter vehicle No4!" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Owner Name:<br />
                                    <asp:TextBox ID="OwnerName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vehicle" ID="RequiredFieldValidator3" ControlToValidate="OwnerName" ErrorMessage="Please enter Owner Name!" />
                                </td>
                                <td>Owner Mobile Number:
                                                    <br />
                                    <asp:TextBox ID="txtOwnerNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Driver Name:<br />
                                    <asp:DropDownList ID="ddlDriverName" runat="server"></asp:DropDownList>
                                    <%-- <asp:RequiredFieldValidator Display="None" InitialValue="0" runat="server" ValidationGroup="vehicle" ID="RequiredFieldValidator_ddlDriverName"
                                                        ControlToValidate="ddlDriverName" ErrorMessage="Please Select The Driver Name" />--%>
                                          
                                </td>
                                <td>Conductor Name:<br />
                                    <asp:DropDownList ID="ddlConductorName" runat="server"></asp:DropDownList>
                                    <%-- <asp:RequiredFieldValidator Display="None" InitialValue="0" runat="server" ValidationGroup="vehicle" ID="RequiredFieldValidator_ddlConductorName"
                                                        ControlToValidate="ddlConductorName" ErrorMessage="Please Select The Conductor Name" />--%>
                                          
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">Sitting Capacity (As Per RC)<br />
                                    <asp:TextBox ID="txtSitting" Width="100px" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vehicle" ID="RequiredFieldValidator4" ControlToValidate="txtSitting" ErrorMessage="Please enter sitting capacity !" />
                                </td>

                            </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend><span class="labelH labelH-info">Additional Information</span></legend>
                        <table width="100%">
                            <tr id="tr1" runat="server">
                                <td>
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"></label>
                                        <div class="controls">
                                            File Number:<br />
                                            <asp:TextBox ID="txtFileNumber" runat="server"></asp:TextBox>
                                            <br />
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead"></label>
                                        <div class="controls">
                                            Engine Number:
                                                    <br />
                                            <asp:TextBox ID="txtEngineNumber" runat="server"></asp:TextBox><asp:RequiredFieldValidator ValidationGroup="vehicle" Display="None" runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtEngineNumber" ErrorMessage="Please enter Engine Number!" />
                                            <br />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Chassis Number:<br />
                                    <asp:TextBox ID="txtChassisNumber" runat="server"></asp:TextBox><asp:RequiredFieldValidator Display="None" ValidationGroup="vehicle" runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtChassisNumber" ErrorMessage="Please enter Chassis Number!" />
                                </td>
                                <td>Make:<br />
                                    <asp:DropDownList ID="ddlMake" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" Display="None" ValidationGroup="vehicle" InitialValue="0" ControlToValidate="ddlMake" ErrorMessage="Please select Make!" />
                                </td>
                            </tr>
                            <tr>
                                <td>Model:<br />
                                    <asp:DropDownList ID="ddlModel" runat="server">
                                    </asp:DropDownList><asp:RequiredFieldValidator runat="server" Display="None" ValidationGroup="vehicle" ID="RequiredFieldValidator8" InitialValue="0" ControlToValidate="ddlModel" ErrorMessage="Please select Model!" />
                                </td>
                                <td>Written Contract :
                                    <asp:CheckBox ID="chkWrittenContract" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead">Period Of Contract:</label>
                                        <div class="controls">
                                            <asp:DropDownList ID="ddlPeriodOfContract" runat="server">
                                                <asp:ListItem Text="1 Year" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2 Year" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="4 Year" Value="4"></asp:ListItem>
                                            </asp:DropDownList><br />
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="control-group">
                                        <label class="control-label" for="typeahead">Diesel Rate: Rs.</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtContractDieselRate" Width="50px" runat="server"></asp:TextBox>
                                        <%--    <asp:RequiredFieldValidator Display="None" runat="server" ValidationGroup="vehicle" ID="RequiredFieldValidator9" ControlToValidate="txtContractDieselRate" ErrorMessage="Please enter Diesel Rate." />--%>
                                            <asp:Label ID="lblCurrentOilRate" runat="server"></asp:Label>
                                            <br />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>Oil Slab<br />
                                    <asp:DropDownList ID="ddlOilSlab" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>Minimun number of Km/Day<br />
                                    <asp:TextBox ID="txtKm" Width="50px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend><span class="labelH labelH-info">Condition Of Tyres</span></legend>
                        <asp:UpdatePanel ID="updatepanel" runat="server">
                            <ContentTemplate>
                                Total Number of Typres :
                                <asp:DropDownList ID="ddlNumberOfTyres" AutoPostBack="true" OnSelectedIndexChanged="ddlNumberOfTyres_SelectedIndexChanged" runat="server">
                                    <asp:ListItem Text="2 Typres" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="4 Typres" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="6 Typres" Value="6"></asp:ListItem>
                                </asp:DropDownList>
                                <br />

                                <table width="100%">
                                    <tr id="trfront" runat="server">
                                        <td>
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead"></label>
                                                <div class="controls">
                                                    Front Right:
                                            <br />
                                                    <asp:DropDownList ID="ddlFrontRight" runat="server">
                                                        <asp:ListItem Text="10 %" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="20 %" Value="20"></asp:ListItem>
                                                        <asp:ListItem Text="30 %" Value="30"></asp:ListItem>
                                                        <asp:ListItem Text="40 %" Value="40"></asp:ListItem>
                                                        <asp:ListItem Text="50 %" Value="50"></asp:ListItem>
                                                        <asp:ListItem Text="60 %" Value="60"></asp:ListItem>
                                                        <asp:ListItem Text="70 %" Value="70"></asp:ListItem>
                                                        <asp:ListItem Text="80 %" Value="80"></asp:ListItem>
                                                        <asp:ListItem Text="90 %" Value="90"></asp:ListItem>
                                                    </asp:DropDownList><br />
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="control-group">
                                                <label class="control-label" for="typeahead"></label>
                                                <div class="controls">
                                                    Front Left:
                                            <br />
                                                    <asp:DropDownList ID="ddlFrontLeft" runat="server">
                                                        <asp:ListItem Text="10 %" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="20 %" Value="20"></asp:ListItem>
                                                        <asp:ListItem Text="30 %" Value="30"></asp:ListItem>
                                                        <asp:ListItem Text="40 %" Value="40"></asp:ListItem>
                                                        <asp:ListItem Text="50 %" Value="50"></asp:ListItem>
                                                        <asp:ListItem Text="60 %" Value="60"></asp:ListItem>
                                                        <asp:ListItem Text="70 %" Value="70"></asp:ListItem>
                                                        <asp:ListItem Text="80 %" Value="80"></asp:ListItem>
                                                        <asp:ListItem Text="90 %" Value="90"></asp:ListItem>
                                                    </asp:DropDownList><br />
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr id="trRear" runat="server" visible="false">
                                        <td>Rear Right:
                                    <br />
                                            <asp:DropDownList ID="ddlRearRight" runat="server">
                                                <asp:ListItem Text="10 %" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="20 %" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="30 %" Value="30"></asp:ListItem>
                                                <asp:ListItem Text="40 %" Value="40"></asp:ListItem>
                                                <asp:ListItem Text="50 %" Value="50"></asp:ListItem>
                                                <asp:ListItem Text="60 %" Value="60"></asp:ListItem>
                                                <asp:ListItem Text="70 %" Value="70"></asp:ListItem>
                                                <asp:ListItem Text="80 %" Value="80"></asp:ListItem>
                                                <asp:ListItem Text="90 %" Value="90"></asp:ListItem>
                                            </asp:DropDownList><br />
                                        </td>
                                        <td>Rear Left:
                                    <br />
                                            <asp:DropDownList ID="ddlRearLeft" runat="server">
                                                <asp:ListItem Text="10 %" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="20 %" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="30 %" Value="30"></asp:ListItem>
                                                <asp:ListItem Text="40 %" Value="40"></asp:ListItem>
                                                <asp:ListItem Text="50 %" Value="50"></asp:ListItem>
                                                <asp:ListItem Text="60 %" Value="60"></asp:ListItem>
                                                <asp:ListItem Text="70 %" Value="70"></asp:ListItem>
                                                <asp:ListItem Text="80 %" Value="80"></asp:ListItem>
                                                <asp:ListItem Text="90 %" Value="90"></asp:ListItem>
                                            </asp:DropDownList><br />
                                        </td>
                                    </tr>
                                    <tr id="trRear2" runat="server" visible="false">
                                        <td>Rear Right 2:
                                    <br />
                                            <asp:DropDownList ID="ddlRearRight2" runat="server">
                                                <asp:ListItem Text="10 %" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="20 %" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="30 %" Value="30"></asp:ListItem>
                                                <asp:ListItem Text="40 %" Value="40"></asp:ListItem>
                                                <asp:ListItem Text="50 %" Value="50"></asp:ListItem>
                                                <asp:ListItem Text="60 %" Value="60"></asp:ListItem>
                                                <asp:ListItem Text="70 %" Value="70"></asp:ListItem>
                                                <asp:ListItem Text="80 %" Value="80"></asp:ListItem>
                                                <asp:ListItem Text="90 %" Value="90"></asp:ListItem>
                                            </asp:DropDownList><br />
                                        </td>
                                        <td>Rear Left 2:
                                    <br />
                                            <asp:DropDownList ID="ddlRearLeft2" runat="server">
                                                <asp:ListItem Text="10 %" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="20 %" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="30 %" Value="30"></asp:ListItem>
                                                <asp:ListItem Text="40 %" Value="40"></asp:ListItem>
                                                <asp:ListItem Text="50 %" Value="50"></asp:ListItem>
                                                <asp:ListItem Text="60 %" Value="60"></asp:ListItem>
                                                <asp:ListItem Text="70 %" Value="70"></asp:ListItem>
                                                <asp:ListItem Text="80 %" Value="80"></asp:ListItem>
                                                <asp:ListItem Text="90 %" Value="90"></asp:ListItem>
                                            </asp:DropDownList><br />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>


                    <br />
                    <fieldset id="dNorms" runat="server">
                        <legend><span class="labelH labelH-info">Mandatory Norms</span></legend>
                        <table width="100%">
                            <tr id="trCost" runat="server">
                                <td>
                                    <asp:CheckBoxList ID="chkNorms" Width="70%" TextAlign="Right" BorderColor="Black" RepeatColumns="4" runat="server"></asp:CheckBoxList>
                                </td>
                            </tr>

                        </table>
                    </fieldset>
                    <br />
                   <%-- <fieldset>
                        <legend><span class="labelH labelH-info">Documents</span></legend>
                        <asp:UpdatePanel ID="updpanel2" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="gvDocuments" />
                            </Triggers>
                            <ContentTemplate>
                                <table border="0">
                                    <tr>
                                        <td colspan="2" width="50%" align="left">
                                            <asp:GridView ID="gvDocuments" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                                                CellPadding="4" Width="900px" ForeColor="#333333" OnRowDataBound="gvDocuments_RowDataBound" OnRowCommand="GridView1_RowCommand" GridLines="None" Style="text-align: left">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Document Type" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocumentType" runat="server" Text='<%# Eval("DocumentName") %>' class="control-label"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Document Expiry Date" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDate" runat="server" CssClass="input-large datepicker">(mm/dd/yyyy)</asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View/Download" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocumentTypeID" runat="server" Text='<%# Eval("ID") %>' Visible="true" class="control-label"></asp:Label>
                                                            <asp:Label ID="lblDocu" Text="-1" runat="server" Visible="false" class="control-label"></asp:Label>
                                                            <asp:HyperLink ID="hypDoc" Target="_blank" runat="server" Text="No document Uploaded"></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="300px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Upload Document" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:FileUpload ID="fiupload" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="400px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btn_Approved" Visible="false" Text="Delete" CommandArgument='<%#Eval("ID")%>' runat="server" CssClass="btn btn-primary" OnClick="btn_Approved_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
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
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>--%>
                </div>
                <div class="form-actions" style="text-align: center">
                    <%--<asp:Button id="btnExecl" runat="server" Text="Excel Download" CssClass="btn btn-primary" onclientclick="ClientSideClick(this)"  UseSubmitBehavior="False" OnClick="btnExecl_Click"  Width="200px" Height="40px" Font-Bold="True" Font-Size="16pt" ForeColor="Black"/>--%>
                    <asp:Button ID="btnSaveChanges" OnClientClick="if (Page_ClientValidate()){if (confirm('Are you sure you want to save vehicle Information?')) return true;else return false;}" ValidationGroup="vehicle" Text="Save Vehicle Details" CssClass="btn btn-success" runat="server" OnClick="btnSaveChanges_Click" />
                </div>
            </div>
        </div>
    </div>
    <div style="display: none" id="progress">
        <table style="text-align:center">
            <tr>
                <td style="text-align:center">
                    <img src="img/animated.gif" />
                </td>
            </tr>
            <tr>
                <td>Wait while document is uploading....
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

