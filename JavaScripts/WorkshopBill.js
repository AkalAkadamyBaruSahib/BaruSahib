
var materialname = new Array();
var tblUploadMaterial;
var SnoIds;
var MaterialList;
var selectedMaterialList = new Array();
var sum = 0;
var listVal = "";

$(document).ready(function () {
    BindEstimate();
    BindCurrentDate();
  
    $("#drpEstimate").change(function (e) {
        $("#divUploadMaterial").modal('show');
        GetMaterialList($(this).val());
        listVal += $(this).val() + ",";
        var EstVal = listVal.substr(0, listVal.length - 1);
        $("input[id*='hdnEstNo']").val(EstVal);
        GetAcademyName($(this).val());
    });
    
    $("input[id*='btnLoad']").click(function (e) {
        LoadWorkshopBillInfo(selectedMaterialList);
        $("input[id*='hdnItemsLength']").val("");
        var SnoValue = "";
        for (var i = 0; i < selectedMaterialList.length; i++) {
            var Material = selectedMaterialList[i];
            SnoValue += Material.Sno + ",";
        }
        SnoValue = SnoValue.substr(0, SnoValue.length - 1);
        $("input[id*='hdnItemsLength']").val(SnoValue);

        $("#divUploadMaterial").modal('hide');
    });

    
 });


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
            '<td id="qty">abc</td>' +
                '<td id="pcs">abc</td>' +
                    '<td id="rate">abc</td>' +
                        '<td id="amount">abc</td>' +
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
        $newRow.find("#srno").html(count);
        $newRow.find("#nameofItem").html(adminLoanList[i].Material.MatName);
        $newRow.find("#qty").html("<table><tr><td><label id='MatQty'>" + adminLoanList[i].Qty + "</label></td></tr></table>");
        $newRow.find("#pcs").html(adminLoanList[i].Unit.UnitName);
        $newRow.find("#rate").html(adminLoanList[i].Material.MatCost);
        var linetotal = adminLoanList[i].Qty * adminLoanList[i].Material.MatCost;
        $newRow.find("#amount").html("<input type='hidden' value='" + linetotal + "' id='txtTotalAmount" + i + "' />" + linetotal);
        count++;
        sum += linetotal;
        $("[id$='lblTotal']").html(sum);
        $("input[id*='hdnTotal']").val(sum);
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
            "bAutoWidth": false,
            "bLengthChange": false,
            "bDestroy": false
        });
    TotalAmt();
}

function BindCurrentDate() {
    var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
    var dNow = new Date();
    var curr_date = dNow.getDate();
    var curr_month = dNow.getMonth();
    var curr_year = dNow.getFullYear();
    var localdate = m_names[curr_month] + curr_date + "," + curr_year;
    $("[id$='lblCurrentDate']").html(localdate);
    $("[id$='lblDate']").html(localdate);
    $("input[id*='hdnCurrentDate']").val(localdate); 
}

function TotalAmt() {
    var tablelength = $("#tbody").children('tr').length;
    var Amt = 0;
    var rate = 0;
    for (var i = 0 ; i < tablelength ; i++) {
        var qty = $("#txtTotalAmount" + i).val();
        Amt += parseInt(qty);
    }
     $("[id$='lblTotal']").html(Amt);
     $("input[id*='hdnTotal']").val(Amt);


    var scrap = $("[id$='lblTotal']").text() * 2 / 100;
    var Total = $("[id$='lblTotal']").text();
    var grandTotal = parseInt(Total) - parseInt(scrap);

   
    $("input[id*='hdnScrap']").val(scrap);
    $("[id$='lblScrap']").html(scrap);
   
    $("[id$='lblGrandTotal']").html(grandTotal);
    $("input[id*='hdnGrandTotal']").val(grandTotal);

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

