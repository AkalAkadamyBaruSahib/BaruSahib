$(document).ready(function () {
    AutofillEmployeeSearchBox();
});

function AutofillEmployeeSearchBox() {
    var dataSrc;

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Services/SecurityController.asmx/GetActiveSecurityEmployee",
        dataType: "json",
        success: function (result, textStatus) {
            if (textStatus == "success") {
                $("#txtEmpName").autocomplete({
                    source: result.d
                });

            }
        },
        error: function (result, textStatus) {
            alert(result.responseText);
        }
    });


}