

$(document).ready(function () {

    
    //LoadVisitors(-1);

    LoadVisitorsByVisitorTypeID(1);

    $("select[ID*='ddltypeofvisitor']").change(function (e) {
        var source = $(this)[0].id;
        LoadVisitorsByVisitorTypeID($(this).val());
    });

});

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
                    $newRow.find("#NoOfDays").html(adminLoanList[i].TimePeriodTo);
                    var url = location.href.substring(0, location.href.lastIndexOf("/") + 1);
                    if (adminLoanList[i].IdentificationPath == "") {
                        $newRow.find("#identityProof").html("<table><tr><td><a href='#' onclick='openLinkDailog(\"" + adminLoanList[i].IdentificationPath + "\",\"" + adminLoanList[i].Name + "\",\"" + adminLoanList[i].Identification + "\")'>" + adminLoanList[i].Identification + "</a></td></tr><tr><td><a href='" + url + "Visitor_AddNew.aspx?VisitorType=" + adminLoanList[i].VisitorTypeID + "&VisitorID=" + adminLoanList[i].ID + "'>Update</a></td></tr></table>");
                    }
                    else {
                        $newRow.find("#identityProof").html("<table><tr><td><a href='" + url + "Visitor_AddNew.aspx?VisitorType=" + adminLoanList[i].VisitorTypeID + "&VisitorID=" + adminLoanList[i].ID + "'>Update</a></td></tr></table>");
                    }
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
    alert(1);
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
                $("select[id*='ddlntypeofvisitor']").val(msg.VisitorTypeID);
                $("input[id*='txtlastDate']").val(msg.TimePeriodTo);
                $("input[id*='txtfirstDate']").val(msg.TimePeriodFrom);
                $("select[id*='ddlroomservice']").val(msg.RoomRentType);
                $("select[id*='ddlelectricitybill']").val(msg.ElectricityBill);
                $("input[id*='txtstate']").val(msg.State);
                $("input[id*='txtcountry']").val(msg.Country);
                $("input[id*='txtcity']").val(msg.City);
                $("input[id*='txtReference']").val(msg.VisitorReference);
                $("select[id*='ddlRoomRent']").val(msg.RoomRent);
                $("input[id*='txtAdmissionNo']").val(msg.AdmissionNumber);

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
                if (msg.VisitorTypeID == 1) {
                    $("[id$='divfileUploadauthority']").hide();
                    $("[id$='divddlelectricitybill']").hide();
                    $("[id$='divddlroomservice']").hide();
                    $("[id$='divddlntypeofvisitor']").hide();

                }
                else {
                    $("[id$='divtxtvehicle']").hide();
                    $("[id$='divtxtnoofperson']").hide();
                    $("[id$='divdrpNumberOfDays']").hide();
                    $("[id$='divddlpurpose']").hide();
                    $("[id$='divVisitorRoomRent']").hide();
                    $("[id$='ddlRoomRent']").hide();
                    $("[id$='ddlpurpose']").hide();
                }

                if (msg.PurposeOfVisit == "Parents Meeting") {
                    $("#divAdminsnNo").show();
                }
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
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

                var visitorType = "";
                for (var i = 0; i < adminLoanList.length; i++) {
                    //var className = "ReportGridItem";
                    //if (i % 2 == 0) {
                    //    className = "ReportGridAlternatingItem";
                    //}
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
                    $newRow.find("#NoOfDays").html(adminLoanList[i].TimePeriodTo);
                    var url = location.href.substring(0, location.href.lastIndexOf("/") + 1);
                    if ($("input[id*='hdnUserType']").val() == 32) {
                        if (adminLoanList[i].IdentificationPath != "") {
                            $newRow.find("#identityProof").html("<table><tr><td><a href='#' onclick='openLinkDailog(\"" + adminLoanList[i].IdentificationPath + "\",\"" + adminLoanList[i].Name + "\",\"" + adminLoanList[i].Identification + "\")'>" + adminLoanList[i].Identification + "</a></td></tr><tr><td><a href='" + url + "Visitor_AddNew.aspx?VisitorType=" + adminLoanList[i].VisitorTypeID + "&VisitorID=" + adminLoanList[i].ID + "'>Update</a></td></tr></table>");
                        }
                        else {
                            $newRow.find("#identityProof").html("<table><tr><td><a href='" + url + "Visitor_AddNew.aspx?VisitorType=" + adminLoanList[i].VisitorTypeID + "&VisitorID=" + adminLoanList[i].ID + "'>Update</a></td></tr></table>");
                        }
                    }
                    else {
                        if (adminLoanList[i].IdentificationPath != "") {
                            $newRow.find("#identityProof").html("<table><tr><td><a href='#' onclick='openLinkDailog(\"" + adminLoanList[i].IdentificationPath + "\",\"" + adminLoanList[i].Name + "\",\"" + adminLoanList[i].Identification + "\")'>" + adminLoanList[i].Identification + "</a></td></tr></table>");
                        }
                    }
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