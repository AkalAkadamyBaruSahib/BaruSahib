$(document).ready(function () {
    BindAcademy();
    var InchargeId = $("input[id*='hdnInchargeID']").val();
    var UserTypeId = $("input[id*='hdnIsAdmin']").val();
    var PsId = $("input[id*='hdnPSID']").val();

    if ($("select[id*='ddlAcademy']").val() == "0" || $("select[id*='ddlAcademy']").val() == "") {
        if (UserTypeId == "4") {
            LoadEstimateViewForPurchase(PsId, UserTypeId, InchargeId);
        }
        else if (UserTypeId == "12") {
            LoadEstimateViewForPurchaseByEmployeeID(PsId, UserTypeId, InchargeId);
        }
        else if (UserTypeId == "2") {
            LoadMaterialDepatchStatus(PsId, UserTypeId, InchargeId)
        }
    }
     

    $("select[id*='ddlAcademy']").change(function () {
        // location.reload();
        if (UserTypeId == "4") {
            LoadEstimateViewForPurchaseByAcaID(PsId, UserTypeId, InchargeId, $(this).val());
        }
        else if (UserTypeId == "12") {
            LoadEstimateViewForPurchaseByEmployeeIDByAcaID(PsId, UserTypeId, InchargeId, $(this).val());
        }
        else if (UserTypeId == "2") {
            LoadMaterialDepatchStatusByAcaID(PsId, InchargeId, $(this).val())
        }
    });

});

function BindAcademy() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/GetAcademy",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                var Result = result.d;
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

