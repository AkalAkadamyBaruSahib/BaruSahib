
var MaterialList = new Array();
var VendorList = new Array();
var MaterialObjectList;
var VendorObjectList;
var MaterialList;
var delItems = 0;
var cntM = 1;

$(document).ready(function () {

    $("#btnSendforApproval").click(function (e) {
        if (Validation()) {
            ClientSideClick(this);
            $("#btnSendforApproval").prop('disabled', true);
            SendRateforApproval();
        }
    });
    if ($("input[id*='hdnMaterialID']").val() == undefined || $("input[id*='hdnMaterialID']").val() == "") {
        GetVendors(0);
        GetMaterials(0);
    }

    $("#aDeleteRow0").hide();
});


function AddMaterialRow() {
    var cntR = cntM - 1;
    if (Validation()) {
        $('#tr' + cntR).last().after('<tr id="tr' + cntM + '"><td><span id="spn' + cntM + '">' + (cntM + 1) + '</span></td>' +
            '<td><input id="txtVendorName' + cntM + '" name="txtVendorName' + cntM + '"  onblur="VendorTextBox_ChangeEvent(' + cntM + ');"  type="text" style="width:200px;" /></td>' +
            '<td><input id="txtMaterialName' + cntM + '" name="txtMaterialName' + cntM + '" onblur="MaterialTextBox_ChangeEvent(' + cntM + ');"  type="text" style="width:200px;" /></td>' +
            '<td><label id="lblMaterialType' + cntM + '" name="lblMaterialType' + cntM + '" ></label></td>' +
            '<td><label id="lblUnit' + cntM + '" name="lblUnit' + cntM + '" ></label></td>' +
            '<td><table  id="myNestedTableOne' + cntM + '"><tr id="trlblMRP' + cntM + '"><td>MRP/Dealer Price: <label id="lblMRP' + cntM + '" name="lblMRP' + cntM + '""></label></td></tr><tr id="trlblDiscount' + cntM + '"><td>Discount: <label id="lblDiscount' + cntM + '" name="lblDiscount' + cntM + '""></label></td></tr><tr id="trlblAdditionalDiscount' + cntM + '"><td>Additional Discount: <label id="lblAdditionalDiscount' + cntM + '" name="lblAdditionalDiscount' + cntM + '""></label></td></tr><tr id="trlblVat' + cntM + '"><td>Vat: <label id="lblVat' + cntM + '" name="lblVat' + cntM + '""></label></td></tr><tr id="trlblRate' + cntM + '"><td>Net Rate: <label id="lblRate' + cntM + '" name="lblRate' + cntM + '""></label></td></tr></table></td>' +
            '<td><table  id="myNestedTableTwo' + cntM + '"><tr id="trMRP' + cntM + '"><td>MRP/Dealer Price: <input id="txtMRP' + cntM + '" name="txtMRP' + cntM + '" value="" type="text" style="width:50px;" onblur="MRPTextBox_ChangeEvent(' + cntM + ');" /></td></tr><tr id="trDiscount' + cntM + '"><td>Discount: <input id="txtDiscount' + cntM + '" name="txtDiscount' + cntM + '" value="" type="text" style="width:50px;" onblur="DiscountTextBox_ChangeEvent(' + cntM + ');" /></td></tr><tr id="trAdditionalDiscount' + cntM + '"><td>Additional Discount: <input id="txtAdditionalDiscount' + cntM + '" name="txtAdditionalDiscount' + cntM + '" value="" type="text" style="width:50px;" onblur="AdditionalDiscountTextBox_ChangeEvent(' + cntM + ');" /></td></tr><tr id="trVat' + cntM + '"><td>Vat: <input id="txtVat' + cntM + '" name="txtVat' + cntM + '" value="" type="text" style="width:50px;" onblur="VatTextBox_ChangeEvent(' + cntM + ');" /></td></tr><tr id="trRate' + cntM + '"><td>Net Rate: <label id="lblNetRate' + cntM + '" name="lblNetRate' + cntM + '" ></label></td></tr></table></td>' +
            '<td><a href="javascript:void(0);" id="aAddNewRow' + cntM + '" onclick="AddMaterialRow();"><b>Add Row</b></a> <a href="javascript:void(0);" id="aDeleteRow' + cntM + '" onclick="removeRow(' + cntM + ');"><b>Delete</b></a><input type="hidden" id="hdnMatID' + cntM + '" /><input type="hidden" id="hdnMatTypeID' + cntM + '" /><input type="hidden" id="hdnUnitID' + cntM + '" /><input type="hidden" id="hdnVendorID' + cntM + '" /></td></tr>');

        fixSerialNumber();
        GetMaterials(cntM);
        GetVendors(cntM);
        $("#aDeleteRow" + cntM).hide();

        if (cntM > 0) {
            $("#aAddNewRow" + cntR).hide();
            $("#aDeleteRow" + cntR).show();
            $("#aAddNewRow0").hide();
            $("#aDeleteRow0").show();
        }
        cntM++;
    }
}

