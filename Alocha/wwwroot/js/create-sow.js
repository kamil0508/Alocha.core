function OpenModalWithValidation() {
    var test = $("#numberInput").val();
    if ($("#numberInput").val() != 0)
        $("#openModal").click();
}

function ClearModal() {
    $("#numberInput").val("");
    $("#statusInput").val("");
}