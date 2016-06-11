﻿
var materialname = new Array();
var SnoIds;
var MaterialList;
var selectedMaterialList = new Array();
var cnt = 1;

$(document).ready(function () {

    AutofillMaterialSearchBox();

    $("input[id*='btnloadMaterials']").click(function (e) {
        
        LoadMaterials();
        LoadEstimateInfo(selectedMaterialList);
        return false;
       
    });
 
    $("input[id*='hdnEstimateID']").val();

    $("#drpMaterialType").change(function () {
        BindMaterialName($(this).val());
    });

    $("#btnSubEstimate").click(function (e) {
        
        if (Page_ClientValidate("visitor")) {
            if (Validation()) {
                ClientSideClick(this);
                SaveEstimate();
            }
        }
    });

    $("#trEstimateDetail").hide();

    $("#lblWorkNameReflect").hide();

    $("#tdWorkAllot").hide();
    
    BindMaterialType();

    $("select[id*='ddlZone']").change(function () {
        BindAcademybyZoneID($(this).val());
    });

    $("select[id*='ddlAcademy']").change(function () {
        BindWorkAllotByAcademyID($(this).val());
        $("#tdWorkAllot").show();
    });

    $("select[id*='ddlWorkAllot']").change(function () {
        $("#lblWorkNameReflect").hide();
        $("#lblWorkNameReflect").html($("select[id*='ddlWorkAllot']").val())
    });

    $("#btnTotalCost").click(function (e) {
        TotalAmt();
    });

    if ($("input[id*='hdnIsAdmin']").val() == 1) {
        BindZone();
    }
    else {
        BindZoneByInchargeID($("input[id*='hdnInchargeID']").val())
    }
});

function Validation() {
    if ($("#fileUploadSignedCopy")[0].files.length == 0) {
        alert("Please Upload the File");
        return false;
    }
    else {
        var fileSize = $("#fileUploadSignedCopy")[0].files[0].size;
        if (fileSize > 1048576) {
            alert("File Size Greater than 1 mb");
            return false;
        }
    }

    var tablelength = $("#tbody").children('tr').length;
    for (var i = 0 ; i < tablelength ; i++) {
        if ($("#drpSourceType" + i).val() == "undefined" || $("#drpSourceType" + i).val() == "0") {
            alert("Please Select the Source Type");
            return false;
        }
        if ($("#txtQty" + i).val() == "" || $("#txtQty" + i).val() == "0") {
            alert("Please Enter the Qty");
            return false;
        }
    }
}

function SaveEstimate() {
    //$("#progress").dialog({ modal: true, width: 300, height: 200, title: "Progress", closeOnEscape: false });
    //$("#progress").dialog('open');

    var params = new Object();
    var Estimate = new Object();

    //Estimate.EstId = $("input[id*='hdnEstimateID']").val();
    Estimate.ZoneId = $("select[id*='ddlZone']").val();
    Estimate.AcaId = $("select[id*='ddlAcademy']").val();
    Estimate.SubEstimate = $("input[id*='txtSubEstimate']").val();
    Estimate.TypeWorkId = $("select[id*='ddlTypeOfWork']").val();
    Estimate.Active = 1;
    Estimate.WAId = $("select[id*='ddlWorkAllot']").val();
    Estimate.FileNme = $("input[id*='txtFileName']").val();
    Estimate.CreatedBy = $("input[id*='hdnInchargeID']").val();
    Estimate.ModifyBy = $("input[id*='hdnInchargeID']").val();
    Estimate.FilePath = "";
    if ($("input[id*='hdnIsAdmin']").val() == 1) {
        Estimate.IsApproved = true;
    }
    else {
        Estimate.IsApproved = false;
    }
    Estimate.IsRejected = false;
    Estimate.IsActive = true;
    Estimate.EstimateAndMaterialOthersRelations = new Object();

    var estimateAndMaterialOthersRelations = new Array();

    var tablelength = $("#tbody").children('tr').length;
    var Amt = 0;
    for (var i = 0 ; i < tablelength ; i++) {

        var EstimateAndMaterialOthersRelation = new Object();
        EstimateAndMaterialOthersRelation.EstId = Estimate.EstId;
        EstimateAndMaterialOthersRelation.MatId = $("#txtMatID" + i).val();
        EstimateAndMaterialOthersRelation.MatTypeId = $("#txtMatTypeID" + i).val();
        EstimateAndMaterialOthersRelation.PSId = $("#drpSourceType" + i).val();
      
        if ($("#txtQty" + i).val() == "" || $("#txtQty" + i).val() == "0") {
             EstimateAndMaterialOthersRelation.Qty = 0;
        }
        else {
           EstimateAndMaterialOthersRelation.Qty = $("#txtQty" + i).val();
        }

        if ($("#txtRate" + i).val() == "" || $("#txtRate" + i).val() == "0") {
            EstimateAndMaterialOthersRelation.Rate = 0;
        }
        else {
            EstimateAndMaterialOthersRelation.Rate = $("#txtRate" + i).val();
        }
        EstimateAndMaterialOthersRelation.UnitId = $("#txtUnitID" + i).val();
        EstimateAndMaterialOthersRelation.CreatedBy = $("input[id*='hdnInchargeID']").val();
        EstimateAndMaterialOthersRelation.ModifyBy = $("input[id*='hdnInchargeID']").val();
        EstimateAndMaterialOthersRelation.Active = 1;
        EstimateAndMaterialOthersRelation.IsApproved = true;
        EstimateAndMaterialOthersRelation.VendorID = 0;
        EstimateAndMaterialOthersRelation.PurchaseQty = 0;
        Amt += parseInt(EstimateAndMaterialOthersRelation.Qty) * parseInt(EstimateAndMaterialOthersRelation.Rate);
        estimateAndMaterialOthersRelations.push(EstimateAndMaterialOthersRelation);
    }
    $("#lblAmt").val(Amt);
    Estimate.EstmateCost = Amt;
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
                $("input[id*='hdnEstimateID']").val(result.d);
                SignedCopyFileUpload(result.d);
              
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

    if (selectedMaterialList.length == "") {
        alert("Please Select the Material Name");
        return false;
    }
    else {
        for (var i = 0; i < selectedMaterialList.length; i++) {
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
        var rowTemplate = '<tr id="rowTemplate"><td id="materialname">abc</td><td id="sourcetype">abc</td><td id="qty">abc</td><td id="unit">cc</td><td id="rate">cc</td><td id="action">cc</td></tr>';

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
                        $newRow.find("#sourcetype").html("<input type='hidden' value='" + adminLoanList[i].MatTypeID + "' id='txtMatTypeID" + i + "' /><select onchange='BindRateBySourceType(" + adminLoanList[i].MatTypeID + ")' id='drpSourceType" + i + "'><option value='0'>-Select Source--</option></select>");
                        $newRow.find("#qty").html("<input style='width:100px;' type='text' id='txtQty" + i + "' name='txtQty'>");
                        $newRow.find("#unit").html("<input type='hidden' value='" + adminLoanList[i].Unit.UnitId + "' id='txtUnitID" + i + "' />" + adminLoanList[i].Unit.UnitName);
                        $newRow.find("#rate").html("<input type='hidden' value='" + adminLoanList[i].MatCost + "' id='txtMatCost" + i + "' /><input style='width:100px;' value='0.00' type='text' id='txtRate" + i + "' name='txtRate'>");
                        $newRow.find("#action").html("<a  href='#' onclick='return MaterialRowToDelete($(this))'>Delete</a>");
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
                        "bDestroy": true,
                        "bFilter": false

                    });
                }
                BindPurchaseSource(adminLoanList.length);
            },
            error: function (result, textStatus) {
                alert(result.responseText);
            }
        });
        $("#trEstimateDetail").show();
    }
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

