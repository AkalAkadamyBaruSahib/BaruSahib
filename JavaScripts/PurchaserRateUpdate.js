
var VendorObjectList;
var cntM = 0;
var VendorList = new Array();

$(document).ready(function () {
    $("#aRateUpdateLink").click(function (e) {
        var MatTypeID = $("input[id*='hdnMaterialType']").val();
        var MatID = $("input[id*='hdnMaterialID']").val();
        window.location.replace("RateUpload.aspx?MatTypeID=" + MatTypeID + "&MatID=" + MatID);
    });
    $("[id*=gvMaterailDetailForPurchase] [id*=aVendorLink]").click(function () {
        var row = $(this).closest("tr");
        row.find("[id*='txtVendorName']").show();
        row.find("[id*='txtVendorName']").prop('disabled', false);
        row.find("[id*='aVendorLink']").hide();

    });

    $("[id*=gvMaterailDetailForPurchase] input[id*='btnDispatch']").click(function (e) {
        var row = $(this).closest("tr");
        var tablelength = row.find("[id*='txtRate']").prop('disabled', false);
        //if (tablelength == "") {
        //    row.find("#errMsg").show();
        //    return false;
        //}
        //else {
        //    return true;
        //}
    });
    GetVendors();
});

function OpenUpdateRatePopUp() {
     $("input[id*='hdnMaterialType']").val(MatTypeID);
    $("input[id*='hdnMaterialID']").val(MatID);
    $('#divUpdateRate').modal('show');
}

function ShowPopup(MatTypeID, MatID) {
    $("input[id*='hdnMaterialType']").val(MatTypeID);
    $("input[id*='hdnMaterialID']").val(MatID);
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