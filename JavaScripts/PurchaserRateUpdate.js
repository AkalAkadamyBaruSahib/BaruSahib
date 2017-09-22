
var VendorObjectList;
var cntM = 0;
var VendorList = new Array();

$(document).ready(function () {
    $("#aRateUpdateLink").click(function (e) {
        var MatTypeID = $("input[id*='hdnMaterialType']").val();
        var MatID = $("input[id*='hdnMaterialID']").val();
        var VendorID = $("input[id*='hdnVendor']").val();
        var NewRate = $("input[id*='hdnNewRate']").val();
        var EstID = $("input[id*='hdnEstID']").val();
        window.location.replace("RateUpload.aspx?MatTypeID=" + MatTypeID + "&MatID=" + MatID + "&VendorID=" + VendorID + "&NewRate=" + NewRate + "&EstID=" + EstID);
    });
    $("[id*=gvMaterailDetailForPurchase] [id*=aVendorLink]").click(function () {
        var row = $(this).closest("tr");
        row.find("[id*='txtVendorName']").show();
        row.find("[id*='txtVendorName']").prop('disabled', false);
        row.find("[id*='aVendorLink']").hide();

    });

    $("[id*=gvMaterailDetailForPurchase] input[id*='btnDispatch']").click(function (e) {
        var row = $(this).closest("tr");
        row.find("[id*='txtRate']").prop('disabled', false);
        row.find("[id*='txtDiscount']").prop('disabled', false);
        row.find("[id*='txtMRP']").prop('disabled', false);
        var discountVal = row.find("[id*='txtDiscount']").val();
        if (discountVal > 100) {
            alert("Discount can not Greater then 100%");
            return false;
        }
    });
    $("[id*=gvMaterailDetailForPurchase] input[id*='txtRate']").change(function (e) {
        var row = $(this).closest("tr");
        row.find("[id*='txtRate']").prop('disabled', false);
        row.find("[id*='txtDiscount']").prop('disabled', false);
        row.find("[id*='txtMRP']").prop('disabled', false);
        $("input[id*='hdnNewRate']").val(row.find("[id*='txtRate']").val());
        if (row.find("[id*='txtRate']").val() == "0") {
            row.find("[id*='txtRate']").val("");
            row.find("[id*='txtRate']").css('border-color', 'red');
        }
        else {
            row.find("[id*='txtRate']").css('border-color', '');
        }

  });
    $("[id*=gvMaterailDetailForPurchase] input[id*='txtPurchaseQty']").change(function (e) {
        var row = $(this).closest("tr");
        row.find("[id*='txtRate']").prop('disabled', false);
        row.find("[id*='txtDiscount']").prop('disabled', false);
        row.find("[id*='txtMRP']").prop('disabled', false);
    });

    $("[id*=gvWorkShopMaterial] input[id*='txtDispatchQty']").change(function (e) {
        var row = $(this).closest("tr");
        row.find("[id*='txtWorkshopRate']").prop('disabled', false);
    });

    $("[id*=gvWorkShopMaterial] input[id*='txtWorkshopRate']").change(function (e) {
        var row = $(this).closest("tr");
        row.find("[id*='txtWorkshopRate']").prop('disabled', false);
        if (row.find("[id*='txtWorkshopRate']").val() == "0") {
            row.find("[id*='txtWorkshopRate']").val("");
            row.find("[id*='txtWorkshopRate']").css('border-color', 'red');
        }
        else {
            row.find("[id*='txtWorkshopRate']").css('border-color', '');
        }
    });

    if ($("input[id*='hdnModule']").val() != 3) {
        GetVendors();
    }
});

function OpenUpdateRatePopUp() {
     $("input[id*='hdnMaterialType']").val(MatTypeID);
    $("input[id*='hdnMaterialID']").val(MatID);
    $('#divUpdateRate').modal('show');
}

function ShowPopup(MatTypeID, MatID,VendorID,NewRate,EstID) {
    $("input[id*='hdnMaterialType']").val(MatTypeID);
    $("input[id*='hdnMaterialID']").val(MatID);
    $("input[id*='hdnVendor']").val(VendorID);
    $("input[id*='hdnNewRate']").val(NewRate);
    $("input[id*='hdnEstID']").val(EstID);
    $(function () {
        $("#divUpdateRate").dialog({
            title: "Material Rate Update",
            buttons: {
                Close: function () {
                    $(this).dialog('close');
                }
            },
            modal: true, width: 450
        });
    });
};

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


function AutofillVendorInfoSearchBox() {
    var dataSrc;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetActiveVendorForAutoFill",
        dataType: "json",
        success: function (result, textStatus) {
            VendorList = result.d;
            $("[id*=gvMaterailDetailForPurchase] [id*=txtVendorName]").autocomplete({
                source: VendorList,
            });

            $("[id*=gvMaterailDetailForPurchase] [id*=txtVendorName]").on('autocompletechange change', function () {
                var row = $(this).closest("tr");
                var Vname = row.find("[id*='txtVendorName']").val();
                $("input[id*='hdnVandorID']").val("");
               var selectedVendorList = $.grep(VendorObjectList, function (e) { return e.VendorName == Vname })[0];
                if (selectedVendorList != undefined) {
                    $("input[id*='hdnVandorID']").val(selectedVendorList.ID);
                }
            }).change();
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}