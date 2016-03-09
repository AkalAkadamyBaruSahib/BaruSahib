var cntB = 2;
$(document).ready(function () {
    $("input[id*='btnUploadSave']").click(function () {
        SaveStoreBill();
    });

   
});
function addNewBill() {
    $('#tblUploadBill tr').last().after('<tr id="trB' + cntB + '"><td><input type="text" style=" width: 143px;height: 18px;" id="txtBillNo' + cntB + '" name="txtBillNo' + cntB + '" value=""></td><td><input type="text" style=" width: 143px;height: 18px;"  id="txtBillName' + cntB + '" name="txtBillName' + cntB + '" value=""></td><td><input id="uploadePurchaseFile' + cntB + '" type="file"  name="uploadePurchaseFile' + cntB + '"/></td><td><a href="javascript:void(0);" onclick="addNewBill(' + cntB + ');"><input id="btnadd" class="btn btn-primary" value="+"  style="width: 10px"/></a></td><td><a href="javascript:void(0);" onclick="removeRow(' + cntB + ');"><input id="btnremove" class="btn btn-primary" value="-" style="width:10px"/></a></td></tr>');
    cntB++;
}
function SaveStoreBill() {
    for (var i = 1; i < cntB; i++) {

        var paramBill = new Object();

        var StoreMaterialBill = new Object();

        var hdnEstId = $("input[id*='hdnEstID']").val();
        StoreMaterialBill.EstID = hdnEstId;
        StoreMaterialBill.BillName = $("input[id*='txtBillName" + i + "']").val();
        StoreMaterialBill.BillNo = $("input[id*='txtBillNo" + i + "']").val();
        StoreMaterialBill.BillPath = "Bills/" + $("#uploadePurchaseFile" + i)[0].files[0].name;


        paramBill.StoreMaterialBill = StoreMaterialBill;

        $.ajax({
            type: "POST",
            async: false,
            contentType: "application/json; charset=utf-8",
            url: "Services/StoreController.asmx/SaveStoreBill",
            data: JSON.stringify(paramBill),
            dataType: "json",
            success: function (result, textStatus) {
                if (textStatus == "success") {
                    StoreBillUpload(i);
                    // ClearTextBox();
                }
            },
            error: function (result, textStatus) {
                alert(result.responseText)
            }
        });
    }

}

function StoreBillUpload(cnt) {
    var files = $("#uploadePurchaseFile" + cnt)[0].files;
    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }

    $.ajax({
        url: "StoreBillHandler.ashx",
        type: "POST",
        async: false,
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            //alert(result);
        },
        error: function (err) {
            alert(err.statusText)
        }
    });
}

function ClearTextBox() {
    $("input[id*='txtBillName']").val("");
    $("input[id*='txtBillNo']").val("");
    cntB = 2;
}

function removeRow(removeNum) {
    $('#trB' + removeNum).remove();
    cntB--;
}