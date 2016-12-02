var grdTicketDiscription;
var grdInProgressTicket;
var grdCompletedTicket;
var TicketCreatedBy = null;

var _Page_tabToSelect = 0;

$(document).ready(function () {


    var $tabs = $("#tabs").tabs();
    $("#tabs").tabs({
        select: function(event, ui) {
            if (ui.index == 0) {
                LoadAssignComplaints($("input[id$='hdnUserType']").val(), $("input[id$='hdnUserID']").val());
            }
            if (ui.index == 1) {
                LoadInProgressComplaints($("input[id$='hdnUserType']").val(), $("input[id$='hdnUserID']").val());
            }
            if (ui.index == 2) {
                LoadCompleteComplaints($("input[id$='hdnUserType']").val(), $("input[id$='hdnUserID']").val());
            }
        }
    });
    LoadAssignComplaints($("input[id$='hdnUserType']").val(), $("input[id$='hdnUserID']").val());

    $("input[id$='btnNewTicket']").click(function () {
        LoadControls();
        $("#divCreateTicket").dialog({ modal: true, width:800, height: 600, title: "Create Ticket", closeOnEscape: false });
        $("#divCreateTicket").dialog('open');
        return false;
    });

    $("input[id$='btnSave']").click(function () {
        SaveTicket();
        return false;
    });


    $("input[id$='btnSaveFeedback']").click(function () {
        SaveFeedback();
        return false;
    });
    

    $("#ddlSeverity").change(function () {
        if ($(this).val() == "2") {
            $("#txtDays").val("");
            $("#txtDays").show();
        }
        else {
            $("#txtDays").val("2");
            $("#txtDays").hide();
        }
        
    });

  //  LoadComplaints1();
});

