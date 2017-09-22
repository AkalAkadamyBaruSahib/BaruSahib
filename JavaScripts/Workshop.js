var d = new Date();
var strDate = d.getDate() + "/" + (d.getMonth() + 1) + "/" +  d.getFullYear() ;
$(document).ready(function () {
    if ($("input[id*='hdnUserType']").val() == 6) {
        LoadMaterialInfo(0);
    }
    else {
        LoadMaterialInfo($("input[id*='hdnAcaID']").val());
    }
    $("select[id*='ddlworkshop']").change(function () {
        LoadMaterialInfo($(this).val());
    });
 
});


function LoadMaterialInfo(acaid) {

      if (typeof grdEstimateDiscription != 'undefined') {
            grdEstimateDiscription.fnClearTable();
        }

        var rowCount = $('#grid').find("#rowTemplate").length;
        for (var i = 0; i < rowCount; i++) {
            $("#rowTemplate").remove();
        }
        var rowTemplate = '<tr id="rowTemplate"><td id="srno"></td><td id="materialname">abc</td><td id="rate">abc</td><td id="instore">abc</td><td id="action">cc</td></tr>';

        var adminLoanList = 0;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "Services/WorkshopController.asmx/GetWorkShopMaterials",
            data: JSON.stringify({ AcaID: parseInt(acaid) }),
            dataType: "json",
            success: function (result, textStatus) {
                if (textStatus == "success") {
                    adminLoanList = result.d;

                    if (adminLoanList.length > 0) {

                        $("#tbody").append(rowTemplate);
                    }

                    for (var i = 0; i < adminLoanList.length; i++) {
                        var className = "info";
                        if (i % 2 == 0) {
                            className = "warning";
                        }
                        var $newRow = $("#rowTemplate").clone();
                        $newRow.find("#srno").html("<input type='hidden' value='" + adminLoanList[i].ID + "' id='hdnID" + i + "' /><span id=spn" + i + ">" + (i + 1) + "</span>");

                        if (acaid > 0) {
                            $newRow.find("#materialname").html("<input type='hidden' value='" + adminLoanList[i].MatID + "' id='hdnmatid" + i + "' />" + adminLoanList[i].Material.MatName);
                        }
                        else {
                            $newRow.find("#materialname").html("<table><tr><td><input type='hidden' value='" + adminLoanList[i].MatID + "' id='hdnmatid" + i + "' />" + adminLoanList[i].Material.MatName + "</td></tr><tr><td><b>Workshop Name:-</b>" + adminLoanList[i].Academy.AcaName);
                        }
                        $newRow.find("#rate").html("<input type='hidden' value='" + adminLoanList[i].AcaID + "' id='hdnAcaID" + i + "' /><input style='width:100px;' value='" + adminLoanList[i].Material.AkalWorkshopRate + "' type='text' id='txtRate" + i + "' name='txtRate'>");
                        $newRow.find("#instore").html("<input style='width:100px;' type='text' id='txtInStore" + i + "' value='" + adminLoanList[i].InStoreQty + "'  name='txtInStore'>");
                        $newRow.find("#action").html("<input style='width:100px;'  class='btn btn-primary' type='button' id='btnUpdate" + i + "' name='btnUpdate' value='Update' onclick='return UpdateWorkshpMaterial(" + i + ")'>");
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
                    grdEstimateDiscription = $('#grid').DataTable(
                    {
                        "bPaginate": false,
                        "bDestroy": true,
                         "bFilter": true
                        //"bFilter": false,
                        //"aaSorting": []
                    });
                    DisableControl();

                }
               
            },
            error: function (result, textStatus) {
                alert(result.responseText);
            }
        });
       
}

function UpdateWorkshpMaterial(rowid) {
    if ($("#txtRate" + rowid).val() == "undefined" || $("#txtRate" + rowid).val() == "0" || $("#txtRate" + rowid).val() == "") {
        alert("Please Enter the Rate");
        return false;
    }
    else if ($("#txtInStore" + rowid).val() == "" || $("#txtInStore" + rowid).val() == "undefined") {
        alert("Please Enter the InstoreQty");
      return false;
  }
  else {
      var params = new Object();
      var WorkshopStoreMaterialDTO = new Object();


      WorkshopStoreMaterialDTO.ID = $("#hdnID" + rowid).val();
      WorkshopStoreMaterialDTO.MatId = $("#hdnmatid" + rowid).val();
      WorkshopStoreMaterialDTO.InStoreQty = $("#txtInStore" + rowid).val();
      WorkshopStoreMaterialDTO.Rate = $("#txtRate" + rowid).val();
      WorkshopStoreMaterialDTO.ModifyBy = $("input[id*='hdnInchargeID']").val();
      WorkshopStoreMaterialDTO.AcaID = $("#hdnAcaID" + rowid).val();
      params.workshopStoreMaterialDTO = WorkshopStoreMaterialDTO;

      $.ajax({
          type: "POST",
          contentType: "application/json; charset=utf-8",
          url: "Services/WorkshopController.asmx/UpdateWorkshopMaterial",
          data: JSON.stringify(params),
          dataType: "json",
          success: function (result, textStatus) {
              if (textStatus == "success") {
                  alert("Record Update Successflly");
              }
          },
          error: function (response) {
              alert(response.status + '' + response.textStatus);
          }
      });
  }
}

function DisableControl() {
    var tablelength = $("#tbody").children('tr').length;
    for (var i = 0 ; i < tablelength; i++) {
        if ($("input[id*='hdnUserType']").val() == 6) {
            $("#txtRate" + i).prop('disabled', true);
            $("#txtInStore" + i).prop('disabled', true);
            $("#btnUpdate" + i).prop('disabled', true);
        }
        else {
            $("#txtRate" + i).prop('disabled', false);
            $("#txtInStore" + i).prop('disabled', false);
            $("#btnUpdate" + i).prop('disabled', false);
        }
    }
}








