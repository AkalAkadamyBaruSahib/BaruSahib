

var settings = {
    rows: 50,
    cols: 10,
    rowCssPrefix: 'row-',
    colCssPrefix: 'col-',
    seatWidth: 35,
    seatHeight: 35,
    seatCss: 'seat',
    selectedSeatCss: 'selectedSeat',
    selectingSeatCss: 'selectingSeat'
};

var bookedSeats = new Array();

$(document).ready(function () {

    $("#aIdentityProof").hide();
    $("#aPhoto").hide();
    $("#aAuthorityLetter").hide();
    $("#aRoomNumber").hide();
    $("#aPrmntProof").hide();
    $("#aPrmntPhoto").hide();
    $("#aPrmntAuthority").hide();
    $("#aPrmnentRoomNumber").hide();
    $("input[id*='hdnBuildingID']").val()
  
    $("input[id*='txtfirstDate']").datepicker({
        minDate: 0
    });
    $("input[id*='txtlastDate']").datepicker({
        minDate: 0
    });

   $('.seat').live('click', function () {
        if ($(this).hasClass(settings.selectedSeatCss)) {
            alert('This seat is already reserved');
        }
        else {
            $(this).toggleClass(settings.selectingSeatCss);
        }
   });

   //$('.selectedSeat').live('click', function () {
   //    $(this).toggleClass(settings.selectingSeatCss);
   //});

    $('#btnShow').click(function () {
        var str = [];
        $.each($('#place li.' + settings.selectedSeatCss + ' a, #place li.' + settings.selectingSeatCss + ' a'), function (index, value) {
            str.push($(this).attr('title'));
        });
    });

    $('#btnclose').click(function () {
        var str = [], item;
        var SelectedRoomNo = "";
        $.each($('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) {
            item = $(this).attr('title');
            SelectedRoomNo += ($(this)[0].text) + ",";
            str.push(item);
            

        });

        if (str.length > 0) {
            var selectedSeats = "";
            if ($("input[id*='hdnbookedSeats']").val() == "") {
                selectedSeats += str.join(',');
                $("input[id*='hdnbookedSeats']").val(selectedSeats);
            }
            else {
               // selectedSeats = $("input[id*='hdnbookedSeats']").val() + ",";
                selectedSeats += str.join(',');
                $("input[id*='hdnbookedSeats']").val(selectedSeats);
            }
            $("#aRoomNumber").show();
            $("#aPrmnentRoomNumber").show();
            SelectedRoomNo = SelectedRoomNo.substr(0, SelectedRoomNo.length - 1);
            $("#aRoomNumber").text("Allocated Rooms: " + SelectedRoomNo);
            $("#aPrmnentRoomNumber").text("Allocated Rooms: " + SelectedRoomNo);
        }

      
    });

    //LoadVisitors(-1);

    LoadBuildings();

    $("#drpbuilding").change(function (e) {

        $("input[id*='hdnBuildingID']").val($(this).val());

        $.when(getBookedRoomList($("input[id*='hdnBuildingID']").val())).then(GetAllRoomList($(this).val()));
    });
    $("#drpPrmntBuilding").change(function (e) {

        $("input[id*='hdnBuildingID']").val($(this).val());

        $.when(getBookedRoomList($("input[id*='hdnBuildingID']").val())).then(GetAllRoomList($(this).val()));
    });

    $("#aRoomNumber").click(function (e) {
        var BuildingID = $("input[id*='hdnBuildingID']").val();
        $.when(getBookedRoomList(BuildingID)).then(GetAllRoomList(BuildingID));
        return false;
    });

    $("#aPrmnentRoomNumber").click(function (e) {
        var BuildingID = $("input[id*='hdnBuildingID']").val();
        $.when(getBookedRoomList(BuildingID)).then(GetAllRoomList(BuildingID));
        return false;
    });

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
 
function EnableDisabledValidator()
{
    if (typeof Page_Validators != 'undefined') {
        for (i = 0; i <= Page_Validators.length; i++) {
            var vldGrp = null;
            if (Page_Validators[i] != null) {
                if (Page_Validators[i].controltovalidate == $("input[id*='fileUploadIdentity']")[0].id
                    || Page_Validators[i].controltovalidate == $("input[id*='fileUploadphoto']")[0].id) {
                    ValidatorEnable(Page_Validators[i], false);
                };
            }
        }
    };
}

function EnableDisabledPermantValidator() {
    if (typeof Page_Validators != 'undefined') {
        for (i = 0; i <= Page_Validators.length; i++) {
            var vldGrp = null;
            if (Page_Validators[i] != null) {
                if (Page_Validators[i].controltovalidate == $("input[id*='fileUploadPrmntPhoto']")[0].id) {
                    ValidatorEnable(Page_Validators[i], false);
                };
            }
        }
    };
}

function GetAllRoomList(selectedValue) {
    var BuildingName = $("#drpbuilding option:selected").text();
    var PermanentBuildingName = $("#drpPrmntBuilding option:selected").text();
    // Get All Rooms
    var VisitoryType = $("input[id*='hdnVisitorType']").val()

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/GetRoomList",
        data: JSON.stringify({ BuildingID: parseInt(selectedValue), VisitorType: parseInt(VisitoryType) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var seatNo = result.d;
                init(bookedSeats, seatNo);
                $('#divRoomNumbers').modal('show');
                $("#spnBuildingName").text(BuildingName);
                $("#spnBuildingName").text(PermanentBuildingName);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function getBookedRoomList(buildingID) {

    $.ajax({
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/GetBookedRoomList",
        data: JSON.stringify({ BuildingID: parseInt(buildingID) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var seatNo = result.d;

                bookedSeats = seatNo.split(',').map(function (item) {
                    return parseInt(item, 10);
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

var init = function (bookedSeats, seatNo) {
    var str = [], className;
    //for (i = 0; i < settings.rows; i++) {
    for (j = 0; j < seatNo.length; j++) {
        //seatNo = (i + j * settings.rows + 1);
        className = settings.seatCss + ' ' + settings.rowCssPrefix + j.toString() + ' ' + settings.colCssPrefix + j.toString();

        if ($.inArray(seatNo[j].ID, bookedSeats) != -1) {
            className += ' ' + settings.selectedSeatCss;
            if (seatNo[j].Number.indexOf("HALL") >= 0) {
                str.push('<li title="Number of Bed(s) : ' + seatNo[j].NumOfBed + '" class="seat">' + //+ className + '"' +
                     '<p><a title="' + seatNo[j].ID + '">' + seatNo[j].Number + '</a></p>' +
                  '</li>');
            }
            else {
                str.push('<li title="Number of Bed(s) : ' + seatNo[j].NumOfBed + '" class="selectedSeat">' + //+ className + '"' +
                    '<p><a title="' + seatNo[j].ID + '">' + seatNo[j].Number + '</a></p>' +
                       '</li>');
            }
        }
        else {
            str.push('<li title="Number of Bed(s) : ' + seatNo[j].NumOfBed + '" class="seat">' + //+ className + '"' +
                     '<p><a title="' + seatNo[j].ID + '">' + seatNo[j].Number + '</a></p>' +
                  '</li>');
        }

    }

    $('#place').html(str.join(''));
    
};

function LoadBuildings() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/GetBuildingNameList",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("#drpbuilding").append($("<option></option>").val(value.ID).html(value.Name));
                    $("#drpPrmntBuilding").append($("<option></option>").val(value.ID).html(value.Name));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function LoadVisitorByVisitorID(visitorID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/GetVisitorInfoByVisitorId",
        data: JSON.stringify({ VisitorID: visitorID }),
        dataType: "json",
        success: function (responce) {

            var msg = responce.d;

            if (msg != undefined) {
                if (msg.VisitorTypeID == 1) {
                    $("input[id*='hdnVisitorID']").val(msg.ID);
                    $("input[id*='txtName']").val(msg.Name);
                    $("input[id*='txtnoofperson']").val(msg.TotalNoOfMen);
                    $("input[id*='txtNoOfFemale']").val(msg.TotalNoOfWomen);
                    $("input[id*='txtNoOfChildren']").val(msg.TotalNoOfChildren);
                    $("input[id*='txtvehicle']").val(msg.VehicleNo);
                    $("select[id*='ddlpurpose']").val(msg.PurposeOfVisit);
                    $("select[id*='drpProofType']").val(msg.Identification);
                    $("select[id*='drpNumberOfDays']").val(msg.NoOfDaysToStay);
                    $("input[id*='txtContactNumber']").val(msg.ContactNumber);
                    $("textarea[id*='txtAddress']").val(msg.VisitorAddress);
                    $("input[id*='hdnBuildingID']").val(msg.BuildingID);
                    $("#drpbuilding").val(msg.BuildingID);
                    $("input[id*='txtlastDate']").val(msg.TimePeriodTo);
                    $("input[id*='txtfirstDate']").val(msg.TimePeriodFrom);
                    $("select[id*='drpCountry']").val(msg.Country);
                    BindState(msg.Country, msg.State, msg.City);
                    $("input[id*='txtReference']").val(msg.VisitorReference);
                    $("select[id*='ddlRoomRent']").val(msg.RoomRent);
                    $("input[id*='txtAdmissionNo']").val(msg.AdmissionNumber);
                    $("input[id*='txtPurposevisit']").val(msg.PurposeOfVisitRemarks);

                    var bookedRoom = "";
                    bookedRoom = GetVisitorRoomList(msg.ID);


                    $("#aIdentityProof").show();
                    $("#aIdentityProof").attr("href", msg.IdentificationPath);
                    $("#aPhoto").show();
                    $("#aPhoto").attr("href", msg.VisitorsPhoto);
                    $("#aAuthorityLetter").show();
                    $("#aAuthorityLetter").attr("href", msg.VisitorsAuthorityLetter);
                    $("#aRoomNumber").show();
                }
                else {
                    $("input[id*='hdnVisitorID']").val(msg.ID);
                    $("input[id*='txtPrmntName']").val(msg.Name);
                    $("select[id*='drpProofType']").val(msg.Identification);
                    $("input[id*='txtPrmntContactNo']").val(msg.ContactNumber);
                    $("textarea[id*='txtPrmntAddress']").val(msg.VisitorAddress);
                    $("input[id*='hdnBuildingID']").val(msg.BuildingID);
                    $("#divPrmntBuilding").val(msg.BuildingID);
                    $("select[id*='ddlntypeofvisitor']").val(msg.VisitorTypeID);
                    $("input[id*='txtprmntTo']").val(msg.TimePeriodTo);
                    $("input[id*='txtprmntFrom']").val(msg.TimePeriodFrom);
                    $("select[id*='ddlroomservice']").val(msg.RoomRentType);
                    $("select[id*='ddlelectricitybill']").val(msg.ElectricityBill);
                    $("select[id*='ddlPrmntIdntity']").val(msg.Identification);
                    $("select[id*='ddlPrmntCountry']").val(msg.Country);

                    BindState(msg.Country, msg.State, msg.City);

                    var bookedRoom = "";
                    bookedRoom = GetVisitorRoomList(msg.ID);


                    $("#aPrmntProof").show();
                    $("#aPrmntProof").attr("href", msg.IdentificationPath);
                    $("#aPrmntPhoto").show();
                    $("#aPrmntPhoto").attr("href", msg.VisitorsPhoto);
                    $("#aPrmntAuthority").show();
                    $("#aPrmntAuthority").attr("href", msg.VisitorsAuthorityLetter);
                    $("#aPrmnentRoomNumber").show();

                }
                $("input[id*='btnSave'] ").val("Update");

                if (msg.VisitorTypeID == 1) {
                    EnableDisabledValidator();
                    $("[id$='divVisitorInfo']").show();
                    $("[id$='divPrmanent']").hide();
                }
                else {
                    EnableDisabledPermantValidator();
                    $("[id$='divVisitorInfo']").hide();
                    $("[id$='divPrmanent']").show();
                }

                if (msg.PurposeOfVisit == "Parents Meeting") {
                    $("#divAdminsnNo").show();
                    $("#divPurposeVisit").hide();
                }
                else if(msg.PurposeOfVisit == "Others")
                {
                    $("#divPurposeVisit").show();
                    $("#divAdminsnNo").hide();
                }
                else
                {
                    $("#divPurposeVisit").hide();
                    $("#divAdminsnNo").hide();
                }
                if ($("input[id*='hdnUserType']").val() == "22") {
                    $("input[id*='txtnoofperson']").prop('disabled', true);
                    $("input[id*='txtNoOfFemale']").prop('disabled', true);
                    $("input[id*='txtNoOfChildren']").prop('disabled', true);
                    $("input[id*='txtvehicle']").prop('disabled', true);
                    $("select[id*='ddlpurpose']").prop('disabled', true);
                    $("select[id*='drpProofType']").prop('disabled', true);
                    $("select[id*='drpNumberOfDays']").prop('disabled', true);
                    $("select[id*='drpCountry']").prop('disabled', true);
                    $("select[id*='drpState']").prop('disabled', true);
                    $("select[id*='drpCity']").prop('disabled', true);
                    $("input[id*='txtReference']").prop('disabled', true);
                    $("select[id*='ddlRoomRent']").prop('disabled', true);
                    $("input[id*='txtAdmissionNo']").prop('disabled', true);
                    $("input[id*='txtPurposevisit']").prop('disabled', true);
                    $("input[id*='txtDlNumber']").prop('disabled', true);
                    $("select[id*='drpProofType']").prop('disabled', true);
                    $("select[id*='ddlntypeofvisitor']").prop('disabled', true);
                    $("select[id*='ddlroomservice']").prop('disabled', true);
                    $("select[id*='ddlelectricitybill']").prop('disabled', true);
                    $("select[id*='ddlPrmntIdntity']").prop('disabled', true);
                    $("select[id*='ddlPrmntCountry']").prop('disabled', true);
                    $("select[id*='ddlPrmntState']").prop('disabled', true);
                    $("select[id*='ddlPrmntCity']").prop('disabled', true);
           
                }
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
        }
    });
}

function GetVisitorRoomList(visitorID) {

    var hdnbookedSeats = "";
    var aRoomNumber = "";
    var roomnumbers = "";
    $.ajax({
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/GetBookedRoomsByVisitorID",
        data: JSON.stringify({ VisitorID: visitorID }),
        dataType: "json",
        success: function (responce) {
            roomnumbers = responce.d;
            for (var i = 0; i < roomnumbers.length; i++) {
                hdnbookedSeats += roomnumbers[i].ID + ",";
                aRoomNumber += roomnumbers[i].Number + ",";
            }
            hdnbookedSeats = hdnbookedSeats.substr(0, hdnbookedSeats.length - 1);
            aRoomNumber = aRoomNumber.substr(0, aRoomNumber.length - 1);
            $("#aRoomNumber").text("Allocated Rooms: " + aRoomNumber);
            $("#aPrmnentRoomNumber").text("Allocated Rooms: " + aRoomNumber);
            $("input[id*='hdnbookedSeats'] ").val(hdnbookedSeats);
        }
    });
    return roomnumbers;
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

function BindState(countryID,stateID,cityID) {
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

                $("input[id*='txtName']").val(msg.FatherName);
                $("select[id*='drpCountry']").val(msg.CountryID);
                $("textarea[id*='txtAddress']").val(msg.Address);
                BindState(msg.CountryID, msg.StateID, msg.CityID);
                $("input[id*='txtContactNumber']").val(msg.ContactNo);
                $("select[id*='ddlpurpose']").val("Parents Meeting");
                $("#divAdminsnNo").show();
                $("input[id*='txtAdmissionNo']").val(admissionNo);
                $("#lblStudentName").html(msg.StudentName);
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
