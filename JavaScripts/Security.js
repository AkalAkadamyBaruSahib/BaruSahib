$(document).ready(function () {
    $("#afileUploadQualification").hide();
    $("#afileUploadAppointment").hide();
    $("#afileUploadPCC").hide();
    $("#afileUploadFamilyRationCard").hide();
    $("#afileUploadExperience").hide();
    $("#afileUploadphoto").hide();
    $("input[id*='btnEdit'] ").hide();
   
    LoadSecurityEmployee(-1);
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
                $("input[id*='txtCutting']").val(rdata.Cutting);
                $("textarea[id*='txtAddress']").val(rdata.Address);
                $("select[id*='ddlZone']").val(rdata.ZoneID);
                $("select[id*='ddlAcademy']").val(rdata.AcaID);
                $("select[id*='ddlDesig']").val(rdata.DesigID);
                $("select[id*='ddlDept']").val(rdata.DeptID);
                $("select[id*='ddlEducation']").val(rdata.Education);
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



