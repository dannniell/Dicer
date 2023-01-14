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
            destroy: true,
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
                    data: 'linkInstagram', className: 'f-linkInstagram', title: 'Instagram', orderable: false,
                    render: function (data, type, row, meta) {
                        return '<center><a id="linkInstagram' + meta.row + '" href="' + data + '" target="_blank"><span class="fas fa-external-link-alt"></span></a></center>';
                    }
                },//4
                {
                    data: 'finalDraft', className: 'f-finalDraft', title: 'Final Draft', orderable: false,
                    render: function (data, type, row, meta) {
                        if (data === null || data === "") {
                            return 'No Data';
                        } else {
                            return '<center><a href="/Campaign/DownloadDraft/' + data + '"><span class="fas fa-download"></span></a></center>';
                        }
                    }
                },//5
                {
                    data: 'postLink', className: 'f-postLink', title: 'Post', orderable: false,
                    render: function (data, type, row, meta) {
                        if (data === null || data === "") {
                            return 'No Data';
                        } else {
                            return '<center><a id="postLink' + meta.row + '" href="' + data + '" target="_blank"><span class="fas fa-external-link-alt"></span></a></center>';
                        }
                    }
                },//6
                {
                    data: 'insight', className: 'f-insight', title: 'Insight', orderable: false,
                    render: function (data, type, row, meta) {
                        if (data === null || data === "") {
                            return 'No Data';
                        } else {
                            return '<center><a href="/Campaign/DownloadInsight/' + data + '"><span class="fas fa-download"></span></a></center>';
                        }
                    }
                },//7
                {
                    data: null, className: 'f-chat', title: 'Chat', orderable: false,
                    render: function (data, type, row, meta) {
                        return '<center><a href="#" id="chat' + meta.row +'" class="chatBtn" ><span class="fas fa-comments"></span></a></center>';
                    }
                },//8
                {
                    data: 'isTaskDone', className: 'f-isAccepted text-center', title: 'Task Done', visible: true , render: function (data, type, row, meta) {
                        if (data) {
                            return '<center><span class="fas fa-check"></span></center>';
                        }
                        else {
                            return '<center><button type="button" class="btn btn-primary btn-sm"> Done </button></center>';
                        }
                    }
                },//9
            ],
            orderCellsTop: true,
            initComplete: function (settings, json) {
            }
        });
    }

    self.TaskDoneParticipant = function (data) {
        var ajaxTypesObj = {
            url: '/Api/Progress/' + initCampaignId + '/TaskDone/' + data.userId,
            method: "POST",
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                self.PopulateTable();
            },
            error: function (e) {
                alert('Failed to Insert Transaction Detail Data!');
            },
        };
        $.ajax(ajaxTypesObj);
    };

    self.ResetDoneModal = function () {
        $("#withdrawl").addClass('d-none');
        $("#withoutWithdrawl").addClass('d-none');
    }

    self.CheckWithdrawl = function () {
        var ajaxTypesObj = {
            url: '/Api/Progress/WithdrawlCheck/' + initCampaignId,
            method: "GET",
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                if (data[0].total > 0) {
                    $("#withdrawl").removeClass('d-none');
                    document.getElementById("accountBalance").innerHTML = 'Not Used Balance: Rp. ' + String(data[0].total).replace(/(.)(?=(\d{3})+$)/g, '$1,');
                } else {
                    $("#withoutWithdrawl").removeClass('d-none');
                }
            },
            error: function (e) {
                alert('Failed to Insert Transaction Detail Data!');
            },
        };
        $.ajax(ajaxTypesObj);
    }

    self.CloseCampaign = function () {
        var ajaxTypesObj = {
            url: '/Api/Progress/' + initCampaignId + '/Completed',
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
    }
};

var _globalAcceptance = new Acceptance();

$(document).ready(function () {
    initCampaignId = parseInt(document.getElementById("initCampaignId").value);
    _globalAcceptance.PopulateTable();
});


$('#submitButton').on('click', function (e) {
    var bankAccount = document.getElementById("bankAccountNumber").value;
    if (bankAccount !== "") {
        _globalAcceptance.CloseCampaign();
    } else {
        alert("Please Enter Valid Bank Account Number!!");
    }
});

$('#btnDone').on('click', function (e) {
    _globalAcceptance.ResetDoneModal();
    _globalAcceptance.CheckWithdrawl();
});

$('#participantTable').on("click", "tbody .chatBtn", function () {
    var row = $(this).parents("tr");
    var data = participantTableData.row(row).data();
    var link = "/Chat?campaignId=" + initCampaignId + "&&userId=" + data.userId;
    window.open(link, '_blank');
});

$('#participantTable').on("click", "tbody .btn", function () {
    var row = $(this).parents("tr");
    var data = participantTableData.row(row).data();
    let text = "Are You Sure?";
    if (confirm(text) == true) {
        _globalAcceptance.TaskDoneParticipant(data);
    } else {
        return null;
    }
});
