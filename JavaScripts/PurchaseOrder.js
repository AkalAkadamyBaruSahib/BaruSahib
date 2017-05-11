
var materialname = new Array();
var tblUploadMaterial;
var SnoIds;
var MaterialList;
var selectedMaterialList = new Array();
var sum = 0;
var listVal = "";
var selectedEstList = new Array();

$(document).ready(function () {
   
    $("select").searchable();

    BindVendors();
    BindEstimate();
    BindCurrentDate();
    BindDeliveryAddress();
    //BindPONumber();
   
    $("[id$='lblIndentNo']").html($("select[id*='drpEstimate']").val());
    $("[id$='lblExciseStatus']").html('NA');
    $("[id$='lblVatStatus']").html('0%');
   
    $("#drpEstimate").change(function (e) {
        if ($(this).val() != 0) {
            $("#divUploadMaterial").modal('show');
            GetMaterialList($(this).val());

            if ($.inArray($(this).val(), $("input[id*='hdnIndentNo']").val().split('_')) == -1) {
                listVal += $(this).val() + "_";
                var EstVal = listVal.substr(0, listVal.length - 1);
                $("input[id*='hdnIndentNo']").val(EstVal);
            }
        }
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
      //  GetVendorAddress(SnoValue);
        if (selectedMaterialList.length > 0) {
            $("[id$='lblIndentNo']").html($("input[id*='hdnIndentNo']").val())
        }
        $("#divUploadMaterial").modal('hide');
    });
    $("input[id*='btntest']").click(function (e) {
        $("#divUploadMaterial").modal('show');
        GetMaterialList($("select[id*='drpEstimate']").val());
    });
    $("#chkExcise").change(function () {
        if (this.checked) {
            $("input[id*='txtExcise']").show();
            $("input[id*='txtExcise']").prop('disabled', false);
        }
        else {
            $("input[id*='txtExcise']").hide();
            $("input[id*='txtExcise']").prop('disabled', true);
        }
    });
  
    $("input[id*='txtExcise']").change(function () {
         Calcution();
    });

    $("input[id*='txtFrieght']").change(function () {
        Calcution()
    });


    $("input[id*='txtLoading']").change(function () {
        Calcution();
    });

    $("#drpVendor").change(function () {
        if ($(this).val() != 0) {
            GetVendorAddress($(this).val());
            $("input[id*='hdnVendorID']").val($(this).val());
        }
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
    var VatSum;
    if ($("#chkExcise").is(":checked")) {
        var Excisetotal = ($("[id$='lblSubTotal']").text()) * ($("input[id*='txtExcise']").val() / 100);
        var subtotal = $("[id$='lblSubTotal']").text();
        ExSum = parseInt(subtotal) + parseInt(Excisetotal);
        ExSum = ExSum.toFixed(2);
        VatSum = ExSum;
    }
    else {
        var subtotal = $("[id$='lblSubTotal']").text();
         VatSum = parseInt(subtotal);
    }

    if ($("input[id*='txtFrieght']").val() == "") {
        VatSum = parseFloat(VatSum) + parseFloat(0.00);
    }
    else {
        VatSum = parseFloat(VatSum) + parseFloat($("input[id*='txtFrieght']").val());
    }

    if ($("input[id*='txtLoading']").val() == "") {
        VatSum = parseFloat(VatSum) + parseFloat(0.00);
    }
    else {
        VatSum = parseFloat(VatSum) + parseFloat($("input[id*='txtLoading']").val());
    }


    $("[id$='lblGrandTotal']").html(VatSum);
    $("[id$='lblExciseStatus']").html('INCLUDED');
    $("input[id*='hdnGrandTotal']").val(VatSum);
    $("input[id*='hdnExciseStatus']").val('INCLUDED');
}

function RemoveItem(chkbox, sno) {
    var SnoIds = ($(chkbox).attr("emrid"));
    var MatID = ($(chkbox).attr("matid"));
    var Material = $.grep(MaterialList, function (e) { return e.Sno == SnoIds; })[0];
    if ($(chkbox).is(':checked')) {
        var mat = $.grep(selectedMaterialList, function (e) { return e.MatId == MatID; })[0];
        if (mat == undefined) {
            selectedMaterialList.push(Material);
        }
    }
    else {
        var selectedMaterialListNew = selectedMaterialList.filter(function (item) {
            return item.Sno != sno;
        });

        selectedMaterialList = selectedMaterialListNew;
      //  LoadWorkshopBillInfo(selectedMaterialList);
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
        data: JSON.stringify({ purchaseSourceID:2, inchargeID: 0 }),
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
                        $newRow.find("#chkmaterial").html("<input type='checkbox' onchange = 'RemoveItem($(this)," + MaterialList[i].Sno + "," + MaterialList[i].Material.MatId + ")' checked='true' id='chkItem_" + i + "' name='chkItem_" + i + "' EMRID='" + MaterialList[i].Sno + "' MATID='" + MaterialList[i].Material.MatId + "' />");
                    }
                    else {
                        $newRow.find("#chkmaterial").html("<input type='checkbox' onchange='RemoveItem($(this)," + MaterialList[i].Sno + "," + MaterialList[i].Material.MatId + ")' id='chkItem_" + i + "' name='chkItem_" + i + "' EMRID='" + MaterialList[i].Sno + "' MATID='" + MaterialList[i].Material.MatId + "'/>");
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
    var rowTemplate = '<tr id="rowTemplate"><td id="srno">abc</td><td style="width:360px" id="description">abc</td><td id="details">cc</td><td id="unit">cc</td><td id="qty">abc</td><td id="unitprice"></td><td style="width:100px"  id="vat"></td><td style="width:100px"  id="net"></td><td id="linetotal"></td></tr>';

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
         $newRow.find("#description").html("<textarea style='width:350px;height:50px' name='txtdescription" + i + "'  id='txtdescription" + i + "' ></textarea>");
        $newRow.find("#details").html(adminLoanList[i].Material.MatName);
        $newRow.find("#unit").html(adminLoanList[i].Unit.UnitName);
        $newRow.find("#qty").html("<input type='text'  id='txtQty" + i + "' name='txtQty" + i + "'  style='width:100px' onchange='Qty_ChangeEvent(" + i + "," + adminLoanList[i].Material.MatCost + ");' />");
        $newRow.find("#unitprice").html(adminLoanList[i].Material.MatCost);
        $newRow.find("#vat").html("<input type='text'  id='txtvat" + i + "' name='txtvat" + i + "' required style='width:90px' onchange='vat_ChangeEvent(" + i + "," + adminLoanList[i].Material.MatCost + ");'>");
        $newRow.find("#net").html("<span id='spnNetPrice" + i + "'>");
        $newRow.find("#linetotal").html("<span id='txtlineTotal" + i + "'><input type='text' id='hdnLineTotal" + i + "'  style='display:none;'");
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
            "bPaginate": false,
            "bDestroy": true,
            "bFilter": false,
            "bInfo": false,
        });

}

function GetVendorAddress(selectedValue) {
    var cityname = "";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetVendorAddress",
        data: JSON.stringify({ vendorID: parseInt(selectedValue) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var adminLoanList = result.d;
                if (adminLoanList.length > 0) {

                    for (var i = 0; i < adminLoanList.length; i++) {
                        var vendorinfo = adminLoanList[i].VendorName + "(" + adminLoanList[i].VendorContactNo + ")";
                        $("span[id*='lblName']").text(vendorinfo.toUpperCase());
                        $("[id$='lblVendorAddress']").html(adminLoanList[i].VendorAddress.toUpperCase());
                        $("input[id*='hdnVendorName']").val(vendorinfo);
                        $("input[id*='hdnVendorAddress']").val(adminLoanList[i].VendorAddress);

                    }
                }
            }
         //  $("input[id*='hdnCity']").val(cityname);
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
            if (msg.length > 0) {
                if ($("select[id*='drpPOFor']").val() == "1") {
                    $("[id$='lblTrustName']").html("THE KALGIDHAR SOCIETY,<br/>" + msg[0].TrustName);
                }
                else {
                    $("[id$='lblTrustName']").html("THE KALGIDHAR TRUST,<br/>" + msg[0].TrustName);
                }
                cityname = msg[0].Address;
                $("[id$='lblAdress']").html(cityname);
                $("input[id*='hdnTrustName']").val(msg[0].TrustName);
                $("input[id*='hdnDeliveryAddress']").val(cityname);
              
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function GetBillingAddressInfo(selectedValue) {
    var cityname = "";
    var name = "";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetDeliveryAddressInfo",
        data: JSON.stringify({ AddressID: parseInt(selectedValue) }),
        dataType: "json",
        success: function (responce) {

            var msg = responce.d;

            if (msg.length > 0) {
                name = msg[0].TrustName;
                cityname =  msg[0].Address;
                if ($("select[id*='drpPOFor']").val() == "1") {
                   $("[id$='lblBillingName']").html("THE KALGIDHAR SOCIETY,<br/>" + name);
                }
                else {
                    $("[id$='lblBillingName']").html("THE KALGIDHAR TRUST,<br/>" + name);
                }
                $("[id$='lblBillingAddres']").html(cityname);
                $("input[id*='hdnBillingName']").val(name);
                $("input[id*='hdnBillingAddres']").val(cityname);
            }
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
        if ($("#txtlineTotal" + i).text() == "") {
            $("#txtlineTotal" + i).text(0);
        }
        var qty = parseFloat($("#txtlineTotal" + i).text());
       
        Amt += parseFloat(qty);
    }
    Amt = Amt.toFixed(2);
    if ($("input[id*='txtFrieght']").val() == "") {
        var frieghtAmount = parseFloat(Amt) + parseFloat(0.00);
    }
    else {
        var frieghtAmount = parseFloat(Amt) + parseFloat($("input[id*='txtFrieght']").val());
    }
    if ($("input[id*='txtLoading']").val() == "") {
        var loadingAmount = parseFloat(frieghtAmount) + parseFloat(0.00);
    }
    else {
        var loadingAmount = parseFloat(frieghtAmount) + parseFloat($("input[id*='txtLoading']").val());
    }
    var totalAmount = loadingAmount.toFixed(2);
    $("[id$='lblSubTotal']").html(Amt);
    $("input[id*='hdnSubTotal']").val(Amt);
    $("[id$='lblGrandTotal']").html(totalAmount);
    $("[id$='lblExciseStatus']").html('INCLUDED');
    $("input[id*='hdnGrandTotal']").val(totalAmount);
    $("input[id*='hdnExciseStatus']").val('INCLUDED');
}

function Qty_ChangeEvent(cntID, matcost) {
    var qty = $("#txtQty" + cntID).val();
    var amount = parseFloat(qty) * parseFloat(matcost);
    amount = amount.toFixed(2);
    $("#txtlineTotal" + cntID).text(amount);
    $("#hdnLineTotal" + cntID).val(amount);
    TotalAmt();
}

function vat_ChangeEvent(cntID,matcost) {
    var qty = $("#txtQty" + cntID).val();
    var vat = $("#txtvat" + cntID).val();
    var amount = parseFloat(qty) * parseFloat(matcost);
    if (vat == "0") {
        amount = amount.toFixed(2);
        $("#txtlineTotal" + cntID).text(amount);
        $("#hdnLineTotal" + cntID).val(amount);
        $("#spnNetPrice" + cntID).text(matcost);
    }
    else {
        var netamount = parseFloat(matcost) * vat / 100;
        var totalnetamount = parseFloat(matcost) + netamount;
        var vatamount = amount * vat / 100;
        var totalamount = amount + vatamount;
        totalamount = totalamount.toFixed(2);
        totalnetamount = totalnetamount.toFixed(2);
        $("#txtlineTotal" + cntID).text(totalamount);
        $("#hdnLineTotal" + cntID).val(totalamount);
        $("#spnNetPrice" + cntID).text(totalnetamount);
    }
    TotalAmt();
}













