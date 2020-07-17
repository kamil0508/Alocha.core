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
            else {
                document.getElementById("vaccineDay").readOnly = true;
            }

            if ($("#status").val() != "")
                document.getElementById("endDate").style.borderColor = "#00c2cc";           
        }
    });
}
