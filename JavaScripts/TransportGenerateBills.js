
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
$(function () {
    $('input[name="chkvehicle"]').click(function () {
        if ($('input[name="chkvehicle"]').length == $('input[name="chkvehicle"]:checked').length) {
            $('input:checkbox[name="chkboxSelectAll"]').attr("checked", "checked");
        }
        else {
            $('input:checkbox[name="chkboxSelectAll"]').removeAttr("checked");
        }
    });
    $('input:checkbox[name="chkboxSelectAll"]').click(function () {
        var slvals = []
        if ($(this).is(':checked')) {
            $('input[name="chkvehicle"]').attr("checked", true);
        }
        else {
            $('input[name="chkvehicle"]').attr("checked", false);
            slvals = null;
        }
    });
})