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
                var initKota = document.getElementById("InitKota").value;
                self.populateLocation(initKota);

                
                if (initKota > 0) {
                    $("#sortContainer").addClass('mr-3');
                    $("#locationContainer").removeClass('d-none');
                    $("#genreContainer").addClass('d-none');
                    $("#monthContainer").addClass('d-none');
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

    self.populateLocation = function (initKota) {
        var row = "";
        $("#ddlLocation").empty();
        row += "<option value='' disabled hidden>Choose location</option>";
        $.each(globalLocation, function (i, v) {
            row += "<option value=" + v.value + ">" + v.text + "</option>";
        });
        $("#ddlLocation").html(row);
        if (initKota == 0) {
            $("#ddlLocation").val('');
        }
    };
}

var globalDataFilter = new dataFilter();

$(document).ready(function () {
    var initGenre = document.getElementById("InitGenre").value;
    var initMonth = document.getElementById("InitMonth").value;
    globalDataFilter.getDefaultKota();
    if (initGenre) {
        $("#sortContainer").addClass('mr-3');
        $("#genreContainer").removeClass('d-none');
        $("#locationContainer").addClass('d-none');
        $("#monthContainer").addClass('d-none');
        $("#sortCategory").val('Type');
        $("#ddlGenre").val(initGenre);
    }
    if (initMonth > 0) {
        $("#sortContainer").addClass('mr-3');
        $("#monthContainer").removeClass('d-none');
        $("#locationContainer").addClass('d-none');
        $("#genreContainer").addClass('d-none');
        $("#sortCategory").val('Month');
        $("#ddlMonth").val(initMonth);
    }

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
        $("#monthContainer").addClass('d-none');
        $("#genreContainer").val('').change();
        $("#monthContainer").val('').change();
    }
    else if ($('#sortCategory').val() === 'Type') {
        $("#sortContainer").addClass('mr-3');
        $("#genreContainer").removeClass('d-none');
        $("#locationContainer").addClass('d-none');
        $("#monthContainer").addClass('d-none');
        $("#locationContainer").val('').change();
        $("#monthContainer").val('').change();
    }
    else if ($('#sortCategory').val() === 'Month') {
        $("#sortContainer").addClass('mr-3');
        $("#monthContainer").removeClass('d-none');
        $("#locationContainer").addClass('d-none');
        $("#genreContainer").addClass('d-none');
        $("#locationContainer").val('').change();
        $("#genreContainer").val('').change();
    }
    else if ($('#sortCategory').val() === 'All') {
        window.location.href = '/Home/HomeCreator';
    }
});

$(document).on("change", "#ddlGenre", function () {
    var genre = $("#ddlGenre").val();
    var search = $("#searchBox").val();

    var link = "/Home/HomeCreator?";
    if (genre) {
        link = link + 'genreString=' + genre + '&&';
    }
    if (search) {
        link = link + 'searchString=' + search + '&&';
    }
    window.location.href = link
});

$(document).on("change", "#ddlLocation", function () {
    var location = $("#ddlLocation").val();
    var search = $("#searchBox").val();

    var link = "/Home/HomeCreator?";

    if (location > 0) {
        link = link + 'locationInt=' + location + '&&';
    }
    if (search) {
        link = link + 'searchString=' + search + '&&';
    }
    window.location.href = link
});

$(document).on("change", "#ddlMonth", function () {
    var month = $("#ddlMonth").val();
    var search = $("#searchBox").val();

    var link = "/Home/HomeCreator?";
    if (month > 0) {
        link = link + 'monthInt=' + month + '&&';
    }
    if (search) {
        link = link + 'searchString=' + search + '&&';
    }
    window.location.href = link
});

$(document).on("click", "#BtnSearch", function () {
    var location = $("#ddlLocation").val();
    var genre = $("#ddlGenre").val();
    var month = $("#ddlMonth").val();
    var search = $("#searchBox").val();

    var link = "/Home/HomeCreator?";

    if (location > 0) {
        link = link + 'locationInt=' + location + '&&';
    }
    if (genre) {
        link = link + 'genreString=' + genre + '&&';
    }
    if (month > 0) {
        link = link + 'monthInt=' + month + '&&';
    }
    if (search) {
        link = link + 'searchString=' + search + '&&';
    }
    window.location.href = link
});

$(document).on("click", "#BtnNext", function () {
    var initPage = $("#InitPage").val();
    var location = $("#ddlLocation").val();
    var genre = $("#ddlGenre").val();
    var month = $("#ddlMonth").val();
    var search = $("#searchBox").val();
    var nextPage = +initPage + +1;
    var link = "/Home/HomeCreator?";
    if (location > 0) {
        link = link + 'locationInt=' + location + '&&';
    }
    if (genre) {
        link = link + 'genreString=' + genre + '&&';
    }
    if (month > 0) {
        link = link + 'monthInt=' + month + '&&';
    }
    if (search) {
        link = link + 'searchString=' + search + '&&';
    }
    window.location.href = link + 'pageNumber=' + nextPage;
});

$(document).on("click", "#BtnPrevious", function () {
    var initPage = $("#InitPage").val();
    var location = $("#ddlLocation").val();
    var genre = $("#ddlGenre").val();
    var month = $("#ddlMonth").val();
    var search = $("#searchBox").val();
    var nextPage = +initPage - +1;
    var link = "/Home/HomeCreator?";
    if (location > 0) {
        link = link + 'locationInt=' + location + '&&';
    }
    if (genre) {
        link = link + 'genreString=' + genre + '&&';
    }
    if (month > 0) {
        link = link + 'monthInt=' + month + '&&';
    }
    if (search) {
        link = link + 'searchString=' + search + '&&';
    }
    window.location.href = link + 'pageNumber=' + nextPage;
});


