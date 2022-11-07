$(document).ready(function () {
    var InitType = document.getElementById("InitType").value;
    var InitGenre = document.getElementById("InitGenre").value;
    var InitGender = document.getElementById("InitGender").value;

    $("#ddlContentType").val(InitType);
    $("#ddlGenre").val(initGenre);
    $("#Gender").val(InitGender);

    //todo
    var cards = document.querySelectorAll(".card");
    for (var i = 0; i < cards.length; i++) {
        cards[i].addEventListener('click', function (e) {
            var link = this.querySelector(".IdCard");
            window.location.href = '/Branch/Details/' + link.value;
        }, false);
    }
});