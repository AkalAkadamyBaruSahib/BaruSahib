$(document).ready(function () {
 
});

function ReturnEstimateMaterial(emrid) {
    $("input[id*='hidEMRID']").val(emrid);

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/WorkshopController.asmx/ReturnEstimateMaterial",
        data: JSON.stringify({ EMRID: emrid }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                alert(" Material Item Return Successfully");
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
        }
    });

}