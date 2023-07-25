$(document).ready(() => {
    if (window.location.href.toLowerCase().includes("manager/sidebar")) {
        onFilter(true);
    }
    if (window.location.href.toLowerCase().includes("sidebar/list")) {
        onFilter();
        $("#urlFilter,#nameFilter").keyup(function (event) {
            if (event.keyCode === 13) {
                onFilter();
            }
        });
    }
    if (window.location.href.toLowerCase().includes("Sidebar/Detail")) {
        if (localStorage.getItem("detail")) {
            const sidebar = JSON.parse(localStorage.getItem("detail"));
            console.log(sidebar)
            $(".page-header-title span").innerHTML = sidebar.name;
            //$(".page-header-subtitle").innerHTML = sidebar.description;
            localStorage.removeItem("detail");
        } else {
            window.history.back();
        }
    }
});
function getFilter() {
    const filter = [];
    if ($("#urlFilter") && $("#urlFilter").val() && $("#urlFilter").val().trim()) {
        filter.push(`contains(code,'${$("#urlFilter").val()}')`)
    }
    if ($("#nameFilter") && $("#nameFilter").val() && $("#nameFilter").val().trim()) {
        filter.push(`contains(name,'${$("#nameFilter").val()}')`)
    }
    return (filter.length > 0 ? filter.join(" and ") : "");
}
function callListAPI(page, itemsPerPage, isManager) {
    const callAPI = new Promise((resolve, reject) => {
        Manager.SidebarManager.GetAllList(page, itemsPerPage, getFilter(), resolve)
    });
    callAPI.then((response) => {
        if (response && response.code == "00") {
            GeneralManage.createTable(response.data.content, ["icon", "name", "url"], page, itemsPerPage, "tableList", onSelect);
            GeneralManage.createPagination(page, response.data.totalCount, itemsPerPage, "tableList", onChangePage);

            function onChangePage(item) {
                callListAPI(item, itemsPerPage, isManager);
            }

            function onSelect(item) {
                console.log(item);
                if ($('#tableList tbody tr.table-primary')) {
                    $("#tableList tbody tr.table-primary").removeClass("table-primary");
                }
                if ($(`#tableList .row-${item.id}`)) {
                    console.log($(`#tableList .row-${item.id}`))
                    $(`#tableList .row-${item.id}`).addClass('table-primary');
                }
                const callDetail = new Promise((resolve, reject) => {
                    Manager.SidebarManager.Detail(item.id, resolve);
                })
                callDetail.then((resp) => {
                    if (resp.code == "00") {
                        if (isManager) {
                            $('#btnDelete').prop('disabled', false);
                            GeneralManage.setAllFormValue("formData", resp.data);
                        } else {
                            localStorage.setItem("detail", JSON.stringify(resp.data));
                            window.location.href = "/Sidebar/Detail"
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
                Manager.SidebarManager.Delete($('[name="id"]').val(), resolve)
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
            var old_element = document.getElementById("btnAdd");
            var new_element = old_element.cloneNode(true);
            old_element.parentNode.replaceChild(new_element, old_element);
            if ($('tbody tr.table-primary')) {
                $("tbody tr.table-primary").removeClass("table-primary");
            }
            GeneralManage.setAllFormValue('formData', {});
            $('#btnDelete').prop('disabled', true);
        })
        $('#btnSave').click(function (e) {
            var old_element = document.getElementById("btnSave");
            var new_element = old_element.cloneNode(true);
            old_element.parentNode.replaceChild(new_element, old_element);
            const callSave = new Promise((resolve, reject) => {
                if ($('[name="id"]').val()) {
                    Manager.SidebarManager.Update(GeneralManage.getAllFormValue('formData'), resolve)
                } else {
                    Manager.SidebarManager.Add(GeneralManage.getAllFormValue('formData'), resolve)
                }
            });
            callSave.then((response) => {
                if (response && response.code == "00") {
                    onFilter(true);
                }
            });
        });
        const callSidebar = new Promise((resolve, reject) => {
            Manager.SidebarManager.GetAllList(0, 1000000, "Status ne 0", resolve);
        });
        callSidebar.then((response) => {
            if (response && response.code == "00") {
                GeneralManage.createSelect(response.data.content, "id", "name", "parent");
            }
        });
        const callRoles = new Promise((resolve, reject) => {
            Manager.RoleManager.GetAllList(0, 1000000, "Status ne 0", resolve);
        });
        callRoles.then((response) => {
            if (response && response.code == "00") {
                GeneralManage.createTable(response.data.content.map(role => ({ ...role, sidebars: role.roleSidebars })), ["*", "code", "name"], 0, 1000000, "roleList", null, [`<input type='checkbox' name='roles' data-value='[0]'>`], false);
            }
        });
        GeneralManage.createSelect([{ id: 1, name: "Kích hoạt" }, { id: 0, name: "Đóng" }], "id", "name", "status");
    }
    callListAPI(page, itemsPerPage, isManager);
}