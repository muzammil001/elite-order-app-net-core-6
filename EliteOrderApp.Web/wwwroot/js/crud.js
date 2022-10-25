function getTable(tableId, url, columns) {

    const table = $(`#${tableId}`).DataTable({
        ajax: {
            url: url,
            dataSrc: ""
        },
        columns: columns
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

    $(`#${tableId}`).on("click",
        buttonId,
        function () {
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
                                    title: err.ResponseText,
                                    type: 'error'
                                });
                            }
                        });
                    }
                });
        });
}

function refreshTable(buttonId, table) {
    $(`${buttonId}`).click(function () {
        table.ajax.reload(null, false);
    });
}


function refreshTableWithTime(table,timeSpan) {
    setInterval(function () {
        table.ajax.reload(null, false); // user paging is not reset on reload
    }, timeSpan);
}