
var materialname = new Array();
var SnoIds;
var MaterialList;
var selectedMaterialList = new Array();
var cnt = 1;
var delItems = 0;

$(document).ready(function () {

    AutofillMaterialSearchBox();

    $("input[id*='btnloadMaterials']").click(function (e) {
        
        LoadMaterials();
        LoadEstimateInfo(selectedMaterialList);
        return false;
       
    });
 
    $("input[id*='hdnEstimateID']").val();

    $("input[id*='hdnIsAdmin']").val();

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

    if ($("input[id*='hdnIsAdmin']").val() == 14 || $("input[id*='hdnIsAdmin']").val() == 13 || $("input[id*='hdnIsAdmin']").val() == 17 || $("input[id*='hdnIsAdmin']").val() == 15) {
        BindMaterialTypeInTransport();
    }
    else {
        BindMaterialType();
    }

    $("select[id*='ddlZone']").change(function () {
        if ($("input[id*='hdnIsAdmin']").val() == 14 || $("input[id*='hdnIsAdmin']").val() == 17 || $("input[id*='hdnIsAdmin']").val() == 15) {
            BindAcademybyZoneIDByEmpID($(this).val(), $("input[id*='hdnInchargeID']").val());
        }
        else {
            BindAcademybyZoneID($(this).val());
        }
       
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

    if ($("input[id*='hdnIsAdmin']").val() == 1 || $("input[id*='hdnIsAdmin']").val() == 13) {
        BindZone();
    }
    else {
        BindZoneByInchargeID($("input[id*='hdnInchargeID']").val())
    }
});

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
    if ($("input[id*='hdnIsAdmin']").val() == 1 || $("input[id*='hdnInchargeID']").val() == 78 || $("input[id*='hdnIsAdmin']").val() == 13) {
        Estimate.IsApproved = true;
    }
    else {
        Estimate.IsApproved = false;
    }
    Estimate.IsRejected = false;
    Estimate.IsActive = true;
    if ($("input[id*='hdnIsAdmin']").val() == 14 || $("input[id*='hdnIsAdmin']").val() == 13 || $("input[id*='hdnIsAdmin']").val() == 17 || $("input[id*='hdnIsAdmin']").val() == 15) {
        Estimate.ModuleID = 2;
    }
    else {
        Estimate.ModuleID = 1;
    }

    Estimate.EstimateAndMaterialOthersRelations = new Object();

    var estimateAndMaterialOthersRelations = new Array();

    var tablelength = $("#tbody").children('tr').length;
    var Amt = 0;
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {

        var EstimateAndMaterialOthersRelation = new Object();
        EstimateAndMaterialOthersRelation.EstId = Estimate.EstId;
        EstimateAndMaterialOthersRelation.MatId = $("#txtMatID" + i).val();
        EstimateAndMaterialOthersRelation.MatTypeId = $("#txtMatTypeID" + i).val();
        EstimateAndMaterialOthersRelation.PSId = $("#drpSourceType" + i).val();

        if ($("#txtQty" + i).val() == "" || $("#txtQty" + i).val() == "0" || $("#txtQty" + i).val() == undefined) {
            EstimateAndMaterialOthersRelation.Qty = 0;
        }
        else {
            EstimateAndMaterialOthersRelation.Qty = $("#txtQty" + i).val();
        }

        if ($("#txtRate" + i).val() == "" || $("#txtRate" + i).val() == "0" || $("#txtRate" + i).val() == undefined) {
            EstimateAndMaterialOthersRelation.Rate = 0;
        }
        else {
            EstimateAndMaterialOthersRelation.Rate = $("#txtRate" + i).val();
        }
        EstimateAndMaterialOthersRelation.Remark = $("#txtRemarks" + i).val();
        EstimateAndMaterialOthersRelation.UnitId = $("#txtUnitID" + i).val();
        EstimateAndMaterialOthersRelation.CreatedBy = $("input[id*='hdnInchargeID']").val();
        EstimateAndMaterialOthersRelation.ModifyBy = $("input[id*='hdnInchargeID']").val();
        EstimateAndMaterialOthersRelation.Active = 1;
        EstimateAndMaterialOthersRelation.IsApproved = true;
        EstimateAndMaterialOthersRelation.VendorID = 0;
        EstimateAndMaterialOthersRelation.PurchaseQty = 0;
        EstimateAndMaterialOthersRelation.PurchaseEmpID = 0;
        EstimateAndMaterialOthersRelation.DispatchStatus = 0;
        estimateAndMaterialOthersRelations.DirectPurchase = false;
        Amt += parseInt(EstimateAndMaterialOthersRelation.Qty) * parseFloat(EstimateAndMaterialOthersRelation.Rate);
        EstimateAndMaterialOthersRelation.Amount = parseInt(EstimateAndMaterialOthersRelation.Qty) * parseFloat(EstimateAndMaterialOthersRelation.Rate);
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
        var rowTemplate = '<tr id="rowTemplate"><td id="srno"></td><td id="materialname">abc</td><td id="sourcetype">abc</td><td id="qty">abc</td><td id="unit">cc</td><td id="rate">cc</td><td id="remarks">cc</td><td id="action">cc</td></tr>';

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
                 
                    for (var i = 0; i < adminLoanList.length; i++) {
                        var className = "info";
                        if (i % 2 == 0) {
                            className = "warning";
                        }
                        var $newRow = $("#rowTemplate").clone();
                        $newRow.find("#srno").html("<span id=spn" + i + ">" + (i + 1) + "</span>");
                        $newRow.find("#materialname").html("<input type='hidden' value='" + adminLoanList[i].MatID + "' id='txtMatID" + i + "' />" + adminLoanList[i].MatName);
                        $newRow.find("#sourcetype").html("<input type='hidden' value='" + adminLoanList[i].MatTypeID + "' id='txtMatTypeID" + i + "' /><select style='width:159px;' onchange='BindRateBySourceType(" + adminLoanList[i].MatTypeID + ")' id='drpSourceType" + i + "'><option value='0'>-Select Source--</option></select>");
                        $newRow.find("#qty").html("<input style='width:100px;' type='text' id='txtQty" + i + "' name='txtQty'>");
                        $newRow.find("#unit").html("<input type='hidden' value='" + adminLoanList[i].Unit.UnitId + "' id='txtUnitID" + i + "' />" + adminLoanList[i].Unit.UnitName);
                        $newRow.find("#rate").html("<input type='hidden' value='" + adminLoanList[i].MatCost + "' id='txtMatCost" + i + "' /><input type='hidden' value='" + adminLoanList[i].LocalRate + "' id='hdnLocalCost" + i + "' /><input style='width:100px;' value='0.00' type='text' id='txtRate" + i + "' name='txtRate'>");
                        $newRow.find("#remarks").html("<input style='width:260px;' type='text' id='txtRemarks" + i + "' name='txtRemarks'>");
                        $newRow.find("#action").html("<a  href='#' onclick='return MaterialRowToDelete($(this)," + adminLoanList[i].MatID + ")'>Delete</a>");
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
                        "bFilter": false,
                        "aaSorting": []
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
                for (var i = 0 ; i < length; i++) {
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
            if ($("input[id*='hdnIsAdmin']").val() == 1)
            {
                window.location.replace("Admin_EstimateView.aspx?EstId=" + estid);
            }
            else if ($("input[id*='hdnIsAdmin']").val() == 2) {
                window.location.replace("Emp_ParticularEstimateView.aspx?EstId=" + estid);
            }
            else {
                window.location.replace("Transport_ParticularEstimateView.aspx?EstId=" + estid);
            }
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
    
    $("select[id*='ddlAcademy'] option").each(function (index, option) {
        $(option).remove();
    });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetAcademybyZoneID",
        data: JSON.stringify({ ZoneID: parseInt(selctZoneID) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $("select[id*='ddlAcademy']").append($("<option></option>").val("0").html("--Select Academy--"));
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
    $("select[id*='ddlWorkAllot'] option").each(function (index, option) {
        $(option).remove();
    });

   $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetWorkAllotByAcademyID",
        data: JSON.stringify({ AcademyID: parseInt(selctAcademyID) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $("select[id*='ddlWorkAllot']").append($("<option></option>").val("0").html("--Select Work Allot--"));
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

function MaterialRowToDelete(selector, matID) {
    selector.closest('tr').remove();
    selectedMaterialList.pop(selector);
    delItems = delItems + 1;
    fixSerialNumber();
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

function fixSerialNumber() {
    var tablelength = $("#tbody").children('tr').length;
    var sno = 0;
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {
        if ($("#spn" + i).text() != "") {
            $("#spn" + i).html(parseInt(sno + 1));
            sno = sno + 1;
        }
    }
}

function TotalAmt() {
    var tablelength = $("#tbody").children('tr').length;
    var Amt = 0;
    var rate = 0;
    var qty = 0;
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {
        if ($("#drpSourceType" + i).val() == "undefined" || $("#drpSourceType" + i).val() == "0") {
            alert("Please Select the Source Type");
            return false;
        }
        if ($("#txtQty" + i).val() == "" || $("#txtQty" + i).val() == "0" || $("#txtQty" + i).val() == undefined) {
            qty = 0;
        }
        else {
            qty = $("#txtQty" + i).val();
        }

        if ($("#txtRate" + i).val() == "" || $("#txtRate" + i).val() == "0" || $("#txtRate" + i).val() == undefined) {
            rate = 0;
        }
        else {
            rate = $("#txtRate" + i).val();
        }

        Amt += parseInt(qty) * parseFloat(rate);
    }
    $("[id$='lblAmt']").html(Amt);
}

function BindRateBySourceType() {
    var matcost = 0;
    var tablelength = $("#tbody").children('tr').length;
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {
        if ($("#drpSourceType" + i).val() == "2") {
            matcost = $("#txtMatCost" + i).val();
            $("#txtRate" + i).val(matcost);
        }
        else if ($("#drpSourceType" + i).val() == "1") {
            matcost = $("#hdnLocalCost" + i).val();
            $("#txtRate" + i).val(matcost);
        }
        else {
            $("#txtRate" + i).val('0.00');
        }
    }
}


function Validation() {

    if ($("#drpMaterialType").val() == undefined || $("#drpMaterialType").val() == null) {
        alert("Please Select the Material Type");
        return false;
    }
    else if ($("#drpMaterialName").val() == undefined || $("#drpMaterialName").val() == null) {
        alert("Please Select the Material Name");
        return false;
    }


    var tablelength = $("#tbody").children('tr').length;
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {
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

function BindMaterialTypeInTransport() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetBindMaterialTypeInTransport",
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

function BindAcademybyZoneIDByEmpID(selctZoneID, inchargeId) {

    $("select[id*='ddlAcademy'] option").each(function (index, option) {
        $(option).remove();
    });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetAcademybyZoneIDByEmpID",
        data: JSON.stringify({ ZoneID: parseInt(selctZoneID), InchargeID: parseInt(inchargeId) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $("select[id*='ddlAcademy']").append($("<option></option>").val("0").html("--Select Academy--"));
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