function GetMaterials(cntID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetActiveMaterials",
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            MaterialObjectList = result.d;
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });


    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetActiveMaterialsForAutoFill",
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            MaterialList = result.d;
            $("#txtMaterialName" + cntID).autocomplete({
                source: MaterialList
            });
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

function removeRow(removeNum) {
    $('#tr' + removeNum).remove();
    delItems = delItems + 1;
    fixSerialNumber();
    return false;
    cntM--;
}

function MaterialTextBox_ChangeEvent(cntID) {
    $("#hdnMatID" + cntID).val("");
    $("#hdnUnitID" + cntID).val("");
    $("#hdnMatTypeID" + cntID).val("");
    var Matname = $("#txtMaterialName" + cntID).val();
    var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];
    if (selectedMaterial != undefined) {
        $("#hdnMatID" + cntID).val(selectedMaterial.MatID);
        $("#lblUnit" + cntID).text(selectedMaterial.Unit.UnitName);
        $("#hdnUnitID" + cntID).val(selectedMaterial.Unit.UnitId);
        $("#hdnMatTypeID" + cntID).val(selectedMaterial.MatTypeID);
        $("#lblMaterialType" + cntID).text(selectedMaterial.MaterialType.MatTypeName);

        $("#lblDiscount" + cntID).text(selectedMaterial.Discount);
        $("#lblAdditionalDiscount" + cntID).text(selectedMaterial.AdditionalDiscount);
        $("#lblVat" + cntID).text(selectedMaterial.Vat);
        if (selectedMaterial.Discount == 0 && selectedMaterial.Vat == 0) {
            $("#lblMRP" + cntID).text(selectedMaterial.MatCost);
        }
        else {
            $("#lblMRP" + cntID).text(selectedMaterial.MRP);
        }
        $("#lblRate" + cntID).text(selectedMaterial.MatCost);
        $("#txtMRP" + cntID).val("");
        $("#txtDiscount" + cntID).val("");
        $("#txtAdditionalDiscount" + cntID).val("");
        $("#txtVat" + cntID).val("");
        $("#lblNetRate" + cntID).text("");
    }
}

function MRPTextBox_ChangeEvent(cntID) {
    var mrp = $("#txtMRP" + cntID).val();
    var discount = $("#txtDiscount" + cntID).val();
    var addtinalDiscount = $("#txtAdditionalDiscount" + cntID).val();
    var vat = $("#txtVat" + cntID).val();
    if (vat == "" && discount == "" && addtinalDiscount == "") {
        $("#lblNetRate" + cntID).text(mrp);
    }
    else if (discount == "" && vat != "" && addtinalDiscount == "") {
        var rateAftervat = parseFloat(mrp) + (parseFloat(mrp) * parseFloat(vat) / 100);
        $("#lblNetRate" + cntID).text(rateAftervat.toFixed(2));
    }
    else if (vat == "" && discount != "" && addtinalDiscount == "") {
        var afterDiscountRate = parseFloat(mrp) - (parseFloat(mrp) * parseFloat(discount) / 100);
        $("#lblNetRate" + cntID).text(afterDiscountRate.toFixed(2));
    }
    else if (vat != "" && discount != "" && addtinalDiscount == "") {
        var afterDiscountRate = parseFloat(mrp) - (parseFloat(mrp) * parseFloat(discount) / 100);
        var totalAmount = parseFloat(afterDiscountRate) + (parseFloat(afterDiscountRate) * parseFloat(vat) / 100);
        $("#lblNetRate" + cntID).text(totalAmount.toFixed(2));
    }
    else {
        var afterDiscountRate = parseFloat(mrp) - (parseFloat(mrp) * parseFloat(discount) / 100);
        var addtionalDiscountRate = parseFloat(afterDiscountRate) - (parseFloat(afterDiscountRate) * parseFloat(addtinalDiscount) / 100);
        var totalAmount = parseFloat(addtionalDiscountRate) + (parseFloat(addtionalDiscountRate) * parseFloat(vat) / 100);
        $("#lblNetRate" + cntID).text(totalAmount.toFixed(2));
    }
}

