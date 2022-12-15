$(document).ready(function () {
    $("#datepicker").datepicker();
    var initDob = document.getElementById("InitDob").value;
    $("#datepicker").val(initDob);
});

$('#datepicker').on("change", function () {
    var changeDob = document.getElementById("datepicker").value;
    $("#hiddenDob").val(changeDob);
});
