var dataProvinsiKota = function () {
    var self = this;
    var globalProvinsi;
    var globalKota;
  
    self.getProvinsi = function () {
        var ajaxTypesObj = {
            url: "/Api/location/GetProvinsi",
            method: "GET",
            dataType: "json",
            success: function (data) {
                globalProvinsi = data;
                self.populateProvinsi();
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

    self.populateProvinsi = function () {
        var row = "";
        $("#ddlprovinsi").empty();
        row += '<option value="">All</option>';
        $.each(globalProvinsi, function (i, v) {
            row += "<option value=" + v.value + ">" + v.text + "</option>";
        });
        $("#ddlprovinsi").html(row);
    };
}

var globalDataProvinsiKota = new dataProvinsiKota();

function getListKota(provinsiId) {
    $.ajax({
        url: '/api/location/getkota/' + provinsiId,
        data: { provinsiId: provinsiId },
        dataType: "json",
        type: "GET",
        error: function () {
            alert("An error ocurred.");
        },
        success: function (data) {
            var row = "";
            $("#ddlkota").empty();
            row += '<option value="">All</option>';
            $.each(data, function (i, v) {
                row += "<option value=" + v.value + ">" + v.text + "</option>";
            });
            $("#ddlkota").html(row);
        }
    });
}

$(document).ready(function () {
    globalDataProvinsiKota.getProvinsi();
    $("#ddlkota").empty();
    var row = '<option value="">All</option>';
    $("#ddlkota").html(row);
});

$(document).on("change", "#ddlprovinsi", function () {
    var provinsiId = this.value;
    if (provinsiId) {
        getListKota(provinsiId)
    }
    else
    {
        $("#ddlkota").empty();
        var row = '<option value="">All</option>';
        $("#ddlkota").html(row);
    }
});
