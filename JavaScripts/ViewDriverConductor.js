$(document).ready(function () {
    $("select").searchable();
});


$(document).ready(function () {


    //LoadVisitors(-1);

    //LoadTransportEmployeeInfo(1);

    $("select[ID*='drpEmployeeType']").change(function (e) {
      //  var source = $(this)[0].id;
        // LoadTransportEmployeeInfo($(this).val());
        $("#trVhiclenumber").show();
    });

    $("select[ID*='ddlVehicleNumber']").change(function (e) {
        var source = $(this)[0].id;
        LoadTransportEmployeeInfo($("select[ID*='drpEmployeeType']").val(), ($(this).val()));
    });

});

function LoadTransportEmployeeInfo(selectedValue,vhicleNumbr) {

    /*create/distroy grid for the new search*/
    if (typeof grdTransportEmployeeDiscription != 'undefined') {
        grdTransportEmployeeDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="Name">abc</td><td id="DLValidity">abc</td><td id="CntactNoInCaseOfEmegeny">abc</td><td id="Qualification">cc</td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GeTransportEmployeeInformation",
        data: JSON.stringify({ EmployeeTypeID: selectedValue, VehicleID: vhicleNumbr }),
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
                    if (adminLoanList[i].EmployeeType == 1) {
                        EmployeeType = "Driver";
                    }
                    else if (adminLoanList[i].EmployeeType == 2) {
                        EmployeeType = "Conductor";
                    }

                    var $newRow = $("#rowTemplate").clone();
                    $newRow.find("#Name").html("<table><tr><td><b>Name :</b> " + adminLoanList[i].Name + "(" + EmployeeType + ")</td></tr><tr><td><b>Vehicle Number :</b> " + adminLoanList[i].VehicleNumber + "</td></tr><tr><td><b>Contact No:</b>" + adminLoanList[i].MobileNumber + "</td></tr><tr><td><b>Date Of Joining:</b>" + adminLoanList[i].DateOfJoining + "</td></tr></table>");
                    var DLScanLink = OpenDLCopy(adminLoanList[i].DLScanCopy);
                    $newRow.find("#DLValidity").html("<table><tr><td><b>DL Validity :</b> " + adminLoanList[i].DLValidity + "</td></tr><tr><td><b>DL Number:</b>" + adminLoanList[i].DLNumber + "</td></tr><tr><td>" + DLScanLink + "</td></tr></table>");
                    $newRow.find("#CntactNoInCaseOfEmegeny").html(adminLoanList[i].ContactNoInCaseOfEmergency);
                    var link = OpenImages(adminLoanList[i].Qualification);
                    var Applicationlink = OpenApplicationForm(adminLoanList[i].ApplicationForm);
                    var url = location.href.substring(0, location.href.lastIndexOf("/") + 1);

                    $newRow.find("#Qualification").html("<table><tr><td><a href='" + url + "Transport_AddNewDriver.aspx?TransportEmployeeID=" + adminLoanList[i].ID + "'>Edit</a></td></tr><tr><td><a href='#' onclick='TranportEmployeeInfoToDelete(" + adminLoanList[i].ID + ")'>Delete</a></td></tr><tr><td>" + link + "</td></tr><tr><td>" + Applicationlink + "</td></tr></table>");
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
                grdTransportEmployeeDiscription = $('#grid').DataTable(
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


function GetTranportEmployeeInfoToUpdate(vehicleEmployeeID) {
    $("select[id*='drpEmployeeType']").prop('disabled', true);
    $("input[id*='btnSave'] ").hide();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/GetTranportEmployeeInfoToUpdate",
        data: JSON.stringify({ VehicleEmployeeID: vehicleEmployeeID }),
        dataType: "json",
        success: function (responce) {

            var rdata = responce.d;

            if (rdata != undefined) {


                $("input[id*='hdnvehicleEmployeeID']").val(rdata.ID);
                $("select[id*='drpEmployeeType']").val(rdata.EmployeeType);
                $("input[id*='txtName']").val(rdata.Name);
                $("input[id*='txtMobileNo']").val(rdata.MobileNumber);
                $("select[id*='drpDlType']").val(rdata.DLType);
                $("input[id*='txtdlvalidity']").val(rdata.DLValidity);
                $("textarea[id*='txtAddress']").val(rdata.Address);
                $("input[id*='txtFatherName']").val(rdata.FatherName);
                $("input[id*='txtDateOfBirth']").val(rdata.DateOfBirth);
                $("input[id*='txtDlNumber']").val(rdata.DLNumber);
                $("input[id*='txtDateOfJoin']").val(rdata.DateOfJoining);
                $("input[id*='txtEmergencyContactNo']").val(rdata.ContactNoInCaseOfEmergency);
                $("input[id*='txtNameOfTheComp']").val(rdata.PreviousCompanyName);
                $("select[id*='ddlyear']").val(rdata.ExperienceInYear);
                $("select[id*='ddlmonth']").val(rdata.ExperienceInMonth);
                $("select[id*='drpTransportType']").val(rdata.TransportTypeID);
                $("select[id*='ddlVehicleNumber']").val(rdata.VehicleID);
                if (rdata.Qualification != null) {
                    $("#aQualification").show();
                    var qualificationPics = rdata.Qualification.split(',');
                    var link = "";
                    for (var i = 0; i < qualificationPics.length; i++) {
                        link += " <a href='" + qualificationPics[i] + "' target='_blank'>Qualification_" + (i + 1) + "</a>";
                    }
                    $("#aQualification")[0].innerHTML = link;
                }
                if (rdata.DLScanCopy != null) {
                    $("#afileUploadDlValidity").show();
                    var dlPics = rdata.DLScanCopy.split(',');
                    var DLlink = "";
                    for (var i = 0; i < dlPics.length; i++) {
                        DLlink += " <a href='" + dlPics[i] + "' target='_blank'>DLScanCopy_" + (i + 1) + "</a>";
                    }
                    $("#afileUploadDlValidity")[0].innerHTML = DLlink;
                }
                if (rdata.ApplicationForm != null) {
                    $("#afileUploadApplicationForm").show();
                    var ApplicationFormPics = rdata.ApplicationForm.split(',');
                    var linkForm = "";
                    for (var i = 0; i < ApplicationFormPics.length; i++) {
                        linkForm += " <a href='" + ApplicationFormPics[i] + "' target='_blank'>ApplicationForm_" + (i + 1) + "</a>";
                    }
                    $("#afileUploadApplicationForm")[0].innerHTML = linkForm;
                }
                var familyrow = 1;
                var Referencerow = 1;
                for (var i = 0; i < rdata.TransportEmployeeRelationDTO.length; i++) {
                    if (rdata.TransportEmployeeRelationDTO[i].RelationTypeID == 1) {

                        if (familyrow > 1) {
                            addFamilyRow();
                        }

                        $("input[id*='txtFamilyName" + familyrow + "']").val(rdata.TransportEmployeeRelationDTO[i].Name);
                        $("input[id*='txtFamilyAge" + familyrow + "']").val(rdata.TransportEmployeeRelationDTO[i].Age);
                        $("select[id*='ddlFamilyRelation" + familyrow + "']").val(rdata.TransportEmployeeRelationDTO[i].Relation);
                        $("input[id*='chkNominee" + familyrow + "']").prop("checked", rdata.TransportEmployeeRelationDTO[i].Nominee);
                        familyrow = parseInt(familyrow) + 1;
                    }
                    else {

                        if (Referencerow > 1) {
                            addRefernceRow();
                        }

                        $("input[id*='txtRefName" + Referencerow + "']").val(rdata.TransportEmployeeRelationDTO[i].Name);
                        $("input[id*='txtRefRelation" + Referencerow + "']").val(rdata.TransportEmployeeRelationDTO[i].Relation);
                        $("input[id*='txtRefPhoneNo" + Referencerow + "']").val(rdata.TransportEmployeeRelationDTO[i].PhoneNo);
                        $("input[id*='txtRefAddress" + Referencerow + "']").val(rdata.TransportEmployeeRelationDTO[i].Address);
                        Referencerow = parseInt(Referencerow) + 1;
                    }
                }
                $("input[id*='btnEdit'] ").show();
                DisableDLControl();
                DisableControl();
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
        }
    });
}


function TranportEmployeeInfoToDelete(vehicleEmployeeID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/TransportController.asmx/TranportEmployeeInfoToDelete",
        data: JSON.stringify({ VehicleEmployeeID: vehicleEmployeeID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                LoadTransportEmployeeInfo();
                alert("Record has been Delete successfully");
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}


function OpenImages(quals) {
    var qualificationPics = quals.split(',');
    var link = "";
    for (var i = 0; i < qualificationPics.length; i++) {
        link += " <a href='" + qualificationPics[i] + "' target='_blank'>Qualification_" + (i + 1) + "</a>";
    }
    return link;
}

function OpenApplicationForm(application) {
    var applicationPics = application.split(',');
    var applicationlink = "";
    for (var i = 0; i < applicationPics.length; i++) {
        applicationlink += " <a href='" + applicationPics[i] + "' target='_blank'>ApplicationForm_" + (i + 1) + "</a>";
    }
    return applicationlink;
}

function OpenDLCopy(dlscan) {
    var dlScanPics = dlscan.split(',');
    var dllink = "";
    for (var i = 0; i < dlScanPics.length; i++) {
        dllink += " <a href='" + dlScanPics[i] + "' target='_blank'>DLScanCopy_" + (i + 1) + "</a>";
    }
    return dllink;
}
