/// <reference path="../jquery-vsdoc.js" />

var $form;


$(document).ready(function () {

    $form = $("#aspnetForm");

    $("button[id*='btnshare']").click(function () {
        var count = 0;
        $("input[id*='chkdrwing']:checked").each(function () {
            count = 1;
        });

        if (count == 0) {
            alert("Please select drawing to send as email attachment.");
            return false;
        }

        $("input[id*='txtTo']").val("");
        $("input[id*='txtSubject']").val("");
        $("textarea[id*='txtBody']").val("");


        $("#divEmailDialog").dialog({ modal: true, width: 550, height: 450, title: "Send Email", closeOnEscape: false });
        $("#divEmailDialog").dialog('open');

        return false;

    });

    $("input[id*='btnCancle']").click(function () {

        $("#divEmailDialog").dialog('close');

    });

    $("input[id*='btnSend']").click(function () {

        //if ($form.valid()) {
        if (ValidateEmail()) {
            if ($("input[id*='txtTo']").val() == "" || $("input[id*='txtSubject']").val() == "" || $("input[id*='txtBody']").val() == "") {
                alert("Please enter in required field");
                return false;
            }
        }
        else { return false;}

        $("#divEmailDialog").dialog('close');
        $("#progressBar").dialog({ modal: true, width: 350, height: 200, title: "Please wait....", closeOnEscape: false });
        $("#progressBar").dialog('open');


            $('#indicator').show();
            var DrawingPath = [];
            //$("input[id*='hdnfiles']")
            var files = "";

            $("input[id*='chkdrwing']:checked").each(function () {
                DrawingPath.push($(this).attr("drwingPath"));
                if (files == "") {
                    files += $(this).attr("drwingPath") + ",";
                }
            });

            $("input[id*='hdnfiles']").val(files);

            var params = new Object();

            params.drawingPaths = DrawingPath;
            //params.from = $("input[id*='txtFrom']").val();
            params.to = $("input[id*='txtTo']").val();
            params.body = $("textarea[id*='txtBody']").val();
            params.subject = $("input[id*='txtSubject']").val();


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //url:"http://localhost:64918/AkalAcademy/Services/DrawingController.asmx/SendDrawingsAsEmails",
                url: "/Services/DrawingController.asmx/SendDrawingsAsEmails",
                data: JSON.stringify(params),
                dataType: "json",
                success: function (result, textStatus) {
                    if (textStatus == "success") {
                        $("#progressBar").dialog('close');
                        
                        alert("Email has been sent successfully.");
                    }
                },
                error: function (result, textStatus) {
                    alert(result.responseText);
                }
            });
        //}

    });

    BindZone();

    BindDrawingType();

    $("select[id*='ddlZone']").change(function () {
        BindAcademybyZoneID($(this).val());
    });

    $("select[id*='ddlDwgType']").change(function () {
        BindSubDrawingByDrawingID($(this).val());
      });

    $("#btnSave").click(function (e) {
        if (Page_ClientValidate("drawing")) {
            ClientSideClick(this);
            SaveDrawing();
        }
    });

    $("#btnEdit").click(function () {
        if (Page_ClientValidate("drawing")) {
            //ClientSideClick(this);
            UpdateDrawingInformation();
        }
    });

    LoadDrawingInfo();

    $("#btnEdit").hide();
   // $form.validate(ValidateOptions());
});

function ValidateEmail() {
    var $email = $("input[id*='txtTo']");
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    if (!filter.test($email.val())) {
        alert('Please provide a valid email address');
        $email.focus;
        return false;
    }
    else
        return true;

}

function ValidateOptions() {
    return {
        // Specify the validation rules
        rules: {
            txtBody: "required",
            txtTo: {
                required: true,
                email: true
            },
            txtSubject: "required"
        },

        // Specify the validation error messages
        messages: {
            txtBody: "Please enter your last name",
            txtTo: "Please enter a valid email address",
            txtSubject: "Please entere your email subject"
        },

    }
}

