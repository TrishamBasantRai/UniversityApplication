function fetchStudentInfo() {
    var serverCall = new ServerCall({
        url: "/Admin/GetSummaryOfStudentsApplied",
        params: JSON.stringify(), callType: "POST"
    })

    serverCall.xhrCall().then((result) => {
        if (result.data.length > 0) {
            let body = document.getElementById('tableBody');
            for (let student of result.data) {
                body.innerHTML += `
                    <tr>
                        <td>${student.NationalIdentityNumber}</td>
                        <td>${student.FirstName}</td>
                        <td>${student.LastName}</td>
                        <td>${student.TotalScore}</td>
                        <td>${student.ApplicationStatus}</td>
                    </tr>
                `
            }
        }
    });
}