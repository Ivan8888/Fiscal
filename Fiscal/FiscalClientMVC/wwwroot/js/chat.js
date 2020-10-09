(function () {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub")
        .build();

    connection.start();



    connection.on("NewMessage", (sender, message, group_name) => {
        var element = document.createElement("p");
        element.innerText = sender + " : " + message;
        if (group_name === "admin_group") {
            $(".chatLog_admin")[0].append(element);
        } else if (group_name === "support_group") {
            $(".chatLog_support")[0].append(element);
        }
    });

    connection.on("GroupChange", (group_name, user_name, explanation) => {
        var element = document.createElement("p");
        element.innerText = user_name + " " + explanation + " group.";
        if (group_name === "admin_group") {
            $(".chatLog_admin")[0].append(element);
        } else if (group_name === "support_group") {
            $(".chatLog_support")[0].append(element);
        }
    });


    //admin_group implementation
    $(".send_admin").click(function () {
        var message = $(".message_admin").val();
        connection.invoke("MessageAll", message, "admin_group");
    });

    $(".join_admin").click(function () {
        connection.invoke("JoinGroup", "admin_group");
    });

    $(".leave_admin").click(function () {
        connection.invoke("LeaveGroup", "admin_group");
    });


    //support_group implementation
    $(".send_support").click(function () {
        var message = $(".message_support").val();
        connection.invoke("MessageAll", message, "support_group");
    });

    $(".join_support").click(function () {
        connection.invoke("JoinGroup", "support_group");
    });

    $(".leave_support").click(function () {
        connection.invoke("LeaveGroup", "support_group");
    });
})();


