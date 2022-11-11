function redirectAction(controller,action,params="", timeSpan) {
    setInterval(function () {
        window.location.href = `/${controller}/${action}/${params}`;
    }, timeSpan);
}

function formatMoney(n) {
    return "Rs " + (Math.round(n * 100) / 100).toLocaleString();
}