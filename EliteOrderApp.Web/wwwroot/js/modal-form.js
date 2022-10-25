//popup 
showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal("show");
        }
    });
}
jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    //$('#view-all').html(res.html);
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    new PNotify({
                        title: 'Record has been added.',
                        type: 'success'
                    });
                    $(".refresh-table").click();
                } else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                new PNotify({
                    title: err.responseText,
                    type: 'error',
                    width: "100%",
                    cornerclass: "no-border-radius",
                    addclass: "stack-custom-bottom bg-primary",
                    stack: { "dir1": "up", "dir2": "right", "spacing1": 1 }
                });
            }
        });
    } catch (e) {
        console.log(e);
    }

    //to prevent form submit event
    return false;
}
