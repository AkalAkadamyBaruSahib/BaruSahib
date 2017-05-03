var VendorObjectList;
var MaterialList = new Array();
var SnoIds;
var MaterialList;
var selectedMaterialList = new Array();
var delItems = 0;
var cntM = 1;
var SourceTypeList;
var materialType;
var MaterialTypeIntransport;
var NonWorkshopMaterialList = Array();
var WorkshopMaterialList = Array();
var MaterialObject;


$(document).ready(function () {

    $("[id*=gvAddItems2] [id*=chkVat]").click(function () {

        var row = $(this).closest("tr");

        var qty = row.find("input[id*='txtQty']").val();

        var rate = row.find("input[id*='txtRateSan']").val();

        var vat = row.find("input[id*='txtVat']").val();

        if ($(this)[0].checked) {
            row.find("input[id*='txtVat']").prop('disabled', true);
            row.find("input[id*='txtVat']").val("");
            var am = parseFloat(qty) * parseFloat(rate);
            roundAmt = am.toFixed(2);
            row.find("[id*='lblAmtSan']").text(roundAmt);
        }
        else {
            row.find("input[id*='txtVat']").prop('disabled', false);

        }
        TotalAmount();

    });

    $("[id*=gvAddItems2] [id*=txtRateSan]").change(function () {

        var row = $(this).closest("tr");

        var qty = row.find("input[id*='txtQty']").val();

        var rate = row.find("input[id*='txtRateSan']").val();

        var vat = row.find("input[id*='txtVat']").val();

        var chkvat = row.find("input[id*='chkVat']").is(":checked");

        var estRate = row.find("[id*='lblEstRate']").text();

        var amount = row.find("[id*='lblAmtSan']").text();

        var roundAmt = 0;

        var totalAmt = 0;

        var regex = new RegExp(/^\+?[0-9(),.-]+$/);

        if (rate == "" || !rate.match(regex) || rate == "0") {
            row.find("input[id*='txtRateSan']").css('border-color', 'red');
            row.find("input[id*='txtRateSan']").val("");
            row.find("#errMsg").show();
            return false;
        }
        else if (qty == "" || qty == "0") {
            row.find("input[id*='txtQty']").css('border-color', 'red');
            return false;
        }
        else {
            if (chkvat == false && vat == "") {
                alert('Please enter the Vat or check the checkBox.');
                return false;
            }
            else {
                row.find("input[id*='txtRateSan']").css('border-color', '');
                row.find("input[id*='txtQty']").css('border-color', '');
                if (chkvat == true) {
                    var am = parseFloat(qty) * parseFloat(rate);
                    roundAmt = am.toFixed(2);
                    row.find("[id*='lblAmtSan']").text(roundAmt);

                    if (parseFloat(rate) > parseFloat(estRate)) {
                        row.find("input[id*='btnSave']").prop('disabled', true);
                        row.find("input[id*='txtRateSan']").css('border-color', 'red');
                        row.find("#spnRate").show();
                        row.find("input[id*='txtRateSan']").val("");
                        row.find("[id*='lblAmtSan']").text("");
                        $("input[id*='hdnAmtSan']").val("");
                        return false;
                    }
                    else {
                        row.find("input[id*='txtRateSan']").css('border-color', '');
                        row.find("input[id*='btnSave']").prop('disabled', false);
                        row.find("#spnRate").hide();
                        row.find("#errMsg").hide();
                    }
                }
                else {
                    var vatAmount = ((parseFloat(qty) * parseFloat(rate)) * parseFloat(vat)) / 100;
                    vatAmount = (parseFloat(qty) * parseFloat(rate)) + parseFloat(vatAmount);
                    var totlAmt = (parseFloat(rate) * parseFloat(vat)) / 100;
                    totlAmt = parseFloat(rate) + parseFloat(totlAmt);
                    roundAmt = vatAmount.toFixed(2);
                    row.find("[id*='lblAmtSan']").text(roundAmt);

                    if (parseFloat(totlAmt) > parseFloat(estRate)) {
                        row.find("input[id*='btnSave']").prop('disabled', true);
                        row.find("input[id*='txtRateSan']").css('border-color', 'red');
                        row.find("#spnRate").show();
                        row.find("input[id*='txtRateSan']").val("");
                        row.find("[id*='lblAmtSan']").text("");
                        $("input[id*='hdnAmtSan']").val("");
                        row.find("#spnRate").hide();
                        row.find("#errMsg").hide();
                        return false;
                    }
                    else {
                        row.find("input[id*='txtRateSan']").css('border-color', '');

                        row.find("input[id*='btnSave']").prop('disabled', false);
                    }
                }
                TotalAmount();
            }
        }
    });

    $("[id*=gvAddItems2] [id*=txtQty]").change(function () {

        var row = $(this).closest("tr");

        var qty = row.find("input[id*='txtQty']").val();

        var regex = new RegExp(/^\+?[0-9(),.-]+$/);

        if (qty == "" || !qty.match(regex) || qty == "0") {
            row.find("input[id*='txtQty']").css('border-color', 'red');
            row.find("input[id*='txtQty']").val("");
            row.find("#errMsgQty").show();
            return false;
        }
        else {
            row.find("input[id*='txtQty']").css('border-color', '');
            var estQty = row.find("[id*='lblEstQty']").text();
            var balQty = row.find("[id*='lblBalQty']").text();

            if (parseFloat(qty) > parseFloat(balQty)) {
                row.find("input[id*='btnSave']").prop('disabled', true);
                row.find("input[id*='txtQty']").css('border-color', 'red');
                row.find("input[id*='txtQty']").val("");
                row.find("#spnQty").show();
                return false;
            }
            else {
                row.find("#spnQty").hide();
                row.find("#errMsgQty").hide();
                row.find("input[id*='txtQty']").css('border-color', '');
                row.find("input[id*='btnSave']").prop('disabled', false);
            }
        }
    });

    $("[id*=gvAddItems2] [id*=txtVat]").change(function () {

        var row = $(this).closest("tr");

        var qty = row.find("input[id*='txtQty']").val();

        var rate = row.find("input[id*='txtRateSan']").val();

        var vat = row.find("input[id*='txtVat']").val();

        var chkvat = row.find("input[id*='chkVat']").is(":checked");

        var estRate = row.find("[id*='lblEstRate']").text();

        if (chkvat == true) {

            row.find("input[id*='txtVat']").prop('disabled', true);

            if (rate != "") {

                var am = parseFloat(qty) * parseFloat(rate);

                row.find("[id*='lblAmtSan']").text(am);


                if (rate > estRate) {

                    row.find("input[id*='btnSave']").prop('disabled', true);

                    row.find("input[id*='txtVat']").css('border-color', 'red');

                    row.find("input[id*='txtVat']").val("");

                    row.find("[id*='lblAmtSan']").text("");

                    row.find("#spnVat").show();

                    return false;
                }

                else {
                    row.find("#spnVat").hide();
                    row.find("input[id*='txtVat']").css('border-color', '');
                    row.find("input[id*='btnSave']").prop('disabled', false);

                }
            }
        }
        else {
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);
            if (vat < 0 || !vat.match(regex)) {
                row.find("input[id*='txtVat']").css('border-color', 'red');
                row.find("input[id*='txtVat']").val("");
                return false;
            }
            else {
                row.find("input[id*='txtVat']").prop('disabled', false);
                if (rate != "") {

                    var vatAmount = ((parseFloat(qty) * parseFloat(rate)) * parseFloat(vat)) / 100;

                    vatAmount = (parseFloat(qty) * parseFloat(rate)) + parseFloat(vatAmount);

                    row.find("[id*='lblAmtSan']").text(vatAmount);

                    var totlAmt = (parseFloat(rate) * parseFloat(vat)) / 100;

                    totlAmt = parseFloat(rate) + parseFloat(totlAmt);

                    if (totlAmt > estRate) {

                        row.find("input[id*='btnSave']").prop('disabled', true);

                        row.find("input[id*='txtVat']").css('border-color', 'red');

                        row.find("input[id*='txtVat']").val("");

                        row.find("#spnVat").show();

                        row.find("input[id*='lblAmtSan']").text("");

                        return false;
                    }
                    else {
                        row.find("#spnVat").hide();
                        row.find("input[id*='txtVat']").css('border-color', '');
                        row.find("input[id*='btnSave']").prop('disabled', false);

                    }
                }
            }
            TotalAmount();
        }
    });

    $("#btnEstimateCost").click(function (e) {
        TotalEstimateAmt();
    });

    if ($("select[id*='ddlBillType1']").val() == 2) {
        AutofillMaterialSearchBox(0);
       // GetMaterialObjectList();
        GetPurchaseSource();
        GetVendors();
        $("#aDeleteRow0").hide();
        $("#btnSubmitApprovel").show();
    }

    $("#btnSubmitApprovel").click(function (e) {
        if ($("select[id*='ddlBillType1']").val() == 2) {
            if (Page_ClientValidate("civilBill")) {
                if (Validation()) {
                    if (ValidationAmount()) {
                        SaveNonSanctionData();
                    }
                }
            }
        }
    });

    $("input[id*='btnSubmit']").click(function (e) {
        if ($("#txtAgencyName").val() != undefined) {
            var vendorname = $("#txtAgencyName").val();
            var ValidateMaterial = $.grep(VendorObjectList, function (e) { return e.VendorName == vendorname })[0];
            if (ValidateMaterial == undefined) {
                alert("Vendor not Exit.Please create a new vendor.");
                $("#txtAgencyName").val("");
                return false;
            }
        }
    });
  
      $("input[id*=btnShowData]").click(function () {
          var chks = $("[id*=GridView1] [id*=chkCtrl]").is(":checked");
          if (!chks) {
              alert("Please select at least one Material..!");
              return false;
          }
    });



});
function TotalAmount() {
    var tablelength = $("[id*=gvAddItems2] tr").length;
    var totalAmt = 0;
    for (var i = 0 ; i < (tablelength) ; i++) {
        var Amt = $("[id*='lblAmtSan_" + i + "']").text();
        Amt = Amt.replace("₹", "");
        Amt = Amt.replace(",", "");
        if (Amt != "") {
            totalAmt = parseFloat(totalAmt) + parseFloat(Amt);
        }
    }
    var roundAmt = totalAmt.toFixed(2);
    $("[id*='lblTotalEstimateCost']").text((roundAmt));
    return false;
}

