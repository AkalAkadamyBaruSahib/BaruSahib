
var materialname = new Array();
var SnoIds;
var MaterialList;
var selectedMaterialList = new Array();

$(document).ready(function () {
    $("input[id*='btnloadMaterials']").click(function (e) {
        LoadMaterials();
        LoadEstimateInfo(selectedMaterialList);
        $("#trEstimateDetail").show();
        return false;
    });

    $("#drpMaterialType").change(function () {
        BindMaterialName($(this).val());
    });

    $("input[id*='btnSubEstimate']").click(function (e) {
        SaveEstimate();
    });

    $("#trEstimateDetail").hide();

    BindMaterialType();
});

function SaveEstimate() {
    var params = new Object();
    var Estimate = new Object();


    Estimate.ZoneId = $("select[id*='ddlZone']").val();
    Estimate.AcaId = $("select[id*='ddlAcademy']").val();
    Estimate.SubEstimate = $("input[id*='txtSubEstimate']").val();
    Estimate.TypeWorkId = $("select[id*='ddlTypeOfWork']").val();
    Estimate.Active = 1;
    Estimate.WAId = $("select[id*='ddlWorkAllot']").val();
    Estimate.FileNme = $("input[id*='txtFileName']").val();
    var fileUploadSignedCopy = $("#fileUploadSignedCopy")[0].files;

    if ($("#fileUploadSignedCopy")[0].files != undefined) {
        Estimate.FilePath = "";
        for (var i = 0; i < fileUploadSignedCopy.length; i++) {

            Estimate.FilePath += "EstFile/" + $("#fileUploadSignedCopy")[0].files[i].name + ",";
        }
        Estimate.FilePath = Estimate.FilePath.substr(0, Estimate.FilePath.length - 1);
    }

    Estimate.IsApproved = true;
    Estimate.IsRejected = false;
    Estimate.IsActive = true;
    Estimate.EstimateAndMaterialOthersRelations = new Object();

    var estimateAndMaterialOthersRelations = new Array();

    var tablelength = $("#tbody").children('tr').length;

    for (var i = 0 ; tablelength ; i++) {

        var EstimateAndMaterialOthersRelation = new Object();
        EstimateAndMaterialOthersRelation.EstId = Estimate.EstId;
        EstimateAndMaterialOthersRelation.MatId = $("#txtMatID" + i).val();
        EstimateAndMaterialOthersRelation.MatTypeId = $("#txtMatTypeID").val();
        EstimateAndMaterialOthersRelation.PSId = $("#drpSourceType" + i).val();
        EstimateAndMaterialOthersRelation.Qty = $("#txtQty" + i).val();
        EstimateAndMaterialOthersRelation.UnitId = $("#txtUnitID" + i).val();
        EstimateAndMaterialOthersRelation.Active = 1;
        EstimateAndMaterialOthersRelation.IsApproved = true;
        EstimateAndMaterialOthersRelation.VendorID = 0;
        EstimateAndMaterialOthersRelation.PurchaseQty = 0;
        estimateAndMaterialOthersRelations.push(EstimateAndMaterialOthersRelation);
    }

    Estimate.EstimateAndMaterialOthersRelations = estimateAndMaterialOthersRelations;
    params.estimate = Estimate;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/SaveEstimateDetail",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                SignedCopyFileUpload();
                alert("Estimate Create Successfully");
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function LoadMaterials() {
 $("#drpMaterialName option:selected").each(function (i, option) {
        selectedMaterialList.push($(this).val());
        
    });
}

function LoadEstimateInfo(selectedMaterialList) {

    var materialIDs = "";
    for (var i = 0; i < selectedMaterialList.length; i++)
    {
        materialIDs += "," + selectedMaterialList[i];
    }

    materialIDs = materialIDs.substr(1, materialIDs.length);

    /*create/distroy grid for the new search*/
    if (typeof grdEstimateDiscription != 'undefined') {
        grdEstimateDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="materialname">abc</td><td id="sourcetype">abc</td><td id="qty">abc</td><td id="unit">cc</td><td id="action">cc</td></tr>';

    var adminLoanList = 0;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetMaterialsByID",
        data: JSON.stringify({ materialList: materialIDs }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                adminLoanList = result.d;
                if (adminLoanList.length > 0) {

                    $("#tbody").append(rowTemplate);
                }
                var EmployeeType = "";
                for (var i = 0; i < adminLoanList.length; i++) {
                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }
                    var $newRow = $("#rowTemplate").clone();
                    $newRow.find("#materialname").html("<input type='hidden' value='" + adminLoanList[i].MatID + "' id='txtMatID" + i + "' />" + adminLoanList[i].MatName);
                    $newRow.find("#sourcetype").html("<input type='hidden' value='" + adminLoanList[i].MatTypeId + "' id='txtMatTypeID" + i + "' /><select id='drpSourceType" + i + "'><option value='0'>-Select Source--</option></select>");
                    $newRow.find("#qty").html("<input type='text' id='txtQty" + i + "' name='txtQty'>");
                    $newRow.find("#unit").html("<input type='hidden' value='" + adminLoanList[i].Unit.UnitId + "' id='txtUnitID" + i + "' />" + adminLoanList[i].Unit.UnitName);
                    $newRow.find("#action").html("<a href='#' onclick='MaterialItemToDelete(" + adminLoanList[i].MatID + ")'>Delete</a>");
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
                grdEstimateDiscription = $('#grid').DataTable(
                {
                    "bPaginate": false,
                    "iDisplayLength": 20,
                    "sPaginationType": "full_numbers",
                    "bAutoWidth": false,
                    "bLengthChange": false,
                    "bDestroy": true,
                    "bSearchable": false

                });
            }
            BindPurchaseSource(adminLoanList.length);
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });

   
}



function BindPurchaseSource(length) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetPurchaseSource",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                for (var i = 0 ; i < length; i++)
                {
                    $.each(Result, function (key, value) {
                        $("#drpSourceType" + i).append($("<option></option>").val(value.PSId).html(value.PSName));
                    });
                }
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindMaterialType() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetBindMaterialType",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("#drpMaterialType").append($("<option></option>").val(value.MatTypeId).html(value.MatTypeName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindMaterialName(selctMaterialType) {

    $('#drpMaterialName option').each(function (index, option) {
        $(option).remove();
    });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetBindMaterialNameByMaterialType",
        data: JSON.stringify({ MatTypeID: parseInt(selctMaterialType) }),
        dataType: "json",
        success: function (result, textStatus) {

            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("#drpMaterialName").append($("<option></option>").val(value.MatId).html(value.MatName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function SignedCopyFileUpload() {

    var files = $("#fileUploadSignedCopy")[0].files;

    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }

    $.ajax({
        url: "EstimateSignedCopyHandler.ashx",
        type: "POST",
        async: false,
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            //alert(result);
        },
        error: function (err) {
            alert(err.statusText)
        }
    });
}

function MaterialItemToDelete(matID) {

}