function DiscountTextBox_ChangeEvent(cntID) {
    var mrp = $("#txtMRP" + cntID).val();
    var discount = $("#txtDiscount" + cntID).val();
    var addtinalDiscount = $("#txtAdditionalDiscount" + cntID).val();
    var vat = $("#txtVat" + cntID).val();
    var afterDiscountRate = parseFloat(mrp) - (parseFloat(mrp) * parseFloat(discount) / 100);
    if (mrp == "") {
        $("#txtMRP" + cntID).css('border-color', 'red');
        return false;
    }
    else {
        if (addtinalDiscount == "") {
            if (vat == "") {
                $("#lblNetRate" + cntID).text(afterDiscountRate.toFixed(2));
            }
            else {
                var totalCalculateAmount = parseFloat(afterDiscountRate) + (parseFloat(afterDiscountRate) * parseFloat(vat) / 100);
                $("#lblNetRate" + cntID).text(totalCalculateAmount.toFixed(2));
            }
        }
        else {
            var addtionalDiscountRate = parseFloat(afterDiscountRate) - (parseFloat(afterDiscountRate) * parseFloat(addtinalDiscount) / 100);
            if (vat == "") {
                $("#lblNetRate" + cntID).text(afterDiscountRate.toFixed(2));
            }
            else {
                var totalCalculateAmount = parseFloat(addtionalDiscountRate) + (parseFloat(addtionalDiscountRate) * parseFloat(vat) / 100);
                $("#lblNetRate" + cntID).text(totalCalculateAmount.toFixed(2));
            }

        }
    }
}

function AdditionalDiscountTextBox_ChangeEvent(cntID) {
    var mrp = $("#txtMRP" + cntID).val();
    var discount = $("#txtDiscount" + cntID).val();
    var addtinalDiscount = $("#txtAdditionalDiscount" + cntID).val();
    var vat = $("#txtVat" + cntID).val();
    var afterDiscountRate = parseFloat(mrp) - (parseFloat(mrp) * parseFloat(discount) / 100);
    var addtionalDiscountRate = parseFloat(afterDiscountRate) - (parseFloat(afterDiscountRate) * parseFloat(addtinalDiscount) / 100);
    if (mrp == "") {
        $("#txtMRP" + cntID).css('border-color', 'red');
        return false;
    }
    else {
        if (addtinalDiscount == 0) {
            if (vat == "") {
                $("#lblNetRate" + cntID).text(afterDiscountRate.toFixed(2));
            }
            else {
                var totalCalculateAmount = parseFloat(afterDiscountRate) + (parseFloat(afterDiscountRate) * parseFloat(vat) / 100);
                $("#lblNetRate" + cntID).text(totalCalculateAmount.toFixed(2));
            }
        }
        else {
            if (vat == "") {
                $("#lblNetRate" + cntID).text(addtionalDiscountRate.toFixed(2));
            }
            else {
                var totalCalculateAmount = parseFloat(addtionalDiscountRate) + (parseFloat(addtionalDiscountRate) * parseFloat(vat) / 100);
                $("#lblNetRate" + cntID).text(totalCalculateAmount.toFixed(2));
            }

        }
    }
}

function VatTextBox_ChangeEvent(cntID) {
    var mrp = $("#txtMRP" + cntID).val();
    var discount = $("#txtDiscount" + cntID).val();
    var addtinalDiscount = $("#txtAdditionalDiscount" + cntID).val();
    var vat = $("#txtVat" + cntID).val();
    if (mrp == "")
    {
        $("#txtMRP" + cntID).css('border-color', 'red');
        return false;
    }
    else if (discount == "") {
        $("#txtDiscount" + cntID).css('border-color', 'red');
        return false;
    }
    else if (addtinalDiscount == "") {
        $("#txtAdditionalDiscount" + cntID).css('border-color', 'red');
        return false;
    }
    else {
        var afterDiscountRate = parseFloat(mrp) - (parseFloat(mrp) * parseFloat(discount) / 100);
        var afterAddDiscountRate = parseFloat(afterDiscountRate) - (parseFloat(afterDiscountRate) * parseFloat(addtinalDiscount) / 100);
        var rateAfterDiscountvat = parseFloat(afterAddDiscountRate) + (parseFloat(afterAddDiscountRate) * parseFloat(vat) / 100);
        $("#lblNetRate" + cntID).text(rateAfterDiscountvat.toFixed(2));
    }
}

