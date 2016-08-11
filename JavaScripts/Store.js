var cntB = 2;
var grdBills;
var d = new Date();
var strDate = d.getDate() + "/" + (d.getMonth() + 1) + "/" +  d.getFullYear() ;
$(document).ready(function () {
    $("#btnUploadSave").click(function () {
        SaveStoreBill();
    });

    //$("#btncloase").click(function () {
    //    $("#tblUploadBill").html('');
    //});
    $("input[id*='hdnVendorID']").val();

    $("#ddlVendorName").change(function (e) {
        $("input[id*='hdnVendorID']").val($(this).val());
    });

    $("input[id*='hdnBillNo']").val();

    $("#ddlLinkBillNo").change(function (e) {
        var option = $("#ddlLinkBillNo :selected").text();
       $("input[id*='hdnBillNo']").val(option);
  
    });

    $("input[id*='btnReceivedMaterial']").click(function (e) {
        ReceivedMaterialAndValidation();
        
    });

    $("input[id*='btnDispatchMaterial']").click(function (e) {
        DispatchMaterialAndValidation();
        
    });
   
});
function addNewBill() {
    $('#tblUploadBill tr').last().after('<tr id="trB' + cntB + '"><td><input type="text" style=" width: 143px;height: 18px;" id="txtBillNo' + cntB + '" name="txtBillNo' + cntB + '" value=""></td><td><input type="text" style=" width: 143px;height: 18px;"  id="txtBillName' + cntB + '" name="txtBillName' + cntB + '" value=""></td><td><input id="uploadePurchaseFile' + cntB + '" type="file"  name="uploadePurchaseFile' + cntB + '"/></td><td><a href="javascript:void(0);" onclick="addNewBill(' + cntB + ');"><input id="btnadd" class="btn btn-primary" value="+"  style="width: 10px"/></a></td><td><a href="javascript:void(0);" onclick="removeRow(' + cntB + ');"><input id="btnremove" class="btn btn-primary" value="-" style="width:10px"/></a></td></tr>');
    cntB++;
}

function OpenReceivedMaterial(EMRId, qty, MatID, EstID) {
    LoadVendors(MatID);
    $("input[id*='hdnIsReceived']").val(1);
    $("input[id*='hdnEMRId']").val(EMRId);
    $("input[id*='txtReceivedQty']").val(qty);
    $("input[id*='txtRate']").val("");
    $("input[id*='txtRate']").val("");
    $("#divIsReceived").modal('show');
    $("input[id*='hdnEstID']").val(EstID);
    LoadBill(EstID);
}

function OpenDispatchMaterial(EMRId) {
    $("input[id*='hdnIsReceived']").val(0);
    $("input[id*='txtLinkBillNo']").val(0);
    $("input[id*='hdnEMRId']").val(EMRId);
    $("input[id*='btnSave']").val('Dispatch');
    //$("select[id*='ddlVendorName']").val('NULL').change();
    $("#trvendorname").hide();
    $("#trupload").hide();
    $("#divIsReceived").modal('show');
}

function OpenUploadbill(EstID) {
    $("input[id*='hdnEstID']").val(EstID);
    $("#divUploadBill").modal('show');
}

function SaveStoreBill() {
    for (var i = 1; i < cntB; i++) {

        var paramBill = new Object();

        var StoreMaterialBill = new Object();

        var hdnEstId = $("input[id*='hdnEstID']").val();
        StoreMaterialBill.EstID = hdnEstId;
        StoreMaterialBill.BillName = $("input[id*='txtBillName" + i + "']").val();
        StoreMaterialBill.BillNo = $("input[id*='txtBillNo" + i + "']").val();
        StoreMaterialBill.BillPath = "Bills/" + $("#uploadePurchaseFile" + i)[0].files[0].name;


        paramBill.StoreMaterialBill = StoreMaterialBill;

        $.ajax({
            type: "POST",
            async: false,
            contentType: "application/json; charset=utf-8",
            url: "Services/StoreController.asmx/SaveStoreBill",
            data: JSON.stringify(paramBill),
            dataType: "json",
            success: function (result, textStatus) {
                if (textStatus == "success") {
                    StoreBillUpload(i);
                    // ClearTextBox();
                }
            },
            error: function (result, textStatus) {
                alert(result.responseText)
            }
        });

    }

}

function StoreBillUpload(cnt) {
    var files = $("#uploadePurchaseFile" + cnt)[0].files;
    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }

    $.ajax({
        url: "StoreBillHandler.ashx",
        type: "POST",
        async: false,
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            BindBillNoByEstID($("input[id*='hdnEstID']").val());
            $("#divUploadBill").modal('hide');
        },
        error: function (err) {
            alert(err.statusText)
        }
    });
}

function ClearTextBox() {
    $("input[id*='txtBillName']").empty();
    $("input[id*='txtBillNo']").empty();
    cntB = 2;
}

function removeRow(removeNum) {
    $('#trB' + removeNum).remove();
    cntB--;
}

