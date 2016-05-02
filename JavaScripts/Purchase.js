$(document).ready(function () {
    AutofillMaterialSearchBox();
    $("input[id*='btnSave']").click(function (e) {
        SaveVendor();
    });
    $("input[id*='btnadd']").click(function (e) {
        AddItemToList();
    });

    $("input[id*='btnEdit'] ").hide();

    $("input[id$='btnEdit']").click(function () {
        UpdateVendorInformation();
     });

    LoadVendorInfo();
});

function AddItemToList() {
    var matname = $("input[id*='txtMaterial']").val();
    var MatlistBox = $("[id*=lstMaterials]");
    var matval = $("input[id*='hdnmaterialid']").val();
    var option = $("<option />").val(matval).html(matname);
    MatlistBox.append(option);
    $("input[id*='txtMaterial']").val("");
    return false;
}

function AutofillMaterialSearchBox() {
    var dataSrc;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetActiveMaterials",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                $("#txtMaterial").autocomplete({
                    source: result.d,
                    focus: function (event, ui) {
                        $('#txtMaterial').val(ui.item.MatName);
                        $("input[id*='hdnmaterialid']").val(ui.item.MatId);
                        return false;
                    },
                   
                });

            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
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

function SaveVendor() {
    var params = new Object();

    var VendorInfo = new Object();

    VendorInfo.VendorName = $("input[id*='txtVendorName']").val();
    VendorInfo.VendorContactNo = $("input[id*='txtPhone']").val();
    VendorInfo.VendorAddress = $("textarea[id*='txtAddress']").val();
    VendorInfo.VendorState = $("input[id*='txtState']").val();
    VendorInfo.VendorCity = $("input[id*='txtCity']").val();
    VendorInfo.VendorZip = $("input[id*='txtZip']").val();
    VendorInfo.Active = true;
    VendorInfo.CreatedOn = "";
    VendorInfo.ModifyOn = "";
    VendorInfo.ModifyBy = "";

    var vendorMaterialRelations = new Array();
    var VendorMaterialRelation = new Object();

    $("#lstMaterials").each(function (index) {
        VendorMaterialRelation = new Object();
        VendorMaterialRelation.MatName = $(this).text();
        vendorMaterialRelations.push(VendorMaterialRelation);
    });

    VendorInfo.VendorMaterialRelationDTO = vendorMaterialRelations;


    params.vendorInfo = VendorInfo;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/AddNewVendorInformation",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                LoadVendorInfo();
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function LoadVendorInfo() {

    /*create/distroy grid for the new search*/
    if (typeof grdVendorDiscription != 'undefined') {
        grdVendorDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="vendorName">abc</td><td id="vendorAddress">abc</td><td id="contactNo">abc</td><td id="action">cc</td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/LoadVendorInformation",
        data: JSON.stringify({ VendorID: 1 }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var adminLoanList = result.d;
                if (adminLoanList.length > 0) {

                    $("#tbody").append(rowTemplate);
                }
              
                for (var i = 0; i < adminLoanList.length; i++) {
                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }
                  
                    var $newRow = $("#rowTemplate").clone();
                    $newRow.find("#vendorName").html(adminLoanList[i].VendorName);
                    $newRow.find("#vendorAddress").html("<table><tr><td><b>Vendor Address :</b> " + adminLoanList[i].VendorAddress + "</td></tr><tr><td><b>State:</b> " + adminLoanList[i].VendorState + "</td></tr><tr><td><b>City:</b> " + adminLoanList[i].VendorCity + "</td></tr><tr><td><b>Zip:</b> " + adminLoanList[i].VendorZip + "</td></tr></table>");
                    $newRow.find("#contactNo").html(adminLoanList[i].VendorContactNo);
                    $newRow.find("#action").html("<table><tr><td><a href='#' onclick='GetVendorInfoToUpdate(" + adminLoanList[i].ID + ")'>Edit</a></td></tr><tr><td><a href='#' onclick='VendorInfoToDelete(" + adminLoanList[i].ID + ")'>Delete</a></td></tr></table>");
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
                grdVendorDiscription = $('#grid').DataTable(
                {
                    "bPaginate": true,
                    "iDisplayLength": 20,
                    "sPaginationType": "full_numbers",
                    "bAutoWidth": false,
                    "bLengthChange": false,
                    "bDestroy": true

                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetVendorInfoToUpdate(vendorID) {

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetVendorInfoToUpdate",
        data: JSON.stringify({ VendorID: vendorID }),
        dataType: "json",
        success: function (responce) {

            var rdata = responce.d;

            if (rdata != undefined) {
                $("input[id*='hdnVendorID']").val(rdata.ID);
                $("input[id*='txtVendorName']").val(rdata.VendorName);
                $("input[id*='txtPhone']").val(rdata.VendorContactNo);
                $("textarea[id*='txtAddress']").val(rdata.VendorAddress);
                $("input[id*='txtState']").val(rdata.VendorState);
                $("input[id*='txtCity']").val(rdata.VendorCity);
                $("input[id*='txtZip']").val(rdata.VendorZip);

                //for (var i = 0; i < rdata.VendorMaterialRelationsDTO.length; i++) {
                       
                //}
                $("input[id*='btnSave'] ").hide();
                $("input[id*='btnEdit'] ").show();
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
        }
    });
}

function UpdateVendorInformation() {

    var updateParams = new Object();
    var VendorInfo = new Object();

    VendorInfo.ID = $("input[id*='hdnVendorID']").val();
    VendorInfo.VendorName = $("input[id*='txtVendorName']").val();
    VendorInfo.VendorContactNo = $("input[id*='txtPhone']").val();
    VendorInfo.VendorAddress = $("textarea[id*='txtAddress']").val();
    VendorInfo.VendorZip = $("input[id*='txtZip']").val();
    VendorInfo.VendorState = $("input[id*='txtState']").val();
    VendorInfo.VendorCity = $("input[id*='txtCity']").val();
    updateParams.VendorInfo = VendorInfo;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/UpdateVendorInformation",
        data: JSON.stringify(updateParams),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                LoadVendorInfo();
                alert("Record has been Upadte successfully");
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function VendorInfoToDelete(vendorID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/VendorInfoToDelete",
        data: JSON.stringify({ VendorID: vendorID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                LoadVendorInfo();
                alert("Record has been Delete successfully");
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}