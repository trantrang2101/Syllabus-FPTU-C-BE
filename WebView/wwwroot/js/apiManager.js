﻿const editors = new Map();

var APIManager = {
    GetAPI: function (serviceUrl, successCallback) {
        $.ajax({
            type: "GET",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('authenticationToken')
            },
            contentType: "application/json",
            success: successCallback,
            error: function (xhr, status, err) {
                console.error(serviceUrl, status, err.toString());
            }
        });
    },
    PostAPI: function (serviceUrl, data, successCallback) {
        $.ajax({
            type: "POST",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('authenticationToken')
            },
            contentType: "application/json",
            data: JSON.stringify(data),
            success: successCallback,
            error: function (xhr, status, err) {
                console.error(serviceUrl, status, err.toString());
            }
        });
    },
    PutAPI: function (serviceUrl, data, successCallback) {
        $.ajax({
            type: "PUT",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('authenticationToken')
            },
            contentType: "application/json",
            data: JSON.stringify(data),
            success: successCallback,
            error: function (xhr, status, err) {
                console.error(serviceUrl, status, err.toString());
            }
        });
    },
    DeleteAPI: function (serviceUrl, successCallback) {
        $.ajax({
            type: "DELETE",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('authenticationToken')
            },
            success: successCallback,
            error: function (xhr, status, err) {
                console.error(serviceUrl, status, err.toString());
            }
        });
    }
};
var GeneralManage = {
    createEditor(name) {
        return ClassicEditor
            .create(document.querySelector(`[name=${name}]`))
            .then(editor => {
                editors[name] = editor;
            })
            .catch(err => console.error(err.stack));
    },
    setAllFormValue: (formId, object) => {
        $(`#${formId} [name]`).each(function () {
            if ($(this) && $(this).attr("name") && $(this).attr("name").length > 0) {
                if ($(this).is('ckeditor')) {
                    editors[$(this).attr("name")].setData(GeneralManage.ObjectByString(object, $(this).attr("name")) ? GeneralManage.ObjectByString(object, $(this).attr("name")) : "")
                } else {
                    $(this).val(GeneralManage.ObjectByString(object, $(this).attr("name")) ? GeneralManage.ObjectByString(object, $(this).attr("name")):"");
                }
            }
        });
        $('#' + formId).height($(window).height() - 410);
    },
    getAllFormValue : (formId)=> {
        const fieldPair = {}
        $(`#${formId} [name]`).each(function () {
            if ($(this) && $(this).attr("name") && $(this).attr("name").length > 0) {
                (GeneralManage.StringToObject(fieldPair,$(this).attr("name"), $(this).is('ckeditor') ? editors[$(this).attr("name")].getData() : $(this).val()))
            }
        });
        console.log(fieldPair)
        return fieldPair;
    },
    StringToObject: (result,inputString, value) => {
        const key = inputString.trim();

        const keys = key.split('.');

        const finalKey = keys.pop();

        let nestedObj = result;
        for (const nestedKey of keys) {
            nestedObj[nestedKey] = {};
            nestedObj = nestedObj[nestedKey];
        }

        nestedObj[finalKey] = parseFloat(value) ? parseFloat(value) : value;
        return result;
    },
    ObjectByString: (o, s)=> {
        s = s.replace(/\[(\w+)\]/g, '.$1'); // convert indexes to properties
        s = s.replace(/^\./, '');           // strip a leading dot
        var a = s.split('.');
        for (var i = 0, n = a.length; i < n; ++i) {
            var k = a[i];
            if (k in o) {
                o = o[k];
            } else {
                return;
            }
        }
        return o;
    },
    createSelect: (list, nameValue, nameDisplay, idName) => {
        const select = document.querySelector(`#${idName}`);
        if (list) {
            list.forEach((item, index) => {
                option = document.createElement("option");
                option.value = item[nameValue];
                option.innerHTML = GeneralManage.ObjectByString(item, nameDisplay);
                select.appendChild(option);
            });
        } else {
            select.innerHTML = '<option>Không tìm thấy</option>'
        }
    },
    createTable: (list, listObjectKey,page,itemsPerPage, idName, onClickRow) => {
        const divContainer = document.getElementById(idName);
        const tBody = divContainer.querySelector(`#${idName} table tbody`);
        tBody.innerHTML = ""
        if (list) {
            list.forEach((item, index) => {
                tr = tBody.insertRow(-1);
                td = document.createElement("td");
                td.classList = 'text-center'
                td.innerHTML = index + 1 + itemsPerPage * page;
                tr.appendChild(td);
                listObjectKey.forEach(key => {
                    td = document.createElement("td");
                    td.innerHTML = GeneralManage.ObjectByString(item, key);
                    tr.appendChild(td);
                });
                tr.addEventListener('click', () => {
                    onClickRow(item)
                });
                tBody.appendChild(tr);
            });
        } else {
            tBody.innerHTML = '<tr><td>Không tìm thấy</td></tr>'
        }
        $('#' + idName + ' .table-responsive').height($(window).height() - 350);
    },
    createPaginationItem: (content, parent,onClickItem,page=null) => {
        const li = document.createElement('li');
        li.innerHTML = `<a class="page-link" href="#">${content}</a>`;
        if (typeof content === "number" && page && page === (content - 1)) {
            li.classList = "page-item active";
        } else {
            li.classList = "page-item";
            li.addEventListener('click', onClickItem);
        }
        parent.appendChild(li);
    },
    createPagination: (page, total, itemsPerPage, idName, onClickItem) => {
        const divContainer = document.getElementById(idName);
        const pagination = divContainer.querySelector(`#${idName} .pagination`);
        pagination.innerHTML = "";
        const no = Math.ceil(total / itemsPerPage);
        if (page > 0) {
            GeneralManage.createPaginationItem('<span aria-hidden="true">&laquo;</span>', pagination, () => { onClickItem(page - 1) });
        }
        for (let i = 0; i < no; i++) {
            GeneralManage.createPaginationItem(i+1, pagination, () => { onClickItem(i) },page);
        }
        if (page < no - 1) {
            GeneralManage.createPaginationItem('<span aria-hidden="true">&raquo;</span>', pagination, () => { onClickItem(page + 1) });
        }
    },
}
var Manager = {
    CurriculumManager: {
        GetAllList: (page, itemsPerPage, filter,resolve) => {
            const url = `https://localhost:7124/api/Curriculum/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter:""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Detail: (id,resolve) => {
            const url = `https://localhost:7124/api/Curriculum/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Update: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Curriculum/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Add: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Curriculum/Add`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Delete: (id, resolve) => {
            const url = `https://localhost:7124/api/Curriculum/Delete/${id}`;
            APIManager.DeleteAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        }
    },
    MajorManager: {
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/Major/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        }
    }
}