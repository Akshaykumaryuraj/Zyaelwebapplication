var count = 1;
$(document).ready(function () {
    var datatable = $('#DCDoctorProfileGrid').dataTable();
    datatable.fnDestroy();
    BindDCDoctorProfileGrid();
});

function BindDCDoctorProfileGrid() {

    var table = $("#DCDoctorProfileGrid").DataTable({
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

                var info = $('#DCDoctorProfileGrid').DataTable().page.info();
                var TemplateStr = 'pageNumber=' + (info.page + 1);

                if ($('#DCDoctorProfileGrid_length select').val() > 0) {
                    TemplateStr += '&pagesize=' + $('#DCDoctorProfileGrid_length select').val();
                } else {
                    TemplateStr += '&pageSize=100';
                }

                $('#DCDoctorProfileGrid').DataTable().ajax.url(
                    '/DCAdmin/getDCDoctorProfileDetails?' + TemplateStr
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
                "data": "consultationCategory", "name": "consultationCategory", orderable: false, "className": "",

            },
            {
                "data": "dcdotp", "name": "dcdotp", orderable: false, "className": "",

            },
            
            //{
            //    "data": "status", "name": "status", orderable: false, "className": "",
            //    "render": function (data, type, row, meta) {
            //        var status = '';
            //        status += '<label class="switch">';
            //        status += '<input type="checkbox" ' + (row.status == true ? "checked" : "") + ' id="rowstatus' + row.doctorPId + '"  onclick="setStatus(' + row.doctorPId + ');">';
            //        //status += '<span class="slider round"></span>';
            //        status += '</label>';


            //        return status;
            //    }
            //},
            {
                "data": "status",
                "name": "status",
                orderable: false,
                "className": "",
                "render": function (data, type, full, meta) {
                    var checked = data ? "checked" : "";
                    return `
        <label class="switch">
            <input type="checkbox" class="profilestatusCheckbox" ${checked} data-id="${full.doctorPId}" />
            <span class="slider round"></span>
        </label>`;
                }
            },
            
            {
                "data": "status", sorting: false, orderable: false, "className": "table-actions",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    Action += '<a href="/DCAdmin/DCDProfileDetailsAdd?DoctorPId=' + row.doctorPId + ' "><img src="/images/edit.png"/></a>';
                    //Action += '<a href="/DCAdmin/DCDProfileDetailsAdd?DoctorID=' + row.doctorID + ' "><img src="/images/edit.png"/></a>';
                    //Action += '<a href="#" onclick="CampusCredentialDetailsDelete(' + row.specialityID + ')"><img src="/images/delete.png"/></a>';
                    return Action;
                }

            }
        ]
    });
}


$('#DCDoctorProfileGrid').on('change', '.profilestatusCheckbox', function () {
    var $toggle = $(this);
    var doctorPId = $toggle.data('id');
    var status = $toggle.is(':checked');

    var message = status
        ? "Are you sure you want to set this doctor Profile Online?"
        : "Are you sure you want to set this doctor Profile Offline?";

    if (!confirm(message)) {
        $toggle.prop('checked', !status);
        return;
    }

    $.ajax({
        type: "POST",
        url: "/DCAdmin/SetDoctorProfileStatus",
        data: { DoctorPId: doctorPId, status: status },
        dataType: "json",
        success: function (response) {
            if (response > 0) {
                alert("Profile status updated successfully.");
            } else {
                alert("Doctor is inactive. Profile status not updated.");
                // rollback toggle if inactive
                $toggle.prop('checked', !status);
            }

            // ✅ Always refresh DataTable
            $('#DCDoctorProfileGrid').DataTable().ajax.reload(null, false);
        },
        error: function () {
            $toggle.prop('checked', !status);
            alert("Failed to update status");
        }
    });
});


//$('#DCDoctorProfileGrid').on('change', '.profilestatusCheckbox', function () {
//    var $toggle = $(this);
//    var doctorPId = $toggle.data('id');
//    var status = $toggle.is(':checked');

//    var message = status
//        ? "Are you sure you want to set this doctor Profile Online?"
//        : "Are you sure you want to set this doctor Profile Offline?";

//    if (!confirm(message)) {
//        $toggle.prop('checked', !status);
//        return;
//    }

//    $.ajax({
//        type: "POST",
//        url: "/DCAdmin/SetDoctorProfileStatus",
//        data: { DoctorPId: doctorPId, status: status },
//        dataType: "json",
//        success: function (response) {
//            if (response > 0) {
//                $('#DCDoctorProfileGrid').DataTable().ajax.reload(null, false);
//            }
//        },
//        error: function () {
//            $toggle.prop('checked', !status);
//            alert("Failed to update status");
//        }
//    });
//});


//function setStatus(doctorPId) {
//    debugger
//    var status = $('#rowstatus' + doctorPId).is(':checked');
//    //var status = $('#checkstatus').prop('checked');
//    var form_data = new FormData();
//    form_data.append("status", status);
//    form_data.append("DoctorPId", doctorPId);
//    $.ajax({
//        type: "POST",
//        url: "/DCAdmin/SetDoctorProfileStatus",
//        dataType: "JSON",
//        data: form_data,
//        cache: false,
//        contentType: false,
//        processData: false,
//        success: function (response) {
//            if (response > 0) {
//                window.location.href = 'DCAdmin/DCDoctorProfileGrid'
//            }
//        }
//    });

//}
function setPriority(specialityID) {
    debugger
    var priority = $('#rowpriority' + specialityID).is(':checked');
    //var status = $('#checkstatus').prop('checked');
    var form_data = new FormData();
    form_data.append("Priority", priority);
    form_data.append("SpecialityID", specialityID);
    $.ajax({
        type: "POST",
        url: "/Admin/SetSpecilizationPriority",
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





