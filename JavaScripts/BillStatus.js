var grdBillDiscription;

$(document).ready(function () {
    LoadBillStatusInfo();
});

function LoadBillStatusInfo() {
    $('#divprogress').modal('show');

    /*create/distroy grid for the new search*/
    if (typeof grdBillDiscription != 'undefined') {
        grdBillDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="zone">abc</td><td id="academy">abc</td><td id="billNo">abc</td><td id="agencyName">abc</td><td id="billAmount">cc</td><td id="hqActivity">cc</td><td id="auditActivity">cc</td><td id="accountActivity">cc</td><td id="reciving">cc</td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetBillStatusDetails",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {

                var adminLoanList = result.d;
                if (adminLoanList.length > 0) {
                    $("#tbody").append(rowTemplate);
                }
                else { $('#divprogress').modal('hide'); }

                for (var i = 0; i < adminLoanList.length; i++) {
                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }

                    var $newRow = $("#rowTemplate").clone();
                    $newRow.find("#zone").html(adminLoanList[i].ZoneName);
                    $newRow.find("#academy").html(adminLoanList[i].AcaName);
                    $newRow.find("#billNo").html("<a class='btn btn-info' href='Admin_BillDetailsAfterApproval.aspx?SubBillId=" + adminLoanList[i].SubBillId + "'>" + adminLoanList[i].SubBillId + "</a>");
                    $newRow.find("#agencyName").html(adminLoanList[i].AgencyName);
                    $newRow.find("#billAmount").html(adminLoanList[i].TotalAmount); 
                    if (adminLoanList[i].FirstVarifyStatus == "1") {
                        $newRow.find("#hqActivity").html("<span class='label label-important' style='font-size: 15.998px;background-color: Green;'>Verified</span>");
                    }
                    else if (adminLoanList[i].FirstVarifyStatus == "0") {
                        $newRow.find("#hqActivity").html("<span class='label label-important' style='font-size: 15.998px;'>Rejected</span>");
                    }
                    else {
                        $newRow.find("#hqActivity").html("<a  href='Admin_ViewBillDetailsForApproval.aspx?SubBillId=" + adminLoanList[i].SubBillId + "'><span class='label label-important' style='font-size: 15.998px;'>Pending</span></a>");
                    }

                    if (adminLoanList[i].SecondVarifyStatus == "1") {
                        $newRow.find("#auditActivity").html("<span class='label label-important' style='font-size: 15.998px;background-color: Green;'>Verified</span>");
                    }
                    else if (adminLoanList[i].SecondVarifyStatus == "0") {
                        $newRow.find("#auditActivity").html("<span class='label label-important' style='font-size: 15.998px;'>Rejected</span>");
                    }
                    else {
                        $newRow.find("#auditActivity").html("<span class='label label-important' style='font-size: 15.998px;'>Pending</span>");

                    }

                    if (adminLoanList[i].PaymentStatus == "1") {
                        $newRow.find("#accountActivity").html("<span class='label label-important' style='font-size: 15.998px;background-color: Green;'>Verified</span>");
                    }
                    else if (adminLoanList[i].PaymentStatus == "0") {
                        $newRow.find("#accountActivity").html("<span class='label label-important' style='font-size: 15.998px;'>Rejected</span>");
                    }
                    else {
                        $newRow.find("#accountActivity").html("<span class='label label-important' style='font-size: 15.998px;'>Pending</span>");
                    }

                    if (adminLoanList[i].RecevingStatus == "1") {
                        $newRow.find("#reciving").html("<span class='label label-important' style='font-size: 15.998px;background-color: Green;'>Verified</span>");
                    }
                    else if (adminLoanList[i].RecevingStatus == "0") {
                        $newRow.find("#reciving").html("<span class='label label-important' style='font-size: 15.998px;'>Rejected</span>");
                    }
                    else {
                        $newRow.find("#reciving").html("<span class='label label-important' style='font-size: 15.998px;'>Pending</span>");
                    }


                    $newRow.find("#reciving").html();

                    $newRow.addClass(className);
                    $newRow.show();

                    if (i == 0) {
                        $("#rowTemplate").replaceWith($newRow);
                    }
                    else {
                        $newRow.appendTo("#grid > tbody");
                    }

                    if (adminLoanList.length == (i + 1)) {
                        $('#divprogress').modal('hide');
                    }
                }
                grdBillDiscription = $('#grid').DataTable(
                {
                    "bPaginate": true,
                    "iDisplayLength": 20,
                    "sPaginationType": "full_numbers",
                    "bSort": false,
                    "bAutoWidth": false,
                    "bLengthChange": false,
                    "bDestroy": true,
                    "bInfo": true
                });
                $("#grid_filter").attr("style", "text-align:right");
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}