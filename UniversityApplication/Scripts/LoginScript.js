function SignIn(e) {
    e.preventDefault();

    var LoginModel = {
        EmailAddress: this.emailAddress.value,
        Password: this.password.value
    };

    var serverCall = new ServerCall({
        url: "/Login/LogIntoAccount",
        params: JSON.stringify(LoginModel), callType: "POST"
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
            toastr.success("Logged in!");
            window.location = result.url;
        }
    });

}