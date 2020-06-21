"use strict";
var KTDatatablesDataSourceAjaxServer = {
    init: function () {
        $("#kt_datatable").DataTable({
            responsive: !0,
            searchDelay: 500,
            processing: !0,
            // serverSide: !0,
            ajax: urlGetUser,
            columns: [
                { data: "UserID" },
                { data: "FirstName" },
                { data: "LastName", responsivePriority: -1 }
            ],
            columnDefs: [
                {
                    targets: -1,
                    title: "Actions",
                    orderable: !1,
                    render: function (t, a, e, l) {
                        return '\t\t\t\t\t\t\t<div class="dropdown dropdown-inline">\t\t\t\t\t\t\t\t<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" data-toggle="dropdown">\t                                <i class="la la-cog"></i>\t                            </a>\t\t\t\t\t\t\t  \t<div class="dropdown-menu dropdown-menu-sm dropdown-menu-right">\t\t\t\t\t\t\t\t\t<ul class="nav nav-hoverable flex-column">\t\t\t\t\t\t\t    \t\t<li class="nav-item"><a class="nav-link" href="#"><i class="nav-icon la la-edit"></i><span class="nav-text">Edit Details</span></a></li>\t\t\t\t\t\t\t    \t\t<li class="nav-item"><a class="nav-link" href="#"><i class="nav-icon la la-leaf"></i><span class="nav-text">Update Status</span></a></li>\t\t\t\t\t\t\t    \t\t<li class="nav-item"><a class="nav-link" href="#"><i class="nav-icon la la-print"></i><span class="nav-text">Print</span></a></li>\t\t\t\t\t\t\t\t\t</ul>\t\t\t\t\t\t\t  \t</div>\t\t\t\t\t\t\t</div>\t\t\t\t\t\t\t<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" title="Edit details">\t\t\t\t\t\t\t\t<i class="la la-edit"></i>\t\t\t\t\t\t\t</a>\t\t\t\t\t\t\t<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" title="Delete">\t\t\t\t\t\t\t\t<i class="la la-trash"></i>\t\t\t\t\t\t\t</a>\t\t\t\t\t\t'
                    }
                }
            ]
        })
    }
};

$(document).ready(function () {
    KTDatatablesDataSourceAjaxServer.init()
});