function SignedCopyFileUpload(estid) {
   var files = $("input[id*='fileUploadSignedCopy']")[0].files;

    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }

    $.ajax({
        url: "EstimateSignedCopyHandler.ashx?EstID=" + estid,
        type: "POST",
        async: false,
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            //$("#progress").dialog('close');
            alert("Estimate Create Successfuly");
            window.location.replace("Emp_ParticularEstimateView.aspx?EstId=" + estid);
        },
        error: function (err) {
            alert(err.statusText)
        }
    });
}

function BindZone()
{
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetZone",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("select[id*='ddlZone']").append($("<option></option>").val(value.ZoneId).html(value.ZoneName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });


}

function BindAcademybyZoneID(selctZoneID) {
    
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetAcademybyZoneID",
        data: JSON.stringify({ ZoneID: parseInt(selctZoneID) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("select[id*='ddlAcademy']").append($("<option></option>").val(value.AcaID).html(value.AcaName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindWorkAllotByAcademyID(selctAcademyID) {
   
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetWorkAllotByAcademyID",
        data: JSON.stringify({ AcademyID: parseInt(selctAcademyID) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("select[id*='ddlWorkAllot']").append($("<option></option>").val(value.WAId).html(value.WorkAllotName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function MaterialRowToDelete(selector) {
    selector.closest('tr').remove();
    selectedMaterialList.pop(selector);
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
                    source: result.d
                });

            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindZoneByInchargeID(inchargeId) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetZoneByInchargeID",
        data: JSON.stringify({ InchargeID: parseInt(inchargeId) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("select[id*='ddlZone']").append($("<option></option>").val(value.ZoneId).html(value.ZoneName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });


}

function TotalAmt() {

    var tablelength = $("#tbody").children('tr').length;
    var Amt = 0;
    var rate = 0;
    var qty = 0;
    for (var i = 0 ; i < tablelength ; i++) {
        if ($("#drpSourceType" + i).val() == "undefined" || $("#drpSourceType" + i).val() == "0") {
            alert("Please Select the Source Type");
            return false;
        }
        if ($("#txtQty" + i).val() == "" || $("#txtQty" + i).val() == "0") {
            alert("Please Enter the Qty");
            return false;
        }
        else {
            qty = $("#txtQty" + i).val();
        }

        if ($("#txtRate" + i).val() == "" || $("#txtRate" + i).val() == "0") {
            rate = 0;
        }
        else {
            rate = $("#txtRate" + i).val();
        }

        Amt += parseInt(qty) * parseInt(rate);
    }
    $("[id$='lblAmt']").html(Amt);
}

function BindRateBySourceType()
{
    var matcost = 0;
    var tablelength = $("#tbody").children('tr').length;
    for (var i = 0 ; i < tablelength ; i++) {
        if ($("#drpSourceType" + i).val() == "2") {
          matcost =  $("#txtMatCost" + i).val();
          $("#txtRate" + i).val(matcost);
        }
        else {
            $("#txtRate" + i).val('0.00');
        }
    }
}


function Validation() {
    var tablelength = $("#tbody").children('tr').length;
    for (var i = 0 ; i < tablelength ; i++) {
        if ($("#drpSourceType" + i).val() == "undefined" || $("#drpSourceType" + i).val() == "0") {
            alert("Please Select the Source Type");
            return false;
        }
        else if ($("#txtQty" + i).val() == "" || $("#txtQty" + i).val() == "0") {
            alert("Please Enter the Qty");
            return false;
        }
    }
    return true;
}