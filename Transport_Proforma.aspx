﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Transport_AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Transport_Proforma.aspx.cs" Inherits="Transport_Proforma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JavaScripts/TransportProforma.js"></script>
    <div id="content" class="span10">
        <asp:HiddenField ID="hdnInchargeID" runat="server" />
           <asp:HiddenField ID="hdnUserId" runat="server" />
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well">
                    <h2><i class="icon-edit"></i>Download Proforma</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <div class="control-group">

                        <div class="controls">
                            Select Proforma Type:
                                        <asp:DropDownList ID="ddlproforma" runat="server">
                                            <asp:ListItem Text="--Choose Proforma Type--" Selected="True" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="GENSET REAPIR AND SERVICE" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="BATTERY QUOTATION" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="TYRE REQUIREMENT OR  QUOTATION" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="SERVICE OTHER REAPIRS  OF VEHICLE" Value="4"></asp:ListItem>
                                        </asp:DropDownList><br />

                        </div>
                    </div>
                </div>
            </div>
            <!--/span-->

        </div>
        <div class="row-fluid sortable">
            <div class="box span12">
                <div class="box-header well" data-original-title>
                    <h2><i class="icon-user"></i>Proforma Details</h2>
                    <div class="box-icon">
                        <a href="#" class="btn btn-setting btn-round"><i class="icon-cog"></i></a>
                        <a href="#" class="btn btn-minimize btn-round"><i class="icon-chevron-up"></i></a>
                        <a href="#" class="btn btn-close btn-round"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <table>
                        <tr>
                            <td colspan="2">
                                <div id="pnlGenset" runat="server">
                                    <div id="divGenset" style="display: none;">
                                        <table style="width: 100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <span style="font-size: 15px; font-weight: bolder; margin-left: 250px;"><b>GENSET REAPIR AND SERVICE</b></span><br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table border="0">
                                                        <tr>
                                                            <td style="width: 120px"><b>AA NAME:</b></td>
                                                            <td>
                                                                <select id="ddlAcaName" name="ddlAcaName" required>
                                                                    <option value="">--Select Academy--</option>
                                                                </select>
                                                                <asp:HiddenField ID="hdnGensetAcaName" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><b>GANSET COMPANY:</b></td>
                                                            <td>
                                                                <input type="text" disabled="disabled" name="txtGansetCompany" id="txtGansetCompany" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 400px;"><b>GENSET SERIAL NUMBER:</b></td>
                                                            <td style="width: 400px">
                                                                <input type="text" name="txtGansetSrNumber" disabled="disabled" id="txtGansetSrNumber" required />
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td><b>GENSET POWER IN KVA:</b></td>
                                                            <td style="width: 400px">
                                                                <input type="text" name="txtGensetPower" disabled="disabled" id="txtGensetPower" required />
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td><b>DATE:</b></td>
                                                            <td style="width: 400px">
                                                                <input type="text" name="txtDate" disabled="disabled" id="txtDate" style="width: 210px" class="input-xlarge datepicker" required />
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td><b>LAST TIME REPAIR DATE:</b></td>
                                                            <td style="width: 400px">
                                                                <input type="text" name="txtLastRepairDate" disabled="disabled" id="txtLastRepairDate" style="width: 210px" class="input-xlarge datepicker" required />
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td><b>LAST TIME REPAIR AMOUNT:</b></td>
                                                            <td style="width: 400px">
                                                                <input type="text" name="txtLastRepairAmount" disabled="disabled" id="txtLastRepairAmount" required />
                                                            </td>

                                                        </tr>
                                                        <tr>

                                                            <td><b>QUOTATION AMOUNT:</b></td>
                                                            <td style="width: 400px">
                                                                <input type="text" name="txtQuotationAmount" disabled="disabled" id="txtQuotationAmount" required />
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td><b>APPROVAL AMOUNT OF SERVICE :</b></td>
                                                            <td style="width: 400px">
                                                                <input type="text" name="txtApprovalAmount" disabled="disabled" id="txtApprovalAmount" required />

                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td><b>TOTAL RUNNING OF GENSET (Hours) :</b></td>
                                                            <td style="width: 400px">
                                                                <input type="text" name="txtGensetTotalRunning" disabled="disabled" id="txtGensetTotalRunning" required />

                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td><b>AVERAGE RUNINIG:</b></td>
                                                            <td style="width: 400px">
                                                                <input type="text" name="txtGensetAverageRunning" disabled="disabled" id="txtGensetAverageRunning" required />
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td><b>SERVICE PLACE-AGENCY/OTHER:</b></td>
                                                            <td style="width: 400px">
                                                                <input type="text" name="txtService" disabled="disabled" id="txtService" required />

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 70px;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <span style="font-size: 15px; font-weight: bolder; margin-left: 250px;"><b>REQUIRED PARTS OF VEHICLE</b></span><br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="tblEstimateMatDetail" style="width: 100%;" class='table table-striped table-bordered'>
                                                        <thead>
                                                            <tr>
                                                                <th style="width: 10%"><b>S.No</b></th>
                                                                <th style="width: 50%"><b>PART NAME</b></th>
                                                                <th style="width: 20%"><b>QUANTITY OF PARTS</b></th>
                                                                <th style="width: 20%"><b>PRICE</b></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tbodyService">
                                                            <tr id="tr0">
                                                                <td>
                                                                    <span id="spn0">1</span>
                                                                </td>
                                                                <td>
                                                                    <input name="txtMaterialName0" id="txtMaterialName0" type="text" />

                                                                </td>
                                                                <td>
                                                                    <input id="txtQty0" name="txtQty0" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtRate0" name="txtRate0" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr1">
                                                                <td>
                                                                    <span id="Span1">2</span>
                                                                </td>
                                                                <td>
                                                                    <input name="txtMaterialName1" id="txtMaterialName1" type="text" />

                                                                </td>
                                                                <td>
                                                                    <input id="txtQty1" name="txtQty1" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtRate1" name="txtRate1" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr5">
                                                                <td>
                                                                    <span id="Span2">3</span>
                                                                </td>
                                                                <td>
                                                                    <input name="txtMaterialName2" id="txtMaterialName2" type="text" />

                                                                </td>
                                                                <td>
                                                                    <input id="txtQty2" name="txtQty2" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtRate2" name="txtRate2" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr6">
                                                                <td>
                                                                    <span id="Span3">4</span>
                                                                </td>
                                                                <td>
                                                                    <input name="txtMaterialName3" id="txtMaterialName3" type="text" />

                                                                </td>
                                                                <td>
                                                                    <input id="txtQty3" name="txtQty3" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtRate3" name="txtRate3" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr7">
                                                                <td>
                                                                    <span id="Span4">5</span>
                                                                </td>
                                                                <td>
                                                                    <input name="txtMaterialName4" id="txtMaterialName4" type="text" />

                                                                </td>
                                                                <td>
                                                                    <input id="txtQty4" name="txtQty4" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtRate4" name="txtRate4" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr15">
                                                                <td>
                                                                    <span id="Span19">6</span>
                                                                </td>
                                                                <td>
                                                                    <input name="txtMaterialName5" id="txtMaterialName5" type="text" />

                                                                </td>
                                                                <td>
                                                                    <input id="txtQty5" name="txtQty5" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtRate5" name="txtRate5" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr16">
                                                                <td>
                                                                    <span id="Span20">7</span>
                                                                </td>
                                                                <td>
                                                                    <input name="txtMaterialName6" id="txtMaterialName6" type="text" />

                                                                </td>
                                                                <td>
                                                                    <input id="txtQty6" name="txtQty6" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtRate6" name="txtRate6" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr17">
                                                                <td>
                                                                    <span id="Span21">8</span>
                                                                </td>
                                                                <td>
                                                                    <input name="txtMaterialName7" id="txtMaterialName7" type="text" />

                                                                </td>
                                                                <td>
                                                                    <input id="txtQty7" name="txtQty7" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtRate7" name="txtRate7" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr20">
                                                                <td>
                                                                    <span id="Span24">9</span>
                                                                </td>
                                                                <td>
                                                                    <input name="txtMaterialName8" id="txtMaterialName8" type="text" />

                                                                </td>
                                                                <td>
                                                                    <input id="txtQty8" name="txtQty8" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtRate8" name="txtRate8" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr21">
                                                                <td>
                                                                    <span id="Span25">10</span>
                                                                </td>
                                                                <td>
                                                                    <input name="txtMaterialName9" id="txtMaterialName9" type="text" />

                                                                </td>
                                                                <td>
                                                                    <input id="txtQty9" name="txtQty9" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtRate9" name="txtRate9" type="text" />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div id="pnlBatteryQuotation" runat="server">
                                    <div id="divBatteryQuotation" style="display: none;">
                                        <table style="width: 100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <span style="font-size: 15px; margin-left: 270px;"><b>FORMAT OF BATTERY QUOTATION</b></span><br />
                                                            </td>
                                                            <td><b>BILL NO:</b></td>
                                                            <td>
                                                                <input type="text" disabled="disabled" name="txtBillNo" id="txtBillNo" required /></td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table border="0">
                                                        <tr>
                                                            <td colspan="2">AA NAME</td>
                                                            <td colspan="2">
                                                                <select id="ddlBatteryAcaName" disabled="disabled" name="ddlBatteryAcaName" required>
                                                                    <option value="">--Select Academy--</option>
                                                                </select>
                                                                <asp:HiddenField ID="hdnBatteryAcaName" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">VEHICLE NUMBER</td>
                                                            <td colspan="2">
                                                                <select id="ddlVehicleNumber" disabled="disabled" name="ddlVehicleNumber" required>
                                                                    <option value="">--Select Vehicle--</option>
                                                                </select>
                                                                <asp:HiddenField ID="hdnBatteryVehicleNo" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">VEHICLE TYPE</td>
                                                            <td colspan="2">
                                                                <input type="text" disabled="disabled" id="txtVehicelType" name="txtVehicelType" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">CRUNNENT MERTER READING</td>
                                                            <td colspan="2">
                                                                <input type="text" disabled="disabled" id="txtCurrentMeterReading" name="txtCurrentMeterReading" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="height: 30px;"><b>NEW BATTERY REQUIREMENT OR QUOTATION</b></td>
                                                            <td colspan="2" style="height: 30px;"><b>OLD BATTERY DETAIL</b></td>
                                                        </tr>
                                                        <tr>
                                                            <td>NO. OF REQUIRED:</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtNoRequird" name="txtNoRequird" required />
                                                            </td>
                                                            <td>MAKE OF BATTERY/CAPACITY</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtBatteryCapacity" name="txtBatteryCapacity" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>NEW MAKE OF BATTERY</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtNewMakeBattery" name="txtNewMakeBattery" required />
                                                            </td>
                                                            <td>OLD BATTREY SERIAL NUMBER</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtOldBatterySrNum" name="txtOldBatterySrNum" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>NEW BATTREY CAPACITY</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtNewBatteryCapacity" name="txtNewBatteryCapacity" required />
                                                            </td>
                                                            <td>OLD PURCHASE DATE</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtPurchaseDate" style="width:210px;" name="txtPurchaseDate" class="input-xlarge datepicker" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>NEW BATTREY SERIAL NUMBER</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtNewBatterySrNum" name="txtNewBatterySrNum" required />
                                                            </td>
                                                            <td>OLD BATTERY SALE PRICE</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtOldBatterySale" name="txtOldBatterySale" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>NEW BATTERY LIFE IN YEARS</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtNewBatteryLife" name="txtNewBatteryLife" required />
                                                            </td>
                                                            <td>APPROVAL AMOUNT</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtBatteryApprovalAmount" name="txtBatteryApprovalAmount" required />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 70px;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <span style="font-size: 15px; font-weight: bolder; margin-left: 350px;"><b>BATTERY RATES</b></span><br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="Table2" style="width: 100%;" class='table table-striped table-bordered'>
                                                        <thead>
                                                            <tr>
                                                                <th><b>NAME OF COMPANY</b></th>
                                                                <th><b>SIZE OF BATTERY</b></th>
                                                                <th></th>
                                                                <th><b>PRICE OF BATTERY</b></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tbody2">
                                                            <tr id="tr2">
                                                                <td>MICROTEK</td>
                                                                <td>
                                                                    <input type="text" id="txtMocrotaxSize" name="txtMocrotaxSize" />
                                                                </td>

                                                                <td>
                                                                    <input type="text" id="txtMocrotax" name="txtMocrotax" />

                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtMocrotaxPrice" name="txtMocrotaxPrice" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>TATA AMARON</td>
                                                                <td>
                                                                    <input type="text" id="txtAmaronSize" name="txtAmaronSize" />
                                                                </td>

                                                                <td>
                                                                    <input type="text" id="txtAmaron" name="txtAmaron" />

                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtAmaronPrice" name="txtAmaronPrice" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>EXIDE</td>
                                                                <td>
                                                                    <input type="text" id="txtExideSize" name="txtExideSize" />
                                                                </td>

                                                                <td>
                                                                    <input type="text" id="txtExide" name="txtExide" />

                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtExidePrice" name="txtExidePrice" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>MICRO TECH</td>
                                                                <td>
                                                                    <input type="text" id="txtMicroTechSize" name="txtMicroTechSize" />
                                                                </td>

                                                                <td>
                                                                    <input type="text" id="txtMicroTech" name="txtMicroTech" />

                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtMicroTechPrice" name="txtMicroTechPrice" />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div id="pnlTyreRequirement" runat="server">
                                    <div id="divTyreRequirement" style="display: none;">
                                        <table style="width: 100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <span style="font-size: 15px; font-weight: bolder; margin-left: 157px;"><b>FORMAT OF VEHICLE TYRE REQUIREMENT OR QUOTATION</b></span><br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table border="0">
                                                        <tr>
                                                            <td colspan="2">NAME OF AKAL ACADEMY:<select disabled="disabled" name="ddlTyreAcaName" id="ddlTyreAcaName" required>
                                                                <option value="">--Select Academy--</option>
                                                            </select>
                                                                <asp:HiddenField ID="hdnTyreAcaName" runat="server" />
                                                            </td>
                                                            <td colspan="2">NAME OF DRIVER AND PHONE NUMBER:<input type="text" id="txtNameofDriver" name="txtNameofDriver" disabled="disabled" style="float: right; margin-right: 70px;" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">VEHICLE  NUMBER:<select disabled="disabled" id="ddlTyreVehicleNo" name="ddlTyreVehicleNo" style="float: right; margin-right: 8px;" required>
                                                                <option value="">--Select Vehicle--</option>
                                                            </select>
                                                                <asp:HiddenField ID="hdnTyreVehicleNo" runat="server" />
                                                            </td>
                                                            <td colspan="2">TYPE OF VEHICLES:<input type="text" disabled="disabled" id="txtTyreVehicleType" name="txtTyreVehicleType" style="float: right; margin-right: 70px;" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>TYRE SIZE</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtTyreSize" name="txtTyreSize" required />
                                                            </td>
                                                            <td>DATE</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtTyreDate" name="txtTyreDate" style="width: 210px;" class="input-xlarge datepicker" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>NO.OF TYRES REQUIRED</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtNoofRequird" name="txtNoofRequird" required />
                                                            </td>
                                                            <td>CURRENT METER READING</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtCurrentMeter" name="txtCurrentMeter" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>NEW TYRE AMOUNT</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtNewTyreAmount" name="txtNewTyreAmount" required />
                                                            </td>
                                                            <td>LAST METER READING OF TYRES CHANGE</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtTyreMeterReading" name="txtTyreMeterReading" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>OLD TYRE SALE AMOUNT</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtOldTyreSaleAmount" name="txtOldTyreSaleAmount" required />
                                                            </td>
                                                            <td>LAST DATE OF TYRE CHANGE</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" style="width: 210px;" id="txtTyreLastChangeDate" name="txtTyreLastChangeDate" class="input-xlarge datepicker" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>APPROVAL AMOUNT</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtTyreApproval" name="txtTyreApproval" required />
                                                            </td>
                                                            <td>TYRE CHANGE ON LAST READING (FRONT/REAR)</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtTyreLastReading" name="txtTyreLastReading" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td>TYRES TOTAL RUNING  KM</td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtTotalRuningKm" name="txtTotalRuningKm" required />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 70px;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <span style="font-size: 15px; font-weight: bolder; margin-left: 400px;"><b>TYRES RATE DETAIL</b></span><br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="Table4" style="width: 100%;" class='table table-striped table-bordered'>
                                                        <thead>
                                                            <tr>
                                                                <th><b>COMPANY NAM</b>E</th>
                                                                <th><b>RATES OF NEW TYRES</b></th>
                                                                <th><b>REQUIRED QTY</b></th>
                                                                <th><b>TOTAL AMOUNT </b></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tbody4">
                                                            <tr id="tr4">
                                                                <td>MRF TYRES</td>
                                                                <td>
                                                                    <input type="text" id="txtMrfRates" name="txtMrfRates" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtMrfQty" name="txtMrfQty" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtMrfAmount" name="txtMrfAmount" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>APPOLO TYRES</td>
                                                                <td>
                                                                    <input type="text" id="txtApoloRates" name="txtApoloRates" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtApoloQty" name="txtApoloQty" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtApoloAmount" name="txtApoloAmount" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>CEAT TYRES</td>
                                                                <td>
                                                                    <input type="text" id="txtCeatRates" name="txtCeatRates" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtCeatQty" name="txtCeatQty" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtCeatAmount" name="txtCeatAmount" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>JK TYRES</td>
                                                                <td>
                                                                    <input type="text" id="txtJkRates" name="txtJkRates" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtJkQty" name="txtJkQty" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtJkAmount" name="txtJkAmount" />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="height: 70px;">
                                                        <tr>
                                                            <td>
                                                                <span style="font-size: 15px; font-weight: bolder; margin-left: 320px;"><b>REQUIREMENT OF NEW TYRES  & OLD TYRES DETAIL</b></span><br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="Table3" style="width: 100%;" class='table table-striped table-bordered'>
                                                        <thead>
                                                            <tr>
                                                                <th></th>
                                                                <th><b>NEW TYRE REQUIRED</b></th>
                                                                <th>OLD TYRE CONDITION</th>
                                                                <th><b>KM Running of individual Tyre</b></th>
                                                                <th>OLD TYRE S.NO.</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tbody3">
                                                            <tr id="tr3">
                                                                <td>FRONT RIGHT</td>
                                                                <td>
                                                                    <input type="text" id="txtFrontRightRequired" name="txtFrontRightRequired" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtFrontRightCondition" name="txtFrontRightCondition" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtFrontRightRunning" name="txtFrontRightRunning" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtFrontRightOldTyreNo" name="txtFrontRightOldTyreNo" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>FRONT LEFT</td>
                                                                <td>
                                                                    <input type="text" id="txtFrontLeftRequired" name="txtFrontLeftRequired" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtFrontLeftCondition" name="txtFrontLeftCondition" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtFrontLeftRunning" name="txtFrontLeftRunning" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtFrontLeftOldTyreNo" name="txtFrontLeftOldTyreNo" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>REAR RIGHT</td>
                                                                <td>
                                                                    <input type="text" id="txtRearRightRequired" name="txtRearRightRequired" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtRearRightCondition" name="txtRearRightCondition" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtRearRightRunning" name="txtRearRightRunning" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtRearRightOldTyreNo" name="txtRearRightOldTyreNo" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>REAR LEFT</td>
                                                                <td>
                                                                    <input type="text" id="txtRearLeftRequired" name="txtRearLeftRequired" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtRearLeftCondition" name="txtRearLeftCondition" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtRearLeftRunning" name="txtRearLeftRunning" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtRearLeftOldTyreNo" name="txtRearLeftOldTyreNo" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>STAFNEY</td>
                                                                <td>
                                                                    <input type="text" id="txtStafneyRequired" name="txtStafneyRequired" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtStafneyCondition" name="txtStafneyCondition" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtStafneyRunning" name="txtStafneyRunning" />
                                                                </td>
                                                                <td>
                                                                    <input type="text" id="txtStafneyldTyreNo" name="txtStafneyldTyreNo" />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div id="pnlServiceandOtherRepair" runat="server">
                                    <div id="divServiceandOtherRepair" style="display: none;">
                                        <table style="width: 100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <span style="font-size: 15px; font-weight: bolder; margin-left: 250px;"><b>SERVICE/OTHER REAPIRS  OF VEHICLE</b></span><br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table border="0">
                                                        <tr>
                                                            <td><span id="Span5">1.</span></td>
                                                            <td style="width: 400px;"><b>AA NAME:</b></td>
                                                            <td>
                                                                <select disabled="disabled" id="ddlServiceAcaName" name="ddlServiceAcaName" required>
                                                                    <option value="">--Select Academy--</option>
                                                                </select>
                                                                <asp:HiddenField ID="hdnServiceAcaName" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><span id="Span6">3.</span></td>
                                                            <td><b>VEHICLE NO./SEATED/ MODEL</b></td>
                                                            <td>
                                                                <select disabled="disabled" id="ddlServiceVehicleNo" name="ddlServiceVehicleNo" required>
                                                                    <option value="">--Select Vehicle--</option>
                                                                </select>
                                                                <asp:HiddenField ID="hdnServiceVehicleNo" runat="server" />
                                                            </td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtSeated" name="txtSeated" required />
                                                            </td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtModel" name="txtModel" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><span id="Span7">5.</span></td>
                                                            <td style="width: 400px;"><b>CURRENT METER READING /LAST METER READING</b></td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtSrvicCurntMetrReading" name="txtSrvicCurntMetrReading" required />
                                                            </td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtSrvicLastMetrReading" name="txtSrvicLastMetrReading" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><span id="Span8">7.</span></td>
                                                            <td><b>QUOTATION AMOUNT</b></td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtServiceQuotationAmount" name="txtServiceQuotationAmount" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><span id="Span9">8</span></td>
                                                            <td><b>APPROVAL AMOUNT</b></td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtServiceApprovalAmount" name="txtServiceApprovalAmount" required />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><span id="Span10">9.</span></td>
                                                            <td><b>AVERAGE OF VEHICLE</b></td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtAvergeVehicle" name="txtAvergeVehicle" required />
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td><span id="Span13">10.</span></td>
                                                            <td><b>SERVICE PLACE-AGENCY/OTHER/BILL NO.</b></td>
                                                            <td>
                                                                <input type="text" disabled="disabled" id="txtServicePlace" name="txtServicePlace" required />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 70px;">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <span style="font-size: 15px; font-weight: bolder; margin-left: 400px;"><b>REQUIRED PARTS OF DETAIL</b></span><br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="tblServiceMatDetail" style="width: 100%;" class='table table-striped table-bordered'>
                                                        <thead>
                                                            <tr>
                                                                <th style="width: 5%;"><b>S.No</b></th>
                                                                <th style="width: 15%;"><b>PART NAME</b></th>
                                                                <th style="width: 15%;"><b>QUANTITY OF PARTS</b></th>
                                                                <th style="width: 15%;"><b>PRICE</b></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tbody1d">
                                                            <tr id="tr1d0">
                                                                <td>
                                                                    <span id="Span1d0">1</span>
                                                                </td>
                                                                <td>
                                                                    <input id="txtMaterial0" name="txtMaterial0" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtQuantity0" name="txtQuantity0" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtPrice0" name="txtPrice0" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr8">
                                                                <td>
                                                                    <span id="Span11">2</span>
                                                                </td>
                                                                <td>
                                                                    <input id="txtMaterial1" name="txtMaterial1" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtQuantity1" name="txtQuantity1" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtPrice1" name="txtPrice1" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr9">
                                                                <td>
                                                                    <span id="Span12">3</span>
                                                                </td>
                                                                <td>
                                                                    <input id="txtMaterial2" name="txtMaterial2" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtQuantity2" name="txtQuantity2" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtPrice2" name="txtPrice2" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr10">
                                                                <td>
                                                                    <span id="Span14">4</span>
                                                                </td>
                                                                <td>
                                                                    <input id="txtMaterial3" name="txtMaterial3" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtQuantity3" name="txtQuantity3" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtPrice3" name="txtPrice3" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr11">
                                                                <td>
                                                                    <span id="Span15">5</span>
                                                                </td>
                                                                <td>
                                                                    <input id="txtMaterial4" name="txtMaterial4" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtQuantity4" name="txtQuantity4" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtPrice4" name="txtPrice4" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr12">
                                                                <td>
                                                                    <span id="Span16">6</span>
                                                                </td>
                                                                <td>
                                                                    <input id="txtMaterial5" name="txtMaterial5" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtQuantity5" name="txtQuantity5" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtPrice5" name="txtPrice5" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr13">
                                                                <td>
                                                                    <span id="Span17">7</span>
                                                                </td>
                                                                <td>
                                                                    <input id="txtMaterial6" name="txtMaterial6" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtQuantity6" name="txtQuantity6" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtPrice6" name="txtPrice6" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr14">
                                                                <td>
                                                                    <span id="Span18">8</span>
                                                                </td>
                                                                <td>
                                                                    <input id="txtMaterial7" name="txtMaterial7" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtQuantity7" name="txtQuantity7" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtPrice7" name="txtPrice7" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr18">
                                                                <td>
                                                                    <span id="Span22">9</span>
                                                                </td>
                                                                <td>
                                                                    <input id="txtMaterial8" name="txtMaterial8" type="text" />

                                                                </td>
                                                                <td>
                                                                    <input id="txtQuantity8" name="txtQuantity8" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtPrice8" name="txtPrice8" type="text" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr19">
                                                                <td>
                                                                    <span id="Span23">10</span>
                                                                </td>
                                                                <td>
                                                                    <input id="txtMaterial9" name="txtMaterial9" type="text" />

                                                                </td>
                                                                <td>
                                                                    <input id="txtQuantity9" name="txtQuantity9" type="text" />
                                                                </td>
                                                                <td>
                                                                    <input id="txtPrice9" name="txtPrice9" type="text" />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr id="trbtnDownload" style="display: none;">
                            <td>
                                <asp:Button ID="btnDownload" runat="server" Text="Download Proforma" CssClass="btn btn-primary" OnClick="btnDownload_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!--/span-->

        </div>
    </div>


</asp:Content>
