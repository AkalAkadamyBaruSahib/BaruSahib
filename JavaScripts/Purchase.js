


$(document).ready(function () {
    if (str.length > 0) {
        var selectedMatItems = "";
        if ($("input[id*='hdnMaterialitems']").val() == "") {
            selectedSeats += str.join(',');
            $("input[id*='hdnMaterialitems']").val(selectedMatItems);
        }
        else {
            selectedMatItems = $("input[id*='hdnMaterialitems']").val() + ",";
            selectedMatItems += str.join(',');
            $("input[id*='hdnMaterialitems']").val(selectedMatItems);
        }

       
    }
});

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
