


$(document).ready(function () {
    AutofillMaterialSearchBox();
});

function AutofillMaterialSearchBox() {
    var dataSrc;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetActiveMaterialsForAutoFill",
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

