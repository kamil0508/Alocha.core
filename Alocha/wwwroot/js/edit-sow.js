function calculateDate() {
    $.ajax({
        type: "GET",
        url: "/Sow/CalculateDate",
        data: {
            date: $("#dateHappening").val(),
            status: $("#status").val()
        },
        contentType: "application/json; charset=utf-8",
        dataType: "text",
        success: function (response) {
            $("#endDate").val(response);            
            if ($("#status").val() == "Prośna") {
                document.getElementById("vaccineDay").readOnly = false;
            }
            if ($("#status").val() == "Laktacja") {
                document.getElementById("vaccineDay").readOnly = true;
                document.getElementById("pigQuantity").readOnly = false;
            }
            if ($("#status").val() == "Luźna") {
                document.getElementById("vaccineDay").readOnly = true;
                document.getElementById("pigQuantity").readOnly = true;
            }
            if ($("#status").val() != "") {
                document.getElementById("endDate").style.borderColor = "#00c2cc";
            }
            if ($("#status").val() == "") {
                document.getElementById("vaccineDay").readOnly = true;
            }
        }
    });
}
