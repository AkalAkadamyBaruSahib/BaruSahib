
/// <reference path="../jquery-vsdoc.js" />

$(document).ready(function () {

    $("input[id*='fiupload_0']").change(function (e) {
        // select the form and submit
        var $fileUpload = $(this)[0];
        FileUpload($fileUpload, "Registration");
    });
    $("input[id*='fiupload_1']").change(function () {
        var $fileUpload = $(this)[0];
        FileUpload($fileUpload, "Passing");
    });
    $("input[id*='fiupload_2']").change(function () {
        var $fileUpload = $(this)[0];
        FileUpload($fileUpload, "Insurance");
    });
    $("input[id*='fiupload_3']").change(function () {
        var $fileUpload = $(this)[0];
        FileUpload($fileUpload, "Tax");
    });
    $("input[id*='fiupload_4']").change(function () {
        var $fileUpload = $(this)[0];
        FileUpload($fileUpload, "Permit");
    });
    $("input[id*='fiupload_5']").change(function () {
        var $fileUpload = $(this)[0];
        FileUpload($fileUpload, "Pollution");
    });

    $("input[id*='fiupload_6']").change(function () {
        var $fileUpload = $(this)[0];
        FileUpload($fileUpload, "Written Contract");
    });
   
});



function FileUpload(control, docName) {

    var VehicleNumber = $("input[id*='txtVehicleNo1']").val() + "-" + $("input[id*='txtVehicleNo2']").val() + "-" + $("input[id*='txtVehicleNo3']").val() + "-" + $("input[id*='txtVehicleNo4']").val();
    var name = docName + "_" + VehicleNumber;

    var files = control.files;

    var data = new FormData();
    for (var i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }

    $.ajax({
        url: "TransportVehicleDocumentUploader.ashx?name=" + name,
        type: "POST",
        async: false,
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            $("a[id*='hypDoc_0']").innerHTML = name;
        },
        error: function (err) {
            alert(err.statusText)
        }
    });
}
