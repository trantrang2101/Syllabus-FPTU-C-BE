$(document).ready(() => {
    if (window.location.href.toLowerCase().includes("grade") || window.location.href.toLowerCase().includes("grade/index")) {
        const userString = localStorage.getItem("user");
        if (userString) {
            const user = JSON.parse(userString);
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
                    createListLink(terms,"[0] ([1] - [2])", ["term.name", "term.startDate", "term.endDate"], "listSemester", onClickTerm);
                    function onClickTerm(item) {
                        createListLink(listCourse[item.term.name] ,"<strong>[0] ([1])</strong> ([2])", ["subject.name", "subject.code","class.code"],"listCourse",null)
                    }
                }
            })
        }
    }
});
function createListLink(list, stringFormat , keys, idName, onClick) {
    const divContainer = document.getElementById(idName);
    divContainer.innerHTML = "";
    for (let item of list) {
        const a = document.createElement("a");
        a.classList = `list-group-item list-group-item-action text-uppercase row-${idName}-${item.id}`;
        a.innerHTML = stringFormat.replace(/\[\d+\]/g, match => {
            const index = parseInt(match.slice(1, -1));
            return GeneralManage.ObjectByString(item, keys[index]);
        });
        a.addEventListener('click', () => {
            onClick(item)
        });
        divContainer.appendChild(a);
    }
}