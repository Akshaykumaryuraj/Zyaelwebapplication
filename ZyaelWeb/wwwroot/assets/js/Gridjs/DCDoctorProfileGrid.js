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
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
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
                "data": "Slots",
                "name": "Slots",
                orderable: false,
                "className": "",
                "render": function (data, type, row, meta) {
                    return '<button type="button" class="viewDoctorSlotBtn btn btn-primary" data-id="' + row.doctorPId + '">Slots</button>';
                }
            },

            {
                "data": "status", sorting: false, orderable: false, "className": "table-actions", "render": function (data, type, row, meta) {
                    var Action = '';
                    Action += '<a class="viewDoctorBtn me-2" data-id="' + row.doctorPId + '"><img src="/images/eye.png"/></a> '; // Existing edit button 
                    Action += '<a href="/DCAdmin/DCDProfileDetailsAdd?DoctorPId=' + row.doctorPId + '"><img src="/images/edit.png"/></a>';
                    return Action;
                }


            },

        ]
    });
}

// Load slots

//flatpickr("#slotDate", {
//    dateFormat: "Y-m-d",   // ✅ matches your API format
//    defaultDate: new Date(), // optional: preselect today
//    onChange: function (selectedDates, dateStr) {
//        var doctorPId = $('#doctorSlotDetailsModal').data('doctor-id');
//        loadDoctorSlotsByDate(doctorPId, dateStr); // reload slots for chosen date
//    }
//});

flatpickr("#slotDate", {
    altInput: true,
    altFormat: "F j, Y",   // e.g. November 30, 2025
    dateFormat: "Y-m-d"
});

$('#DCDoctorProfileGrid').on('click', '.viewDoctorSlotBtn', function () {
    var doctorPId = $(this).data('id');
    $('#doctorSlotDetailsModal').data('doctor-id', doctorPId);

    var modal = new bootstrap.Modal(document.getElementById('doctorSlotDetailsModal'));
    modal.show();

    var today = new Date().toISOString().split('T')[0];
    $('#slotDate').val(today);
    loadDoctorSlotsByDate(doctorPId, today);
});

$('#slotDate').on('change', function () {
    var doctorPId = $('#doctorSlotDetailsModal').data('doctor-id');
    var selectedDate = $(this).val();
    loadDoctorSlotsByDate(doctorPId, selectedDate);
});

function loadDoctorSlotsByDate(doctorPId, selectedDate) {
    $('#doctorSlots').html('<tr><td colspan="3" class="text-center">Loading...</td></tr>');

    $.ajax({
        url: "/DCAdmin/GetDoctorSlotsByDateandID",
        type: "GET",
        data: { DoctorPId: doctorPId, Date: selectedDate },
        dataType: "json",
        success: function (slots) {
            var rows = '';
            $.each(slots, function (i, slot) {
                var slotDate = slot.date.split('T')[0]; // "2025-11-30"
                if (slot.available === true && slotDate === selectedDate) {
                    rows += '<tr>' +
                        //'<td>' + slotDate + '</td>' +
                        '<td>' + slot.time + '</td>' +
                        '<td><span class="badge bg-success">Available</span></td>' +
                        '</tr>';
                }
            });

            if (rows === '') {
                rows = '<tr><td colspan="3" class="text-center text-muted">No available slots for this date</td></tr>';
            }

            $('#doctorSlots').html(rows);
        },
        error: function () {
            $('#doctorSlots').html('<tr><td colspan="3" class="text-danger text-center">Failed to load slots</td></tr>');
        }
    });
}




$('#DCDoctorProfileGrid').on('click', '.viewDoctorBtn', function () {
    var doctorPId = $(this).data('id');

    // Show modal
    var modal = new bootstrap.Modal(document.getElementById('doctorDetailsModal'));
    modal.show();

    // Load profile info
    $.get("/DCAdmin/GetDoctorProfileDetails", { DoctorPId: doctorPId }, function (doctor) {
        $('#doctorName').text(doctor.firstName);
        $('#doctorLastName').text(doctor.lastName);
        $('#doctorGender').text(doctor.gender);
        $('#doctorStudies').text(doctor.studies);
        $('#doctorPhone').text(doctor.phoneNumber);
        $('#doctorCategory').text(doctor.consultationCategory);      
        $('#doctorExperience').text(doctor.experience);
        $('#doctorEmailAddress').text(doctor.emailAddress);
        $('#doctorConsultationFees').text(doctor.consultationFees);
        $('#doctorProficientLanguage').text(doctor.proficientLanguage);
        $('#doctorDoctorBio').text(doctor.doctorBio);
        $('#doctorDoctorBio_1').text(doctor.doctorBio_1);
        $('#doctorDoctorBio_2').text(doctor.doctorBio_2);
        $('#doctorDoctorProcedure').text(doctor.doctorProcedure);
        $('#doctorDoctorProcedure_1').text(doctor.doctorProcedure_1);
        $('#doctorDoctorProcedure_2').text(doctor.doctorProcedure_2);
        $('#doctorDoctorIntroVideoLink').text(doctor.doctorIntroVideoLink);
        $('#doctorDoctorLatitude').text(doctor.latitude);
        $('#doctorDoctorLongitude').text(doctor.longitude);
        $('#doctorCity').text(doctor.city);
        $('#doctorAddress_1').text(doctor.address_1);
        $('#doctorAddress_2').text(doctor.address_2);
        $('#doctorAboutDoctor').text(doctor.aboutDoctor);
        $('#doctorStatus').text(doctor.status ? "Online" : "Offline");
    });
});

// Confirm button example
$('#confirmActionBtn').on('click', function () {
    alert("Confirmed action for doctor profile!");
    var modal = bootstrap.Modal.getInstance(document.getElementById('doctorDetailsModal'));
    modal.hide();
});


// Handle View button click
//$('#DCDoctorProfileGrid').on('click', '.viewDoctorBtn', function () {
//    var doctorPId = $(this).data('id');

//    $.ajax({
//        type: "GET",
//        url: "/DCAdmin/GetDoctorProfileDetails", // ✅ create this API
//        data: { DoctorPId: doctorPId },
//        dataType: "json",
//        success: function (doctor) {
//            if (doctor) {
//                // Populate modal fields
//                $('#doctorName').text(doctor.firstName);
//                $('#doctorPhone').text(doctor.phoneNumber);
//                $('#doctorCategory').text(doctor.consultationCategory);
//                $('#doctorDotp').text(doctor.dcdotp);
//                $('#doctorStatus').text(doctor.status ? "Online" : "Offline");

//                // Show modal
//                $('#doctorDetailsModal').modal('show');
//            } else {
//                alert("Doctor details not found.");
//            }
//        },
//        error: function () {
//            alert("Failed to fetch doctor details.");
//        }
//    });
//});




// Example confirm button action
$('#confirmActionBtn').on('click', function () {
    alert("Confirmed action for doctor profile!");
    $('#doctorDetailsModal').modal('hide');
});


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