function AutofillVendorInfoSearchBox() {
    var dataSrc;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetActiveVendorForAutoFill",
        dataType: "json",
        success: function (result, textStatus) {
                    $("#txtAgencyName").autocomplete({
                    source: result.d,
                    appendTo: '#menu',
                });

            $("#txtAgencyName").on('autocompletechange change', function () {
                    var Vname = $("#txtAgencyName").val();
                    var selectedMaterial = $.grep(VendorObjectList, function (e) { return e.VendorName == Vname })[0];
                    if (selectedMaterial != undefined) {
                        $("input[id*='hdnVandorID']").val(selectedMaterial.ID);
                    }
                }).change();
       },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetVendorObject() {
    if (VendorObjectList == undefined || VendorObjectList.length == 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Services/PurchaseControler.asmx/GetActiveVendorObjectForAutoFill",
            dataType: "json",
            async: false,
            success: function (result, textStatus) {
                VendorObjectList = result.d;
            },
            error: function (result, textStatus) {
                alert(result.responseText);
            }
        })
    }
}

function GetVendors() {
    $.when(GetVendorObject()).done
    (AutofillVendorInfoSearchBox());
}

function GetPurchaseSource() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetPurchaseSource",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                SourceTypeList = result.d;
                $.each(SourceTypeList, function (key, value) {
                    $("#ddlSourceType0").append($("<option></option>").val(value.PSId).html(value.PSName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindPurchaseSource(cntID) {
    $.each(SourceTypeList, function (key, value) {
        $("#ddlSourceType" + cntID).append($("<option></option>").val(value.PSId).html(value.PSName));
    });
}

// Step1 :- Get All Active Materials and Create MaterialObjectList Object
//function GetMaterialObjectList() {
//   // $("#progress").modal('show');
//    if (MaterialObject == undefined || MaterialObject.length == 0) {
//        $.ajax({
//            type: "POST",
//            contentType: "application/json; charset=utf-8",
//            url: "Services/PurchaseControler.asmx/GetActiveMaterials",
//            dataType: "json",
//            async: false,
//            success: function (result, textStatus) {
//                if (result.d != undefined && result.d.length > 0) {
//                    MaterialObject = result.d;
//                    if (MaterialObject != undefined && MaterialObject.length > 0) {
//                        GetMaterialsForAutoFill();
                        
//                    }
//                }
//            },
//            error: function (result, textStatus) {
//                alert(result.responseText);
//            }
//        })
//    }
//    //else {
//    //    $("#progress").dialog('close');
//    //}
//}

// Step2 :- Get Workshop and Non Workshop Material Objects
//function GetMaterialsForAutoFill() {

//    var tempArray = $.grep(MaterialObject, function (e) { return e.MatTypeID != 83 }); //83 Live // 75 Local

//    $.each(tempArray, function (index, value) {
//        NonWorkshopMaterialList.push(value.MatName);
//    });

//    // Workshop Material Object
//    tempArray = $.grep(MaterialObject, function (e) { return e.MatTypeID == 83 }); //83 Live // 75 Local

//    $.each(tempArray, function (index, value) {
//        WorkshopMaterialList.push(value.MatName);
//    });
//  //  $("#progress").modal('hide');
//}

function Qty_ChangeEvent(cntID) {
    var rate = $("#txtRate" + cntID).val();
    var qty = $("#txtQty" + cntID).val();
    $("#txtTotal" + cntID).text(rate * qty);
    TotalEstimateAmt();
}

function Rate_ChangeEvent(cntID) {
    var rate = $("#txtRate" + cntID).val();
    var qty = $("#txtQty" + cntID).val();
    $("#txtTotal" + cntID).text(rate * qty);
    TotalEstimateAmt();
}
function SourceType_ChangeEvent(cntID) {
    ClearData(cntID);
}

function MaterialTextBox_ChangeEvent(cntID) {
    var Matname = $("#txtMaterialName" + cntID).val();
    var selectedMaterial = GetSelectedMaterial(Matname);
    if (selectedMaterial.length > 0) {
        $("#hdnMatID" + cntID).val(selectedMaterial[0].MatID);
        $("#lblUnit" + cntID).text(selectedMaterial[0].Unit.UnitName);
        $("#hdnUnitID" + cntID).val(selectedMaterial[0].Unit.UnitId);
        $("#spnMaterialTypeID" + cntID).text(selectedMaterial[0].MaterialType.MatTypeName);
        $("#hdnMatTypeID" + cntID).val(selectedMaterial[0].MaterialType.MatTypeId);
        $("#hdnMaterialTypeName" + cntID).val(selectedMaterial[0].MaterialType.MatTypeName);

        var drpSourceType = $("#ddlSourceType" + cntID).val();
        if (selectedMaterial != undefined) {
            if (drpSourceType == "2") {
                $("#txtRate" + cntID).val(selectedMaterial[0].MatCost);
            }
            else if (drpSourceType == "1") {
                $("#txtRate" + cntID).val(selectedMaterial[0].LocalRate);
            }
            else if (drpSourceType == "3") {
                $("#txtRate" + cntID).val(selectedMaterial[0].AkalWorkshopRate);
            }
            else {
                $("#txtRate" + cntID).val('0.00');
            }
        }
    }
}

function fixSerialNumber() {
    var tablelength = $("#tbody2").children('tr').length;
    var sno = 0;
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {
        if ($("#spn" + i).text() != "") {
            $("#spn" + i).html(parseInt(sno + 1));
            sno = sno + 1;
        }
    }
}

function removeRow(removeNum) {
    $('#tr' + removeNum).remove();
    delItems = delItems + 1;
    fixSerialNumber();
    return false;
    cntM--;
}

function ClearData(cntID) {
    $("#txtMaterialName" + cntID).val("");
    $("#txtRate" + cntID).val("");
    $("#lblUnit" + cntID).text("");
    $("#txtTotal" + cntID).val("");
    $("#txtQty" + cntID).val("");
    $("#spnMaterialTypeID" + cntID).text("");
}

function BindRateBySourceType() {
    var matcost = 0;
    var tablelength = $("#tbody2").children('tr').length;
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {
        if ($("#drpSourceType" + i).val() == "2") {
            matcost = $("#txtMatCost" + i).val();
            $("#txtRate" + i).val(matcost);
        }
        else if ($("#drpSourceType" + i).val() == "1") {
            matcost = $("#hdnLocalCost" + i).val();
            $("#txtRate" + i).val(matcost);
        }
        else if ($("#drpSourceType" + i).val() == "3") {
            matcost = $("#hdnAkalWorkshopCost" + i).val();
            $("#txtRate" + i).val(matcost);
        }
        else {
            $("#txtRate" + i).val('0.00');
        }
    }
}

function AddMaterialRow() {

    if (Validation()) {
        $('#tblEstimateMatDetail tr').last().after('<tr id="tr' + cntM + '"><td><span id="spn' + cntM + '">' + (cntM + 1) + '</span></td>' +
            '<td> <select id="ddlSourceType' + cntM + '" name="ddlSourceType' + cntM + '" style="width:150px;" onchange="SourceType_ChangeEvent(' + cntM + ');" ><option value="0">Select Source Type</option></select></td>' +
           '<td><input id="txtMaterialName' + cntM + '" onblur="MaterialTextBox_ChangeEvent(' + cntM + ');" name="txtMaterialName' + cntM + '" value="" type="text" style="width:200px;" /><br/><div id="menu-container' + cntM + '" style="position:absolute; width:500px;"></div></td>' +
            '<td> <span id="spnMaterialTypeID' + cntM + '" style="width:150px;"></td>' +
            '<td><input id="txtQty' + cntM + '" name="txtQty' + cntM + '" value="" type="text" style="width:80px;" onchange="Qty_ChangeEvent(' + cntM + ');" /></td>' +
            '<td>  <label id="lblUnit' + cntM + '" name="lblUnit' + cntM + '" ></label></td>' +
            '<td> <input id="txtRate' + cntM + '" name="txtRate' + cntM + '" value="" type="text" style="width:80px;" onchange="Rate_ChangeEvent(' + cntM + ');" /></td>' +
            '<td><span id="txtTotal' + cntM + '" name="txtTotal' + cntM + '" class="span6 typeahead" /></td>' +
            '<td><a href="javascript:void(0);" id="aAddNewRow' + cntM + '" onclick="AddMaterialRow();"><b>Add Row</b></a> <a href="javascript:void(0);" id="aDeleteRow' + cntM + '" onclick="removeRow(' + cntM + ');"><b>Delete</b></a><input type="hidden" id="hdnMatID' + cntM + '" /><input type="hidden" id="hdnMatTypeID' + cntM + '" /><input type="hidden" id="hdnUnitID' + cntM + '" /><input type="hidden" id="hdnMaterialTypeName' + cntM + '" /></td></tr>');

        AutofillMaterialSearchBox(cntM);
        BindPurchaseSource(cntM);
        fixSerialNumber();

        $("#aDeleteRow" + cntM).hide();

        if (cntM > 0) {
            var cntR = cntM - 1;
            $("#aAddNewRow" + cntR).hide();
            $("#aDeleteRow" + cntR).show();
            $("#aAddNewRow0").hide();
            $("#aDeleteRow0").show();
        }

        cntM++;
    }
}

function Validation() {
    TotalEstimateAmt();
    if ($("#txtAgencyName").val() != undefined) {
        var vendorname = $("#txtAgencyName").val();
        var ValidateMaterial = $.grep(VendorObjectList, function (e) { return e.VendorName == vendorname })[0];
        if (ValidateMaterial == undefined) {
            alert("Vendor not Exit.Please create a new vendor.");
            $("#txtAgencyName").val("");
            return false;
        }
    }

    var tablelength = $("#tbody2").children('tr').length;
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {

        if ($("#ddlSourceType" + i).val() == "undefined" || $("#ddlSourceType" + i).val() == "0") {
            $("#ddlSourceType" + i).css('border-color', 'red');
            return false;
        }
        else {
            $("#ddlSourceType" + i).css('border-color', '');
        }

        if ($("#txtMaterialName" + i).val() == "" || $("#txtMaterialName" + i).val() == "0") {
            $("#txtMaterialName" + i).css('border-color', 'red');
            return false;
        }
        else {
            if ($("#txtMaterialName" + i).val() != undefined) {
                var Matname = $("#txtMaterialName" + i).val();
                var ValidateMaterial = GetSelectedMaterial(Matname);
                if (ValidateMaterial.length == 0) {
                    $("#txtMaterialName" + i).css('border-color', 'red');
                    $("#txtMaterialName" + i).val("");
                    $("#hdnMatID" + i).val("");
                    return false;
                }
                else {
                    $("#txtMaterialName" + i).css('border-color', '');
                }
            }
        }

        if ($("#txtQty" + i).val() != undefined) {
            var value = $("#txtQty" + i).val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);

            if ($("#txtQty" + i).val() == "" || $("#txtQty" + i).val() == "0" || !value.match(regex)) {
                $("#txtQty" + i).css('border-color', 'red');
                return false;
            }
            else {
                $("#txtQty" + i).css('border-color', '');
            }
        }
        if ($("#txtRate" + i).val() != undefined) {
            var value = $("#txtRate" + i).val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);

            if ($("#txtRate" + i).val() == "" || $("#txtRate" + i).val() == "0" || !value.match(regex)) {
                $("#txtRate" + i).css('border-color', 'red');
                return false;
            }
            else {
                $("#txtRate" + i).css('border-color', '');
            }
        }

        $("#txtTotal" + i).text(($("#txtRate" + i).val()) * ($("#txtQty" + i).val()));

    }
  
    return true;
}

function TotalEstimateAmt() {
    //BillSumitRateCondition();
    var tablelength = $("#tbody2").children('tr').length;
    var Amt = 0;
    var rate = 0;
    var qty = 0;
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {
        if ($("#txtQty" + i).val() == "" || $("#txtQty" + i).val() == undefined) {
            qty = 0;
        }
        else {
            qty = $("#txtQty" + i).val();
        }

        if ($("#txtRate" + i).val() == "" || $("#txtRate" + i).val() == undefined) {
            rate = 0;
        }
        else {
            rate = $("#txtRate" + i).val();
        }

        Amt += parseFloat(qty) * parseFloat(rate);
    }
    var RoundAmt = Amt.toFixed(2);
    $("[id$='lblAmt']").html(RoundAmt);
    $("input[id*='hdnAmount']").val(RoundAmt);

}

function SaveNonSanctionData() {

    var params = new Object();
    var SubmitBillByUser = new Object();
    if ($("input[id*='hdnUpdateBillID']").val() != "") {
        SubmitBillByUser.SubBillId = $("input[id*='hdnUpdateBillID']").val();
    }
    SubmitBillByUser.ZoneId = $("input[id*='hdnZoneID']").val();
    SubmitBillByUser.AcaId = $("input[id*='hdnAcaID']").val();
    SubmitBillByUser.ChargetoBillTyId =1;
    SubmitBillByUser.BillDate = $("input[id*='txtBillDate']").val();
    SubmitBillByUser.GateEntryNo = $("input[id*='txtGateEntryNo']").val();
    SubmitBillByUser.VendorID = $("input[id*='hdnVandorID']").val();
    SubmitBillByUser.AgencyName = $("input[id*='txtAgencyName']").val();
    SubmitBillByUser.CreatedBy = $("input[id*='hdnInchargeID']").val();
    SubmitBillByUser.ModifyBy = $("input[id*='hdnInchargeID']").val();
    SubmitBillByUser.Active = 1;
    SubmitBillByUser.BillType = $("select[id*='ddlBillType1']").val();
    SubmitBillByUser.VendorBillNumber = $("input[id*='txtAgenyBillNo']").val();
    SubmitBillByUser.VendorBillPath = "";
    SubmitBillByUser.WAId = "";

    SubmitBillByUser.SubmitBillByUserAndMaterialOthersRelation = new Object();

    var submitBillByUserAndMaterialOthersRelation = new Array();

    var tablelength = $("#tbody2").children('tr').length;
    var Amt = 0;
   
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {
        if ($("#hdnMatID" + i).val() != undefined) {
            var SubmitBillByUserAndMaterialOthersRelation = new Object();
            SubmitBillByUserAndMaterialOthersRelation.SubBillId = SubmitBillByUser.SubBillId;
            SubmitBillByUserAndMaterialOthersRelation.MatId = $("#hdnMatID" + i).val();
            SubmitBillByUserAndMaterialOthersRelation.MatTypeId = $("#hdnMatTypeID" + i).val();

            if ($("#txtQty" + i).val() == "" || $("#txtQty" + i).val() == undefined) {
                SubmitBillByUserAndMaterialOthersRelation.Qty = 0;
            }
            else {
                SubmitBillByUserAndMaterialOthersRelation.Qty = $("#txtQty" + i).val();
            }

            if ($("#txtRate" + i).val() == "" || $("#txtRate" + i).val() == undefined) {
                SubmitBillByUserAndMaterialOthersRelation.Rate = 0;
            }
            else {
                SubmitBillByUserAndMaterialOthersRelation.Rate = $("#txtRate" + i).val();
            }
            SubmitBillByUserAndMaterialOthersRelation.UnitName = $("#lblUnit" + i).text();
            SubmitBillByUserAndMaterialOthersRelation.ItemName = $("#txtMaterialName" + i).val();
            SubmitBillByUserAndMaterialOthersRelation.Remark = "";
            SubmitBillByUserAndMaterialOthersRelation.UnitId = $("#hdnUnitID" + i).val();
            SubmitBillByUserAndMaterialOthersRelation.CreatedBy = $("input[id*='hdnInchargeID']").val();
            SubmitBillByUserAndMaterialOthersRelation.ModifyBy = $("input[id*='hdnInchargeID']").val();
            SubmitBillByUserAndMaterialOthersRelation.Active = 1;
            SubmitBillByUserAndMaterialOthersRelation.Vat = 0;
            Amt += parseFloat(SubmitBillByUserAndMaterialOthersRelation.Qty) * parseFloat(SubmitBillByUserAndMaterialOthersRelation.Rate);
            SubmitBillByUserAndMaterialOthersRelation.Amount = parseFloat(SubmitBillByUserAndMaterialOthersRelation.Qty) * parseFloat(SubmitBillByUserAndMaterialOthersRelation.Rate);
            submitBillByUserAndMaterialOthersRelation.push(SubmitBillByUserAndMaterialOthersRelation);
        }
    }
    $("#lblAmt").val(Amt);
    SubmitBillByUser.TotalAmount = Amt;
    SubmitBillByUser.SubmitBillByUserAndMaterialOthersRelation = submitBillByUserAndMaterialOthersRelation;
    params.SubmitBillByUser = SubmitBillByUser;

    var agencyName = SubmitBillByUser.AgencyName;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/ConstructionUserController.asmx/SaveCivilBillDetail",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                $("input[id*='hdnSubBillID']").val(result.d);
                AgencyBillCopyUpload(result.d, agencyName);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function AgencyBillCopyUpload(billid,agencyName) {
    var files = $("input[id*='fileAgencyBill']")[0].files;

    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }

    $.ajax({
        url: "NonSactionBillCopyUploadHandler.ashx?BillID=" + billid + "&agencyname=" + agencyName,
        type: "POST",
        async: false,
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            alert("Bill Submit Successfuly");
            window.location.replace("Emp_BillDetails.aspx?BillId=" + billid);
        },
        error: function (err) {
            alert(err.statusText)
        }
    });
}

function BillSumitRateCondition() {
    var acaID = $("input[id*='hdnAcaID']").val();

    var billTypeID = $("select[id*='ddlBillType1']").val();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/ConstructionUserController.asmx/BillSumitRateCondition",
        data: JSON.stringify({ AcademyID: parseInt(acaID), BillTypeID: parseInt(billTypeID) }),
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var totalBillingAmount = result.d == undefined ? 0 : result.d;
                var Amount = $("input[id*='hdnAmount']").val() == "" ? 0 : parseFloat($("input[id*='hdnAmount']").val());
                $("input[id*='hdnTotalAmount']").val(totalBillingAmount);
                var billingAmount = 0;
                var Amt = 0;
                var RoundAmt = 0;
                billingAmount = parseFloat(Amount) + parseFloat(totalBillingAmount);
                if (totalBillingAmount == 5000 && billingAmount > 5000) {
                    $("#spnmsg").html("You have already submitted bills for Rs 5000 for this month now you can not submit more bills for this month.");
                    return false;
                }
                else if (totalBillingAmount < 5000 && billingAmount > 5000) {
                    Amt = (parseFloat(5000) - parseFloat(totalBillingAmount));
                    RoundAmt = Amt.toFixed(2);
                    $("#spnmsg").html("You already submitted bills for Rs " + totalBillingAmount + " for this month now you can submit bills up to Rs" + RoundAmt + ".");
                    return false;
                }
                else {
                    Amt = (parseFloat(5000) - parseFloat(totalBillingAmount));
                    RoundAmt = Amt.toFixed(2);
                    $("#spnmsg").html("You already submitted bills for Rs " + totalBillingAmount + " for this month now you can submit bills up to Rs" + RoundAmt + ".");
                    return false;
                }
                return true;
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }

    });
   
}

