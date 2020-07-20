function RemoveFromTable(smallPigId) {
    var result = confirm("Czy napewno chcesz usunąć ten wpis?");
    if (!result)
        return;
    else {
        $.ajax({
            type: "GET",
            url: "/SmallPig/Remove",
            data: { id: smallPigId },
            contentType: "application/json; charset=utf-8",
            dataType: "text",
            success: function () {
                location.reload(true);
            },
            error: function (response) {
                alert(response);
            }
        });
    }
}