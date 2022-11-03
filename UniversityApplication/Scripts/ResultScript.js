var numberOfSubjects = 1;

window.onload = Details();

function buildErrorMessage(select, data) {
    var option = document.createElement('OPTION');
    option.setAttribute('value', data);
    option.innerHTML = data;
    select.appendChild(option);

    return select;
}


function Details(/*e*/) {
    //e.preventDefault();
    var serverCall = new ServerCall({
        url: "/Subject/Index",
        params: JSON.stringify(), callType: "POST"
    })
    serverCall.xhrCall().then((result) => {
        if (result.data.length > 0) {
            var selectOne = document.createElement('SELECT');
            var selectTwo = document.createElement('SELECT');
            var selectThree = document.createElement('SELECT');
            selectOne.setAttribute('id', 'subOne');
            selectTwo.setAttribute('id', 'subTwo');
            selectThree.setAttribute('id', 'subThree');
            const subjectOne = document.getElementById("subjectOne");
            const subjectTwo = document.getElementById("subjectTwo");
            const subjectThree = document.getElementById("subjectThree");
            //errorPane.innerHTML = "";

            for (var i = 0; i < result.data.length; i++) {
                buildErrorMessage(selectOne, result.data[i]);
                buildErrorMessage(selectTwo, result.data[i]);
                buildErrorMessage(selectThree, result.data[i]);
            }

            subjectOne.appendChild(selectOne);
            subjectTwo.appendChild(selectTwo);
            subjectThree.appendChild(selectThree);
        }
        else {
            alert("1");
            //window.location = result.url;
        }
    });

    var serverCall = new ServerCall({
        url: "/Grade/Index",
        params: JSON.stringify(), callType: "POST"
    })
    serverCall.xhrCall().then((result) => {
        if (result.data.length > 0) {
            var selectOne = document.createElement('SELECT');
            var selectTwo = document.createElement('SELECT');
            var selectThree = document.createElement('SELECT');
            selectOne.setAttribute('id', 'gOne');
            selectTwo.setAttribute('id', 'gTwo');
            selectThree.setAttribute('id', 'gThree');
            const gradeOne = document.getElementById("gradeOne");
            const gradeTwo = document.getElementById("gradeTwo");
            const gradeThree = document.getElementById("gradeThree");
            //errorPane.innerHTML = "";

            for (var i = 0; i < result.data.length; i++) {
                buildErrorMessage(selectOne, result.data[i].Grade);
                buildErrorMessage(selectTwo, result.data[i].Grade);
                buildErrorMessage(selectThree, result.data[i].Grade);
            }

            //errorPane.appendChild(select);
            gradeOne.appendChild(selectOne);
            gradeTwo.appendChild(selectTwo);
            gradeThree.appendChild(selectThree);
        }
        else {
            //window.location = result.url;
        }
    });

}


function Results(e) {
    e.preventDefault();
    var SubjectNamesVar = [this.subOne.value, this.subTwo.value, this.subThree.value];
    var SubjectGradesVar = [this.gradeOne.value, this.gradeTwo.value, this.gradeThree.value];
    var ResultModel = {
        SubjectNames: SubjectNamesVar,
        Grades: SubjectGradesVar
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