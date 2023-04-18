"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/notification")
    .build();

connection.on("update", function () {
    window.location.reload();
});

connection.start()
    .then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});