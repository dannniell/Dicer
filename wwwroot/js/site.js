// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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
        $("#sortContainer").removeClass('mr-3');
        $("#locationContainer").addClass('d-none');
        $("#locationContainer").val('').change();
        $("#genreContainer").addClass('d-none');
        $("#genreContainer").val('').change();
    }
});
