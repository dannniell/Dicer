"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (message, date, senderEmail) {
    var currentEmail = document.getElementById("currentEmail").value;

    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you
    // should be aware of possible script injection concerns.
    if (senderEmail === currentEmail) {
        $('#discussion').append('<p class="chatSender"><small>' + date + '  </small> : <strong>' + message + '</strong></p><br><br>');
    }
    else {
        $('#discussion').append('<p class="chatReceiver"><small>' + date + '  </small> : <strong>' + message + '</strong></p><br><br>');
    }

    var elem = document.getElementById('chat');
    elem.scrollTop = elem.scrollHeight;
});

connection.on("InitReceiveMessage", function (data) {
    var currentEmail = document.getElementById("currentEmail").value;
    var client = document.getElementById("clientMail").value;
    var creator = document.getElementById("creatorMail").value;

    var targetEmail;
    if (currentEmail === client) {
        targetEmail = creator;
    } else {
        targetEmail = client;
    };

    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you
    // should be aware of possible script injection concerns.
    for (var item in data) {
        if (data[item].email !== targetEmail) {
            $('#discussion').append('<p class="chatSender"><small>' + data[item].messageTime +'  </small> : <strong>' + data[item].messageData + '</strong></p><br><br>');
            
        }
        else {
            $('#discussion').append('<p class="chatReceiver"><small>' + data[item].messageTime + '  </small> : <strong>' + data[item].messageData + '</strong></p><br><br>');
            
        }
    }

    var elem = document.getElementById('chat');
    elem.scrollTop = elem.scrollHeight;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;

    var campaignId = document.getElementById("campaignId").value;
    var client = document.getElementById("clientMail").value;
    var creator = document.getElementById("creatorMail").value;

    var groupName = campaignId + client + creator;
    connection.invoke("JoinChatRoom", groupName).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;

    var campaignId = document.getElementById("campaignId").value;
    var client = document.getElementById("clientMail").value;
    var creator = document.getElementById("creatorMail").value;

    var groupName = campaignId + client + creator;
    var currentEmail = document.getElementById("currentEmail").value;

    connection.invoke("SendMessageToGroup", groupName, message, currentEmail).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();

    $('#messageInput').val('');
});