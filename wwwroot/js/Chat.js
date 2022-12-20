"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (message, date) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `Time: ${date} user says ${message}`;
});

connection.on("InitReceiveMessage", function (data) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you
    // should be aware of possible script injection concerns.
    data.forEach(function (item) {
        li.textContent = `Time: ${item.messageTime} user says ${item.messageData}`;
    });
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    connection.invoke("JoinChatRoom", "1danielalferian71@gmail.comdanielalferian9@gmail.com").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var email = "danielalferian9@gmail.com";
    connection.invoke("SendMessageToGroup", "1danielalferian71@gmail.comdanielalferian9@gmail.com", message, email).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});