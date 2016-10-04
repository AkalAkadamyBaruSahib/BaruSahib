var d = new Date();
var CurrentDate = d.getDate() + "/" + (d.getMonth() + 1) + "/" +  d.getFullYear() ;

$(document).ready(function () {
 
    $("#btnUploadSave").click(function () {
        SaveEmpTransferInfo();
    });

    BindZone();

    $("select[id*='ddlZone']").change(function () {
        BindAcademybyZoneID($(this).val());
    });
});
function LoadSecurityEmployee() {

    /*create/distroy grid for the new search*/
    if (typeof grdTicketDiscription != 'undefined') {
        grdTicketDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="Name">abc</td><td id="Address">abc</td><td id="ContactNo">abc</td><td id="Education">cc</td><td id="Qualification"></td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/SecurityController.asmx/GetSecurityEmployeeInformation",
        data: JSON.stringify({ SecurityEmployeeID:1 }),
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

                    $newRow.find("#Name").html("<table><tr><td><li id='image-1' class='thumbnail'><a target='_blank' style='background:url(" + adminLoanList[i].Photo + ")'  href='" + adminLoanList[i].Photo + "'><img class='grayscale' width='75Px' height='75PX' src='" + adminLoanList[i].Photo + "' ></a></li></ul> </td></tr><tr><td><b>Name :</b> " + adminLoanList[i].Name + "</td></tr></table>");
                    $newRow.find("#Address").html(adminLoanList[i].Address);
                    $newRow.find("#ContactNo").html(adminLoanList[i].MobileNo);
                    $newRow.find("#Education").html("<table><tr><td><b>Qualifiaction :</b> " + adminLoanList[i].Education + "</td></tr><tr><td><a href='#' onclick='openLinkDailog(\"" + adminLoanList[i].QualificationLetter + "\",\"" + adminLoanList[i].Name + "\")'>Qualification Scan Copy</a></td></tr></table>");
                    $newRow.find("#Qualification").html("<table><tr><td><a href='#' onclick='GetSecurityEmployeeInfoToUpdate(" + adminLoanList[i].ID + ")'>Edit</a></td></tr><tr><td><a href='#' onclick='SecurityEmployeeInfoToDelete(" + adminLoanList[i].ID + ")'>Delete</a></td></tr><tr><td><a href='#' onclick='openLinkDailog(\"" + adminLoanList[i].AppointmentLetter + "\",\"" + adminLoanList[i].Name + "\")'>Appointment Letter</a></td></tr><tr><td><a href='#' onclick='openLinkDailog(\"" + adminLoanList[i].ExperienceLetter + "\",\"" + adminLoanList[i].Name + "\")'>Experience Letter</a></td></tr><tr><td><a href='#' onclick='openLinkDailog(\"" + adminLoanList[i].PCC + "\",\"" + adminLoanList[i].Name + "\")'>PCC Letter</a></td></tr><tr><td><a href='#' onclick='openLinkDailog(\"" + adminLoanList[i].FamilyRationCard + "\",\"" + adminLoanList[i].Name + "\")'>Family Rashan Card</a></td></tr></table>");
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
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function openLinkDailog(src, name, doc) {
    $("#spnName").text(name);
    $("#spanidentityfication").text(doc);
    OpenDailogLink(src);
}

function OpenDailogLink(src) {
    $('#iframeDailog').attr("src", src);
    $('#myModal').modal('show');
}

function SecurityEmployeeInfoToDelete(securityEmployeeID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/SecurityController.asmx/SecurityEmployeeInfoToDelete",
        data: JSON.stringify({ SecurityEmployeeID: securityEmployeeID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                LoadSecurityEmployee();
                alert("Record has been Delete successfully");
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function GetSecurityEmployeeInfoToUpdate(securityEmployeeID) {
    var rdata = "";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/SecurityController.asmx/GetSecurityEmployeeInfoToUpdate",
        data: JSON.stringify({ SecurityEmployeeID: securityEmployeeID }),
        dataType: "json",
        success: function (responce) {
            rdata = responce.d;
            if (rdata != undefined) {
                $("input[id*='hdnsecurityEmployeeID']").val(rdata.ID);
                $("input[id*='txtName']").val(rdata.Name);
                $("input[id*='txtMobileNo']").val(rdata.MobileNo);
                $("input[id*='txtSalary']").val(rdata.Salary);
                $("input[id*='txtCutting']").val(rdata.Deduction);
                $("textarea[id*='txtAddress']").val(rdata.Address);
                $("select[id*='ddlZone']").val(rdata.ZoneID);
                BindAcademybyZoneID(rdata.ZoneID);
                $("select[id*='ddlAcademy']").val(rdata.AcaID);
                $("select[id*='ddlDesig']").val(rdata.DesigID);
                $("select[id*='ddlEducation']").val(rdata.Education);
                $("select[id*='txtDateofJoining']").val(rdata.DOJ);
                $("select[id*='txtDateofAppraisal']").val(rdata.DateOfAppraisal);
                $("input[id*='txtLastAppraisal']").val(rdata.LastAppraisal);
                $("#afileUploadQualification").show();
                $("#afileUploadQualification").attr("href", rdata.QualificationLetter);
                $("#afileUploadAppointment").show();
                $("#afileUploadAppointment").attr("href", rdata.AppointmentLetter);
                $("#afileUploadPCC").show();
                $("#afileUploadPCC").attr("href", rdata.PCC);
                $("#afileUploadFamilyRationCard").show();
                $("#afileUploadFamilyRationCard").attr("href", rdata.FamilyRationCard);
                $("#afileUploadExperience").show();
                $("#afileUploadExperience").attr("href", rdata.ExperienceLetter);
                $("#afileUploadphoto").show();
                $("#afileUploadphoto").attr("href", rdata.Photo);

                $("input[id*='btnSave'] ").val("Update");
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
        }
    });
}

function BindZone() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/SecurityController.asmx/GetZone",
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
        url: "Services/SecurityController.asmx/GetAcademybyZoneID",
        data: JSON.stringify({ ZoneID: parseInt(selctZoneID) }),
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $("select[id*='ddlAcademy']").append($("<option></option>").val("0").html("--Select Academy--"));
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

function OpenTransferEmployee(EmpID, ZoneID, AcaID, Name) {
    $("input[id*='hdnEmpID']").val(EmpID);
    $("input[id*='hdnZoneId']").val(ZoneID);
    $("input[id*='hdnAcaId']").val(AcaID);
    $("input[id*='hdnName']").val(Name);
    $("#divTransferEmployee").modal('show');
   
}

function SaveEmpTransferInfo() {
    var paramBill = new Object();

    var EmployeeTransfer = new Object();

    var hdnEmpID = $("input[id*='hdnEmpID']").val();
    var hdnZoneID = $("input[id*='hdnZoneId']").val();
    var hdnAcaID = $("input[id*='hdnAcaId']").val();
    EmployeeTransfer.EmpID = hdnEmpID;
    EmployeeTransfer.OldAcaID = hdnAcaID;
    EmployeeTransfer.OldZoneID = hdnZoneID;
    EmployeeTransfer.NewAcaID = $("select[id*='ddlAcademy']").val();
    EmployeeTransfer.NewZoneID = $("select[id*='ddlZone']").val();
    EmployeeTransfer.DateOfTransfer = CurrentDate;
    EmployeeTransfer.CreatedBy = $("input[id*='hdnInchargeID']").val();


    var ext = $("#uploadeTransferLetter")[0].files[0].name.split('.').pop();
    var dates = "";
    for (var i = 0; i < CurrentDate.length; i++)
    {
        dates += CurrentDate[i].replace('/', '');
    }
    
    var transferLetter = $("input[id*='hdnName']").val() + "_" + dates + "." + ext;
    EmployeeTransfer.TransferLatter = "SecurityAppointmentLetter/TransferLetters/" + transferLetter;


    paramBill.EmployeeTransfer = EmployeeTransfer;

    $.ajax({
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "Services/SecurityController.asmx/SaveSecurityTransferLetter",
        data: JSON.stringify(paramBill),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                TransferLetterUpload(transferLetter);

            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}


function TransferLetterUpload(filename) {
    var files = $("#uploadeTransferLetter")[0].files;
    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }

    $.ajax({
        url: "TransferLetterHandler.ashx?name=" + filename,
        type: "POST",
        async: false,
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            $("#divTransferEmployee").modal('hide');
        },
        error: function (err) {
            alert(err.statusText)
        }
    });
}