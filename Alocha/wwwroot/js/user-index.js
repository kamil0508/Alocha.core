function OpenModalWithValidation() {
    if ($("#sumaryValidationChangePassword").html().length != 40)
        $("#openModalChangePassword").click();
    if ($("#sumaryValidationAddPhoneNumber").html().length != 40)
        $("#openModalAddPhone").click();
}



