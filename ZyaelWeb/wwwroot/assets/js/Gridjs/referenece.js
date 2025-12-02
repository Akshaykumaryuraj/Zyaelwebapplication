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
                "render": function (data, type, full, meta) {

                    var con = '<span>' + count + '</span>';
                    count = count + 1;
                    return con;
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

    var doctorID = $(this).attr('data-id'); // "4"
    console.log("DoctorID:", doctorID);

    $.ajax({
        url: '/DCAdmin/GetUserAppointmentByDoctorID',
        type: 'GET',
        data: { DoctorID: doctorID }, // pass as query string
        dataType: 'json',
        success: function (data) {
            var rows = data.map(a =>
                `<tr>
                    <td>${a.userRandomID}</td>
                    <td>${a.patientName}</td>
                    <td>${a.appointmentDate}</td>
                    <td>${a.userSelectedSlot}</td>
                    <td>${a.status}</td>
                </tr>`
            ).join('');

            $('#appointmentsTable tbody').html(rows);
            $('#appointmentsModal').modal('show');
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
            $('#appointmentsTable tbody').html(
                '<tr><td colspan="4" class="text-center text-danger">Failed to load appointments</td></tr>'
            );
        }
    });
});




