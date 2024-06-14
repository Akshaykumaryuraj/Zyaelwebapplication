
$(document).ready(function () {
    debugger
    var datatable = $('#PatientDetailsGrid').dataTable();
    datatable.fnDestroy();
    BindPatientDetailsGrid();
});

function BindPatientDetailsGrid(vendor) {
    debugger
    var count = 1;
    var table = $("#PatientDetailsGrid").DataTable({
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
                var info = $('#PatientDetailsGrid').DataTable().page.info();
                var TemplateStr = 'pageNumber=' + (info.page + 1);

                if ($('#PatientDetailsGrid_length select').val() > 0) {
                    TemplateStr += '&pagesize=' + $('#PatientDetailsGrid_length select').val();
                } else {
                    TemplateStr += '&pageSize=100';
                }
                TemplateStr += '&vendor=' + vendor;
                $('#PatientDetailsGrid').DataTable().ajax.url(
                    '/Patient/getPatientGridDetails?' + TemplateStr
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

            //{
            //    "data": "patientName", "name": "patientName", orderable: false, "className": "",
               
            //},
            {
                "data": "patientName", "name": "patientName", orderable: false, "className": "",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    Action += '<a href="/Patient/PatientRecords?PatientID=' + row.patientID + ' ">' + row.patientName + '</a>';
                    return Action;
                }
            },

            {
                "data": "patientRegNo", "name": "patientRegNo", orderable: false, "className": "",

            },
            {
                "data": "patientMobileNumber", "name": "patientMobileNumber", orderable: false, "className": "",

            },

            {
                "data": "status", "name": "status", orderable: false, "className": "",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    Action += '<label class="">Pending</label>';
                    return Action;
                }
               
            },

            {
                "data": "status", sorting: false, orderable: false, "className": "table-actions",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    Action += '<a href="/Patient/getPatientGridDetails?PatientID=' + row.patientID + ' "><img src="/images/edit.png"/></a>';
                    Action += '<a href="#" onclick="ProfileHeadDetailsDelete(' + row.idOCTORid + ')"><img src="/images/delete.png"/></a>';
                    return Action;
                }

            }
        ]
    });
}
