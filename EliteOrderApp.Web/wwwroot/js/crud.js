function getTable(tableId, url, columns, columnDefs=null, paging = true, ordering = true, info = true, filter = true) {

    const table = $(`#${tableId}`).DataTable({
        "paging": paging,
        "ordering": ordering,
        "info": info,
        "bFilter": filter,
        ajax: {
            url: url,
            dataSrc: ""
        },
        columns: columns,
        columnDefs: columnDefs
    });

    return table;
}

function editRecord(tableId, buttonId, popUpUrl, buttonDataId, modalTitle) {
    $(`#${tableId}`).on("click",
        buttonId,
        function () {
            const button = $(this);

            showInPopup(`${popUpUrl}${button.attr(`${buttonDataId}`)}`, `${modalTitle}`);
        });
}

function deleteRecord(tableId, buttonId, url, buttonDataId, table) {

    $(`#${tableId}`).on("click", buttonId, function (e) {
        var button = $(this);
        bootbox.confirm("Are you sure you want to delete this?",
            function (result) {
                if (result) {
                    $.ajax({
                        url: `${url}` + button.attr(`${buttonDataId}`),
                        method: "DELETE",
                        success: function () {
                            new PNotify({
                                title: 'Record has been deleted.',
                                type: 'success'
                            });
                            table.row(button.parents("tr")).remove().draw();
                        },
                        error: function (err) {
                            new PNotify({
                                title: err.responseText,
                                type: 'error'
                            });
                        }
                    });
                }
            });
    });
}

function approvedRecord(tableId, buttonId, url, buttonDataId, table,confirmMsg) {

    $(`#${tableId}`).on("click", buttonId, function (e) {
        var button = $(this);
        bootbox.confirm(confirmMsg,
            function (result) {
                if (result) {
                    $.ajax({
                        url: `${url}` + button.attr(`${buttonDataId}`),
                        method: "GET",
                        success: function () {
                            new PNotify({
                                title: 'Record has been updated.',
                                type: 'success'
                            });
                            table.row(button.parents("tr")).remove().draw();
                        },
                        error: function (err) {
                            new PNotify({
                                title: err.responseText,
                                type: 'error'
                            });
                        }
                    });
                }
            });
    });
}

function refreshTable(table) {
    table.ajax.reload(null, false);
}

function refreshTableOnClick(buttonId, table) {
    $(`${buttonId}`).click(function () {
        table.ajax.reload(null, false);
    });
}
function refreshPage(timeSpan) {
    setInterval(function () {
        window.location.reload(); // user paging is not reset on reload
    }, timeSpan);
}

function refreshTableWithTime(table, timeSpan) {
    setInterval(function () {
        table.ajax.reload(null, false); // user paging is not reset on reload
    }, timeSpan);
}

function saveRecord(url, data, table, msg) {
    $.ajax({
        url: url,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        method: "POST",
        success: function () {
            new PNotify({
                title: msg,
                type: 'success'
            });
            if (table !== null) {
                table.ajax.reload(null, false);
            }

        },
        error: function (err) {

            new PNotify({
                title: err.responseText,
                type: 'error'
            });
        }
    });
}

