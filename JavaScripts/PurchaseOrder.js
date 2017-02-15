
var materialname = new Array();
var tblUploadMaterial;
var SnoIds;
var MaterialList;
var selectedMaterialList = new Array();
var sum = 0;
var listVal = "";

$(document).ready(function () {
   
    BindVendors();
    BindEstimate();
    BindCurrentDate();
    BindDeliveryAddress();
    BindPONumber();
   
    $("#rowExcise").hide();
    $("[id$='lblIndentNo']").html($("select[id*='drpEstimate']").val());
    $("[id$='lblExciseStatus']").html('NA');
    $("[id$='lblVatStatus']").html('0%');
   
    $("#drpEstimate").change(function (e) {
        $("#divUploadMaterial").modal('show');
        GetMaterialList($(this).val());
        listVal += $(this).val() + ",";
        var EstVal = listVal.substr(0, listVal.length - 1);
        $("[id$='lblIndentNo']").html(EstVal);
        $("input[id*='hdnIndentNo']").val(EstVal);
    });
    $("input[id*='btnLoad']").click(function (e) {
        LoadPurchaseOrderInfo(selectedMaterialList);
        $("input[id*='hdnItemsLength']").val("");
        var SnoValue = "";
        for (var i = 0; i < selectedMaterialList.length; i++)
        {
            var Material = selectedMaterialList[i];
            SnoValue += Material.Sno + ",";
        }
        SnoValue = SnoValue.substr(0,SnoValue.length - 1);
        $("input[id*='hdnItemsLength']").val(SnoValue);
        
        $("#divUploadMaterial").modal('hide');
    });
    $("input[id*='btntest']").click(function (e) {
        $("#divUploadMaterial").modal('show');
        GetMaterialList($("select[id*='drpEstimate']").val());
    });
    $("#chkExcise").change(function () {
        if (this.checked) {
            $("#rowExcise").show();
        }
        else {
            $("#rowExcise").hide();
        }
    });
  
    $("input[id*='txtvat']").change(function () {
        var vatValue = $("input[id*='txtvat']").val() + "%";
        $("[id$='lblVatStatus']").html(vatValue);
        $("input[id*='hdnVatStatus']").val(vatValue);
        Calcution();
    });
    $("#drpVendor").change(function () {
        GetVendorAddress($(this).val());
    });
    $("#drpDeliveryAddress").change(function () {
        GetDeliveryAddressInfo($(this).val());
    });
    $("#drpBillingAddress").change(function () {
        GetBillingAddressInfo($(this).val());
    });
    $("#drpFreight").change(function () {
      var  FreightVal = $(this).val();
      $("[id$='lblFreight']").html(FreightVal);
      $("input[id*='hdnFreight']").val(FreightVal);
    });
  
   
});


function Calcution() {
    if ($("#chkExcise").is(":checked")) {
     
        var Excisetotal = ($("[id$='lblSubTotal']").text()) * ($("input[id*='txtExcise']").val() / 100);
        var subtotal = $("[id$='lblSubTotal']").text();
        var ExSum = parseInt(subtotal) + parseInt(Excisetotal);
        var VatTotal = ExSum * $("input[id*='txtvat']").val() / 100;
        var VatSum = VatTotal + ExSum;
    }
    else {
        var VatTotal = $("[id$='lblSubTotal']").text() * $("input[id*='txtvat']").val() / 100;
        var subtotal = $("[id$='lblSubTotal']").text();
        var VatSum = parseInt(subtotal) + parseInt(VatTotal);
    }
    $("[id$='lblGrandTotal']").html(VatSum);
    $("[id$='lblExciseStatus']").html('INCLUDED');
    $("input[id*='hdnGrandTotal']").val(VatSum);
    $("input[id*='hdnExciseStatus']").val('INCLUDED');

}

function RemoveItem(chkbox) {
    var SnoIds = ($(chkbox).attr("emrid"));
    if ($(chkbox).is(':checked')) {
        selectedMaterialList.push($.grep(MaterialList, function (e) { return e.Sno == SnoIds; })[0]);
    }
    else {
        selectedMaterialList.pop($.grep(MaterialList, function (e) { return e.Sno == SnoIds; })[0]);
    }
}

