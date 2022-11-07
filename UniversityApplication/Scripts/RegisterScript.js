function buildErrorMessage(ul, errorMessage) {
    var li = document.createElement('LI');
    li.innerHTML = errorMessage;
    ul.appendChild(li);

    return ul;
}


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
            var ul = document.createElement('UL');
            const errorPane = document.getElementById("errorPane");
            errorPane.innerHTML = ""; 

            for (var i = 0; i < result.data.length; i++) {
                buildErrorMessage(ul, result.data[i].ErrorMessage);
            }

            errorPane.appendChild(ul);
        }
        else {
            window.location = result.url;
        }
    });

}