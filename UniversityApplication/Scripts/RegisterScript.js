$(function () {
    let form = document.querySelector('form');

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
    $("#buttonRegister").click(function () {
        var emailAddress = $("#emailAddress").val();
        var password = $("#password").val();
        var authObj = { EmailAddress: emailAddress, Password: password, ConfirmPassword: confirmPassword};
        $.ajax({
            type: "POST",
            url: "/Register",
            data: authObj,
            dataType: "json",
            success: function (response) {
                if (response.result) {
                    toastr.success('Email Registered! You may now login.');
                    window.location = response.url;
                }
                else {
                    toastr.error('Email already exists, please use another email.');
                    return false;
                }
            },
            failure: function (response) {
                toastr.error('Unable to make request!');
            },
            error: function (response) {
                toastr.error('Error. Please contact the System Administrator.');

            }
        });
    });
});