function BindZone() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetDrawingZone",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("select[id*='ddlZone']").append($("<option></option>").val(value.ZoneId).html(value.ZoneName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindAcademybyZoneID(selctZoneID) {

    $("select[id*='ddlAcademy'] option").each(function (index, option) {
        $(option).remove();
    });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetAcademybyZoneID",
        data: JSON.stringify({ ZoneID: parseInt(selctZoneID) }),
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $("select[id*='ddlAcademy']").append($("<option></option>").val("0").html("--Select Academy--"));
                $.each(Result, function (key, value) {
                    $("select[id*='ddlAcademy']").append($("<option></option>").val(value.AcaID).html(value.AcaName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}
    
function BindDrawingType() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetDrawingType",
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $.each(Result, function (key, value) {
                    $("select[id*='ddlDwgType']").append($("<option></option>").val(value.DwTypeId).html(value.DwTypeName));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function BindSubDrawingByDrawingID(selctDrawingID) {
    $("select[id*='ddlSubDrawingType'] option").each(function (index, option) {
        $(option).remove();
    });

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetSubDrawingByDrawingID",
        data: JSON.stringify({ DrawingID: parseInt(selctDrawingID) }),
        async: false,
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
                $("select[id*='ddlSubDrawingType']").append($("<option></option>").val("0").html("--Select Sub Drawing Type--"));
                $.each(Result, function (key, value) {
                    $("select[id*='ddlSubDrawingType']").append($("<option></option>").val(value.ID).html(value.Description));
                });
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function SaveDrawing() {

    var params = new Object();
    var Drawing = new Object();


    Drawing.ZoneId = $("select[id*='ddlZone']").val();
    Drawing.AcaId = $("select[id*='ddlAcademy']").val();
    Drawing.DwTypeId = $("select[id*='ddlDwgType']").val();
    Drawing.DwgNo = $("input[id*='txtDrwNo']").val();
    Drawing.RevisionNo = $("input[id*='txtRevisionNo']").val();
    var fileUploadDwg = $("input[id*='fuDwgFile']")[0].files;
    if ($("input[id*='fuDwgFile']")[0].files != undefined) {
        Drawing.DwgFilePath = "";
        for (var i = 0; i < fileUploadDwg.length; i++) {
            Drawing.DwgFilePath += "AutoCad/" + $("input[id*='fuDwgFile']")[0].files[i].name + ",";
            Drawing.DwgFileName = $("input[id*='fuDwgFile']")[0].files[i].name;
        }
        Drawing.DwgFilePath = Drawing.DwgFilePath.substr(0, Drawing.DwgFilePath.length - 1);
    }
    var fileUploadPDF = $("input[id*='fuDwgFile']")[0].files;
    if ($("input[id*='fuPdf']")[0].files != undefined) {
        Drawing.PdfFilePath = "";
        for (var i = 0; i < fileUploadPDF.length; i++) {
            Drawing.PdfFilePath += "PDF/" + $("input[id*='fuPdf']")[0].files[i].name + ",";
            Drawing.PdfFileName = $("input[id*='fuPdf']")[0].files[i].name;
        }
        Drawing.PdfFilePath = Drawing.PdfFilePath.substr(0, Drawing.PdfFilePath.length - 1);
    }
    Drawing.DrawingName = $("input[id*='txtDrwName']").val();
    Drawing.ShiftedStatus = $("input[id*='hdnInchargeID']").val();
    Drawing.SubDwgTypeID = $("select[id*='ddlSubDrawingType']").val();
    Drawing.CreatedBy = $("input[id*='hdnInchargeID']").val();
    Drawing.ModifyBy = $("input[id*='hdnInchargeID']").val();
    Drawing.IsApproved = true;
    Drawing.Active = 1;
    params.drawing = Drawing;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/SaveDrawingDetail",
        data: JSON.stringify(params),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                AutoCadFileFileUpload();
                PDFFileFileUpload();
                LoadDrawingInfo();
                alert("Drawing Created Successfully");
                ClearTextBox();
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function AutoCadFileFileUpload() {
    var files = $("input[id*='fuDwgFile']")[0].files;

    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }

    $.ajax({
        url: "AutoCadFileFileUploadHandler.ashx",
        type: "POST",
        async: false,
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            //$("#progress").dialog('close');
        },
        error: function (err) {
            alert(err.statusText)
        }
    });
}

function PDFFileFileUpload() {
    var files = $("input[id*='fuPdf']")[0].files;

    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }

    $.ajax({
        url: "PDFFileFileUploadHandler.ashx",
        type: "POST",
        async: false,
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
        },
        error: function (err) {
            alert(err.statusText)
        }
    });
}

function LoadDrawingInfo() {

    /*create/distroy grid for the new search*/
    if (typeof grdDrawingDiscription != 'undefined') {
        grdDrawingDiscription.fnClearTable();
    }

    var rowCount = $('#grid').find("#rowTemplate").length;
    for (var i = 0; i < rowCount; i++) {
        $("#rowTemplate").remove();
    }
    var rowTemplate = '<tr id="rowTemplate"><td id="drawingDetails">abc</td><td id="download">abc</td><td id="uploaded">abc</td></tr>';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GeTDrawingInformation",
        data: JSON.stringify({ DrawingID: 1 }),
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var adminLoanList = result.d;
                if (adminLoanList.length > 0) {

                    $("#tbody").append(rowTemplate);
                }
             
                for (var i = 0; i < adminLoanList.length; i++) {
                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }
                  
                    var $newRow = $("#rowTemplate").clone();
                    $newRow.find("#drawingDetails").html("<table><tr><td><b>Zone :</b> " + adminLoanList[i].ZoneName + "</td><td><b>Drawing No:</b> " + adminLoanList[i].DwgNo + "</td></tr><tr><td><b>Academy:</b>" + adminLoanList[i].AcaName + "</td><td><b>Revision No:</b> " + adminLoanList[i].RevisionNo + "</td></tr><tr><td colspan='2'><b>Drawing Name:</b>" + adminLoanList[i].DrawingName + "</td></tr></table>");
                    $newRow.find("#download").html("<table><tr><td><b>.PDF Files :</b> " + adminLoanList[i].PdfFileName + "</td></tr><tr><td><b>.DWG Files:</b>" + adminLoanList[i].DwgFileName + "</td></tr></table>");
                    $newRow.find("#uploaded").html("<table><tr><td></td></tr>" + adminLoanList[i].CreatedOn + "<tr><td><a href='#' onclick='GetDrawingInfoToUpdate(" + adminLoanList[i].DwgId + ")'>Edit Drawing</a></td></tr></table>");
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
                grdDrawingDiscription = $('#grid').DataTable(
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

function GetDrawingInfoToUpdate(drawingID) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetDrawingInfoToUpdate",
        data: JSON.stringify({ DrawingID: drawingID }),
        dataType: "json",
        async: false,
        success: function (responce) {

            var rdata = responce.d;

            if (rdata != undefined) {
                $("input[id*='hdnDrawingID']").val(rdata.DwgId);
                $("select[id*='ddlZone']").val(rdata.ZoneId);
                $("select[id*='ddlDwgType']").val(rdata.DwTypeId);
                $("input[id*='txtDrwNo']").val(rdata.DwgNo);
                $("input[id*='txtRevisionNo']").val(rdata.RevisionNo);
                $("input[id*='txtDrwName']").val(rdata.DrawingName);
                $("input[id*='hdnInchargeID']").val(rdata.CreatedBy);
                $("input[id*='hdnInchargeID']").val(rdata.ModifyBy);
                BindAcademybyZoneID(rdata.ZoneId);
                $("select[id*='ddlAcademy']").val(rdata.AcaId);
                BindSubDrawingByDrawingID(rdata.DwTypeId)
                $("select[id*='ddlSubDrawingType']").val(rdata.SubDwgTypeID);
                $("select[id*='ddlZone']").prop('disabled', true);
                $("select[id*='ddlAcademy']").prop('disabled', true);
                $("input[id*='txtDrwNo']").prop('disabled', true);
                $("input[id*='txtRevisionNo']").prop('disabled', true);
                $("input[id*='txtDrwName']").prop('disabled', true);
                $("input[id*='btnSave'] ").hide();
                $("input[id*='btnEdit'] ").show();

            }
        },
        error: function (response) {
            alert(response.status + '' + response.textStatus);
        }
    });
}

function UpdateDrawingInformation() {

    var updateParams = new Object();

    var Drawing = new Object();
    Drawing.DwgId = $("input[id*='hdnDrawingID']").val();
    Drawing.ZoneId = $("select[id*='ddlZone']").val();
    Drawing.AcaId = $("select[id*='ddlAcademy']").val();
    Drawing.DwTypeId = $("select[id*='ddlDwgType']").val();
    Drawing.DwgNo = $("input[id*='txtDrwNo']").val();
    Drawing.RevisionNo = $("input[id*='txtRevisionNo']").val();
    var fileUploadDwg = $("input[id*='fuDwgFile']")[0].files;
    if ($("input[id*='fuDwgFile']")[0].files != undefined) {
        Drawing.DwgFilePath = "";
        for (var i = 0; i < fileUploadDwg.length; i++) {
            Drawing.DwgFilePath += "AutoCad/" + $("input[id*='fuDwgFile']")[0].files[i].name + ",";
            Drawing.DwgFileName = $("input[id*='fuDwgFile']")[0].files[i].name;
        }
        Drawing.DwgFilePath = Drawing.DwgFilePath.substr(0, Drawing.DwgFilePath.length - 1);
    }
    var fileUploadPDF = $("input[id*='fuDwgFile']")[0].files;
    if ($("input[id*='fuPdf']")[0].files != undefined) {
        Drawing.PdfFilePath = "";
        for (var i = 0; i < fileUploadPDF.length; i++) {
            Drawing.PdfFilePath += "PDF/" + $("input[id*='fuPdf']")[0].files[i].name + ",";
            Drawing.PdfFileName = $("input[id*='fuPdf']")[0].files[i].name;
        }
        Drawing.PdfFilePath = Drawing.PdfFilePath.substr(0, Drawing.PdfFilePath.length - 1);
    }
    Drawing.DrawingName = $("input[id*='txtDrwName']").val();
    Drawing.SubDwgTypeID = $("select[id*='ddlSubDrawingType']").val();
    Drawing.CreatedBy = $("input[id*='hdnInchargeID']").val();
    Drawing.ModifyBy = $("input[id*='hdnInchargeID']").val();
    Drawing.IsApproved = true;
    Drawing.Active = 1;
  
    updateParams.Drawing = Drawing;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/UpdateDrawingInformation",
        data: JSON.stringify(updateParams),
        dataType: "json",
        async: false,
        success: function (result, textStatus) {
            if (textStatus == "success") {
                AutoCadFileFileUpload();
                PDFFileFileUpload();
                LoadDrawingInfo();
                alert("Drawing Upadte successfully");
                ClearTextBox();
               
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText)
        }
    });
}

function ClearTextBox() {
    $("input[id*='txtDrwNo']").val("");
    $("input[id*='txtRevisionNo']").val("");
    $("input[id*='txtDrwName']").val("");
    $("input[id*='fuDwgFile']").val("");
    $("input[id*='fuPdf']").val("");
    $("select[id*='ddlZone']").val("");
    $("select[id*='ddlAcademy']").val("");
    $("select[id*='ddlDwgType']").val("");
    $("select[id*='ddlSubDrawingType']").val("");
}