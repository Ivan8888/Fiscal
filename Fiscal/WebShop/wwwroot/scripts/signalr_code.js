(function() {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub")
        .build();

    connection.start().catch(err => console.error(err.toString()));

    connection.on("NewMessage", (sender, message) => {
        var element = document.createElement("p");
        element.innerText = sender + ':' + message;
        $("#all_messages").append(element);
    });

    connection.on("GroupChange", list => {
        $("#group_members").text(list);
    });

    $("#send_message").click(function () {
        var message = $('#message_text').val();
        connection.invoke("MessageAll", message);
    });

    $("#join_group").click(function () {
        var sender = $("#sender_text").val();
        connection.invoke("JoinGroup", sender);
    });

    $("#leave_group").click(function () {
        connection.invoke("LeaveGroup");
    });
})();