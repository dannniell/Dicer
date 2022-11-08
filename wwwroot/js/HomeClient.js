$(document).ready(function () {
    //todo
    var cards = document.querySelectorAll(".card");
    for (var i = 0; i < cards.length; i++) {
        cards[i].addEventListener('click', function (e) {
            var link = this.querySelector(".IdCard");
            window.location.href = '/Campaign/Detail?id=' + link.value;
        }, false);
    }
});