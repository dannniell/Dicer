"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (message, date, namaSendiri, namaLawanBicara) {
    var namaSender = namaSendiri;
    var li = document.createElement("li");
    var br = document.createElement("br");
    var br2 = document.createElement("br");
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    if (namaSender !== namaLawanBicara) {
        li.id = 'sender'
        li.className = 'chatSender';
        li.textContent = `${date} ${namaSendiri} says ${message}`;
    }
    else if (namaSender == namaLawanBicara) {
        li.id = 'receiver'
        li.className = 'chatReceiver';
        li.textContent = `${date} ${namaLawanBicara} says ${message}`;
    }
    document.getElementById("messagesList").appendChild(li);
    document.getElementById("messagesList").appendChild(br);
    document.getElementById("messagesList").appendChild(br2);
});

connection.on("InitReceiveMessage", function (data) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you
    // should be aware of possible script injection concerns.
    data.forEach(function (item) {
        li.textContent = `${item.messageTime} user says ${item.messageData}`;
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
    var message = document.getElementById("messageInput").value;
    var namaSendiri = document.getElementById("userInput").value;
    var namaLawanBicara = 'Daniel';
    connection.invoke("SendMessageToGroup", "1danielalferian71@gmail.comdanielalferian9@gmail.com", message, namaSendiri, namaLawanBicara).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});