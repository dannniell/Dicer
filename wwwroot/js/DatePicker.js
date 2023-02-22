$(document).ready(function () {
    $('#datepicker').datepicker({
        onSelect: function (date) {
            $('#datepicker').val(date);
        },
    }).change(function () {
        var changeDob = document.getElementById("datepicker").value;
        $("#hiddenDob").val(changeDob);
    });
    var initDob = document.getElementById("InitDob").value;
    $("#datepicker").val(initDob);
});