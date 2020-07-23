setInterval(function () {
    var counter = document.querySelector("#counter");
    var count = counter.textContent * 1 - 1;
    counter.textContent = count;
    if (count <= 0) {
        $("#submitConfirmation").click();
    }
}, 1000);