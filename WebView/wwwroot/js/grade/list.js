$(document).ready(() => {
    if (document.getElementById('passwordIcon')) {
        document.getElementById('passwordIcon').addEventListener('click', () => {
            if (document.querySelector('#passwordIcon i').classList.contains("fa-eye-slash")) {
                document.querySelector('#passwordIcon i').classList = "fa-regular fa-eye";
                document.getElementById('password').type = "text";
            } else {
                document.querySelector('#passwordIcon i').classList = "fa-regular fa-eye-slash";
                document.getElementById('password').type = "password";
            }
        })
    }
    const user = GeneralManage.GetLocalStorage("user");
    const term = GeneralManage.GetLocalStorage("term");
    if (user && term) {
        const callAPI = new Promise((resolve, reject) => {
            Manager.CourseManager.GetAllList(0, 100000, `Teacher/Id eq ${user.id} and Term/Id eq ${term.id}`, resolve)
        });
        callAPI.then((response) => {
            if (response && response.code == "00") {
                const listSubject = response.data.content
                    .reduce((a, b) => {
                        const found = a.find((e) => e.subject.id === b.subject.id);
                        return (
                            found
                                ? found.courses.push({ ...b, courseId: b.id })
                                : a.push({
                                    subject: b.subject,
                                    courses: [{ ...b, courseId: b.id }],
                                }),
                            a
                        );
                    }, [])
                    .map((x) => ({ ...x.subject, ...x }));
                console.log(listSubject)
                createListLink(listSubject, "<strong>[0]</strong> - [1]", ["code", "name"], "listSemester", onClickTerm);
                function onClickTerm(item) {
                    if ($('#listSemester a.bg-primary-subtle')) {
                        $("#listSemester a.bg-primary-subtle").removeClass("bg-primary-subtle");
                    }
                    if ($(`.row-listSemester-${item.id}`)) {
                        $(`.row-listSemester-${item.id}`).addClass('bg-primary-subtle');
                    }
                    createListLink(item.courses, "<strong>[0]</strong>", ["class.code"], "listCourse", onClickDetail)
                    function onClickDetail(item) {
                        if ($('#listCourse a.bg-primary-subtle')) {
                            $("#listCourse a.bg-primary-subtle").removeClass("bg-primary-subtle");
                        }
                        if ($(`.row-listCourse-${item.id}`)) {
                            $(`.row-listCourse-${item.id}`).addClass('bg-primary-subtle');
                        }
                        $('#btnPassword').click()
                        //window.location.href = "/Grade/Detail?id=" + item.id;
                    }
                }
            }
        })
    }
});
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