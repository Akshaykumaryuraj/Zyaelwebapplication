
$(document).ready(function () {
    debugger
    var datatable = $('#Vendorcredentialgrid').dataTable();
    datatable.fnDestroy();
    BindVendorsCredentialGrid();
});

function BindVendorsCredentialGrid(vendor) {
    debugger
    var count = 1;
    var table = $("#Vendorcredentialgrid").DataTable({
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
                var info = $('#Vendorcredentialgrid').DataTable().page.info();
                var TemplateStr = 'pageNumber=' + (info.page + 1);

                if ($('#Vendorcredentialgrid_length select').val() > 0) {
                    TemplateStr += '&pagesize=' + $('#Vendorcredentialgrid_length select').val();
                } else {
                    TemplateStr += '&pageSize=100';
                }
                TemplateStr += '&vendor=' + vendor;
                $('#Vendorcredentialgrid').DataTable().ajax.url(
                    '/Admin/getVendorsCredentialDetails?' + TemplateStr
                );
            },
            "dataSrc": function (json) {
               
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
                "data": "userName", "name": "userName", orderable: false, "className": "",

            },

            {
                "data": "email", "name": "email", orderable: false, "className": "",

            },

            {
                "data": "status", "name": "status", orderable: false, "className": "",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    Action += '<label class="badge badge-danger">Pending</label>';
                    return Action;
                }
               
            },

            {
                "data": "status", sorting: false, orderable: false, "className": "table-actions",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    Action += '<div class="dropsdown">';
                    Action += '<button type="button dropbtn" class="btn-sm btn-gradient-dark btn-icon-text dropbtn">Edit<i class="mdi mdi-file-check btn-icon-append" ></i > ';
                    Action += '<div class="dropdown-content"><a href = "#">Pending</a><a href="#">In progress</a><a href="#">Rejected</a></div> ';
                    Action += '</button></div>';
                    Action += '<button type="button" class="btn-sm btn-gradient-primary btn-icon-text">';
                    Action += '<i class="mdi mdi-file-check btn-icon-prepend"></i>Submit';
                    Action += '</button>';
                    return Action;
                }

            }
        ]
    });
}