function ValidationAmount() {
    var alreadyBillAmount = $("input[id*='hdnAmount']").val();
    var billAmount = $("input[id*='hdnTotalAmount']").val();
    var billingAmount = 0;
    billingAmount = parseFloat(billAmount) + parseFloat(alreadyBillAmount);
    if ($("input[id*='hdnAmount']").val() == "") {
        alert("Please click on the BillCost button");
        return false;
    }
    else if (( billingAmount > 5000) && $("input[id*='hdnUpdateBillID']").val() == "") {
        alert("You already submitted bills for Rs 5000 for this month. Now you can not submit more bills for this month.");
        return false;
    }
    return true;
}

function LoadNonSanctionedBillDetailByBillID(billID) {
    $("input[id*='hdnUpdateBillID']").val(billID);
    $("#btnSubmitApprovel").show();
    AutofillMaterialSearchBox(0);
    GetPurchaseSource();
    $("#aDeleteRow0").hide();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/ConstructionUserController.asmx/GetNonSanctionedBillDetailByBillID",
         data: JSON.stringify({ BillID: billID }),
        dataType: "json",
        success: function (responce) {

            var msg = responce.d;

            if (msg != undefined) {

                $("input[id*='txtBillDate']").val(getJsonDate(msg.BillDate));
                $("input[id*='txtGateEntryNo']").val(msg.GateEntryNo);
                $("input[id*='txtAgencyName']").val(msg.AgencyName);
                $("select[id*='ddlBillType1']").val(msg.BillType);
                $("input[id*='txtAgenyBillNo']").val(msg.VendorBillNumber);
                $("input[id*='hdnVandorID']").val(msg.VendorID);

                if (msg.VendorBillPath != null) {
                    $("#afileVendorBillPath").show();
                    var dlPics = msg.VendorBillPath.split(',');
                    var DLlink = "";
                    for (var i = 0; i < dlPics.length; i++) {
                        DLlink += " <a href=Bills/" + dlPics[i] + " target='_blank'>AgencyBillCopy</a>";
                    }
                    $("#afileVendorBillPath")[0].innerHTML = DLlink;
                }

              
                GetPurchaseSource();

                var matrow = 0;
                for (var i = 0; i < msg.SubmitBillByUserAndMaterialOthersRelation.length; i++) {
                    if (matrow > 0) {
                        AddUpdateMaterialRow();
                    }
                   // SourceType_ChangeEvent(i);
                    $("#ddlSourceType" + matrow).val(1);
                    $("#txtMaterialName" + matrow).val(msg.SubmitBillByUserAndMaterialOthersRelation[i].ItemName);
                    $("#txtQty" + matrow).val(msg.SubmitBillByUserAndMaterialOthersRelation[i].Qty);
                    $("#txtRate" + matrow).val(msg.SubmitBillByUserAndMaterialOthersRelation[i].Rate);
                    $("#lblUnit" + matrow).text(msg.SubmitBillByUserAndMaterialOthersRelation[i].UnitName);
                    $("#txtTotal" + matrow).text((msg.SubmitBillByUserAndMaterialOthersRelation[i].Qty) * (msg.SubmitBillByUserAndMaterialOthersRelation[i].Rate));

                    var selectedMaterial = GetSelectedMaterial(msg.SubmitBillByUserAndMaterialOthersRelation[i].ItemName);
                    if (selectedMaterial.length > 0) {
                        $("#hdnMatID" + matrow).val(selectedMaterial[0].MatID);
                        $("#lblUnit" + matrow).text(selectedMaterial[0].Unit.UnitName);
                        $("#hdnUnitID" + matrow).val(selectedMaterial[0].Unit.UnitId);
                        $("#spnMaterialTypeID" + matrow).text(selectedMaterial[0].MaterialType.MatTypeName);
                        $("#hdnMatTypeID" + matrow).val(selectedMaterial[0].MaterialType.MatTypeId);
                        $("#hdnMaterialTypeName" + matrow).val(selectedMaterial[0].MaterialType.MatTypeName);
                    }
                    matrow = parseInt(matrow) + 1;
                }
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
        }
    });
}

