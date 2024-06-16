
$(document).ready(function () {
    debugger
    var datatable = $('#LabtestDetailsGrid').dataTable();
    datatable.fnDestroy();
    BindLabtestDetailsGrid();
});

function BindLabtestDetailsGrid(vendor) {
    debugger
    var count = 1;
    var table = $("#LabtestDetailsGrid").DataTable({
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
                var info = $('#LabtestDetailsGrid').DataTable().page.info();
                var TemplateStr = 'pageNumber=' + (info.page + 1);

                if ($('#LabtestDetailsGrid_length select').val() > 0) {
                    TemplateStr += '&pagesize=' + $('#LabtestDetailsGrid_length select').val();
                } else {
                    TemplateStr += '&pageSize=100';
                }
                TemplateStr += '&vendor=' + vendor;
                $('#LabtestDetailsGrid').DataTable().ajax.url(
                    '/DiagnosticLabProduct/getLabTestGridDetails?' + TemplateStr
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
                "data": "labTestName", "name": "labTestName", orderable: false, "className": "",

            },

         
            {
                "data": "labTestCode", "name": "labTestCode", orderable: false, "className": "",

            },
            {
                "data": "labTestPrice", "name": "labTestPrice", orderable: false, "className": "",

            },
            {
                "data": "labTestDiscountPrice", "name": "labTestDiscountPrice", orderable: false, "className": "",

            },
            {
                "data": "status", "name": "status", orderable: false, "className": "",
                "render": function (data, type, row, meta) {
                    var status = '';
                    console.log(row.status);
                    status += '<label class="switch">';
                    status += '<input type="checkbox" ' + (row.status == "True" ? "checked" : "") + ' id="rowstatus' + row.labTestID + '"  onclick="setStatus(' + row.labTestID + ');">';
                    //status += '<span class="slider round"></span>';
                    status += '</label>';

                    return status;
                }
            },

            {
                "data": "status", sorting: false, orderable: false, "className": "table-actions",
                "render": function (data, type, row, meta) {
                    var Action = '';
                    Action += '<a href="/DiagnosticLabProduct/LabTestDetailsAdd?LabTestID=' + row.labTestID + ' "><img src="/images/edit.png"/></a>';
                    Action += '<a href="#" onclick="ProfileHeadDetailsDelete(' + row.idOCTORid + ')"><img src="/images/delete.png"/></a>';
                    return Action;
                }

            }
        ]
    });
}
//function setStatus(labTestID) {

//    var status = $('#rowstatus' + labTestID).is(':checked');
//    //var status = $('#checkstatus').prop('checked');
//    var form_data = new FormData();
//    form_data.append("status", status);
//    form_data.append("LabTestID", labTestID);
//    $.ajax({
//        type: "POST",
//        url: "/DiagnosticLabProduct/SetLabTestActiveStatus",
//        dataType: "JSON",
//        data: form_data,
//        cache: false,
//        contentType: false,
//        processData: false,
//        success: function (response) {
//            if (response > 0) {
//                window.location.href = 'DiagnosticLabProduct/LabTestsGrid'
//            }
//        }
//    });
//}

function setStatus(labTestID) {

    var status = $('#rowstatus' + labTestID).is(':checked');
    //var status = $('#checkstatus').prop('checked');
    var form_data = new FormData();
    form_data.append("status", status);
    form_data.append("LabTestID", labTestID);
    $.ajax({
        type: "POST",
        url: "/DiagnosticLabProduct/SetLabTestActiveStatus",
        dataType: "JSON",
        data: form_data,
        cache: false,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response > 0) {
                window.location.href = 'DiagnosticLabProduct/LabTestsGrid'
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