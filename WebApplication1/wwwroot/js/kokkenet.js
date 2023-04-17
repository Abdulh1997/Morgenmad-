var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
"use strict";
connection.on("ReceiveMessage", function () {
    if (window.history.replaceState) {
        window.history.replaceState(null, null, window.location.href);
    }
    window.location = window.location.href;
    window.location.reload();
});

connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});
