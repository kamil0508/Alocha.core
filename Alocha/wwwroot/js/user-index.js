function OpenModalWithValidation() {
    if ($("#sumaryValidation").html().length != 40)
        $("#openModal").click();
}
