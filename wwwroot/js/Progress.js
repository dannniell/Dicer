var participantTableData = null;
var initCampaignId;
var _globalSelectedItem = [];

var Acceptance = function () {
    var self = this;

    self.PopulateTable = function () {
        var ajaxTypesObj = {
            url: '/Api/Progress/' + initCampaignId,
            method: "GET",
            dataType: "json",
            success: function (data) {
                self.ParticipantTable(data);
            },
            error: function (e) {
                alert('Failed to Insert Transaction Detail Data!');
            },
        };
        $.ajax(ajaxTypesObj);
    }

    self.ParticipantTable = function (data) {
        participantTableData = $('#participantTable').DataTable({
            data: data,
            scrollY: true,
            order: [1, 'asc'],
            dom: "<'#CustomHeaderDataTable'<'d-flex justify-content-between align-items-center'<'app-table-title'><'app-table-search d-flex'Bf>><'card-body p-0'tr>><'app-table-pagination d-flex justify-content-between p-1 'ilp>",
            language: {
                'search': '',
                'searchPlaceholder': 'Quick Filter',
                'zeroRecords': 'No data available.'
            },
            columns: [
                { data: 'userId', className: 'f-userId', title: 'User Id', visible: false },//0
                { data: 'name', className: 'f-name', title: 'Name' },//1,
                { data: 'followers', className: 'f-followers', title: 'Followers' },//2
                {
                    data: 'er', className: 'f-er', title: 'Engagement Rate',
                    render: function (data, type, row, meta) {
                        return data + ' %';
                    }
                },//3
                {
                    data: 'linkInstagram', className: 'f-linkInstagram', title: 'Instagram',
                    render: function (data, type, row, meta) {
                        return '<center><a id="linkInstagram' + meta.row + '" href="' + data + '" target="_blank">link</a></center>';
                    }
                },//4
                {
                    data: 'finalDraft', className: 'f-finalDraft', title: 'Final Draft',
                    render: function (data, type, row, meta) {
                        if (data === null || data === "") {
                            return 'No Data';
                        } else {
                            return '<center><a href="/Campaign/DownloadDraft/' + data + '">download</a></center>';
                        }
                    }
                },//5
                {
                    data: 'postLink', className: 'f-postLink', title: 'Post',
                    render: function (data, type, row, meta) {
                        if (data === null || data === "") {
                            return 'No Data';
                        } else {
                            return '<center><a id="postLink' + meta.row + '" href="' + data + '" target="_blank">post link</a></center>';
                        }
                    }
                },//6
                {
                    data: 'insight', className: 'f-insight', title: 'Insight',
                    render: function (data, type, row, meta) {
                        if (data === null || data === "") {
                            return 'No Data';
                        } else {
                            return '<center><a href="/Campaign/DownloadInsight/' + data + '">download</a></center>';
                        }
                    }
                },//7
                {
                    data: null, className: 'f-chat', title: 'Chat',
                    render: function (data, type, row, meta) {
                        return '<center><a href="#" id="chat'+ meta.row +'" class="chatBtn" >open chat</a></center>';
                    }
                },//7
                {
                    data: 'isTaskDone', className: 'f-isAccepted text-center', title: 'Task Done', visible: true , render: function (data, type, row, meta) {
                        if (data) {
                            return '<center><input type="checkbox" class="form-check-input" id="isActiveChk_' + meta.row + '" checked disabled/></center>';
                        }
                        else {
                            return '<center><input type="checkbox" class="form-check-input" id="isActiveChk_' + meta.row + '" disabled/></center>';
                        }
                    }
                },//8
            ],
            orderCellsTop: true,
            initComplete: function (settings, json) {
            }
        });
    }

    self.AcceptParticipant = function () {
        var listData = {};
        listData['users'] = _globalSelectedItem;

        var ajaxTypesObj = {
            url: '/Api/Acceptance/' + initCampaignId,
            method: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(listData),
            success: function (data) {
                alert("Payment Success!!");
                window.location.href = '/Campaign/Detail?id=' + initCampaignId;
            },
            error: function (e) {
                alert('Failed to Insert Transaction Detail Data!');
            },
        };
        $.ajax(ajaxTypesObj);
    };
};

var _globalAcceptance = new Acceptance();

$(document).ready(function () {
    initCampaignId = parseInt(document.getElementById("initCampaignId").value);
    _globalAcceptance.PopulateTable();
});


$('#submitForm').on('click', function (e) {
    _globalAcceptance.AcceptParticipant();
});

$('#participantTable').on("click", "tbody .chatBtn", function () {
    var data = participantTableData.row($(this).attr('row-id')).data();
    var link = "/Chat?campaignId=" + initCampaignId + "&&userId=" + data.userId;
    window.open(link, '_blank');
});

$('#participantTable').on("click", "tbody .select-checkbox", function () {
    var data = participantTableData.row($(this).attr('row-id')).data();

    if ($(this).prop('checked')) {
        _globalSelectedItem.push({
            userId: data.userId
        });
    }
    else {
        for (var i = 0; i < _globalSelectedItem.length; i++) {
            if (_globalSelectedItem[i].userId == data.userId) {
                _globalSelectedItem.splice(i, 1);
            }
        }
    }

    var count = _globalSelectedItem.length;
    document.getElementById("TotalCreator").innerHTML = count;

    var initCommision = document.getElementById("initCommision").value;
    var total = (initCommision * count) + 3000;
    document.getElementById("totalPrice").innerHTML = 'Rp ' + String(total).replace(/(.)(?=(\d{3})+$)/g, '$1,');
});

