<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Transport_VehicleServiceDetails.aspx.cs" Inherits="Transport_VehicleServiceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/VehicleServiceDetail.js"></script>
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
    <asp:HiddenField ID="hdnInchargeID" runat="server" />
    <asp:HiddenField ID="hdnTyreNum" runat="server" />
    <asp:HiddenField ID="hdnSeatedCapacity" runat="server" />
    <asp:HiddenField ID="hdnVehicleServiceID" runat="server" />

    <div id="content" class="span10">
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-edit"></i>Vehicle Service Detail</h2>
                    <div class="box-icon">
                        <%--<a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>--%>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <asp:ValidationSummary ID="vs" runat="server" ForeColor="Red" ValidationGroup="vehicle" />
                <div class="box-content">
                    <table border="0" style="width: 100%;">
                        <tr>
                            <td>
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Select Academy<br />
                                        <asp:DropDownList ID="drpAcademy" runat="server">
                                            <asp:ListItem Value="0">--Select Academy--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqdrpAcademy" Display="None" runat="server" ValidationGroup="vehicle" ControlToValidate="drpAcademy" InitialValue="0" ErrorMessage="Please Select Academy" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <br />
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Vehicle<br />
                                        <asp:DropDownList ID="drpVehicle" runat="server">
                                            <asp:ListItem Value="0">--Select Vehicle--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server" ValidationGroup="vehicle" ControlToValidate="drpVehicle" InitialValue="0" ErrorMessage="Please Select Vehicles" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <br />
                                    </div>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Current Meter Reading<br />
                                        <asp:TextBox ID="txtCurrentMeterReading" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="regxnumbervalidator" runat="server" ValidationGroup="vehicle" ControlToValidate="txtCurrentMeterReading" ForeColor="Red" Font-Size="13px" ErrorMessage="Invalid No!!!Use Numeric Value" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                        <%--         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server" ValidationGroup="vehicle" ControlToValidate="txtCurrentMeterReading" ErrorMessage="Please enter Current Meter Reading." ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                        <br />
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Current Meter Reading File<br />
                                        <asp:FileUpload ID="fileCurrentMeterReading" runat="server" AllowMultiple="true" />
                                        <%--         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server" ValidationGroup="vehicle" ControlToValidate="fileCurrentMeterReading" ErrorMessage="Please upload Current Meter Reading File." ForeColor="Red"></asp:RequiredFieldValidator>
                                        --%>
                                        <div id="aMeterReading" style="display: none;"></div>
                                        <br />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Last Service KM<br />
                                        <asp:TextBox ID="txtLastServiceKM" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtLastServiceKM" ValidationGroup="vehicle" ForeColor="Red" Font-Size="13px" ErrorMessage="Invalid No!!!Use Numeric Value" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="None" runat="server" ValidationGroup="vehicle" ControlToValidate="txtLastServiceKM" ErrorMessage="Please enter Last Service KM." ForeColor="Red"></asp:RequiredFieldValidator>
                                        <br />
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Last Service Date<br />
                                        <asp:TextBox ID="txtLastServiceDate" runat="server" CssClass="input-xlarge datepicker" Width="240px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Format.Use(DD/MM/YYYY)." ValidationGroup="vehicle" ForeColor="Red" ControlToValidate="txtLastServiceDate" SetFocusOnError="true" ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server" ValidationGroup="vehicle" ControlToValidate="txtLastServiceDate" ErrorMessage="Please enter Last Service Date." ForeColor="Red"></asp:RequiredFieldValidator>
                                        <br />
                                    </div>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Battery Instalation Date:<br />
                                        <asp:TextBox ID="txtBattery" runat="server" CssClass="input-xlarge datepicker" Width="240px" />
                                        <asp:RegularExpressionValidator ID="regPurchaseDate" runat="server" ErrorMessage="Invalid Format.Use(DD/MM/YYYY)." ValidationGroup="vehicle" ForeColor="Red" ControlToValidate="txtBattery" SetFocusOnError="true" ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None" ValidationGroup="vehicle" ControlToValidate="txtBattery" ErrorMessage="Please enter Battery Instalation Date." ForeColor="Red"></asp:RequiredFieldValidator>

                                        <br />
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Make Of Battery<br />
                                        <asp:TextBox ID="txtMakeofbattery" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None" ValidationGroup="vehicle" ControlToValidate="txtMakeofbattery" ErrorMessage="Please enter Make Of Battery." ForeColor="Red"></asp:RequiredFieldValidator>
                                        <br />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Battery Capacity:<br />
                                        <asp:TextBox ID="txtBatteryCapacity" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None" ValidationGroup="vehicle" ControlToValidate="txtBatteryCapacity" ErrorMessage="Please enter  Battery Capacity." ForeColor="Red"></asp:RequiredFieldValidator>
                                        <br />
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Battery Serial Number<br />
                                        <asp:TextBox ID="txtBatterySerialNum" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="None" ValidationGroup="vehicle" ControlToValidate="txtBatterySerialNum" ErrorMessage="Please enter Battery Serial Number." ForeColor="Red"></asp:RequiredFieldValidator>
                                        <br />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="control-group">
                                    <label class="control-label" for="typeahead"></label>
                                    <div class="controls">
                                        Battery Life In Years:<br />
                                        <asp:TextBox ID="txtBatteryLifeInYears" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtBatteryLifeInYears" ValidationGroup="vehicle" ForeColor="Red" Font-Size="13px" ErrorMessage="Invalid No!!!Use Numeric Value" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="None" ValidationGroup="vehicle" ControlToValidate="txtBatteryLifeInYears" ErrorMessage="Please enter  Battery Life In Years." ForeColor="Red"></asp:RequiredFieldValidator>
                                        <br />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr id="trTyre" style="display: none;">
                            <td colspan="2">
                                <table id="tblTyre" style="width: 80%;" class='table table-striped table-bordered'>
                                    <thead>
                                        <tr>
                                            <th>Tyre Type</th>
                                            <th><b>KM Running Of Individual Tyre</b></th>
                                            <th>Tyre Sr.No.</th>

                                        </tr>
                                    </thead>
                                    <tbody id="tbody1">
                                        <tr id="tr3">
                                            <td>FRONT RIGHT</td>
                                            <td>
                                                <input type="text" id="txtFrontRightRunning" name="txtFrontRightRunning" required />
                                            </td>
                                            <td>
                                                <input type="text" id="txtFrontRightOldTyreNo" name="txtFrontRightOldTyreNo" required />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>FRONT LEFT</td>
                                            <td>
                                                <input type="text" id="txtFrontLeftRunning" name="txtFrontLeftRunning" required />
                                            </td>
                                            <td>
                                                <input type="text" id="txtFrontLeftOldTyreNo" name="txtFrontLeftOldTyreNo" required />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>REAR RIGHT</td>
                                            <td>
                                                <input type="text" id="txtRearRightRunning" name="txtRearRightRunning" required />
                                            </td>
                                            <td>
                                                <input type="text" id="txtRearRightOldTyreNo" name="txtRearRightOldTyreNo" required />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>REAR LEFT</td>
                                            <td>
                                                <input type="text" id="txtRearLeftRunning" name="txtRearLeftRunning" required />
                                            </td>
                                            <td>
                                                <input type="text" id="txtRearLeftOldTyreNo" name="txtRearLeftOldTyreNo" required />
                                            </td>
                                        </tr>
                                        <tr id="trRearLeftTwo" style="display: none;">
                                            <td>REAR LEFT TWO</td>
                                            <td>
                                                <input type="text" id="txtRearLeftTwoRunning" name="txtRearLeftTwoRunning" required disabled="disabled" />
                                            </td>
                                            <td>
                                                <input type="text" id="txtRearLeftTwoOldTyreNo" name="txtRearLeftTwoOldTyreNo" required disabled="disabled" />
                                            </td>
                                        </tr>
                                        <tr id="trRearRightTwo" style="display: none;">
                                            <td>REAR RIGHT TWO</td>
                                            <td>
                                                <input type="text" id="txtRearRightTwoRunning" name="txtRearRightTwoRunning" required />
                                            </td>
                                            <td>
                                                <input type="text" id="txtRearRightTwoOldTyreNo" name="txtRearRightTwoOldTyreNo" required />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>STAFNEY</td>
                                            <td>
                                                <input type="text" id="txtStafneyRunning" name="txtStafneyRunning" required />
                                            </td>
                                            <td>
                                                <input type="text" id="txtStafneyldTyreNo" name="txtStafneyldTyreNo" required />
                                            </td>
                                        </tr>


                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="form-actions" style="text-align: left;">
                    <input type="button" id="btnSaveVehicleService" value="Save" class="btn btn-primary" />
                    <input type="button" id="btnEdit" value="Edit" class="btn btn-primary" style="display: none;" />
                </div>
            </div>
        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-user"></i>Vehicle Service Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div id="divVehicleServiceInfo" runat="server">
                        <table id="grid" class='table table-striped table-bordered bootstrap-datatable datatable'>
                            <thead>
                                <tr>
                                    <th style="color: #cc3300;width:15%">Academy</th>
                                    <th style="color: #cc3300;width:15%">Vehicle Number</th>
                                    <th style="color: #cc3300;width:30%">Last Service</th>
                                    <th style="color: #cc3300;width:30%">Battery Detail</th>
                                    <th style="color: #cc3300; width:10%">Action</th>
                                </tr>
                            </thead>
                            <tbody id="tbody">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

