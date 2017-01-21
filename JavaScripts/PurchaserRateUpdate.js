$(document).ready(function () {
    $("#aRateUpdateLink").click(function (e) {
        var MatTypeID = $("input[id*='hdnMaterialType']").val();
        var MatID = $("input[id*='hdnMaterialID']").val();
        window.location.replace("RateUpload.aspx?MatTypeID=" + MatTypeID + "&MatID=" + MatID);
    });
});

function OpenUpdateRatePopUp() {
     $("input[id*='hdnMaterialType']").val(MatTypeID);
    $("input[id*='hdnMaterialID']").val(MatID);
    $('#divUpdateRate').modal('show');
}

function ShowPopup(MatTypeID, MatID) {
    $("input[id*='hdnMaterialType']").val(MatTypeID);
    $("input[id*='hdnMaterialID']").val(MatID);
    $(function () {
        $("#divUpdateRate").dialog({
            title: "Material Rate Update",
            buttons: {
                Close: function () {
                    $(this).dialog('close');
                }
            },
            modal: true, width: 450
        });
    });
};