function LoadEstimateViewForPurchase(PsId, UserTypeId, InchargeId) {
    var ZoneInfo = "<div class='row-fluid sortable'>";
    ZoneInfo += "<div class='box span12'>";
    ZoneInfo += "<div class='box-header well' data-original-title>";
    ZoneInfo += "<h2><i class='icon-user'></i> Material Dispatch Details</h2>";
    ZoneInfo += "<div class='box-icon'>";
    ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
    ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
    ZoneInfo += "</div>";
    ZoneInfo += "</div>";
    ZoneInfo += "<div class='box-content'>";
    ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
    ZoneInfo += "<thead>";
    ZoneInfo += "<tr>";
    ZoneInfo += "<th style='display:none;'></th>";
    ZoneInfo += "<th style='display:none;'></th>";
    ZoneInfo += "</tr>";
    ZoneInfo += "</thead>";
    ZoneInfo += "<tbody>";
    /*create/distroy grid for the new search*/
  

    var rowTemplate = "<div class='row-fluid sortable'>";
    rowTemplate += "<div class='box span12'>";
    rowTemplate += "<div class='box-header well' data-original-title>";
    rowTemplate += "<h2><i class='icon-user'></i> Material Dispatch Details</h2>";
    rowTemplate += "<div class='box-icon'>";
    rowTemplate += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
    rowTemplate += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
    rowTemplate += "</div>";
    rowTemplate += "</div>";
    rowTemplate += "<div class='box-content'>";
    rowTemplate += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
    rowTemplate += "<thead>";
    rowTemplate += "<tr>";
    rowTemplate += "<th style='display:none;'></th>";
    rowTemplate += "<th style='display:none;'></th>";
    rowTemplate += "</tr>";
    rowTemplate += "</thead>";
    rowTemplate += "<tbody>";


    var bottomrow = "</tbody>";
    bottomrow += "</table>";
    bottomrow += "</div>";
    bottomrow += "</div>";
    bottomrow += "</div>";


    var htmlContainer = "";
 
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/EstimateViewForPurchase",
        data: JSON.stringify({ PSID:PsId,UserTypeID:UserTypeId, InchrgID:InchargeId}),
        dataType: "json",
        success: function (result, textStatus) {
        
            if (textStatus == "success") {
                var Est = result.d;
            

                var EmployeeType = "";
                var ZoneInfo = "";
                htmlContainer = rowTemplate;
                for (var i = 0; i < Est.length; i++) {

                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }


                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td style='display:none;'>1</td>";
                    ZoneInfo += "<td>";
                    ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td width='20%'><b style='color:red;'>Estimate No:</b> " + Est[i].EstId + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Sanction Date:</b> " + getJsonDate(Est[i].SanctionDate) + "</td>";
                    ZoneInfo += "<td class='center' width='25%'><b style='color:red;'>Sub Estimate:</b> " + Est[i].SubEstimate + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Academy:</b> " + Est[i].Academy.AcaName + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Zone:</b> " + Est[i].Zone.ZoneName + "</td>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='Purchase_ViewEstMaterial.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                    }
                    else if ($("input[id*='hdnIsAdmin']").val() == "12") {
                        ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='PurchaseEmployee_ViewEstMaterial.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                    }
                    else {
                        if (PsId == "1") {
                            ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?IsLocal=1&EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
                        }
                        else {
                            ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
                        }
                    }
                    ZoneInfo += "</tr>";
                    ZoneInfo += "</table>";
                    ZoneInfo += "<table border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
                    ZoneInfo += "<tr style='color:Green;'>";
                    ZoneInfo += "<th width='5%'><b>Sr. No.</b></th>";
                    ZoneInfo += "<th width='20%'>Material Name</th>";
                    ZoneInfo += "<th width='2%'>Unit</th>";
                    ZoneInfo += "<th width='2%'>Quantity</th>";
                    ZoneInfo += "<th width='5%'>Source Type</th>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<th width='27%'>Purchase Officer</th>";
                    }
                    ZoneInfo += "<th width='15%'>Purchase Date</th>";
                    ZoneInfo += "<th width='20%'>Remark</th>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<th width='0%'>Action</th>";
                    }
                    ZoneInfo += "</tr>";
                    var count = 0;
                    if (Est[i].EstimateAndMaterialOthersRelations != null) {
                        var materials = Est[i].EstimateAndMaterialOthersRelations;
                        for (var j = 0; j < materials.length; j++) {
                            if (materials[j].EstId >0) {

                                ZoneInfo += "<tr>";
                                ZoneInfo += "<td>" + (count + 1) + "</td>";
                                ZoneInfo += "<td>" + materials[j].Material.MatName + "</td>";
                                ZoneInfo += "<td>" + materials[j].Unit.UnitName + "</td>";
                                ZoneInfo += "<td>" + materials[j].Qty + "</td>";
                                ZoneInfo += "<td>" + materials[j].PurchaseSource.PSName + "</td>";
                                if ($("input[id*='hdnIsAdmin']").val() == "4") {
                                    ZoneInfo += "<td class='left'>";
                                    ZoneInfo += "<table>";
                                    ZoneInfo += "<tr><td> <b>Name:</b> " + materials[j].Incharge.InName + " </td></tr>";
                                    if (materials[j].EmployeeAssignDateTime == "1/1/1900 12:00:00 AM") {
                                        ZoneInfo += "<tr><td><b>Assigned Date:</b> </td></tr>";
                                    }
                                    else {
                                        ZoneInfo += "<tr><td style='color:darkred;'><b>Assigned Date:</b> " + getJsonDate(materials[j].EmployeeAssignDateTime) + "</td></tr>";
                                    }
                                    ZoneInfo += "</table>";
                                    ZoneInfo += "</td>";
                                }


                                if (materials[j].DispatchDate != null) {
                                    ZoneInfo += "<td>" + getJsonDate(materials[j].DispatchDate) + "</td>";
                                }
                                else { ZoneInfo += "<td>Not Purchased</td>"; }


                                ZoneInfo += "<td>" + materials[j].remarkByPurchase + "</td>";

                                if ($("input[id*='hdnIsAdmin']").val() == "4") {
                                    ZoneInfo += "<td width='30%'><a href='javascript: openModelPopUp(" + Est.EstId + "," + materials.Sno + ");'><span class='label label-warning'  style='font-size: 15.998px;'>Reject Item</span></a></td>";
                                }

                                ZoneInfo += "</tr>";
                                count++;
                            }
                        }
                    }
                    ZoneInfo += "</tr>";
                    ZoneInfo += "</table>";
                    ZoneInfo += "</td>";
                }
                htmlContainer = rowTemplate + ZoneInfo + bottomrow;

                // htmlContainer.appendTo("#divEstimateDetails");
                $("#divEstimateDetails").empty();
                $("#divEstimateDetails").append(htmlContainer);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function getJsonDate(strDate) {
    var displayDate = "";
    if (strDate != null) {
        var date = new Date(parseInt(strDate.substr(6)));
        // format display date (e.g. 04/10/2012)
        displayDate = $.datepicker.formatDate("mm/dd/yy", date);
    }
    return displayDate;
}

function LoadEstimateViewForPurchaseByAcaID(PsId, UserTypeId, InchargeId, AcaId) {

    var ZoneInfo = "<div class='row-fluid sortable'>";
    ZoneInfo += "<div class='box span12'>";
    ZoneInfo += "<div class='box-header well' data-original-title>";
    ZoneInfo += "<h2><i class='icon-user'></i> Material Dispatch Details</h2>";
    ZoneInfo += "<div class='box-icon'>";
    ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
    ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
    ZoneInfo += "</div>";
    ZoneInfo += "</div>";
    ZoneInfo += "<div class='box-content'>";
    ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
    ZoneInfo += "<thead>";
    ZoneInfo += "<tr>";
    ZoneInfo += "<th style='display:none;'></th>";
    ZoneInfo += "<th style='display:none;'></th>";
    ZoneInfo += "</tr>";
    ZoneInfo += "</thead>";
    ZoneInfo += "<tbody>";
    /*create/distroy grid for the new search*/


    var rowTemplate = "<div class='row-fluid sortable'>";
    rowTemplate += "<div class='box span12'>";
    rowTemplate += "<div class='box-header well' data-original-title>";
    rowTemplate += "<h2><i class='icon-user'></i> Material Dispatch Details</h2>";
    rowTemplate += "<div class='box-icon'>";
    rowTemplate += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
    rowTemplate += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
    rowTemplate += "</div>";
    rowTemplate += "</div>";
    rowTemplate += "<div class='box-content'>";
    rowTemplate += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
    rowTemplate += "<thead>";
    rowTemplate += "<tr>";
    rowTemplate += "<th style='display:none;'></th>";
    rowTemplate += "<th style='display:none;'></th>";
    rowTemplate += "</tr>";
    rowTemplate += "</thead>";
    rowTemplate += "<tbody>";


    var bottomrow = "</tbody>";
    bottomrow += "</table>";
    bottomrow += "</div>";
    bottomrow += "</div>";
    bottomrow += "</div>";


    var htmlContainer = "";
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Services/PurchaseControler.asmx/EstimateViewForPurchaseByAcaID",
            data: JSON.stringify({ PSID: PsId, UserTypeID: UserTypeId, InchrgID: InchargeId, AcaID: AcaId }),
            dataType: "json",
            success: function (result, textStatus) {

            if (textStatus == "success") {
                var Est = result.d;


                var EmployeeType = "";
                var ZoneInfo = "";
                htmlContainer = rowTemplate;
            
                for (var i = 0; i < Est.length; i++) {

                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }


                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td style='display:none;'>1</td>";
                    ZoneInfo += "<td>";
                    ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td width='20%'><b style='color:red;'>Estimate No:</b> " + Est[i].EstId + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Sanction Date:</b> " + getJsonDate(Est[i].ModifyOn) + "</td>";
                    ZoneInfo += "<td class='center' width='25%'><b style='color:red;'>Sub Estimate:</b> " + Est[i].SubEstimate + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Academy:</b> " + Est[i].Academy.AcaName + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Zone:</b> " + Est[i].Zone.ZoneName + "</td>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='Purchase_ViewEstMaterial.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                    }
                    else if ($("input[id*='hdnIsAdmin']").val() == "12") {
                       ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='PurchaseEmployee_ViewEstMaterial.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                    }
                    else {
                        if (PsId == "1") {
                            ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?IsLocal=1&EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
                        }
                        else {
                            ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
                        }
                    }
                    ZoneInfo += "</tr>";
                    ZoneInfo += "</table>";
                    ZoneInfo += "<table border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
                    ZoneInfo += "<tr style='color:Green;'>";
                    ZoneInfo += "<th width='5%'><b>Sr. No.</b></th>";
                    ZoneInfo += "<th width='20%'>Material Name</th>";
                    ZoneInfo += "<th width='2%'>Unit</th>";
                    ZoneInfo += "<th width='2%'>Quantity</th>";
                    ZoneInfo += "<th width='5%'>Source Type</th>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<th width='27%'>Purchase Officer</th>";
                    }
                    ZoneInfo += "<th width='15%'>Purchase Date</th>";
                    ZoneInfo += "<th width='20%'>Remark</th>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<th width='0%'>Action</th>";
                    }
                    ZoneInfo += "</tr>";
                    var count = 0;
                    if (Est[i].EstimateAndMaterialOthersRelations != null) {
                        var materials = Est[i].EstimateAndMaterialOthersRelations;
                        for (var j = 0; j < materials.length; j++) {
                            if (materials[j].EstId > 0) {

                                ZoneInfo += "<tr>";
                                ZoneInfo += "<td>" + (count + 1) + "</td>";
                                ZoneInfo += "<td>" + materials[j].Material.MatName + "</td>";
                                ZoneInfo += "<td>" + materials[j].Unit.UnitName + "</td>";
                                ZoneInfo += "<td>" + materials[j].Qty + "</td>";
                                ZoneInfo += "<td>" + materials[j].PurchaseSource.PSName + "</td>";
                                if ($("input[id*='hdnIsAdmin']").val() == "4") {
                                    ZoneInfo += "<td class='left'>";
                                    ZoneInfo += "<table>";
                                    ZoneInfo += "<tr><td> <b>Name:</b> " + materials[j].Incharge.InName + " </td></tr>";
                                    if (materials[j].EmployeeAssignDateTime == "1/1/1900 12:00:00 AM") {
                                        ZoneInfo += "<tr><td><b>Assigned Date:</b> </td></tr>";
                                    }
                                    else {
                                        ZoneInfo += "<tr><td style='color:darkred;'><b>Assigned Date:</b> " + getJsonDate(materials[j].EmployeeAssignDateTime) + "</td></tr>";
                                    }
                                    ZoneInfo += "</table>";
                                    ZoneInfo += "</td>";
                                }

                                if (materials[j].DispatchDate != null) {
                                    ZoneInfo += "<td>" + getJsonDate(materials[j].DispatchDate) + "</td>";
                                }
                                else { ZoneInfo += "<td>Not Purchased</td>"; }


                                ZoneInfo += "<td>" + materials[j].remarkByPurchase + "</td>";

                                if ($("input[id*='hdnIsAdmin']").val() == "4") {
                                    ZoneInfo += "<td width='30%'><a href='javascript: openModelPopUp(" + Est.EstId + "," + materials.Sno + ");'><span class='label label-warning'  style='font-size: 15.998px;'>Reject Item</span></a></td>";
                                }

                                ZoneInfo += "</tr>";
                                count++;
                            }
                        }
                    }
                    ZoneInfo += "</tr>";
                    ZoneInfo += "</table>";
                    ZoneInfo += "</td>";
                }
                htmlContainer = rowTemplate + ZoneInfo + bottomrow;

                // htmlContainer.appendTo("#divEstimateDetails");
                $("#divEstimateDetails").empty();
                $("#divEstimateDetails").append(htmlContainer);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function LoadEstimateViewForPurchaseByEmployeeID(PsId, UserTypeId, InchargeId) {
    var ZoneInfo = "<div class='row-fluid sortable'>";
    ZoneInfo += "<div class='box span12'>";
    ZoneInfo += "<div class='box-header well' data-original-title>";
    ZoneInfo += "<h2><i class='icon-user'></i> Material Dispatch Details</h2>";
    ZoneInfo += "<div class='box-icon'>";
    ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
    ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
    ZoneInfo += "</div>";
    ZoneInfo += "</div>";
    ZoneInfo += "<div class='box-content'>";
    ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
    ZoneInfo += "<thead>";
    ZoneInfo += "<tr>";
    ZoneInfo += "<th style='display:none;'></th>";
    ZoneInfo += "<th style='display:none;'></th>";
    ZoneInfo += "</tr>";
    ZoneInfo += "</thead>";
    ZoneInfo += "<tbody>";
    /*create/distroy grid for the new search*/


    var rowTemplate = "<div class='row-fluid sortable'>";
    rowTemplate += "<div class='box span12'>";
    rowTemplate += "<div class='box-header well' data-original-title>";
    rowTemplate += "<h2><i class='icon-user'></i> Material Dispatch Details</h2>";
    rowTemplate += "<div class='box-icon'>";
    rowTemplate += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
    rowTemplate += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
    rowTemplate += "</div>";
    rowTemplate += "</div>";
    rowTemplate += "<div class='box-content'>";
    rowTemplate += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
    rowTemplate += "<thead>";
    rowTemplate += "<tr>";
    rowTemplate += "<th style='display:none;'></th>";
    rowTemplate += "<th style='display:none;'></th>";
    rowTemplate += "</tr>";
    rowTemplate += "</thead>";
    rowTemplate += "<tbody>";


    var bottomrow = "</tbody>";
    bottomrow += "</table>";
    bottomrow += "</div>";
    bottomrow += "</div>";
    bottomrow += "</div>";


    var htmlContainer = "";

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/EstimateViewForPurchaseByEmployeeID",
        data: JSON.stringify({ PSID: PsId, UserTypeID: UserTypeId, InchrgID: InchargeId }),
        dataType: "json",
        success: function (result, textStatus) {

            if (textStatus == "success") {
                var Est = result.d;


                var EmployeeType = "";
                var ZoneInfo = "";
                htmlContainer = rowTemplate;
                for (var i = 0; i < Est.length; i++) {

                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }


                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td style='display:none;'>1</td>";
                    ZoneInfo += "<td>";
                    ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td width='20%'><b style='color:red;'>Estimate No:</b> " + Est[i].EstId + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Sanction Date:</b> " + getJsonDate(Est[i].ModifyOn) + "</td>";
                    ZoneInfo += "<td class='center' width='25%'><b style='color:red;'>Sub Estimate:</b> " + Est[i].SubEstimate + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Academy:</b> " + Est[i].Academy.AcaName + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Zone:</b> " + Est[i].Zone.ZoneName + "</td>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='Purchase_ViewEstMaterial.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                    }
                    else if ($("input[id*='hdnIsAdmin']").val() == "12") {
                        ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='PurchaseEmployee_ViewEstMaterial.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                    }
                    else {
                        if (PsId == "1") {
                            ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?IsLocal=1&EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
                        }
                        else {
                            ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
                        }
                    }
                    ZoneInfo += "</tr>";
                    ZoneInfo += "</table>";
                    ZoneInfo += "<table border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
                    ZoneInfo += "<tr style='color:Green;'>";
                    ZoneInfo += "<th width='5%'><b>Sr. No.</b></th>";
                    ZoneInfo += "<th width='20%'>Material Name</th>";
                    ZoneInfo += "<th width='2%'>Unit</th>";
                    ZoneInfo += "<th width='2%'>Quantity</th>";
                    ZoneInfo += "<th width='5%'>Source Type</th>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<th width='27%'>Purchase Officer</th>";
                    }
                    ZoneInfo += "<th width='15%'>Purchase Date</th>";
                    ZoneInfo += "<th width='20%'>Remark</th>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<th width='0%'>Action</th>";
                    }
                    ZoneInfo += "</tr>";
                    var count = 0;
                    if (Est[i].EstimateAndMaterialOthersRelations != null) {
                        var materials = Est[i].EstimateAndMaterialOthersRelations;
                        for (var j = 0; j < materials.length; j++) {
                            if (materials[j].EstId > 0) {

                                ZoneInfo += "<tr>";
                                ZoneInfo += "<td>" + (count + 1) + "</td>";
                                ZoneInfo += "<td>" + materials[j].Material.MatName + "</td>";
                                ZoneInfo += "<td>" + materials[j].Unit.UnitName + "</td>";
                                ZoneInfo += "<td>" + materials[j].Qty + "</td>";
                                ZoneInfo += "<td>" + materials[j].PurchaseSource.PSName + "</td>";
                                if ($("input[id*='hdnIsAdmin']").val() == "4") {
                                    ZoneInfo += "<td class='left'>";
                                    ZoneInfo += "<table>";
                                    ZoneInfo += "<tr><td> <b>Name:</b> " + materials[j].Incharge.InName + " </td></tr>";
                                    if (materials[j].EmployeeAssignDateTime == "1/1/1900 12:00:00 AM") {
                                        ZoneInfo += "<tr><td><b>Assigned Date:</b> </td></tr>";
                                    }
                                    else {
                                        ZoneInfo += "<tr><td style='color:darkred;'><b>Assigned Date:</b> " + getJsonDate(materials[j].EmployeeAssignDateTime) + "</td></tr>";
                                    }
                                    ZoneInfo += "</table>";
                                    ZoneInfo += "</td>";
                                }


                                ZoneInfo += "<td>" + getJsonDate(materials[j].DispatchDate) + "</td>";


                                ZoneInfo += "<td>" + materials[j].remarkByPurchase + "</td>";

                                if ($("input[id*='hdnIsAdmin']").val() == "4") {
                                    ZoneInfo += "<td width='30%'><a href='javascript: openModelPopUp(" + Est.EstId + "," + materials.Sno + ");'><span class='label label-warning'  style='font-size: 15.998px;'>Reject Item</span></a></td>";
                                }

                                ZoneInfo += "</tr>";
                                count++;
                            }
                        }
                    }
                    ZoneInfo += "</tr>";
                    ZoneInfo += "</table>";
                    ZoneInfo += "</td>";
                }
                htmlContainer = rowTemplate + ZoneInfo + bottomrow;

                // htmlContainer.appendTo("#divEstimateDetails");
                $("#divEstimateDetails").empty();
                $("#divEstimateDetails").append(htmlContainer);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function LoadEstimateViewForPurchaseByEmployeeIDByAcaID(PsId, UserTypeId, InchargeId, AcaId) {
    var ZoneInfo = "<div class='row-fluid sortable'>";
    ZoneInfo += "<div class='box span12'>";
    ZoneInfo += "<div class='box-header well' data-original-title>";
    ZoneInfo += "<h2><i class='icon-user'></i> Material Dispatch Details</h2>";
    ZoneInfo += "<div class='box-icon'>";
    ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
    ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
    ZoneInfo += "</div>";
    ZoneInfo += "</div>";
    ZoneInfo += "<div class='box-content'>";
    ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
    ZoneInfo += "<thead>";
    ZoneInfo += "<tr>";
    ZoneInfo += "<th style='display:none;'></th>";
    ZoneInfo += "<th style='display:none;'></th>";
    ZoneInfo += "</tr>";
    ZoneInfo += "</thead>";
    ZoneInfo += "<tbody>";
    /*create/distroy grid for the new search*/


    var rowTemplate = "<div class='row-fluid sortable'>";
    rowTemplate += "<div class='box span12'>";
    rowTemplate += "<div class='box-header well' data-original-title>";
    rowTemplate += "<h2><i class='icon-user'></i> Material Dispatch Details</h2>";
    rowTemplate += "<div class='box-icon'>";
    rowTemplate += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
    rowTemplate += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
    rowTemplate += "</div>";
    rowTemplate += "</div>";
    rowTemplate += "<div class='box-content'>";
    rowTemplate += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
    rowTemplate += "<thead>";
    rowTemplate += "<tr>";
    rowTemplate += "<th style='display:none;'></th>";
    rowTemplate += "<th style='display:none;'></th>";
    rowTemplate += "</tr>";
    rowTemplate += "</thead>";
    rowTemplate += "<tbody>";


    var bottomrow = "</tbody>";
    bottomrow += "</table>";
    bottomrow += "</div>";
    bottomrow += "</div>";
    bottomrow += "</div>";


    var htmlContainer = "";
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/EstimateViewForPurchaseByAcaID",
        data: JSON.stringify({ PSID: PsId, UserTypeID: UserTypeId, InchrgID: InchargeId, AcaID: AcaId }),
        dataType: "json",
        success: function (result, textStatus) {

            if (textStatus == "success") {
                var Est = result.d;


                var EmployeeType = "";
                var ZoneInfo = "";
                htmlContainer = rowTemplate;

                for (var i = 0; i < Est.length; i++) {

                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }


                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td style='display:none;'>1</td>";
                    ZoneInfo += "<td>";
                    ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td width='20%'><b style='color:red;'>Estimate No:</b> " + Est[i].EstId + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Sanction Date:</b> " + Est[i].ModifyOn + "</td>";
                    ZoneInfo += "<td class='center' width='25%'><b style='color:red;'>Sub Estimate:</b> " + Est[i].SubEstimate + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Academy:</b> " + Est[i].Academy.AcaName + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Zone:</b> " + Est[i].Zone.ZoneName + "</td>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='Purchase_ViewEstMaterial.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                    }
                    else if ($("input[id*='hdnIsAdmin']").val() == "12") {
                        ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='PurchaseEmployee_ViewEstMaterial.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                    }
                    else {
                        if (PsId == "1") {
                            ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?IsLocal=1&EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
                        }
                        else {
                            ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
                        }
                    }
                    ZoneInfo += "</tr>";
                    ZoneInfo += "</table>";
                    ZoneInfo += "<table border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
                    ZoneInfo += "<tr style='color:Green;'>";
                    ZoneInfo += "<th width='5%'><b>Sr. No.</b></th>";
                    ZoneInfo += "<th width='20%'>Material Name</th>";
                    ZoneInfo += "<th width='2%'>Unit</th>";
                    ZoneInfo += "<th width='2%'>Quantity</th>";
                    ZoneInfo += "<th width='5%'>Source Type</th>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<th width='27%'>Purchase Officer</th>";
                    }
                    ZoneInfo += "<th width='15%'>Purchase Date</th>";
                    ZoneInfo += "<th width='20%'>Remark</th>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<th width='0%'>Action</th>";
                    }
                    ZoneInfo += "</tr>";
                    var count = 0;
                    if (Est[i].EstimateAndMaterialOthersRelations != null) {
                        var materials = Est[i].EstimateAndMaterialOthersRelations;
                        for (var j = 0; j < materials.length; j++) {
                            if (materials[j].EstId > 0) {

                                ZoneInfo += "<tr>";
                                ZoneInfo += "<td>" + (count + 1) + "</td>";
                                ZoneInfo += "<td>" + materials[j].Material.MatName + "</td>";
                                ZoneInfo += "<td>" + materials[j].Unit.UnitName + "</td>";
                                ZoneInfo += "<td>" + materials[j].Qty + "</td>";
                                ZoneInfo += "<td>" + materials[j].PurchaseSource.PSName + "</td>";
                                if ($("input[id*='hdnIsAdmin']").val() == "4") {
                                    ZoneInfo += "<td class='left'>";
                                    ZoneInfo += "<table>";
                                    ZoneInfo += "<tr><td> <b>Name:</b> " + materials[j].Incharge.InName + " </td></tr>";
                                    if (materials[j].EmployeeAssignDateTime == "1/1/1900 12:00:00 AM") {
                                        ZoneInfo += "<tr><td><b>Assigned Date:</b> </td></tr>";
                                    }
                                    else {
                                        ZoneInfo += "<tr><td style='color:darkred;'><b>Assigned Date:</b> " + getJsonDate(materials[j].EmployeeAssignDateTime) + "</td></tr>";
                                    }
                                    ZoneInfo += "</table>";
                                    ZoneInfo += "</td>";
                                }


                                if (materials[j].DispatchDate != null) {
                                    ZoneInfo += "<td>" + getJsonDate(materials[j].DispatchDate) + "</td>";
                                }
                                else { ZoneInfo += "<td>Not Purchased</td>"; }


                                ZoneInfo += "<td>" + materials[j].remarkByPurchase + "</td>";

                                if ($("input[id*='hdnIsAdmin']").val() == "4") {
                                    ZoneInfo += "<td width='30%'><a href='javascript: openModelPopUp(" + Est.EstId + "," + materials.Sno + ");'><span class='label label-warning'  style='font-size: 15.998px;'>Reject Item</span></a></td>";
                                }

                                ZoneInfo += "</tr>";
                                count++;
                            }
                        }
                    }
                    ZoneInfo += "</tr>";
                    ZoneInfo += "</table>";
                    ZoneInfo += "</td>";
                }
                htmlContainer = rowTemplate + ZoneInfo + bottomrow;

                // htmlContainer.appendTo("#divEstimateDetails");
                $("#divEstimateDetails").empty();
                $("#divEstimateDetails").append(htmlContainer);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function LoadMaterialDepatchStatus(PsId, UserTypeId, InchargeId) {
    var ZoneInfo = "<div class='row-fluid sortable'>";
    ZoneInfo += "<div class='box span12'>";
    ZoneInfo += "<div class='box-header well' data-original-title>";
    ZoneInfo += "<h2><i class='icon-user'></i> Material Dispatch Details</h2>";
    ZoneInfo += "<div class='box-icon'>";
    ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
    ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
    ZoneInfo += "</div>";
    ZoneInfo += "</div>";
    ZoneInfo += "<div class='box-content'>";
    ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
    ZoneInfo += "<thead>";
    ZoneInfo += "<tr>";
    ZoneInfo += "<th style='display:none;'></th>";
    ZoneInfo += "<th style='display:none;'></th>";
    ZoneInfo += "</tr>";
    ZoneInfo += "</thead>";
    ZoneInfo += "<tbody>";
    /*create/distroy grid for the new search*/


    var rowTemplate = "<div class='row-fluid sortable'>";
    rowTemplate += "<div class='box span12'>";
    rowTemplate += "<div class='box-header well' data-original-title>";
    rowTemplate += "<h2><i class='icon-user'></i> Material Dispatch Details</h2>";
    rowTemplate += "<div class='box-icon'>";
    rowTemplate += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
    rowTemplate += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
    rowTemplate += "</div>";
    rowTemplate += "</div>";
    rowTemplate += "<div class='box-content'>";
    rowTemplate += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
    rowTemplate += "<thead>";
    rowTemplate += "<tr>";
    rowTemplate += "<th style='display:none;'></th>";
    rowTemplate += "<th style='display:none;'></th>";
    rowTemplate += "</tr>";
    rowTemplate += "</thead>";
    rowTemplate += "<tbody>";


    var bottomrow = "</tbody>";
    bottomrow += "</table>";
    bottomrow += "</div>";
    bottomrow += "</div>";
    bottomrow += "</div>";


    var htmlContainer = "";

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/MaterialDepatchStatus",
        data: JSON.stringify({ PSID: PsId, UserTypeID: UserTypeId, InchrgID: InchargeId }),
        dataType: "json",
        success: function (result, textStatus) {

            if (textStatus == "success") {
                var Est = result.d;


                var EmployeeType = "";
                var ZoneInfo = "";
                htmlContainer = rowTemplate;
                for (var i = 0; i < Est.length; i++) {

                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }


                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td style='display:none;'>1</td>";
                    ZoneInfo += "<td>";
                    ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td width='20%'><b style='color:red;'>Estimate No:</b> " + Est[i].EstId + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Sanction Date:</b> " + getJsonDate(Est[i].ModifyOn) + "</td>";
                    ZoneInfo += "<td class='center' width='25%'><b style='color:red;'>Sub Estimate:</b> " + Est[i].SubEstimate + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Academy:</b> " + Est[i].Academy.AcaName + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Zone:</b> " + Est[i].Zone.ZoneName + "</td>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='Purchase_ViewEstMaterial.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                    }
                    else if ($("input[id*='hdnIsAdmin']").val() == "12") {
                        ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='PurchaseEmployee_ViewEstMaterial.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                    }
                    else {
                        if (PsId == "1") {
                            ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?IsLocal=1&EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
                        }
                        else {
                            ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
                        }
                    }
                    ZoneInfo += "</tr>";
                    ZoneInfo += "</table>";
                    ZoneInfo += "<table border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
                    ZoneInfo += "<tr style='color:Green;'>";
                    ZoneInfo += "<th width='5%'><b>Sr. No.</b></th>";
                    ZoneInfo += "<th width='20%'>Material Name</th>";
                    ZoneInfo += "<th width='2%'>Unit</th>";
                    ZoneInfo += "<th width='2%'>Quantity</th>";
                    ZoneInfo += "<th width='5%'>Source Type</th>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<th width='27%'>Purchase Officer</th>";
                    }
                    ZoneInfo += "<th width='15%'>Purchase Date</th>";
                    ZoneInfo += "<th width='20%'>Remark</th>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<th width='0%'>Action</th>";
                    }
                    ZoneInfo += "</tr>";
                    var count = 0;
                    if (Est[i].EstimateAndMaterialOthersRelations != null) {
                        var materials = Est[i].EstimateAndMaterialOthersRelations;
                        for (var j = 0; j < materials.length; j++) {
                            if (materials[j].EstId > 0) {

                                ZoneInfo += "<tr>";
                                ZoneInfo += "<td>" + (count + 1) + "</td>";
                                ZoneInfo += "<td>" + materials[j].Material.MatName + "</td>";
                                ZoneInfo += "<td>" + materials[j].Unit.UnitName + "</td>";
                                ZoneInfo += "<td>" + materials[j].Qty + "</td>";
                                ZoneInfo += "<td>" + materials[j].PurchaseSource.PSName + "</td>";
                                if ($("input[id*='hdnIsAdmin']").val() == "4") {
                                    ZoneInfo += "<td class='left'>";
                                    ZoneInfo += "<table>";
                                    ZoneInfo += "<tr><td> <b>Name:</b> " + materials[j].Incharge.InName + " </td></tr>";
                                    if (materials[j].EmployeeAssignDateTime == "1/1/1900 12:00:00 AM") {
                                        ZoneInfo += "<tr><td><b>Assigned Date:</b> </td></tr>";
                                    }
                                    else {
                                        ZoneInfo += "<tr><td style='color:darkred;'><b>Assigned Date:</b> " + getJsonDate(materials[j].EmployeeAssignDateTime) + "</td></tr>";
                                    }
                                    ZoneInfo += "</table>";
                                    ZoneInfo += "</td>";
                                }

                                if (materials[j].DispatchDate != null) {
                                    ZoneInfo += "<td>" + getJsonDate(materials[j].DispatchDate) + "</td>";
                                }
                                else { ZoneInfo += "<td>Not Purchased</td>"; }

                                ZoneInfo += "<td>" + materials[j].remarkByPurchase + "</td>";

                                if ($("input[id*='hdnIsAdmin']").val() == "4") {
                                    ZoneInfo += "<td width='30%'><a href='javascript: openModelPopUp(" + Est.EstId + "," + materials.Sno + ");'><span class='label label-warning'  style='font-size: 15.998px;'>Reject Item</span></a></td>";
                                }

                                ZoneInfo += "</tr>";
                                count++;
                            }
                        }
                    }
                    ZoneInfo += "</tr>";
                    ZoneInfo += "</table>";
                    ZoneInfo += "</td>";
                }
                htmlContainer = rowTemplate + ZoneInfo + bottomrow;

                // htmlContainer.appendTo("#divEstimateDetails");
                $("#divEstimateDetails").empty();
                $("#divEstimateDetails").append(htmlContainer);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}

function LoadMaterialDepatchStatusByAcaID(PsId, InchargeId, AcaId) {
    var ZoneInfo = "<div class='row-fluid sortable'>";
    ZoneInfo += "<div class='box span12'>";
    ZoneInfo += "<div class='box-header well' data-original-title>";
    ZoneInfo += "<h2><i class='icon-user'></i> Material Dispatch Details</h2>";
    ZoneInfo += "<div class='box-icon'>";
    ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
    ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
    ZoneInfo += "</div>";
    ZoneInfo += "</div>";
    ZoneInfo += "<div class='box-content'>";
    ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
    ZoneInfo += "<thead>";
    ZoneInfo += "<tr>";
    ZoneInfo += "<th style='display:none;'></th>";
    ZoneInfo += "<th style='display:none;'></th>";
    ZoneInfo += "</tr>";
    ZoneInfo += "</thead>";
    ZoneInfo += "<tbody>";
    /*create/distroy grid for the new search*/


    var rowTemplate = "<div class='row-fluid sortable'>";
    rowTemplate += "<div class='box span12'>";
    rowTemplate += "<div class='box-header well' data-original-title>";
    rowTemplate += "<h2><i class='icon-user'></i> Material Dispatch Details</h2>";
    rowTemplate += "<div class='box-icon'>";
    rowTemplate += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
    rowTemplate += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
    rowTemplate += "</div>";
    rowTemplate += "</div>";
    rowTemplate += "<div class='box-content'>";
    rowTemplate += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
    rowTemplate += "<thead>";
    rowTemplate += "<tr>";
    rowTemplate += "<th style='display:none;'></th>";
    rowTemplate += "<th style='display:none;'></th>";
    rowTemplate += "</tr>";
    rowTemplate += "</thead>";
    rowTemplate += "<tbody>";


    var bottomrow = "</tbody>";
    bottomrow += "</table>";
    bottomrow += "</div>";
    bottomrow += "</div>";
    bottomrow += "</div>";


    var htmlContainer = "";

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/PurchaseControler.asmx/MaterialDepatchStatusByAcaID",
        data: JSON.stringify({ PSID: PsId, InchrgID: InchargeId, AcaID: AcaId }),
        dataType: "json",
        success: function (result, textStatus) {

            if (textStatus == "success") {
                var Est = result.d;


                var EmployeeType = "";
                var ZoneInfo = "";
                htmlContainer = rowTemplate;
                for (var i = 0; i < Est.length; i++) {

                    var className = "info";
                    if (i % 2 == 0) {
                        className = "warning";
                    }


                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td style='display:none;'>1</td>";
                    ZoneInfo += "<td>";
                    ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td width='20%'><b style='color:red;'>Estimate No:</b> " + Est[i].EstId + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Sanction Date:</b> " + getJsonDate(Est[i].ModifyOn) + "</td>";
                    ZoneInfo += "<td class='center' width='25%'><b style='color:red;'>Sub Estimate:</b> " + Est[i].SubEstimate + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Academy:</b> " + Est[i].Academy.AcaName + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Zone:</b> " + Est[i].Zone.ZoneName + "</td>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='Purchase_ViewEstMaterial.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                    }
                    else if ($("input[id*='hdnIsAdmin']").val() == "12") {
                        ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='PurchaseEmployee_ViewEstMaterial.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                    }
                    else {
                        if ($("input[id*='hdnPSID']").val() == "1") {
                            ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?IsLocal=1&EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
                        }
                        else {
                            ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est[i].EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
                        }
                    }
                    ZoneInfo += "</tr>";
                    ZoneInfo += "</table>";
                    ZoneInfo += "<table border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
                    ZoneInfo += "<tr style='color:Green;'>";
                    ZoneInfo += "<th width='5%'><b>Sr. No.</b></th>";
                    ZoneInfo += "<th width='20%'>Material Name</th>";
                    ZoneInfo += "<th width='2%'>Unit</th>";
                    ZoneInfo += "<th width='2%'>Quantity</th>";
                    ZoneInfo += "<th width='5%'>Source Type</th>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<th width='27%'>Purchase Officer</th>";
                    }
                    ZoneInfo += "<th width='15%'>Purchase Date</th>";
                    ZoneInfo += "<th width='20%'>Remark</th>";
                    if ($("input[id*='hdnIsAdmin']").val() == "4") {
                        ZoneInfo += "<th width='0%'>Action</th>";
                    }
                    ZoneInfo += "</tr>";
                    var count = 0;
                    if (Est[i].EstimateAndMaterialOthersRelations != null) {
                        var materials = Est[i].EstimateAndMaterialOthersRelations;
                        for (var j = 0; j < materials.length; j++) {
                            if (materials[j].EstId > 0) {

                                ZoneInfo += "<tr>";
                                ZoneInfo += "<td>" + (count + 1) + "</td>";
                                ZoneInfo += "<td>" + materials[j].Material.MatName + "</td>";
                                ZoneInfo += "<td>" + materials[j].Unit.UnitName + "</td>";
                                ZoneInfo += "<td>" + materials[j].Qty + "</td>";
                                ZoneInfo += "<td>" + materials[j].PurchaseSource.PSName + "</td>";
                                if ($("input[id*='hdnIsAdmin']").val() == "4") {
                                    ZoneInfo += "<td class='left'>";
                                    ZoneInfo += "<table>";
                                    ZoneInfo += "<tr><td> <b>Name:</b> " + materials[j].Incharge.InName + " </td></tr>";
                                    if (materials[j].EmployeeAssignDateTime == "1/1/1900 12:00:00 AM") {
                                        ZoneInfo += "<tr><td><b>Assigned Date:</b> </td></tr>";
                                    }
                                    else {
                                        ZoneInfo += "<tr><td style='color:darkred;'><b>Assigned Date:</b> " + getJsonDate(materials[j].EmployeeAssignDateTime) + "</td></tr>";
                                    }
                                    ZoneInfo += "</table>";
                                    ZoneInfo += "</td>";
                                }

                                if (materials[j].DispatchDate != null) {
                                    ZoneInfo += "<td>" + getJsonDate(materials[j].DispatchDate) + "</td>";
                                }
                                else { ZoneInfo += "<td>Not Purchased</td>"; }

                                ZoneInfo += "<td>" + materials[j].remarkByPurchase + "</td>";

                                if ($("input[id*='hdnIsAdmin']").val() == "4") {
                                    ZoneInfo += "<td width='30%'><a href='javascript: openModelPopUp(" + Est.EstId + "," + materials.Sno + ");'><span class='label label-warning'  style='font-size: 15.998px;'>Reject Item</span></a></td>";
                                }

                                ZoneInfo += "</tr>";
                                count++;
                            }
                        }
                    }
                    ZoneInfo += "</tr>";
                    ZoneInfo += "</table>";
                    ZoneInfo += "</td>";
                }
                htmlContainer = rowTemplate + ZoneInfo + bottomrow;

                // htmlContainer.appendTo("#divEstimateDetails");
                $("#divEstimateDetails").empty();
                $("#divEstimateDetails").append(htmlContainer);
            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });
}
   