function OpenViewbill(estID) {


    $("#spnEstID").text(estID);
    /*create/distroy grid for the new search*/
    if (typeof grdBills != 'undefined') {
        grdBills.fnClearTable();
    }

    var rowCount = $('#grdBills').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }

    var rowTemplate = '<tr id="rowTemplate"><td id="billName"></td><td id="view"></td><td id="delete"></td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/StoreController.asmx/GetBillDetails",
        data: JSON.stringify({ EstID: estID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var adminLoanList = result.d;
                if (adminLoanList.length > 0) {

                    $("#tbody").append(rowTemplate);
                }

                for (var i = 0; i < adminLoanList.length; i++) {

                    var $newRow = $("#rowTemplate").clone();
                    $newRow.find("#billName").html("<a target='_blank' href='" + adminLoanList[i].BillPath + "' >" + adminLoanList[i].BillName + "</a>");
                    $newRow.find("#view").html("<a target='_blank' href='" + adminLoanList[i].BillPath + "' >View</a>");
                    $newRow.find("#delete").html("<a href='#' onclick='MaterialBillToDelete(" + adminLoanList[i].ID + ")'>Delete</a>");
                    $newRow.show();
                    if (i == 0) {
                        $("#rowTemplate").replaceWith($newRow);
                    }
                    else {
                        $newRow.appendTo("#grdBills > tbody");
                    }

                }
                grdBills = $('#grdBills').DataTable();
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });

    $("#divViewbill").modal('show');
}

