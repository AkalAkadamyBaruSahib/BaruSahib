
/// <reference path="../jquery-vsdoc.js" />

$(document).ready(function () {

   
});

function test(control)
{
    if (control.value == "2") {
        $("#divPendingDocumentReport").show();
        $("#divDailyReport").hide();

    }
    else if (control.value == "1") {
        $("#divPendingDocumentReport").hide();
        $("#divDailyReport").show();
    }
    else {
        $("#divPendingDocumentReport").hide();
        $("#divDailyReport").hide();
    }
}