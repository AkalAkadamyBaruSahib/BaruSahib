
var MaterialList = new Array();
var MaterialObjectList;
var SnoIds;
var MaterialList = Array();;
var selectedMaterialList = new Array();
var delItems = 0;
var cntM = 1;
var SourceTypeList;
var MaterialTypeList;
var materialType;
var MaterialTypeIntransport;


$(document).ready(function () {
    GetMaterialObjectList();
    $("select").searchable();

    $("#btnSubEstimate").click(function (e) {
        if (Page_ClientValidate("visitor")) {
            if (Validation()) {
                ClientSideClick(this);
                SaveEstimate();
            }
        }
    });

    $("#trEstimateDetail").hide();

    $("#lblWorkNameReflect").hide();

    $("#tdWorkAllot").hide();

    $("select[id*='ddlZone']").change(function () {
        if ($("input[id*='hdnIsAdmin']").val() == 14 || $("input[id*='hdnIsAdmin']").val() == 17 || $("input[id*='hdnIsAdmin']").val() == 15) {
            BindAcademybyZoneIDByEmpID($(this).val(), $("input[id*='hdnInchargeID']").val());
        }
        else {
            BindAcademybyZoneID($(this).val());
        }
    });

    $("select[id*='ddlAcademy']").change(function () {
        BindWorkAllotByAcademyID($(this).val());
        $("#tdWorkAllot").show();
    });

    $("select[id*='ddlWorkAllot']").change(function () {
        $("#lblWorkNameReflect").hide();
        $("#lblWorkNameReflect").html($("select[id*='ddlWorkAllot']").val())
    });

    $("#btnEstimateCost").click(function (e) {
        TotalAmt();
    });

    BindZoneByInchargeID($("input[id*='hdnInchargeID']").val())

    if ($("input[id*='hdnIsAdmin']").val() == 6) {
        $("#lblSourceType").show();
        $("select[id*='ddlAcademy']").append($("<option></option>").val("0").html("--Select Workshop--"));
        BindAcademybyZoneIDByEmpID(21, $("input[id*='hdnInchargeID']").val());
    }
    else if ($("input[id*='hdnIsAdmin']").val() == 30) {
        $("select[id*='ddlAcademy']").append($("<option></option>").val("0").html("--Select Workshop--"));
        BindAcademybyZoneIDByEmpID(21, $("input[id*='hdnInchargeID']").val());
    }
    else {
        $("#lblzone").show();
        $("#lblAcademy").show();
        $("select[id*='ddlZone']").append($("<option></option>").val("0").html("--Select Zone--"));
        $("select[id*='ddlAcademy']").append($("<option></option>").val("0").html("--Select Academy--"));
    }
  
    GetPurchaseSource();
    $("#aDeleteRow0").hide();
    GetMaterialType();
});

