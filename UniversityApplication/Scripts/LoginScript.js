function buildErrorMessage(ul, errorMessage) {
    var li = document.createElement('LI');
    li.innerHTML = errorMessage;
    ul.appendChild(li);

    return ul;
}


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