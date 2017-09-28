
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
    BindPONumber();

    $("[id$='lblIndentNo']").html($("select[id*='drpEstimate']").val());
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
        $("input[id*='hdnDeliveryID']").val($(this).val());
    });
  $("#drpBillingAddress").change(function () {
      GetBillingAddressInfo($(this).val());
      $("input[id*='hdnBillingID']").val($(this).val());
    });
   $("#drpFreight").change(function () {
        var  FreightVal = $(this).val();
        $("[id$='lblFreight']").html(FreightVal);
        $("input[id*='hdnFreight']").val(FreightVal);
    });

    $("#drpPOBucket").change(function (e) {
        UpdatePurchaseOrderInfo($(this).val());
        $("#trPonum").show();
    });


});


function Calcution() {
    var subtotal = $("[id$='lblSubTotal']").text();
    VatSum = parseInt(subtotal);

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
    $("input[id*='hdnGrandTotal']").val(VatSum);
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
        data: JSON.stringify({ purchaseSourceID: 2, inchargeID: 0 }),
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
        data: JSON.stringify({ EstimateID: parseInt(selectedValue),PSID:parseInt(2)}),
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
    var rowTemplate = '<tr id="rowTemplate"><td id="srno">abc</td><td style="width:360px" id="description">abc</td><td id="details">cc</td><td id="unit">cc</td><td id="qty">abc</td><td id="unitprice"></td><td style="width:50px"  id="vat"></td><td style="width:100px"  id="net"></td><td id="linetotal"></td></tr>';

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
        $newRow.find("#srno").html("<input type='text'  id='txtSno" + i + "' name='txtSno" + i + "' value='" + adminLoanList[i].Sno + "'  style='width:100px;display:none;');' />" + count);
        $newRow.find("#description").html("<textarea style='width:350px;height:50px' name='txtdescription" + i + "'  id='txtdescription" + i + "' ></textarea>");
        $newRow.find("#details").html("<input type='text'  id='txtMatName" + i + "' name='txtMatName" + i + "' value='" + adminLoanList[i].Material.MatName + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.MatName);
        $newRow.find("#unit").html("<input type='text'  id='txtUnitName" + i + "' name='txtUnitName" + i + "' value='" + adminLoanList[i].Unit.UnitName + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Unit.UnitName);
        $newRow.find("#qty").html("<input type='text'  id='txtQty" + i + "' name='txtQty" + i + "'  style='width:50px' onchange='Qty_ChangeEvent(" + i + "," + adminLoanList[i].Material.MatCost + "," + adminLoanList[i].Material.GST + ",0);' /><input type='text'  id='txtMatID" + i + "' name='txtMatID" + i + "' value='" + adminLoanList[i].Material.MatId + "'  style='width:100px;display:none;');' />");
        if (adminLoanList[i].Material.GST == 5 || adminLoanList[i].Material.GST == 12 || adminLoanList[i].Material.GST == 18 || adminLoanList[i].Material.GST == 28) {
            $newRow.find("#unitprice").html("<input type='text'  id='txtMatCost" + i + "' name='txtMatCost" + i + "' value='" + adminLoanList[i].Material.MatCost + "'  style='width:100px;display:none;');' /><table><tr><td>MRP: <input type='text'  id='txtMRP" + i + "' name='txtMRP" + i + "' value='" + adminLoanList[i].Material.MRP + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.MRP + " </td></tr><tr><td>Discount:<input type='text'  id='txtDiscount" + i + "' name='txtDiscount" + i + "' value='" + adminLoanList[i].Material.Discount + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.Discount + "</td></tr><tr><td>Additional Discount:<input type='text'  id='txtAdditionalDiscount" + i + "' name='txtAdditionalDiscount" + i + "' value='" + adminLoanList[i].Material.AdditionalDiscount + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.AdditionalDiscount + "</td></tr><tr><td>GST:" + adminLoanList[i].Material.GST + "</td></tr></table>");
            $newRow.find("#vat").html("<input type='text'  id='txtvat" + i + "' disabled='disabled' name='txtvat" + i + "' value='" + adminLoanList[i].Material.GST + "' required style='width:90px'>");
            $newRow.find("#net").html("<span id='spnNetPrice" + i + "'>" + adminLoanList[i].Material.MatCost);
        }
        else {
            $newRow.find("#unitprice").html("<input type='text'  id='txtMatCost" + i + "' name='txtMatCost" + i + "' value='" + adminLoanList[i].Material.MatCost + "'  style='width:100px;display:none;');' /><table><tr><td>MRP: <input type='text'  id='txtMRP" + i + "' name='txtMRP" + i + "' value='" + adminLoanList[i].MRP + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.MRP + " </td></tr><tr><td>Discount:<input type='text'  id='txtDiscount" + i + "' name='txtDiscount" + i + "' value='" + adminLoanList[i].Material.Discount + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.Discount + "</td></tr><tr><td>Additional Discount:<input type='text'  id='txtAdditionalDiscount" + i + "' name='txtAdditionalDiscount" + i + "' value='" + adminLoanList[i].Material.AdditionalDiscount + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.AdditionalDiscount + "</td></tr><tr><td>GST:" + adminLoanList[i].Material.GST + "</td></tr></table>");
            $newRow.find("#vat").html("<select id='drpGst" + i + "' name='drpGst" + i + "' required onchange='ddlGST_ChangeEvent(" + i + "," + adminLoanList[i].Material.MatCost + ");' style='width:120px;'><option value='-1'>-Select GST-</option><option value='0'>0%</option><option value='5'>5%</option> <option value='12'>12%</option><option value='18'>18%</option><option value='28'>28%</option></select>");
            $newRow.find("#net").html("<span id='spnNetPrice" + i + "'>");
        }

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
                        var vendorinfo = adminLoanList[i].VendorName;
                        $("span[id*='lblName']").text(vendorinfo.toUpperCase());
                        $("[id$='lblVendorAddress']").html(adminLoanList[i].VendorAddress.toUpperCase());
                        $("input[id*='hdnVendorName']").val(vendorinfo);
                        $("input[id*='hdnVendorAddress']").val(adminLoanList[i].VendorAddress);
                        $("input[id*='hdnVendorContactNo']").val(adminLoanList[i].VendorContactNo);
                        $("[id$='lblVendorPhone']").html(adminLoanList[i].VendorContactNo);

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
                if (selectedValue == 119) {
                    if ($("select[id*='drpPOFor']").val() == "1") {
                        $("[id$='lblTrustName']").html("THE KALGIDHAR SOCIETY,<br/>");
                    }
                    else if ($("select[id*='drpPOFor']").val() == "2") {
                        $("[id$='lblTrustName']").html("THE KALGIDHAR TRUST,<br/>");
                    }
                    else {
                        $("[id$='lblTrustName']").html("GURUDWARA BARU SAHIB,<br/>");
                    }
                }
                else {
                    if ($("select[id*='drpPOFor']").val() == "1") {
                        $("[id$='lblTrustName']").html("THE KALGIDHAR SOCIETY,<br/>" + msg[0].TrustName);
                    }
                    else if ($("select[id*='drpPOFor']").val() == "2") {
                        $("[id$='lblTrustName']").html("THE KALGIDHAR TRUST,<br/>" + msg[0].TrustName);
                    }
                    else {
                        $("[id$='lblTrustName']").html("GURUDWARA BARU SAHIB,<br/>" + msg[0].TrustName);
                    }
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
                cityname = msg[0].Address;
                if (selectedValue == 119) {
                    if ($("select[id*='drpPOFor']").val() == "1") {
                        $("[id$='lblBillingName']").html("THE KALGIDHAR SOCIETY,<br/>");
                    }
                    else if ($("select[id*='drpPOFor']").val() == "2") {
                        $("[id$='lblBillingName']").html("THE KALGIDHAR TRUST,<br/>");
                    }
                    else {
                        $("[id$='lblBillingName']").html("GURUDWARA BARU SAHIB,<br/>");
                    }
                }
                else {
                    if ($("select[id*='drpPOFor']").val() == "1") {
                        $("[id$='lblBillingName']").html("THE KALGIDHAR SOCIETY,<br/>" + name);
                    }
                    else if ($("select[id*='drpPOFor']").val() == "2") {
                        $("[id$='lblBillingName']").html("THE KALGIDHAR TRUST,<br/>" + name);
                    }
                    else {
                        $("[id$='lblBillingName']").html("GURUDWARA BARU SAHIB,<br/>" + name);
                    }
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

function Qty_ChangeEvent(cntID, matcost, gst,updategst) {
    
    var qty = $("#txtQty" + cntID).val();
    var amount;
    if (gst == 0)
    {
        var gstamount = (parseFloat(matcost) * parseFloat(updategst)) / 100;
        gstamount = parseFloat(matcost) + parseFloat(gstamount);
        amount = parseFloat(qty) * parseFloat(gstamount);
        amount = amount.toFixed(2);
        $("#txtlineTotal" + cntID).text(amount);
        $("#hdnLineTotal" + cntID).val(amount);
        TotalAmt();
    }
    else
    {
        amount = parseFloat(qty) * parseFloat(matcost);
        amount = amount.toFixed(2);
        $("#txtlineTotal" + cntID).text(amount);
        $("#hdnLineTotal" + cntID).val(amount);
        TotalAmt();
    }
}
function ddlGST_ChangeEvent(cntID, matcost) {
    var qty = $("#txtQty" + cntID).val();
    var vat = $("#drpGst" + cntID).val();
    var amount = parseFloat(qty) * parseFloat(matcost);
    ////if (vat == "0") {
    //    amount = amount.toFixed(2);
    //    $("#txtlineTotal" + cntID).text(amount);
    //    $("#hdnLineTotal" + cntID).val(amount);
    //    $("#spnNetPrice" + cntID).text(matcost);
    ////}
    //else {
    var netamount = parseFloat(matcost) * vat / 100;
    var totalnetamount = parseFloat(matcost) + netamount;
    var vatamount = amount * vat / 100;
    var totalamount = amount + vatamount;
    totalamount = totalamount.toFixed(2);
    totalnetamount = totalnetamount.toFixed(2);
    $("#txtlineTotal" + cntID).text(totalamount);
    $("#hdnLineTotal" + cntID).val(totalamount);
    $("#spnNetPrice" + cntID).text(totalnetamount);
    //}
    TotalAmt();
}

function BindPONumber() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetPONumberList",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("#drpPOBucket").append($("<option></option>").val(value.ID).html(value.PurchaseOrderNumber));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function UpdatePurchaseOrderInfo(purchaseorderno) {

    /*create/distroy grid for the new search*/
    if (typeof grdTicketDiscription != 'undefined') {
        grdTicketDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="srno">abc</td><td style="width:360px" id="description">abc</td><td id="details">cc</td><td id="unit">cc</td><td id="qty">abc</td><td id="unitprice"></td><td style="width:100px"  id="vat"></td><td style="width:100px"  id="net"></td><td id="linetotal"></td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetPurchaserOrderDetailByPONumber",
        data: JSON.stringify({ purchaserOrderID: parseInt(purchaseorderno) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var adminLoanList = result.d;
                $("#drpVendor").val(adminLoanList[0].VendorID);
                $("input[id*='hdnVendorID']").val(adminLoanList[0].VendorID);
                $("input[id*='txtPO']").val($("#drpPOBucket option:selected").text());
                $("input[id*='hdnPoID']").val($("#drpPOBucket").val()); 
                $("input[id*='hdnUpdatePoID']").val($("#drpPOBucket option:selected").text());
                GetVendorAddress(adminLoanList[0].VendorID);
                if (adminLoanList.length > 0) {
                    $("#tbody").append(rowTemplate);
                }
                $("input[id*='hdnItemsLength']").val("");
                var SnoValue = "";
                var count = 1;
                for (var i = 0; i < adminLoanList.length; i++) {
                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }
                    GetUpdateMaterialList(adminLoanList[i].SnoID)
                    var $newRow = $("#rowTemplate").clone();
                    $newRow.find("#srno").html(count);
                    $newRow.find("#description").html("<textarea style='width:350px;height:50px' name='txtdescription" + i + "'  id='txtdescription" + i + "' >" + adminLoanList[i].Description + "</textarea>");
                    $newRow.find("#details").html("<input type='text'  id='txtMatName" + i + "' name='txtMatName" + i + "' value='" + adminLoanList[i].Material.MatName + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.MatName);
                    $newRow.find("#unit").html("<input type='text'  id='txtUnitName" + i + "' name='txtUnitName" + i + "' value='" + adminLoanList[i].UnitName + "'  style='width:100px;display:none;');' />" + adminLoanList[i].UnitName);
                    $newRow.find("#qty").html("<input type='text'  id='txtQty" + i + "' name='txtQty" + i + "'  style='width:100px' value='" + adminLoanList[i].Qty + "' onchange='Qty_ChangeEvent(" + i + "," + adminLoanList[i].Material.MatCost + "," + adminLoanList[i].Material.GST + "," + adminLoanList[i].GST + ");' /><input type='text'  id='txtMatID" + i + "' name='txtMatID" + i + "' value='" + adminLoanList[i].Material.MatId + "'  style='width:100px;display:none;');' />");
                    if (adminLoanList[i].GST == 5 || adminLoanList[i].GST == 12 || adminLoanList[i].GST == 18 || adminLoanList[i].GST == 28) {
                        if(adminLoanList[i].Material.Discount==0 && adminLoanList[i].Material.AdditionalDiscount==0 &&  adminLoanList[i].Material.MRP==0)
                        {
                            $newRow.find("#unitprice").html("<input type='text'  id='txtMatCost" + i + "' name='txtMatCost" + i + "' value='" + adminLoanList[i].Material.MatCost + "'  style='width:100px;display:none;');' /><table><tr><td>MRP: <input type='text'  id='txtMRP" + i + "' name='txtMRP" + i + "' value='" + adminLoanList[i].Material.MatCost + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.MatCost + " </td></tr><tr><td>Discount:<input type='text'  id='txtDiscount" + i + "' name='txtDiscount" + i + "' value='" + adminLoanList[i].Material.Discount + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.Discount + "</td></tr><tr><td>Additional Discount:<input type='text'  id='txtAdditionalDiscount" + i + "' name='txtAdditionalDiscount" + i + "' value='" + adminLoanList[i].Material.AdditionalDiscount + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.AdditionalDiscount + "</td></tr><tr><td>GST:" + adminLoanList[i].GST + "</td></tr></table>");
                        }
                        else
                        {
                            $newRow.find("#unitprice").html("<input type='text'  id='txtMatCost" + i + "' name='txtMatCost" + i + "' value='" + adminLoanList[i].Material.MatCost + "'  style='width:100px;display:none;');' /><table><tr><td>MRP: <input type='text'  id='txtMRP" + i + "' name='txtMRP" + i + "' value='" + adminLoanList[i].Material.MRP + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.MRP + " </td></tr><tr><td>Discount:<input type='text'  id='txtDiscount" + i + "' name='txtDiscount" + i + "' value='" + adminLoanList[i].Material.Discount + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.Discount + "</td></tr><tr><td>Additional Discount:<input type='text'  id='txtAdditionalDiscount" + i + "' name='txtAdditionalDiscount" + i + "' value='" + adminLoanList[i].Material.AdditionalDiscount + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.AdditionalDiscount + "</td></tr><tr><td>GST:" + adminLoanList[i].GST + "</td></tr></table>");
                   
                        }
                        $newRow.find("#vat").html("<input type='text'  id='txtvat" + i + "' disabled='disabled' name='txtvat" + i + "' value='" + adminLoanList[i].GST + "' required style='width:90px'>");
                    }
                    else {
                        if (adminLoanList[i].Material.Discount == 0 && adminLoanList[i].Material.AdditionalDiscount == 0 && adminLoanList[i].Material.MRP == 0) {
                            $newRow.find("#unitprice").html("<input type='text'  id='txtMatCost" + i + "' name='txtMatCost" + i + "' value='" + adminLoanList[i].Material.MatCost + "'  style='width:100px;display:none;');' /><table><tr><td>MRP: <input type='text'  id='txtMRP" + i + "' name='txtMRP" + i + "' value='" + adminLoanList[i].Material.MatCost + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.MatCost + " </td></tr><tr><td>Discount:<input type='text'  id='txtDiscount" + i + "' name='txtDiscount" + i + "' value='" + adminLoanList[i].Material.Discount + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.Discount + "</td></tr><tr><td>Additional Discount:<input type='text'  id='txtAdditionalDiscount" + i + "' name='txtAdditionalDiscount" + i + "' value='" + adminLoanList[i].Material.AdditionalDiscount + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.AdditionalDiscount + "</td></tr><tr><td>GST:" + adminLoanList[i].Material.GST + "</td></tr></table>");
                        }
                        else {
                            $newRow.find("#unitprice").html("<input type='text'  id='txtMatCost" + i + "' name='txtMatCost" + i + "' value='" + adminLoanList[i].Material.MatCost + "'  style='width:100px;display:none;');' /><table><tr><td>MRP: <input type='text'  id='txtMRP" + i + "' name='txtMRP" + i + "' value='" + adminLoanList[i].Material.MatCost + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.MRP + " </td></tr><tr><td>Discount:<input type='text'  id='txtDiscount" + i + "' name='txtDiscount" + i + "' value='" + adminLoanList[i].Material.Discount + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.Discount + "</td></tr><tr><td>Additional Discount:<input type='text'  id='txtAdditionalDiscount" + i + "' name='txtAdditionalDiscount" + i + "' value='" + adminLoanList[i].Material.AdditionalDiscount + "'  style='width:100px;display:none;');' />" + adminLoanList[i].Material.AdditionalDiscount + "</td></tr><tr><td>GST:" + adminLoanList[i].Material.GST + "</td></tr></table>");

                        }
                        $newRow.find("#vat").html("<select id='drpGst" + i + "' name='drpGst" + i + "' required onchange='ddlGST_ChangeEvent(" + i + "," + adminLoanList[i].Material.MatCost + ");' style='width:120px;'><option value='-1'>-Select GST-</option><option value='0'>0%</option><option value='5'>5%</option> <option value='12'>12%</option><option value='18'>18%</option><option value='28'>28%</option></select>");
                    }

                    var amount = parseFloat(adminLoanList[i].Qty) * parseFloat(adminLoanList[i].Material.MatCost);
                    if (adminLoanList[i].GST == "0") {
                        if (adminLoanList[i].Material.MRP == "0") {
                            amount = amount.toFixed(2);
                            $newRow.find("#net").html("<span id='spnNetPrice" + i + "'>" + adminLoanList[i].Material.MatCost + "</span>");
                            $newRow.find("#linetotal").html("<span id='txtlineTotal" + i + "'>" + amount + "</span><input type='text' id='hdnLineTotal" + i + "'  style='display:none;'");
                        }
                        else {
                            var netamount = parseFloat(adminLoanList[i].Material.MRP) * adminLoanList[i].Material.Discount / 100;
                            netamount = parseFloat(adminLoanList[i].Material.MRP) - parseFloat(netamount);
                            var netAddDiscount = parseFloat(netamount) * adminLoanList[i].Material.AdditionalDiscount / 100;
                            netAddDiscount = parseFloat(netamount) - parseFloat(netAddDiscount);
                            var vatamount = netAddDiscount * adminLoanList[i].GST / 100;
                            var totalnetamount = parseFloat(netAddDiscount) + parseFloat(vatamount);
                            amount = amount + vatamount;
                            amount = amount.toFixed(2);
                            totalnetamount = totalnetamount.toFixed(2);
                            $newRow.find("#net").html("<span id='spnNetPrice" + i + "'>" + totalnetamount + "</span>");
                            $newRow.find("#linetotal").html("<span id='txtlineTotal" + i + "'>" + amount + "</span><input type='text' id='hdnLineTotal" + i + "'  style='display:none;'");
                        }
                    }
                    else {
                        var netamount = parseFloat(adminLoanList[i].Material.MRP) * adminLoanList[i].Material.Discount / 100;
                        netamount = parseFloat(adminLoanList[i].Material.MRP) - parseFloat(netamount);
                        var netAddDiscount = parseFloat(netamount) * adminLoanList[i].Material.AdditionalDiscount / 100;
                        netAddDiscount = parseFloat(netamount) - parseFloat(netAddDiscount);
                        var vatamount = netAddDiscount * adminLoanList[i].GST / 100;
                        var totalnetamount = parseFloat(netAddDiscount) + parseFloat(vatamount);
                        amount = parseFloat(totalnetamount) * parseFloat(adminLoanList[i].Qty);
                        totalnetamount = totalnetamount.toFixed(2);
                        $newRow.find("#net").html("<span id='spnNetPrice" + i + "'>" + totalnetamount + "</span>");
                        $newRow.find("#linetotal").html("<span id='txtlineTotal" + i + "'>" + amount.toFixed(2) + "</span><input type='text' id='hdnLineTotal" + i + "'  style='display:none;'");
                    }
                    sum += parseFloat(amount);
                    $("[id$='lblSubTotal']").html(sum);
                    $("input[id*='hdnSubTotal']").val(sum);
                    $("[id$='lblGrandTotal']").html(sum);
                    $("input[id*='hdnGrandTotal']").val(sum);
                    count++;
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

        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}


function GetUpdateMaterialList(selectedValue) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetMaterialDetailList",
        data: JSON.stringify({ sno: parseInt(selectedValue) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                MaterialList = result.d;
                for (var i = 0; i < MaterialList.length; i++) {
                    selectedMaterialList.push(MaterialList[i]);
                }
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}











