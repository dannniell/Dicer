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
        $.each(globalProvinsi, function (i, v) {
            row += "<option value=" + v.value + ">" + v.text + "</option>";
        });
        $("#ddlprovinsi").html(row);
    };

    self.getDefaultKota = function () {
        var ajaxTypesObj = {
            url: "/Api/location/GetKota/11",
            method: "GET",
            dataType: "json",
            success: function (data) {
                globalKota = data;
                self.populateKota();
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

    self.populateKota = function () {
        var row = "";
        $("#ddlkota").empty();
        $.each(globalKota, function (i, v) {
            row += "<option value=" + v.value + ">" + v.text + "</option>";
        });
        $("#ddlkota").html(row);
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
            $.each(data, function (i, v) {
                row += "<option value=" + v.value + ">" + v.text + "</option>";
            });
            $("#ddlkota").html(row);
        }
    });
}

window.onload = function () {
    globalDataProvinsiKota.getProvinsi();
    globalDataProvinsiKota.getDefaultKota();
}
/*$(document).ready(function () {
    globalDataProvinsiKota.getProvinsi();
    globalDataProvinsiKota.getDefaultKota();
});*/

$(document).on("change", "#ddlprovinsi", function () {
    var provinsiId = this.value;
    getListKota(provinsiId)
});
