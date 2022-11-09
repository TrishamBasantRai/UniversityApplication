function Register(e) {
    e.preventDefault();

    var RegisterModel = {
        EmailAddress: this.emailAddress.value,
        Password: this.password.value,
        ConfirmPassword: this.confirmPassword.value
    };

    var serverCall = new ServerCall({
        url: "/Register/RegisterNewAccount",
        params: JSON.stringify(RegisterModel), callType: "POST"
    })
    serverCall.xhrCall().then((result) => {

        if (result.hasErrors) {
            toastr.error("Error encountered! Please check them!");
            let errorPane = document.getElementById('errorPane');
            var html = `<ul>`;
            for (let errors of result.data) {
                html += `<li>${errors.ErrorMessage}</li>`
            }
            html += `</ul>`;
            errorPane.innerHTML = html;
        }
        else {
            toastr.success("Account successfully registed!");
            window.location = result.url;
        }
    });

}