function SaveTicket() {

    var userType = $("input[id*='hdnUserType']").val();

    var params = new Object();
    params.InchageID = $("input[id*='txtuserID']").val();
    params.UserTypeID = userType;

    var ComplaintTickets = new Object();
    ComplaintTickets.ID = $("input[id*='hdnID']").val();

    if (ComplaintTickets.ID == "0") {
        ComplaintTickets.CreatedBy = $("input[id*='txtuserID']").val();
        ComplaintTickets.Severity = $('#ddlSeverity option:selected').text();
        ComplaintTickets.SeverityDays = $("#txtDays").val();
        params.isAdd = 1;
    }
    else {
        ComplaintTickets.TentativeDate = $("input[id*='txtCompletionDate']").val();
        ComplaintTickets.ModifyBy = $("input[id*='txtuserID']").val();
        ComplaintTickets.Comments = $("textarea[id*='txtComments']").val();
        ComplaintTickets.Status = $('#ddlStatus option:selected').val();
        ComplaintTickets.CreatedBy = TicketCreatedBy;
        params.isAdd = 0;
    }

    if (userType == "1") {
        ComplaintTickets.IsApproved = parseInt($('#ddlApproved option:selected').val());
        ComplaintTickets.IsApprovedRequired = false;
    }
    else if (userType == "2")
    {
        ComplaintTickets.IsApproved = true;
        ComplaintTickets.IsApprovedRequired = false;
    }

    ComplaintTickets.ComplaintType = $('#ddlComplaintType option:selected').val();
    ComplaintTickets.Description = $("textarea[id*='txtBody']").val();
    ComplaintTickets.Image = "";
    params.complaintTickets = ComplaintTickets;
   

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/AcadamicUserController.asmx/SaveComplaintTicket",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                $("#divCreateTicket").dialog('close');
                alert(result.d);
                LoadAssignComplaints($("input[id$='hdnUserType']").val(), $("input[id$='hdnUserID']").val());
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function LoadComplaints() {

    /*create/distroy grid for the new search*/
    if (typeof grdTicketDiscription != 'undefined') {
        grdTicketDiscription.fnClearTable();
    }

    var rowCount = $('#grdTicketDiscription').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }

    var rowTemplate = '<tr id="rowTemplate"><td id="ZoneAca"></td><td id="Discription"></td><td id="CreatedOn"></td><td id="TantativeDate"></td><td id="CompletionDate"></td><td id="Status"></td><td id="feedback"></td><td id="edit"></td></tr>';

    var LoginID = $("input[id$='hdnLoginID']").val();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/AcadamicUserController.asmx/GetComplaintTickets",
        data: JSON.stringify({ loginID: LoginID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var adminLoanList = result.d;
                if (adminLoanList.length > 0) {
                    
                    $("#tbody").append(rowTemplate);
                }

                for (var i = 0; i < adminLoanList.length; i++) {
                    var className = "ReportGridItem";
                    if (i % 2 == 0) {
                        className = "ReportGridAlternatingItem";
                    }

                    var $newRow = $("#rowTemplate").clone();

                    $newRow.find("#ZoneAca").html("<table><tr><td><b>Zone:</b> " + adminLoanList[i].Zone + "</td></tr><tr><td><b>Academy:</b> " + adminLoanList[i].Academy + "</td></tr></table>");
                    $newRow.find("#Discription").html(adminLoanList[i].Description);
                    $newRow.find("#CreatedOn").html(adminLoanList[i].CreatedOn);
                    $newRow.find("#TantativeDate").html(adminLoanList[i].TentativeDate);
                    $newRow.find("#CompletionDate").html(adminLoanList[i].CompletionDate);
                    $newRow.find("#Status").html(adminLoanList[i].Status);
                    $newRow.find("#feedback").html(adminLoanList[i].Feedback);
                    
                    if ($("input[id*='hdnUserType']").val() != "2" && adminLoanList[i].Status.indexOf("Completed") > -1 && adminLoanList[i].Feedback == "") {
                        $newRow.find("#feedback").html("<a href='#' onclick='LoadFeedBack(" + adminLoanList[i].ID + ")'>Feedback</a>");
                    }

                    if ($("input[id*='hdnUserType']").val() == "2") {
                        $newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a>");
                    }
                    else {
                        //$newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a> / <a href='#' onclick='DeleteTicket(" + adminLoanList[i].ID + ")'>Delete</a>");
                       $newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a>");
                    }


                    $newRow.addClass(className);
                    $newRow.show();
                    if (i == 0) {
                        $("#rowTemplate").replaceWith($newRow);
                    }
                    else {
                        $newRow.appendTo("#grdTicketDiscription > tbody");
                    }
                    $("#grdTicketDiscription").removeAttr("style");

                }
                
                grdTicketDiscription= $('#grdTicketDiscription').DataTable(
                    {
                        "bPaginate": true,
                        "iDisplayLength": 12,
                        "sPaginationType": "full_numbers",
                        "aaSorting": [[2, 'desc']],
                        "bAutoWidth": false,
                        "bLengthChange": false,
                        "language": {
                            "zeroRecords": "No loans available"
                        },
                        "bDestroy": true

                    });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}


function LoadFeedBack(ticketID)
{
    $("input[id*='hdnID']").val(ticketID);
    $("#divFeedback").dialog({ modal: true, width: 500, height: 200, title: "Give feedback to ticket", closeOnEscape: false });
    $("#divFeedback").dialog('open');
    return false;
}


function SaveFeedback(ticketID, sfeedback) {

    var sfeedback = $('#ddlfeedback option:selected').text();
    var ticketID = $("input[id*='hdnID']").val();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/AcadamicUserController.asmx/SaveTicketFeedback",
        data: JSON.stringify({ ID: ticketID, feedback: sfeedback }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                $("#divFeedback").dialog('close');
                alert("Feedback has been given to ticket and updated to admin.")
                LoadAssignComplaints($("input[id$='hdnUserType']").val(), $("input[id$='hdnUserID']").val());
                return false;
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
    return false;
}


function LoadData(ticketID) {

    $("input[id*='hdnID']").val(ticketID);
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/AcadamicUserController.asmx/GetComplaintTicketsByID",
        data: JSON.stringify({ ID: ticketID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var adminLoanList = result.d;
                $("textarea[id*='txtComments']").focus();
                $('#ddlComplaintType').val(adminLoanList.ComplaintType);
                $("textarea[id*='txtBody']").val(adminLoanList.Description);
                $("#ddlStatus").val(adminLoanList.Status);
                $("textarea[id*='txtComments']").val(adminLoanList.Comments);
                $("input[id*='txtCompletionDate']").val(adminLoanList.TentativeDate);
                $("#ddlSeverity").val(adminLoanList.Severity);
                if (adminLoanList.Severity == "Regular") {
                    $("#txtDays").val(adminLoanList.SeverityDays);
                    $("#txtDays").show();
                }
                TicketCreatedBy = adminLoanList.CreatedBy;

                $("#divCreateTicket").dialog({ modal: true, width: 800, height: 600, title: "Create Ticket", closeOnEscape: false });
                $("#divCreateTicket").dialog('open');
                LoadControls();
                return false;
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
    return false;
}


function DeleteTicket(ticketID) {

    $("input[id*='hdnID']").val(ticketID);
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/AcadamicUserController.asmx/DeleteTicket",
        data: JSON.stringify({ ID: ticketID }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                alert("Complaint Ticket has been deleted.");
                LoadAssignComplaints($("input[id$='hdnUserType']").val(), $("input[id$='hdnUserID']").val());
                return false;
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });

    return false;
}


function LoadControls() {
    var userType = $("input[id*='hdnUserType']").val();

    if (userType == 2) {
        if ($("#ddlComplaintType").val() != "") {
            $("#ddlComplaintType").prop("disabled", true);
        }

        if ($("#ddlComplaintType").val() != "") {
            $("#ddlComplaintType").prop("disabled", true);
        }

        $("#txtBody").prop("disabled", true);
        $("#ddlSeverity").prop("disabled", true);
        $("#txtDays").prop("disabled", true);
        $("#spnApproved").hide();
        $("#ddlApproved").hide();
    }
    else if (userType != 2 && userType != 1) {
        $("#trCompletionDate").hide();
        $("#trComments").hide();
        $("#trStatus").hide();
    }
    else if (userType == 1) {
        $("#trApproved").show();
    }
}


function LoadAssignComplaints(UserType,inchargeID)
{
   
         /*create/distroy grid for the new search*/
        if (typeof grdTicketDiscription != 'undefined') {
            grdTicketDiscription.fnClearTable();
        }

          var rowCount = $('#grdTicketDiscription').find("#rowTemplate").length;
        for (var i = 0; i < rowCount; i++) {
            $("#rowTemplate").remove();
        }

        var rowTemplate = '<tr id="rowTemplate"><td id="ZoneAca"></td><td id="Discription"></td><td id="CreatedOn"></td><td id="TantativeDate"></td><td id="CompletionDate"></td><td id="Status"></td><td id="feedback"></td><td id="edit"></td></tr>';

        var LoginID = $("input[id$='hdnLoginID']").val();
        var Status = "Assigned";

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Services/AcadamicUserController.asmx/GetComplaintTickets",
            data: JSON.stringify({ UserType: parseInt(UserType), InchargeID: parseInt(inchargeID), complaintStatus: Status }),
            dataType: "json",
            success: function (result, textStatus) {
                if (textStatus == "success") {
                    var adminLoanList = result.d;
                    if (adminLoanList.length > 0) {

                        $("#tbody").append(rowTemplate);
                    }

                    for (var i = 0; i < adminLoanList.length; i++) {
                        var className = "ReportGridItem";
                        if (i % 2 == 0) {
                            className = "ReportGridAlternatingItem";
                        }

                        var $newRow = $("#rowTemplate").clone();

                        $newRow.find("#ZoneAca").html("<table><tr><td><b>Zone:</b> " + adminLoanList[i].Zone + "</td></tr><tr><td><b>Academy:</b> " + adminLoanList[i].Academy + "</td></tr></table>");
                        $newRow.find("#Discription").html(adminLoanList[i].Description);
                        $newRow.find("#CreatedOn").html(adminLoanList[i].CreatedOn);
                        $newRow.find("#TantativeDate").html(adminLoanList[i].TentativeDate);
                        $newRow.find("#CompletionDate").html(adminLoanList[i].CompletionDate);
                        $newRow.find("#Status").html(adminLoanList[i].Status);
                        $newRow.find("#feedback").html(adminLoanList[i].Feedback);

                        if ($("input[id*='hdnUserType']").val() != "2" && adminLoanList[i].Status.indexOf("Completed") > -1 && adminLoanList[i].Feedback == "") {
                            $newRow.find("#feedback").html("<a href='#' onclick='LoadFeedBack(" + adminLoanList[i].ID + ")'>Feedback</a>");
                        }

                        if ($("input[id*='hdnUserType']").val() == "2") {
                            $newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a>");
                        }
                        else {
                            //$newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a> / <a href='#' onclick='DeleteTicket(" + adminLoanList[i].ID + ")'>Delete</a>");
                            $newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a>");
                        }


                        $newRow.addClass(className);
                        $newRow.show();
                        if (i == 0) {
                            $("#rowTemplate").replaceWith($newRow);
                        }
                        else {
                            $newRow.appendTo("#grdTicketDiscription > tbody");
                        }
                        $("#grdTicketDiscription").removeAttr("style");

                    }

                    grdTicketDiscription = $('#grdTicketDiscription').DataTable(
                        {
                            "bPaginate": true,
                            "iDisplayLength": 12,
                            "sPaginationType": "full_numbers",
                            "aaSorting": [[2, 'desc']],
                            "bAutoWidth": false,
                            "bLengthChange": false,
                            "language": {
                                "zeroRecords": "No loans available"
                            },
                            "bDestroy": true

                        });
                }
            },
            error: function (result, textStatus) {
                alert(result.responseText);
            }
        });
    }


function LoadInProgressComplaints(UserType, inchargeID) {

    var rowCount = $('#grdInProgressTicket').find("#progresRow").length;

    if (rowCount == 0) {

        if (typeof grdInProgressTicket != 'undefined') {
            grdInProgressTicket.fnClearTable();
        }


        for (var i = 0; i < rowCount; i++) {
            $("#progresRow").remove();
        }

        var progresRow = '<tr id="progresRow"><td id="ZoneAca"></td><td id="Discription"></td><td id="CreatedOn"></td><td id="TantativeDate"></td><td id="CompletionDate"></td><td id="Status"></td><td id="feedback"></td><td id="edit"></td></tr>';

        var LoginID = $("input[id$='hdnLoginID']").val();
        var Status = "In Progress";

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Services/AcadamicUserController.asmx/GetComplaintTickets",
            data: JSON.stringify({ UserType: parseInt(UserType), InchargeID: parseInt(inchargeID), complaintStatus: Status }),
            dataType: "json",
            success: function (result, textStatus) {
                if (textStatus == "success") {
                    var adminLoanList = result.d;
                    if (adminLoanList.length > 0) {

                        $("#tbodyProgress").append(progresRow);
                    }

                    for (var i = 0; i < adminLoanList.length; i++) {
                        var className = "ReportGridItem";
                        if (i % 2 == 0) {
                            className = "ReportGridAlternatingItem";
                        }

                        var $newProgresRow = $("#progresRow").clone();

                        $newProgresRow.find("#ZoneAca").html("<table><tr><td><b>Zone:</b> " + adminLoanList[i].Zone + "</td></tr><tr><td><b>Academy:</b> " + adminLoanList[i].Academy + "</td></tr></table>");
                        $newProgresRow.find("#Discription").html(adminLoanList[i].Description);
                        $newProgresRow.find("#CreatedOn").html(adminLoanList[i].CreatedOn);
                        $newProgresRow.find("#TantativeDate").html(adminLoanList[i].TentativeDate);
                        $newProgresRow.find("#CompletionDate").html(adminLoanList[i].CompletionDate);
                        $newProgresRow.find("#Status").html(adminLoanList[i].Status);
                        $newProgresRow.find("#feedback").html(adminLoanList[i].Feedback);

                        if ($("input[id*='hdnUserType']").val() != "2" && adminLoanList[i].Status.indexOf("Completed") > -1 && adminLoanList[i].Feedback == "") {
                            $newProgresRow.find("#feedback").html("<a href='#' onclick='LoadFeedBack(" + adminLoanList[i].ID + ")'>Feedback</a>");
                        }

                        if ($("input[id*='hdnUserType']").val() == "2") {
                            $newProgresRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a>");
                        }
                        else {
                            //$newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a> / <a href='#' onclick='DeleteTicket(" + adminLoanList[i].ID + ")'>Delete</a>");
                            $newProgresRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a>");
                        }


                        $newProgresRow.addClass(className);
                        $newProgresRow.show();
                        if (i == 0) {
                            $("#progresRow").replaceWith($newProgresRow);
                        }
                        else {
                            $newProgresRow.appendTo("#grdInProgressTicket > tbody");
                        }
                        $("#grdInProgressTicket").removeAttr("style");

                    }

                    grdInProgressTicket = $('#grdInProgressTicket').DataTable(
                        {
                            "bPaginate": true,
                            "iDisplayLength": 12,
                            "sPaginationType": "full_numbers",
                            "aaSorting": [[2, 'desc']],
                            "bAutoWidth": false,
                            "bLengthChange": false,
                            "language": {
                                "zeroRecords": "No loans available"
                            },
                            "bDestroy": true

                        });
                }
            },
            error: function (result, textStatus) {
                alert(result.responseText);
            }
        });
    }
}

