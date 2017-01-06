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

        if (rate == "" || !rate.match(regex) || rate < 0) {
            row.find("input[id*='txtRateSan']").css('border-color', 'red');
            row.find("input[id*='txtRateSan']").val("");
            row.find("#errMsg").show();
            return false;
        }
        else if (qty == "") {
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

         if (qty == "" || !qty.match(regex) || qty <0) {
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
               if (vat < 0|| !vat.match(regex)) {
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

    AutofillVendorInfoSearchBox();
})

function TotalAmount() {
    var tablelength = $("[id*=gvAddItems2] tr").length;
    var totalAmt = 0;
    for (var i = 0 ; i < (tablelength) ; i++) {
        var Amt = $("[id*='lblAmtSan_" + i + "']").text();
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
            if (textStatus == "success") {
              //  $("input['id*=txtAgency']").autocomplete({
                $("#txtAgencyName").autocomplete({
                    source: result.d,
                    appendTo: '#menu-container0'
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}