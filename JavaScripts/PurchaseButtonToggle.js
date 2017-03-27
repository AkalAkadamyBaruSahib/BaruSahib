$(document).ready(function () {
});

function imagesChanges(estid) {
    $("[id*=tblmat] [id*=imgMinus" + estid + "]").show();
    $("[id*=tblmat] [id*=imgPlus" + estid + "]").hide();
}
function imagesMinusChanges(estid) {
    $("[id*=tblmat] [id*=imgPlus" + estid + "]").show();
    $("[id*=tblmat] [id*=imgMinus" + estid + "]").hide();
}