var connection = new signalR.HubConnectionBuilder()
    .withUrl("/mychathub")
    .build();

connection.on("NewMessage", (sender, message) => {
    $(".chatLog").text($(".chatLog").text() + " Message from " + sender + ":" + message);
});

connection.start().catch(err => console.error(err.toString()));

$(".all").click(function () {
    var sender = $(".username").val();
    var message = $(".message").val();
    connection.invoke("MessageAll", sender, message);
});