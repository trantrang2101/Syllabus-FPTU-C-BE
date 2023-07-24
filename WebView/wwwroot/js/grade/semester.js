$(document).ready(() => {
    const user = GeneralManage.GetLocalStorage("user");
    const term = GeneralManage.GetLocalStorage("term");
    if (user && term) {
        const callAPI = new Promise((resolve, reject) => {
            Manager.StudentCourseManager.GetAllList(0, 100000, "Student/Id eq " + user.id, resolve)
        });
        callAPI.then((response) => {
            if (response && response.code == "00") {
                const terms = response.data.content
                    .map((x) => ({ ...x.course, studentCourseId: x.id }))
                    .reduce((a, b) => {
                        const found = a.find((e) => e.term.id === b.term.id);
                        return (
                            found
                                ? found.courses.push({ ...b })
                                : a.push({
                                    term: b.term,
                                    courses: [{ ...b, studentCourseId: b.studentCourseId }],
                                }),
                            a
                        );
                    }, []);
                const listCourse = terms.reduce((map, val) => {
                    if (!map[String(val.term.name)]) {
                        map[String(val.term.name)] = [];
                    }
                    map[String(val.term.name)].push(...val.courses);
                    return map;
                }, {});
                createListLink(terms.map(x => x.term), "[0] ([1] - [2])", ["name", "startDate", "endDate"], "listSemester", onClickTerm);
                function onClickTerm(item) {
                    if ($('#listSemester a.bg-primary-subtle')) {
                        $("#listSemester a.bg-primary-subtle").removeClass("bg-primary-subtle");
                    }
                    if ($(`.row-listSemester-${item.id}`)) {
                        $(`.row-listSemester-${item.id}`).addClass('bg-primary-subtle');
                    }
                    createListLink(listCourse[item.name], "<strong>[0] ([1])</strong> ([2])", ["subject.name", "subject.code", "class.code"], "listCourse", onClickDetail)
                    function onClickDetail(item) {
                        console.log(item);
                        if ($('#listCourse a.bg-primary-subtle')) {
                            $("#listCourse a.bg-primary-subtle").removeClass("bg-primary-subtle");
                        }
                        if ($(`.row-listCourse-${item.id}`)) {
                            $(`.row-listCourse-${item.id}`).addClass('bg-primary-subtle');
                        }
                        const callDetail = new Promise((resolve, reject) => {
                            Manager.GradeDetailManager.GetAllList(0, 100000, "StudentCourse/Id eq " + item.studentCourseId, resolve)
                        });
                        callDetail.then((response) => {
                            if (response && response.code == "00") {
                                console.log(response.data.content);
                                const listMark = [];
                                const data = response.data.content
                                    .map((x) => ({
                                        ...x,
                                        assessment: {
                                            ...x.gradeGeneral.assessment,
                                            weight: x.gradeGeneral.weight,
                                        },
                                    }))
                                    .map((x) => ({ ...x, category: { ...x.assessment.category } }))
                                    .reduce((a, b) => {
                                        const found = a.find((e) => e.category.id === b.category.id);
                                        return (
                                            found
                                                ? found.assessment.push({
                                                    ...b.assessment,
                                                    mark: b.mark,
                                                    comment: b.comment,
                                                })
                                                : a.push({
                                                    category: b.category,
                                                    assessment: [
                                                        { ...b.assessment, mark: b.mark, comment: b.comment },
                                                    ],
                                                }),
                                            a
                                        );
                                    }, []);
                                data.forEach((category) => {
                                    const totalWeight = category.assessment.reduce(
                                        (total, item) => total + item.weight,
                                        0
                                    );
                                    const totalMark = category.assessment.reduce(
                                        (total, item) => total + item.mark,
                                        0
                                    );
                                    const averageMark = totalMark / category.assessment.length;
                                    category.assessment.push({
                                        name: 'Total',
                                        weight: totalWeight,
                                        mark: averageMark ? averageMark.toFixed(2) : null,
                                        comment: null,
                                    });
                                    listMark.push(category);
                                });
                                createMarkTable(listMark,'listMark')
                            }
                        });
                    }
                }
            }
        })
    }
});
function createMarkTable(list, idName) {
    const divContainer = document.getElementById(idName);
    const tBody = divContainer.querySelector(`#${idName} table tbody`);
    tBody.innerHTML = ""
    if (list) {
        list.forEach((item) => {
            item.assessment.forEach((entity, index) => {
                tr = tBody.insertRow(-1);
                if (index === 0) {
                    td = document.createElement("td");
                    td.classList = "fw-bold";
                    td.setAttribute('rowSpan', item.assessment.length);
                    td.innerHTML = item.category.name;
                    tr.appendChild(td);
                }
                td = document.createElement("td");
                td.classList = "fw-semibold";
                td.innerHTML = entity.name;
                tr.appendChild(td);
                td = document.createElement("td");
                td.innerHTML = entity.weight;
                tr.appendChild(td);
                td = document.createElement("td");
                td.innerHTML = entity.mark;
                tr.appendChild(td);
                td = document.createElement("td");
                td.innerHTML = entity.comment;
                tr.appendChild(td);
                tBody.appendChild(tr);
            })
        });
    } else {
        tBody.innerHTML = '<tr><td>Không tìm thấy</td></tr>'
    }
}
function createListLink(list, stringFormat, keys, idName, onClick) {
    const divContainer = document.getElementById(idName);
    divContainer.innerHTML = "";
    for (let item of list) {
        const a = document.createElement("a");
        a.classList = `list-group-item list-group-item-action text-uppercase row-${idName}-${item.id}`;
        a.innerHTML = stringFormat.replace(/\[\d+\]/g, match => {
            const index = parseInt(match.slice(1, -1));
            return GeneralManage.ObjectByString(item, keys[index]);
        });
        if (onClick) {
            a.addEventListener('click', () => {
                onClick(item)
            });
        }
        divContainer.appendChild(a);
    }
}