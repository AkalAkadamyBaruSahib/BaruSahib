
$(document).ready(function () {

   
});

function GetMaterialDetails(MatID) {
   
    $('#myModal').modal('show');
    /*create/distroy grid for the new search*/
    if (typeof grdTicketDiscription != 'undefined') {
        grdTicketDiscription.fnClearTable();
    }

    var rowCount = $('#grdMatDiscription').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }


    var rowTemplate = '<tr id="rowTemplate"><td id="BillNo"><td id="AgencyName"></td><td id="MatName"></td><td id="Quantity"></td><td id="Rate"></td><td id="StockEntryNo"></td><td id="CreatedOn"></td></tr>';


    var WorkAllotID = $("input[id$='hdnWorkAllotID']").val();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/ConstructionUserController.asmx/GetMaterialDetails",
        data: JSON.stringify({ MatID: MatID, WorkAllotID: WorkAllotID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var adminLoanList = $.parseJSON(result.d);

                if (adminLoanList.length > 0) {
                    $("#tbody").append(rowTemplate);
                }

                for (var i = 0; i < adminLoanList.length; i++) {
                    var className = "ReportGridItem";
                    if (i % 2 == 0) {
                        className = "ReportGridAlternatingItem";
                    }

                    var $newRow = $("#rowTemplate").clone();

                    $newRow.find("#BillNo").html("<span class='label label-success'>" + adminLoanList[i].SubBillId + "</span>");
                    $newRow.find("#AgencyName").html(adminLoanList[i].AgencyName);
                    $newRow.find("#MatName").html(adminLoanList[i].ItemName);
                    $newRow.find("#Quantity").html(adminLoanList[i].Qty);
                    $newRow.find("#Rate").html(adminLoanList[i].Rate);
                    $newRow.find("#StockEntryNo").html(adminLoanList[i].StockEntryNo);
                    $newRow.find("#CreatedOn").html(adminLoanList[i].SBCreatedOn);

                    $newRow.addClass(className);
                    $newRow.show();
                    if (i == 0) {
                        $("#rowTemplate").replaceWith($newRow);
                    }
                    else {
                        $newRow.appendTo("#grdMatDiscription > tbody");
                    }
                    $("#grdMatDiscription").removeAttr("style");

                }

                grdTicketDiscription = $('#grdMatDiscription').DataTable();
                $("#grdMatDiscription_length").hide();
                $("#grdMatDiscription_filter").hide();
                $("#grdMatDiscription_info").hide();
                $("#grdMatDiscription_paginate").hide();
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}
