$(document).ready(function () {
    $("input[id*='btnEdit'] ").hide();
    LoadVisitorRoomDetail();
    $("input[id*='btnEdit']").click(function (e) {
        UpdateVisitorRoomInfo();
    });
});

function LoadVisitorRoomDetail() {

    /*create/distroy grid for the new search*/
    if (typeof grdTicketDiscription != 'undefined') {
        grdTicketDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="buildingName">abc</td><td id="buildingFloor">abc</td><td id="roomNumber">abc</td><td id="numberofBed">cc</td><td id="action"></td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/GetVisitorRoomDetail",
        data: JSON.stringify({ RoomID: 1 }),
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

                    $newRow.find("#buildingName").html(adminLoanList[i].BuildingName.Name);
                    $newRow.find("#buildingFloor").html(adminLoanList[i].BuildingFloor);
                    $newRow.find("#roomNumber").html(adminLoanList[i].Number);
                    $newRow.find("#numberofBed").html(adminLoanList[i].NumOfBed);
                    $newRow.find("#action").html("<table><tr><td><a href='#' onclick='GetVisitorRommDetailToUpdate(" + adminLoanList[i].ID + ")'>Edit</a></td></tr></table>");
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

function GetVisitorRommDetailToUpdate(roomID) {
    $("input[id*='btnSave'] ").hide();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/GetRoomInfoToUpdate",
        data: JSON.stringify({ RoomID: roomID }),
        dataType: "json",
        success: function (responce) {

            var rdata = responce.d;

            if (rdata != undefined) {
                $("input[id*='hdnRoomID']").val(rdata.ID);
                $("select[id*='drpBuildingName']").val(rdata.BuildingID);
                $("select[id*='drpBuildingFloor']").val(rdata.BuildingFloor);
                $("input[id*='txtRoomNumber']").val(rdata.Number);
                $("input[id*='txtNoOfBed']").val(rdata.NumOfBed);
                if (rdata.IsPermanent == true) {
                    $("input[id*='chkIsPermant']").prop("checked", true);
                }
                else {
                    $("input[id*='chkIsPermant']").prop("checked", false);
                }
                $("input[id*='btnEdit'] ").show();
            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
        }
    });
}

function UpdateVisitorRoomInfo() {

    var updateParams = new Object();
    var roomNumbers = new Object();

    roomNumbers.ID = $("input[id*='hdnRoomID']").val();
    roomNumbers.BuildingID = $("select[id*='drpBuildingName']").val();
    roomNumbers.BuildingFloor = $("select[id*='drpBuildingFloor']").val();
    roomNumbers.Number = $("input[id*='txtRoomNumber']").val();
    roomNumbers.NumOfBed = $("input[id*='txtNoOfBed']").val();
    roomNumbers.IsPermanent = $("input[id*='chkIsPermant']").is(":checked");

    
   
    updateParams.RoomNumbers = roomNumbers;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/VisitorUserController.asmx/UpdateVisitorRoomInfo",
        data: JSON.stringify(updateParams),
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            if (textStatus == "success") {
                LoadVisitorRoomDetail();
                alert("Record has been Upadte successfully");

                $("select[id*='drpBuildingName']").val("");
                $("select[id*='drpBuildingFloor']").val("");
                $("input[id*='txtRoomNumber']").val("");
                $("input[id*='txtNoOfBed']").val("");
                $("input[id*='chkIsPermant']").prop("checked", false);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}