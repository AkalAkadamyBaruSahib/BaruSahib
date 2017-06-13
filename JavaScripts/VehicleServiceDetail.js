var VehicleServiceInfo;
$(document).ready(function () {
    BindAcademy($("input[id*='hdnInchargeID']").val());
    $("select[id*='drpAcademy']").change(function () {
        BindVehicles($(this).val(), 0);
        ClearonChangeFunction();
       
    });

    $("select[id*='drpVehicle']").change(function () {
        GetVehiclesSitterCapacity($(this).val());
        ClearonChangeFunction();
    });

    $("input[id*='btnSaveVehicleService']").click(function (e) {
        if (Page_ClientValidate("vehicle")) {
            if (Validation()) {
                ClientSideClick(this);
                $("#btnSaveVehicleService").prop('disabled', true);
                SaveVehicleServiceDeatil();
            }
        }
    });

    $("input[id*='btnEdit']").click(function (e) {
        if (Page_ClientValidate("vehicle")) {
            if (Validation()) {
                ClientSideClick(this);
                $("#btnEdit").prop('disabled', true);
                UpdateVehicleServiceDeatil();
            }
        }
    });

    $("#aMeterReading").hide();

    LoadVehicleServiceInfo();

   });
function BindVehicles(acaID,vehicleID) {
  
    $("select[id*='drpVehicle'] option").each(function (index, option) {
        $(option).remove();
    });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetTrustVehiclesByAcademyIdAndTypeID",
        data: JSON.stringify({ AcaID: parseInt(acaID), TypeID: 1 }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $("select[id*='drpVehicle']").append($("<option></option>").val("0").html("--Select Vehicle--"));
                $.each(Result, function (key, value) {
                    $("select[id*='drpVehicle']").append($("<option></option>").val(value.ID).html(value.Number));
                });
            }
            if (vehicleID != 0) {
                $("select[id*='drpVehicle']").val(vehicleID);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetVehiclesSitterCapacity(vehicleID) {
    $("input[id*='hdnSeatedCapacity']").val("");
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetVehiclesInfoByVehicleID",
        data: JSON.stringify({ VehicleID: parseInt(vehicleID) }),
        dataType: "json",
        success: function (responce) {
            var rdata = responce.d;
            if (rdata != undefined) {
                $("input[id*='hdnSeatedCapacity']").val(rdata.Sitter);
                GetTyreInfoBySeatingCapacity(rdata.Sitter);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindAcademy(inchargeId) {
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
                    $("select[id*='drpAcademy']").append($("<option></option>").val(value.AcaID).html(value.AcaName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetTyreInfoBySeatingCapacity(sittingCapacity) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetVehiclesTyreCountBySitting",
        data: JSON.stringify({ seatingCapacity: parseInt(sittingCapacity) }),
        dataType: "json",
        success: function (responce) {
            var rdata = responce.d;
            if (rdata != undefined) {
                $("input[id*='hdnTyreNum']").val(rdata.NumOfTyre);
                if (rdata.NumOfTyre == 7) {
                    $("#trTyre").show();
                    $("#trRearLeftTwo").show();
                    $("#trRearRightTwo").show();
                    $("#txtRearLeftTwoRunning").prop('disabled', false);
                    $("#txtRearLeftTwoOldTyreNo").prop('disabled', false);
                    $("#txtRearRightTwoRunning").prop('disabled', false);
                    $("#txtRearRightTwoOldTyreNo").prop('disabled', false);
                }
                else {
                    $("#trTyre").show();
                    $("#trRearLeftTwo").hide();
                    $("#trRearRightTwo").hide();
                    $("#txtRearRightTwoRunning").val(0);
                    $("#txtRearLeftTwoOldTyreNo").val(0);
                    $("#txtRearLeftTwoRunning").val(0);
                    $("#txtRearRightTwoOldTyreNo").val(0);
                    $("#txtRearRightTwoRunning").prop('disabled', true);
                    $("#txtRearLeftTwoOldTyreNo").prop('disabled', true);
                    $("#txtRearLeftTwoRunning").prop('disabled', true);
                    $("#txtRearRightTwoOldTyreNo").prop('disabled', true);
                }
            }
            else {
                $("#trTyre").show();
                $("#trRearLeftTwo").hide();
                $("#trRearRightTwo").hide();
                $("#txtRearRightTwoRunning").val(0);
                $("#txtRearLeftTwoOldTyreNo").val(0);
                $("#txtRearLeftTwoRunning").val(0);
                $("#txtRearRightTwoOldTyreNo").val(0);
                $("#txtRearRightTwoRunning").prop('disabled', true);
                $("#txtRearLeftTwoOldTyreNo").prop('disabled', true);
                $("#txtRearLeftTwoRunning").prop('disabled', true);
                $("#txtRearRightTwoOldTyreNo").prop('disabled', true);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function SaveVehicleServiceDeatil() {
    var params = new Object();
    var VehicleServiceRecord = new Object();
    VehicleServiceRecord.ID = 0;
    VehicleServiceRecord.VehicleID = $("select[id*='drpVehicle']").val();
    VehicleServiceRecord.AcaID = $("select[id*='drpAcademy']").val();
    VehicleServiceRecord.CurrentKm = $("input[id*='txtCurrentMeterReading']").val();
    VehicleServiceRecord.FrontLeftKm = $("#txtFrontLeftRunning").val();
    VehicleServiceRecord.FrontRightKm = $("#txtFrontRightRunning").val();
    VehicleServiceRecord.FrontLeftSerialNum = $("#txtFrontLeftOldTyreNo").val();
    VehicleServiceRecord.FrontRightSerialNum = $("#txtFrontRightOldTyreNo").val();
    VehicleServiceRecord.RearLeftOneKm = $("#txtRearLeftRunning").val();
    VehicleServiceRecord.RearLeftSecondKm = $("#txtRearLeftTwoRunning").val();
    VehicleServiceRecord.RearRightOneKm = $("#txtRearRightRunning").val();
    VehicleServiceRecord.RearRightSecondKm = $("#txtRearRightTwoRunning").val();
    VehicleServiceRecord.RearLeftOneSerialNum = $("#txtRearLeftOldTyreNo").val();
    VehicleServiceRecord.RearLeftSecondSerialNum = $("#txtRearLeftTwoOldTyreNo").val();
    VehicleServiceRecord.RearRightOneSerialNum = $("#txtRearRightOldTyreNo").val();
    VehicleServiceRecord.RearRightSecondSerialNum = $("#txtRearRightTwoOldTyreNo").val();
    VehicleServiceRecord.LastServiceKm = $("input[id*='txtLastServiceKM']").val();
    VehicleServiceRecord.StafneySerialNum = $("#txtStafneyldTyreNo").val();
    VehicleServiceRecord.StafneyKm = $("#txtStafneyRunning").val();
    VehicleServiceRecord.MeterReadingFilePath = "";
    VehicleServiceRecord.BatteryInstalationDate = $("input[id*='txtBattery']").val();
    VehicleServiceRecord.CreatedOn = "";
    VehicleServiceRecord.CreatedBy = $("input[id*='hdnInchargeID']").val();
    VehicleServiceRecord.LastServiceDate= $("input[id*='txtLastServiceDate']").val();
    VehicleServiceRecord.MakeofBattery = $("input[id*='txtMakeofbattery']").val();
    VehicleServiceRecord.BatteryCapacity = $("input[id*='txtBatteryCapacity']").val();
    VehicleServiceRecord.BatterySerialNum = $("input[id*='txtBatterySerialNum']").val();
    VehicleServiceRecord.BatteryLifeInYears = $("input[id*='txtBatteryLifeInYears']").val();
  
    params.VehicleServiceRecord = VehicleServiceRecord;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/SaveVehicleDetailService",
        data: JSON.stringify(params),
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            if (textStatus == "success") {
                $("input[id*='hdnVehicleServiceID']").val(result.d);
                MeterReadingFilePath(result.d);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }

    });

}

function MeterReadingFilePath(vserviceid) {
    var files = $("input[id*='fileCurrentMeterReading']")[0].files;

    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }

    $.ajax({
        url: "VehicleCurrentMeterReadingHandler.ashx?VehicleServiceID=" + vserviceid + "&VehicleNumber=" + $("select[id*='drpVehicle'] option:selected").text(),
        type: "POST",
        async: false,
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            ClearText();
            alert("Vehicle Service Detail Create Successfuly");
            $("#btnSaveVehicleService").prop('disabled', false);
            $("#btnEdit").prop('disabled', false);
            LoadVehicleServiceInfo();
           
        },
        error: function (err) {
            alert(err.statusText)
        }
    });
}

function ClearText() {
    $("select[id*='drpAcademy']").val("");
    $("select[id*='drpVehicle']").val("");
    $("input[id*='txtCurrentMeterReading']").val("");
    $("input[id*='txtBattery']").val("");
    $("input[id*='txtLastServiceKM']").val("");
    $("#txtFrontLeftRunning").val("");
    $("#txtFrontRightRunning").val("");
    $("#txtFrontLeftOldTyreNo").val("");
    $("#txtFrontRightOldTyreNo").val("");
    $("#txtRearLeftRunning").val("");
    $("#txtRearLeftTwoRunning").val("");
    $("#txtRearRightRunning").val("");
    $("#txtRearRightTwoRunning").val("");
    $("#txtRearLeftOldTyreNo").val("");
    $("#txtRearLeftTwoOldTyreNo").val("");
    $("#txtRearRightOldTyreNo").val("");
    $("#txtRearRightTwoOldTyreNo").val("");
    $("#txtStafneyldTyreNo").val("");
    $("#txtStafneyRunning").val("");
    $("input[id*='txtLastServiceDate']").val("");
    $("input[id*='txtMakeofbattery']").val("");
    $("input[id*='txtBatteryCapacity']").val("");
    $("input[id*='txtBatterySerialNum']").val("");
    $("input[id*='txtBatteryLifeInYears']").val("");
   
}

function LoadVehicleServiceInfo() {
    var InchargeID =$("input[id*='hdnInchargeID']").val();
    /*create/distroy grid for the new search*/
    if (typeof grdVehicleServiceInfo != 'undefined') {
        VehicleServiceInfo.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="Academy">abc</td><td id="Number">abc</td><td id="service">abc</td><td id="createdon">abc</td><td id="action">cc</td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetLoadVehicleServiceInformation",
        data: JSON.stringify({ inchargeID: parseInt(InchargeID) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var adminLoanList = result.d;
                if (adminLoanList.length > 0) {
                    $("#tbody").append(rowTemplate);
                }
             
                for (var i = 0; i < adminLoanList.length; i++) {
                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }
                  
                    var $newRow = $("#rowTemplate").clone();
                    $newRow.find("#Academy").html(adminLoanList[i].Academy.AcaName);
                    $newRow.find("#Number").html(adminLoanList[i].Vehicles.Number);
                    //var scanLink = OpenCurrentMeterReading(adminLoanList[i].MeterReadingFilePath);
                    //if (scanLink != null) {
                    //    $newRow.find("#curntMeter").html("<table><tr><td><b>Current Meter Reading :</b> " + adminLoanList[i].CurrentKm + "</td></tr><tr><td>" + scanLink + "</td></tr></table>");
                    //}
                    //else {
                    //    $newRow.find("#curntMeter").html("<table><tr><td><b>Current Meter Reading :</b> " + adminLoanList[i].CurrentKm + "</td></tr></table>");
                    //}
                    $newRow.find("#service").html("<table><tr><td><b>Last Service KM :</b> " + adminLoanList[i].LastServiceKm + "</td></tr><tr><td><b>Last Service Date:</b>" + getJsonDate(adminLoanList[i].LastServiceDate) + "</td></tr></table>");
                    $newRow.find("#createdon").html("<table><tr><td><b>Battery Instalation Date:</b> " + getJsonDate(adminLoanList[i].BatteryInstalationDate) + "</td></tr><tr><td><b>Make Of Battery:</b>" + adminLoanList[i].MakeofBattery + "</td></tr></table>");
                    $newRow.find("#action").html("<table><tr><td><a href='#' onclick='GetVehicleInfoToUpdate(" + adminLoanList[i].ID + ")'>Edit</a></td></tr></table>");
                    $newRow.addClass(className);
                    $newRow.show();

                    if (i == 0) {
                        $("#rowTemplate").replaceWith($newRow);
                    }
                    else {
                        $newRow.appendTo("#grid > tbody");
                    }
                    $("#grid").removeAttr("style");

                }
                VehicleServiceInfo = $('#grid').DataTable(
                {
                    "bPaginate": true,
                    "iDisplayLength": 20,
                    "sPaginationType": "full_numbers",
                    "bAutoWidth": false,
                    "bLengthChange": false,
                    "bDestroy": true

                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetVehicleInfoToUpdate(vehicleServiceID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetGetVehicleInfoToUpdate",
        data: JSON.stringify({ VehicleServiceID: vehicleServiceID }),
        dataType: "json",
        success: function (responce) {

            var rdata = responce.d;
            if (rdata != undefined) {
                $("input[id*='hdnVehicleServiceID']").val(rdata.ID);
                $("select[id*='drpAcademy']").val(rdata.AcaID);
                $("select[id*='drpVehicle']").val(rdata.VehicleID);
                BindVehicles(rdata.AcaID, rdata.VehicleID);
                $("input[id*='txtCurrentMeterReading']").val(rdata.CurrentKm);
                $("#txtFrontLeftRunning").val(rdata.FrontLeftKm);
                $("#txtFrontRightRunning").val(rdata.FrontRightKm);
                $("#txtFrontLeftOldTyreNo").val(rdata.FrontLeftSerialNum);
                $("#txtFrontRightOldTyreNo").val(rdata.FrontRightSerialNum);
                $("#txtRearLeftRunning").val(rdata.RearLeftOneKm);
                $("#txtRearLeftTwoRunning").val(rdata.RearLeftSecondKm);
                $("#txtRearRightRunning").val(rdata.RearRightOneKm);
                $("#txtRearRightTwoRunning").val(rdata.RearRightSecondKm);
                $("#txtRearLeftOldTyreNo").val(rdata.RearLeftOneSerialNum);
                $("#txtRearLeftTwoOldTyreNo").val(rdata.RearLeftSecondSerialNum);
                $("#txtRearRightOldTyreNo").val(rdata.RearRightOneSerialNum);
                $("#txtRearRightTwoOldTyreNo").val(rdata.RearRightSecondSerialNum);
                $("input[id*='txtLastServiceKM']").val(rdata.LastServiceKm);
                $("#txtStafneyldTyreNo").val(rdata.StafneySerialNum);
                $("#txtStafneyRunning").val(rdata.StafneyKm);
                $("input[id*='txtBattery']").val(getJsonDate(rdata.BatteryInstalationDate));
                $("input[id*='txtLastServiceDate']").val(getJsonDate(rdata.LastServiceDate));
                $("input[id*='txtMakeofbattery']").val(rdata.MakeofBattery);
                $("input[id*='txtBatteryCapacity']").val(rdata.BatteryCapacity);
                $("input[id*='txtBatterySerialNum']").val(rdata.BatterySerialNum);
                $("input[id*='txtBatteryLifeInYears']").val(rdata.BatteryLifeInYears);
                if (rdata.MeterReadingFilePath != "") {
                    $("#aMeterReading").show();
                    var meterReadingFilePics = rdata.MeterReadingFilePath.split(',');
                    var link = "";
                    for (var i = 0; i < meterReadingFilePics.length; i++) {
                        link += " <a href='" + meterReadingFilePics[i] + "' target='_blank'>CurrentMeterReading_" + (i + 1) + "</a>";
                    }
                    $("#aMeterReading")[0].innerHTML = link;
                }
                GetVehiclesSitterCapacity(rdata.VehicleID);
                EnableDisabledValidator();
                $("input[id*='btnEdit'] ").show();
                $("input[id*='btnSaveVehicleService'] ").hide();
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
        }
    });
}

function getJsonDate(strDate) {
    var displayDate = "";
    if (strDate != null) {
        var date = new Date(parseInt(strDate.substr(6)));
        // format display date (e.g. 04/10/2012)
        displayDate = $.datepicker.formatDate("dd/mm/yy", date);
    }
    return displayDate;
}

function UpdateVehicleServiceDeatil() {
    var updateParams = new Object();
    var VehicleServiceRecord = new Object();
    VehicleServiceRecord.ID = $("input[id*='hdnVehicleServiceID']").val();
    VehicleServiceRecord.VehicleID = $("select[id*='drpVehicle']").val();
    VehicleServiceRecord.AcaID = $("select[id*='drpAcademy']").val();
    VehicleServiceRecord.CurrentKm = $("input[id*='txtCurrentMeterReading']").val();
    VehicleServiceRecord.FrontLeftKm = $("#txtFrontLeftRunning").val();
    VehicleServiceRecord.FrontRightKm = $("#txtFrontRightRunning").val();
    VehicleServiceRecord.FrontLeftSerialNum = $("#txtFrontLeftOldTyreNo").val();
    VehicleServiceRecord.FrontRightSerialNum = $("#txtFrontRightOldTyreNo").val();
    VehicleServiceRecord.RearLeftOneKm = $("#txtRearLeftRunning").val();
    VehicleServiceRecord.RearLeftSecondKm = $("#txtRearLeftTwoRunning").val();
    VehicleServiceRecord.RearRightOneKm = $("#txtRearRightRunning").val();
    VehicleServiceRecord.RearRightSecondKm = $("#txtRearRightTwoRunning").val();
    VehicleServiceRecord.RearLeftOneSerialNum = $("#txtRearLeftOldTyreNo").val();
    VehicleServiceRecord.RearLeftSecondSerialNum = $("#txtRearLeftTwoOldTyreNo").val();
    VehicleServiceRecord.RearRightOneSerialNum = $("#txtRearRightOldTyreNo").val();
    VehicleServiceRecord.RearRightSecondSerialNum = $("#txtRearRightTwoOldTyreNo").val();
    VehicleServiceRecord.LastServiceKm = $("input[id*='txtLastServiceKM']").val();
    VehicleServiceRecord.StafneySerialNum = $("#txtStafneyldTyreNo").val();
    VehicleServiceRecord.StafneyKm = $("#txtStafneyRunning").val();
    VehicleServiceRecord.BatteryInstalationDate = $("input[id*='txtBattery']").val();
    VehicleServiceRecord.LastServiceDate = $("input[id*='txtLastServiceDate']").val();
    VehicleServiceRecord.MakeofBattery = $("input[id*='txtMakeofbattery']").val();
    VehicleServiceRecord.BatteryCapacity = $("input[id*='txtBatteryCapacity']").val();
    VehicleServiceRecord.BatterySerialNum = $("input[id*='txtBatterySerialNum']").val();
    VehicleServiceRecord.BatteryLifeInYears = $("input[id*='txtBatteryLifeInYears']").val();
 
 
    updateParams.VehicleServiceRecord = VehicleServiceRecord;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/UpdateVehicleDetailService",
        data: JSON.stringify(updateParams),
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            if (textStatus == "success") {
                if ($("input[id*='fileCurrentMeterReading']")[0].files.length != 0) {
                    MeterReadingFilePath($("input[id*='hdnVehicleServiceID']").val());
                }
                else {
                    ClearText();
                    alert("Vehicle Service Detail Create Successfuly");
                    $("#btnSaveVehicleService").prop('disabled', false);
                    $("#btnEdit").prop('disabled', false);
                    LoadVehicleServiceInfo();
                }
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }

    });
}

function EnableDisabledValidator() {
    if (typeof Page_Validators != 'undefined') {
        for (i = 0; i <= Page_Validators.length; i++) {
            var vldGrp = null;
            if (Page_Validators[i] != null) {
                if (Page_Validators[i].controltovalidate == $("input[id*='fileCurrentMeterReading']")[0].id) {
                    ValidatorEnable(Page_Validators[i], false);
                };
            }
        }
    };
}

function OpenCurrentMeterReading(scan) {
    if (scan != null) {
        var currentMeterReadingPics = scan.split(',');
        var dllink = "";
        for (var i = 0; i < currentMeterReadingPics.length; i++) {
            dllink += " <a href='" + currentMeterReadingPics[i] + "' target='_blank'>CurrentMeterReadingCopy_" + (i + 1) + "</a>";
        }
        return dllink;
    }
}

function Validation() {

    if ($("#txtFrontRightOldTyreNo").val() != undefined) {
        var value = $("#txtFrontRightOldTyreNo").val();
        var regex = new RegExp(/^\+?[0-9(),-]+$/);

        if ($("#txtFrontRightOldTyreNo").val() == "") {
            $("#txtFrontRightOldTyreNo").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtFrontRightOldTyreNo").css('border-color', '');
        }
    }

    if ($("#txtFrontRightRunning").val() != undefined) {
        var value = $("#txtFrontRightRunning").val();
        var regex = new RegExp(/^\+?[0-9(),-]+$/);

        if ($("#txtFrontRightRunning").val() == "" || !value.match(regex)) {
            $("#txtFrontRightRunning").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtFrontRightRunning").css('border-color', '');
        }
    }

    if ($("#txtFrontLeftOldTyreNo").val() != undefined) {
        var value = $("#txtFrontLeftOldTyreNo").val();
        var regex = new RegExp(/^\+?[0-9(),-]+$/);

        if ($("#txtFrontLeftOldTyreNo").val() == "") {
            $("#txtFrontLeftOldTyreNo").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtFrontLeftOldTyreNo").css('border-color', '');
        }
    }

    if ($("#txtFrontLeftRunning").val() != undefined) {
        var value = $("#txtFrontLeftRunning").val();
        var regex = new RegExp(/^\+?[0-9(),-]+$/);

        if ($("#txtFrontLeftRunning").val() == "" || !value.match(regex)) {
            $("#txtFrontLeftRunning").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtFrontLeftRunning").css('border-color', '');
        }
    }

    if ($("#txtRearRightOldTyreNo").val() != undefined) {
        var value = $("#txtRearRightOldTyreNo").val();
        var regex = new RegExp(/^\+?[0-9(),-]+$/);

        if ($("#txtRearRightOldTyreNo").val() == "") {
            $("#txtRearRightOldTyreNo").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtRearRightOldTyreNo").css('border-color', '');
        }
    }

    if ($("#txtRearRightRunning").val() != undefined) {
        var value = $("#txtRearRightRunning").val();
        var regex = new RegExp(/^\+?[0-9(),-]+$/);

        if ($("#txtRearRightRunning").val() == "" || !value.match(regex)) {
            $("#txtRearRightRunning").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtRearRightRunning").css('border-color', '');
        }
    }

    if ($("#txtRearLeftOldTyreNo").val() != undefined) {
        var value = $("#txtRearLeftOldTyreNo").val();
        var regex = new RegExp(/^\+?[0-9(),-]+$/);

        if ($("#txtRearLeftOldTyreNo").val() == "") {
            $("#txtRearLeftOldTyreNo").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtRearLeftOldTyreNo").css('border-color', '');
        }
    }
   
    if ($("#txtRearLeftRunning").val() != undefined) {
        var value = $("#txtRearLeftRunning").val();
        var regex = new RegExp(/^\+?[0-9(),-]+$/);

        if ($("#txtRearLeftRunning").val() == "" || !value.match(regex)) {
            $("#txtRearLeftRunning").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtRearLeftRunning").css('border-color', '');
        }
    }
  
    if ($("#txtRearLeftTwoOldTyreNo").val() != undefined) {
        var value = $("#txtRearLeftTwoOldTyreNo").val();
        var regex = new RegExp(/^\+?[0-9(),-]+$/);

        if ($("#txtRearLeftTwoOldTyreNo").val() == "") {
            $("#txtRearLeftTwoOldTyreNo").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtRearLeftTwoOldTyreNo").css('border-color', '');
        }
    }

    if ($("#txtRearLeftTwoRunning").val() != undefined) {
        var value = $("#txtRearLeftTwoRunning").val();
        var regex = new RegExp(/^\+?[0-9(),-]+$/);

        if ($("#txtRearLeftTwoRunning").val() == "" || !value.match(regex)) {
            $("#txtRearLeftTwoRunning").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtRearLeftTwoRunning").css('border-color', '');
        }
    }
   
    if ($("#txtRearRightTwoOldTyreNo").val() != undefined) {
        var value = $("#txtRearRightTwoOldTyreNo").val();
        var regex = new RegExp(/^\+?[0-9(),-]+$/);

        if ($("#txtRearRightTwoOldTyreNo").val() == "") {
            $("#txtRearRightTwoOldTyreNo").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtRearRightTwoOldTyreNo").css('border-color', '');
        }
    }

    if ($("#txtRearRightTwoRunning").val() != undefined) {
        var value = $("#txtRearRightTwoRunning").val();
        var regex = new RegExp(/^\+?[0-9(),-]+$/);

        if ($("#txtRearRightTwoRunning").val() == "" || !value.match(regex)) {
            $("#txtRearRightTwoRunning").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtRearRightTwoRunning").css('border-color', '');
        }
    }
   
    if ($("#txtStafneyldTyreNo").val() != undefined) {
        var value = $("#txtStafneyldTyreNo").val();
        var regex = new RegExp(/^\+?[0-9(),-]+$/);

        if ($("#txtStafneyldTyreNo").val() == "") {
            $("#txtStafneyldTyreNo").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtStafneyldTyreNo").css('border-color', '');
        }
    }
    if ($("#txtStafneyRunning").val() != undefined) {
        var value = $("#txtStafneyRunning").val();
        var regex = new RegExp(/^\+?[0-9(),-]+$/);

        if ($("#txtStafneyRunning").val() == "" || !value.match(regex)) {
            $("#txtStafneyRunning").css('border-color', 'red');
            return false;
        }
        else {
            $("#txtStafneyRunning").css('border-color', '');
        }
    }
   
    return true;
}

function ClearonChangeFunction() {
    $("input[id*='txtCurrentMeterReading']").val("");
    $("input[id*='txtBattery']").val("");
    $("input[id*='txtLastServiceKM']").val("");
    $("#txtFrontLeftRunning").val("");
    $("#txtFrontRightRunning").val("");
    $("#txtFrontLeftOldTyreNo").val("");
    $("#txtFrontRightOldTyreNo").val("");
    $("#txtRearLeftRunning").val("");
    $("#txtRearLeftTwoRunning").val("");
    $("#txtRearRightRunning").val("");
    $("#txtRearRightTwoRunning").val("");
    $("#txtRearLeftOldTyreNo").val("");
    $("#txtRearLeftTwoOldTyreNo").val("");
    $("#txtRearRightOldTyreNo").val("");
    $("#txtRearRightTwoOldTyreNo").val("");
    $("#txtStafneyldTyreNo").val("");
    $("#txtStafneyRunning").val("");
    $("input[id*='txtLastServiceDate']").val("");
    $("input[id*='txtMakeofbattery']").val("");
    $("input[id*='txtBatteryCapacity']").val("");
    $("input[id*='txtBatterySerialNum']").val("");
    $("input[id*='txtBatteryLifeInYears']").val("");
    $("#aMeterReading").hide();
}
