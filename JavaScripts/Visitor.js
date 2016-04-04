

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
    $("input[id*='hdnBuildingID']").val()
  
    //$("input[id*='txtfirstDate']").datepicker({
    //    maxDate: 0
    //});
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
                selectedSeats = $("input[id*='hdnbookedSeats']").val() + ",";
                selectedSeats += str.join(',');
                $("input[id*='hdnbookedSeats']").val(selectedSeats);
            }
            $("#aRoomNumber").show();
            SelectedRoomNo = SelectedRoomNo.substr(0, SelectedRoomNo.length - 1);
            $("#aRoomNumber").text("Allocated Rooms: " + SelectedRoomNo);
        }
    });

    LoadVisitors(-1);

    LoadBuildings();

    $("select[ID*='ddltypeofvisitor']").change(function (e) {
        var source = $(this)[0].id;

        LoadVisitorsByVisitorTypeID($(this).val());
    });

    $("#drpbuilding").change(function (e) {

        $("input[id*='hdnBuildingID']").val($(this).val());

        $.when(getBookedRoomList($("input[id*='hdnBuildingID']").val())).then(GetAllRoomList($(this).val()));
    });

    $("#aRoomNumber").click(function (e) {
        var BuildingID = $("input[id*='hdnBuildingID']").val();
        $.when(getBookedRoomList(BuildingID)).then(GetAllRoomList(BuildingID));
        return false;
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

function GetAllRoomList(selectedValue)
{
    // Get All Rooms
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/GetRoomList",
        data: JSON.stringify({ BuildingID: parseInt(selectedValue) }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var seatNo = result.d;

                init(bookedSeats, seatNo);
                $('#divRoomNumbers').modal('show');
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
                str.push('<li class="selectedSeat">' + //+ className + '"' +
                      '<a title="' + seatNo[j].ID + '">' + seatNo[j].Number + '</a>' +
                      '</li>');
            }
            else {
                str.push('<li class="seat">' + //+ className + '"' +
                      '<a title="' + seatNo[j].ID + '">' + seatNo[j].Number + '</a>' +
                      '</li>');
            }
            
           
        }
  //  }
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
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function LoadVisitors() {

    /*create/distroy grid for the new search*/
    if (typeof grdTicketDiscription != 'undefined') {
        grdTicketDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="Name">abc</td><td id="Rooms">abc</td><td id="arrivedOn">abc</td><td id="NoOfDays">cc</td><td id="identityProof"></td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/GetVisitorInformation",
        data: JSON.stringify({ from: '01/01/2000', to: '01/01/2000' }),
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
                    if (adminLoanList[i].VisitorTypeID == 1) {
                        visitorType = "Visitor";
                    }
                    else if (adminLoanList[i].VisitorTypeID == 2) {
                        visitorType = "Sewadar";
                    }
                    else {
                        visitorType = "Employee";
                    }
                    var $newRow = $("#rowTemplate").clone();

                    $newRow.find("#Name").html("<table><tr><td><ul class='thumbnails gallery><li id='image-1' class='thumbnail'><a target='_blank' style='background:url(" + adminLoanList[i].VisitorsPhoto + ")'  href='" + adminLoanList[i].VisitorsPhoto + "'><img class='grayscale' width='75Px' height='75PX' src='" + adminLoanList[i].VisitorsPhoto + "' ></a></li></ul> </td></tr><tr><td><b>Name :</b> " + adminLoanList[i].Name + "(" + visitorType + ")</td></tr><tr><td><b>Contact No:</b>" + adminLoanList[i].ContactNumber + "</td></tr><tr><td><b>Address:</b> " + adminLoanList[i].VisitorAddress + "</td></tr></table>");
                    $newRow.find("#Rooms").html("<table><tr><td><b>Building Name :</b> " + adminLoanList[i].BuildingName + "</td></tr><tr><td><b>RoomNumber(s):</b> " + adminLoanList[i].RoomNumbers + "</td></tr><tr><td><a href='#' class='warning' onclick='CheckOutRoom(" + adminLoanList[i].ID + ")'>Check-Out</a></td></tr></table>"); //(adminLoanList[i].Rooms);
                    $newRow.find("#arrivedOn").html(adminLoanList[i].CreatedOn);
                    $newRow.find("#NoOfDays").html(adminLoanList[i].NoOfDaysToStay);

                    $newRow.find("#identityProof").html("<table><tr><td><a href='#' onclick='openLinkDailog(\"" + adminLoanList[i].IdentificationPath + "\",\"" + adminLoanList[i].Name + "\",\"" + adminLoanList[i].Identification + "\")'>" + adminLoanList[i].Identification + "</a></td></tr><tr><td><a href='#' onclick='LoadVisitorByVisitorID(" + adminLoanList[i].ID + ")'>Update</a></td></tr></table>");
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

                $("input[id*='hdnVisitorID']").val(msg.ID);
                $("input[id*='txtName']").val(msg.Name);
                $("input[id*='txtnoofperson']").val(msg.TotalNoOfPerson); 
                $("input[id*='txtvehicle']").val(msg.VehicleNo);
                $("select[id*='ddlpurpose']").val(msg.PurposeOfVisit);
                $("select[id*='drpProofType']").val(msg.Identification);
                $("select[id*='drpNumberOfDays']").val(msg.NoOfDaysToStay);
                $("input[id*='txtContactNumber']").val(msg.ContactNumber);
                $("textarea[id*='txtAddress']").val(msg.VisitorAddress);
                $("input[id*='hdnBuildingID']").val(msg.BuildingID);
                $("#drpbuilding").val(msg.BuildingID);
                $("select[id*='ddlntypeofvisitor']").val(msg.VisitorTypeID);
                $("input[id*='txtlastDate']").val(msg.TimePeriodTo);
                $("input[id*='txtfirstDate']").val(msg.TimePeriodFrom);
                $("select[id*='ddlroomservice']").val(msg.RoomRent);
                $("select[id*='ddlelectricitybill']").val(msg.ElectricityBill);
                $("input[id*='txtstate']").val(msg.State);
                $("input[id*='txtcountry']").val(msg.Country);
                $("input[id*='txtcity']").val(msg.City);
                $("input[id*='txtReference']").val(msg.VisitorReference);
                
                var bookedRoom = "";
                    bookedRoom = GetVisitorRoomList(msg.ID);
                

                $("#aIdentityProof").show();
                $("#aIdentityProof").attr("href", msg.IdentificationPath);
                $("#aPhoto").show();
                $("#aPhoto").attr("href", msg.VisitorsPhoto);
                $("#aAuthorityLetter").show();
                $("#aAuthorityLetter").attr("href", msg.VisitorsAuthorityLetter);
                $("#aRoomNumber").show();
                $("input[id*='btnSave'] ").val("Update");
                EnableDisabledValidator();
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
            $("input[id*='hdnbookedSeats'] ").val(hdnbookedSeats);
        }
    });
    return roomnumbers;
}

