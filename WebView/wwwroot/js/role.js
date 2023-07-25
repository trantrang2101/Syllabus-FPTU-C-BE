$(document).ready(() => {
    GeneralManage.createEditor('description');
    if (window.location.href.toLowerCase().includes("manager/role")) {
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
    return (filter.length > 0 ? filter.join(" and ") : "");
}
function callListAPI(page, itemsPerPage, isManager) {
    const callAPI = new Promise((resolve, reject) => {
        Manager.RoleManager.GetAllList(page, itemsPerPage, getFilter(), resolve)
    });
    callAPI.then((response) => {
        if (response && response.code == "00") {
            GeneralManage.createTable(response.data.content, ["code", "name"], page, itemsPerPage, "tableList", onSelect);
            GeneralManage.createPagination(page, response.data.totalCount, itemsPerPage, "tableList", onChangePage);

            function onChangePage(item) {
                callListAPI(item, itemsPerPage, isManager);
            }

            function onSelect(item) {
                if ($('#tableList tbody tr.table-primary')) {
                    $("#tableList tbody tr.table-primary").removeClass("table-primary");
                }
                if ($(`#tableList .row-${item.id}`)) {
                    console.log($(`.row-${item.id}`))
                    $(`#tableList .row-${item.id}`).addClass('table-primary');
                }
                $('#btnDelete').prop('disabled', false);
                GeneralManage.setAllFormValue("formData", item);
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
            const callDelete = new Promise((resolve, reject) => {
                Manager.RoleManager.Delete($('[name="id"]').val(), resolve)
            });
            callDelete.then((response) => {
                if (response && response.code == "00") {
                    onFilter(true);
                }
            });
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
                    Manager.RoleManager.Update(GeneralManage.getAllFormValue('formData'), resolve)
                } else {
                    Manager.RoleManager.Add(GeneralManage.getAllFormValue('formData'), resolve)
                }
            });
            callSave.then((response) => {
                if (response && response.code == "00") {
                    onFilter(true);
                }
            });
        });

        const callSidebar = new Promise((resolve, reject) => {
            Manager.SidebarManager.GetAllList(0, 1000000, "status ne 0", resolve)
        });
        callSidebar.then((response) => {
            if (response && response.code == "00") {
                GeneralManage.createTable(response.data.content, ["*", "icon", "name", "url"], 0, 1000000, "sidebarList", null, [`<input type='checkbox' name='sidebars' data-value='[0]'>`, `<i class="[0]"></i>`], false);
                $('#btnAdd').click();
            }
        });
        GeneralManage.createSelect([{ id: 1, name: "Kích hoạt" }, { id: 0, name: "Đóng" }], "id", "name", "status");
    }

    callListAPI(page, itemsPerPage, isManager);
}