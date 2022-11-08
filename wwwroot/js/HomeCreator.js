var dataFilter = function () {
    var self = this;
    var globalLocation;

    self.getDefaultKota = function () {
        var ajaxTypesObj = {
            url: "/Api/location/GetProvinsi",
            method: "GET",
            dataType: "json",
            success: function (data) {
                globalLocation = data;
                self.populateLocation();

                var initKota = document.getElementById("InitKota").value;
                if (initKota > 0) {
                    $("#sortContainer").addClass('mr-3');
                    $("#locationContainer").removeClass('d-none');
                    $("#genreContainer").addClass('d-none');
                    $("#sortCategory").val('Location');
                    $("#ddlLocation").val(initKota);
                }
            },
            error: function (e) {
                alert('Failed to Insert Transaction Detail Data!');
            },
            complete: function (data) {
                //alert('Transaction Detail Data Successfully Inserted!');
            }
        };
        $.ajax(ajaxTypesObj);
    };

    self.populateLocation = function () {
        var row = "";
        $("#ddlLocation").empty();
        $.each(globalLocation, function (i, v) {
            row += "<option value=" + v.value + ">" + v.text + "</option>";
        });
        $("#ddlLocation").html(row);
    };
}

var globalDataFilter = new dataFilter();

$(document).ready(function () {
    var initGenre = document.getElementById("InitGenre").value;
    globalDataFilter.getDefaultKota();
    if (initGenre !== "") {
        $("#sortContainer").addClass('mr-3');
        $("#genreContainer").removeClass('d-none');
        $("#locationContainer").addClass('d-none');
        $("#sortCategory").val('Type');
        $("#ddlGenre").val(initGenre);
    }

    //todo
    var cards = document.querySelectorAll(".card");
    for (var i = 0; i < cards.length; i++) {
        cards[i].addEventListener('click', function (e) {
            var link = this.querySelector(".IdCard");
            window.location.href = '/Campaign/Detail?id=' + link.value;
        }, false);
    }
});

$(document).on("change", "#sortCategory", function () {
    if ($('#sortCategory').val() === 'Location') {
        $("#sortContainer").addClass('mr-3');
        $("#locationContainer").removeClass('d-none');
        $("#genreContainer").addClass('d-none');
        $("#genreContainer").val('').change();
    }
    else if ($('#sortCategory').val() === 'Type') {
        $("#sortContainer").addClass('mr-3');
        $("#genreContainer").removeClass('d-none');
        $("#locationContainer").addClass('d-none');
        $("#locationContainer").val('').change();
    }
    else if ($('#sortCategory').val() === 'All') {
        window.location.href = '/Home/HomeCreator';
        /*$("#sortContainer").removeClass('mr-3');
        $("#locationContainer").addClass('d-none');
        $("#locationContainer").val('').change();
        $("#genreContainer").addClass('d-none');
        $("#genreContainer").val('').change();*/
    }
});

$(document).on("change", "#ddlGenre", function () {
    var genre = $("#ddlGenre").val();

    window.location.href = '/Home/HomeCreator?genreString=' + genre;
});

$(document).on("change", "#ddlLocation", function () {
    var location = $("#ddlLocation").val();

    window.location.href = '/Home/HomeCreator?locationInt=' + location;
});

$(document).on("click", "#BtnNext", function () {
    var initPage = $("#InitPage").val();
    var location = $("#ddlLocation").val();
    var genre = $("#ddlGenre").val();
    var search = $("#searchBox").val();
    var nextPage = +initPage + +1;
    if (location > 0) {
        window.location.href = '/Home/HomeCreator?locationInt=' + location + '&&pageNumber=' + nextPage;
    }
    if (genre !== "") {
        window.location.href = '/Home/HomeCreator?genreString=' + genre + '&&pageNumber=' + nextPage;
    }
    if (search !== "") {
        window.location.href = '/Home/HomeCreator?searchString=' + search + '&&pageNumber=' + nextPage;
    }
});

$(document).on("click", "#BtnPrevious", function () {
    var initPage = $("#InitPage").val();
    var location = $("#ddlLocation").val();
    var genre = $("#ddlGenre").val();
    var search = $("#searchBox").val();
    var nextPage = +initPage - +1;
    if (location > 0) {
        window.location.href = '/Home/HomeCreator?locationInt=' + location + '&&pageNumber=' + nextPage;
    }
    if (genre !== "") {
        window.location.href = '/Home/HomeCreator?genreString=' + genre + '&&pageNumber=' + nextPage;
    }
    if (search !== "") {
        window.location.href = '/Home/HomeCreator?searchString=' + search + '&&pageNumber=' + nextPage;
    }
});


