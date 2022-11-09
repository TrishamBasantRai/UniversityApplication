function fetchStudentStatus() {
    var serverCall = new ServerCall({
        url: "/Student/GetStatusDetails",
        params: JSON.stringify(), callType: "POST"
    })

    serverCall.xhrCall().then((result) => {
        if (!result.data)
            return;

        let name = document.getElementById('name');
        let status = document.getElementById('status');
        let score = document.getElementById('score');
        name.innerHTML += `${result.data.FirstName} ${result.data.LastName}`
        score.innerHTML += `${result.data.TotalPoints}`
        status.innerHTML += `${result.data.ApplicationStatus}`
    });
}