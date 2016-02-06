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
    