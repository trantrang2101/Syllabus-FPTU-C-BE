$(document).ready(() => {
    if (window.location.href.toLowerCase().includes("manager/course")) {
        onFilter(true);
    }
    if (window.location.href.toLowerCase().includes("course/list")) {
        onFilter();
        $("#classFilter,#subjectFilter,#teacherFilter,#termFilter").keyup(function (event) {
            if (event.keyCode === 13) {
                onFilter();
            }
        });
    }
    if (window.location.href.toLowerCase().includes("course/detail")) {
        console.log(GeneralManage.getParameterByName("id"));
        const callDetail = new Promise((resolve, reject) => {
            Manager.CourseManager.Detail(GeneralManage.getParameterByName("id"), resolve);
        })
        callDetail.then((resp) => {
            if (resp.code == "00") {
                GeneralManage.setAllFormValue("courseDetail", resp.data, false);
            }
        })
    }
});
function getFilter() {
    const filter = [];
    if ($("#classFilter") && $("#classFilter").val() && $("#classFilter").val().trim()) {
        filter.push(`contains(code,'${$("#classFilter").val()}')`)
    }
    if ($("#subjectFilter") && $("#subjectFilter").val() && $("#subjectFilter").val().trim()) {
        filter.push(`contains(name,'${$("#subjectFilter").val()}')`)
    }
    if ($("#teacherFilter") && $("#teacherFilter").val() && $("#teacherFilter").val().trim()) {
        filter.push(`contains(code,'${$("#teacherFilter").val()}')`)
    }
    if ($("#termFilter") && $("#termFilter").val() && $("#termFilter").val().trim()) {
        filter.push(`contains(name,'${$("#termFilter").val()}')`)
    }
    return (filter.length > 0 ? filter.join(" and ") : "");
}
function callListAPI(page, itemsPerPage, isManager) {
    const callAPI = new Promise((resolve, reject) => {
        Manager.CourseManager.GetAllList(page, itemsPerPage, getFilter(), resolve)
    });
    callAPI.then((response) => {
        if (response && response.code == "00") {
            GeneralManage.createTable(response.data.content, ["class.name", "subject.name", "teacher.name", "term.name"], page, itemsPerPage, "tableList", onSelect);
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
                    Manager.CourseManager.Detail(item.id, resolve);
                })
                callDetail.then((resp) => {
                    if (resp.code == "00") {
                        if (isManager) {
                            $('#btnDelete').prop('disabled', false);
                            GeneralManage.setAllFormValue("formData", resp.data);
                        } else {
                            $.ajax({
                                url: '/Course/SetSessionData',
                                type: 'POST',
                                data: { key: "Detail", value: JSON.stringify(resp.data) },
                                success: function (data) {
                                    window.location.href = "/Course/Detail?id=" + resp.data.id
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
                Manager.CourseManager.Delete($('[name="id"]').val(), resolve)
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
                    Manager.CourseManager.Update(GeneralManage.getAllFormValue('formData'), resolve)
                } else {
                    Manager.CourseManager.Add(GeneralManage.getAllFormValue('formData'), resolve)
                }
            });
            callSave.then((response) => {
                if (response && response.code == "00") {
                    onFilter(true);
                }
            });
        });

        const callClass = new Promise((resolve, reject) => {
            Manager.ClassManager.GetAllList(0, 1000000, "Status ne 0", resolve);
        });
        callClass.then((response) => {
            if (response && response.code == "00") {
                GeneralManage.createSelect(response.data.content, "id", "name", "class");
            }
        });

        const callAccount = new Promise((resolve, reject) => {
            Manager.AccountManager.GetAllList(0, 1000000, "roles/any(role: tolower(role/code) eq 'teacher') and Status ne 0", resolve);
        });
        callAccount.then((response) => {
            if (response && response.code == "00") {
                GeneralManage.createSelect(response.data.content, "id", "name", "teacher");
            }
        });



        const callSubject = new Promise((resolve, reject) => {
            Manager.SubjectManager.GetAllList(0, 1000000, "Status ne 0", resolve);
        });
        callSubject.then((response) => {
            if (response && response.code == "00") {
                GeneralManage.createSelect(response.data.content, "id", "name", "subject");
            }
        });


        const callTerm = new Promise((resolve, reject) => {
            Manager.TermManager.GetAllList(0, 1000000, "Status ne 0", resolve);
        });
        callTerm.then((response) => {
            if (response && response.code == "00") {
                GeneralManage.createSelect(response.data.content, "id", "name", "term");
            }
        });

        GeneralManage.createSelect([{ id: 1, name: "Kích hoạt" }, { id: 0, name: "Đóng" }], "id", "name", "status");
    }

    callListAPI(page, itemsPerPage, isManager);
}