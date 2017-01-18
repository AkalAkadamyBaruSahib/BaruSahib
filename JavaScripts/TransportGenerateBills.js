
/// <reference path="../jquery-vsdoc.js" />

$(document).ready(function () {
    $("input[id*=chkboxSelectAll]:checkbox").click(function () {
        if ($(this)[0].checked) {
            $("input[id*='chkvehicle']").each(function (e) {
              //  $("#ContentPlaceHolder1_repVehicle_chkvehicle_0")[0].checked = true;
                $(this).prop('checked', true);
            });
        }
        else {
            $("input[id*='chkvehicle']").each(function (e) {
                $(this).prop('checked', false);
            });
        }
    });

    $("input[id*=btnDownload]").click(function () {
        var chks = $("[id*=repVehicle] [id*=chkvehicle]").is(":checked");
        if (!chks) {
            alert("Please select at least one checkbox..!");
            return false;
        }
    });

});

function BindVehicles(acaID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetContracturalVehiclesByAcaID",
        data: JSON.stringify({ AcaID: parseInt(acaID), TypeID: 2 }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("input[id*='chkvehicle']").append($("<option></option>").val(value.ID).html(value.Number));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}
