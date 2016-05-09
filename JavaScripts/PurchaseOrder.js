
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
    $("#lblIndentNo").html($("select[id*='drpEstimate']").val());
    $("#lblExciseStatus").html('NA');
    $("#lblVatStatus").html('0%');
    $("#drpEstimate").change(function (e) {
        $("#divUploadMaterial").modal('show');
        GetMaterialList($(this).val());
        listVal += $(this).val() + ",";
        $("#lblIndentNo").text(listVal);
    });
    $("input[id*='btnLoad']").click(function (e) {
        LoadPurchaseOrderInfo(selectedMaterialList);
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
    $("input[id*='btnExcise']").click(function (e) {
        Calcution();
    });
    $("#txtvat").change(function () {
        var vatValue = $("#txtvat").val() + "%";
        $("#lblVatStatus").html(vatValue);
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

 
   
});

function Calcution() {
    if ($("#chkExcise").is(":checked")) {
        var Excisetotal = ($("#subtotal").text()) * ($("#txtExcise").val() / 100);
        var subtotal = $("#subtotal").text();
        var ExSum = parseInt(subtotal) + parseInt(Excisetotal);
        var VatTotal = ExSum * $("#txtvat").val() / 100;
        var VatSum = VatTotal + ExSum;
    }
    else {
        var VatTotal = $("#subtotal").text() * $("#txtvat").val() / 100;
        var subtotal = $("#subtotal").text();
        var VatSum = parseInt(subtotal) + parseInt(VatTotal);
    }
    $("#lblGrandTotal").html(VatSum);
    $("#lblExciseStatus").html('INCLUDED');

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
        $newRow.find("#qty").html(adminLoanList[i].Qty);
        $newRow.find("#description").html("<table><tr><td><textarea name='txtdescriptipn' id='txtdescriptipn' ></textarea></td></tr></table>");
        $newRow.find("#details").html(adminLoanList[i].Material.MatName);
        $newRow.find("#unitprice").html(adminLoanList[i].Rate);
        var linetotal = adminLoanList[i].Qty * adminLoanList[i].Rate;
        $newRow.find("#linetotal").html(linetotal);
        count++;
        sum += linetotal;
        $("#subtotal").html(sum);
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

            for (var i = 0; i < msg.length; i++) {
                var vendorinfo = msg[i].VendorName + "(" + msg[i].VendorContactNo + ")";
                $("#lblName").text(vendorinfo);
                $("#lblVendorAddress").text(msg[i].VendorAddress);
                cityname = msg[i].VendorCity + "-" + msg[i].VendorZip + "," + msg[i].VendorState;
                $("#lblCity").text(cityname);
            }
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
    $('#lblCurrentDate').text(localdate);
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

            for (var i = 0; i < msg.length; i++) {
                $("#lblTrustName").text(msg[i].TrustName);
                cityname = msg[i].Address + "," + msg[i].City + "-" + "(" + msg[i].State + ")" + "," + msg[i].Zipcode;
                $("#lblAdress").text(cityname);
                $("#lblPhoneNo").text(PhoneNumber);
            }
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

            for (var i = 0; i < msg.length; i++) {
                cityname = msg[i].Address;
                var fulladree = msg[i].City  + "(" + msg[i].State + ")" + "," + msg[i].Zipcode;
                $("#lblBillingName").text(cityname);
                $("#lblBillingAddres").text(fulladree);
                $("#lblBillingPhone").text(PhoneNumber);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}












