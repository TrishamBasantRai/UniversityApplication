var numberOfSubjectsOnScreen = 3;

function Details(/*e*/) {
    var numberOfSubjects = 1;
    let body = document.getElementById('tableBody');

    while (numberOfSubjects < 4) {
        body.innerHTML += `
            <tr id="row${numberOfSubjects}">
                <td>Subject ${numberOfSubjects}</td>
                <td id="subjectCol${numberOfSubjects}"></td>
                <td id="gradeCol${numberOfSubjects}"></td>
            </tr>`
        numberOfSubjects++;
    }

    var serverCall = new ServerCall({
        url: "/Subject/Index",
        params: JSON.stringify(), callType: "POST"
    })

    serverCall.xhrCall().then((result) => {
        if (result.data.length > 0) {
            numberOfSubjects = 1;

            while (numberOfSubjects < 4) {
                var subjectVariable = document.getElementById(`subjectCol${numberOfSubjects}`);
                var html = `<select id="subject${numberOfSubjects}">`

                for (let subject of result.data) {
                    html += `<option value="${subject}">${subject}</option>`
                }

                html += `</select>`
                subjectVariable.innerHTML = html;
                numberOfSubjects++;
            }
        }
        else {
            alert("1");
            //window.location = result.url;
        }
    });

    var serverCall = new ServerCall({
        url: "/Grade/Index",
        params: JSON.stringify(), callType: "POST"
    });

    serverCall.xhrCall().then((result) => {
        if (result.data.length > 0) {
            numberOfSubjects = 1;

            while (numberOfSubjects < 4) {
                var gradeVariable = document.getElementById(`gradeCol${numberOfSubjects}`);
                var html = `<select id="grade${numberOfSubjects}">`

                for (let grade of result.data) {
                    html += `<option value="${grade.Grade}">${grade.Grade}</option>`
                }

                html += `</select>`
                gradeVariable.innerHTML = html;
                numberOfSubjects++;
            }
        }
        else {
            //window.location = result.url;
        }
    });
}


function Results(e) {
    e.preventDefault();

    var SubjectNamesValue = [];
    var SubjectGradesValue = [];

    for (var i = 1; i <= numberOfSubjectsOnScreen; i++) {
        var subjectNameEl = document.getElementById(`subject${i}`);
        var subjectGradeEl = document.getElementById(`grade${i}`);

        SubjectNamesValue.push(subjectNameEl.value);
        SubjectGradesValue.push(subjectGradeEl.value);
    }

    var ResultModel = {
        SubjectNames: SubjectNamesValue,
        Grades: SubjectGradesValue
    }
    var serverCall = new ServerCall({
        url: "/Result/Index",
        params: JSON.stringify(ResultModel), callType: "POST"
    })
    serverCall.xhrCall().then((result) => {
        if (result) {
            window.location = result.url;
        }
    });

}

function Remove() {
    if (numberOfSubjectsOnScreen == 1) {
        alert("Minimum number of subjects reached!");
    }
    else {
        var subjectTobeRemoved = document.getElementById(`row${numberOfSubjectsOnScreen}`);
        subjectTobeRemoved.style.display = 'none';
        numberOfSubjectsOnScreen--;
    }

}

function Add() {
    if (numberOfSubjectsOnScreen == 3) {
        alert("Maximum number of subjects reached!");
    }
    else {
        numberOfSubjectsOnScreen++;
        var subjectToBeAdded = document.getElementById(`row${numberOfSubjectsOnScreen}`);
        subjectToBeAdded.style.display = '';
    }
}
