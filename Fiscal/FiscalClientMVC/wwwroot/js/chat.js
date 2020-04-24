(function () {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub")
        .build();

    connection.on("NewMessage", (sender, message) => {
        var element = document.createElement("p");
        element.innerText = " Message from " + sender + " : " + message;
        $(".chatLog")[0].append(element);

        //$(".chatLog").text($(".chatLog").text() + " Message from " + sender + " : " + message);
    });

    connection.start();

    $(".all").click(function () {
        var message = $(".message").val();
        connection.invoke("MessageAll", message);
    });
})();


