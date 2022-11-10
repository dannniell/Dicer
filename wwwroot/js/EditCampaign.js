$(document).ready(function () {
    var InitType = document.getElementById("InitType").value;
    var InitGenre = document.getElementById("InitGenre").value;
    var InitGender = document.getElementById("InitGender").value;

    $("#ddlContentType").val(InitType);
    $("#ddlGenre").val(InitGenre);
    $("#Gender").val(InitGender);
});