var count = 1;
$(document).ready(function () {
    var datatable = $('#DCDoctorActivationGrid').dataTable();
    datatable.fnDestroy();
    BindDCDoctorActivationGrid();
});

function BindDCDoctorActivationGrid() {
    debugger

    var table = $("#DCDoctorActivationGrid").DataTable({
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

                var info = $('#DCDoctorActivationGrid').DataTable().page.info();
                var TemplateStr = 'pageNumber=' + (info.page + 1);

                if ($('#DCDoctorActivationGrid_length select').val() > 0) {
                    TemplateStr += '&pagesize=' + $('#DCDoctorActivationGrid_length select').val();
                } else {
                    TemplateStr += '&pageSize=100';
                }

                $('#DCDoctorActivationGrid').DataTable().ajax.url(
                    '/DCAdmin/getDCDoctorActiveDetails?' + TemplateStr
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
                "data": "phoneNumber", "name": "phoneNumber", orderable: false, "className": "",

            }, 
  
            {
                "data": "isActive",
                "name": "isActive",
                orderable: false,
                "className": "",
                "render": function (data, type, full, meta) {
                    // data = value of isActive from DB (true/false or 1/0)
                    var checked = data ? "checked" : "";
                    return '<input type="checkbox" class="isActiveCheckbox" '
                        + checked
                        + ' data-id="' + full.doctorID + '" '
                        + 'onclick="setStatus(' + full.doctorID + ')" />';
                }
            },
            //{
            //    "data": "status", "name": "status", orderable: false, "className": "",

            //},
            {
                "data": "status",
                "name": "status",
                orderable: false,
                "className": "",
                "render": function (data, type, full, meta) {
                    // data = value of isActive from DB (true/false or 1/0)
                    var checked = data ? "checked" : "";
                    return '<input type="checkbox" class="statusCheckbox" '
                        + checked
                        + ' data-id="' + full.doctorID + '" '
                        + 'onclick="setStatus(' + full.doctorID + ')" />';
                }
            },
           
            
        ]
    });
}


// Attach event after DataTable initialization
$('#DCDoctorActivationGrid').on('change', '.isActiveCheckbox', function () {
    var doctorId = $(this).data('id');
    var isActive = $(this).is(':checked');

    $.ajax({
        type: "POST",
        url: "/DCAdmin/SetDoctorActiveStatus",
        data: { DoctorID: doctorId, isActive: isActive },
        dataType: "json",
        success: function (response) {
            if (response > 0) {
                // reload table or redirect if needed
                $('#DCDoctorActivationGrid').DataTable().ajax.reload();
            }
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
            alert("Failed to update status");
        }
    });
});

// Attach event after DataTable initialization
$('#DCDoctorActivationGrid').on('change', '.statusCheckbox', function () {
    var doctorId = $(this).data('id');
    var status = $(this).is(':checked');

    $.ajax({
        type: "POST",
        url: "/DCAdmin/SetDoctorOnlineStatus",
        data: { DoctorID: doctorId, status: status },
        dataType: "json",
        success: function (response) {
            if (response > 0) {
                // reload table or redirect if needed
                $('#DCDoctorActivationGrid').DataTable().ajax.reload();
            }
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
            alert("Failed to update status");
        }
    });
});






