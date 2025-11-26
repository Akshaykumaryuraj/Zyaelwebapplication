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
                    var checked = data ? "checked" : "";
                    return `
            <label class="switch">
                <input type="checkbox" class="isActiveCheckbox" ${checked} data-id="${full.doctorID}" />
                <span class="slider round"></span>
            </label>`;
                }
            },
           
            {
                "data": "goLive",
                "name": "goLive",
                orderable: false,
                "className": "",
                "render": function (data, type, full, meta) {
                    var checked = data ? "checked" : "";
                    return `
        <label class="switch">
            <input type="checkbox" class="statusCheckbox" ${checked} data-id="${full.doctorID}" />
            <span class="slider round"></span>
        </label>`;
                }
            },
           
            
        ]
    });
}


$('#DCDoctorActivationGrid').on('change', '.isActiveCheckbox', function () {
    var $toggle = $(this);
    var doctorId = $toggle.data('id');
    var isActive = $toggle.is(':checked');

    // Confirmation message based on action
    var message = isActive
        ? "Are you sure you want to ENABLE this doctor?"
        : "Are you sure you want to DISABLE this doctor?";

    if (!confirm(message)) {
        // User cancelled → revert toggle back
        $toggle.prop('checked', !isActive);
        return;
    }

    $.ajax({
        type: "POST",
        url: "/DCAdmin/SetDoctorActiveStatus",
        data: { DoctorID: doctorId, isActive: isActive },
        dataType: "json",
        success: function (response) {
            if (response > 0) {
                $('#DCDoctorActivationGrid').DataTable().ajax.reload(null, false);
            }
        },
        error: function () {
            // Rollback toggle if update fails
            $toggle.prop('checked', !isActive);
            alert("Failed to update status");
        }
    });
});

// Attach event after DataTable initialization

$('#DCDoctorActivationGrid').on('change', '.statusCheckbox', function () {
    var $toggle = $(this);
    var doctorId = $toggle.data('id');
    var goLive = $toggle.is(':checked');

    var message = goLive
        ? "Are you sure you want to set this doctor ONLINE?"
        : "Are you sure you want to set this doctor OFFLINE?";

    if (!confirm(message)) {
        $toggle.prop('checked', !goLive);
        return;
    }

    $.ajax({
        type: "POST",
        url: "/DCAdmin/SetDoctorGoLiveStatus",
        data: { DoctorID: doctorId, goLive: goLive },
        dataType: "json",
        success: function (response) {
            if (response > 0) {
                $('#DCDoctorActivationGrid').DataTable().ajax.reload(null, false);
            }
        },
        error: function () {
            $toggle.prop('checked', !goLive);
            alert("Failed to update status");
        }
    });
});






