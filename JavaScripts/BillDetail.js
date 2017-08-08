var grdBillDiscription;

$(document).ready(function () {
    LoadBillForApprovalInfo(0);
});

function LoadBillForApprovalInfo(acaId) {
    $('#divprogress').modal('show');

    /*create/distroy grid for the new search*/
    if (typeof grdBillDiscription != 'undefined') {
        grdBillDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="billDeatil">abc</td><td id="zone">abc</td><td id="academy">abc</td><td id="amount">cc</td><td id="chargeable">cc</td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetBillDetails",
        data: JSON.stringify({ acaID: acaId }),
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
                    $newRow.find("#billDeatil").html("<table><tr><td><b>Bill No: </b>" + adminLoanList[i].SubBillId + "</td></tr><tr><td><b>Bill Submission Date:</b> " + adminLoanList[i].BillDate + "</td></tr><tr><td><b>Agency Namee:</b> " + adminLoanList[i].AgencyName + "</td></tr></table>");
                    $newRow.find("#zone").html(adminLoanList[i].ZoneName);
                    $newRow.find("#academy").html(adminLoanList[i].AcaName);
                    $newRow.find("#amount").html("<table><tr><td><b>Total Amount: </b>" + adminLoanList[i].TotalAmount + "</td></tr><tr><td><a class='btn btn-info' target='_blank' href='InVoice.aspx?BillId=" + adminLoanList[i].SubBillId + "'><i class='icon-edit icon-white'></i>Print Bill</a></td></tr></table>");
                    if (adminLoanList[i].BillType == "1") {
                        $newRow.find("#chargeable").html("<table><tr><td><b>Sanctioned</b></td></tr><tr><td><b>Work Name</b>" + adminLoanList[i].WorkAllotName + "</td></tr></table>");
                    }
                    else {
                        $newRow.find("#chargeable").html("<table><tr><td><b>Non Sanctioned</b></td></tr><tr><td><b>Bill Type:</b>" + adminLoanList[i].BillTypeName + "'</td></tr></table>");
                    }
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

                $("#grid_filter").append("<div style='float:right'><span>Select Academy:</span><select id='ddlAcademy' onchange='ddlAcademy_onchange(this);' ><option value='0'>--Select Academy--</option></select></div>");
                //$("#grid_filter").attr("style", "text-align:right");
                bindAcademy();
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function bindAcademy() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetAcademy",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("#ddlAcademy").append($("<option></option>").val(value.AcaID).html(value.AcaName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function ddlAcademy_onchange(ddl) {
    var acaID = $(ddl).val();
    LoadBillForApprovalInfo(acaID);
}
