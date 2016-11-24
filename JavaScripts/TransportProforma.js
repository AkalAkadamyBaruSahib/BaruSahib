
var MaterialList = new Array();
var MaterialObjectList;
var MaterialList;
$(document).ready(function () {

    BindAcademyByInchargeID($("input[id*='hdnInchargeID']").val());

    $("select[id*='ddlTyreAcaName']").change(function () {
        $("input[id*='hdnTyreAcaName']").val($("#ddlTyreAcaName option:selected").text());
        BindVechileByAcaID($(this).val());
    });

    $("select[id*='ddlServiceAcaName']").change(function () {
        BindVechileByAcaID($(this).val());
        $("input[id*='hdnServiceAcaName']").val($("#ddlServiceAcaName option:selected").text());
    });

    $("select[id*='ddlBatteryAcaName']").change(function () {
        $("input[id*='hdnBatteryAcaName']").val($("#ddlBatteryAcaName option:selected").text());
        BindVechileByAcaID($(this).val());
    });

    $("select[id*='ddlServiceVehicleNo']").change(function () {
        BindSeatedAndModelByVehicleID($(this).val());
        $("input[id*='hdnServiceVehicleNo']").val($("#ddlServiceVehicleNo option:selected").text());

    });

    $("select[id*='ddlTyreVehicleNo']").change(function () {
        $("input[id*='hdnTyreVehicleNo']").val($("#ddlTyreVehicleNo option:selected").text());
        BindSeatedAndModelByVehicleID($(this).val());
    });

    $("select[id*='ddlVehicleNumber']").change(function () {
        BindSeatedAndModelByVehicleID($(this).val());
        $("input[id*='hdnBatteryVehicleNo']").val($("#ddlVehicleNumber option:selected").text());
    });

    $("select[id*='ddlAcaName']").change(function () {
        $("input[id*='hdnGensetAcaName']").val($("#ddlAcaName option:selected").text());
    });

    GetMaterials(2);

    $("select[id*='ddlproforma']").change(function () {
        if ($(this).val() == 1) {
            $("#divGenset").show();
            $("#divBatteryQuotation").hide();
            $("#divTyreRequirement").hide();
            $("#divServiceandOtherRepair").hide();
            $("#trbtnDownload").show();
            $("#ddlAcaName").prop('disabled', false);
            $("#txtGansetCompany").prop('disabled', false);
            $("#txtGansetSrNumber").prop('disabled', false);
            $("#txtGensetPower").prop('disabled', false);
            $("#txtDate").prop('disabled', false);
            $("#txtLastRepairDate").prop('disabled', false);
            $("#txtLastRepairAmount").prop('disabled', false);
            $("#txtQuotationAmount").prop('disabled', false);
            $("#txtGensetTotalRunning").prop('disabled', false);
            $("#txtService").prop('disabled', false);
            $("#txtGensetAverageRunning").prop('disabled', false);
            $("#txtMaterialName0").prop('disabled', false);
            $("#txtQty0").prop('disabled', false);
            $("#txtRate0").prop('disabled', false);
            $("#ddlBatteryAcaName").prop('disabled', true);
            $("#ddlVehicleNumber").prop('disabled', true);
            $("#txtVehicelType").prop('disabled', true);
            $("#txtCurrentMeterReading").prop('disabled', true);
            $("#txtBatteryCapacity").prop('disabled', true);
            $("#txtNewMakeBattery").prop('disabled', true);
            $("#txtNewBatteryCapacity").prop('disabled', true);
            $("#txtPurchaseDate").prop('disabled', true);
            $("#txtNewBatterySrNum").prop('disabled', true);
            $("#txtOldBatterySale").prop('disabled', true);
            $("#txtNewBatteryLife").prop('disabled', true);
            $("#txtNoRequird").prop('disabled', true);
            $("#txtOldBatterySrNum").prop('disabled', true);
            $("#txtBatteryApprovalAmount").prop('disabled', true);
            $("#ddlTyreAcaName").prop('disabled', true);
            $("#txtNameofDriver").prop('disabled', true);
            $("#ddlTyreVehicleNo").prop('disabled', true);
            $("#txtTyreVehicleType").prop('disabled', true);
            $("#txtTyreSize").prop('disabled', true);
            $("#txtTyreDate").prop('disabled', true);
            $("#txtNoofRequird").prop('disabled', true);
            $("#txtCurrentMeter").prop('disabled', true);
            $("#txtNewTyreAmount").prop('disabled', true);
            $("#txtTyreMeterReading").prop('disabled', true);
            $("#txtOldTyreSaleAmount").prop('disabled', true);
            $("#txtTyreLastChangeDate").prop('disabled', true);
            $("#txtTyreApproval").prop('disabled', true);
            $("#txtTotalRuningKm").prop('disabled', true);
            $("#txtTyreLastReading").prop('disabled', true);
            $("#ddlServiceAcaName").prop('disabled', true);
            $("#ddlServiceVehicleNo").prop('disabled', true);
            $("#txtSeated").prop('disabled', true);
            $("#txtModel").prop('disabled', true);
            $("#txtSrvicCurntMetrReading").prop('disabled', true);
            $("#txtSrvicLastMetrReading").prop('disabled', true);
            $("#txtServiceQuotationAmount").prop('disabled', true);
            $("#txtAvergeVehicle").prop('disabled', true);
            $("#txtServicePlace").prop('disabled', true);
            $("#txtServiceApprovalAmount").prop('disabled', true);
            $("#txtMaterial0").prop('disabled', true);
            $("#txtQuantity0").prop('disabled', true);
            $("#txtPrice0").prop('disabled', true); 
            $("#txtBillNo").prop('disabled', true);
            $("#txtEngineerName").prop('disabled', false);
            $("#txtEngineerNumber").prop('disabled', false);
            $("#txtBatterySeated").prop('disabled', true);
            $("#txtBatteryModel").prop('disabled', true);
            $("#txtBatteryDriverandNumber").prop('disabled', true);
            $("#txtServiceDriverandNumber").prop('disabled', true);
            $("#txtServiceVehicelType").prop('disabled', true);
            ClearTextBox();
        }
        else if ($(this).val() == 2) {
            $("#divGenset").hide();
            $("#divBatteryQuotation").show();
            $("#divTyreRequirement").hide();
            $("#divServiceandOtherRepair").hide();
            $("#trbtnDownload").show();
            $("#ddlBatteryAcaName").prop('disabled', false);
            $("#ddlVehicleNumber").prop('disabled', false);
            $("#txtVehicelType").prop('disabled', false);
            $("#txtCurrentMeterReading").prop('disabled', false);
            $("#txtBatteryCapacity").prop('disabled', false);
            $("#txtNewMakeBattery").prop('disabled', false);
            $("#txtNewBatteryCapacity").prop('disabled', false);
            $("#txtPurchaseDate").prop('disabled', false);
            $("#txtNewBatterySrNum").prop('disabled', false);
            $("#txtOldBatterySale").prop('disabled', false);
            $("#txtNewBatteryLife").prop('disabled', false);
            $("#txtNoRequird").prop('disabled', false);
            $("#txtOldBatterySrNum").prop('disabled', false);
            $("#txtBatteryApprovalAmount").prop('disabled', false);
            $("#ddlTyreAcaName").prop('disabled', true);
            $("#txtNameofDriver").prop('disabled', true);
            $("#ddlTyreVehicleNo").prop('disabled', true);
            $("#txtTyreVehicleType").prop('disabled', true);
            $("#txtTyreSize").prop('disabled', true);
            $("#txtTyreDate").prop('disabled', true);
            $("#txtNoofRequird").prop('disabled', true);
            $("#txtCurrentMeter").prop('disabled', true);
            $("#txtNewTyreAmount").prop('disabled', true);
            $("#txtTyreMeterReading").prop('disabled', true);
            $("#txtOldTyreSaleAmount").prop('disabled', true);
            $("#txtTyreLastChangeDate").prop('disabled', true);
            $("#txtTyreApproval").prop('disabled', true);
            $("#txtTotalRuningKm").prop('disabled', true);
            $("#txtTyreLastReading").prop('disabled', true);
            $("#ddlServiceAcaName").prop('disabled', true);
            $("#ddlServiceVehicleNo").prop('disabled', true);
            $("#txtSeated").prop('disabled', true);
            $("#txtModel").prop('disabled', true);
            $("#txtSrvicCurntMetrReading").prop('disabled', true);
            $("#txtSrvicLastMetrReading").prop('disabled', true);
            $("#txtServiceQuotationAmount").prop('disabled', true);
            $("#txtAvergeVehicle").prop('disabled', true);
            $("#txtServicePlace").prop('disabled', true);
            $("#txtServiceApprovalAmount").prop('disabled', true);
            $("#txtMaterial0").prop('disabled', true);
            $("#txtQuantity0").prop('disabled', true);
            $("#txtPrice0").prop('disabled', true);
            $("#ddlAcaName").prop('disabled', true);
            $("#txtGansetCompany").prop('disabled', true);
            $("#txtGansetSrNumber").prop('disabled', true);
            $("#txtGensetPower").prop('disabled', true);
            $("#txtDate").prop('disabled', true);
            $("#txtLastRepairDate").prop('disabled', true);
            $("#txtLastRepairAmount").prop('disabled', true);
            $("#txtQuotationAmount").prop('disabled', true);
            $("#txtApprovalAmount").prop('disabled', true);
            $("#txtGensetTotalRunning").prop('disabled', true);
            $("#txtService").prop('disabled', true);
            $("#txtGensetAverageRunning").prop('disabled', true);
            $("#txtMaterialName0").prop('disabled', true);
            $("#txtQty0").prop('disabled', true);
            $("#txtRate0").prop('disabled', true);
            $("#txtBillNo").prop('disabled', false);
            $("#txtEngineerName").prop('disabled', true);
            $("#txtEngineerNumber").prop('disabled', true);
            $("#txtBatterySeated").prop('disabled', false);
            $("#txtBatteryModel").prop('disabled', false);
            $("#txtBatteryDriverandNumber").prop('disabled', false);
            $("#txtServiceDriverandNumber").prop('disabled', true);
            $("#txtServiceVehicelType").prop('disabled', true);
            ClearTextBox();
        }
        else if ($(this).val() == 3) {
            $("#divGenset").hide();
            $("#divBatteryQuotation").hide();
            $("#divTyreRequirement").show();
            $("#divServiceandOtherRepair").hide();
            $("#trbtnDownload").show();
            $("#ddlTyreAcaName").prop('disabled', false);
            $("#txtNameofDriver").prop('disabled', false);
            $("#ddlTyreVehicleNo").prop('disabled', false);
            $("#txtTyreVehicleType").prop('disabled', false);
            $("#txtTyreSize").prop('disabled', false);
            $("#txtTyreDate").prop('disabled', false);
            $("#txtNoofRequird").prop('disabled', false);
            $("#txtCurrentMeter").prop('disabled', false);
            $("#txtNewTyreAmount").prop('disabled', false);
            $("#txtTyreMeterReading").prop('disabled', false);
            $("#txtOldTyreSaleAmount").prop('disabled', false);
            $("#txtTyreLastChangeDate").prop('disabled', false);
            $("#txtTyreApproval").prop('disabled', false);
            $("#txtTotalRuningKm").prop('disabled', false);
            $("#txtTyreLastReading").prop('disabled', false);
            $("#ddlAcaName").prop('disabled', true);
            $("#txtGansetCompany").prop('disabled', true);
            $("#txtGansetSrNumber").prop('disabled', true);
            $("#txtGensetPower").prop('disabled', true);
            $("#txtDate").prop('disabled', true);
            $("#txtLastRepairDate").prop('disabled', true);
            $("#txtLastRepairAmount").prop('disabled', true);
            $("#txtQuotationAmount").prop('disabled', true);
            $("#txtApprovalAmount").prop('disabled', true);
            $("#txtGensetTotalRunning").prop('disabled', true);
            $("#txtService").prop('disabled', true);
            $("#txtGensetAverageRunning").prop('disabled', true);
            $("#txtMaterialName0").prop('disabled', true);
            $("#txtQty0").prop('disabled', true);
            $("#txtRate0").prop('disabled', true);
            $("#ddlBatteryAcaName").prop('disabled', true);
            $("#ddlVehicleNumber").prop('disabled', true);
            $("#txtVehicelType").prop('disabled', true);
            $("#txtCurrentMeterReading").prop('disabled', true);
            $("#txtBatteryCapacity").prop('disabled', true);
            $("#txtNewMakeBattery").prop('disabled', true);
            $("#txtNewBatteryCapacity").prop('disabled', true);
            $("#txtPurchaseDate").prop('disabled', true);
            $("#txtNewBatterySrNum").prop('disabled', true);
            $("#txtOldBatterySale").prop('disabled', true);
            $("#txtNewBatteryLife").prop('disabled', true);
            $("#txtNoRequird").prop('disabled', true);
            $("#txtOldBatterySrNum").prop('disabled', true);
            $("#txtBatteryApprovalAmount").prop('disabled', true);
            $("#ddlServiceAcaName").prop('disabled', true);
            $("#ddlServiceVehicleNo").prop('disabled', true);
            $("#txtSeated").prop('disabled', true);
            $("#txtModel").prop('disabled', true);
            $("#txtSrvicCurntMetrReading").prop('disabled', true);
            $("#txtSrvicLastMetrReading").prop('disabled', true);
            $("#txtServiceQuotationAmount").prop('disabled', true);
            $("#txtAvergeVehicle").prop('disabled', true);
            $("#txtServicePlace").prop('disabled', true);
            $("#txtServiceApprovalAmount").prop('disabled', true);
            $("#txtMaterial0").prop('disabled', true);
            $("#txtQuantity0").prop('disabled', true);
            $("#txtPrice0").prop('disabled', true);
            $("#txtBillNo").prop('disabled', true);
            $("#txtEngineerName").prop('disabled', true);
            $("#txtEngineerNumber").prop('disabled', true);
            $("#txtBatterySeated").prop('disabled', true);
            $("#txtBatteryModel").prop('disabled', true);
            $("#txtBatteryDriverandNumber").prop('disabled', true);
            $("#txtServiceDriverandNumber").prop('disabled', true);
            $("#txtServiceVehicelType").prop('disabled', true);
            ClearTextBox();
        }
        else if ($(this).val() == 4) {
            $("#divGenset").hide();
            $("#divBatteryQuotation").hide();
            $("#divTyreRequirement").hide();
            $("#divServiceandOtherRepair").show();
            $("#trbtnDownload").show();
            $("#ddlServiceAcaName").prop('disabled', false);
            $("#ddlServiceVehicleNo").prop('disabled', false);
            $("#txtSeated").prop('disabled', false);
            $("#txtModel").prop('disabled', false);
            $("#txtSrvicCurntMetrReading").prop('disabled', false);
            $("#txtSrvicLastMetrReading").prop('disabled', false);
            $("#txtServiceQuotationAmount").prop('disabled', false);
            $("#txtAvergeVehicle").prop('disabled', false);
            $("#txtServicePlace").prop('disabled', false);
            $("#txtServiceApprovalAmount").prop('disabled', false);
            $("#txtMaterial0").prop('disabled', false);
            $("#txtQuantity0").prop('disabled', false);
            $("#txtPrice0").prop('disabled', false);
            $("#ddlAcaName").prop('disabled', true);
            $("#txtGansetCompany").prop('disabled', true);
            $("#txtGansetSrNumber").prop('disabled', true);
            $("#txtGensetPower").prop('disabled', true);
            $("#txtDate").prop('disabled', true);
            $("#txtLastRepairDate").prop('disabled', true);
            $("#txtLastRepairAmount").prop('disabled', true);
            $("#txtQuotationAmount").prop('disabled', true);
            $("#txtApprovalAmount").prop('disabled', true);
            $("#txtGensetTotalRunning").prop('disabled', true);
            $("#txtService").prop('disabled', true);
            $("#txtGensetAverageRunning").prop('disabled', true);
            $("#txtMaterialName0").prop('disabled', true);
            $("#txtQty0").prop('disabled', true);
            $("#txtRate0").prop('disabled', true);
            $("#ddlBatteryAcaName").prop('disabled', true);
            $("#ddlVehicleNumber").prop('disabled', true);
            $("#txtVehicelType").prop('disabled', true);
            $("#txtCurrentMeterReading").prop('disabled', true);
            $("#txtBatteryCapacity").prop('disabled', true);
            $("#txtNewMakeBattery").prop('disabled', true);
            $("#txtNewBatteryCapacity").prop('disabled', true);
            $("#txtPurchaseDate").prop('disabled', true);
            $("#txtNewBatterySrNum").prop('disabled', true);
            $("#txtOldBatterySale").prop('disabled', true);
            $("#txtNewBatteryLife").prop('disabled', true);
            $("#txtNoRequird").prop('disabled', true);
            $("#txtOldBatterySrNum").prop('disabled', true);
            $("#txtBatteryApprovalAmount").prop('disabled', true);
            $("#ddlTyreAcaName").prop('disabled', true);
            $("#txtNameofDriver").prop('disabled', true);
            $("#ddlTyreVehicleNo").prop('disabled', true);
            $("#txtTyreVehicleType").prop('disabled', true);
            $("#txtTyreSize").prop('disabled', true);
            $("#txtTyreDate").prop('disabled', true);
            $("#txtNoofRequird").prop('disabled', true);
            $("#txtCurrentMeter").prop('disabled', true);
            $("#txtNewTyreAmount").prop('disabled', true);
            $("#txtTyreMeterReading").prop('disabled', true);
            $("#txtOldTyreSaleAmount").prop('disabled', true);
            $("#txtTyreLastChangeDate").prop('disabled', true);
            $("#txtTyreApproval").prop('disabled', true);
            $("#txtTotalRuningKm").prop('disabled', true);
            $("#txtTyreLastReading").prop('disabled', true);
            $("#txtBillNo").prop('disabled', true);
            $("#txtEngineerName").prop('disabled', true);
            $("#txtEngineerNumber").prop('disabled', true);
            $("#txtBatterySeated").prop('disabled', true);
            $("#txtBatteryModel").prop('disabled', true);
            $("#txtBatteryDriverandNumber").prop('disabled', true);
            $("#txtServiceDriverandNumber").prop('disabled', false);
            $("#txtServiceVehicelType").prop('disabled', false);
            ClearTextBox();
        }
        else {
            $("#divGenset").hide();
            $("#divBatteryQuotation").hide();
            $("#divTyreRequirement").hide();
            $("#divServiceandOtherRepair").hide();
            $("#trbtnDownload").hide();
            $("#ddlAcaName").prop('disabled', true);
            $("#txtGansetCompany").prop('disabled', true);
            $("#txtGansetSrNumber").prop('disabled', true);
            $("#txtGensetPower").prop('disabled', true);
            $("#txtDate").prop('disabled', true);
            $("#txtLastRepairDate").prop('disabled', true);
            $("#txtLastRepairAmount").prop('disabled', true);
            $("#txtQuotationAmount").prop('disabled', true);
            $("#txtApprovalAmount").prop('disabled', true);
            $("#txtGensetTotalRunning").prop('disabled', true);
            $("#txtService").prop('disabled', true);
            $("#txtGensetAverageRunning").prop('disabled', true);
            $("#txtMaterialName0").prop('disabled', true);
            $("#txtQty0").prop('disabled', true);
            $("#txtRate0").prop('disabled', true);
            $("#ddlBatteryAcaName").prop('disabled', true);
            $("#ddlVehicleNumber").prop('disabled', true);
            $("#txtVehicelType").prop('disabled', true);
            $("#txtCurrentMeterReading").prop('disabled', true);
            $("#txtBatteryCapacity").prop('disabled', true);
            $("#txtNewMakeBattery").prop('disabled', true);
            $("#txtNewBatteryCapacity").prop('disabled', true);
            $("#txtPurchaseDate").prop('disabled', true);
            $("#txtNewBatterySrNum").prop('disabled', true);
            $("#txtOldBatterySale").prop('disabled', true);
            $("#txtNewBatteryLife").prop('disabled', true);
            $("#txtNoRequird").prop('disabled', true);
            $("#txtOldBatterySrNum").prop('disabled', true);
            $("#txtBatteryApprovalAmount").prop('disabled', true);
            $("#ddlTyreAcaName").prop('disabled', true);
            $("#txtNameofDriver").prop('disabled', true);
            $("#ddlTyreVehicleNo").prop('disabled', true);
            $("#txtTyreVehicleType").prop('disabled', true);
            $("#txtTyreSize").prop('disabled', true);
            $("#txtTyreDate").prop('disabled', true);
            $("#txtNoofRequird").prop('disabled', true);
            $("#txtCurrentMeter").prop('disabled', true);
            $("#txtNewTyreAmount").prop('disabled', true);
            $("#txtTyreMeterReading").prop('disabled', true);
            $("#txtOldTyreSaleAmount").prop('disabled', true);
            $("#txtTyreLastChangeDate").prop('disabled', true);
            $("#txtTyreApproval").prop('disabled', true);
            $("#txtTotalRuningKm").prop('disabled', true);
            $("#txtTyreLastReading").prop('disabled', true);
            $("#ddlServiceAcaName").prop('disabled', true);
            $("#ddlServiceVehicleNo").prop('disabled', true);
            $("#txtSeated").prop('disabled', true);
            $("#txtModel").prop('disabled', true);
            $("#txtSrvicCurntMetrReading").prop('disabled', true);
            $("#txtSrvicLastMetrReading").prop('disabled', true);
            $("#txtServiceQuotationAmount").prop('disabled', true);
            $("#txtAvergeVehicle").prop('disabled', true);
            $("#txtServicePlace").prop('disabled', true);
            $("#txtServiceApprovalAmount").prop('disabled', true);
            $("#txtMaterial0").prop('disabled', true);
            $("#txtQuantity0").prop('disabled', true);
            $("#txtPrice0").prop('disabled', true);
            $("#txtBillNo").prop('disabled', true);
            $("#txtEngineerName").prop('disabled', true);
            $("#txtEngineerNumber").prop('disabled', true);
            $("#txtBatterySeated").prop('disabled', true);
            $("#txtBatteryModel").prop('disabled', true);
            $("#txtBatteryDriverandNumber").prop('disabled', true);
            $("#txtServiceDriverandNumber").prop('disabled', true);
            $("#txtServiceVehicelType").prop('disabled', true);
            ClearTextBox();
        }
    });

    $("input[id*='txtQty0']").change(function () {
        var value = $("#txtQty0").val()
        var regex = new RegExp(/^\+?[0-9(),.-]+$/);
        if ($("#txtQty0").val() == "" || $("#txtQty0").val() == "0" || !value.match(regex)) {
            $("#txtQty0").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtQty0" + i).css('border-color', '');
        }
    });
});