function BindZone() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetZone",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("select[id*='ddlZone']").append($("<option></option>").val(value.ZoneId).html(value.ZoneName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindZoneByInchargeID(inchargeId) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetZoneByInchargeID",
        data: JSON.stringify({ InchargeID: parseInt(inchargeId) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("select[id*='ddlZone']").append($("<option></option>").val(value.ZoneId).html(value.ZoneName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindAcademybyZoneID(selctZoneID) {

    $("select[id*='ddlAcademy'] option").each(function (index, option) {
        $(option).remove();
    });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetAcademybyZoneID",
        data: JSON.stringify({ ZoneID: parseInt(selctZoneID) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                if ($("input[id*='hdnModule']").val() == 4) {
                    $("select[id*='ddlAcademy']").append($("<option></option>").val("0").html("--Select Workshop Type--"));
                }
                else {
                    $("select[id*='ddlAcademy']").append($("<option></option>").val("0").html("--Select Academy--"));
                }
                $.each(Result, function (key, value) {
                    $("select[id*='ddlAcademy']").append($("<option></option>").val(value.AcaID).html(value.AcaName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindAcademybyZoneIDByEmpID(selctZoneID, inchargeId) {

    $("select[id*='ddlAcademy'] option").each(function (index, option) {
        $(option).remove();
    });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetAcademybyZoneIDByEmpID",
        data: JSON.stringify({ ZoneID: parseInt(selctZoneID), InchargeID: parseInt(inchargeId) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                if ($("input[id*='hdnModule']").val() == 4) {
                    $("select[id*='ddlAcademy']").append($("<option></option>").val("0").html("--Select Workshop--"));
                }
                else {
                    $("select[id*='ddlAcademy']").append($("<option></option>").val("0").html("--Select Academy--"));
                }
                $.each(Result, function (key, value) {
                    $("select[id*='ddlAcademy']").append($("<option></option>").val(value.AcaID).html(value.AcaName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindWorkAllotByAcademyID(selctAcademyID) {
    $("select[id*='ddlWorkAllot'] option").each(function (index, option) {
        $(option).remove();
    });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetWorkAllotByAcademyID",
        data: JSON.stringify({ AcademyID: parseInt(selctAcademyID) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $("select[id*='ddlWorkAllot']").append($("<option></option>").val("0").html("--Select Work Allot--"));
                $.each(Result, function (key, value) {
                    $("select[id*='ddlWorkAllot']").append($("<option></option>").val(value.WAId).html(value.WorkAllotName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function AddMaterialRow() {

    if (Validation()) {
        $('#tblEstimateMatDetail tr').last().after('<tr id="tr' + cntM + '"><td><span id="spn' + cntM + '">' + (cntM + 1) + '</span></td>' +
          '<td> <select id="ddlMaterialType' + cntM + '"  name="ddlMaterialType' + cntM + '" style="width:150px;" ><option value="0">Select Material Type</option></select></td>' +
          '<td><input id="txtMaterialName' + cntM + '" onblur="MaterialTextBox_ChangeEvent(' + cntM + ');" name="txtMaterialName' + cntM + '" value="" type="text" style="width:200px;" /><br/><div id="menu-container' + cntM + '" style="position:absolute; width:500px;"></div></td>' +
          '<td>  <label id="lblUnit' + cntM + '" name="lblUnit' + cntM + '" ></label></td>' +
          '<td><input id="txtQty' + cntM + '" name="txtQty' + cntM + '" value="" type="text" style="width:80px;" /></td>' +
          '<td> <select id="ddlSourceType' + cntM + '"  name="ddlSourceType' + cntM + '" style="width:150px;" ><option value="0">Select Source Type</option></select></td>' +
          '<td> <input id="txtRate' + cntM + '" name="txtRate' + cntM + '" value="" type="text" style="width:80px;" /></td>' +
          '<td><input id="txtRemarks' + cntM + '" name="txtRemarks' + cntM + '" value="" type="text"class="span6 typeahead" style="width:200px;"/></td>' +
          '<td><a href="javascript:void(0);" id="aAddNewRow' + cntM + '" onclick="AddMaterialRow();"><b>Add Row</b></a> <a href="javascript:void(0);" id="aDeleteRow' + cntM + '" onclick="removeRow(' + cntM + ');"><b>Delete</b></a><input type="hidden" id="hdnMatID' + cntM + '" /><input type="hidden" id="hdnMatTypeID' + cntM + '" /><input type="hidden" id="hdnUnitID' + cntM + '" />   <input type="hidden" id="hdnMatCost' + cntM + '" /><input type="hidden" id="hdnLocalCost' + cntM + '" /><input type="hidden" id="hdnAkalWorkshopCost' + cntM + '" /></td></tr>');

        BindPurchaseSource(cntM);
        BindMaterialType(cntM);
        fixSerialNumber();

        $("#aDeleteRow" + cntM).hide();

        if (cntM > 0) {
            var cntR = cntM - 1;
            $("#aAddNewRow" + cntR).hide();
            $("#aDeleteRow" + cntR).show();
            $("#aAddNewRow0").hide();
            $("#aDeleteRow0").show();
        }

        cntM++;
    }
}

function GetPurchaseSource() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetPurchaseSource",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                SourceTypeList = result.d;
                $.each(SourceTypeList, function (key, value) {
                    $("#ddlSourceType0").append($("<option></option>").val(value.PSId).html(value.PSName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });

    $("#ddlSourceType0").on('SourceType change', function () {
        var SourceTypeID = this.value;
        if (SourceTypeID != undefined) {
            if ($("#ddlSourceType0").val() == "2") {
                matcost = $("#hdnMatCost0").val();
                $("#txtRate0").val(matcost);
            }
            else if ($("#ddlSourceType0").val() == "1") {
                matcost = $("#hdnLocalCost0").val();
                $("#txtRate0").val(matcost);
            }
            else if ($("#ddlSourceType0").val() == "3") {
                matcost = $("#hdnAkalWorkshopCost0").val();
                $("#txtRate0").val(matcost);
            }
            else {
                $("#txtRate0").val('0.00');
            }
        }
    }).change();
}

function BindPurchaseSource(cntID) {
    $.each(SourceTypeList, function (key, value) {
        $("#ddlSourceType" + cntID).append($("<option></option>").val(value.PSId).html(value.PSName));


        $("#ddlSourceType" + cntID).on('ddlSourceTypechange change', function () {

            var MatTypeID = this.value;
            if (MatTypeID != undefined && this.value != 0) {
                if ($("#ddlSourceType" + cntID).val() == "2") {
                    matcost = $("#hdnMatCost" + cntID).val();
                    $("#txtRate" + cntID).val(matcost);
                }
                else if ($("#ddlSourceType" + cntID).val() == "1") {
                    matcost = $("#hdnLocalCost" + cntID).val();
                    $("#txtRate" + cntID).val(matcost);
                }
                else if ($("#ddlSourceType" + cntID).val() == "3") {
                    matcost = $("#hdnAkalWorkshopCost" + cntID).val();
                    $("#txtRate" + cntID).val(matcost);
                }
                else {
                    $("#txtRate" + cntID).val('0.00');
                }
            }
        }).change();
    });
}

function MaterialTextBox_ChangeEvent(cntID) {

    var Matname = $("#txtMaterialName" + cntID).val();

    var selectedMaterial = $.grep(MaterialObjectList, function (e) { return e.MatName == Matname })[0];

    if (selectedMaterial != undefined) {
        $("#hdnMatID" + cntID).val(selectedMaterial.MatID);
        $("#lblUnit" + cntID).text(selectedMaterial.Unit.UnitName);
        $("#hdnUnitID" + cntID).val(selectedMaterial.Unit.UnitId);
        $("#hdnMatTypeID" + cntID).val(selectedMaterial.MatTypeID);
        $("#hdnMatCost" + cntID).val(selectedMaterial.MatCost);
        $("#hdnLocalCost" + cntID).val(selectedMaterial.LocalRate);
        $("#hdnAkalWorkshopCost" + cntID).val(selectedMaterial.AkalWorkshopRate);
    }
}

function SaveEstimate() {
    //$("#progress").dialog({ modal: true, width: 300, height: 200, title: "Progress", closeOnEscape: false });
    //$("#progress").dialog('open');

    var params = new Object();
    var Estimate = new Object();

    //Estimate.EstId = $("input[id*='hdnEstimateID']").val();
    Estimate.ZoneId = $("select[id*='ddlZone']").val();
    Estimate.AcaId = $("select[id*='ddlAcademy']").val();
    Estimate.SubEstimate = $("input[id*='txtSubEstimate']").val();
    Estimate.TypeWorkId = $("select[id*='ddlTypeOfWork']").val();
    Estimate.Active = 1;
    Estimate.WAId = $("select[id*='ddlWorkAllot']").val();
    Estimate.FileNme = $("input[id*='txtFileName']").val();
    Estimate.CreatedBy = $("input[id*='hdnInchargeID']").val();
    Estimate.ModifyBy = $("input[id*='hdnInchargeID']").val();
    Estimate.FilePath = "";
    if ($("input[id*='hdnIsAdmin']").val() == 1 || $("input[id*='hdnIsAdmin']").val() == 13 || $("input[id*='hdnIsAdmin']").val() == 6) {
        Estimate.IsApproved = true;
    }
    else {
        Estimate.IsApproved = false;
    }
    Estimate.IsRejected = false;
    Estimate.IsActive = true;
    Estimate.ModuleID = $("input[id*='hdnModule']").val();
    Estimate.IsReceived = false;
    Estimate.ReceivedBy = 0;
    Estimate.EstimateAndMaterialOthersRelations = new Object();

    var estimateAndMaterialOthersRelations = new Array();

    var tablelength = $("#tbody").children('tr').length;
    var Amt = 0;
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {
        if ($("#hdnMatID" + i).val() != undefined) {
            var EstimateAndMaterialOthersRelation = new Object();
            EstimateAndMaterialOthersRelation.EstId = Estimate.EstId;
            EstimateAndMaterialOthersRelation.MatId = $("#hdnMatID" + i).val();
            EstimateAndMaterialOthersRelation.MatTypeId = $("#hdnMatTypeID" + i).val();
            EstimateAndMaterialOthersRelation.PSId = $("#ddlSourceType" + i).val();

            if ($("#txtQty" + i).val() == "" || $("#txtQty" + i).val() == undefined) {
                EstimateAndMaterialOthersRelation.Qty = 0;
            }
            else {
                EstimateAndMaterialOthersRelation.Qty = $("#txtQty" + i).val();
            }

            if ($("#txtRate" + i).val() == "" || $("#txtRate" + i).val() == undefined) {
                EstimateAndMaterialOthersRelation.Rate = 0;
            }
            else {
                EstimateAndMaterialOthersRelation.Rate = $("#txtRate" + i).val();
            }
            EstimateAndMaterialOthersRelation.Remark = $("#txtRemarks" + i).val();
            EstimateAndMaterialOthersRelation.UnitId = $("#hdnUnitID" + i).val();
            EstimateAndMaterialOthersRelation.CreatedBy = $("input[id*='hdnInchargeID']").val();
            EstimateAndMaterialOthersRelation.ModifyBy = $("input[id*='hdnInchargeID']").val();
            EstimateAndMaterialOthersRelation.Active = 1;
            EstimateAndMaterialOthersRelation.IsApproved = true;
            EstimateAndMaterialOthersRelation.VendorID = 0;
            EstimateAndMaterialOthersRelation.PurchaseQty = 0;
            EstimateAndMaterialOthersRelation.PurchaseEmpID = 0;
            EstimateAndMaterialOthersRelation.DispatchStatus = 0;
            EstimateAndMaterialOthersRelation.DirectPurchase = false;
            Amt += parseFloat(EstimateAndMaterialOthersRelation.Qty) * parseFloat(EstimateAndMaterialOthersRelation.Rate);
            EstimateAndMaterialOthersRelation.Amount = parseFloat(EstimateAndMaterialOthersRelation.Qty) * parseFloat(EstimateAndMaterialOthersRelation.Rate);
            estimateAndMaterialOthersRelations.push(EstimateAndMaterialOthersRelation);
        }
    }
    $("#lblAmt").val(Amt);
    Estimate.EstmateCost = Amt;
    Estimate.EstimateAndMaterialOthersRelations = estimateAndMaterialOthersRelations;
    params.estimate = Estimate;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/SaveEstimateDetail",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                $("input[id*='hdnEstimateID']").val(result.d);
                SignedCopyFileUpload(result.d);

            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function SignedCopyFileUpload(estid) {
    var files = $("input[id*='fileUploadSignedCopy']")[0].files;

    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }

    $.ajax({
        url: "EstimateSignedCopyHandler.ashx?EstID=" + estid,
        type: "POST",
        async: false,
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            //$("#progress").dialog('close');
            alert("Estimate Create Successfuly");
            if ($("input[id*='hdnIsAdmin']").val() == 1) {
                window.location.replace("Admin_ParticularEstimateView.aspx?EstId=" + estid);
            }
            else if ($("input[id*='hdnIsAdmin']").val() == 30 || $("input[id*='hdnIsAdmin']").val() == 6) {
                window.location.replace("WorkshopEmployee_ParticularEstimateView.aspx?EstId=" + estid);
            }
            else if ($("input[id*='hdnIsAdmin']").val() == 2) {
                window.location.replace("Emp_ParticularEstimateView.aspx?EstId=" + estid);
            }
            else {
                window.location.replace("Transport_ParticularEstimateView.aspx?EstId=" + estid);
            }
        },
        error: function (err) {
            alert(err.statusText)
        }
    });
}

function TotalAmt() {
    if (Validation()) {
        var tablelength = $("#tbody").children('tr').length;
        var Amt = 0;
        var rate = 0;
        var qty = 0;
        for (var i = 0 ; i < (tablelength + delItems) ; i++) {
            if ($("#txtQty" + i).val() == "" || $("#txtQty" + i).val() == undefined) {
                qty = 0;
            }
            else {
                qty = $("#txtQty" + i).val();
            }

            if ($("#txtRate" + i).val() == "" || $("#txtRate" + i).val() == undefined) {
                rate = 0;
            }
            else {
                rate = $("#txtRate" + i).val();
            }

            Amt += parseFloat(qty) * parseFloat(rate);
        }
        var RoundAmt = Amt.toFixed(2);
        $("[id$='lblAmt']").html(RoundAmt);
    }
}

function fixSerialNumber() {
    var tablelength = $("#tbody").children('tr').length;
    var sno = 0;
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {
        if ($("#spn" + i).text() != "") {
            $("#spn" + i).html(parseInt(sno + 1));
            sno = sno + 1;
        }
    }
}

function Validation() {
    var tablelength = $("#tbody").children('tr').length;
    for (var i = 0 ; i < (tablelength + delItems) ; i++) {
        if ($("#ddlMaterialType" + i).val() == "undefined" || $("#ddlMaterialType" + i).val() == "0") {
            $("#ddlMaterialType" + i).css('border-color', 'red');
            return false;
        }
        else {
            $("#ddlMaterialType" + i).css('border-color', '');
        }
        if ($("#txtMaterialName" + i).val() == "" || $("#txtMaterialName" + i).val() == "0") {
            $("#txtMaterialName" + i).css('border-color', 'red');
            return false;
        }
        else {
            $("#txtMaterialName" + i).css('border-color', '');

        }

        if ($("#hdnMatID" + i).val() == "undefined" || $("#hdnMatID" + i).val() == "") {
            $("#txtMaterialName" + i).css('border-color', 'red');
            $("#txtMaterialName" + i).val("");
            return false;
        }
        else {
            $("#txtMaterialName" + i).css('border-color', '');
        }

        if ($("#txtQty" + i).val() != undefined) {
            var value = $("#txtQty" + i).val()
            var regex = new RegExp(/^\+?[0-9(),.-]+$/);

            if ($("#txtQty" + i).val() == "" || $("#txtQty" + i).val() == "0" || !value.match(regex)) {
                $("#txtQty" + i).css('border-color', 'red');
                return false;
            }
            else {
                $("#txtQty" + i).css('border-color', '');
            }
        }
        if ($("#ddlSourceType" + i).val() == "undefined" || $("#ddlSourceType" + i).val() == "0") {
            $("#ddlSourceType" + i).css('border-color', 'red');
            return false;
        }
        else {
            $("#ddlSourceType" + i).css('border-color', '');
        }
    }
    return true;
}

function removeRow(removeNum) {
    $('#tr' + removeNum).remove();
    delItems = delItems + 1;
    fixSerialNumber();
    return false;
    cntM--;
}

function ClearData(cntID) {
    $("#txtMaterialName" + cntID).val("");
    $("#txtRate" + cntID).val("");
    $("#lblUnit" + cntID).text("");
    $("#txtRemarks" + cntID).val("");
    $("#txtQty" + cntID).val("");
    $("#spnMaterialTypeID" + cntID).text("");
    $("#ddlSourceType" + cntID).val("0");
}

function GetMaterialType() {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Services/PurchaseControler.asmx/GetActiveMaterialTypes",
            dataType: "json",
            success: function (result, textStatus) {
                materialType = result.d;
                $.each(materialType, function (key, value) {
                    $("#ddlMaterialType0").append($("<option></option>").val(value.MatTypeId).html(value.MatTypeName));
                });
            },
            error: function (result, textStatus) {
                alert(result.responseText);
            }
        });
 

    $("#ddlMaterialType0").on('MaterialType change', function () {
        var MatTypename = this.value;
        if (MatTypename != undefined && this.value != 0) {
            MaterialList = [];
            if (MaterialObjectList != undefined && MaterialObjectList.length > 0) {
                var tempArray = $.grep(MaterialObjectList, function (e) { return e.MatTypeID == MatTypename }); //83 Live // 75 Local
                $.each(tempArray, function (index, value) {
                    MaterialList.push(value.MatName);
                });

                $("#txtMaterialName0").autocomplete({
                    source: MaterialList,
                    appendTo: '#menu-container0'
                });
            }
        }
        ClearData(0);
    }).change();

}

function BindMaterialType(cntID) {
    $.each(materialType, function (key, value) {
        $("#ddlMaterialType" + cntID).append($("<option></option>").val(value.MatTypeId).html(value.MatTypeName));

        $("#ddlMaterialType" + cntID).on('ddlMaterialTypechange change', function () {

            var MatTypeID = this.value;
            if (MatTypeID != undefined && this.value != 0) {
                MaterialList = [];
                    var tempArray = $.grep(MaterialObjectList, function (e) { return e.MatTypeID == MatTypeID }); //83 Live // 75 Local
                    $.each(tempArray, function (index, value) {
                        MaterialList.push(value.MatName);
                    });
                    $("#txtMaterialName" + cntID).autocomplete({
                        source: MaterialList,
                        appendTo: '#menu-container' + cntID
                    });
                }
          ClearData(cntID)
        }).change();
    });
}

function GetMaterialObjectList() {
    if (MaterialObjectList == undefined || MaterialObjectList.length == 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Services/PurchaseControler.asmx/GetActiveMaterials",
            dataType: "json",
            async: false,
            success: function (result, textStatus) {
                if (result.d != undefined) {
                    MaterialObjectList = result.d;
                }
            },
            error: function (result, textStatus) {
                alert(result.responseText);
            }
        })
    }
 
}













