﻿
var materialname = new Array();
var tblUploadMaterial;
var grdBills;
var SnoIds;
var MaterialList;
var selectedMaterialList = new Array();
var sum = 0;
var listVal = "";
var delItems = 0;
var cntM = 1;
var removeMaterialList = "";

$(document).ready(function () {
    $("select").searchable();
});
$(document).ready(function () {
    BindEstimate($("input[id*='hdnInchargeID']").val());
    BindCurrentDate();

    $("#drpEstimate").change(function (e) {
        $("#divUploadMaterial").modal('show');
        GetMaterialList($(this).val());
        listVal += $(this).val() + "_";
        var EstVal = listVal.substr(0, listVal.length - 1);
        $("input[id*='hdnEstNo']").val(EstVal);
        GetAcademyName($(this).val());
        $("input[id*='btnViewBill']").show();
    });

    $("input[id*='btnLoad']").click(function (e) {
        LoadWorkshopBillInfo(selectedMaterialList);
        $("#divUploadMaterial").modal('hide');

    });

    $("input[id*='btnViewBill']").click(function (e) {
        OpenViewbill($("select[id*='drpEstimate']").val());
    });

});

function RemoveItem(chkbox, sno) {
    var SnoIds = ($(chkbox).attr("emrid"));
    var Material = $.grep(MaterialList, function (e) { return e.Sno == SnoIds; })[0];
    if ($(chkbox).is(':checked')) {
        selectedMaterialList.push(Material);
    }
    else {
        var selectedMaterialListNew = selectedMaterialList.filter(function (item) {
            return item.Sno != sno;
        });

        selectedMaterialList = selectedMaterialListNew;
        LoadWorkshopBillInfo(selectedMaterialList);
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

function BindEstimate(inchargeId) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetEstimateNumberList",
        data: JSON.stringify({ purchaseSourceID: 3, inchargeID: parseInt(inchargeId) }),
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
        data: JSON.stringify({ EstimateID: parseInt(selectedValue), PSID: parseInt(3) }),
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
                        $newRow.find("#chkmaterial").html("<input type='checkbox' onchange = 'RemoveItem($(this)," + MaterialList[i].Sno + ")' checked='true' id='chkItem_" + i + "' name='chkItem_" + i + "' EMRID='" + MaterialList[i].Sno + "' />");
                    }
                    else {
                        $newRow.find("#chkmaterial").html("<input type='checkbox' onchange='RemoveItem($(this)," + MaterialList[i].Sno + ")' id='chkItem_" + i + "' name='chkItem_" + i + "' EMRID='" + MaterialList[i].Sno + "' />");
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

function LoadWorkshopBillInfo(selectedMaterialList) {

    /*create/distroy grid for the new search*/
    if (typeof grdTicketDiscription != 'undefined') {
        grdTicketDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate">' +
        '<td id="srno">1</td>' +
    '<td id="nameofItem">abc</td>' +
            //'<td id="qty">abc</td>' +
                '<td id="pcs">abc</td>' +
                    '<td id="rate">abc</td>' +
                        //'<td id="amount">abc</td>' +
                           '<td id="delete">abc</td>' +
                            '</tr>';

    var adminLoanList = selectedMaterialList;
    if (adminLoanList.length > 0) {

        $("#tbody").append(rowTemplate);
    }

    var count = 1;
    for (var i = 0; i < adminLoanList.length; i++) {
        var className = "info";
        if (i % 2 == 0) {
            className = "warning";
        }
        var $newRow = $("#rowTemplate").clone();
        $newRow.find("#srno").html("<span id=spn" + i + ">" + (i + 1) + "</span>");
        $newRow.find("#nameofItem").html(adminLoanList[i].Material.MatName);
        //$newRow.find("#qty").html("<table><tr><td><label id='MatQty'>" + adminLoanList[i].PurchaseQty + "</label></td></tr></table>");
        $newRow.find("#pcs").html(adminLoanList[i].Unit.UnitName);
        $newRow.find("#rate").html(adminLoanList[i].Material.AkalWorkshopRate);
        //var linetotal = adminLoanList[i].PurchaseQty * adminLoanList[i].Material.AkalWorkshopRate;
        //$newRow.find("#amount").html("<input type='hidden' value='" + linetotal + "' id='txtTotalAmount" + i + "' />" + linetotal);
        $newRow.find("#delete").html("<a  href='#' onclick='return RemoveItem($(this)," + adminLoanList[i].Sno + ")'>Delete</a>");
        count++;
       // sum += linetotal;

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
            "bFilter": false,
            "bInfo": false,
            "bDestroy": true
        });
    TotalAmt();
    MaterialItemLength(selectedMaterialList);
  
}

function BindCurrentDate() {
    var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
    var dNow = new Date();
    var curr_date = dNow.getDate();
    var curr_month = dNow.getMonth();
    var curr_year = dNow.getFullYear();
    var localdate = m_names[curr_month] + curr_date + "," +"  "+ curr_year;
    $("[id$='lblCurrentDate']").html(localdate);
    $("[id$='lblDate']").html(localdate);
    $("input[id*='hdnCurrentDate']").val(localdate); 
}

function TotalAmt() {
    var tablelength = $("#tbody").children('tr').length;
    var Amt = 0;
    var rate = 0;
    for (var i = 0 ; i < tablelength + delItems ; i++) {
        if ($("#txtTotalAmount" + i).val() == undefined) {
            var qty = 0;
            Amt += parseInt(qty);
        }
        else {
            var qty = $("#txtTotalAmount" + i).val();
            Amt += parseInt(qty);
        }
    }
    $("[id$='lblTotal']").html(Amt);
    $("input[id*='hdnTotal']").val(Amt);
}

function GetAcademyName(selectedValue) {
    var Academyname = "";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/WorkshopController.asmx/GetAcademyNameByEstId",
        data: JSON.stringify({ EstimateID: parseInt(selectedValue) }),
        dataType: "json",
        success: function (responce) {
            var msg = responce.d;
            
            Academyname = msg[0].Academy.AcaName;
            $("[id$='lblAcademy']").html(Academyname);
            $("input[id*='hdnAcademy']").val(Academyname);
        
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function MaterialItemLength(selectedMaterialList)
{
    $("input[id*='hdnItemsLength']").val("");
    var SnoValue = "";

    for (var i = 0; i < selectedMaterialList.length + delItems; i++) {
        var Material = selectedMaterialList[i];
        SnoValue += Material.Sno + ",";
    }
    SnoValue = SnoValue.substr(0, SnoValue.length - 1);
    $("input[id*='hdnItemsLength']").val(SnoValue);
}

function OpenViewbill(estID) {
    $("#spnEstID").text(estID);
    /*create/distroy grid for the new search*/
    if (typeof grdBills != 'undefined') {
        grdBills.fnClearTable();
    }

    var rowCount = $('#grdBills').find("#rowViewTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowViewTemplate").remove();
    }

    var rowViewTemplate = '<tr id="rowViewTemplate"><td id="billName"></td><td id="view"></td><td id="delete"></td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/WorkshopController.asmx/GetBillDetails",
        data: JSON.stringify({ EstID: parseInt(estID) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var adminLoanList = result.d;
                if (adminLoanList.length > 0) {

                    $("#tbodyViewBill").append(rowViewTemplate);
                }

                for (var i = 0; i < adminLoanList.length; i++) {

                    var $newRow = $("#rowViewTemplate").clone();
                    $newRow.find("#billName").html("<a target='_blank' href='" + adminLoanList[i].BillPath + "' >Bill_No_" + adminLoanList[i].BillNumber + "</a>");
                    $newRow.find("#view").html("<a target='_blank' href='" + adminLoanList[i].BillPath + "' >View</a>");
                    $newRow.find("#delete").html("<a href='#' onclick='WorkshopBillToDelete(" + adminLoanList[i].ID + ")'>Delete</a>");
                    $newRow.show();
                    if (i == 0) {
                        $("#rowViewTemplate").replaceWith($newRow);
                    }
                    else {
                        $newRow.appendTo("#grdBills > tbody");
                    }

                }
                grdBills = $('#grdBills').DataTable({
                    "bPaginate": false,
                    "bDestroy": true,
                    //"bFilter": true
                    "bFilter": false,
                    //"aaSorting": []
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });

    $("#divViewbill").modal('show');
}

function WorkshopBillToDelete(billID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/WorkshopController.asmx/WorkshopBillToDelete",
        data: JSON.stringify({ BillID: billID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                $("input[id*='hdnEstID']").val(result.d);
                
                alert("Bill Delete Successfully");
                OpenViewbill(result.d);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}


