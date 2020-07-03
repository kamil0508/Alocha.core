var url;
$(document).ready(function () {
    url = document.getElementById("LinkUrl").value;
});

setInterval(function () {
    var counter = document.querySelector("#counter");
    var count = counter.textContent * 1 - 1;
    counter.textContent = count;
    if (count <= 0) {
        location.href = url;
    }
}, 1000);