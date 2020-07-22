function OpenModalWithValidation() {
    if ($("#numberInput").val() != 0)
        $("#openModal").click();
}

function ClearModal() {
    $("#numberInput").val("");
    $("#statusInput").val("");
}