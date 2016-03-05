
/// <reference path="../jquery-vsdoc.js" />

$(document).ready(function () {

   
});

function test(control)
{
    if (control.value == "2") {
        $("#divPendingDocumentReport").show();
        $("#divDailyReport").hide();
        $("#divSummaryReport").hide();

    }
    else if (control.value == "1") {
        $("#divPendingDocumentReport").hide();
        $("#divDailyReport").show();
        $("#divSummaryReport").hide();
    }
    else if (control.value == "3")
    {
        $("#divPendingDocumentReport").hide();
        $("#divDailyReport").hide();
        $("#divSummaryReport").show();
    }
    else {
        $("#divPendingDocumentReport").hide();
        $("#divDailyReport").hide();
        $("#divSummaryReport").hide();
    }
}