function LoadVendors(MatID) {
    
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/StoreController.asmx/GetVendorsNameList",
        data: JSON.stringify({ matID: MatID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                if (Result.length > 0) {
                    $("#ddlVendorName  option").each(function (index, option) {
                        $(option).remove();
                    });
                }
                $.each(Result, function (key, value) {
                    $("#ddlVendorName").append($("<option></option>").val(value.ID).html(value.VendorName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function MaterialBillToDelete(billID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/StoreController.asmx/StoreBillToDelete",
        data: JSON.stringify({ BillID: billID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                $("input[id*='hdnEstID']").val(result.d);
                OpenViewbill(result.d);
                alert("Bill Delete Successfully");
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function LoadBill(EstID) {

    $("#ddlLinkBillNo  option").each(function (index, option) {
        $(option).remove();
    });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/StoreController.asmx/GetMaterialBillList",
        data: JSON.stringify({ estID: EstID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $("#ddlLinkBillNo").append($("<option></option>").val("0").html("--Select Bill No--"));
                $.each(Result, function (key, value) {
                    $("#ddlLinkBillNo").append($("<option></option>").val(value.ID).html(value.BillNo));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}


function BindBillNoByEstID(EstID) {

    //$("#drpBillNo  option").each(function (index, option) {
    //    $(option).remove();
    //});

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/StoreController.asmx/GetMaterialBillList",
        data: JSON.stringify({ estID: EstID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                BindBillDropdown(Result);
                $("input[id*='hdnUloadBill']").val(result.d);

               
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindBillDropdown(Result) {
    var tablelength = $("#tblmatDetail > tbody").children('tr').length;
    var id = 0;
    for (var i = 0 ; i < tablelength; i++) {

        id = (i + 1);
        $("#drpBillNo" + i + "  option").each(function (index, option) {
            $(option).remove();
        });

        $("#drpBillNo" + i).append($("<option></option>").val("0").html("--Select Bill No--"));
        $.each(Result, function (key, value) {
            $("#drpBillNo" + i).append($("<option></option>").val(value.ID).html(value.BillNo));
        });
        $("#drpBillNo" + i).val($("#hdnBillNo" + i).val());
    }
}



function ReceivedMaterialAndValidation() {

    if ($("input[id*='hdnUloadBill']").val() == "" || $("input[id*='hdnUloadBill']").val() == "0") {
        alert("Please Upload the Bill");
        return false;
    }

    var hdnStoreQuantity = 0;
    var tablelength = $("#tblmatDetail > tbody").children('tr').length;
    for (var i = 0 ; i < (tablelength - 1) ; i++) {
        var txtstoreQty = parseInt($("#txtInStoreQuantity" + i).val());

        if ($("#hdnInStoreQuantity" + i).val() == undefined) {
            hdnStoreQuantity = 0;
        }
        else {
            hdnStoreQuantity = $("#hdnInStoreQuantity" + i).val();
        }

        var txtRemainstoreQty = parseInt($("#txtRemainingQty" + i).val());
        var hdnRemainingQty = parseInt($("#hdnRemainingQty" + i).val());
        var lblstoreQty = $("#lblInStoreQty" + i).val();

        var totalstoreQty = parseInt(hdnStoreQuantity) + parseInt(txtstoreQty);
        var totalRemaingQty = parseInt(hdnStoreQuantity) + parseInt(txtRemainstoreQty);
        var bal = parseInt(hdnRemainingQty) + parseInt(txtRemainstoreQty);
        var storebal = parseInt(lblstoreQty) + parseInt(txtstoreQty);
    

        if ($("#txtInStoreQuantity" + i).val() > 0) {

            if ($("#hdnPurchaseQty" + i).val() == "0.00") {
                alert("Can Not Received the Material Without Purchase");
                return false;
            }
            else if ($("#drpBillNo" + i).val() == undefined || $("#drpBillNo" + i).val() == "0") {
                alert("Please Select the Bill No");
                return false;
            }
            else if (totalstoreQty > parseInt($("#hdnPurchaseQty" + i).val()) || (totalRemaingQty) > parseInt($("#hdnPurchaseQty" + i).val()) || bal > parseInt($("#hdnPurchaseQty" + i).val()) || storebal > parseInt($("#hdnPurchaseQty" + i).val())) {
                alert("Can Not Received the Material Greater than  Purchase Material");
                return false;
            }
            else {
                var params = new Object();
                var StockEntry = new Object();
                StockEntry.EMRID = $("#hdnEMRId" + i).val();
                if ($("#txDispatchQuantity" + i).val() == "" || $("#txDispatchQuantity" + i).val() == undefined) {
                    StockEntry.Quantity = $("#txtInStoreQuantity" + i).val();
                }
                else {
                    StockEntry.Quantity = $("#txtRemainingQty" + i).val();
                }
                StockEntry.ReceivedBy = $("input[id*='hdnInchargeID']").val();
                StockEntry.BillPath = $("#drpBillNo" + i).val();

                params.stockentry = StockEntry;

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Services/StoreController.asmx/SaveStroeMaterialDetail",
                    data: JSON.stringify(params),
                    dataType: "json",
                    async: false,
                    success: function (result, textStatus) {
                        if (textStatus == "success") {
                            if ($("#hdnlblDispatchQuantity" + i).val() == "" || $("#lblDispatchQuantity" + i).val() == undefined) {
                                $("#lblInStoreQty" + i).html(result.d);
                            }
                            else {
                                $("#lblRemainingQty" + i).html(result.d);
                            }
                            $("#txtInStoreQuantity" + i).val(0);
                            alert("Material Received Successfully");
                        }
                    },
                    error: function (result, textStatus) {
                        alert(result.responseText)
                    }
                });
            }
        }
    }
    return true;
}

function DispatchMaterialAndValidation() {

    var tablelength = $("#tblmatDetail > tbody").children('tr').length;
    var txtDispatchQuantity = 0;
    var hdnlblStoreQuantity = 0;
    var hdnlblRemainingQty = 0;
    var hdnlblDispatchQuantity = 0;

    for (var i = 0 ; i < (tablelength - 1) ; i++) {

        if ($("#hdnlblStoreQuantity" + i).val() == undefined || $("#hdnlblStoreQuantity" + i).val() == "") {
            hdnlblStoreQuantity = "0";
        }
        else {
            hdnlblStoreQuantity = $("#hdnlblStoreQuantity" + i).val();
        }
        if ($("#hdnlblRemainingQty" + i).val() == undefined || $("#hdnlblRemainingQty" + i).val() == "") {
            hdnlblRemainingQty = "0";
        }
        else {
            hdnlblRemainingQty = $("#hdnlblRemainingQty" + i).val();
        }
        if ($("#hdnlblDispatchQuantity" + i).val() == undefined || $("#hdnlblDispatchQuantity" + i).val() == "") {
            hdnlblDispatchQuantity = 0;
        }
        else {
            hdnlblDispatchQuantity = $("#hdnlblDispatchQuantity" + i).val();
        }
       
        txtDispatchQuantity = $("#txDispatchQuantity" + i).val();
       
        var totalDispatch = parseInt(txtDispatchQuantity) + parseInt(hdnlblDispatchQuantity);
        var totalStoreQty = parseInt(hdnlblRemainingQty) + parseInt(hdnlblDispatchQuantity);

        if ($("#txDispatchQuantity" + i).val() > 0) {
            if (parseInt(txtDispatchQuantity) > parseInt(hdnlblStoreQuantity) && parseInt(totalDispatch) > parseInt(totalStoreQty)) {
                alert("Can not Dispatch Qty Greater than In Store Qty");
                return false;
            }
            else {
                var params = new Object();
                var StockDispatchEntry = new Object();
                StockDispatchEntry.EMRID = $("#hdnEMRId" + i).val();
                StockDispatchEntry.DispatchQuantity = $("#txDispatchQuantity" + i).val();
                StockDispatchEntry.DispatchBy = $("input[id*='hdnInchargeID']").val();


                params.stockdispatchentry = StockDispatchEntry;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Services/StoreController.asmx/SaveDisatchMaterialDetail",
                    data: JSON.stringify(params),
                    dataType: "json",
                    async: false,
                    success: function (result, textStatus) {
                        if (textStatus == "success") {
                            if ($("#hdnlblDispatchQuantity" + i).val() == "" || $("#hdnlblDispatchQuantity" + i).val() == undefined) {
                                $("#lblInStoreQty" + i).html(result.d);
                            }
                            else {
                                $("#lblRemainingQty" + i).html(result.d);
                            }
                            $("#txDispatchQuantity" + i).val(0);
                            alert("Material Dispatch Successfully");
                        }
                    },
                    error: function (result, textStatus) {
                        alert(result.responseText)
                    }
                });
            }
        }
    }

    return true;
}


