function buildErrorMessage(ul, errorMessage) {
    var li = document.createElement('LI');
    li.innerHTML = errorMessage;
    ul.appendChild(li);

    return ul;
}


function Details(e) {
    e.preventDefault();

    var studentModel = {
        NationalIdentityNumber: this.NID.value,
        FirstName: this.firstName.value,
        LastName: this.lastName.value,
        Address: this.address.value,
        PhoneNumber: this.phoneNumber.value,
        DateOfBirth: this.dob.value,
        GuardianName: this.guardianName.value
    };

    var serverCall = new ServerCall({
        url: "/Student/Index",
        params: JSON.stringify(studentModel), callType: "POST"
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