function BindAcademyByInchargeID(inchargeId) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetAcademyByInchargeID",
       data: JSON.stringify({ InchargeID: parseInt(inchargeId) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("select[id*='ddlAcaName']").append($("<option></option>").val(value.AcaID).html(value.AcaName));
                    $("select[id*='ddlBatteryAcaName']").append($("<option></option>").val(value.AcaID).html(value.AcaName));
                    $("select[id*='ddlTyreAcaName']").append($("<option></option>").val(value.AcaID).html(value.AcaName));
                    $("select[id*='ddlServiceAcaName']").append($("<option></option>").val(value.AcaID).html(value.AcaName));
                
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindVechileByAcaID(acaId) {

    $("select[id*='ddlTyreVehicleNo'] option").each(function (index, option) {
        $(option).remove();
    });
    $("select[id*='ddlVehicleNumber'] option").each(function (index, option) {
        $(option).remove();
    });

    $("select[id*='ddlServiceVehicleNo'] option").each(function (index, option) {
        $(option).remove();
    });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetVehiclesByAcademyID",
        data: JSON.stringify({ AcaID: parseInt(acaId) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $("select[id*='ddlTyreVehicleNo']").append($("<option></option>").val("0").html("--Select Vehicle--"));
                $("select[id*='ddlVehicleNumber']").append($("<option></option>").val("0").html("--Select Vehicle--"));
                $("select[id*='ddlServiceVehicleNo']").append($("<option></option>").val("0").html("--Select Vehicle--"));
                $.each(Result, function (key, value) {
                    if ($("select[id*='ddlproforma']").val() == 2) {
                        $("select[id*='ddlVehicleNumber']").append($("<option></option>").val(value.ID).html(value.Number));
                    }
                    else if ($("select[id*='ddlproforma']").val() == 3) {
                        $("select[id*='ddlTyreVehicleNo']").append($("<option></option>").val(value.ID).html(value.Number));
                    }
                    else if ($("select[id*='ddlproforma']").val() == 4) {
                        $("select[id*='ddlServiceVehicleNo']").append($("<option></option>").val(value.ID).html(value.Number));
                    }
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindSeatedAndModelByVehicleID(vehicleID) {

    $("input[id*='txtSeated']").val("");
    $("input[id*='txtModel']").val("");
    $("input[id*='txtVehicelType']").val("");
    $("input[id*='txtNameofDriver']").val("");
    $("input[id*='txtTyreVehicleType']").val("");

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetVehiclesInfoByVehicleID",
        data: JSON.stringify({ VehicleID: parseInt(vehicleID) }),
        dataType: "json",
        success: function (responce) {
            var rdata = responce.d;
            if (rdata != undefined) {
                if ($("select[id*='ddlproforma']").val() == 2) {
                    $("input[id*='txtVehicelType']").val(rdata.TransportTypes.Type);
                    $("input[id*='txtBatterySeated']").val(rdata.Sitter);
                    $("input[id*='txtBatteryModel']").val(rdata.Model);
                }
                else if ($("select[id*='ddlproforma']").val() == 3) {
                    $("input[id*='txtTyreVehicleType']").val(rdata.TransportTypes.Type);
                }
                else if ($("select[id*='ddlproforma']").val() == 4) {
                    $("input[id*='txtSeated']").val(rdata.Sitter);
                    $("input[id*='txtModel']").val(rdata.Model);
                    $("input[id*='txtServiceVehicelType']").val(rdata.TransportTypes.Type);
                }
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetVehicleEmployeeInfo",
        data: JSON.stringify({ VehicleID: parseInt(vehicleID), EmpType: parseInt(1) }),
        dataType: "json",
        success: function (responce) {
            var rdata = responce.d;
            if (rdata != undefined) {
                if ($("select[id*='ddlproforma']").val() == 3) {
                    $("input[id*='txtNameofDriver']").val(rdata.Name + "(" + rdata.MobileNumber + ")");
                }
                else if ($("select[id*='ddlproforma']").val() == 2) {
                    $("input[id*='txtBatteryDriverandNumber']").val(rdata.Name + "(" + rdata.MobileNumber + ")");
                }
                else if ($("select[id*='ddlproforma']").val() == 4) {
                    $("input[id*='txtServiceDriverandNumber']").val(rdata.Name + "(" + rdata.MobileNumber + ")");
                }
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetMaterials(sourceTypeID) {
    $.when(GetMaterialsAjax(sourceTypeID)
   ).done
    (GetMaterialsFromMaterialObject(sourceTypeID));
}

function GetMaterialsAjax(sourceTypeID) {
    if (MaterialObjectList == undefined || MaterialObjectList.length == 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ sourceTypeID: parseInt(sourceTypeID) }),
            url: "Services/PurchaseControler.asmx/GetMaterialsBySourceTypeIDList",
            dataType: "json",
            async: false,
            success: function (result, textStatus) {
                MaterialObjectList = result.d;
            },
            error: function (result, textStatus) {
                alert(result.responseText);
            }
        })
    }
    else {
        $("#progress").dialog('close');
    }
}

function GetMaterialsFromMaterialObject(sourceTypeID) {

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ sourceTypeID: parseInt(sourceTypeID) }),
        url: "Services/PurchaseControler.asmx/GetMaterialsBySourceTypeID",
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            MaterialList = result.d;
        
            $("#txtMaterialName0").autocomplete({
                source: MaterialList,
                minlength: 8
            });
            $("#txtMaterialName1").autocomplete({
                source: MaterialList,
                minlength: 8
            });
            $("#txtMaterialName2").autocomplete({
                source: MaterialList,
                minlength: 8
            });
            $("#txtMaterialName3").autocomplete({
                source: MaterialList,
                minlength: 8
            });
            $("#txtMaterialName4").autocomplete({
                source: MaterialList,
                minlength: 8
            });
            $("#txtMaterialName5").autocomplete({
                source: MaterialList,
                minlength: 8
            });
            $("#txtMaterialName6").autocomplete({
                source: MaterialList,
                minlength: 8
            });
            $("#txtMaterialName7").autocomplete({
                source: MaterialList,
                minlength: 8
            });
            $("#txtMaterialName8").autocomplete({
                source: MaterialList,
                minlength: 8
            });
            $("#txtMaterialName9").autocomplete({
                source: MaterialList,
                minlength: 8
            });
            $("#txtMaterialName0").on('autocompletechange change', function () {
                var Matname = this.value;
                if (MaterialObjectList != undefined) {
                    var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                    if (selectedMaterial != undefined) {
                        $("#hdnMatID0").val(selectedMaterial.MatID);
                        $("#txtRate0").val(selectedMaterial.MatCost);
                        $("#hdnUnitID0").val(selectedMaterial.Unit.UnitId);
                    }
                }

            }).change();

            $("#txtMaterialName1").on('autocompletechange change', function () {
                var Matname = this.value;
                if (MaterialObjectList != undefined) {
                    var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                    if (selectedMaterial != undefined) {
                        $("#txtRate1").val(selectedMaterial.MatCost);
                     }
                }

            }).change();

            $("#txtMaterialName2").on('autocompletechange change', function () {
                var Matname = this.value;
                if (MaterialObjectList != undefined) {
                    var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                    if (selectedMaterial != undefined) {
                        $("#txtRate2").val(selectedMaterial.MatCost);
                     }
                }

            }).change();

            $("#txtMaterialName3").on('autocompletechange change', function () {
                var Matname = this.value;
                if (MaterialObjectList != undefined) {
                    var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                    if (selectedMaterial != undefined) {
                        $("#txtRate3").val(selectedMaterial.MatCost);
               
                    }
                }

            }).change();

          
            $("#txtMaterialName4").on('autocompletechange change', function () {
                var Matname = this.value;
                if (MaterialObjectList != undefined) {
                    var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                    if (selectedMaterial != undefined) {
                        $("#txtRate4").val(selectedMaterial.MatCost);
                    }
                }

            }).change();
            $("#txtMaterialName5").on('autocompletechange change', function () {
                var Matname = this.value;
                if (MaterialObjectList != undefined) {
                    var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                    if (selectedMaterial != undefined) {
                        $("#txtRate5").val(selectedMaterial.MatCost);
                    }
                }

            }).change();
            $("#txtMaterialName6").on('autocompletechange change', function () {
                var Matname = this.value;
                if (MaterialObjectList != undefined) {
                    var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                    if (selectedMaterial != undefined) {
                        $("#txtRate6").val(selectedMaterial.MatCost);
                    }
                }

            }).change();
            $("#txtMaterialName7").on('autocompletechange change', function () {
                var Matname = this.value;
                if (MaterialObjectList != undefined) {
                    var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                    if (selectedMaterial != undefined) {
                        $("#txtRate7").val(selectedMaterial.MatCost);
                    }
                }

            }).change();

            $("#txtMaterialName8").on('autocompletechange change', function () {
                var Matname = this.value;
                if (MaterialObjectList != undefined) {
                    var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                    if (selectedMaterial != undefined) {
                        $("#txtRate8").val(selectedMaterial.MatCost);
                    }
                }

            }).change();

            $("#txtMaterialName9").on('autocompletechange change', function () {
                var Matname = this.value;
                if (MaterialObjectList != undefined) {
                    var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                    if (selectedMaterial != undefined) {
                        $("#txtRate9").val(selectedMaterial.MatCost);
                    }
                }

            }).change();
           
               $("#txtMaterial0").autocomplete({
                    source: MaterialList,
                    minlength: 8
               });
               $("#txtMaterial1").autocomplete({
                   source: MaterialList,
                   minlength: 8
               });
               $("#txtMaterial2").autocomplete({
                   source: MaterialList,
                   minlength: 8
               });
               $("#txtMaterial3").autocomplete({
                   source: MaterialList,
                   minlength: 8
               });
               $("#txtMaterial4").autocomplete({
                   source: MaterialList,
                   minlength: 8
               });
               $("#txtMaterial5").autocomplete({
                   source: MaterialList,
                   minlength: 8
               });
               $("#txtMaterial6").autocomplete({
                   source: MaterialList,
                   minlength: 8
               });
               $("#txtMaterial7").autocomplete({
                   source: MaterialList,
                   minlength: 8
               });
               $("#txtMaterial8").autocomplete({
                   source: MaterialList,
                   minlength: 8
               });
               $("#txtMaterial9").autocomplete({
                   source: MaterialList,
                   minlength: 8
               });
             
                $("#txtMaterial0").on('autocompletechange change', function () {
                    var Matname = this.value;
                    if (MaterialObjectList != undefined) {
                        var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                        if (selectedMaterial != undefined) {
                            $("#txtPrice0").val(selectedMaterial.MatCost);
                        }
                    }
                }).change();
                $("#txtMaterial1").on('autocompletechange change', function () {
                    var Matname = this.value;
                    if (MaterialObjectList != undefined) {
                        var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                        if (selectedMaterial != undefined) {
                            $("#txtPrice1").val(selectedMaterial.MatCost);
                        }
                    }
                }).change();
                $("#txtMaterial2").on('autocompletechange change', function () {
                    var Matname = this.value;
                    if (MaterialObjectList != undefined) {
                        var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                        if (selectedMaterial != undefined) {
                            $("#txtPrice2").val(selectedMaterial.MatCost);
                        }
                    }
                }).change();
                $("#txtMaterial3").on('autocompletechange change', function () {
                    var Matname = this.value;
                    if (MaterialObjectList != undefined) {
                        var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                        if (selectedMaterial != undefined) {
                            $("#txtPrice3").val(selectedMaterial.MatCost);
                        }
                    }
                }).change();
                $("#txtMaterial4").on('autocompletechange change', function () {
                    var Matname = this.value;
                    if (MaterialObjectList != undefined) {
                        var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                        if (selectedMaterial != undefined) {
                            $("#txtPrice4").val(selectedMaterial.MatCost);
                        }
                    }
                }).change();
                $("#txtMaterial5").on('autocompletechange change', function () {
                    var Matname = this.value;
                    if (MaterialObjectList != undefined) {
                        var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                        if (selectedMaterial != undefined) {
                            $("#txtPrice5").val(selectedMaterial.MatCost);
                        }
                    }
                }).change();
                $("#txtMaterial6").on('autocompletechange change', function () {
                    var Matname = this.value;
                    if (MaterialObjectList != undefined) {
                        var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                        if (selectedMaterial != undefined) {
                            $("#txtPrice6").val(selectedMaterial.MatCost);
                        }
                    }
                }).change();
                $("#txtMaterial7").on('autocompletechange change', function () {
                    var Matname = this.value;
                    if (MaterialObjectList != undefined) {
                        var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                        if (selectedMaterial != undefined) {
                            $("#txtPrice7").val(selectedMaterial.MatCost);
                        }
                    }
                }).change();
                $("#txtMaterial8").on('autocompletechange change', function () {
                    var Matname = this.value;
                    if (MaterialObjectList != undefined) {
                        var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                        if (selectedMaterial != undefined) {
                            $("#txtPrice8").val(selectedMaterial.MatCost);
                        }
                    }
                }).change();
                $("#txtMaterial9").on('autocompletechange change', function () {
                    var Matname = this.value;
                    if (MaterialObjectList != undefined) {
                        var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
                        if (selectedMaterial != undefined) {
                            $("#txtPrice9").val(selectedMaterial.MatCost);
                        }
                    }
                }).change();
            
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function ClearTextBox() {
    $("#ddlAcaName").val("");
    $("#txtGansetCompany").val("");
    $("#txtGansetSrNumber").val("");
    $("#txtGensetPower").val("");
    $("#txtDate").val("");
    $("#txtLastRepairDate").val("");
    $("#txtLastRepairAmount").val("");
    $("#txtQuotationAmount").val("");
    $("#txtApprovalAmount").val("");
    $("#txtGensetTotalRunning").val("");
    $("#txtService").val("");
    $("#txtGensetAverageRunning").val("");
    $("#txtMaterialName0").val("");
    $("#txtQty0").val("");
    $("#txtRate0").val("");
    $("#txtMaterialName1").val("");
    $("#txtQty1").val("");
    $("#txtRate1").val("");
    $("#txtMaterialName2").val("");
    $("#txtQty2").val("");
    $("#txtRate2").val("");
    $("#txtMaterialName3").val("");
    $("#txtQty3").val("");
    $("#txtRate3").val("");
    $("#txtMaterialName4").val("");
    $("#txtQty4").val("");
    $("#txtRate4").val("");
    $("#txtMaterialName5").val("");
    $("#txtQty5").val("");
    $("#txtRate5").val("");
    $("#txtMaterialName6").val("");
    $("#txtQty6").val("");
    $("#txtRate6").val("");
    $("#txtMaterialName7").val("");
    $("#txtQty7").val("");
    $("#txtRate7").val("");
    $("#txtMaterialName8").val("");
    $("#txtQty8").val("");
    $("#txtRate8").val("");
    $("#txtMaterialName9").val("");
    $("#txtQty9").val("");
    $("#txtRate9").val("");
    $("#ddlBatteryAcaName").val("");
    $("#ddlVehicleNumber").val("");
    $("#txtVehicelType").val("");
    $("#txtCurrentMeterReading").val("");
    $("#txtBatteryCapacity").val("");
    $("#txtNewMakeBattery").val("");
    $("#txtNewBatteryCapacity").val("");
    $("#txtPurchaseDate").val("");
    $("#txtNewBatterySrNum").val("");
    $("#txtOldBatterySale").val("");
    $("#txtNewBatteryLife").val("");
    $("#txtNoRequird").val("");
    $("#txtOldBatterySrNum").val("");
    $("#txtBatteryApprovalAmount").val("");
    $("#ddlTyreAcaName").val("");
    $("#txtNameofDriver").val("");
    $("#ddlTyreVehicleNo").val("");
    $("#txtTyreVehicleType").val("");
    $("#txtTyreSize").val("");
    $("#txtTyreDate").val("");
    $("#txtNoofRequird").val("");
    $("#txtCurrentMeter").val("");
    $("#txtNewTyreAmount").val("");
    $("#txtTyreMeterReading").val("");
    $("#txtOldTyreSaleAmount").val("");
    $("#txtTyreLastChangeDate").val("");
    $("#txtTyreApproval").val("");
    $("#txtTotalRuningKm").val("");
    $("#txtTyreLastReading").val("");
    $("#ddlServiceAcaName").val("");
    $("#ddlServiceVehicleNo").val("");
    $("#txtSeated").val("");
    $("#txtModel").val("");
    $("#txtSrvicCurntMetrReading").val("");
    $("#txtSrvicLastMetrReading").val("");
    $("#txtServiceQuotationAmount").val("");
    $("#txtAvergeVehicle").val("");
    $("#txtServicePlace").val("");
    $("#txtServiceApprovalAmount").val("");
    $("#txtMaterial0").val("");
    $("#txtQuantity0").val("");
    $("#txtPrice0").val("");
    $("#txtMaterial1").val("");
    $("#txtQuantity1").val("");
    $("#txtPrice1").val("");
    $("#txtMaterial2").val("");
    $("#txtQuantity2").val("");
    $("#txtPrice2").val("");
    $("#txtMaterial3").val("");
    $("#txtQuantity3").val("");
    $("#txtPrice3").val("");
    $("#txtMaterial4").val("");
    $("#txtQuantity4").val("");
    $("#txtPrice4").val("");
    $("#txtMaterial5").val("");
    $("#txtQuantity5").val("");
    $("#txtPrice5").val("");
    $("#txtMaterial6").val("");
    $("#txtQuantity6").val("");
    $("#txtPrice6").val("");
    $("#txtMaterial7").val("");
    $("#txtQuantity7").val("");
    $("#txtPrice7").val("");
    $("#txtMaterial8").val("");
    $("#txtQuantity8").val("");
    $("#txtPrice8").val("");
    $("#txtMaterial9").val("");
    $("#txtQuantity9").val("");
    $("#txtPrice9").val("");
    $("#txtMocrotaxSize").val("");
    $("#txtMocrotax").val("");
    $("#txtMocrotaxPrice").val("");
    $("#txtAmaronSize").val("");
    $("#txtAmaron").val("");
    $("#txtAmaronPrice").val("");
    $("#txtExideSize").val("");
    $("#txtExide").val("");
    $("#txtExidePrice").val("");
    $("#txtMicroTechSize").val("");
    $("#txtMicroTech").val("");
    $("#txtMicroTechPrice").val("");
    $("#txtBillNo").val("");
    $("#txtMrfRates").val("");
    $("#txtMrfQty").val("");
    $("#txtMrfAmount").val("");
    $("#txtApoloRates").val("");
    $("#txtApoloQty").val("");
    $("#txtApoloAmount").val("");
    $("#txtCeatRates").val("");
    $("#txtCeatQty").val("");
    $("#txtCeatAmount").val("");
    $("#txtJkRates").val("");
    $("#txtJkQty").val("");
    $("#txtJkAmount").val("");
    $("#txtFrontRightRequired").val("");
    $("#txtFrontRightCondition").val("");
    $("#txtFrontRightRunning").val("");
    $("#txtFrontRightOldTyreNo").val("");
    $("#txtFrontLeftRequired").val("");
    $("#txtFrontLeftCondition").val("");
    $("#txtFrontLeftRunning").val("");
    $("#txtFrontLeftOldTyreNo").val("");
    $("#txtRearRightRequired").val("");
    $("#txtRearRightCondition").val("");
    $("#txtRearRightRunning").val("");
    $("#txtRearRightOldTyreNo").val("");
    $("#txtRearLeftRequired").val("");
    $("#txtRearLeftCondition").val("");
    $("#txtRearLeftRunning").val("");
    $("#txtRearLeftOldTyreNo").val("");
    $("#txtStafneyRequired").val("");
    $("#txtStafneyCondition").val("");
    $("#txtStafneyRunning").val("");
    $("#txtStafneyldTyreNo").val("");
    $("#txtEngineerName").val("");
    $("#txtEngineerNumber").val("");
    $("#txtBatterySeated").val("");
    $("#txtBatteryModel").val("");
    $("#txtBatteryDriverandNumber").val("");
    $("#txtServiceDriverandNumber").val("");
    $("#txtServiceVehicelType").val("");
   
}





