var grdTicketDiscription;
var TicketCreatedBy = null;

$(document).ready(function () {

    
    LoadComplaints();

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
                LoadComplaints();
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
                        $newRow.find("#edit").html("<a href='#' onclick='LoadData(" + adminLoanList[i].ID + ")'>Update</a> / <a href='#' onclick='DeleteTicket(" + adminLoanList[i].ID + ")'>Delete</a>");
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
                LoadComplaints();
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
                LoadComplaints();
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

