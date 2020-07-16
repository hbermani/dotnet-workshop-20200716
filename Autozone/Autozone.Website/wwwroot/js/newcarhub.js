
var connection = new signalR.HubConnectionBuilder().withUrl("/newcarhub").build();

connection.on("ReceiveMessage", function(user, message) {
    const $list = $("#latest-cars-list");
    const $element = $(`<li>${message}</li>`);
    $list.append($element);
});

connection.start().then(function () {
    console.log("SignalR connection started!");
}).catch(function (err) {
    return console.error(err.toString());
});

