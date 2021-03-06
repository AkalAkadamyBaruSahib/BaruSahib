﻿var grdTicketDiscription;
var grdInProgressTicket;
var grdCompletedTicket;
var TicketCreatedBy = null;

var _Page_tabToSelect = 0;
var d = new Date();
var currentDate = d.getDate() + "/" + (d.getMonth() + 1) + "/" + d.getFullYear();

$(document).ready(function () {
    $.fn.dataTableExt.sErrMode = 'throw';

    LoadAssignComplaints();
    var $tabs = $("#tabs").tabs();
    $("#tabs").tabs({
        select: function(event, ui) {
            if (ui.index == 0) {
                LoadAssignComplaints();
            }
            if (ui.index == 1) {
                LoadInProgressComplaints(true);
            }
            if (ui.index == 2) {
                LoadCompleteComplaints(true);
            }
        }
    });
    

    $("input[id$='btnNewTicket']").click(function () {
        LoadControls();
        $("#divCreateTicket").dialog({ modal: true, width:800, height: 600, title: "Create Ticket", closeOnEscape: false });
        $("#divCreateTicket").dialog('open');
        return false;
    });

    $("input[id$='btnSave']").click(function () {
        if ($("#ddlStatus").val() == "In Progress") {
            if (Page_ClientValidate("Comp")) {
                SaveTicket();
                return false;
            }
        }
        else {
            SaveTicket();
            return false;
        }
    });


    $("input[id$='btnSaveFeedback']").click(function () {
        SaveFeedback();
        return false;
    });
    

    $("#ddlSeverity").change(function () {
        if ($(this).val() == "Regular") {
            $("#txtDays").val("");
            $("#txtDays").show();
        }
        else {
            $("#txtDays").val("2");
            $("#txtDays").hide();
        }
        
    });

   

    $("#ddlStatus").change(function () {
        if ($(this).val() == "In Progress") {
            $("input[id$='txtCompletionDate']").prop("disabled", false);
        }
        else {
            $("input[id$='txtCompletionDate']").prop("disabled", true);
        }
    });

    if ($("input[id*='hdnUserType']").val() == "10")
    {
        $("#btnNewTicket").show();
    }

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
        ComplaintTickets.Severity = $('#ddlSeverity option:selected').val();
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
                LoadCompleteComplaints(true);
                LoadInProgressComplaints(true);
                LoadAssignComplaints();
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
                grdTicketDiscription = null;
                grdTicketDiscription = $('#grdTicketDiscription').DataTable(
                    {
                        "bDestroy": true,
                        "bPaginate": true,
                        "iDisplayLength": 12,
                        "sPaginationType": "full_numbers",
                        "aaSorting": [[2, 'desc']],
                        
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
        data: JSON.stringify({ ID: ticketID, feedback: sfeedback}),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                $("#divFeedback").dialog('close');
                alert("Feedback has been given to ticket and updated to admin.")
                LoadCompleteComplaints(true);
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

    //if ($("input[id*='hdnUserType']").val() == "10") {

    //    $("#ddlStatus").prop('disabled', false);
    //    $('#ddlStatus').find('option').remove().end();
    //    $("select[id*='ddlStatus']").append($("<option></option>").val("ReOpen").html("ReOpen"));
    //}

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
                $('#ddlComplaintType').val(adminLoanList.ComplaintType);
                $("textarea[id*='txtBody']").val(adminLoanList.Description);
                $("#ddlStatus").val(adminLoanList.StatusID);
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
                LoadAssignComplaints();
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

    if (userType == 2 || userType == 33) {
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
    else if (userType != 2 && userType != 1 && userType == 33) {
        $("#trCompletionDate").hide();
        $("#trComments").hide();
        $("#trStatus").hide();
    }
    else if (userType == 1) {
        $("#trApproved").show();
    }
}

function LoadAssignComplaints() {

    var UserType = $("input[id$='hdnUserType']").val();
    var InchargeID = $("input[id$='hdnUserID']").val();

    /*create/distroy grid for the new search*/
    if (typeof grdTicketDiscription != 'undefined') {
        grdTicketDiscription.fnClearTable();
    }

    var rowCount = $('#grdTicketDiscription').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }

    var rowTemplate = '<tr id="rowTemplate"><td id="TicketID"></td><td id="ZoneAca"></td><td id="Discription"></td><td id="CreatedOn"></td><td id="TantativeDate"></td><td id="CompletionDate"></td><td id="Status"></td><td id="feedback"></td><td id="edit"></td></tr>';

    var LoginID = $("input[id$='hdnLoginID']").val();
    var Status = "Assigned";
   $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/AcadamicUserController.asmx/GetComplaintTickets",
        data: JSON.stringify({ UserType: parseInt(UserType), InchargeID: parseInt(InchargeID), complaintStatus: Status, RoleID: 1 }),
        dataType: "json",
        async: false,
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

                    $newRow.find("#TicketID").html("ComplaintID_" + adminLoanList[i].ID);
                    $newRow.find("#ZoneAca").html("<table><tr><td><b>Zone:</b> " + adminLoanList[i].Zone + "</td></tr><tr><td><b>Academy:</b> " + adminLoanList[i].Academy + "</td></tr></table>");
                    $newRow.find("#Discription").html(adminLoanList[i].Description);
                    $newRow.find("#CreatedOn").html(adminLoanList[i].CreatedOn);
                    $newRow.find("#TantativeDate").html(adminLoanList[i].TentativeDate);
                    $newRow.find("#CompletionDate").html(adminLoanList[i].CompletionDate);
                    $newRow.find("#Status").html(adminLoanList[i].Status);
                    $newRow.find("#feedback").html(adminLoanList[i].Feedback);

                    if ($("input[id*='hdnUserType']").val() == "10" && adminLoanList[i].Status.indexOf("Completed") > -1 && adminLoanList[i].Feedback == "") {
                        $newRow.find("#feedback").html("<a href='#' onclick='LoadFeedBack(" + adminLoanList[i].ID + ")'>Feedback</a>");
                    }
                    //14- TRANSPORT MANAGER,15-BACK OFFICE,17-TRANSPORT INCHARGE,19-TRANSPORT TRAINEE,33-COMPLAINTS
                    if ($("input[id*='hdnUserType']").val() == "33" || $("input[id*='hdnUserType']").val() == "14" || $("input[id*='hdnUserType']").val() == "15" || $("input[id*='hdnUserType']").val() == "13") {
                        $newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'  class='btn btn-primary'>Update</a>");
                    }
                    else {
                        //$newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a> / <a href='#' onclick='DeleteTicket(" + adminLoanList[i].ID + ")'>Delete</a>");
                        $newRow.find("#edit").html("");
                    }

                    //if (adminLoanList[i].CreatedOn < currentDate) {
                    //    $("#rowTemplate").css('background-color', 'Red');
                    //}

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
                grdTicketDiscription = null;
                grdTicketDiscription = $('#grdTicketDiscription').DataTable(
                    {
                        "bDestroy": true,
                        "bPaginate": true,
                        "iDisplayLength": 12,
                        "sPaginationType": "full_numbers",
                        "aaSorting": [[3, 'desc']],
                        "bAutoWidth": false,
                        "bLengthChange": false,
                        "language": {
                            "zeroRecords": "No loans available"
                        }
                    });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function LoadInProgressComplaints(IsRefresh) {

    var UserType = $("input[id$='hdnUserType']").val();
    var InchargeID = $("input[id$='hdnUserID']").val();

    var rowCount = $('#grdInProgressTicket').find("#progresRow").length;

    if (rowCount == 0 || IsRefresh == true) {

        if (typeof grdInProgressTicket != 'undefined') {
            grdInProgressTicket.fnClearTable();
        }


        for (var i = 0; i < rowCount; i++) {
            $("#progresRow").remove();
        }

        var progresRow = '<tr id="progresRow"><td id="TicketID"></td><td id="ZoneAca"></td><td id="Discription"></td><td id="CreatedOn"></td><td id="TantativeDate"></td><td id="CompletionDate"></td><td id="Status"></td><td id="feedback"></td><td id="edit"></td></tr>';

        var LoginID = $("input[id$='hdnLoginID']").val();
        var Status = "In Progress";
       

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Services/AcadamicUserController.asmx/GetComplaintTickets",
            data: JSON.stringify({ UserType: parseInt(UserType), InchargeID: parseInt(InchargeID), complaintStatus: Status, RoleID: 1 }),
            dataType: "json",
            async: false,
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

                        $newProgresRow.find("#TicketID").html("ComplaintID_" + adminLoanList[i].ID);
                        $newProgresRow.find("#ZoneAca").html("<table><tr><td><b>Zone:</b> " + adminLoanList[i].Zone + "</td></tr><tr><td><b>Academy:</b> " + adminLoanList[i].Academy + "</td></tr></table>");
                        $newProgresRow.find("#Discription").html(adminLoanList[i].Description);
                        $newProgresRow.find("#CreatedOn").html(adminLoanList[i].CreatedOn);
                        $newProgresRow.find("#TantativeDate").html(adminLoanList[i].TentativeDate);
                        $newProgresRow.find("#CompletionDate").html(adminLoanList[i].CompletionDate);
                        $newProgresRow.find("#Status").html(adminLoanList[i].Status);
                        $newProgresRow.find("#feedback").html(adminLoanList[i].Feedback);

                        if ($("input[id*='hdnUserType']").val() == "10" && adminLoanList[i].Status.indexOf("Completed") > -1 && adminLoanList[i].Feedback == "") {
                            $newProgresRow.find("#feedback").html("<a href='#' onclick='LoadFeedBack(" + adminLoanList[i].ID + ")'>Feedback</a>");
                        }
                        //14- TRANSPORT MANAGER,15-BACK OFFICE,17-TRANSPORT INCHARGE,19-TRANSPORT TRAINEE,33-COMPLAINTS
                        if ($("input[id*='hdnUserType']").val() == "33" || $("input[id*='hdnUserType']").val() == "14" || $("input[id*='hdnUserType']").val() == "15" || $("input[id*='hdnUserType']").val() == "13") {
                            $newProgresRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")' class='btn btn-primary'>Update</a>");
                        }
                        else {
                            //$newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a> / <a href='#' onclick='DeleteTicket(" + adminLoanList[i].ID + ")'>Delete</a>");
                            $newProgresRow.find("#edit").html("");
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
                            "aaSorting": [[3, 'desc']],
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

function LoadCompleteComplaints(IsRefresh) {

    var UserType = $("input[id$='hdnUserType']").val();
    var InchargeID = $("input[id$='hdnUserID']").val();

    var rowCount = $('#grdCompletedTicket').find("#rowTemplate").length;

    if (rowCount == 0 || IsRefresh == true) {
        /*create/distroy grid for the new search*/
        if (typeof grdCompletedTicket != 'undefined') {
            grdCompletedTicket.fnClearTable();
        }

        for (var i = 0; i < rowCount; i++) {
            $("#rowTemplate").remove();
        }

        var rowTemplate = '<tr id="rowCompTemplate"><td id="TicketID"></td><td id="ZoneAca"></td><td id="Discription"></td><td id="CreatedOn"></td><td id="TantativeDate"></td><td id="CompletionDate"></td><td id="Status"></td><td id="feedback"></td><td id="edit"></td></tr>';

        var LoginID = $("input[id$='hdnLoginID']").val();
        var Status = "Completed";
       
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Services/AcadamicUserController.asmx/GetComplaintTickets",
            data: JSON.stringify({ UserType: parseInt(UserType), InchargeID: parseInt(InchargeID), complaintStatus: Status, RoleID: 1 }),
            dataType: "json",
            async: false,
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

                        var $newRow = $("#rowCompTemplate").clone();

                        $newRow.find("#TicketID").html("ComplaintID_" + adminLoanList[i].ID);
                        $newRow.find("#ZoneAca").html("<table><tr><td><b>Zone:</b> " + adminLoanList[i].Zone + "</td></tr><tr><td><b>Academy:</b> " + adminLoanList[i].Academy + "</td></tr></table>");
                        $newRow.find("#Discription").html(adminLoanList[i].Description);
                        $newRow.find("#CreatedOn").html(adminLoanList[i].CreatedOn);
                        $newRow.find("#TantativeDate").html(adminLoanList[i].TentativeDate);
                        $newRow.find("#CompletionDate").html(adminLoanList[i].CompletionDate);
                        $newRow.find("#Status").html(adminLoanList[i].Status);
                        $newRow.find("#feedback").html(adminLoanList[i].Feedback);

                        if ($("input[id*='hdnUserType']").val() == "10" && adminLoanList[i].Status.indexOf("Completed") > -1 && adminLoanList[i].Feedback == "") {
                            $newRow.find("#feedback").html("<a href='#' onclick='LoadFeedBack(" + adminLoanList[i].ID + ")' class='btn btn-primary'>Feedback</a>");
                        }

                        if ($("input[id*='hdnUserType']").val() == "10") {
                            $newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")' class='btn btn-primary'>Update</a>");
                        }
                        else {
                            //$newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a> / <a href='#' onclick='DeleteTicket(" + adminLoanList[i].ID + ")'>Delete</a>");
                            $newRow.find("#edit").html("");
                        }


                        $newRow.addClass(className);
                        $newRow.show();
                        if (i == 0) {
                            $("#rowCompTemplate").replaceWith($newRow);
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
                            "aaSorting": [[3, 'desc']],
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

