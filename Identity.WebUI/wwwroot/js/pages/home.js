$(document).ready(function () {


    $("#btnLogout").on("click", function () {
        $.ajax({
            type: "GET",
            url: "Authorize/Logout",
            cache: false,
            processData: false,
            success: function (response) {
                if (response.isSuccess) {
                    window.location.assign(response.url);
                }
                else {
                    messageAlertWithError();
                }
            },
            fail: function (response) {
                console.log(response);
                messageAlertWithError();
            }
        });
    });
});