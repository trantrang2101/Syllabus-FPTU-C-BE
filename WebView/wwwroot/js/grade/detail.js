$(document).ready(() => {
    const callAPI = new Promise((resolve, reject) => {
        const user = GeneralManage.GetLocalStorage('user'), term = GeneralManage.GetLocalStorage('term');
        Manager.CourseManager.GetAllList(0, 10000000, `Teacher/Id eq ${user.id} and Term/Id eq ${term.id}`, resolve);
    });
    callAPI.then((response) => {
        if (response && response.code == "00") {
            const listCourse = response.data.content.map(x => ({ ...x, classCode: x.class.code + " - " + x.subject.code }));
            GeneralManage.createSelect(listCourse, "id", "classCode", "listCourse");
            console.log(GeneralManage.getParameterByName("id"));
            $('#listCourse').on('change', () => {
                const callDetail = new Promise((resolve, reject) => {
                    Manager.GradeDetailManager.GetAllList(0, 10000000, `StudentCourse/Course/Id eq ${$('#listCourse').val()}`, resolve);
                });
                callDetail.then((res) => {
                    if (res && res.code == "00") {
                        const assessments = convertListAssesst(res.data.content);
                        const listStudent = convertListStudent(res.data.content);
                        const listMatrix = convertListGradeToMatrix(
                            res.data.content,
                            assessments,
                            listStudent
                        );
                        const listAssessment = assessments.reduce((a, b) => {
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
                        console.log(assessments);
                    }
                })
            });
            if (GeneralManage.getParameterByName("id")) {
                $('#listCourse').val(GeneralManage.getParameterByName("id"));
            }
        }
    });

});
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