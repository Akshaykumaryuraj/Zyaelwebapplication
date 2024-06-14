
$(document).ready(function () {
    var datatable = $('#OnlineAppointmentDetailsGrid').dataTable();
    datatable.fnDestroy();
    BindOnlineAppointmentDetailsGrid();
});

function BindOnlineAppointmentDetailsGrid(vendor) {
    
    var count = 1;
    var table = $("#OnlineAppointmentDetailsGrid").DataTable({
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
                var info = $('#OnlineAppointmentDetailsGrid').DataTable().page.info();
                var TemplateStr = 'pageNumber=' + (info.page + 1);

                if ($('#OnlineAppointmentDetailsGrid_length select').val() > 0) {
                    TemplateStr += '&pagesize=' + $('#OnlineAppointmentDetailsGrid_length select').val();
                } else {
                    TemplateStr += '&pageSize=100';
                }
                TemplateStr += '&vendor=' + vendor;
                $('#OnlineAppointmentDetailsGrid').DataTable().ajax.url(
                    '/Appointment/getOnlineAppointmentsGridDetails?' + TemplateStr
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
                "data": "userRandomID", "name": "userRandomID", orderable: false, "className": "",
                
            },
            {
                "data": "firstName", "name": "firstName", orderable: false, "className": "",

            },
            //{
            //    "data": "iDoctorID", "name": "iDoctorID", orderable: false, "className": "",

            //},

            //{
            //    "data": "userSelectedSlot", "name": "userSelectedSlot", orderable: false, "className": "",

            //},
            {
                "data": "userSelectedSlot", "name": "userSelectedSlot", orderable: false, "className": "",

            },
            {
                "data": "updates", "name": "updates", orderable: false, "className": "",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    Action += '<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#enableOTP">Check-in</button>';
                    //Action += '<label class="">active</label>';
                    return Action;
                }

            },
            {
                "data": "status", "name": "status", orderable: false, "className": "",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    Action += '<label class="">Online Paid</label>';
                    return Action;
                }

            },

            {
                "data": "status", sorting: false, orderable: false, "className": "table-actions",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    Action += '<a href="/InternalDoctor/InternalDoctorDetailsAdd?IDoctorID=' + row.iDoctorID + ' "><img src="/images/edit.png"/></a>';
                    Action += '<a href="#" onclick="ProfileHeadDetailsDelete(' + row.idOCTORid + ')"><img src="/images/delete.png"/></a>';
                    return Action;
                }

            }
        ]
    });
}
