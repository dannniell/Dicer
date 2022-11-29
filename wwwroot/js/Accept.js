var participantTable;
var initCampaignId;

var Acceptance = function () {
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
        participantTable = $('#participantTable').DataTable({
            data: data,
            order: [[1, 'asc'], [2, 'asc']],
            scrollX: true,
            scrollY: true,
            scrollCollapse: true,
            //dom: "<'#CustomHeaderDataTable'<'d-flex justify-content-between align-items-center'<'app-table-title'><'app-table-search d-flex'Bf>><'card-body p-0'tr>><'app-table-pagination d-flex justify-content-between p-1 'ilp>", // remove page length, filte
            buttons: [
                {
                    text: '<table><tr><td><i class="ms-Icon ms-Icon--CalculatorAddition"></i></td><td class="buttonIcon">&nbsp;New</td></tr></table>',
                    className: 'btn-sm btn-primary new-data-AdditionalLink',
                    title: '',
                    action: function (e, dt, node, config) {
                        //self.resetFormAdditionalLink();
                        //$("#modalAdditionalLink").modal("show");
                    }
                },
                {
                    text: '<table><tr><td><i class="ms-Icon ms-Icon--Refresh"></i></td><td class="buttonIcon">&nbsp;Refresh</td></tr></table>',
                    title: '',
                    className: 'btn-sm btn-primary new-data-AdditionalLink',
                    title: 'Refresh Data',
                    action: function (e, dt, node, config) {
                        // self.AdditionalLinkTable.ajax.reload();
                    }
                }
            ],
            language: {
                'search': '',
                'searchPlaceholder': 'Quick Filter',
                'zeroRecords': 'No data available.'
            },
            columns: [
                { data: 'userId', className: 'f-userId', title: 'User Id', visible: false },//0
                { data: 'name', className: 'f-name', title: 'Name' },//1,
                { data: 'gender', className: 'f-gender', title: 'Gender' },//2
                { data: 'age', className: 'f-age', title: 'Age' },//3
                { data: 'kota', className: 'f-kota', title: 'Kota' },//4
                { data: 'provinsi', className: 'f-provinsi', title: 'Provinsi' },//5
                { data: 'followers', className: 'f-followers', title: 'Followers' },//6
                { data: 'er', className: 'f-er', title: 'Engagement Rate' },//7
                { data: 'linkInstagram', className: 'f-linkInstagram', title: 'Instagram Url' },//8
                {
                    data: 'isAccepted', className: 'f-isAccepted text-center', title: 'Is Accepted', visible: false , render: function (data, type, row, meta) {
                        if (data) {
                            return '<center><input type="checkbox" class="form-check-input" id="isActiveChk_' + meta.row + '" checked disabled/></center>';
                        }
                        else {
                            return '<center><input type="checkbox" class="form-check-input" id="isActiveChk_' + meta.row + '" disabled/></center>';
                        }
                    }
                },//9
            ],
            orderCellsTop: true,
            initComplete: function (settings, json) {
                participantTable.draw();
                $('div.dataTables_filter input').addClass('customQuickFilter');
                $('.dt-buttons').removeClass('btn-group').removeClass('dt-buttons');
                $('.btn-secondary').removeClass('btn-secondary');
                participantTable.columns.adjust().draw();
            }
        });
    }
};

var _globalAcceptance = new Acceptance();

$(document).ready(function () {
    initCampaignId = parseInt(document.getElementById("initCampaignId").value);
    _globalAcceptance.PopulateTable();
});


