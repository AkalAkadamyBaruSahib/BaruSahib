
/// <reference path="../jquery-vsdoc.js" />

$(document).ready(function () {

    $("input[id*='fiupload_0']").change(function (e) {
        var ext = $(this).val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'pdf']) == -1) {
            $(this).val("");
            alert('Invalid extension! Please Upload the .jpg,jpeg,gif,png,pdf Files');
        }
        else {
            var $fileUpload = $(this)[0];
            FileUpload($fileUpload, "Registration", 0);
        }
    });


    $("input[id*='fiupload_1']").change(function () {
        var ext = $(this).val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'pdf']) == -1) {
            $(this).val("");
            alert('Invalid extension! Please Upload the .jpg,jpeg,gif,png,pdf Files');
        }
        else {
            var $fileUpload = $(this)[0];
            FileUpload($fileUpload, "Passing", 1);
        }
    });


    $("input[id*='fiupload_2']").change(function () {
        var ext = $(this).val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'pdf']) == -1) {
            $(this).val("");
            alert('Invalid extension! Please Upload the .jpg,jpeg,gif,png,pdf Files');
        }
        else {
            var $fileUpload = $(this)[0];
            FileUpload($fileUpload, "Insurance", 2);
        }
    });


    $("input[id*='fiupload_3']").change(function () {
        var ext = $(this).val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'pdf']) == -1) {
            $(this).val("");
            alert('Invalid extension! Please Upload the .jpg,jpeg,gif,png,pdf Files');
        }
        else {
            var $fileUpload = $(this)[0];
            FileUpload($fileUpload, "Tax", 3);
        }
    });


    $("input[id*='fiupload_4']").change(function () {
        var ext = $(this).val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'pdf']) == -1) {
            $(this).val("");
            alert('Invalid extension! Please Upload the .jpg,jpeg,gif,png,pdf Files');
        }
        else {
            var $fileUpload = $(this)[0];
            FileUpload($fileUpload, "Permit", 4);
        }
    });


    $("input[id*='fiupload_5']").change(function () {
        var ext = $(this).val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'pdf']) == -1) {
            $(this).val("");
            alert('Invalid extension! Please Upload the .jpg,jpeg,gif,png,pdf Files');
        }
        else {
            var $fileUpload = $(this)[0];
            FileUpload($fileUpload, "Pollution", 5);
        }
    });


    $("input[id*='fiupload_6']").change(function () {
        var ext = $(this).val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'pdf']) == -1) {
            $(this).val("");
            alert('Invalid extension! Please Upload the .jpg,jpeg,gif,png,pdf Files');
        }
        else {
            var $fileUpload = $(this)[0];
            FileUpload($fileUpload, "Written Contract", 6);
        }
    });

    $("input[id*='fiupload_7']").change(function () {
            var $fileUpload = $(this)[0];
            FileUpload($fileUpload, "Route Map", 7);
    });

});



function FileUpload(control, docName, cnt) {

    if ($("input[id*='txtDate_" + cnt + "']").val() != "(mm/dd/yyyy)")
    {
        $("#progress").dialog({ modal: true, width: 400, height: 200, title: "Progress", closeOnEscape: false });
        $("#progress").dialog('open');

        //var VehicleNumber = $("input[id*='txtVehicleNo1']").val() + "-" + $("input[id*='txtVehicleNo2']").val() + "-" + $("input[id*='txtVehicleNo3']").val() + "-" + $("input[id*='txtVehicleNo4']").val();
        var VehicleNumber = $("select[id*='drpVehicle'] :selected").text();
        var name = docName + "_" + VehicleNumber;
        var ID = $("span[id*='lblDocu_" + cnt + "']").text();

        var DocumentTypeID = $("span[id*='lblDocumentTypeID_" + cnt + "']").text();
        var date = $("input[id*='txtDate_" + cnt + "']").val();
        var VehicleID = $("select[id*='drpVehicle']").val();

        var files = control.files;

        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
        }

        var ext = files[0].name.split('.').pop();

        $.ajax({
            url: "TransportVehicleDocumentUploader.ashx?name=" + name + " &ID=" + ID + " &VehicleID=" + VehicleID + " &DocumentTypeID=" + DocumentTypeID + " &date=" + date,
            type: "POST",
            async: false,
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                $("a[id*='hypDoc_" + cnt + "']")[0].innerHTML = name + "." + ext;
                //var path = "./VehicleDoc/" + name + "." + ext;
                //$("a[id*='hypDoc_" + cnt + "']")[0].href = "<%=Page.ResolveUrl('" + path + "')%>";
                $("a[id*='hypDoc_" + cnt + "']")[0].href = "../../VehicleDoc/" + name + "." + ext;
                $("#progress").dialog('close');
              
                alert("File has been uploaded successfully");
            },
            error: function (err) {
                alert(err.statusText)
            }
        });
    }
    else {
        $("input[id*='fiupload_" + cnt + "']").val("");
        alert("Please enter document expiry date");
    }

}
