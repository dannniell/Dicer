$(document).ready(function () {
    var InitProvinsi = document.getElementById("InitProvinsi").value;
    var InitKota = document.getElementById("InitKota").value;
    var tag = document.getElementById("InitTypeTag").value;

    if (tag == "IG Feed")
    {
        document.getElementById("typeTag").style.backgroundColor = "#EFA46B";
    }
    else if (tag == "IG Story") 
    {
        document.getElementById("typeTag").style.backgroundColor = "#5cd175";
    }
    else if (tag == "IG Reels")
    {
        document.getElementById("typeTag").style.backgroundColor = "#e37d7d";
    }
    $("#provinsi").val(InitProvinsi);
    $("#kota").val(InitKota);
});

$('#campaignCompleted').on('click', function () {
    var initCampaignId = parseInt(document.getElementById("InitCampaignId").value);
    var ajaxTypesObj = {
        url: '/Api/Acceptance/' + initCampaignId + '/Completed',
        method: "POST",
        dataType: "json",
        success: function (data) {
            window.location.href = '/Mycampaign/Done';
        },
        error: function (e) {
            alert('Failed to Complete Campaign!');
        },
    };
    $.ajax(ajaxTypesObj);
});