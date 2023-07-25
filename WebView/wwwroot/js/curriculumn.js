$(document).ready(() => {
    if (window.location.href.toLowerCase().includes("manager/curriculum")) {
        onFilter(true);
    }else if (window.location.href.toLowerCase().includes("curriculum/list")) {
        onFilter();
        $("#codeFilter,#nameFilter").keyup(function (event) {
            if (event.keyCode === 13) {
                onFilter();
            }
        });
    }else if (window.location.href.toLowerCase().includes("curriculum/detail")) {
        console.log(GeneralManage.getParameterByName("id"));
        const callDetail = new Promise((resolve, reject) => {
            Manager.CurriculumManager.Detail(GeneralManage.getParameterByName("id"), resolve);
        })
        callDetail.then((resp) => {
            if (resp.code == "00") {
                if ($('.page-header-subtitle') && $('.page-header-subtitle').length>0) {
                    GeneralManage.setAllFormValue("curriculumDetail", resp.data, false);
                    GeneralManage.createTable([...resp.data.curriculumDetails].sort((a, b) => a.semester - b.semester), ["semester", "subject.code", "subject.name", "credit"], 0, 10000000, "detailList", onSelect);
                    function onSelect(item) {
                        console.log(item)
                        const callDetail = new Promise((resolve, reject) => {
                            Manager.CurriculumDetailManager.Detail(item.id, resolve);
                        })
                        callDetail.then((resp) => {
                            if (resp.code == "00") {
                                $.ajax({
                                    url: '/Curriculum/SetSessionData',
                                    type: 'POST',
                                    data: { key: "Subject", value: JSON.stringify(item) },
                                    success: function (data) {
                                        window.location.href = "/Curriculum/Subject?id=" + item.id
                                    },
                                    error: function (xhr, status, error) {
                                        // Handle error if needed
                                        console.error("Error setting session data:", error);
                                    }
                                });
                            }
                        })
                    }
                } else {
                    $.ajax({
                        url: '/Curriculum/SetSessionData',
                        type: 'POST',
                        data: { key: "Detail", value: JSON.stringify(resp.data) },
                        success: function (data) {
                            window.location.href = "/Curriculum/Detail?id=" + resp.data.id
                        },
                        error: function (xhr, status, error) {
                            // Handle error if needed
                            console.error("Error setting session data:", error);
                        }
                    });
                }
            }
        })
    } else if (window.location.href.toLowerCase().includes("curriculum/subject")) {
        console.log(GeneralManage.getParameterByName("id"));
        const callDetail = new Promise((resolve, reject) => {
            Manager.CurriculumDetailManager.Detail(GeneralManage.getParameterByName("id"), resolve);
        })
        callDetail.then((resp) => {
            if (resp.code == "00") {
                if ($('.page-header-subtitle') && $('.page-header-subtitle').length > 0) {
                    GeneralManage.setAllFormValue("curriculumDetail", resp.data, false);
                    const list = resp.data.gradeGenerals.map((x) => ({
                        ...x,
                        assessment: {
                            ...x.assessment,
                            weight: x.weight,
                            minMark: x.minMark
                        },
                    }))
                        .map((x) => ({ ...x, category: { ...x.assessment.category } }))
                        .reduce((a, b) => {
                            const found = a.find((e) => e.category.id === b.category.id);
                            return (
                                found
                                    ? found.assessment.push({
                                        ...b.assessment,
                                    })
                                    : a.push({
                                        category: b.category,
                                        assessment: [
                                            { ...b.assessment },
                                        ],
                                    }),
                                a
                            );
                        }, []);
                    createMarkTable(list,"detailList")
                } else {
                    $.ajax({
                        url: '/Curriculum/SetSessionData',
                        type: 'POST',
                        data: { key: "Subject", value: JSON.stringify(resp.data) },
                        success: function (data) {
                            window.location.href = "/Curriculum/Subject?id=" + resp.data.id
                        },
                        error: function (xhr, status, error) {
                            // Handle error if needed
                            console.error("Error setting session data:", error);
                        }
                    });
                }
            }
        })
    }
});
function getFilter() {
    const filter = [];
    if ($("#codeFilter") && $("#codeFilter").val() && $("#codeFilter").val().trim()) {
        filter.push(`contains(code,'${$("#codeFilter").val()}')`)
    }
    if ($("#nameFilter") && $("#nameFilter").val() && $("#nameFilter").val().trim()) {
        filter.push(`contains(name,'${$("#nameFilter").val()}')`)
    }
    return (filter.length > 0 ? filter.join(" and ") : "");
}
function callListAPI(page, itemsPerPage, isManager) {
    const callAPI = new Promise((resolve, reject) => {
        Manager.CurriculumManager.GetAllList(page, itemsPerPage, getFilter(), resolve)
    });
    callAPI.then((response) => {
        if (response && response.code == "00") {
            GeneralManage.createTable(response.data.content, ["major.name", "code", "name"], page, itemsPerPage, "tableList", onSelect);
            GeneralManage.createPagination(page, response.data.totalCount, itemsPerPage, "tableList", onChangePage);

            function onChangePage(item) {
                callListAPI(item, itemsPerPage, isManager);
            }

            function onSelect(item) {
                if ($('tbody tr.table-primary')) {
                    $("tbody tr.table-primary").removeClass("table-primary");
                }
                if ($(`.row-${item.id}`)) {
                    console.log($(`.row-${item.id}`))
                    $(`.row-${item.id}`).addClass('table-primary');
                }
                const callDetail = new Promise((resolve, reject) => {
                    Manager.CurriculumManager.Detail(item.id, resolve);
                })
                callDetail.then((resp) => {
                    if (resp.code == "00") {
                        if (isManager) {
                            $('#btnDelete').prop('disabled', false);
                            GeneralManage.setAllFormValue("formData", resp.data);
                        } else {
                            $.ajax({
                                url: '/Curriculum/SetSessionData',
                                type: 'POST',
                                data: { key: "Detail", value: JSON.stringify(resp.data) },
                                success: function (data) {
                                    window.location.href = "/Curriculum/Detail?id=" + resp.data.id
                                },
                                error: function (xhr, status, error) {
                                    // Handle error if needed
                                    console.error("Error setting session data:", error);
                                }
                            });
                        }
                    }
                })
            }
        }
    })
}
function onFilter(isManager = false) {
    var page = 0, itemsPerPage = 20;
    if (isManager) {
        GeneralManage.createEditor('description');
        $('#btnDelete').prop('disabled', true);
        $('#btnDelete').click(function (e) {
            const old_element = document.getElementById("btnDelete");
            const new_element = old_element.cloneNode(true);
            old_element.parentNode.replaceChild(new_element, old_element);
            var myModal = new bootstrap.Modal(document.getElementById('deleteConfirmModal'));
            myModal.show();
        })
        $('#btnDeleteConfirm').on('click', () => {
            const callDelete = new Promise((resolve, reject) => {
                Manager.CurriculumManager.Delete($('[name="id"]').val(), resolve)
            });
            callDelete.then((response) => {
                if (response && response.code == "00") {
                    onFilter(true);
                }
            });
            var myModal = new bootstrap.Modal(document.getElementById('deleteConfirmModal'));
            myModal.hide();
        })
        $('#btnAdd').click(function (e) {
            if ($('tbody tr.table-primary')) {
                $("tbody tr.table-primary").removeClass("table-primary");
            }
            const old_element = document.getElementById("btnAdd");
            const new_element = old_element.cloneNode(true);
            old_element.parentNode.replaceChild(new_element, old_element);
            GeneralManage.setAllFormValue('formData', {});
            $('#btnDelete').prop('disabled', true);
        })
        $('#btnSave').click(function (e) {
            const old_element = document.getElementById("btnSave");
            const new_element = old_element.cloneNode(true);
            old_element.parentNode.replaceChild(new_element, old_element);
            const callSave = new Promise((resolve, reject) => {
                if ($('[name="id"]').val()) {
                    Manager.CurriculumManager.Update(GeneralManage.getAllFormValue('formData'), resolve)
                } else {
                    Manager.CurriculumManager.Add(GeneralManage.getAllFormValue('formData'), resolve)
                }
            });
            callSave.then((response) => {
                if (response && response.code == "00") {
                    onFilter(true);
                }
            });
        });

        const callMajor = new Promise((resolve, reject) => {
            Manager.MajorManager.GetAllList(0, 1000000, "Status ne 0", resolve);
        });
        callMajor.then((response) => {
            if (response && response.code == "00") {
                GeneralManage.createSelect(response.data.content, "id", "name", "major");
            }
        });
        GeneralManage.createSelect([{ id: 1, name: "Kích hoạt" }, { id: 0, name: "Đóng" }], "id", "name", "status");
    }

    callListAPI(page, itemsPerPage, isManager);
}

function createMarkTable(list, idName) {
    const divContainer = document.getElementById(idName);
    const tBody = divContainer.querySelector(`#${idName} table tbody`);
    tBody.innerHTML = ""
    if (list) {
        list.forEach((item) => {
            item.assessment.forEach((entity, index) => {
                tr = tBody.insertRow(-1);
                if (index === 0) {
                    console.log(item);
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
                td.classList = "text-center";
                tr.appendChild(td);
                td = document.createElement("td");
                td.classList = "text-center";
                td.innerHTML = entity.minMark;
                tr.appendChild(td);
                tBody.appendChild(tr);
            })
        });
    } else {
        tBody.innerHTML = '<tr><td>Không tìm thấy</td></tr>'
    }
}