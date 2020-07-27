window.onload = function () {
    $.ajax({
        type: "GET",
        url: "/Home/UpcomingTaskCount",
        contentType: "application/json; charset=utf-8",
        dataType: "text",
        success: function (count) {
            if (count != "0") {
                alertify.set("notifier", "delay", 10);
                var msg = alertify.error("Ilość najblirzszych zdarzeń: " + count);
                msg.callback = function (isClicked) {
                    if (isClicked)
                        window.location.href = "/Home/UpcomingTask";
                };
            }
            else {
                alertify.set("notifier", "delay", 10);
                var msg = alertify.success("Nie ma nadchodzących zdarzeń w najbliższym tygodniu");
                msg.callback = function (isClicked) {
                    if (isClicked)
                        window.location.href = "/Home/UpcomingTask";
                };
            }
        },
        error: function () {
            var msg = alertify.success("Loading...");
            msg.callback = function (isClicked) {
                if (isClicked)
                    window.location.href = "/Home/UpcomingTask";
            };
        }
    });
}