function LoadCompleteComplaints(UserType, inchargeID) {

    var rowCount = $('#grdCompletedTicket').find("#rowTemplate").length;

    if (rowCount == 0) {
        /*create/distroy grid for the new search*/
        if (typeof grdCompletedTicket != 'undefined') {
            grdCompletedTicket.fnClearTable();
        }


        for (var i = 0; i < rowCount; i++) {
            $("#rowTemplate").remove();
        }

        var rowTemplate = '<tr id="rowTemplate"><td id="ZoneAca"></td><td id="Discription"></td><td id="CreatedOn"></td><td id="TantativeDate"></td><td id="CompletionDate"></td><td id="Status"></td><td id="feedback"></td><td id="edit"></td></tr>';

        var LoginID = $("input[id$='hdnLoginID']").val();
        var Status = "Completed";

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Services/AcadamicUserController.asmx/GetComplaintTickets",
            data: JSON.stringify({ UserType: parseInt(UserType), InchargeID: parseInt(inchargeID), complaintStatus: Status }),
            dataType: "json",
            success: function (result, textStatus) {
                if (textStatus == "success") {
                    var adminLoanList = result.d;
                    if (adminLoanList.length > 0) {
                        $("#tbody2").append(rowTemplate);
                    }

                    for (var i = 0; i < adminLoanList.length; i++) {
                        var className = "ReportGridItem";
                        if (i % 2 == 0) {
                            className = "ReportGridAlternatingItem";
                        }

                        var $newRow = $("#rowTemplate").clone();

                        $newRow.find("#ZoneAca").html("<table><tr><td><b>Zone:</b> " + adminLoanList[i].Zone + "</td></tr><tr><td><b>Academy:</b> " + adminLoanList[i].Academy + "</td></tr></table>");
                        $newRow.find("#Discription").html(adminLoanList[i].Description);
                        $newRow.find("#CreatedOn").html(adminLoanList[i].CreatedOn);
                        $newRow.find("#TantativeDate").html(adminLoanList[i].TentativeDate);
                        $newRow.find("#CompletionDate").html(adminLoanList[i].CompletionDate);
                        $newRow.find("#Status").html(adminLoanList[i].Status);
                        $newRow.find("#feedback").html(adminLoanList[i].Feedback);

                        if ($("input[id*='hdnUserType']").val() != "2" && adminLoanList[i].Status.indexOf("Completed") > -1 && adminLoanList[i].Feedback == "") {
                            $newRow.find("#feedback").html("<a href='#' onclick='LoadFeedBack(" + adminLoanList[i].ID + ")'>Feedback</a>");
                        }

                        if ($("input[id*='hdnUserType']").val() == "2") {
                            $newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a>");
                        }
                        else {
                            //$newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a> / <a href='#' onclick='DeleteTicket(" + adminLoanList[i].ID + ")'>Delete</a>");
                            $newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a>");
                        }


                        $newRow.addClass(className);
                        $newRow.show();
                        if (i == 0) {
                            $("#rowTemplate").replaceWith($newRow);
                        }
                        else {
                            $newRow.appendTo("#grdCompletedTicket > tbody");
                        }
                        $("#grdCompletedTicket").removeAttr("style");

                    }

                    grdCompletedTicket = $('#grdCompletedTicket').DataTable(
                        {
                            "bPaginate": true,
                            "iDisplayLength": 12,
                            "sPaginationType": "full_numbers",
                            "aaSorting": [[2, 'desc']],
                            "bAutoWidth": false,
                            "bLengthChange": false,
                            "language": {
                                "zeroRecords": "No loans available"
                            },
                            "bDestroy": true

                        });
                }
            },
            error: function (result, textStatus) {
                alert(result.responseText);
            }
        });
    }
}