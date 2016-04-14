
var materialname = new Array();
var tblUploadMaterial;
var SnoIds;
var MaterialList;
var selectedMaterialList = new Array();

$(document).ready(function () {
    BindVendors();
    BindEstimate();

    $("#drpEstimate").change(function (e) {
        $("#divUploadMaterial").modal('show');
        GetMaterialList($(this).val());
    });


    $("input[id*='btnLoad']").click(function (e) {
        var SnoIds = "";
        $('input:checkbox[id^="chkItem_"]:checked').each(function () {
            SnoIds = ($(this).attr("emrid"));
            selectedMaterialList.push($.grep(MaterialList, function (e) { return e.Sno == SnoIds; })[0]);
        });
        LoadPurchaseOrderInfo(selectedMaterialList);
        $("#divUploadMaterial").modal('hide');
    });

    $("input[id*='btntest']").click(function (e) {
        $("#divUploadMaterial").modal('show');
        GetMaterialList($("select[id*='drpEstimate']").val());
    });

});


function RemoveItem(sno) {
    alert(sno);
}

function IsMaterialAlreadySelected(sno) {
    var flag = false;

    if (selectedMaterialList != undefined) {
        var IsItem = $.grep(selectedMaterialList, function (e) { return e.Sno == sno; });
        if (IsItem.length > 0) {
            flag = true;
        }
    }
    return flag;
}

function BindMaterialItems(estID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetMaterialItemsByEstID",
        data: JSON.stringify({ EstID: estID }),
        dataType: "json",
        success: function (result, textStatus) {
            var item = result.d;
            if (textStatus == "success") {
                $.each(item, function (i, item) {
                    $('#ddlRejectItems').append($('<option>', {
                        value: item.MatId,
                        text: item.MatName
                    }));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function openModelPopUp(EstID, EMRID) {

    $("#divRejectItem").modal('show');
    $("input[id*='hidEstID']").val(EstID);
    $("input[id*='hidEMRID']").val(EMRID);
    $('#lblestid').html("<strong>Reject Item for Estimate No: " + EstID + "</strong>");
}

function RejectMaterialItems() {
    var emrID = $("input[id*='hidEMRID']").val();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/RejectMaterialItemByID",
        data: JSON.stringify({ EMRID: emrID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function AutofillMaterialSearchBox()
{
    var dataSrc = new Array();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseController.asmx/GetActiveMaterials",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                dataSrc = result.d;
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });

    $("#txtMaterial").autocomplete({
        source: dataSrc
    });
}

function BindVendors() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetVendorsNameList",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("#drpVendor").append($("<option></option>").val(value.ID).html(value.VendorName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindEstimate() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetEstimateNumberList",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("#drpEstimate").append($("<option></option>").val(value.EstId).html(value.EstId));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetMaterialList(selectedValue) {

    /*create/distroy grid for the new search*/
    if (typeof tblUploadMaterial != 'undefined') {
        tblUploadMaterial.fnClearTable();
    }

    var rowCount = $('#tblUploadMaterial').find("#rowTemplate1").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate1").remove();
    }
    var rowTemplate = '<tr id="rowTemplate1"><td id="materialname"></td><td id="chkmaterial"></td></tr>';
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetMaterialList",
        data: JSON.stringify({ EstimateID: parseInt(selectedValue) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                MaterialList = result.d;
                if (MaterialList.length > 0) {
                    $("#tbody1").append(rowTemplate);
                }

                for (var i = 0; i < MaterialList.length; i++) {

                    var $newRow = $("#rowTemplate1").clone();
                    $newRow.find("#materialname").html(MaterialList[i].Material.MatName);
                    if (IsMaterialAlreadySelected(MaterialList[i].Sno)) {
                        $newRow.find("#chkmaterial").html("<table><tr><td style='float: right; width :150px;'><input type='checkbox' onchange='RemoveItem(" + MaterialList[i].Sno + ")'  checked='true' id='chkItem_" + i + "' name='chkItem_" + i + "' EMRID='" + MaterialList[i].Sno + "' style' width: 16px;height: 16px;'></td></tr></table>");
                    }
                    else {
                        $newRow.find("#chkmaterial").html("<table><tr><td style='float: right; width :150px;'><input type='checkbox' id='chkItem_" + i + "' name='chkItem_" + i + "' EMRID='" + MaterialList[i].Sno + "' style' width: 16px;height: 16px;'></td></tr></table>");
                    }
                    $newRow.show();
                    if (i == 0) {
                        $("#rowTemplate1").replaceWith($newRow);
                    }
                    else {
                        $newRow.appendTo("#tblUploadMaterial > tbody");
                    }

                }
                tblUploadMaterial = $('#tblUploadMaterial').DataTable();
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function LoadPurchaseOrderInfo(selectedMaterialList) {

    /*create/distroy grid for the new search*/
    if (typeof grdTicketDiscription != 'undefined') {
        grdTicketDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="srno">abc</td><td id="qty">abc</td><td id="description">abc</td><td id="details">cc</td><td id="unitprice"></td><td id="linetotal"></td></tr>';

    var adminLoanList = selectedMaterialList;
    if (adminLoanList.length > 0) {

        $("#tbody").append(rowTemplate);
    }
    var visitorType = "";
    var count = 1;
    for (var i = 0; i < adminLoanList.length; i++) {
        var className = "info";
        if (i % 2 == 0) {
            className = "warning";
        }
        var $newRow = $("#rowTemplate").clone();
        $newRow.find("#srno").html(count);
        $newRow.find("#qty").html(adminLoanList[i].Qty);
        $newRow.find("#description").html("<table><tr><td><input type='textbox' id='txtdescriptipn' name='txtdescriptipn'></td></tr></table>");
        $newRow.find("#details").html(adminLoanList[i].Material.MatName);
        $newRow.find("#unitprice").html(adminLoanList[i].Rate);
        $newRow.find("#linetotal").html(adminLoanList[i].Qty * adminLoanList[i].Rate);
        count++;
        $newRow.addClass(className);
        $newRow.show();

        if (i == 0) {
            $("#rowTemplate").replaceWith($newRow);
        }
        else {
            $newRow.appendTo("#grid > tbody");
        }
        $("#grid").removeAttr("style");

    }

    grdTicketDiscription = $('#grid').DataTable(
        {
            "bPaginate": true,
            "iDisplayLength": 12,
            "sPaginationType": "full_numbers",
            "aaSorting": [[2, 'desc']],
            "bAutoWidth": false,
            "bLengthChange": false,
            "bDestroy": true

        });
}





