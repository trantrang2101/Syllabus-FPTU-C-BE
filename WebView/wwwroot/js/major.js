$(document).ready(() => {
    if (window.location.href.toLowerCase().includes("manager/major")) {
        onFilter(true);
    }
    if (window.location.href.toLowerCase().includes("major/list")) {
        onFilter();
        $("#codeFilter,#nameFilter").keyup(function (event) {
            if (event.keyCode === 13) {
                onFilter();
            }
        });
    }
    if (window.location.href.toLowerCase().includes("major/detail")) {
        console.log(GeneralManage.getParameterByName("id"));
        const callDetail = new Promise((resolve, reject) => {
            Manager.MajorManager.Detail(GeneralManage.getParameterByName("id"), resolve);
        })
        callDetail.then((resp) => {
            if (resp.code == "00") {
                GeneralManage.setAllFormValue("majorDetail", resp.data, false);
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
        Manager.MajorManager.GetAllList(page, itemsPerPage, getFilter(), resolve)
    });
    callAPI.then((response) => {
        if (response && response.code == "00") {
            GeneralManage.createTable(response.data.content, ["code", "name"], page, itemsPerPage, "tableList", onSelect);
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
                    Manager.MajorManager.Detail(item.id, resolve);
                })
                callDetail.then((resp) => {
                    if (resp.code == "00") {
                        if (isManager) {
                            $('#btnDelete').prop('disabled', false);
                            GeneralManage.setAllFormValue("formData", resp.data);
                        } else {
                            $.ajax({
                                url: '/Major/SetSessionData',
                                type: 'POST',
                                data: { key: "Detail", value: JSON.stringify(resp.data) },
                                success: function (data) {
                                    window.location.href = "/Major/Detail?id=" + resp.data.id
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
            var old_element = document.getElementById("btnDelete");
            var new_element = old_element.cloneNode(true);
            old_element.parentNode.replaceChild(new_element, old_element);
            var myModal = new bootstrap.Modal(document.getElementById('deleteConfirmModal'));
            myModal.show();
        })
        $('#btnDeleteConfirm').on('click', () => {
            const callDelete = new Promise((resolve, reject) => {
                Manager.MajorManager.Delete($('[name="id"]').val(), resolve)
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
            var old_element = document.getElementById("btnAdd");
            var new_element = old_element.cloneNode(true);
            old_element.parentNode.replaceChild(new_element, old_element);
            GeneralManage.setAllFormValue('formData', {});
            $('#btnDelete').prop('disabled', true);
        })
        $('#btnSave').click(function (e) {
            var old_element = document.getElementById("btnSave");
            var new_element = old_element.cloneNode(true);
            old_element.parentNode.replaceChild(new_element, old_element);
            const callSave = new Promise((resolve, reject) => {
                if ($('[name="id"]').val()) {
                    Manager.MajorManager.Update(GeneralManage.getAllFormValue('formData'), resolve)
                } else {
                    Manager.MajorManager.Add(GeneralManage.getAllFormValue('formData'), resolve)
                }
            });
            callSave.then((response) => {
                if (response && response.code == "00") {
                    onFilter(true);
                }
            });
        });

        GeneralManage.createSelect([{ id: 1, name: "Kích hoạt" }, { id: 0, name: "Đóng" }], "id", "name", "status");
    }

    callListAPI(page, itemsPerPage, isManager);
}