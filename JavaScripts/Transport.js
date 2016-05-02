
/// <reference path="../jquery-vsdoc.js" />

$(document).ready(function () {

    $("input[id*='fiupload_0']").change(function (e) {
        // select the form and submit
        var $fileUpload = $(this)[0];
        FileUpload($fileUpload, "Registration", 0);
    });
    $("input[id*='fiupload_1']").change(function () {
        var $fileUpload = $(this)[0];
        FileUpload($fileUpload, "Passing", 1);
    });
    $("input[id*='fiupload_2']").change(function () {
        var $fileUpload = $(this)[0];
        FileUpload($fileUpload, "Insurance", 2);
    });
    $("input[id*='fiupload_3']").change(function () {
        var $fileUpload = $(this)[0];
        FileUpload($fileUpload, "Tax", 3);
    });
    $("input[id*='fiupload_4']").change(function () {
        var $fileUpload = $(this)[0];
        FileUpload($fileUpload, "Permit", 4);
    });
    $("input[id*='fiupload_5']").change(function () {
        var $fileUpload = $(this)[0];
        FileUpload($fileUpload, "Pollution", 5);
    });

    $("input[id*='fiupload_6']").change(function () {
        var $fileUpload = $(this)[0];
        FileUpload($fileUpload, "Written Contract", 6);
    });

});



function FileUpload(control, docName, cnt) {

    $("#progress").dialog({ modal: true, width: 400, height: 200, title: "Progress", closeOnEscape: false });
    $("#progress").dialog('open');

    var VehicleNumber = $("input[id*='txtVehicleNo1']").val() + "-" + $("input[id*='txtVehicleNo2']").val() + "-" + $("input[id*='txtVehicleNo3']").val() + "-" + $("input[id*='txtVehicleNo4']").val();
    var name = docName + "_" + VehicleNumber;

    var files = control.files;

    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }

    var ext = files[0].name.split('.').pop();

    $.ajax({
        url: "TransportVehicleDocumentUploader.ashx?name=" + name,
        type: "POST",
        async: false,
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            $("a[id*='hypDoc_" + cnt + "']")[0].innerHTML = name + "." + ext;
            $("a[id*='hypDoc_" + cnt + "']")[0].href = "VehicleDoc/" + name + "." + ext;
            $("#progress").dialog('close');
        },
        error: function (err) {
            alert(err.statusText)
        }
    });
}
