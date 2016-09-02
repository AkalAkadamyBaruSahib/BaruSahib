var cnt = 2;
var cntR = 2;
//$(document).ready(function () {

//    $("select").searchable();
//});

$(document).ready(function () {

    $("select[id*='drpEmployeeType']").change(function () {
        DisableDLControl();
    });

    $("select[id*='drpTransportType']").change(function () {
        DisableControl();
    });
    $("input[id*='txtDateOfBirth']").datepicker({
        maxDate: 0
    });
    $("input[id*='txtDateOfJoin']").datepicker({
        maxDate: 0
    });
    $("input[id*='txtdlvalidity']").datepicker({
        minDate: 0
    });

    $("#aQualification").hide();

    $("#afileUploadDlValidity").hide();

    $("#afileUploadApplicationForm").hide();

    $("input[id*='btnEdit'] ").hide();


    LoadTransportEmployeeInfo();

    $("input[id$='btnSaveChanges']").click(function () {
        var txt;
        var r = confirm("Is the Information you added is correct?");
        if (r == true) {
            return true;
        } else {
            return false;
        }

    });
    $("#btnEdit").click(function (e) {
        if (Page_ClientValidate("driver")) {
            ClientSideClick(this);
            UpdateTansportEmployeeInfo();
            return false;
        }
    });
   
    $("#anc_add").click(function () {
        $('#tbl1 tr').last().after('<tr><td>Static Content [' + cnt + ']</td><td><input type="text" name="txtbx' + cnt + '" value="' + cnt + '"></td></tr>'); // adding new tr after last tr of table
        cnt++; // incrementing variable cnt by 1
    });
    $("#anc_rem").click(function () {
        $('#tbl1 tr:last-child').remove();
    });
    $("#btnSave").click(function (e) {
        if (Page_ClientValidate("driver")) {
            ClientSideClick(this);
            var params = new Object();
            var VehicleEmployee = new Object();
            VehicleEmployee.EmployeeType = $("select[id*='drpEmployeeType']").val();
            VehicleEmployee.Name = $("input[id*='txtName']").val();
            VehicleEmployee.MobileNumber = $("input[id*='txtMobileNo']").val();
            VehicleEmployee.DLType = $("select[id*='drpDlType']").val();
            VehicleEmployee.DLValidity = $("input[id*='txtdlvalidity']").val();
            VehicleEmployee.Address = $("textarea[id*='txtAddress']").val();
            VehicleEmployee.FatherName = $("input[id*='txtFatherName']").val();
            VehicleEmployee.DateOfBirth = $("input[id*='txtDateOfBirth']").val();
            var fileUploadQualification = $("#fileUploadQualification")[0].files;

            if ($("#fileUploadQualification")[0].files != undefined) {
                VehicleEmployee.Qualification = "";
                for (var i = 0; i < fileUploadQualification.length; i++) {

                    VehicleEmployee.Qualification += "TransportEmployeeQualification/" + $("#fileUploadQualification")[0].files[i].name + ",";
                }
                VehicleEmployee.Qualification = VehicleEmployee.Qualification.substr(0, VehicleEmployee.Qualification.length - 1);
            }

            VehicleEmployee.DLNumber = $("input[id*='txtDlNumber']").val();

            var fileUploadDlValidity = $("#fileUploadDlValidity")[0].files;
            if ($("#fileUploadDlValidity")[0].files != undefined) {
                VehicleEmployee.DLScanCopy = "";
                for (var i = 0; i < fileUploadDlValidity.length; i++) {

                    VehicleEmployee.DLScanCopy += "TransportDLScanCopy/" + $("#fileUploadDlValidity")[0].files[i].name + ",";
                }
                VehicleEmployee.DLScanCopy = VehicleEmployee.DLScanCopy.substr(0, VehicleEmployee.DLScanCopy.length - 1);
            }

            VehicleEmployee.DateOfJoining = $("input[id*='txtDateOfJoin']").val();

            var fileUploadApplicationForm = $("#fileUploadApplicationForm")[0].files;
            if ($("#fileUploadApplicationForm")[0].files != undefined) {
                VehicleEmployee.ApplicationForm = "";
                for (var i = 0; i < fileUploadApplicationForm.length; i++) {

                    VehicleEmployee.ApplicationForm += "TransportApplicationForm/" + $("#fileUploadApplicationForm")[0].files[i].name + ",";
                }
                VehicleEmployee.ApplicationForm = VehicleEmployee.ApplicationForm.substr(0, VehicleEmployee.ApplicationForm.length - 1);
            }
            VehicleEmployee.ContactNoInCaseOfEmergency = $("input[id*='txtEmergencyContactNo']").val();
            VehicleEmployee.PreviousCompanyName = $("input[id*='txtNameOfTheComp']").val();
            VehicleEmployee.ExperienceInYear = $("select[id*='ddlyear']").val();
            VehicleEmployee.ExperienceInMonth = $("select[id*='ddlmonth']").val();
            VehicleEmployee.TransportTypeID = $("select[id*='drpTransportType']").val();
            VehicleEmployee.VehicleID = $("select[id*='ddlVehicleNumber']").val();
            params.VehicleEmployee = VehicleEmployee;



            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Services/TransportController.asmx/SaveNewTransportEmployee",
                data: JSON.stringify(params),
                dataType: "json",
                success: function (result, textStatus) {
                    if (textStatus == "success") {
                        var VehicleEmployeeID = result.d;
                        SaveFamilyTransEmpRelation(VehicleEmployeeID);
                        SaveReferenceTransEmpRelation(VehicleEmployeeID);
                        DLScanCopyUpload();
                        ApplicationFormUpload();
                        QualificationFileUpload();
                        LoadTransportEmployeeInfo();
                        alert("Record has been Saved successfully");
                        $("#btnSave").val("Save");
                        $("#btnSave").prop('disabled', false);
                        ClearTextBox();

                    }
                },
                error: function (result, textStatus) {
                    alert(result.responseText)
                }
            });
        }
    });
    $("#btnClose").click(function () {
        $("div[id*='divEstimateDetails']").show();
        $("#divTransportGps").hide();
    });
});
    
    function DisableDLControl() {
    //Get DropDownList selected value
    var selectedValue = $("select[id*='drpEmployeeType']").val();

    //Enable Controls
    if (selectedValue == "1") {
        $("input[id*='txtDlNumber']").prop('disabled', false);
        $("#fileUploadDlValidity").prop('disabled', false);
        $("select[id*='drpDlType']").prop('disabled', false);
        $("input[id*='txtdlvalidity']").prop('disabled', false);
    }
        //Disable Controls
    else {

        $("input[id*='txtDlNumber']").prop('disabled', true);
        $("#fileUploadDlValidity").prop('disabled', true);
        $("select[id*='drpDlType']").prop('disabled', true);
        $("input[id*='txtdlvalidity']").prop('disabled', true);
    }
    }

    function addFamilyRow(){
        $('#tblFamilyMembrDetail tr').last().after('<tr id="tr' + cnt + '"><td><input type="text" style="width: 192px;height: 18px;" id="txtFamilyName' + cnt + '" name="txtFamilyName' + cnt + '" value=""></td><td style="float: left; width: 174px;"><input type="text" Style="width: 62px; height: 18px;"  id="txtFamilyAge' + cnt + '" name="txtFamilyAge' + cnt + '" value=""></td><td  style="float: left;  width: 83px;"><select name="actFamilyRelation' + cnt + '" id="ddlFamilyRelation' + cnt + '" Style="width: 170px; height: 25px;" ><option value="0">--Select One--</option> <option value="Father">Father</option><option value="Mother">Mother</option><option value="Wife">Wife</option><option value="Brother">Brother</option><option value="Sister">Sister</option></select></td><td style="float: right; width: 230px;"><input type="checkbox" id="chkNominee' + cnt + '" name="chkNominee' + cnt + '" value="" style=" width: 16px;height: 16px;"></td><td><a href="javascript:void(0);" onclick="removeRow(' + cnt + ');">Delete</a></td></tr>');
    cnt++;
}

    function addRefernceRow() {
    $('#tblRefernceDetail tr').last().after('<tr id="trR' + cntR + '"><td><input type="text" style=" width: 192px;height: 18px;" id="txtRefName' + cntR + '" name="txtRefName' + cntR + '" value=""></td><td><input type="text" style=" width: 192px;height: 18px;"  id="txtRefAddress' + cntR + '" name="txtRefAddress' + cntR + '" value=""></td><td><input type="text" style=" width: 192px;height: 18px;"  id="txtRefPhoneNo' + cntR + '" name="txtRefPhoneNo' + cntR + '" value=""></td><td><input type="text" style=" width: 192px;height: 18px;"  id="txtRefRelation' + cntR + '" name="txtRefRelation' + cntR + '" value=""></td><td><a href="javascript:void(0);" onclick="removeRowReference(' + cntR + ');">Delete</a></td></tr>');
    cntR++;
}
   

    function DLScanCopyUpload() {
        var files = $("#fileUploadDlValidity")[0].files;

        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }

        $.ajax({
            url: "TransportDLScanCopyHandler.ashx",
            type: "POST",
            async: false,
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                //alert(result);
            },
            error: function (err) {
                alert(err.statusText)
            }
        });
    }

    function ApplicationFormUpload() {
        var files = $("#fileUploadApplicationForm")[0].files;

        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }

        $.ajax({
            url: "TransportApllicationFormHandler.ashx",
            type: "POST",
            async: false,
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                //alert(result);
            },
            error: function (err) {
                alert(err.statusText)
            }
        });
    }

    function QualificationFileUpload() {

        var files = $("#fileUploadQualification")[0].files;

        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }

        $.ajax({
            url: "TransportEmployeeQualificationHandler.ashx",
            type: "POST",
            async: false,
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                //alert(result);
            },
            error: function (err) {
                alert(err.statusText)
            }
        });
    }

    function LoadTransportEmployeeInfo() {

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
            data: JSON.stringify({ VehicleEmployeeID: 1 }),
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
                        $newRow.find("#Qualification").html("<table><tr><td><a href='#' onclick='GetTranportEmployeeInfoToUpdate(" + adminLoanList[i].ID + ")'>Edit</a></td></tr><tr><td><a href='#' onclick='TranportEmployeeInfoToDelete(" + adminLoanList[i].ID + ")'>Delete</a></td></tr><tr><td>" + link + "</td></tr><tr><td>" + Applicationlink + "</td></tr></table>");
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

    function ValidateUploadFile() {
        if ($("#fileUploadQualification").val() == "" || $("#fileUploadDlValidity").val() == "" || $("#fileUploadApplicationForm").val() == "") {
            alert("Please Upload The File");
            return false;
        }

        else { return false; }
    }

    function ValidateOptions() {
        return {
            // Specify the validation rules
            rules: {
                fileUploadQualification: "required",
                fileUploadDlValidity: "required",
                fileUploadApplicationForm: "required"
            },

            // Specify the validation error messages
            messages: {
                fileUploadQualification: "Please Upload The Qualification File",
                fileUploadQualification: "Please Upload The Dl Scan Copy",
                fileUploadQualification: "Please Upload The Application Form"
            },

        }
    }

    function removeRow(removeNum) {
        $('#tr' + removeNum).remove();
        cnt--;
    }

    function removeRowReference(removeNum) {
        $('#trR' + removeNum).remove();
        cntR--;
    }

    function show(input) {
        if (input.files && input.files[0]) {
            var filerdr = new FileReader();
            filerdr.onload = function (e) {
                $('#user_img').attr('src', e.target.result);
            }
            filerdr.readAsDataURL(input.files[0]);
        }
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
   
    function GetTranportEmployeeInfoToUpdate(vehicleEmployeeID) {
        
        $("#divemptype").hide();
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

    function SaveFamilyTransEmpRelation(VehicleEmployeeID) {
        for (var i = 1; i < cnt; i++) {

            var paramFamily = new Object();

            var TransportEmployeeRelation = new Object();

            TransportEmployeeRelation.VehicleEmployeeID = VehicleEmployeeID;
            TransportEmployeeRelation.RelationTypeID = 1;
            TransportEmployeeRelation.Name = $("input[id*='txtFamilyName" + i + "']").val();
            TransportEmployeeRelation.Age = $("input[id*='txtFamilyAge" + i + "']").val();
            TransportEmployeeRelation.Relation = $("select[id*='ddlFamilyRelation" + i + "']").val();
            TransportEmployeeRelation.Nominee = $("input[id*='chkNominee" + i + "']").is(":checked");

            paramFamily.TransportEmployeeRelation = TransportEmployeeRelation;

            $.ajax({
                type: "POST",
                async: false,
                contentType: "application/json; charset=utf-8",
                url: "Services/TransportController.asmx/SaveTransEmpRelation",
                data: JSON.stringify(paramFamily),
                dataType: "json",
                success: function (result, textStatus) {
                    if (textStatus == "success") {

                    }
                },
                error: function (result, textStatus) {
                    alert(result.responseText)
                }
            });
        }
    }

    function SaveReferenceTransEmpRelation(VehicleEmployeeID) {

        for (var i = 1; i < cntR; i++) {

            var paramRefrnce = new Object();

            var TransportEmployeeRelation = new Object();

            TransportEmployeeRelation.VehicleEmployeeID = VehicleEmployeeID;
            TransportEmployeeRelation.RelationTypeID = 2;
            TransportEmployeeRelation.Name = $("input[id*='txtRefName" + i + "']").val();
            TransportEmployeeRelation.Relation = $("input[id*='txtRefRelation" + i + "']").val();
            TransportEmployeeRelation.PhoneNo = $("input[id*='txtRefPhoneNo" + i + "']").val();
            TransportEmployeeRelation.Address = $("input[id*='txtRefAddress" + i + "']").val();

            paramRefrnce.TransportEmployeeRelation = TransportEmployeeRelation;

            $.ajax({
                type: "POST",
                async: false,
                contentType: "application/json; charset=utf-8",
                url: "Services/TransportController.asmx/SaveTransEmpRelation",
                data: JSON.stringify(paramRefrnce),
                dataType: "json",
                success: function (result, textStatus) {
                    if (textStatus == "success") {

                    }
                },
                error: function (result, textStatus) {
                    alert(result.responseText)
                }
            });
        }
    }

    function UpdateTansportEmployeeInfo() {

        var updateParams = new Object();
        var vehicleEmployee = new Object();

        vehicleEmployee.ID = $("input[id*='hdnvehicleEmployeeID']").val();
        vehicleEmployee.EmployeeType = $("select[id*='drpEmployeeType']").val();
        vehicleEmployee.Name = $("input[id*='txtName']").val();
        vehicleEmployee.MobileNumber = $("input[id*='txtMobileNo']").val();
        vehicleEmployee.DLType = $("select[id*='drpDlType']").val();
        vehicleEmployee.DLValidity = $("input[id*='txtdlvalidity']").val();
        vehicleEmployee.Address = $("textarea[id*='txtAddress']").val();
        vehicleEmployee.FatherName = $("input[id*='txtFatherName']").val();
        vehicleEmployee.DateOfBirth = $("input[id*='txtDateOfBirth']").val();

        var fileUploadQualification = $("#fileUploadQualification")[0].files;
        if ($("#fileUploadQualification")[0].files[0] != undefined) {
            vehicleEmployee.Qualification = "";
            for (var i = 0; i < fileUploadQualification.length; i++) {
                vehicleEmployee.Qualification += "TransportEmployeeQualification/" + $("#fileUploadQualification")[0].files[i].name + ",";
            }
            vehicleEmployee.Qualification = vehicleEmployee.Qualification.substr(0, vehicleEmployee.Qualification.length - 1);
        }
        vehicleEmployee.DLNumber = $("input[id*='txtDlNumber']").val();
        var fileUploadDlValidity = $("#fileUploadDlValidity")[0].files;
        if ($("#fileUploadDlValidity")[0].files[0] != undefined) {
            vehicleEmployee.DLScanCopy = "";
            for (var i = 0; i < fileUploadDlValidity.length; i++) {
               
                vehicleEmployee.DLScanCopy += "TransportDLScanCopy/" + $("#fileUploadDlValidity")[0].files[i].name + ",";
            }
            vehicleEmployee.DLScanCopy = vehicleEmployee.DLScanCopy.substr(0, vehicleEmployee.DLScanCopy.length - 1);
        }
        vehicleEmployee.DateOfJoining = $("input[id*='txtDateOfJoin']").val();

        var fileUploadApplicationForm = $("#fileUploadApplicationForm")[0].files;
        if ($("#fileUploadApplicationForm")[0].files[0] != undefined) {
            vehicleEmployee.ApplicationForm = "";
            for (var i = 0; i < fileUploadApplicationForm.length; i++) {
                vehicleEmployee.ApplicationForm += "TransportApplicationForm/" + $("#fileUploadApplicationForm")[0].files[i].name + ",";
            }
            vehicleEmployee.ApplicationForm = vehicleEmployee.ApplicationForm.substr(0, vehicleEmployee.ApplicationForm.length - 1);
        }

        vehicleEmployee.ContactNoInCaseOfEmergency = $("input[id*='txtEmergencyContactNo']").val();
        vehicleEmployee.PreviousCompanyName = $("input[id*='txtNameOfTheComp']").val();
        vehicleEmployee.ExperienceInYear = $("select[id*='ddlyear']").val();
        vehicleEmployee.ExperienceInMonth = $("select[id*='ddlmonth']").val();
        vehicleEmployee.TransportTypeID = $("select[id*='drpTransportType']").val();
        vehicleEmployee.VehicleID = $("select[id*='ddlVehicleNumber']").val();

        var transportEmployeeRelation = null;
        var arr = [];
        for (var i = 1; i < cnt; i++) {
            transportEmployeeRelation = new Object();
            transportEmployeeRelation.VehicleEmployeeID = vehicleEmployee.ID;
            transportEmployeeRelation.RelationTypeID = 1;
            transportEmployeeRelation.Name = $("input[id*='txtFamilyName" + i + "']").val();
            transportEmployeeRelation.Age = $("input[id*='txtFamilyAge" + i + "']").val();
            transportEmployeeRelation.Relation = $("select[id*='ddlFamilyRelation" + i + "']").val();
            transportEmployeeRelation.Nominee = $("input[id*='chkNominee" + i + "']").is(":checked");
            arr.push(transportEmployeeRelation);
        }

        for (var i = 1; i < cntR; i++) {
            transportEmployeeRelation = new Object();
            transportEmployeeRelation.VehicleEmployeeID = vehicleEmployee.ID;
            transportEmployeeRelation.RelationTypeID = 2;
            transportEmployeeRelation.Name = $("input[id*='txtRefName" + i + "']").val();
            transportEmployeeRelation.Relation = $("input[id*='txtRefRelation" + i + "']").val();
            transportEmployeeRelation.PhoneNo = $("input[id*='txtRefPhoneNo" + i + "']").val();
            transportEmployeeRelation.Address = $("input[id*='txtRefAddress" + i + "']").val();

            arr.push(transportEmployeeRelation);

        }

        vehicleEmployee.TransportEmployeeRelation = arr;
        updateParams.VehicleEmployee = vehicleEmployee;

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Services/TransportController.asmx/UpdateTransportEmployeeInfo",
            data: JSON.stringify(updateParams),
            dataType: "json",
            async: false,
            success: function (result, textStatus) {
                if (textStatus == "success") {
                    DLScanCopyUpload();
                    ApplicationFormUpload();
                    QualificationFileUpload();
                    LoadTransportEmployeeInfo();
                    alert("Record has been Upadte successfully");
                    ClearTextBox();
                } 
            },
            error: function (result, textStatus) {
                alert(result.responseText)
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

    function ClearTextBox() {

        $("input[id*='txtName']").val("");
        $("input[id*='txtMobileNo']").val("");
        $("input[id*='txtdlvalidity']").val("");
        $("textarea[id*='txtAddress']").val("");
        $("input[id*='txtDateOfBirth']").val("");
        $("input[id*='txtDlNumber']").val("");
        $("input[id*='txtDateOfJoin']").val("");
        $("input[id*='txtEmergencyContactNo']").val("");
        $("input[id*='txtNameOfTheComp']").val("");
        $("select[id*='drpDlType']").empty();
        $("select[id*='drpEmployeeType']").empty();
        $("select[id*='drpTransportType']").empty();
        $("select[id*='ddlyear']").empty()
        $("select[id*='ddlmonth']").empty();
        $("input[id*='txtFamilyName']").val("");
        $("input[id*='txtFamilyAge']").val("");
        $("select[id*='ddlFamilyRelation']").val("");
        $("input[id*='chkNominee']").removeAttr('checked');
        $("input[id*='txtRefName']").val("");
        $("input[id*='txtRefRelation']").val("");
        $("input[id*='txtRefPhoneNo']").val("");
        $("input[id*='txtRefAddress']").val("");
        $("select[id*='ddlVehicleNumber']").empty();
        cnt = 2;
        cntR = 2;
    }

    function DisableControl() {
        //Get DropDownList selected value
        var selectedValue = $("select[id*='drpTransportType']").val();

        //Enable Controls
        if (selectedValue == "2") {
            $("#fileUploadQualification").prop('disabled', true);
            $("input[id*='txtDateOfJoin']").prop('disabled', true);
            $("#fileUploadApplicationForm").prop('disabled', true);
            $("textarea[id*='txtAddress']").prop('disabled', true);
            $("#fRefenceDetail").hide();
            $("#fFamilyDetail").hide();
            $("#fCompanyDetail").hide();
        }   //Disable Controls
        else {
            $("#fileUploadQualification").prop('disabled', false);
            $("input[id*='txtDateOfJoin']").prop('disabled', false);
            $("#fileUploadApplicationForm").prop('disabled', false);
            $("textarea[id*='txtAddress']").prop('disabled', false);
            $("#fRefenceDetail").show();
            $("#fFamilyDetail").show();
            $("#fCompanyDetail").show();
        }
    }

    function Validation() {
        if ($("select[id*='drpDlType']").val() == "undefined" || $("select[id*='drpDlType']").val() == "0") {
            alert("Please Select the DL Type");
            return false;
        }
        else if ($("input[id*='txtdlvalidity']").val() == "" || $("input[id*='txtdlvalidity']").val() == "0") {
            alert("Please Enter the DL Validity");
            return false;
        }
        else if ($("input[id*='txtDlNumber']").val() == "" || $("input[id*='txtDlNumber']").val() == "0") {
            alert("Please Enter the DL Number");
            return false;
        }
        return true;
    }
   

   

    
    