function getJsonDate(strDate) {
    var displayDate = "";
    if (strDate != null) {
        var date = new Date(parseInt(strDate.substr(6)));
        // format display date (e.g. 04/10/2012)
        displayDate = $.datepicker.formatDate("mm/dd/yy", date);
    }
    return displayDate;
}

function AddUpdateMaterialRow() {
    $('#tblEstimateMatDetail tr').last().after('<tr id="tr' + cntM + '"><td><span id="spn' + cntM + '">' + (cntM + 1) + '</span></td>' +
        '<td> <select id="ddlSourceType' + cntM + '"  name="ddlSourceType' + cntM + '" style="width:150px;" onchange="SourceType_ChangeEvent(' + cntM + '); ><option value="0">Select Source Type</option></select></td>' +
       '<td><input id="txtMaterialName' + cntM + '" onblur="MaterialTextBox_ChangeEvent(' + cntM + ');" name="txtMaterialName' + cntM + '" value="" type="text" style="width:200px;" /><br/><div id="menu-container' + cntM + '" style="position:absolute; width:500px;"></div></td>' +
        '<td> <span id="spnMaterialTypeID' + cntM + '" style="width:150px;"></td>' +
        '<td><input id="txtQty' + cntM + '" name="txtQty' + cntM + '" value="" type="text" style="width:80px;" onchange="Qty_ChangeEvent(' + cntM + ');"/></td>' +
        '<td>  <label id="lblUnit' + cntM + '" name="lblUnit' + cntM + '" ></label></td>' +
        '<td> <input id="txtRate' + cntM + '" name="txtRate' + cntM + '" value="" type="text" style="width:80px;" onchange="Rate_ChangeEvent(' + cntM + ');" /></td>' +
        '<td><span id="txtTotal' + cntM + '" name="txtTotal' + cntM + '" class="span6 typeahead" style="width:200px;"/></td>' +
        '<td><a href="javascript:void(0);" id="aAddNewRow' + cntM + '" onclick="AddMaterialRow();"><b>Add Row</b></a> <a href="javascript:void(0);" id="aDeleteRow' + cntM + '" onclick="removeRow(' + cntM + ');"><b>Delete</b></a><input type="hidden" id="hdnMatID' + cntM + '" /><input type="hidden" id="hdnMatTypeID' + cntM + '" /><input type="hidden" id="hdnUnitID' + cntM + '" /><input type="hidden" id="hdnMaterialTypeName' + cntM + '" /></td></tr>');

    BindPurchaseSource(cntM);
    fixSerialNumber();
    AutofillMaterialSearchBox(cntM);
    GetVendors();

    $("#aDeleteRow" + cntM).hide();

    if (cntM > 0) {
        var cntR = cntM - 1;
        $("#aAddNewRow" + cntR).hide();
        $("#aDeleteRow" + cntR).show();
        $("#aAddNewRow0").hide();
        $("#aDeleteRow0").show();
    }
    cntM++;
}

function AutofillMaterialSearchBox(cnt) {
    var dataSrc;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetActiveMaterialsForAutoFill",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                $("#txtMaterialName" + cnt).autocomplete({
                    source: result.d,
                    appendTo: '#menu-container' + cnt,
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });


}

function GetSelectedMaterial(materialName) {
    $.ajax({
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetBindMaterialByMaterialName",
        data: JSON.stringify({ matName: materialName }),
        dataType: "json",
        success: function (responce) {
            MaterialList = responce.d;
        }
    });
    return MaterialList;

}