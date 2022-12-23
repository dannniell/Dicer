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

    self.getInitKota = function (initKota, initProvinsi) {
        var ajaxTypesObj = {
            url: '/Api/location/GetKota/' + initProvinsi,
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
        row += '<option value="">All</option>';
        $.each(globalKota, function (i, v) {
            if (initKota == v.value) {
                row += "<option value=" + v.value + " selected>" + v.text + "</option>";
            } else {
                row += "<option value=" + v.value + ">" + v.text + "</option>";
            }
        });
        $("#ddlkota").html(row);
    };

    self.getInitProvinsi = function (initProvinsi) {
        var ajaxTypesObj = {
            url: "/Api/location/GetProvinsi",
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
        row += '<option value="">All</option>';
        $.each(globalProvinsi, function (i, v) {
            if (initProvinsi == v.value) {
                row += "<option value=" + v.value + " selected>" + v.text + "</option>";
            } else {
                row += "<option value=" + v.value + ">" + v.text + "</option>";
            }
        });
        $("#ddlprovinsi").html(row);
    };
}

var _globalDataProvinsiKota = new dataProvinsiKota();

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
    document.getElementById("InitProvinsi").style.display = "none";
    var initProvinsi = document.getElementById("InitProvinsi").value;
    if (initProvinsi === null || initProvinsi === "") {
        _globalDataProvinsiKota.getProvinsi();
/*        $("#ddlkota").empty();
        var row = '<option value="">All</option>';
        $("#ddlkota").html(row);*/
    } else {
        _globalDataProvinsiKota.getInitProvinsi(initProvinsi);
    }
    document.getElementById("InitKota").style.display = "none";
    var initKota = document.getElementById("InitKota").value;
    if (initKota != null || initKota != "") {
        _globalDataProvinsiKota.getInitKota(initKota, initProvinsi);
    }
    
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
