
$(document).ready(function () {
    debugger
    var datatable = $('#SpecializationsGrid').dataTable();
    datatable.fnDestroy();
    BindSpecializationsGrid();
});

function BindSpecializationsGrid(vendor) {
    debugger
    var count = 1;
    var table = $("#SpecializationsGrid").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "searchable": true,
        "stateSave": true,
        "bDestroy": true,
        "order": [[1, "asc"]],
        "ajax": {
            type: "Post",
            datatype: "json",
            data: function () {
                debugger
                var info = $('#SpecializationsGrid').DataTable().page.info();
                var TemplateStr = 'pageNumber=' + (info.page + 1);

                if ($('#SpecializationsGrid_length select').val() > 0) {
                    TemplateStr += '&pagesize=' + $('#SpecializationsGrid_length select').val();
                } else {
                    TemplateStr += '&pageSize=100';
                }
                TemplateStr += '&vendor=' + vendor;
                $('#SpecializationsGrid').DataTable().ajax.url(
                    '/Admin/getSpecializationsDetails?' + TemplateStr
                );
            },
            "dataSrc": function (json) {
                debugger
                return json.data;
            }
        },

        "columns": [
            {


                "render": function (data, type, row, meta) {
                    
                    var con = '<span>' + count + '</span>';
                    count = count + 1;
                    return con;
                }
            },

            {
                "data": "specialityName", "name": "specialityName", orderable: false, "className": "",
                
            },

            {
                "data": "specialityCode", "name": "specialityCode", orderable: false, "className": "",

            },

            {
                "data": "status", "name": "status", orderable: false, "className": "",
                "render": function (data, type, row, meta) {
                    var status = '';
                    status += '<label class="switch">';
                    status += '<input type="checkbox" ' + (row.status == "True" ? "checked" : "") + ' id="rowstatus' + row.specialityID + '"  onclick="setStatus(' + row.specialityID + ');">';
                    //status += '<span class="slider round"></span>';
                    status += '</label>';


                    return status;
                }
            },
           
            {
                "data": "status", sorting: false, orderable: false, "className": "table-actions",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    Action += '<a href="/Admin/SpecialitiesDetailsAdd?SpecialityID=' + row.specialityID + ' "><img src="/images/edit.png"/></a>';
                    Action += '<a href="#" onclick="ProfileHeadDetailsDelete(' + row.specialityID + ')"><img src="/images/delete.png"/></a>';
                    return Action;
                }

            }
        ]
    });
}
function setStatus(specialityID) {
    debugger
    var status = $('#rowstatus' + specialityID).is(':checked');
    //var status = $('#checkstatus').prop('checked');
    var form_data = new FormData();
    form_data.append("status", status);
    form_data.append("SpecialityID", specialityID);
    $.ajax({
        type: "POST",
        url: "/Admin/SetSpecilizationStatus",
        dataType: "JSON",
        data: form_data,
        cache: false,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response > 0) {
                window.location.href = 'Admin/SpecializationsGrid'
            }
        }
    });
}
