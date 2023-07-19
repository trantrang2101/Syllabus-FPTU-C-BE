$(document).ready(() => {
    if (window.location.href.toLowerCase().includes("curriculum/list")) {
        onFilter();
        $("#codeFilter,#nameFilter").keyup(function (event) {
            if (event.keyCode === 13) {
                onFilter();
            }
        });
    }
    if (window.location.href.toLowerCase().includes("manager/curriculum")) {
        onFilter(true);
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
    return (filter.length > 0 ? filter.join(" and "):"");
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
                callListAPI(item, itemsPerPage);
            }

            function onSelect(item) {
                if (isManager) {
                    $('#btnDelete').prop('disabled', false);
                    const callDetail = new Promise((resolve, reject) => {
                        Manager.CurriculumManager.Detail(item.id, resolve);
                    })
                    callDetail.then((resp) => {
                        if (resp.code == "00") {
                            GeneralManage.setAllFormValue("formData", resp.data);
                        }
                    })
                }
            }
        }
    })
}
function onFilter(isManager = false) {
    var page = 0, itemsPerPage = 20;
    if (isManager) {
        GeneralManage.createEditor('description');
        $('#btnDelete').prop('disabled', true);
        $('#btnDelete').on('click', () => {
            const callDelete = new Promise((resolve, reject) => {
                Manager.CurriculumManager.Delete($('[name="id"]').val(), resolve)
            });
            callDelete.then((response) => {
                if (response && response.code == "00") {
                    onFilter(true);
                }
            });
        })
        $('#btnAdd').on('click', () => {
            GeneralManage.setAllFormValue('formData', {});
            $('#btnDelete').prop('disabled', true);
        })
        $('#btnSave').on('click', () => {
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
        })
    }

    callListAPI(page, itemsPerPage, isManager);

    const callMajor = new Promise((resolve, reject) => {
        Manager.MajorManager.GetAllList(0, 1000000, "", resolve);
    });
    callMajor.then((response) => {
        if (response && response.code == "00") {
            GeneralManage.createSelect(response.data.content, "id", "name", "major");
        }
    });
}