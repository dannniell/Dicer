var dataProvinsiKota = function () {
    var self = this;
    var globalProvinsi;
    var globalKota;
  
    self.getInitProvinsi = function (initProvinsi) {
        var ajaxTypesObj = {
            url: "/Api/GetProvinsi",
            method: "GET",
            dataType: "json",
            success: function (data) {
                globalProvinsi = data;
                self.populateInitProvinsi(initProvinsi);
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

    self.populateInitProvinsi = function (initProvinsi) {
        var row = "";
        $("#ddlprovinsi").empty();
        $.each(globalProvinsi, function (i, v) {
            if (initProvinsi == v.value) {
                row += "<option value=" + v.value + " selected>" + v.text + "</option>";
            } else {
                row += "<option value=" + v.value + ">" + v.text + "</option>";
            }
        });
        $("#ddlprovinsi").html(row);
    };

    self.getInitKota = function (initKota, initProvinsi) {
        var ajaxTypesObj = {
            url: '/Api/GetKota/' + initProvinsi,
            data: { provinsiId: initProvinsi },
            type: "GET",
            dataType: "json",
            success: function (data) {
                globalKota = data;
                self.populateInitKota(initKota);
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

    self.populateInitKota = function (initKota) {
        var row = "";
        $("#ddlkota").empty();
        $.each(globalKota, function (i, v) {
            if (initKota == v.value) {
                row += "<option value=" + v.value + " selected>" + v.text + "</option>";
            } else {
                row += "<option value=" + v.value + ">" + v.text + "</option>";
            }
        });
        $("#ddlkota").html(row);
    };
}

var globalDataProvinsiKota = new dataProvinsiKota();

function getListKota(provinsiId) {
    $.ajax({
        url: '/api/getkota/' + provinsiId,
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

$(document).ready(function () {
    document.getElementById("InitProvinsi").style.display = "none";
    var initProvinsi = document.getElementById("InitProvinsi").value;
    globalDataProvinsiKota.getInitProvinsi(initProvinsi);

    document.getElementById("InitKota").style.display = "none";
    var initKota = document.getElementById("InitKota").value;
    globalDataProvinsiKota.getInitKota(initKota, initProvinsi);
});

$(document).on("change", "#ddlprovinsi", function () {
    var provinsiId = this.value;
    getListKota(provinsiId)
});
