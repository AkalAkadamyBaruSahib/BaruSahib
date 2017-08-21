var grdTransportStudentDiscription;
var grdTransportStaffDiscription;
$(document).ready(function () {
    BindAcademyByInchargeID($("input[id*='hdnInchargeID']").val());
   

    $("select[id*='drpAcademy']").change(function () {
        $("input[id*='hdnAcaID']").val($(this).val());
    });

    $("select[id*='drpPassenger']").change(function () {
        if ($(this).val() == 1)
        {
            $("#divStudentDetail").show();
            $("#divStaffDetail").hide();
        }
        else
        {
            $("#divStaffDetail").show();
            $("#divStudentDetail").hide();
        }
    });

    $("select[id*='drpViewPassenger']").change(function () {
        if ($(this).val() == 1) {
            $("#divTransportStudentDetails").show();
            $("#divTransportStaffInfo").hide();
            LoadTransportStudentInfo($("input[id*='hdnInchargeID']").val());
        }
        else {
            $("#divTransportStaffInfo").show();
            $("#divTransportStudentDetails").hide();
            LoadTransportStaffInfo($("input[id*='hdnInchargeID']").val());

        }
    });
});

function LoadTransportStudentInfo(userID) {

    /*create/distroy grid for the new search*/
    if (typeof grdTransportStudentDiscription != 'undefined') {
        grdTransportStudentDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="admisnNo">abc</td><td id="Name">abc</td><td id="clss">abc</td><td id="CntactNo">abc</td><td id="villagename">abc</td><td id="action">cc</td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GeTransportStudentInfo",
        data: JSON.stringify({ inchargeID: parseInt(userID) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var adminLoanList = result.d;
                if (adminLoanList.length > 0) {
                    $("#tbody").append(rowTemplate);
                }
                var EmployeeType = "";
                for (var i = 0; i < adminLoanList.length; i++) {
                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }
         
                    var $newRow = $("#rowTemplate").clone();
                    $newRow.find("#admisnNo").html(adminLoanList[i].AdmissionNumber);
                    $newRow.find("#Name").html("<table><tr><td><b>Name: </b>" + adminLoanList[i].StudentName + "</td></tr><tr><td><b>Father Name: </b>" + adminLoanList[i].FatherName + "</td></tr></table>");
                    $newRow.find("#clss").html(adminLoanList[i].Class);
                    $newRow.find("#CntactNo").html(adminLoanList[i].ContactNo);
                    $newRow.find("#villagename").html(adminLoanList[i].NameOfVillage);
                    $newRow.find("#action").html("<table><tr><td><a href='#' onclick='GetTranportStudentInfoToUpdate(" + adminLoanList[i].ID + ")'>Edit</a></td></tr><tr><td><a href='#' onclick='GetTranportStudentInfoToDelete(" + adminLoanList[i].ID + ")'>Delete</a></td></tr></table>");
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
                grdTransportStudentDiscription = $('#grid').DataTable(
                {
                    "bPaginate": true,
                    "iDisplayLength": 20,
                    "sPaginationType": "full_numbers",
                    "bAutoWidth": false,
                    "bLengthChange": false,
                    "bDestroy": true

                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetTranportStudentInfoToUpdate(stuID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetStudentInfoByStudentID",
        data: JSON.stringify({ studentID: parseInt(stuID) }),
        dataType: "json",
        success: function (responce) {
            var msg = responce.d;
            if (msg != undefined) {
                $("input[id*='txtAdmissionNumber']").val(msg.AdmissionNumber);
                $("input[id*='txtStudentName']").val(msg.StudentName);
                $("input[id*='txtFatherName']").val(msg.FatherName);
                $("input[id*='txtClass']").val(msg.Class);
                $("input[id*='txtNameOfVillage']").val(msg.NameOfVillage);
                $("input[id*='txtContactNumber']").val(msg.ContactNo);
                $("input[id*='hdnAcaID']").val(msg.AcaID);
                $("select[id*='drpAcademy']").val(msg.AcaID);
                $("select[id*='drpPassenger']").val(1);
                $("#divStaffDetail").hide();
                $("#divStudentDetail").show();
                
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
        }
    });
}

function GetTranportStudentInfoToDelete(stuID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetStudentInfoToDeleteByStudentID",
        data: JSON.stringify({ studentID: stuID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                LoadTransportStudentInfo($("input[id*='hdnInchargeID']").val());
                $("#divTransportStudentDetails").show();
                $("#divTransportStaffInfo").hide();
                alert("Record has been Delete successfully");
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindAcademyByInchargeID(inchargeId) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetAcademyByInchargeID",
        data: JSON.stringify({ InchargeID: parseInt(inchargeId) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("select[id*='drpAcademy']").append($("<option></option>").val(value.AcaID).html(value.AcaName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function LoadTransportStaffInfo(userID) {

    /*create/distroy grid for the new search*/
    if (typeof grdTransportStaffDiscription != 'undefined') {
        grdTransportStaffDiscription.fnClearTable();
    }

    var rowCount = $('#gridStaff').find("#rowStaffTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowStaffTemplate").remove();
    }
    var rowStaffTemplate = '<tr id="rowStaffTemplate"><td id="staffID">abc</td><td id="staffType">abc</td><td id="staffName">abc</td><td id="fathername">abc</td><td id="action">abc</td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetStaffInfoInTransport",
        data: JSON.stringify({ inchargeID: parseInt(userID) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var adminLoanList = result.d;
                if (adminLoanList.length > 0) {
                    $("#tbodyStaff").append(rowStaffTemplate);
                }
                var StaffType = "";
                for (var i = 0; i < adminLoanList.length; i++) {
                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }
                    if (adminLoanList[i].StaffType == 1)
                    {
                        StaffType = "Teaching";
                    }
                    else
                    {
                        StaffType="Non Teaching"
                    }
                    var $newRow = $("#rowStaffTemplate").clone();
                    $newRow.find("#staffID").html(adminLoanList[i].ID);
                    $newRow.find("#staffType").html(StaffType);
                    $newRow.find("#staffName").html(adminLoanList[i].StaffName);
                    $newRow.find("#fathername").html(adminLoanList[i].FatherName);
                    $newRow.find("#action").html("<table><tr><td><a href='#' onclick='GetTranportStaffInfoToUpdate(" + adminLoanList[i].ID + ")'>Edit</a></td></tr><tr><td><a href='#' onclick='GetTranportStaffInfoToDelete(" + adminLoanList[i].ID + ")'>Delete</a></td></tr></table>");
                    $newRow.addClass(className);
                    $newRow.show();

                    if (i == 0) {
                        $("#rowStaffTemplate").replaceWith($newRow);
                    }
                    else {
                        $newRow.appendTo("#gridStaff > tbody");
                    }
                    $("#gridStaff").removeAttr("style");

                }
                grdTransportStaffDiscription = $('#gridStaff').DataTable(
                {
                    "bPaginate": true,
                    "iDisplayLength": 20,
                    "sPaginationType": "full_numbers",
                    "bAutoWidth": false,
                    "bLengthChange": false,
                    "bDestroy": true

                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetTranportStaffInfoToUpdate(stafID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetStaffInfoByStaffID",
        data: JSON.stringify({ staffID: parseInt(stafID) }),
        dataType: "json",
        success: function (responce) {
            var msg = responce.d;
            if (msg != undefined) {
                $("input[id*='txtStafftName']").val(msg.StaffName);
                $("input[id*='txtStaffFatherName']").val(msg.FatherName);
                $("input[id*='hdnStaffID']").val(msg.ID);
                $("select[id*='drpStaffType']").val(msg.StaffType);
                $("select[id*='drpPassenger']").val(2);
                $("#divStaffDetail").show();
                $("#divStudentDetail").hide();
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
        }
    });
}

function GetTranportStaffInfoToDelete(stafID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetStaffInfoToDeleteByStaffID",
        data: JSON.stringify({ staffID: stafID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                LoadTransportStaffInfo($("input[id*='hdnInchargeID']").val());
                $("#divTransportStudentDetails").hide();
                $("#divTransportStaffInfo").show();
                alert("Record has been Delete successfully");
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}