function IsMaterialAlreadySelected(sno) {
    var flag = false;

    if (selectedMaterialList != undefined) {
        var IsItem = $.grep(selectedMaterialList, function (e) { return e.Sno == sno; });
        if (IsItem.length > 0) {
            flag = true;
        }
    }
    return flag;
}

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

function BindVendors() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetVendorsNameList",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("#drpVendor").append($("<option></option>").val(value.ID).html(value.VendorName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindEstimate() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetEstimateNumberList",
        data: JSON.stringify({ purchaseSourceID: 3, inchargeID: 0 }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("#drpEstimate").append($("<option></option>").val(value.EstId).html(value.EstId));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetMaterialList(selectedValue) {

    /*create/distroy grid for the new search*/
    if (typeof tblUploadMaterial != 'undefined') {
        tblUploadMaterial.fnClearTable();
    }

    var rowCount = $('#tblUploadMaterial').find("#rowTemplate1").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate1").remove();
    }
    var rowTemplate = '<tr id="rowTemplate1"><td id="srno" style="width:45px;"></td><td id="materialname" style="width:230px;"></td><td id="chkmaterial"></td></tr>';
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetMaterialList",
        data: JSON.stringify({ EstimateID: parseInt(selectedValue) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                MaterialList = result.d;
                if (MaterialList.length > 0) {
                    $("#tbody1").append(rowTemplate);
                }
                var count = 1;
                for (var i = 0; i < MaterialList.length; i++) {

                    var $newRow = $("#rowTemplate1").clone();
                    $newRow.find("#srno").html(count);
                    $newRow.find("#materialname").html(MaterialList[i].Material.MatName);
                    if (IsMaterialAlreadySelected(MaterialList[i].Sno)) {
                        $newRow.find("#chkmaterial").html("<table><tr><td style='float: right; width :150px;'><input type='checkbox' onchange = 'RemoveItem(this)' checked='true' id='chkItem_" + i + "' name='chkItem_" + i + "' EMRID='" + MaterialList[i].Sno + "' style' width: 16px;height: 16px;'></td></tr></table>");
                    }
                    else {
                        $newRow.find("#chkmaterial").html("<table><tr><td style='float: right; width :150px;'><input type='checkbox' onchange='RemoveItem(this)' id='chkItem_" + i + "' name='chkItem_" + i + "' EMRID='" + MaterialList[i].Sno + "' style' width: 16px;height: 16px;'></td></tr></table>");
                    }
                    count++;
                    $newRow.show();
                    if (i == 0) {
                        $("#rowTemplate1").replaceWith($newRow);
                    }
                    else {
                        $newRow.appendTo("#tblUploadMaterial > tbody");
                    }

                }
                tblUploadMaterial = $('#tblUploadMaterial').DataTable();
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function LoadPurchaseOrderInfo(selectedMaterialList) {

    /*create/distroy grid for the new search*/
    if (typeof grdTicketDiscription != 'undefined') {
        grdTicketDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="srno">abc</td><td id="qty">abc</td><td id="description">abc</td><td id="details">cc</td><td id="unitprice"></td><td id="linetotal"></td></tr>';

    var adminLoanList = selectedMaterialList;
    if (adminLoanList.length > 0) {

        $("#tbody").append(rowTemplate);
    }
    var visitorType = "";
    var count = 1;
    for (var i = 0; i < adminLoanList.length; i++) {
        var className = "info";
        if (i % 2 == 0) {
            className = "warning";
        }
        var $newRow = $("#rowTemplate").clone();
        $newRow.find("#srno").html(count);
        $newRow.find("#qty").html("<table><tr><td><label id='MatQty'>" + adminLoanList[i].Qty + "</label></td></tr></table>");
        $newRow.find("#description").html("<table><tr><td><textarea name='txtdescription" + i + "'  id='txtdescription" + i + "' ></textarea></td></tr></table>");
        $newRow.find("#details").html(adminLoanList[i].Material.MatName);
        $newRow.find("#unitprice").html(adminLoanList[i].Rate);
        var linetotal = adminLoanList[i].Qty * adminLoanList[i].Rate;
        $newRow.find("#linetotal").html("<input type='hidden' value='" + linetotal + "' id='txtlineTotal" + i + "' />" + linetotal);
        count++;
        sum += linetotal;

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

    grdTicketDiscription = $('#grid').DataTable(
        {
            "bPaginate": true,
            "iDisplayLength": 12,
            "sPaginationType": "full_numbers",
            "aaSorting": [[2, 'desc']],
            "bAutoWidth": false,
            "bLengthChange": false,
            "bDestroy": true

        });
    TotalAmt();
}

function GetVendorAddress(selectedValue) {
    var cityname = "";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetVendorAddress",
        data: JSON.stringify({ VendorID: parseInt(selectedValue) }),
        dataType: "json",
        success: function (responce) {

            var msg = responce.d;
            var vendorinfo = msg[0].VendorName + "(" + msg[0].VendorContactNo + ")";
            $("span[id*='lblName']").text(vendorinfo);
            $("[id$='lblVendorAddress']").html(msg[0].VendorAddress);
            cityname = msg[0].VendorCity + "-" + msg[0].VendorZip + "," + msg[0].VendorState;
            $("[id$='lblCity']").html(cityname);

            $("input[id*='hdnVendorName']").val(vendorinfo);
            $("input[id*='hdnVendorAddress']").val(msg[0].VendorAddress);
            $("input[id*='hdnCity']").val(cityname);
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function BindCurrentDate() {
    var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
    var dNow = new Date();
    var curr_date = dNow.getDate();
    var curr_month = dNow.getMonth();
    var curr_year = dNow.getFullYear();
    var localdate = m_names[curr_month] + curr_date + "," + curr_year;
    $("[id$='lblCurrentDate']").html(localdate);
    $("input[id*='hdnCurrentDate']").val(localdate);
}

function BindDeliveryAddress() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetDeliveryAddressList",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("#drpDeliveryAddress").append($("<option></option>").val(value.ID).html(value.TrustName));
                    $("#drpBillingAddress").append($("<option></option>").val(value.ID).html(value.TrustName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetDeliveryAddressInfo(selectedValue) {
    var cityname = "";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetDeliveryAddressInfo",
        data: JSON.stringify({ AddressID: parseInt(selectedValue) }),
        dataType: "json",
        success: function (responce) {

            var msg = responce.d;
                $("[id$='lblTrustName']").html(msg[0].TrustName);
                cityname = msg[0].Address + "," + msg[0].City + "-" + "(" + msg[0].State + ")" + "," + msg[0].Zipcode;
                $("[id$='lblAdress']").html(cityname);
                $("[id$='lblPhoneNo']").html(msg[0].PhoneNumber);

                $("input[id*='hdnTrustName']").val(msg[0].TrustName);
                $("input[id*='hdnDeliveryAddress']").val(cityname);
                $("input[id*='hdnDeliveryPhoneNo']").val(msg[0].PhoneNumber);
            
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function BindPONumber() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetPONumber",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
               
              }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetBillingAddressInfo(selectedValue) {
    var cityname = "";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetDeliveryAddressInfo",
        data: JSON.stringify({ AddressID: parseInt(selectedValue) }),
        dataType: "json",
        success: function (responce) {

            var msg = responce.d;

           
                cityname = msg[0].Address;
                $("[id$='lblBillingName']").html(cityname);
                var fulladree = msg[0].City + "(" + msg[0].State + ")" + "," + msg[0].Zipcode;
                $("[id$='lblBillingAddres']").html(fulladree);
                $("[id$='lblBillingPhone']").html(msg[0].PhoneNumber);

                $("input[id*='hdnBillingName']").val(cityname);
                $("input[id*='hdnBillingAddres']").val(fulladree);
                $("input[id*='hdnBillingPhone']").val(msg[0].PhoneNumber);
          
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function TotalAmt() {
    var tablelength = $("#tbody").children('tr').length;
    var Amt = 0;
    var rate = 0;
    for (var i = 0 ; i < tablelength ; i++) {
        var qty = $("#txtlineTotal" + i).val();
        Amt += parseInt(qty);
    }
    $("[id$='lblSubTotal']").html(Amt);
    $("input[id*='hdnSubTotal']").val(Amt);
}














