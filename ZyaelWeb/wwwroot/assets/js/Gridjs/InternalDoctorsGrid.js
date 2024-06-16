
$(document).ready(function () {
    debugger
    var datatable = $('#InternalDoctorsGrid').dataTable();
    datatable.fnDestroy();
    BindInternalDoctorsGrid();
});

function BindInternalDoctorsGrid(vendor) {
    debugger
    var count = 1;
    var table = $("#InternalDoctorsGrid").DataTable({
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
                var info = $('#InternalDoctorsGrid').DataTable().page.info();
                var TemplateStr = 'pageNumber=' + (info.page + 1);

                if ($('#InternalDoctorsGrid_length select').val() > 0) {
                    TemplateStr += '&pagesize=' + $('#InternalDoctorsGrid_length select').val();
                } else {
                    TemplateStr += '&pageSize=100';
                }
                TemplateStr += '&vendor=' + vendor;
                $('#InternalDoctorsGrid').DataTable().ajax.url(
                    '/InternalDoctor/getInternalDoctorsDetails?' + TemplateStr
                );
            },
            "dataSrc": function (json) {
                console.log(json);
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
                "data": "firstName", "name": "firstName", orderable: false, "className": "",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    Action += '<a href="/InternalDoctor/InternalDoctorDetailsAdd?IDoctorID=' + row.iDoctorID + ' ">' + row.firstName + '</a>';
                    return Action;
                }
            },
            {
                "data": "specialization", "name": "specialization", orderable: false, "className": "",

            },

            //{
            //    "data": "emailAddress", "name": "emailAddress", orderable: false, "className": "",

            //},
            {
                "data": "city", "name": "city", orderable: false, "className": "",

            },
            {
                "data": "status", "name": "status", orderable: false, "className": "",
                "render": function (data, type, row, meta) {
                    var status = '';
                    status += '<label class="switch">';
                    status += '<input type="checkbox" ' + (row.status == "True" ? "checked" : "") + ' id="rowstatus' + row.iDoctorID + '"  onclick="setStatus(' + row.iDoctorID + ');">';
                    //status += '<span class="slider round"></span>';
                    status += '</label>';


                    return status;
                }
            },

            {
                "data": "views", "name": "views", orderable: false, "className": "",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    //Action += '<a href="/InternalDoctor/_Partialslotspopup?IDoctorID=' + row.iDoctorID + ' ">Active</button>';
                    Action += '<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#enableOTP" onclick="showviewslotpopup(\'' + row.firstName + '\',' + row.iDoctorID + ')">View</button>';
                    //Action += '<label class="">active</label>';
                    return Action;
                }

            },
            
            {
                "data": "updates", "name": "updates", orderable: false, "className": "",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    //Action += '<a href="/InternalDoctor/_Partialslotspopup?IDoctorID=' + row.iDoctorID + ' ">Active</button>';
                    Action += '<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#smallModal" onclick="showslotpopup(\'' + row.firstName + '\',' + row.iDoctorID + ')">Active</button>';
                    //Action += '<label class="">active</label>';
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
function setStatus(iDoctorID) {

    var status = $('#rowstatus' + iDoctorID).is(':checked');
    //var status = $('#checkstatus').prop('checked');
    var form_data = new FormData();
    form_data.append("status", status);
    form_data.append("IDoctorID", iDoctorID);
    $.ajax({
        type: "POST",
        url: "/InternalDoctor/SetInternalDoctorActiveStatus",
        dataType: "JSON",
        data: form_data,
        cache: false,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response > 0) {
                window.location.href = 'InternalDoctor/InternalDoctorsGrid'
            }
        }
    });
}


function showviewslotpopup(firstName, Idoctorid) {
    $('#firstName').val(firstName);
    $('#hdnIDoctorID').val(Idoctorid);

}
function showslotpopup(firstName, Idoctorid) {
    $('#firstName').val(firstName);
    $('#hdnIDoctorID').val(Idoctorid);

}