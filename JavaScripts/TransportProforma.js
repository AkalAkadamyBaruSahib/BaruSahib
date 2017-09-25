
var MaterialList = new Array();
var MaterialObjectList;
var MaterialList;
var Amt = 0;
var rate = 0;
var qty = 0;

$(document).ready(function () {
    $("select").searchable();
});

$(document).ready(function () {

    BindAcademyByInchargeID($("input[id*='hdnInchargeID']").val());

    $("select[id*='ddlTyreAcaName']").change(function () {
        $("input[id*='hdnTyreAcaName']").val($("#ddlTyreAcaName option:selected").text());
        $("input[id*='hdnTyreAcaID']").val($(this).val());
        BindTrustVechileByAcaID($(this).val());
    });

    $("select[id*='ddlServiceAcaName']").change(function () {
        BindTrustVechileByAcaID($(this).val());
        $("input[id*='hdnServiceAcaName']").val($("#ddlServiceAcaName option:selected").text());
        $("input[id*='hdnServiceAcaID']").val($(this).val());
    });

    $("select[id*='ddlBatteryAcaName']").change(function () {
        $("input[id*='hdnBatteryAcaName']").val($("#ddlBatteryAcaName option:selected").text());
        $("input[id*='hdnBatteryAcaID']").val($(this).val());
        BindTrustVechileByAcaID($(this).val());
    });

    $("select[id*='ddlServiceVehicleNo']").change(function () {
        BindSeatedAndModelByVehicleID($(this).val());
        $("input[id*='hdnServiceVehicleNo']").val($("#ddlServiceVehicleNo option:selected").text());
        $("input[id*='hdnServiceVehicleID']").val($(this).val());
    });

    $("select[id*='ddlTyreVehicleNo']").change(function () {
        $("input[id*='hdnTyreVehicleNo']").val($("#ddlTyreVehicleNo option:selected").text());
        $("input[id*='hdnTyreVehicleID']").val($(this).val());
        BindSeatedAndModelByVehicleID($(this).val());
    });

    $("select[id*='ddlVehicleNumber']").change(function () {
        BindSeatedAndModelByVehicleID($(this).val());
        $("input[id*='hdnBatteryVehicleNo']").val($("#ddlVehicleNumber option:selected").text());
        $("input[id*='hdnBatteryVehicleID']").val($(this).val());
    });

    $("select[id*='ddlAcaName']").change(function () {
        $("input[id*='hdnGensetAcaName']").val($("#ddlAcaName option:selected").text());
        $("input[id*='hdnGenAcaID']").val($(this).val());
    });

    $("#txtMrfQty").change(function () {
        var mrfrate = $("#txtMrfRates").val();
        if (mrfrate == "") {
            $("#txtMrfAmount").val(0);
        }
        else {
            var mrfqty = $("#txtMrfQty").val();
            var mrf = parseFloat(mrfrate) * parseFloat(mrfqty);
            $("#txtMrfAmount").val(mrf);
        }
    });

    $("#txtMrfRates").change(function () {
        var mrfqty = $("#txtMrfQty").val();
        if (mrfqty == "") {
            $("#txtMrfAmount").val($("#txtMrfRates").val());
        }
        else {
            var mrfrate = $("#txtMrfRates").val();
            var mrf = parseFloat(mrfrate) * parseFloat(mrfqty);
            $("#txtMrfAmount").val(mrf);
        }

    });

    $("#txtApoloQty").change(function () {
        var apolorate = $("#txtApoloRates").val();
        if (apolorate == "") {
            $("#txtApoloAmount").val(0);
        }
        else {
            var apoloqty = $("#txtApoloQty").val();
            var apolo = parseFloat(apolorate) * parseFloat(apoloqty);
            $("#txtApoloAmount").val(apolo);
        }
    });

    $("#txtApoloRates").change(function () {
        var mrfqty = $("#txtApoloQty").val();
        if (mrfqty == "") {
            $("#txtApoloAmount").val($("#txtApoloRates").val());
        }
        else {
            var mrfrate = $("#txtApoloRates").val();
            var mrf = parseFloat(mrfrate) * parseFloat(mrfqty);
            $("#txtApoloAmount").val(mrf);
        }

    });

    $("#txtCeatQty").change(function () {
        var ceatrate = $("#txtCeatRates").val();
        if (ceatrate == "") {
            $("#txtCeatAmount").val(0);
        }
        else {
            var ceatqty = $("#txtCeatQty").val();
            var ceat = parseFloat(ceatrate) * parseFloat(ceatqty);
            $("#txtCeatAmount").val(ceat);
        }
    });

    $("#txtCeatRates").change(function () {
        var mrfqty = $("#txtCeatQty").val();
        if (mrfqty == "") {
            $("#txtCeatAmount").val($("#txtCeatRates").val());
        }
        else {
            var mrfrate = $("#txtCeatRates").val();
            var mrf = parseFloat(mrfrate) * parseFloat(mrfqty);
            $("#txtCeatAmount").val(mrf);
        }

    });

    $("#txtJkQty").change(function () {
        var jkrate = $("#txtJkRates").val();
        if (jkrate == "") {
            $("#txtJkAmount").val(0);
        }
        else {
            var jkqty = $("#txtJkQty").val();
            var jk = parseFloat(jkrate) * parseFloat(jkqty);
            $("#txtJkAmount").val(jk);
        }
    });

    $("#txtJkRates").change(function () {
        var mrfqty = $("#txtJkQty").val();
        if (mrfqty == "") {
            $("#txtJkAmount").val($("#txtJkRates").val());
        }
        else {
            var mrfrate = $("#txtJkRates").val();
            var mrf = parseFloat(mrfrate) * parseFloat(mrfqty);
            $("#txtJkAmount").val(mrf);
        }

    });

    $("select[id*='ddlproforma']").change(function () {
        if ($(this).val() == 1) {
            GetMaterials();
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
            $("#txtBatterySeated").prop('disabled', true);
            $("#txtBatteryModel").prop('disabled', true);
            $("#txtBatteryDriverandNumber").prop('disabled', true);
            $("#txtServiceDriverandNumber").prop('disabled', true);
            $("#txtServiceVehicelType").prop('disabled', true);
            $("#txtBatteryGensetNo").prop('disabled', true);
            $("#txtBatteryGensetPower").prop('disabled', true);
            $("#txtBatteryGensetCompany").prop('disabled', true);
            $("#txtBatteryInvertarCompany").prop('disabled', true);
            $("#ddlBatteryTye").prop('disabled', true);
            $("#txtMocrotaxSize").prop('disabled', true);
            $("#txtMocrotax").prop('disabled', true);
            $("#txtMocrotaxPrice").prop('disabled', true);
            $("#txtAmaronSize").prop('disabled', true);
            $("#txtAmaron").prop('disabled', true);
            $("#txtAmaronPrice").prop('disabled', true);
            $("#txtExideSize").prop('disabled', true);
            $("#txtExide").prop('disabled', true);
            $("#txtExidePrice").prop('disabled', true);
            $("#txtMicroTechSize").prop('disabled', true);
            $("#txtMicroTech").prop('disabled', true);
            $("#txtMicroTechPrice").prop('disabled', true);
            $("#txtMrfRates").prop('disabled', true);
            $("#txtMrfQty").prop('disabled', true);
            $("#txtMrfAmount").prop('disabled', true);
            $("#txtApoloRates").prop('disabled', true);
            $("#txtApoloQty").prop('disabled', true);
            $("#txtApoloAmount").prop('disabled', true);
            $("#txtCeatRates").prop('disabled', true);
            $("#txtCeatQty").prop('disabled', true);
            $("#txtCeatAmount").prop('disabled', true);
            $("#txtJkRates").prop('disabled', true);
            $("#txtJkQty").prop('disabled', true);
            $("#txtJkAmount").prop('disabled', true);
            $("#txtTyreSeated").prop('disabled', true);
            $("#txtTyreModel").prop('disabled', true);
            var length = $("#tbody1d").children('tr').length;
            $("input[id*='hdnGensetTableLength']").val(length);
            ClearTextBox();
        }
        else if ($(this).val() == 2) {
            $("#divGenset").hide();
            $("#divBatteryQuotation").show();
            $("#divTyreRequirement").hide();
            $("#divServiceandOtherRepair").hide();
            $("#trbtnDownload").show();
            $("#trVehicleDetail").hide();
            $("#trDriverDetail").hide();
            $("#trVehicelTypeDetail").hide();
            $("#trGensetNumber").hide();
            $("#trGensetPower").hide();
            $("#trGensetCompany").hide();
            $("#trInvertar").hide();
            $("#ddlBatteryAcaName").prop('disabled', false);
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
            $("#ddlVehicleNumber").prop('disabled', false);
            $("#txtVehicelType").prop('disabled', false);
            $("#txtBatterySeated").prop('disabled', false);
            $("#txtBatteryModel").prop('disabled', false);
            $("#txtBatteryDriverandNumber").prop('disabled', false);
            $("#txtBatteryGensetNo").prop('disabled', false);
            $("#txtBatteryGensetPower").prop('disabled', false);
            $("#txtBatteryGensetCompany").prop('disabled', false);
            $("#txtBatteryInvertarCompany").prop('disabled', false);
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
            $("#txtServiceDriverandNumber").prop('disabled', true);
            $("#txtServiceVehicelType").prop('disabled', true);
            $("#ddlBatteryTye").prop('disabled', false);
            $("#txtMocrotaxSize").prop('disabled', false);
            $("#txtMocrotax").prop('disabled', false);
            $("#txtMocrotaxPrice").prop('disabled', false);
            $("#txtAmaronSize").prop('disabled', false);
            $("#txtAmaron").prop('disabled', false);
            $("#txtAmaronPrice").prop('disabled', false);
            $("#txtExideSize").prop('disabled', false);
            $("#txtExide").prop('disabled', false);
            $("#txtExidePrice").prop('disabled', false);
            $("#txtMicroTechSize").prop('disabled', false);
            $("#txtMicroTech").prop('disabled', false);
            $("#txtMicroTechPrice").prop('disabled', false);
            $("#txtMrfRates").prop('disabled', true);
            $("#txtMrfQty").prop('disabled', true);
            $("#txtMrfAmount").prop('disabled', true);
            $("#txtApoloRates").prop('disabled', true);
            $("#txtApoloQty").prop('disabled', true);
            $("#txtApoloAmount").prop('disabled', true);
            $("#txtCeatRates").prop('disabled', true);
            $("#txtCeatQty").prop('disabled', true);
            $("#txtCeatAmount").prop('disabled', true);
            $("#txtJkRates").prop('disabled', true);
            $("#txtJkQty").prop('disabled', true);
            $("#txtJkAmount").prop('disabled', true);
            $("#txtTyreSeated").prop('disabled', true);
            $("#txtTyreModel").prop('disabled', true);
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
            $("#txtBatterySeated").prop('disabled', true);
            $("#txtBatteryModel").prop('disabled', true);
            $("#txtBatteryDriverandNumber").prop('disabled', true);
            $("#txtServiceDriverandNumber").prop('disabled', true);
            $("#txtServiceVehicelType").prop('disabled', true);
            $("#txtBatteryGensetNo").prop('disabled', true);
            $("#txtBatteryGensetPower").prop('disabled', true);
            $("#txtBatteryGensetCompany").prop('disabled', true);
            $("#txtBatteryInvertarCompany").prop('disabled', true);
            $("#ddlBatteryTye").prop('disabled', true);
            $("#txtMocrotaxSize").prop('disabled', true);
            $("#txtMocrotax").prop('disabled', true);
            $("#txtMocrotaxPrice").prop('disabled', true);
            $("#txtAmaronSize").prop('disabled', true);
            $("#txtAmaron").prop('disabled', true);
            $("#txtAmaronPrice").prop('disabled', true);
            $("#txtExideSize").prop('disabled', true);
            $("#txtExide").prop('disabled', true);
            $("#txtExidePrice").prop('disabled', true);
            $("#txtMicroTechSize").prop('disabled', true);
            $("#txtMicroTech").prop('disabled', true);
            $("#txtMicroTechPrice").prop('disabled', true);
            $("#txtMrfRates").prop('disabled', false);
            $("#txtMrfQty").prop('disabled', false);
            $("#txtMrfAmount").prop('disabled', false);
            $("#txtApoloRates").prop('disabled', false);
            $("#txtApoloQty").prop('disabled', false);
            $("#txtApoloAmount").prop('disabled', false);
            $("#txtCeatRates").prop('disabled', false);
            $("#txtCeatQty").prop('disabled', false);
            $("#txtCeatAmount").prop('disabled', false);
            $("#txtJkRates").prop('disabled', false);
            $("#txtJkQty").prop('disabled', false);
            $("#txtJkAmount").prop('disabled', false);
            $("#txtTyreSeated").prop('disabled', false);
            $("#txtTyreModel").prop('disabled', false);
            ClearTextBox();
        }
        else if ($(this).val() == 4) {
            GetMaterials();
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
            $("#txtBatterySeated").prop('disabled', true);
            $("#txtBatteryModel").prop('disabled', true);
            $("#txtBatteryDriverandNumber").prop('disabled', true);
            $("#txtServiceDriverandNumber").prop('disabled', false);
            $("#txtServiceVehicelType").prop('disabled', false);
            $("#txtBatteryGensetNo").prop('disabled', true);
            $("#txtBatteryGensetPower").prop('disabled', true);
            $("#txtBatteryGensetCompany").prop('disabled', true);
            $("#txtBatteryInvertarCompany").prop('disabled', true);
            $("#ddlBatteryTye").prop('disabled', true);
            $("#txtMocrotaxSize").prop('disabled', true);
            $("#txtMocrotax").prop('disabled', true);
            $("#txtMocrotaxPrice").prop('disabled', true);
            $("#txtAmaronSize").prop('disabled', true);
            $("#txtAmaron").prop('disabled', true);
            $("#txtAmaronPrice").prop('disabled', true);
            $("#txtExideSize").prop('disabled', true);
            $("#txtExide").prop('disabled', true);
            $("#txtExidePrice").prop('disabled', true);
            $("#txtMicroTechSize").prop('disabled', true);
            $("#txtMicroTech").prop('disabled', true);
            $("#txtMicroTechPrice").prop('disabled', true);
            $("#txtMrfRates").prop('disabled', true);
            $("#txtMrfQty").prop('disabled', true);
            $("#txtMrfAmount").prop('disabled', true);
            $("#txtApoloRates").prop('disabled', true);
            $("#txtApoloQty").prop('disabled', true);
            $("#txtApoloAmount").prop('disabled', true);
            $("#txtCeatRates").prop('disabled', true);
            $("#txtCeatQty").prop('disabled', true);
            $("#txtCeatAmount").prop('disabled', true);
            $("#txtJkRates").prop('disabled', true);
            $("#txtJkQty").prop('disabled', true);
            $("#txtJkAmount").prop('disabled', true);
            $("#txtTyreSeated").prop('disabled', true);
            $("#txtTyreModel").prop('disabled', true);

            $("input[id*='hdnServiceTableLength']").val($("#tbodyService").children('tr').length);
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
            $("#txtBatterySeated").prop('disabled', true);
            $("#txtBatteryModel").prop('disabled', true);
            $("#txtBatteryDriverandNumber").prop('disabled', true);
            $("#txtServiceDriverandNumber").prop('disabled', true);
            $("#txtServiceVehicelType").prop('disabled', true);
            $("#txtBatteryGensetNo").prop('disabled', true);
            $("#txtBatteryGensetPower").prop('disabled', true);
            $("#txtBatteryGensetCompany").prop('disabled', true);
            $("#txtBatteryInvertarCompany").prop('disabled', true);
            $("#ddlBatteryTye").prop('disabled', true);
            $("#txtMocrotaxSize").prop('disabled', true);
            $("#txtMocrotax").prop('disabled', true);
            $("#txtMocrotaxPrice").prop('disabled', true);
            $("#txtAmaronSize").prop('disabled', true);
            $("#txtAmaron").prop('disabled', true);
            $("#txtAmaronPrice").prop('disabled', true);
            $("#txtExideSize").prop('disabled', true);
            $("#txtExide").prop('disabled', true);
            $("#txtExidePrice").prop('disabled', true);
            $("#txtMicroTechSize").prop('disabled', true);
            $("#txtMicroTech").prop('disabled', true);
            $("#txtMicroTechPrice").prop('disabled', true);
            $("#txtMrfRates").prop('disabled', true);
            $("#txtMrfQty").prop('disabled', true);
            $("#txtMrfAmount").prop('disabled', true);
            $("#txtApoloRates").prop('disabled', true);
            $("#txtApoloQty").prop('disabled', true);
            $("#txtApoloAmount").prop('disabled', true);
            $("#txtCeatRates").prop('disabled', true);
            $("#txtCeatQty").prop('disabled', true);
            $("#txtCeatAmount").prop('disabled', true);
            $("#txtJkRates").prop('disabled', true);
            $("#txtJkQty").prop('disabled', true);
            $("#txtJkAmount").prop('disabled', true);
            $("#txtTyreSeated").prop('disabled', true);
            $("#txtTyreModel").prop('disabled', true);

            ClearTextBox();
        }
    });

    $("#btnGensetTotalAmt").click(function (e) {
        TotalGensetAmt();
    });

    $("#btnServiceTotalAmt").click(function (e) {
        TotalServiceAmt();
    });

    $("select[id*='ddlBatteryTye']").change(function () {
        if ($("select[id*='ddlBatteryTye']").val() == "Vehicle Battery") {
            $("#trVehicleDetail").show();
            $("#trDriverDetail").show();
            $("#trVehicelTypeDetail").show();
            $("#trGensetNumber").hide();
            $("#trGensetPower").hide();
            $("#trGensetCompany").hide();
            $("#trInvertar").hide();
            $("#ddlVehicleNumber").prop('disabled', false);
            $("#txtVehicelType").prop('disabled', false);
            $("#txtBatterySeated").prop('disabled', false);
            $("#txtBatteryModel").prop('disabled', false);
            $("#txtBatteryDriverandNumber").prop('disabled', false);
            $("#txtBatteryGensetNo").prop('disabled', true);
            $("#txtBatteryGensetPower").prop('disabled', true);
            $("#txtBatteryGensetCompany").prop('disabled', true);
            $("#txtBatteryInvertarCompany").prop('disabled', true);
        }
        else if ($("select[id*='ddlBatteryTye']").val() == "Genset Battery") {
            $("#trVehicleDetail").hide();
            $("#trDriverDetail").hide();
            $("#trVehicelTypeDetail").hide();
            $("#trGensetNumber").show();
            $("#trGensetPower").show();
            $("#trGensetCompany").show();
            $("#trInvertar").hide();
            $("#ddlVehicleNumber").prop('disabled', true);
            $("#txtVehicelType").prop('disabled', true);
            $("#txtBatterySeated").prop('disabled', true);
            $("#txtBatteryModel").prop('disabled', true);
            $("#txtBatteryDriverandNumber").prop('disabled', true);
            $("#txtBatteryGensetNo").prop('disabled', false);
            $("#txtBatteryGensetPower").prop('disabled', false);
            $("#txtBatteryGensetCompany").prop('disabled', false);
            $("#txtBatteryInvertarCompany").prop('disabled', true);
        }
        else if ($("select[id*='ddlBatteryTye']").val() == "Inverter Battery") {
            $("#trVehicleDetail").hide();
            $("#trDriverDetail").hide();
            $("#trVehicelTypeDetail").hide();
            $("#trGensetNumber").hide();
            $("#trGensetPower").hide();
            $("#trGensetCompany").hide();
            $("#trInvertar").show();
            $("#ddlVehicleNumber").prop('disabled', true);
            $("#txtVehicelType").prop('disabled', true);
            $("#txtBatterySeated").prop('disabled', true);
            $("#txtBatteryModel").prop('disabled', true);
            $("#txtBatteryDriverandNumber").prop('disabled', true);
            $("#txtBatteryGensetNo").prop('disabled', true);
            $("#txtBatteryGensetPower").prop('disabled', true);
            $("#txtBatteryGensetCompany").prop('disabled', true);
            $("#txtBatteryInvertarCompany").prop('disabled', false);
        }
        else {
            $("#trVehicleDetail").hide();
            $("#trDriverDetail").hide();
            $("#trVehicelTypeDetail").hide();
            $("#trGensetNumber").hide();
            $("#trGensetPower").hide();
            $("#trGensetCompany").hide();
            $("#trInvertar").hide();
            $("#ddlVehicleNumber").prop('disabled', true);
            $("#txtVehicelType").prop('disabled', true);
            $("#txtBatterySeated").prop('disabled', true);
            $("#txtBatteryModel").prop('disabled', true);
            $("#txtBatteryDriverandNumber").prop('disabled', true);
            $("#txtBatteryGensetNo").prop('disabled', true);
            $("#txtBatteryGensetPower").prop('disabled', true);
            $("#txtBatteryGensetCompany").prop('disabled', true);
            $("#txtBatteryInvertarCompany").prop('disabled', true);
        }
    });

    $("#txtQty0").change(function () {
        if ($("#txtQty0").val() != undefined) {
            var value = $("#txtQty0").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty0").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty0").css('border-color', '');
                    qty = $("#txtQty0").val();
                    rate = $("#txtRate0").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount0").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty1").change(function () {
        if ($("#txtQty1").val() != undefined) {
            var value = $("#txtQty1").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty1").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty1").css('border-color', '');
                    qty = $("#txtQty1").val();
                    rate = $("#txtRate1").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount1").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty2").change(function () {
        if ($("#txtQty2").val() != undefined) {
            var value = $("#txtQty2").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty2").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty2").css('border-color', '');
                    qty = $("#txtQty2").val();
                    rate = $("#txtRate2").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount2").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty3").change(function () {
        if ($("#txtQty3").val() != undefined) {
            var value = $("#txtQty3").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty3").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty3").css('border-color', '');
                    qty = $("#txtQty3").val();
                    rate = $("#txtRate3").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount3").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty4").change(function () {
        if ($("#txtQty4").val() != undefined) {
            var value = $("#txtQty4").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty4").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty4").css('border-color', '');
                    qty = $("#txtQty4").val();
                    rate = $("#txtRate4").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount4").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty5").change(function () {
        if ($("#txtQty5").val() != undefined) {
            var value = $("#txtQty5").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty5").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty5").css('border-color', '');
                    qty = $("#txtQty5").val();
                    rate = $("#txtRate5").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount5").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty6").change(function () {
        if ($("#txtQty6").val() != undefined) {
            var value = $("#txtQty6").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty6").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty6").css('border-color', '');
                    qty = $("#txtQty6").val();
                    rate = $("#txtRate6").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount6").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty7").change(function () {
        if ($("#txtQty7").val() != undefined) {
            var value = $("#txtQty7").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty7").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty7").css('border-color', '');
                    qty = $("#txtQty7").val();
                    rate = $("#txtRate7").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount7").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty8").change(function () {
        if ($("#txtQty8").val() != undefined) {
            var value = $("#txtQty8").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty8").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty8").css('border-color', '');
                    qty = $("#txtQty8").val();
                    rate = $("#txtRate8").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount8").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty9").change(function () {
        if ($("#txtQty9").val() != undefined) {
            var value = $("#txtQty9").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty9").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty9").css('border-color', '');
                    qty = $("#txtQty9").val();
                    rate = $("#txtRate9").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount9").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty10").change(function () {
        if ($("#txtQty10").val() != undefined) {
            var value = $("#txtQty10").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty10").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty10").css('border-color', '');
                    qty = $("#txtQty10").val();
                    rate = $("#txtRate10").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount10").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty11").change(function () {
        if ($("#txtQty11").val() != undefined) {
            var value = $("#txtQty11").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty11").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty11").css('border-color', '');
                    qty = $("#txtQty11").val();
                    rate = $("#txtRate11").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount11").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty12").change(function () {
        if ($("#txtQty12").val() != undefined) {
            var value = $("#txtQty12").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty12").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty12").css('border-color', '');
                    qty = $("#txtQty12").val();
                    rate = $("#txtRate12").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount12").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty13").change(function () {
        if ($("#txtQty13").val() != undefined) {
            var value = $("#txtQty13").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty13").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty13").css('border-color', '');
                    qty = $("#txtQty13").val();
                    rate = $("#txtRate13").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount13").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty14").change(function () {
        if ($("#txtQty14").val() != undefined) {
            var value = $("#txtQty14").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty14").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty14").css('border-color', '');
                    qty = $("#txtQty14").val();
                    rate = $("#txtRate14").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount14").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty15").change(function () {
        if ($("#txtQty15").val() != undefined) {
            var value = $("#txtQty15").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty15").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty15").css('border-color', '');
                    qty = $("#txtQty15").val();
                    rate = $("#txtRate15").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount15").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty16").change(function () {
        if ($("#txtQty16").val() != undefined) {
            var value = $("#txtQty16").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty16").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty16").css('border-color', '');
                    qty = $("#txtQty16").val();
                    rate = $("#txtRate16").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount16").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty17").change(function () {
        if ($("#txtQty17").val() != undefined) {
            var value = $("#txtQty17").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty17").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty17").css('border-color', '');
                    qty = $("#txtQty17").val();
                    rate = $("#txtRate17").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount17").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty18").change(function () {
        if ($("#txtQty18").val() != undefined) {
            var value = $("#txtQty18").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty18").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty18").css('border-color', '');
                    qty = $("#txtQty18").val();
                    rate = $("#txtRate18").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount18").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQty19").change(function () {
        if ($("#txtQty19").val() != undefined) {
            var value = $("#txtQty19").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty19").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty19").css('border-color', '');
                    qty = $("#txtQty19").val();
                    rate = $("#txtRate19").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount19").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate0").change(function () {
        if ($("#txtRate0").val() != undefined) {
            var value = $("#txtRate0").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate0").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate0").css('border-color', '');
                    qty = $("#txtQty0").val();
                    rate = $("#txtRate0").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount0").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate1").change(function () {
        if ($("#txtRate1").val() != undefined) {
            var value = $("#txtRate1").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate1").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate1").css('border-color', '');
                    qty = $("#txtQty1").val();
                    rate = $("#txtRate1").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount1").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate2").change(function () {
        if ($("#txtRate2").val() != undefined) {
            var value = $("#txtRate2").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate2").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate2").css('border-color', '');
                    qty = $("#txtQty2").val();
                    rate = $("#txtRate2").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount2").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate3").change(function () {
        if ($("#txtRate3").val() != undefined) {
            var value = $("#txtRate3").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate3").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate3").css('border-color', '');
                    qty = $("#txtQty3").val();
                    rate = $("#txtRate3").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount3").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate4").change(function () {
        if ($("#txtRate4").val() != undefined) {
            var value = $("#txtRate4").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate4").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate4").css('border-color', '');
                    qty = $("#txtQty4").val();
                    rate = $("#txtRate4").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount4").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate5").change(function () {
        if ($("#txtRate5").val() != undefined) {
            var value = $("#txtRate5").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate5").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate5").css('border-color', '');
                    qty = $("#txtQty5").val();
                    rate = $("#txtRate5").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount5").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate6").change(function () {
        if ($("#txtRate6").val() != undefined) {
            var value = $("#txtRate6").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate6").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate6").css('border-color', '');
                    qty = $("#txtQty6").val();
                    rate = $("#txtRate6").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount6").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate7").change(function () {
        if ($("#txtRate7").val() != undefined) {
            var value = $("#txtRate7").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate7").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate7").css('border-color', '');
                    qty = $("#txtQty7").val();
                    rate = $("#txtRate7").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount7").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate8").change(function () {
        if ($("#txtRate8").val() != undefined) {
            var value = $("#txtRate8").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate8").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate8").css('border-color', '');
                    qty = $("#txtQty8").val();
                    rate = $("#txtRate8").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount8").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate9").change(function () {
        if ($("#txtRate9").val() != undefined) {
            var value = $("#txtRate9").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate9").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate9").css('border-color', '');
                    qty = $("#txtQty9").val();
                    rate = $("#txtRate9").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount9").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate10").change(function () {
        if ($("#txtRate10").val() != undefined) {
            var value = $("#txtRate10").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate10").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate10").css('border-color', '');
                    qty = $("#txtQty10").val();
                    rate = $("#txtRate10").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount10").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate11").change(function () {
        if ($("#txtRate11").val() != undefined) {
            var value = $("#txtRate11").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate11").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate11").css('border-color', '');
                    qty = $("#txtQty11").val();
                    rate = $("#txtRate11").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount11").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate12").change(function () {
        if ($("#txtRate12").val() != undefined) {
            var value = $("#txtRate12").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate12").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate12").css('border-color', '');
                    qty = $("#txtQty12").val();
                    rate = $("#txtRate12").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount12").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate13").change(function () {
        if ($("#txtRate13").val() != undefined) {
            var value = $("#txtRate13").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate13").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate13").css('border-color', '');
                    qty = $("#txtQty13").val();
                    rate = $("#txtRate13").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount13").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate14").change(function () {
        if ($("#txtRate14").val() != undefined) {
            var value = $("#txtRate14").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate14").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate14").css('border-color', '');
                    qty = $("#txtQty14").val();
                    rate = $("#txtRate14").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount14").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate15").change(function () {
        if ($("#txtRate15").val() != undefined) {
            var value = $("#txtRate15").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate15").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate15").css('border-color', '');
                    qty = $("#txtQty15").val();
                    rate = $("#txtRate15").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount15").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate16").change(function () {
        if ($("#txtRate16").val() != undefined) {
            var value = $("#txtRate16").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate16").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate16").css('border-color', '');
                    qty = $("#txtQty16").val();
                    rate = $("#txtRate16").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount16").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate17").change(function () {
        if ($("#txtRate17").val() != undefined) {
            var value = $("#txtRate17").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate17").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate17").css('border-color', '');
                    qty = $("#txtQty17").val();
                    rate = $("#txtRate17").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount17").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate18").change(function () {
        if ($("#txtRate18").val() != undefined) {
            var value = $("#txtRate18").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate18").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate18").css('border-color', '');
                    qty = $("#txtQty18").val();
                    rate = $("#txtRate18").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount18").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtRate19").change(function () {
        if ($("#txtRate19").val() != undefined) {
            var value = $("#txtRate19").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtRate19").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtRate19").css('border-color', '');
                    qty = $("#txtQty19").val();
                    rate = $("#txtRate19").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtAmount19").val(RoundAmt);
                    TotalGensetAmt();
                }
            }
        }
    });

    $("#txtQuantity0").change(function () {
        if ($("#txtQuantity0").val() != undefined) {
            var value = $("#txtQuantity0").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity0").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity0").css('border-color', '');
                    qty = $("#txtQuantity0").val();
                    rate = $("#txtPrice0").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount0").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity1").change(function () {
        if ($("#txtQuantity1").val() != undefined) {
            var value = $("#txtQuantity1").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity1").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity1").css('border-color', '');
                    qty = $("#txtQuantity1").val();
                    rate = $("#txtPrice1").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount1").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity2").change(function () {
        if ($("#txtQuantity2").val() != undefined) {
            var value = $("#txtQuantity2").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity2").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity2").css('border-color', '');
                    qty = $("#txtQuantity2").val();
                    rate = $("#txtPrice2").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount2").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity3").change(function () {
        if ($("#txtQuantity3").val() != undefined) {
            var value = $("#txtQuantity3").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity3").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity3").css('border-color', '');
                    qty = $("#txtQuantity3").val();
                    rate = $("#txtPrice3").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount3").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity4").change(function () {
        if ($("#txtQuantity4").val() != undefined) {
            var value = $("#txtQuantity4").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity4").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity4").css('border-color', '');
                    qty = $("#txtQuantity4").val();
                    rate = $("#txtPrice4").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount4").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity5").change(function () {
        if ($("#txtQuantity5").val() != undefined) {
            var value = $("#txtQuantity5").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity5").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity5").css('border-color', '');
                    qty = $("#txtQuantity5").val();
                    rate = $("#txtPrice5").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount5").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity6").change(function () {
        if ($("#txtQuantity6").val() != undefined) {
            var value = $("#txtQuantity6").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity6").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity6").css('border-color', '');
                    qty = $("#txtQuantity6").val();
                    rate = $("#txtPrice6").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount6").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity7").change(function () {
        if ($("#txtQuantity7").val() != undefined) {
            var value = $("#txtQuantity7").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity7").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity7").css('border-color', '');
                    qty = $("#txtQuantity7").val();
                    rate = $("#txtPrice7").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount7").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity8").change(function () {
        if ($("#txtQuantity8").val() != undefined) {
            var value = $("#txtQuantity8").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity8").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity8").css('border-color', '');
                    qty = $("#txtQuantity8").val();
                    rate = $("#txtPrice8").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount8").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity9").change(function () {
        if ($("#txtQuantity9").val() != undefined) {
            var value = $("#txtQuantity9").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity9").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity9").css('border-color', '');
                    qty = $("#txtQuantity9").val();
                    rate = $("#txtPrice9").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount9").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity10").change(function () {
        if ($("#txtQuantity10").val() != undefined) {
            var value = $("#txtQuantity10").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity10").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity10").css('border-color', '');
                    qty = $("#txtQuantity10").val();
                    rate = $("#txtPrice10").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount10").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity11").change(function () {
        if ($("#txtQuantity11").val() != undefined) {
            var value = $("#txtQuantity11").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity11").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity11").css('border-color', '');
                    qty = $("#txtQuantity11").val();
                    rate = $("#txtPrice11").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount11").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity12").change(function () {
        if ($("#txtQuantity12").val() != undefined) {
            var value = $("#txtQuantity12").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity12").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity12").css('border-color', '');
                    qty = $("#txtQuantity12").val();
                    rate = $("#txtPrice12").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount12").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity13").change(function () {
        if ($("#txtQuantity13").val() != undefined) {
            var value = $("#txtQuantity13").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity13").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity13").css('border-color', '');
                    qty = $("#txtQuantity13").val();
                    rate = $("#txtPrice13").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount13").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity14").change(function () {
        if ($("#txtQuantity14").val() != undefined) {
            var value = $("#txtQuantity14").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity14").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity14").css('border-color', '');
                    qty = $("#txtQuantity14").val();
                    rate = $("#txtPrice14").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount14").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity15").change(function () {
        if ($("#txtQuantity15").val() != undefined) {
            var value = $("#txtQuantity15").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity15").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity15").css('border-color', '');
                    qty = $("#txtQuantity15").val();
                    rate = $("#txtPrice15").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount15").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity16").change(function () {
        if ($("#txtQuantity16").val() != undefined) {
            var value = $("#txtQuantity16").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity16").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity16").css('border-color', '');
                    qty = $("#txtQuantity16").val();
                    rate = $("#txtPrice16").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount16").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity17").change(function () {
        if ($("#txtQuantity17").val() != undefined) {
            var value = $("#txtQuantity17").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity17").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity17").css('border-color', '');
                    qty = $("#txtQuantity17").val();
                    rate = $("#txtPrice17").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount17").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity18").change(function () {
        if ($("#txtQuantity18").val() != undefined) {
            var value = $("#txtQuantity18").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity18").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity18").css('border-color', '');
                    qty = $("#txtQuantity18").val();
                    rate = $("#txtPrice18").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount18").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtQuantity19").change(function () {
        if ($("#txtQuantity19").val() != undefined) {
            var value = $("#txtQuantity19").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity19").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity19").css('border-color', '');
                    qty = $("#txtQuantity19").val();
                    rate = $("#txtPrice19").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount19").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice0").change(function () {
        if ($("#txtPrice0").val() != undefined) {
            var value = $("#txtPrice0").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice0").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice0").css('border-color', '');
                    qty = $("#txtQuantity0").val();
                    rate = $("#txtPrice0").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount0").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice1").change(function () {
        if ($("#txtPrice1").val() != undefined) {
            var value = $("#txtPrice1").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice1").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice1").css('border-color', '');
                    qty = $("#txtQuantity1").val();
                    rate = $("#txtPrice1").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount1").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice2").change(function () {
        if ($("#txtPrice2").val() != undefined) {
            var value = $("#txtPrice").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice2").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice2").css('border-color', '');
                    qty = $("#txtQuantity2").val();
                    rate = $("#txtPrice2").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount2").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice3").change(function () {
        if ($("#txtPrice3").val() != undefined) {
            var value = $("#txtPrice3").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice3").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice3").css('border-color', '');
                    qty = $("#txtQuantity3").val();
                    rate = $("#txtPrice3").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount3").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice4").change(function () {
        if ($("#txtPrice4").val() != undefined) {
            var value = $("#txtPrice4").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice4").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice4").css('border-color', '');
                    qty = $("#txtQuantity4").val();
                    rate = $("#txtPrice4").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount4").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice5").change(function () {
        if ($("#txtPrice5").val() != undefined) {
            var value = $("#txtPrice5").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice5").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice5").css('border-color', '');
                    qty = $("#txtQuantity5").val();
                    rate = $("#txtPrice5").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount5").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice6").change(function () {
        if ($("#txtPrice6").val() != undefined) {
            var value = $("#txtPrice6").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice6").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice6").css('border-color', '');
                    qty = $("#txtQuantity6").val();
                    rate = $("#txtPrice6").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount6").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice7").change(function () {
        if ($("#txtPrice7").val() != undefined) {
            var value = $("#txtPrice7").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice7").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice7").css('border-color', '');
                    qty = $("#txtQuantity7").val();
                    rate = $("#txtPrice7").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount7").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice8").change(function () {
        if ($("#txtPrice8").val() != undefined) {
            var value = $("#txtPrice8").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice8").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice8").css('border-color', '');
                    qty = $("#txtQuantity8").val();
                    rate = $("#txtPrice8").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount8").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice9").change(function () {
        if ($("#txtPrice9").val() != undefined) {
            var value = $("#txtPrice9").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice9").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice9").css('border-color', '');
                    qty = $("#txtQuantity9").val();
                    rate = $("#txtPrice9").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount9").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice10").change(function () {
        if ($("#txtPrice10").val() != undefined) {
            var value = $("#txtPrice10").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice10").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice10").css('border-color', '');
                    qty = $("#txtQuantity10").val();
                    rate = $("#txtPrice10").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount10").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice11").change(function () {
        if ($("#txtPrice11").val() != undefined) {
            var value = $("#txtPrice11").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice11").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice11").css('border-color', '');
                    qty = $("#txtQuantity11").val();
                    rate = $("#txtPrice11").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount11").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice12").change(function () {
        if ($("#txtPrice12").val() != undefined) {
            var value = $("#txtPrice12").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice12").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice12").css('border-color', '');
                    qty = $("#txtQuantity12").val();
                    rate = $("#txtPrice12").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount12").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice13").change(function () {
        if ($("#txtPrice13").val() != undefined) {
            var value = $("#txtPrice13").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice13").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice13").css('border-color', '');
                    qty = $("#txtQuantity13").val();
                    rate = $("#txtPrice13").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount13").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice14").change(function () {
        if ($("#txtPrice14").val() != undefined) {
            var value = $("#txtPrice14").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice14").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice14").css('border-color', '');
                    qty = $("#txtQuantity14").val();
                    rate = $("#txtPrice14").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount14").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice15").change(function () {
        if ($("#txtPrice15").val() != undefined) {
            var value = $("#txtPrice15").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice15").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice15").css('border-color', '');
                    qty = $("#txtQuantity15").val();
                    rate = $("#txtPrice15").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount15").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice16").change(function () {
        if ($("#txtPrice16").val() != undefined) {
            var value = $("#txtPrice16").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice16").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice16").css('border-color', '');
                    qty = $("#txtQuantity16").val();
                    rate = $("#txtPrice16").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount16").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice17").change(function () {
        if ($("#txtPrice17").val() != undefined) {
            var value = $("#txtPrice17").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice17").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice17").css('border-color', '');
                    qty = $("#txtQuantity17").val();
                    rate = $("#txtPrice17").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount17").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice18").change(function () {
        if ($("#txtPrice18").val() != undefined) {
            var value = $("#txtPrice18").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice18").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice18").css('border-color', '');
                    qty = $("#txtQuantity18").val();
                    rate = $("#txtPrice18").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount18").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtPrice19").change(function () {
        if ($("#txtPrice19").val() != undefined) {
            var value = $("#txtPrice19").val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtPrice19").css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtPrice19").css('border-color', '');
                    qty = $("#txtQuantity19").val();
                    rate = $("#txtPrice19").val();
                    Amt = parseFloat(qty) * parseFloat(rate);
                    var RoundAmt = Amt.toFixed(2);
                    $("#txtSerAmount19").val(RoundAmt);
                    TotalServiceAmt();
                }
            }
        }
    });

    $("#txtMaterialName0").change(function () {
        BindMaterialDetailByMatID(($(this).val()), 0);
    });

    $("#txtMaterialName1").change(function () {
        BindMaterialDetailByMatID($(this).val(), 1);
    });

    $("#txtMaterialName2").change(function () {
        BindMaterialDetailByMatID($(this).val(), 2);
    });

    $("#txtMaterialName3").change(function () {
        BindMaterialDetailByMatID($(this).val(), 3);
    });

    $("#txtMaterialName4").change(function () {
        BindMaterialDetailByMatID($(this).val(), 4);
    });

    $("#txtMaterialName5").change(function () {
        BindMaterialDetailByMatID($(this).val(), 5);
    });

    $("#txtMaterialName6").change(function () {
        BindMaterialDetailByMatID($(this).val(), 6);
    });

    $("#txtMaterialName7").change(function () {
        BindMaterialDetailByMatID($(this).val(), 7);
    });

    $("#txtMaterialName8").change(function () {
        BindMaterialDetailByMatID($(this).val(), 8);
    });

    $("#txtMaterialName9").change(function () {
        BindMaterialDetailByMatID($(this).val(), 9);
    });

    $("#txtMaterialName10").change(function () {
        BindMaterialDetailByMatID($(this).val(), 10);
    });

    $("#txtMaterialName11").change(function () {
        BindMaterialDetailByMatID($(this).val(), 11);
    });

    $("#txtMaterialName12").change(function () {
        BindMaterialDetailByMatID($(this).val(), 12);
    });

    $("#txtMaterialName13").change(function () {
        BindMaterialDetailByMatID($(this).val(), 13);
    });

    $("#txtMaterialName14").change(function () {
        BindMaterialDetailByMatID($(this).val(), 14);
    });

    $("#txtMaterialName15").change(function () {
        BindMaterialDetailByMatID($(this).val(), 15);
    });

    $("#txtMaterialName16").change(function () {
        BindMaterialDetailByMatID($(this).val(), 16);
    });

    $("#txtMaterialName17").change(function () {
        BindMaterialDetailByMatID($(this).val(), 17);
    });

    $("#txtMaterialName18").change(function () {
        BindMaterialDetailByMatID($(this).val(), 18);
    });

    $("#txtMaterialName19").change(function () {
        BindMaterialDetailByMatID($(this).val(), 19);
    });

    $("#txtMaterial0").change(function () {
        BindMaterialDetailByMatID(($(this).val()), 0);
    });

    $("#txtMaterial1").change(function () {
        BindMaterialDetailByMatID($(this).val(), 1);
    });

    $("#txtMaterial2").change(function () {
        BindMaterialDetailByMatID($(this).val(), 2);
    });

    $("#txtMaterial3").change(function () {
        BindMaterialDetailByMatID($(this).val(), 3);
    });

    $("#txtMaterial4").change(function () {
        BindMaterialDetailByMatID($(this).val(), 4);
    });

    $("#txtMaterial5").change(function () {
        BindMaterialDetailByMatID($(this).val(), 5);
    });

    $("#txtMaterial6").change(function () {
        BindMaterialDetailByMatID($(this).val(), 6);
    });

    $("#txtMaterial7").change(function () {
        BindMaterialDetailByMatID($(this).val(), 7);
    });

    $("#txtMaterial8").change(function () {
        BindMaterialDetailByMatID($(this).val(), 8);
    });

    $("#txtMaterial9").change(function () {
        BindMaterialDetailByMatID($(this).val(), 9);
    });

    $("#txtMaterial10").change(function () {
        BindMaterialDetailByMatID($(this).val(), 10);
    });

    $("#txtMaterial11").change(function () {
        BindMaterialDetailByMatID($(this).val(), 11);
    });

    $("#txtMaterial12").change(function () {
        BindMaterialDetailByMatID($(this).val(), 12);
    });

    $("#txtMaterial13").change(function () {
        BindMaterialDetailByMatID($(this).val(), 13);
    });

    $("#txtMaterial14").change(function () {
        BindMaterialDetailByMatID($(this).val(), 14);
    });

    $("#txtMaterial15").change(function () {
        BindMaterialDetailByMatID($(this).val(), 15);
    });

    $("#txtMaterial16").change(function () {
        BindMaterialDetailByMatID($(this).val(), 16);
    });

    $("#txtMaterial17").change(function () {
        BindMaterialDetailByMatID($(this).val(), 17);
    });

    $("#txtMaterial18").change(function () {
        BindMaterialDetailByMatID($(this).val(), 18);
    });

    $("#txtMaterial19").change(function () {
        BindMaterialDetailByMatID($(this).val(), 19);
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

function BindTrustVechileByAcaID(acaId) {

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
        url: "Services/TransportController.asmx/GetVehiclesByAcademyIDandTypeID",
        data: JSON.stringify({ AcaID: parseInt(acaId), TypeID: 2 }),
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
                    $("input[id*='txtTyreSeated']").val(rdata.Sitter);
                    $("input[id*='txtTyreModel']").val(rdata.Model);
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

function GetMaterials() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetActiveMaterialsByMatTypeID",
        data: JSON.stringify({ MatTypeID: 49 }),
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            materialType = result.d;
            $.each(materialType, function (key, value) {
                if ($("select[id*='ddlproforma']").val() == 1) {
                    $("#txtMaterialName0").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName1").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName2").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName3").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName4").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName5").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName6").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName7").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName8").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName9").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName10").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName11").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName12").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName13").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName14").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName15").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName16").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName17").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName19").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterialName18").append($("<option></option>").val(value.MatID).html(value.MatName));
                }
                else if ($("select[id*='ddlproforma']").val() == 4) {
                    $("#txtMaterial0").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial1").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial2").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial3").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial4").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial5").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial6").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial7").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial8").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial9").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial10").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial11").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial12").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial13").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial14").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial15").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial16").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial17").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial18").append($("<option></option>").val(value.MatID).html(value.MatName));
                    $("#txtMaterial19").append($("<option></option>").val(value.MatID).html(value.MatName));
                }
            });
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindMaterialDetailByMatID(matID, count) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetBindMaterialByMaterialID",
        data: JSON.stringify({ matID: parseInt(matID) }),
        dataType: "json",
        success: function (responce) {
            var msg = responce.d;
            if (msg != undefined) {
                if ($("select[id*='ddlproforma']").val() == 1) {
                    $("input[id*='hdnMaterialName" + count + "']").val(msg[0].MatName);
                    $("#hdnMatID" + count).val(msg[0].MatID);
                    $("#txtRate" + count).val(msg[0].MatCost);
                    $("#txtUnit" + count).val(msg[0].Unit.UnitName);
                    $("#hdnUnitID" + count).val(msg[0].Unit.UnitId);
                    $("input[id*='hdnUnitName" + count + "']").val(msg[0].Unit.UnitName);
                    $("#txtUnit" + count).prop('disabled', true);
                }
                else if ($("select[id*='ddlproforma']").val() == 4) {
                    $("#txtPrice" + count).val(msg[0].MatCost);
                    $("#txtSerUnit" + count).val(msg[0].Unit.UnitName);
                    $("#hdnSerUnitID" + count).val(msg[0].Unit.UnitId);
                    $("#hdnSerMatID" + count).val(msg[0].MatID);
                    $("input[id*='hdnMaterial" + count + "']").val(msg[0].MatName);
                    $("input[id*='hdnSerUnitName" + count + "']").val(msg[0].Unit.UnitName);
                    $("#txtSerUnit" + count).prop('disabled', true);
                }
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
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
    $("#txtMaterialName10").val("");
    $("#txtQty10").val("");
    $("#txtRate10").val("");
    $("#txtMaterialName11").val("");
    $("#txtQty11").val("");
    $("#txtRate11").val("");
    $("#txtMaterialName12").val("");
    $("#txtQty12").val("");
    $("#txtRate12").val("");
    $("#txtMaterialName13").val("");
    $("#txtQty13").val("");
    $("#txtRate13").val("");
    $("#txtMaterialName14").val("");
    $("#txtQty14").val("");
    $("#txtRate14").val("");
    $("#txtMaterialName15").val("");
    $("#txtQty15").val("");
    $("#txtRate15").val("");
    $("#txtMaterialName16").val("");
    $("#txtQty16").val("");
    $("#txtRate16").val("");
    $("#txtMaterialName17").val("");
    $("#txtQty17").val("");
    $("#txtRate17").val("");
    $("#txtMaterialName18").val("");
    $("#txtQty18").val("");
    $("#txtRate18").val("");
    $("#txtMaterialName19").val("");
    $("#txtQty19").val("");
    $("#txtRate19").val("");
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
    $("#txtMaterial10").val("");
    $("#txtQuantity10").val("");
    $("#txtPrice10").val("");
    $("#txtMaterial11").val("");
    $("#txtQuantity11").val("");
    $("#txtPrice11").val("");
    $("#txtMaterial12").val("");
    $("#txtQuantity12").val("");
    $("#txtPrice12").val("");
    $("#txtMaterial13").val("");
    $("#txtQuantity13").val("");
    $("#txtPrice13").val("");
    $("#txtMaterial14").val("");
    $("#txtQuantity14").val("");
    $("#txtPrice14").val("");
    $("#txtMaterial15").val("");
    $("#txtQuantity15").val("");
    $("#txtPrice15").val("");
    $("#txtMaterial16").val("");
    $("#txtQuantity16").val("");
    $("#txtPrice16").val("");
    $("#txtMaterial17").val("");
    $("#txtQuantity17").val("");
    $("#txtPrice17").val("");
    $("#txtMaterial18").val("");
    $("#txtQuantity18").val("");
    $("#txtPrice18").val("");
    $("#txtMaterial19").val("");
    $("#txtQuantity19").val("");
    $("#txtPrice19").val("");
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
    $("#txtBatterySeated").val("");
    $("#txtBatteryModel").val("");
    $("#txtBatteryDriverandNumber").val("");
    $("#txtServiceDriverandNumber").val("");
    $("#txtServiceVehicelType").val("");
    $("#txtTyreRemarks").val("");
    $("#txtServiceRemarks").val("");
    $("#txtGensetRemarks").val("");
    $("#txtBatteryRemarks").val("");
    $("#txtBatteryGensetNo").val("");
    $("#txtBatteryGensetPower").val("");
    $("#txtBatteryGensetCompany").val("");
    $("#txtBatteryInvertarCompany").val("");
    $("#ddlBatteryTye").val("");
    $("#txtSerAmount0").val("");
    $("#txtSerAmount1").val("");
    $("#txtSerAmount2").val("");
    $("#txtSerAmount3").val("");
    $("#txtSerAmount4").val("");
    $("#txtSerAmount5").val("");
    $("#txtSerAmount6").val("");
    $("#txtSerAmount7").val("");
    $("#txtSerAmount8").val("");
    $("#txtSerAmount9").val("");
    $("#txtSerAmount10").val("");
    $("#txtSerAmount11").val("");
    $("#txtSerAmount12").val("");
    $("#txtSerAmount13").val("");
    $("#txtSerAmount14").val("");
    $("#txtSerAmount15").val("");
    $("#txtSerAmount16").val("");
    $("#txtSerAmount17").val("");
    $("#txtSerAmount18").val("");
    $("#txtSerAmount19").val("");
    $("#txtAmount0").val("");
    $("#txtAmount1").val("");
    $("#txtAmount2").val("");
    $("#txtAmount3").val("");
    $("#txtAmount4").val("");
    $("#txtAmount5").val("");
    $("#txtAmount6").val("");
    $("#txtAmount7").val("");
    $("#txtAmount8").val("");
    $("#txtAmount9").val("");
    $("#txtAmount10").val("");
    $("#txtAmount11").val("");
    $("#txtAmount12").val("");
    $("#txtAmount13").val("");
    $("#txtAmount14").val("");
    $("#txtAmount15").val("");
    $("#txtAmount16").val("");
    $("#txtAmount17").val("");
    $("#txtAmount18").val("");
    $("#txtAmount19").val("");
    $("#txtUnit0").val("");
    $("#txtUnit1").val("");
    $("#txtUnit2").val("");
    $("#txtUnit3").val("");
    $("#txtUnit4").val("");
    $("#txtUnit5").val("");
    $("#txtUnit6").val("");;
    $("#txtUnit7").val("");
    $("#txtUnit8").val("");
    $("#txtUnit9").val("");
    $("#txtUnit10").val("");
    $("#txtUnit11").val("");
    $("#txtUnit12").val("");
    $("#txtUnit13").val("");
    $("#txtUnit14").val("");
    $("#txtUnit15").val("");
    $("#txtUnit16").val("");
    $("#txtUnit17").val("");
    $("#txtUnit18").val("");
    $("#txtUnit19").val("");
    $("#txtSerUnit0").val("");
    $("#txtSerUnit1").val("");
    $("#txtSerUnit2").val("");
    $("#txtSerUnit3").val("");
    $("#txtSerUnit4").val("");
    $("#txtSerUnit5").val("");
    $("#txtSerUnit6").val("");
    $("#txtSerUnit7").val("");
    $("#txtSerUnit8").val("");
    $("#txtSerUnit9").val("");
    $("#txtSerUnit10").val("");
    $("#txtSerUnit11").val("");
    $("#txtSerUnit19").val("");
    $("#txtSerUnit12").val("");
    $("#txtSerUnit13").val("");
    $("#txtSerUnit14").val("");
    $("#txtSerUnit15").val("");
    $("#txtSerUnit16").val("");
    $("#txtSerUnit17").val("");
    $("#txtSerUnit18").val("");
    $("#txtSerUnit19").val("");
    $("#hdnMatID0").val("");
    $("#hdnUnitID0").val("");
    $("#hdnSerMatID0").val("");
    $("#hdnSerUnitID0").val("");
    $("#hdnMatID1").val("");
    $("#hdnUnitID1").val("");
    $("#hdnSerMatID1").val("");
    $("#hdnSerUnitID1").val("");
    $("#hdnMatID2").val("");
    $("#hdnUnitID2").val("");
    $("#hdnSerMatID2").val("");
    $("#hdnSerUnitID2").val("");
    $("#hdnMatID3").val("");
    $("#hdnUnitID3").val("");
    $("#hdnSerMatID3").val("");
    $("#hdnSerUnitID3").val("");
    $("#hdnMatID4").val("");
    $("#hdnUnitID4").val("");
    $("#hdnSerMatID4").val("");
    $("#hdnSerUnitID4").val("");
    $("#hdnMatID5").val("");
    $("#hdnUnitID5").val("");
    $("#hdnSerMatID5").val("");
    $("#hdnSerUnitID5").val("");
    $("#hdnMatID6").val("");
    $("#hdnUnitID6").val("");
    $("#hdnSerMatID6").val("");
    $("#hdnSerUnitID6").val("");
    $("#hdnMatID7").val("");
    $("#hdnUnitID7").val("");
    $("#hdnSerMatID7").val("");
    $("#hdnSerUnitID7").val("");
    $("#hdnMatID8").val("");
    $("#hdnUnitID8").val("");
    $("#hdnSerMatID8").val("");
    $("#hdnSerUnitID8").val("");
    $("#hdnMatID9").val("");
    $("#hdnUnitID9").val("");
    $("#hdnSerMatID9").val("");
    $("#hdnSerUnitID9").val("");
    $("#hdnMatID10").val("");
    $("#hdnUnitID10").val("");
    $("#hdnSerMatID10").val("");
    $("#hdnSerUnitID10").val("");
    $("#hdnMatID1").val("");
    $("#hdnUnitID1").val("");
    $("#hdnSerMatID1").val("");
    $("#hdnSerUnitID1").val("");
    $("#hdnMatID2").val("");
    $("#hdnUnitID2").val("");
    $("#hdnSerMatID2").val("");
    $("#hdnSerUnitID2").val("");
    $("#hdnMatID3").val("");
    $("#hdnUnitID3").val("");
    $("#hdnSerMatID3").val("");
    $("#hdnSerUnitID3").val("");
    $("#hdnMatID4").val("");
    $("#hdnUnitID4").val("");
    $("#hdnSerMatID4").val("");
    $("#hdnSerUnitID4").val("");
    $("#hdnMatID5").val("");
    $("#hdnUnitID5").val("");
    $("#hdnSerMatID5").val("");
    $("#hdnSerUnitID5").val("");
    $("#hdnMatID6").val("");
    $("#hdnUnitID6").val("");
    $("#hdnSerMatID6").val("");
    $("#hdnSerUnitID6").val("");
    $("#hdnMatID7").val("");
    $("#hdnUnitID7").val("");
    $("#hdnSerMatID7").val("");
    $("#hdnSerUnitID7").val("");
    $("#hdnMatID8").val("");
    $("#hdnUnitID8").val("");
    $("#hdnSerMatID8").val("");
    $("#hdnSerUnitID8").val("");
    $("#hdnMatID9").val("");
    $("#hdnUnitID9").val("");
    $("#hdnSerMatID9").val("");
    $("#hdnSerUnitID9").val("");
    $("#hdnMatID10").val("");
    $("#hdnUnitID10").val("");
    $("#hdnSerMatID10").val("");
    $("#hdnSerUnitID10").val("");
    $("#hdnMatID11").val("");
    $("#hdnUnitID11").val("");
    $("#hdnSerMatID11").val("");
    $("#hdnSerUnitID11").val("");
    $("#hdnMatID12").val("");
    $("#hdnUnitID12").val("");
    $("#hdnSerMatID12").val("");
    $("#hdnSerUnitID12").val("");
    $("#hdnMatID13").val("");
    $("#hdnUnitID13").val("");
    $("#hdnSerMatID13").val("");
    $("#hdnSerUnitID13").val("");
    $("#hdnMatID14").val("");
    $("#hdnUnitID14").val("");
    $("#hdnSerMatID14").val("");
    $("#hdnSerUnitID14").val("");
    $("#hdnMatID15").val("");
    $("#hdnUnitID15").val("");
    $("#hdnSerMatID15").val("");
    $("#hdnSerUnitID15").val("");
    $("#hdnMatID16").val("");
    $("#hdnUnitID16").val("");
    $("#hdnSerMatID16").val("");
    $("#hdnSerUnitID16").val("");
    $("#hdnMatID17").val("");
    $("#hdnUnitID17").val("");
    $("#hdnSerMatID17").val("");
    $("#hdnSerUnitID17").val("");
    $("#hdnMatID18").val("");
    $("#hdnUnitID18").val("");
    $("#hdnSerMatID18").val("");
    $("#hdnSerUnitID18").val("");
    $("#hdnMatID19").val("");
    $("#hdnUnitID19").val("");
    $("#hdnSerMatID19").val("");
    $("#hdnSerUnitID19").val("");

}

function TotalGensetAmt() {
    if (Validation()) {
        var tablelength = $("#tbodyService").children('tr').length;
        var Amt = 0;
        var rate = 0;
        var qty = 0;
        for (var i = 0 ; i < (tablelength) ; i++) {
            if ($("#hdnMatID" + i).val() == "" || $("#hdnMatID" + i).val() == undefined) {
                $("#txtMaterialName" + i).val("");
                $("#txtRate" + i).val("");
                $("#txtQty" + i).val("");
            }
            else {
                if ($("#txtQty" + i).val() == "" || $("#txtQty" + i).val() == undefined) {
                    qty = 0;
                }
                else {
                    qty = $("#txtQty" + i).val();
                }

                if ($("#txtRate" + i).val() == "" || $("#txtRate" + i).val() == undefined) {
                    rate = 0;
                }
                else {
                    rate = $("#txtRate" + i).val();
                }
                Amt += parseFloat(qty) * parseFloat(rate);
            }
        }
        var RoundAmt = Amt.toFixed(2);
        $("[id$='lblTotal']").html(RoundAmt);
        $("input[id*='hdnGensetTotal']").val(RoundAmt);
    }
}

function Validation() {
    var tablelength = $("#tbodyService").children('tr').length;
    for (var i = 0 ; i < (tablelength) ; i++) {
        if ($("#txtQty" + i).val() != undefined) {
            var value = $("#txtQty" + i).val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQty" + i).css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQty" + i).css('border-color', '');
                }
            }
        }
    }
    return true;
}

function TotalServiceAmt() {
    if (serviceValidation()) {
        var tablelength = $("#tbody1d").children('tr').length;
        var Amt = 0;
        var rate = 0;
        var qty = 0;
        for (var i = 0 ; i < (tablelength) ; i++) {
            if ($("#hdnSerMatID" + i).val() == "" || $("#hdnSerMatID" + i).val() == undefined) {
                $("#txtMaterial" + i).val("");
                $("#txtPrice" + i).val("");
                $("#txtQuantity" + i).val("");
            }
            else {
                if ($("#txtQuantity" + i).val() == "" || $("#txtQuantity" + i).val() == undefined) {
                    qty = 0;
                }
                else {
                    qty = $("#txtQuantity" + i).val();
                }

                if ($("#txtPrice" + i).val() == "" || $("#txtPrice" + i).val() == undefined) {
                    rate = 0;
                }
                else {
                    rate = $("#txtPrice" + i).val();
                }
                Amt += parseFloat(qty) * parseFloat(rate);
            }
        }
        var RoundAmt = Amt.toFixed(2);
        $("[id$='lblServiceTotal']").html(RoundAmt);
        $("input[id*='hdnServiceTotal']").val(RoundAmt);
    }
}

function serviceValidation() {

    var tablelength = $("#tbody1d").children('tr').length;
    for (var i = 0 ; i < (tablelength) ; i++) {
        if ($("#txtQuantity" + i).val() != undefined) {
            var value = $("#txtQuantity" + i).val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (value != "") {
                if (!value.match(regex)) {
                    $("#txtQuantity" + i).css('border-color', 'red');
                    return false;
                }
                else {
                    $("#txtQuantity0" + i).css('border-color', '');
                }
            }
        }
    }
    return true;
}



