$(document).ready(function () {

    $("input[id*=chkAdminsnNumber]:checkbox").click(function () {
        if ($(this)[0].checked) {
            $("#trStudentSearch").show();
        }
        else {
            $("#trStudentSearch").hide();
        }
    });

    $('#btnSearch').click(function () {
        GetVisitorInfoByAdminsnNumber($("input[id*='txtAdmisnNo']").val());
    });

});

function GetVisitorInfoByAdminsnNumber(admissionNo) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/GetVisitorInfoByAdminsnNumber",
        data: JSON.stringify({ AdmissionNumber: parseInt(admissionNo) }),
        dataType: "json",
        success: function (responce) {
            var msg = responce.d;
            if (msg != undefined) {
                $("input[id*='txtAdmissionNumber']").val(msg.AdmissionNumber);
                $("input[id*='txtStudentName']").val(msg.StudentName);
                $("input[id*='txtFatherName']").val(msg.FatherName);
                $("input[id*='txtClass']").val(msg.Class);
                $("select[id*='drpCountry']").val(msg.CountryID);
                $("input[id*='txtAddress']").val(msg.Address);
                BindState(msg.CountryID, msg.StateID, msg.CityID);
                $("input[id*='txtContactNumber']").val(msg.ContactNo);
            }
            else {
                alert("Admission Number does not exit.");
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
        }
    });
}

function BindState(countryID, stateID, cityID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/GetStateByCountryID",
        data: JSON.stringify({ CountryID: parseInt(countryID) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("select[id*='drpState']").append($("<option></option>").val(value.StateId).html(value.StateName));
                    $("select[id*='ddlPrmntState']").append($("<option></option>").val(value.StateId).html(value.StateName));
                });
                $("select[id*='drpState']").val(stateID);
                $("select[id*='ddlPrmntState']").val(stateID);
                BindCity(stateID, cityID);

            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindCity(StateID, CityID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/GetCityByStateID",
        data: JSON.stringify({ stateID: parseInt(StateID) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("select[id*='drpCity']").append($("<option></option>").val(value.CityId).html(value.CityName));
                    $("select[id*='ddlPrmntCity']").append($("<option></option>").val(value.CityId).html(value.CityName));
                });
                $("select[id*='drpCity']").val(CityID);
                $("select[id*='ddlPrmntCity']").val(CityID);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}