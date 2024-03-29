﻿var participantTableData = null;
var initCampaignId;
var _globalSelectedItem = [];

var Progress = function () {
    var self = this;

    self.PopulateTable = function () {
        var ajaxTypesObj = {
            url: '/Api/Acceptance/' + initCampaignId,
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
                {
                    data: null,
                    className: 'text-center',
                    title: 'Check',
                    orderable: false,
                    render: function (data, type, row, meta) {
                        return '<input type="checkbox" class="select-checkbox" row-id="' + meta.row + '" >'
                    }
                },
                { data: 'userId', className: 'f-userId', title: 'User Id', visible: false },//0
                { data: 'name', className: 'f-name', title: 'Name' },//1,
                { data: 'gender', className: 'f-gender', title: 'Gender' },//2
                { data: 'age', className: 'f-age', title: 'Age' },//3
                {
                    data: 'kota', className: 'f-kotaProvinsi', title: 'Address',
                    render: function (data, type, row) {
                        return data + ', ' + row['provinsi'];
                    },
                },//4
                { data: 'followers', className: 'f-followers', title: 'Followers' },//5
                {
                    data: 'er', className: 'f-er', title: 'Engagement Rate',
                    render: function (data, type, row, meta) {
                        return data + ' %';
                    }
                },//6
                {
                    data: 'linkInstagram', className: 'f-linkInstagram', title: 'Instagram',
                    render: function (data, type, row, meta) {
                        return '<center><a id="linkInstagram' + meta.row + '" href="' + data + '" target="_blank"><span class="fas fa-external-link-alt"></span></a></center>';
                    }
                },//7
                {
                    data: 'isAccepted', className: 'f-isAccepted text-center', title: 'Is Accepted', visible: false , render: function (data, type, row, meta) {
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

var _globalAcceptance = new Progress();

$(document).ready(function () {
    initCampaignId = parseInt(document.getElementById("initCampaignId").value);
    _globalAcceptance.PopulateTable();
});

$('#submitForm').on('click', function (e) {
    if (_globalSelectedItem.length > 0) {
        _globalAcceptance.AcceptParticipant();
    } else {
        alert('Please Select Participant!');
    }
});


$('#participantTable').on("click", "tbody .select-checkbox", function () {
    var row = $(this).parents("tr");
    var data = participantTableData.row(row).data();

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