function SendRateforApproval() {

    var tablelength = $("#tbody").children('tr').length;
    var param = new Object();
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {
        if ($("#hdnMatID" + i).val() != undefined) {

            param.UserName = $("input[id*='hdnUserName']").val();
            param.MatName = $("#txtMaterialName" + i).val();
            param.MatTypeID = $("#hdnMatTypeID" + i).val();
            param.MatCost = $("#lblRate" + i).text();
            param.VendorName = $("#txtVendorName" + i).val();

            var mrp = $("#txtMRP" + i).val();
            var discount = $("#txtDiscount" + i).val();
            var vat = $("#txtVat" + i).val();
            var addtinalDiscount = $("#txtAdditionalDiscount" + i).val();
            var afterDiscountRate = parseFloat(mrp) - (parseFloat(mrp) * parseFloat(discount) / 100);
            var addtinalDiscountRate = parseFloat(afterDiscountRate) - (parseFloat(afterDiscountRate) * parseFloat(addtinalDiscount) / 100);
            var rateAfterDiscountvat = parseFloat(addtinalDiscountRate) + (parseFloat(addtinalDiscountRate) * parseFloat(vat) / 100);

            var MaterialNonApprovedRate = new Object();

            MaterialNonApprovedRate.MatID = $("#hdnMatID" + i).val();
            MaterialNonApprovedRate.CreatedBy = $("input[id*='hdnInchargeID']").val();
            MaterialNonApprovedRate.CreatedOn = "";
            MaterialNonApprovedRate.MRP = $("#txtMRP" + i).val();
            MaterialNonApprovedRate.Discount = $("#txtDiscount" + i).val();
            MaterialNonApprovedRate.AdditionalDiscount = $("#txtAdditionalDiscount" + i).val();
            MaterialNonApprovedRate.Vat = $("#txtVat" + i).val();
            MaterialNonApprovedRate.NetRate = rateAfterDiscountvat.toFixed(2);
            MaterialNonApprovedRate.VendorID = $("#hdnVendorID" + i).val();

            param.materialNonApprovedRate = MaterialNonApprovedRate;

            $.ajax({
                type: "POST",
                async: false,
                contentType: "application/json; charset=utf-8",
                url: "Services/PurchaseControler.asmx/SaveNonApprovedRate",
                data: JSON.stringify(param),
                dataType: "json",
                success: function (result, textStatus) {
                    if (textStatus == "success") {
                     

                    }
                },
                error: function (result, textStatus) {
                    alert(result.responseText)
                }
            });
        }
    }
    alert("Rate Send for Approval Successfully");
    ClearTextBox();
    $("#btnSendforApproval").prop('disabled', false);
}

function Validation() {
    var tablelength = $("#tbody").children('tr').length;
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {
        if ($("#txtVendorName" + i).val() == "" || $("#txtVendorName" + i).val() == "0") {
            $("#txtVendorName" + i).css('border-color', 'red');
            return false;
        }
        else {
            $("#txtVendorName" + i).css('border-color', '');

        }

        if ($("#hdnVendorID" + i).val() == "undefined" || $("#hdnVendorID" + i).val() == "") {
            $("#txtVendorName" + i).css('border-color', 'red');
            $("#txtVendorName" + i).val("");
            return false;
        }
        else {
            $("#txtVendorName" + i).css('border-color', '');
        }

        if ($("#txtMaterialName" + i).val() == "" || $("#txtMaterialName" + i).val() == "0") {
            $("#txtMaterialName" + i).css('border-color', 'red');
            return false;
        }
        else {
            $("#txtMaterialName" + i).css('border-color', '');

        }

        if ($("#hdnMatID" + i).val() == "undefined" || $("#hdnMatID" + i).val() == "") {
            $("#txtMaterialName" + i).css('border-color', 'red');
            $("#txtMaterialName" + i).val("");
            return false;
        }
        else {
            $("#txtMaterialName" + i).css('border-color', '');
        }

        if ($("#txtMRP" + i).val() != undefined) {
            var value = $("#txtMRP" + i).val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);

            if ($("#txtMRP" + i).val() == "" || $("#txtMRP" + i).val() == "0" || !value.match(regex)) {
                $("#txtMRP" + i).css('border-color', 'red');
                return false;
            }
            else {
                $("#txtMRP" + i).css('border-color', '');
            }
        }

        if ($("#txtDiscount" + i).val() != undefined) {
            var value = $("#txtDiscount" + i).val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);

            if ($("#txtDiscount" + i).val() == "" || !value.match(regex)) {
                $("#txtDiscount" + i).css('border-color', 'red');
                return false;
            }
            else {
                $("#txtDiscount" + i).css('border-color', '');
            }
        }

        if ($("#txtAdditionalDiscount" + i).val() != undefined) {
            var value = $("#txtAdditionalDiscount" + i).val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);

            if ($("#txtAdditionalDiscount" + i).val() == "" || !value.match(regex)) {
                $("#txtAdditionalDiscount" + i).css('border-color', 'red');
                return false;
            }
            else {
                $("#txtAdditionalDiscount" + i).css('border-color', '');
            }
        }

        if ($("#txtVat" + i).val() != undefined) {
            var value = $("#txtVat" + i).val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);

            if ($("#txtVat" + i).val() == "" || !value.match(regex)) {
                $("#txtVat" + i).css('border-color', 'red');
                return false;
            }
            else {
                $("#txtVat" + i).css('border-color', '');
            }
        }
     }
    return true;
}

