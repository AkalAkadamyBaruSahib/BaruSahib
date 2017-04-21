

$(document).ready(function () {
    if ($("input[id*='hdnUserType']").val() == 23)
    {
        $.when(DrawRateNonApprovedChart()).then(hideAtag());
        $("#trPurchase").show();
    }
    else
    {
        $.when(DrawChart(), DrawDrawingChart(), DrawBillsChart()).then(hideAtag());
        $("#trAdmin").show();
    }

});

function hideAtag()
{
    $("a").each(function (e) {
        var href = $(this)[0].href;
        if (href == "http://canvasjs.com/") {
            $(this).hide();
        }
    });
}

function DrawChart() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/AdminController.asmx/GetEstimateChartData",
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;

                var chart = new CanvasJS.Chart("chartContainer", {
                    title: {
                        text: "All Academy Estimates"
                    },
                    data: [
                    {
                        type: "column",
                        dataPoints: [
                            { label: "Total", y: Result.TotalEstimates },
                            { label: "Approved", y: Result.ApprovedEstimates },
                            { label: "NotApproved", y: Result.NonApprovedEstimates }
                        ],
                        click: onClick
                    }
                    ]
                });
                chart.render();
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function onClick(e) {
    var event = e;
    if (e.dataPointIndex == 1) {
        location.href = "Admin_EstimateView.aspx";
    }
    else if (e.dataPointIndex == 2) {
        location.href = "Admin_EstimateView.aspx?fromChartNonApproved=1";
    }
}

function DrawDrawingChart() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/AdminController.asmx/GetDrawingChartData",
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;

                var chart = new CanvasJS.Chart("divDrawingChart", {
                    title: {
                        text: "All Academy Drawings"
                    },
                    data: [
                    {
                        type: "column",
                        dataPoints: [
                            { label: "Total", y: Result.TotalDrawings },
                            { label: "Approved", y: Result.ApprovedDrawings },
                            { label: "NotApproved", y: Result.NonApprovedDrawings }
                        ],
                        click: DrawingButton_onClick
                    }
                    ]
                });
                chart.render();
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function DrawingButton_onClick(e) {
    var event = e;
    if (e.dataPointIndex == 1) {
        location.href = "Admin_DrawingView.aspx";
    }
    else if (e.dataPointIndex == 2) {
        location.href = "Admin_DrawingView.aspx?fromChartNonApproved=1";
    }
}

function DrawBillsChart() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/AdminController.asmx/GetSubmitBillChartData",
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;

                var chart = new CanvasJS.Chart("divBillsChart", {
                    title: {
                        text: "Approved/Non Approved Bills"
                    },
                    data: [
                    {
                        type: "column",
                        dataPoints: [
                            { label: "Total", y: Result.TotalLocalPurchased },
                            { label: "Approved", y: Result.ApprovedBills },
                            { label: "NotApproved", y: Result.NonApprovedBills }
                        ],
                        click: BillButton_onClick
                    }
                    ]
                });
                chart.render();
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BillButton_onClick(e)
{ }

function DrawRateNonApprovedChart() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetRateNonApprovedChartData",
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;

                var chart = new CanvasJS.Chart("divRateNonApprovedChart", {
                    title: {
                        text: "All  Non Approved Rates"
                    },
                    data: [
                    {
                        type: "column",
                        dataPoints: [
                            { label: "ApprovedRates", y: Result.ApprovedRates },
                            { label: "NotApprovedRates", y: Result.NonApprovedRates }
                        ],
                        click: RateNonApprovedButton_onClick
                    }
                    ]
                });
                chart.render();
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function RateNonApprovedButton_onClick(e) {
    var event = e;
    if (e.dataPointIndex == 1) {
        location.href = "RateApproved.aspx";
    }
    else if (e.dataPointIndex == 2) {
        location.href = "RateUpload.aspx";
    }
}