function CheckOutRoom(visitorID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/CheckOutVisitor",
        data: JSON.stringify({ VisitorID: visitorID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                LoadVisitors();
                alert("visitor has been check-out successfully");
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function LoadVisitorsByVisitorTypeID(selectedValue) {
    /*create/distroy grid for the new search*/
    if (typeof grdTicketDiscription != 'undefined') {
        grdTicketDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="Name">abc</td><td id="Rooms">abc</td><td id="arrivedOn">abc</td><td id="NoOfDays">cc</td><td id="identityProof"><a id="identity" href="#" onclick="" target="_blank">Identity</a></td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/GetVisitorInformationByVisitorTypeID",
        data: JSON.stringify({ visitorTypeID: selectedValue }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var adminLoanList = result.d;
                if (adminLoanList.length > 0) {

                    $("#tbody").append(rowTemplate);
                }

                var visitorType="";
                for (var i = 0; i < adminLoanList.length; i++) {
                    //var className = "ReportGridItem";
                    //if (i % 2 == 0) {
                    //    className = "ReportGridAlternatingItem";
                    //}
                    if(adminLoanList[i].VisitorTypeID==1)
                    {
                        visitorType="Visitor";
                    }
                    else if(adminLoanList[i].VisitorTypeID==2)
                    {
                        visitorType="Sewadar";
                    }
                    else
                    {
                        visitorType="Employee";
                    }
                    var $newRow = $("#rowTemplate").clone();
                    $newRow.find("#Name").html("<table><tr><td><ul class='thumbnails gallery><li id='image-1' class='thumbnail'><a target='_blank' style='background:url(" + adminLoanList[i].VisitorsPhoto + ")'  href='" + adminLoanList[i].VisitorsPhoto + "'><img class='grayscale' width='75Px' height='75PX' src='" + adminLoanList[i].VisitorsPhoto + "' ></a></li></ul> </td></tr><tr><td><b>Name :</b> " + adminLoanList[i].Name + "(" + visitorType + ")</td></tr><tr><td><b>Contact No:</b>" + adminLoanList[i].ContactNumber + "</td></tr><tr><td><b>Address:</b> " + adminLoanList[i].VisitorAddress + "</td></tr></table>");
                    $newRow.find("#Rooms").html("<table><tr><td><b>Building Name :</b> " + adminLoanList[i].BuildingName + "</td></tr><tr><td><b>RoomNumber(s):</b> " + adminLoanList[i].RoomNumbers + "</td></tr></table>"); //(adminLoanList[i].Rooms);
                    $newRow.find("#arrivedOn").html(adminLoanList[i].CreatedOn);
                    $newRow.find("#NoOfDays").html(adminLoanList[i].NoOfDaysToStay);
                    $newRow.find("#identityProof").html("<table><tr><td><a href='#' onclick='openLinkDailog(\"" + adminLoanList[i].IdentificationPath + "\",\"" + adminLoanList[i].Name + "\",\"" + adminLoanList[i].Identification + "\")'>" + adminLoanList[i].Identification + "</a></td></tr><tr><td><a href='#' onclick='LoadVisitorByVisitorID(" + adminLoanList[i].ID + ")'>Update</a></td></tr></table>");
                   
                    //                    $newRow.addClass(className);
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