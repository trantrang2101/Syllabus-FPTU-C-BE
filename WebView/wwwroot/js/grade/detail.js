let listStudent, listAssessment, listMatrix, assessments;
$(document).ready(() => {
    const callAPI = new Promise((resolve, reject) => {
        const user = GeneralManage.GetLocalStorage('user'), term = GeneralManage.GetLocalStorage('term');
        Manager.CourseManager.GetAllList(0, 10000000, `Teacher/Id eq ${user.id} and Term/Id eq ${term.id}`, resolve);
    });
    callAPI.then((response) => {
        if (response && response.code == "00") {
            const listCourse = response.data.content.map(x => ({ ...x, classCode: x.class.code + " - " + x.subject.code }));
            GeneralManage.createSelect(listCourse, "id", "classCode", "listCourse");
            $('#listCourse').on('change', () => {
                onChange();
            });
            setTimeout(() => {
                if (GeneralManage.getParameterByName("id")) {
                    $('#listCourse').val(GeneralManage.getParameterByName("id"));
                    onChange();
                }
            })
        }
    });

});
function createListAssessment(idName) {
    const divContainer = document.getElementById(idName);
    divContainer.innerHTML = "";
    if (divContainer) {
        listAssessment.forEach((x, i) => {
            const label = document.createElement('label');
            label.classList = 'h6 mb-2';
            label.innerHTML = x.category.name;
            divContainer.appendChild(label);
            if (x.assessment) {
                x.assessment.forEach(assessment => {
                    const div = document.createElement('div');
                    div.classList = 'form-check mb-2';
                    div.innerHTML = `<input class="form-check-input" id="assessment${assessment.id}" type="checkbox" onclick="onCheck(${i},${assessment.id})" checked="${assessment.enable}"/>
                                <label class="form-check-label" for="assessment${assessment.id}">${assessment.name}</label>`;
                    divContainer.appendChild(div);
                })
            }
        })
    }
} 
function onCheck(i, id) {
    const index = listAssessment[i].assessment.findIndex(
        (x) => x.id == id
    ), indexCommon = assessments.findIndex((x) => x.id == id);
    listAssessment[i].assessment[index].enable = !listAssessment[i].assessment[index].enable;
    listAssessment[i].enable = !listAssessment[i].assessment.every(
        (item) => !item.enable
    );
    assessments[indexCommon].enable = listAssessment[i].assessment[index].enable;
    console.log(assessments[indexCommon]);
    console.log(listAssessment[i].assessment[index].enable);
    createTable();
}
function getEnableAssessment(assessment) {
    return assessment.filter((x) => x.enable).length;
}
function createTable()
{
    console.log("--------------------------------------------")
    const table = document.querySelector('#listStudent table');
    table.innerHTML = "";
    const tHead = document.createElement('thead'), tBody = document.createElement('tbody');
    let tr, td, th;
    //-----Create thead #1
    tr = tHead.insertRow(-1);
    th = document.createElement("th");
    th.classList = `text-center align-middle`
    th.innerHTML = "Họ tên sinh viên";
    th.setAttribute('rowSpan', 2);
    tr.appendChild(th);
    th = document.createElement("th");
    th.classList = `text-center align-middle`
    th.innerHTML = "Mã số sinh viên";
    th.setAttribute('rowSpan', 2);
    tr.appendChild(th);
    listAssessment.forEach((item) => {
        if (item.enable) {
            th = document.createElement("th");
            th.classList = `text-center align-middle`
            th.setAttribute('rowSpan', item.assessment.length > 1 ? 1 : 2);
            th.setAttribute('colSpan', getEnableAssessment(item.assessment));
            console.log(getEnableAssessment(item.assessment));
            th.innerHTML = item.category.name;
            tr.appendChild(th);
        }
    })
    tHead.appendChild(tr);
    //-----Create thead #2
    tr = tHead.insertRow(-1);
    listAssessment.forEach((item) => {
        item.assessment.forEach((assessment) => {
            if (item.assessment.length > 1 && assessment.enable) {
                th = document.createElement("th");
                th.classList = `text-center align-middle`
                th.innerHTML = `${assessment.name}`;
                tr.appendChild(th);
            }
        })
    })
    //Create tbody
    listStudent.forEach((item, i) => {
        tr = tBody.insertRow(-1);
        td = document.createElement("td");
        td.innerHTML = item.name;
        td.classList = `align-middle`
        tr.appendChild(td);
        td = document.createElement("td");
        td.innerHTML = item.code;
        td.classList = `align-middle`
        tr.appendChild(td);
        listMatrix[i].forEach((entity, j) => {
            if (assessments[j].enable) {
                td = document.createElement("td");
                td.innerHTML = `<input type="hidden" value="${j}">
                               <input type="number" value="${entity.mark}" min="0" max="10" id="mark${entity.id}" class="form-control" onchange="checkValidateMark(${i},${j})">`;
                tr.appendChild(td);
            }
        })
    })
    tHead.appendChild(tr);
    table.appendChild(tHead);
    table.appendChild(tBody);
}
function checkValidateMark(i, j) {
    console.log(listMatrix[i][j])
    if (listMatrix[i][j].mark < 0 || listMatrix[i][j].mark > 10) {
        listMatrix[i][j].mark = listMatrix[i][j].oldMark;
        $('#mark' + listMatrix[i][j].id).val(listMatrix[i][j].mark);
    } else {
        listMatrix[i][j].mark = $('#mark' + listMatrix[i][j].id).val();
    }
    listMatrix[i][j].changes = listMatrix[i][j].mark !== listMatrix[i][j].oldMark;
}
function onChange() {
    const callDetail = new Promise((resolve, reject) => {
        Manager.GradeDetailManager.GetAllList(0, 10000000, `StudentCourse/Course/Id eq ${$('#listCourse').val()}`, resolve);
    });
    callDetail.then((res) => {
        if (res && res.code == "00") {
            assessments = convertListAssesst(res.data.content);
            console.log(assessments);
            listStudent = convertListStudent(res.data.content);
            console.log(listStudent);
            listMatrix = convertListGradeToMatrix(
                res.data.content,
                assessments,
                listStudent
            );
            console.log(listMatrix);
            listAssessment = assessments.reduce((a, b) => {
                const found = a.find((e) => e.category.id === b.category.id);
                return (
                    found
                        ? found.assessment.push({
                            enable: true,
                            ...b,
                        })
                        : a.push({
                            category: b.category,
                            enable: true,
                            assessment: [
                                {
                                    ...b,
                                    enable: true,
                                },
                            ],
                        }),
                    a
                );
            }, []);
            createListAssessment('listAssessment');
            createTable();
            console.log(listAssessment);
        }
    })
}
function convertListAssesst(listValue) {
    return listValue
        .map((item) => ({
            ...item.gradeGeneral.assessment,
            enable: true,
        }))
        .filter(
            (value, index, self) =>
                self.findIndex((t) => t.id === value.id) === index
        );
}
function convertListStudent(listValue) {
    return listValue
        .map((item) => item.studentCourse.student)
        .filter(
            (value, index, self) =>
                self.findIndex((t) => t.id === value.id) === index
        );
}
function convertListGradeToMatrix(
    listValue,
    assessments = [],
    listStudent = []
) {
    if (assessments.length == 0)
        assessments = convertListAssesst(listValue);
    if (listStudent.length == 0)
        listStudent = convertListStudent(listValue);
    const data = listValue.map((x) => ({
        id: x.id,
        mark: x.mark,
        comment: x.comment,
        assessment: x.gradeGeneral.assessment,
        student: x.studentCourse.student,
        studentCourse: { id: x.studentCourse.id },
        gradeGeneral: { id: x.gradeGeneral.id },
        status: x.status,
    }));
    let listMatrix = [];
    if (
        listStudent &&
        Array.isArray(listStudent) &&
        assessments &&
        Array.isArray(listStudent)
    ) {
        listStudent.forEach((item, i) => {
            if (!listMatrix[i]) {
                listMatrix[i] = new Array(assessments.length);
            }
            assessments.forEach((assessment, index) => {
                const score = data.find(
                    (s) =>
                        s.assessment.id === assessment.id && s.student.id === item.id
                );
                listMatrix[i][index] = {
                    changes: false,
                    oldMark: score?.mark,
                    ...score,
                };
            });
        });
    }
    return listMatrix;
}
function onSave() {
    const changeArray= [];
    listMatrix.forEach((row) => {
        row.forEach((cell) => {
            if (cell.changes) {
                changeArray.push(cell);
            }
        });
    });
    if (changeArray.length > 0) {
        const value = changeArray.map((x) => ({
            id: x.id,
            gradeGeneralId: x.gradeGeneral.id,
            studentCourseId: x.studentCourse.id,
            mark: parseFloat(x.mark),
            status: x.status,
        }));
        console.log(value);
    }
    console.log(changeArray);
}