function ClearTextBox() {
    var tablelength = $("#tbody").children('tr').length;
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {
        $("#txtMaterialName" + i).val("");
        $("#txtMRP" + i).val("");
        $("#txtDiscount" + i).val("");
        $("#txtVat" + i).val("");
        $("#lblMaterialType" + i).text("");
        $("#lblUnit" + i).text("");
        $("#lblRate" + i).text("");
        $("#lblNetRate" + i).text("");
        $("#lblNetRate" + i).text("");
        $("#txtVendorName" + i).val("");
        $("#txtAdditionalDiscount" + i).val("");
    }
}

function GetMaterialInfoByMatID(matID, vendorID, newRate) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetBindMaterialByMaterialID",
        data: JSON.stringify({ matID: parseInt(matID) }),
        dataType: "json",
        success: function (responce) {
          var msg = responce.d;
            var cuntID = 0;
            if (msg != undefined) {
                $("#hdnMatID0").val(msg[0].MatID);
                $("#lblUnit0").text(msg[0].Unit.UnitName);
                $("#hdnUnitID0").val(msg[0].Unit.UnitId);
                $("#hdnMatTypeID0").val(msg[0].MatTypeID);
                $("#lblDiscount0").text(msg[0].Discount);
                $("#lblVat0").text(msg[0].Vat);
                if (msg[0].Discount == 0 && msg[0].Vat == 0)
                {
                    $("#lblMRP0").text(msg[0].MatCost);
                }
                else
                {
                    $("#lblMRP0").text(msg[0].MRP);
                }
                $("#lblRate0").text(msg[0].MatCost);
                $("#txtMaterialName0").val(msg[0].MatName);
                $("#lblMaterialType0").text(msg[0].MaterialType.MatTypeName);
                $("#txtMRP0").val(newRate);
                $("#lblNetRate0").text(newRate);
                $("#lblAdditionalDiscount0").text(msg[0].AdditionalDiscount);
                GetVendorName(vendorID);
                $("#hdnVendorID0").val(vendorID);
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
        }
    });
}

function GetVendorName(vendorID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetVendorAddress",
        data: JSON.stringify({ vendorID: parseInt(vendorID) }),
        dataType: "json",
        success: function (responce) {
            var msg = responce.d;
            if (msg != undefined) {
                $("#txtVendorName0").val(msg[0].VendorName);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetVendors(cntID) {
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
    });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetActiveVendorForAutoFill",
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            VendorList = result.d;
            $("#txtVendorName" + cntID).autocomplete({
                source: VendorList,
            });
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function VendorTextBox_ChangeEvent(cntID) {
    var Vname = $("#txtVendorName" + cntID).val();
    $("#hdnVendorID" + cntID).val("");
    var selectedMaterial = $.grep(VendorObjectList, function (e) { return e.VendorName == Vname })[0];
    if (selectedMaterial != undefined) {
        //  $("input[id*='hdnVendorID']").val(selectedMaterial.ID);
        $("#hdnVendorID" + cntID).val(selectedMaterial.ID);
    }
}












