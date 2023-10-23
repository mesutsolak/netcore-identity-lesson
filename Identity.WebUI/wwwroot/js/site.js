let spinnerVisible = false;

function showProgress() {
    if (!spinnerVisible) {
        $("span#spinner").addClass("is-active");
        spinnerVisible = true;
    }
};

function hideProgress() {
    if (spinnerVisible) {
        let spinner = $("span#spinner");
        spinner.stop();
        spinner.removeClass("is-active");
        spinnerVisible = false;
    }
};

function onBegin() {
    showProgress();
}

function onFailure(response) {
    hideProgress();
    messageAlertWithError();
}

function onSuccess(response) {
    let errorMessages = "";

    $(".error-messages").html("");
    $(".modal-error-messages").html("");

    hideProgress();

    if (!response.isSuccess) {
        $.each(response.errors, function (index, value) {
            errorMessages += `- ${value}<br>`;
        });

        if ($('.modal-error-messages').length > 0 || $('.error-definition').length > 0) {
            var className = response.isModal ? "modal-error-messages" : "error-definition";
            $(`.${className}`).html(errorMessages);
        } else {
            messageAlert(response.icon, response.title, errorMessages.replace("<br>", "\n"));
        }
    }

    if (response.isSuccess && !isNil(response.successMessage)) {
        messageAlert(response.icon, response.title, response.successMessage);
    }


    if (!isNil(response.function)) {
        window[response.function](response.data);
    }

    if (!isNil(response.url)) {
        setTimeout(function () {
            window.location.assign(response.url);
        }, 1000)
    }
}

function submitForm(formId) {
    $(`#${formId}Form`).submit();
}

function modalHide(modalId) {
    $(`#${modalId}Modal`).modal("hide");
}

function modalShow(modalId) {
    $(`#${modalId}Modal`).modal("show");
}

function messageAlert(icon, title, text) {
    Swal.fire({
        icon: icon,
        title: title,
        text: text
    });
}

function messageAlertWithError() {
    messageAlert("error", "Uygulama Hatası", "Bir hata meydana geldi.Lütfen ilgili kişilerle iletişime geçiniz !")
}


function isNil(val) {
    return val === undefined || val === null || val === "";
}

function pageReflesh() {
    setTimeout(function () {
        location.reload();
    }, 1000);
}