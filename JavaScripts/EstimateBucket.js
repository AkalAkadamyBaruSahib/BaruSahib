var grdBucketDiscription;

$(document).ready(function () {

    $("select").searchable();

    GetMaterialType();
    $("input[id*='btnLoad']").click(function (e) {
        if (Validation()) {
            SaveEstimateBucketDetail();
        }
    });

    $("input[id*='btnUpdateLoadBucket']").click(function (e) {
        if (Validation()) {
            UpdateEstimateBucketDetail();
        }
    });
    
    LoadBucketInfo();
});

function GetMaterialType() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetActiveMaterialTypes",
        dataType: "json",
        success: function (result, textStatus) {
            materialType = result.d;
            $.each(materialType, function (key, value) {
                $("#drpMaterialType").append($("<option></option>").val(value.MatTypeId).html(value.MatTypeName));
            });
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}
 

function drpMaterialType_onchange(ddl) {
    var materialType = $(ddl).val();
    GetMaterialName(materialType);
}

function GetMaterialName(matType) {
   
    $("#drpMaterialName").multiselect('destroy');
    $("#drpMaterialName  option").each(function (index, option) {
        $(option).remove();
    });
   

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetActiveMaterialsByMatTypeID",
        data: JSON.stringify({ MatTypeID: parseInt(matType) }),
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            materialType = result.d;
            $.each(materialType, function (key, value) {
                $("#drpMaterialName").append($("<option></option>").val(value.MatID).html(value.MatName));
            });
            $('#drpMaterialName').multiselect({
                includeSelectAllOption: true,
                enableFiltering: true
            });


        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function SaveEstimateBucketDetail() {
    var params = new Object();
    var BucketName = new Object();
    BucketName.BucketID = 0;
    BucketName.Name = $("#txtBucketName").val();
    var estimateBucketMaterialRelation = new Array();

    $("#drpMaterialName  :selected").each(function (index) {

        var EstimateBucketMaterialRelation = new Object();
     //   EstimateBucketMaterialRelation.BucketID = BucketName.ID;
        EstimateBucketMaterialRelation.MatTypeID = $("#drpMaterialType").val();
        EstimateBucketMaterialRelation.InchargeID = $("input[id*='hdnInchargeID']").val();
        EstimateBucketMaterialRelation.MatID = $(this).val();

        estimateBucketMaterialRelation.push(EstimateBucketMaterialRelation);
    });

    BucketName.EstimateBucketMaterialRelation = estimateBucketMaterialRelation;

    params.bucketName = BucketName;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/AddNewBucketInformation",
        data: JSON.stringify(params),
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            if (textStatus == "success") {
                LoadBucketInfo();
                alert("Bucket Create successfully");
                ClearText();
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function LoadBucketInfo() {

    /*create/distroy grid for the new search*/
    if (typeof grdBucketDiscription != 'undefined') {
        grdBucketDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="srNo">abc</td><td id="bucketName">abc</td><td id="materialName">abc</td><td id="action">cc</td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetBucketInformation",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var adminLoanList = result.d;
                if (adminLoanList.length > 0) {

                    $("#tbody").append(rowTemplate);
                }
                var visitorType = "";
                for (var i = 0; i < adminLoanList.length; i++) {
                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }
                    var $newRow = $("#rowTemplate").clone();

                    $newRow.find("#srNo").html("<span id='spn'>" + (i + 1) + "</span>");
                    $newRow.find("#bucketName").html(adminLoanList[i].BucketName);
                    $newRow.find("#materialName").html(adminLoanList[i].MatName);
                    $newRow.find("#action").html("<table><tr><td><a href='#' onclick='GetBucketInfoToUpdate(" + adminLoanList[i].BucketID + ")'>Add More Material</a></td></tr><tr><td><a href='#' onclick='GetBucketInfoToDelete(" + adminLoanList[i].BucketID + ")'>Delete Bucket</a></td></tr></table>");

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

                grdBucketDiscription = $('#grid').DataTable(
                    {
                        "bPaginate": true,
                        "iDisplayLength": 20,
                        "sPaginationType": "full_numbers",
                        "bSort": false,
                        "bAutoWidth": false,
                        "bLengthChange": false,
                        "bDestroy": true,
                        "bInfo": true

                    });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetBucketInfoToUpdate(bucketID) {
    var values = "";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetBucketInfoToUpdate",
        data: JSON.stringify({ estBucketID: bucketID }),
        dataType: "json",
        success: function (responce) {
            var rdata = responce.d;
            if (rdata != undefined) {
                $("input[id*='hdnBucketID']").val(rdata.BucketID);
                $("#txtBucketName").val(rdata.Name);
                GetMaterialName(rdata.EstimateBucketMaterialRelation[0].MatTypeID);

                for (var i = 0; i < rdata.EstimateBucketMaterialRelation.length; i++) {
                    values += rdata.EstimateBucketMaterialRelation[i].MatID + ",";
                    $("#drpMaterialType").val(rdata.EstimateBucketMaterialRelation[i].MatTypeID);
                }

                $.each(values.split(","), function (i, e) {

                    $('input[type=checkbox]').each(function () {
                        if ($(this).val() == e) {
                            $(this)[0].checked = true;
                        }
                    });

                    $(".multiselect-selected-text").text(rdata.EstimateBucketMaterialRelation.length + " selected items");
                    //    //$("#drpMaterialName option[value='" + e + "']").prop("selected", true);
                    //    //$("#drpMaterialName").multiselect().find(":checkbox[value='" + e + "']").attr("checked", "checked");
                    //    $("#drpMaterialName").multiselect("widget").find(":checkbox[value='" + e + "']").attr("checked", "checked");
                    //    ////$("#drpMaterialName option[value='" + e + "']").attr("selected", 1);
                    //    ////$("#drpMaterialName option[value='" + e + "']").prop("selected", true);
                });

                $("input[id*='btnLoad']").hide();
                $("input[id*='btnUpdateLoadBucket']").show();
             
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
        }
    });
}

function UpdateEstimateBucketDetail() {

    var params = new Object();
    var BucketName = new Object();
    BucketName.BucketID = $("input[id*='hdnBucketID']").val();
    BucketName.Name = $("#txtBucketName").val();
    var estimateBucketMaterialRelation = new Array();

   // $("#drpMaterialName  :selected").each(function (index) {
        $('input[type=checkbox]').each(function () {
            if ($(this)[0].checked == true) {
                var EstimateBucketMaterialRelation = new Object();
                EstimateBucketMaterialRelation.BucketID = $("input[id*='hdnBucketID']").val();
                EstimateBucketMaterialRelation.MatTypeID = $("#drpMaterialType").val();
                EstimateBucketMaterialRelation.InchargeID = $("input[id*='hdnInchargeID']").val();
                EstimateBucketMaterialRelation.MatID = $(this).val();

                estimateBucketMaterialRelation.push(EstimateBucketMaterialRelation);
            }
    });

    BucketName.EstimateBucketMaterialRelation = estimateBucketMaterialRelation;

    params.bucketName = BucketName;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/UpdateBucketInformation",
        data: JSON.stringify(params),
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            if (textStatus == "success") {
                LoadBucketInfo();
                $("input[id*='btnLoad']").show();
                $("input[id*='btnUpdateLoadBucket']").hide();
                alert("Bucket Update successfully");
                ClearText();
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function ClearText()
{
    $("#txtBucketName").val("");
    $("#drpMaterialType").val("");
 //   $("#drpMaterialName").multiselect("refresh");
}

function GetBucketInfoToDelete(bucketID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/BucketInfoToDelete",
        data: JSON.stringify({ bucketID: bucketID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                LoadBucketInfo();
                alert("Record has been Delete successfully");
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function Validation() {
    if ($("#txtBucketName").val() == "" || $("#txtBucketName").val() == "undefined") {
        $("#txtBucketName").css('border-color', 'red');
        return false;
    }
    else {
        $("#txtBucketName").css('border-color', '');
    }

    if ($("#drpMaterialType").val() == "undefined" || $("#drpMaterialType").val() == "0" || $("#drpMaterialType").val() == "") {
        $("#drpMaterialType").css('border-color', 'red');
        return false;
    }
    else {
        $("#drpMaterialType").css('border-color', '');
    }

    if ($("#drpMaterialName").val() == "undefined" || $("#drpMaterialName").val() == "0" || $("#drpMaterialName").val() == "") {
        $("#drpMaterialName").css('border-color', 'red');
        return false;
    }
    else {
        $("#drpMaterialName").css('border-color', '');
    }

    return true;
}