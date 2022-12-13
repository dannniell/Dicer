var globalUserId;
var globalBalance;
var withdraw = function () {
    var self = this; 

    self.getBalance = function () {
        var ajaxTypesObj = {
            url: "/Api/Payment/" + globalUserId,
            method: "GET",
            dataType: "json",
            success: function (data) {
                globalBalance = data;
                document.getElementById("accountBalance").innerHTML = 'Balance: Rp. ' + String(data).replace(/(.)(?=(\d{3})+$)/g, '$1,');
            },
            error: function (e) {
                alert('Failed to Insert Transaction Detail Data!');
            },
            complete: function (data) {
                //alert('Transaction Detail Data Successfully Inserted!');
            }
        };
        $.ajax(ajaxTypesObj);
    };

    self.withdrawal = function (amount) {
        var ajaxTypesObj = {
            url: "/Api/Payment/" + globalUserId + "/Withdrawal/" + amount,
            method: "POST",
            dataType: "json",
            success: function (data) {
                alert("Withdrawal Success!!");
                location.reload();
            },
            error: function (e) {
                alert('Failed to Insert Transaction Detail Data!');
            },
            complete: function (data) {
                //alert('Transaction Detail Data Successfully Inserted!');
            }
        };
        $.ajax(ajaxTypesObj);
    };
}

var globalWithdraw = new withdraw();

$(document).ready(function () {
    globalUserId = document.getElementById("InitUserId").value;
    globalWithdraw.getBalance();
});

$('#submitForm').on('click', function (e) {
    var amount = document.getElementById("withdrawalAmount").value;
    var bankAccount = document.getElementById("bankAccountNumber").value;
    if (bankAccount !== "") {
        if (amount !== "") {
            if (amount <= globalBalance) {
                globalWithdraw.withdrawal(amount);
            } else {
                alert("Not Enough Balance !!");
                $("#withdrawalAmount").val("");
            }
        } else {
            alert("Please Enter Withdrawal Amount!!");
        }
        
    } else {
        alert("Please Enter Valid Bank Account Number!!");
    }
    
});