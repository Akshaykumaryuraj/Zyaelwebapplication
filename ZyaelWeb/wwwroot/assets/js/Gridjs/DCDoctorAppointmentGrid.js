var count = 1;
$(document).ready(function () {
    var datatable = $('#DCDoctorAppointmentGrid').dataTable();
    datatable.fnDestroy();
    BindDCDoctorAppointmentGrid();
});

function BindDCDoctorAppointmentGrid() {

    var table = $("#DCDoctorAppointmentGrid").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "searchable": true,
        "stateSave": true,
        "order": [[1, "asc"]],
        "ajax": {

            type: "Post",
            datatype: "json",

            data: function () {

                var info = $('#DCDoctorAppointmentGrid').DataTable().page.info();
                var TemplateStr = 'pageNumber=' + (info.page + 1);

                if ($('#DCDoctorAppointmentGrid_length select').val() > 0) {
                    TemplateStr += '&pagesize=' + $('#DCDoctorAppointmentGrid_length select').val();
                } else {
                    TemplateStr += '&pageSize=100';
                }

                $('#DCDoctorAppointmentGrid').DataTable().ajax.url(
                    '/DCAdmin/getDCDoctorsDetails?' + TemplateStr
                );
            },
            "dataSrc": function (json) {
                return json.data;
            }
        },

        "columns": [

            {
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },

            {
                "data": "doctorFirstName",
                "name": "doctorFirstName",
                orderable: false,
                "className": "",
                "render": function (data, type, row) {
                    return '<a href="#" class="doctorNameLink" data-id="' + row.doctorID + '">' + data + '</a>';
                }
            },
            {
                "data": "phoneNumber", "name": "phoneNumber", orderable: false, "className": "",

            }
 
            //{
            //    "data": "status", sorting: false, orderable: false, "className": "table-actions", "render": function (data, type, row, meta) {
            //        var Action = '';
            //        Action += '<a class="viewDoctorBtn me-2" data-id="' + row.doctorID + '"><img src="/images/eye.png"/></a> '; // Existing edit button 
            //        Action += '<a href="/DCAdmin/DCDProfileDetailsAdd?DoctorPId=' + row.doctorID + '"><img src="/images/edit.png"/></a>';
            //        return Action;
            //    }


            //}

        ]
    });
}


$('#DCDoctorAppointmentGrid').on('click', '.doctorNameLink', function (e) {
    e.preventDefault();

    var doctorID = $(this).attr('data-id');
    $('#appointmentsModal').data('doctor-id', doctorID).modal('show');

    function loadAppointments() {
        var status = $('#statusFilter').val();
        var date = $('#dateFilter').val();
        var search = $('#searchFilter').val();

        // Build filter badges
        var badges = [];
        if (status) badges.push(`<span class="badge bg-info me-1">Status: ${status}</span>`);
        if (date) badges.push(`<span class="badge bg-warning text-dark me-1">Date: ${date}</span>`);
        if (search) badges.push(`<span class="badge bg-success me-1">Search: ${search}</span>`);

        $('#activeFilters').html(badges.join(' ') || '<span class="text-muted">No filters applied</span>');

        // AJAX call
        $.ajax({
            url: '/DCAdmin/GetUserAppointmentByDoctorID',
            type: 'GET',
            data: { DoctorID: doctorID, Status: status, AppointmentDate: date, SearchText: search },
            dataType: 'json',
            success: function (data) {
                var rows = data.map(a =>
                    `<tr>
                    <td>${a.userRandomID ?? ''}</td>
                    <td>${a.patientName ?? ''}</td>
                    <td>${a.appointmentDate ?? ''}</td>
                    <td>${a.userSelectedSlot ?? ''}</td>
                    <td>${a.status ?? ''}</td>
                </tr>`
                ).join('');
                $('#appointmentsTable tbody').html(rows || '<tr><td colspan="5" class="text-center text-muted">No appointments found</td></tr>');
            },
            error: function () {
                $('#appointmentsTable tbody').html('<tr><td colspan="5" class="text-center text-danger">Failed to load appointments</td></tr>');
            }
        });
    }

    // initial load
    loadAppointments();

    // filters
    $('#statusFilter').off('change').on('change', loadAppointments);
    flatpickr("#dateFilter", { dateFormat: "Y-m-d", onChange: loadAppointments });
    $('#searchFilter').off('input').on('input', function () {
        var val = $(this).val();
        if (val.length === 0 || val.length >= 3) {
            loadAppointments();
        }
    });

    // reset button
    $('#resetFilters').off('click').on('click', function () {
        $('#statusFilter').val('');
        $('#dateFilter').val('');
        $('#searchFilter').val('');
        $('#activeFilters').html('<span class="text-muted">No filters applied</span>');
        loadAppointments();
    });
});


//$('#DCDoctorAppointmentGrid').on('click', '.doctorNameLink', function (e) {
//    e.preventDefault();

//    var doctorID = $(this).attr('data-id');
//    $('#appointmentsModal').data('doctor-id', doctorID).modal('show');

//    function loadAppointments() {
//        var status = $('#statusFilter').val();
//        var date = $('#dateFilter').val();
//        var search = $('#searchFilter').val();

//        $.ajax({
//            url: '/DCAdmin/GetUserAppointmentByDoctorID',
//            type: 'GET',
//            data: { DoctorID: doctorID, Status: status, AppointmentDate: date, SearchText: search },
//            success: function (data) {
//                var rows = data.map(a =>
//                    `<tr>
//                        <td>${a.userRandomID ?? ''}</td>
//                        <td>${a.patientName ?? ''}</td>
//                        <td>${a.appointmentDate ?? ''}</td>
//                        <td>${a.userSelectedSlot ?? ''}</td>
//                        <td>${a.status ?? ''}</td>
//                    </tr>`
//                ).join('');
//                $('#appointmentsTable tbody').html(rows || '<tr><td colspan="5" class="text-center text-muted">No appointments found</td></tr>');
//            }
//        });
//    }

//    // initial load
//    loadAppointments();

//    // filters
//    $('#statusFilter').off('change').on('change', loadAppointments);
//    flatpickr("#dateFilter", { dateFormat: "Y-m-d", onChange: loadAppointments });

//    // search
//    $('#searchFilter').off('input').on('input', function () {
//        var val = $(this).val();
//        if (val.length === 0 || val.length >= 3) {
//            loadAppointments();
//        }
//    });
//});

