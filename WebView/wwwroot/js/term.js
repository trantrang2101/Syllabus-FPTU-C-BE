$(document).ready(() => {
    if (window.location.href.toLowerCase().includes("manager/term")) {
        onFilter(true);
    }
});
function getFilter() {
    const filter = [];
    return (filter.length > 0 ? filter.join(" and "):"");
}
function callListAPI(page, itemsPerPage, isManager) {
    const callAPI = new Promise((resolve, reject) => {
        Manager.TermManager.GetAllList(page, itemsPerPage, getFilter(), resolve)
    });
    callAPI.then((response) => {
        if (response && response.code == "00") {
            GeneralManage.createTable(response.data.content.map((x) => ({ ...x, startDateShow: moment(x.startDate).format("DD/MM/YYYY"), endDateShow: moment(x.endDate).format("DD/MM/YYYY") })), ["startDateShow", "endDateShow", "name"], page, itemsPerPage, "tableList", onSelect);
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
                $('#btnDelete').prop('disabled', false);
                GeneralManage.setAllFormValue("formData", { ...item, startDate: moment(item.startDate).format("YYYY-MM-DD"), endDate : moment(item.endDate).format("YYYY-MM-DD") });
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
                Manager.TermManager.Delete($('[name="id"]').val(), resolve)
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
                    console.log("update");
                    Manager.TermManager.Update(GeneralManage.getAllFormValue('formData'), resolve)
                } else {
                    console.log("update");
                    const value = GeneralManage.getAllFormValue('formData');
                    delete value['id'];
                    Manager.TermManager.Add(value, resolve)
                }
                $('#btnAdd').click()
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