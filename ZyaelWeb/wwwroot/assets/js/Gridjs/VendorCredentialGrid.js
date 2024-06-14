
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
                console.log(json.data);

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
                    var status = '';
                    status += '<label class="switch">';
                    status += '<input type="checkbox" ' + (row.status == "True" ? "checked" : "") + ' id="rowstatus' + row.id + '"  onclick="setStatus(' + row.id + ',\'' + vendor+'\');">';
                    //status += '<span class="slider round"></span>';
                    status += '</label>';


                    return status;
                }
            },

            {
                "data": "action", sorting: false, orderable: false, "className": "table-actions",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    Action += '<a href="#" onclick="ProfileHeadDetailsDelete(' + row.idOCTORid + ')"><img src="/images/edit.png"/></a>';
                    Action += '<a href="#" onclick="ProfileHeadDetailsDelete(' + row.idOCTORid + ')"><img src="/images/delete.png"/></a>';
                    return Action;
                }

            }
        ]
    });


   
    }
function setStatus(id, vendor) {
    debugger
    var status = $('#rowstatus' + id).is(':checked');
    //var status = $('#checkstatus').prop('checked');
    var form_data = new FormData();
    form_data.append("status", status);
    form_data.append("ID", id);
    form_data.append("Vendor", vendor);

    $.ajax({
        type: "POST",
        url: "/Admin/SetVendorsLoginStatus",
        dataType: "JSON",
        data: form_data,
        cache: false,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response > 0) {
                window.location.href = 'Admin/VendorsCredentialGrid'
            }
        }
    });
}
