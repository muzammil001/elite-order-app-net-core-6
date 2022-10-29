function redirectAction(controller,action,params="", timeSpan) {
    setInterval(function () {
        window.location.href = `/${controller}/${action}/${params}`;
    }, timeSpan);
}