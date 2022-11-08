$(document).ready(function () {
    //todo
    var cards = document.querySelectorAll(".card");
    for (var i = 0; i < cards.length; i++) {
        cards[i].addEventListener('click', function (e) {
            var link = this.querySelector(".IdCard");
            window.location.href = '/Campaign/DetailClient?id=' + link.value;
        }, false);
    }
});