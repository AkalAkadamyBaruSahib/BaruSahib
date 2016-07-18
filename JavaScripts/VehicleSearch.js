$(document).ready(function () {
    if ($("input[id*='hdnUserTypeID']").val() == 13) {
        AutofillVehicleSearchBox();
    }
    else {
        AutofillVehicleSearchBoxByInchargeID($("input[id*='hdnInchargeID']").val())
    }
});

function AutofillVehicleSearchBox() {
    var dataSrc;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetActiveVehicles",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                $("#txtVehicle").autocomplete({
                    source: result.d
                });

            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });


}

function AutofillVehicleSearchBoxByInchargeID(inchargeid) {
    var dataSrc;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetActiveVehiclesByInchargeID",
        data: JSON.stringify({ InchargeID: parseInt(inchargeid) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                $("#txtVehicle").autocomplete({
                    source: result.d
                });

            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });


}