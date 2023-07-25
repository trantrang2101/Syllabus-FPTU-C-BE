const editors = new Map();

var APIManager = {
    ErrorCatch: (jqXHR, textStatus, errorThrown) =>{
        // Check for the 401 Unauthorized status code
        if (jqXHR.status === 401) {
            console.log('Unauthorized: You need to log in or provide valid credentials.');
            localStorage.clear();
            window.location.href = '/Login';
        } else {
            window.location.href = "/Error" + jqXHR.status
        }
    },
    GetAPI: function (serviceUrl, successCallback) {
        $.ajax({
            type: "GET",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + GeneralManage.GetLocalStorage('authenticationToken')
            },
            contentType: "application/json",
            success: successCallback,
            error: APIManager.ErrorCatch
        });
    },
    PostAPI: function (serviceUrl, data, successCallback) {
        $.ajax({
            type: "POST",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + GeneralManage.GetLocalStorage('authenticationToken')
            },
            contentType: "application/json",
            data: JSON.stringify(data),
            success: successCallback,
            error: APIManager.ErrorCatch
        });
    },
    PutAPI: function (serviceUrl, data, successCallback) {
        $.ajax({
            type: "PUT",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + GeneralManage.GetLocalStorage('authenticationToken')
            },
            contentType: "application/json",
            data: JSON.stringify(data),
            success: successCallback,
            error: APIManager.ErrorCatch
        });
    },
    DeleteAPI: function (serviceUrl, successCallback) {
        $.ajax({
            type: "DELETE",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + GeneralManage.GetLocalStorage('authenticationToken')
            },
            success: successCallback,
            error: APIManager.ErrorCatch
        });
    }
};
var GeneralManage = {
    GetLocalStorage: (key) => {
        if (localStorage.getItem(key)) {
            try {
                return JSON.parse(localStorage.getItem(key));
            } catch {
                return localStorage.getItem(key);
            }
        }
        return null;
    },
    buildNested: (arr, parentId = 0,parentProperty = "parent") => {
        if (arr && arr.length > 0) {
            let result = [];
            const list = arr.filter((x) => (x[parentProperty] ? x[parentProperty].id : 0) === parentId);
            if (list.length > 0) {
                for (let item of list) {
                    let children = GeneralManage.buildNested(arr, item.id, parentProperty);
                    if (children.length) {
                        item.children = children;
                    } else {
                        delete item.children;
                    }
                    delete item[parentProperty];
                    result.push({ ...item, expand: false });
                }
            }
            return result;
        }
        return [];
    },
    createEditor(name) {
        return ClassicEditor
            .create(document.querySelector(`[name=${name}]`))
            .then(editor => {
                editors[name] = editor;
            })
            .catch(err => console.error(err.stack));
    },
    setAllFormValue: (formId, object, isSetHeight = true) => {
        $(`#${formId} [name]`).each(function () {
            if ($(this) && $(this).attr("name") && $(this).attr("name").length > 0) {
                const value = GeneralManage.ObjectByString(object, $(this).attr("name"));
                if ($(this).is('ckeditor')) {
                    editors[$(this).attr("name")].setData(value !== null && value !== undefined ? value : "")
                } else if ($(this).is('div')) {
                    $(this).html(value !== null && value !== undefined ? value : $(this).first().val()).change();;
                } else if ($(this).is('select')) {
                    $(this).val(value !== null && value !== undefined ? value : $(this).first().val()).change();;
                } else if ($(this).is('[type="checkbox"]') || $(this).is('[type="radio"]')) {
                    if (value !== null && value !== undefined && typeof value === 'object' && Array.isArray(value)) {
                        const check = value.some(x => x.id === $(this).data('value').id);
                        $(this).prop("checked", check);
                    } else {
                        $(this).prop("checked", false);
                    }
                } else {
                    const value = GeneralManage.ObjectByString(object, $(this).attr("name"));
                    $(this).val(value !== null && value !== undefined ? value : "").change();;
                }
            }
        });
        if (isSetHeight) {
            $('#' + formId).height($(window).height() - 350);
        }
    },
    getAllFormValue: (formId) => {
        const fieldPair = {}
        $(`#${formId} [name]`).each(function () {
            if ($(this) && $(this).attr("name") && $(this).attr("name").length > 0) {
                if ($(this).is('ckeditor'))
                    (GeneralManage.StringToObject(fieldPair, $(this).attr("name"), editors[$(this).attr("name")].getData()))
                else if ($(this).attr('type') === 'date' || $(this).hasClass("datepicker"))
                    (GeneralManage.StringToObject(fieldPair, $(this).attr("name"), $(this).val()));
                else if ($(this).attr('type') === 'checkbox' || ($(this).attr('type') === 'radio')) {
                    if ($(this).is(':checked')) {
                        GeneralManage.StringToObject(fieldPair, $(this).attr("name"), $(this).data('value'),true);
                    }
                }
                else 
                    GeneralManage.StringToObject(fieldPair, $(this).attr("name"), parseFloat($(this).val()) ? parseFloat($(this).val()) : $(this).val());
            }
        });
        return fieldPair;
    },
    StringToObject: (result, inputString, value, isCheckBox=false) => {
        if (!value) {
            return;
        }
        const key = inputString.trim();

        const keys = key.split('.');

        const finalKey = keys.pop();

        let nestedObj = result;
        for (const nestedKey of keys) {
            nestedObj[nestedKey] = {};
            nestedObj = nestedObj[nestedKey];
        }
        if (nestedObj[finalKey]) {
            if (Array.isArray(nestedObj[finalKey])) {
                nestedObj[finalKey] = [...nestedObj[finalKey]]
            } else {
                nestedObj[finalKey] = [nestedObj[finalKey]]
            }
            nestedObj[finalKey].push(value);
        } else {
            if (isCheckBox) {
                nestedObj[finalKey] = [value];
            } else {
                nestedObj[finalKey] = value;
            }
        }
        return result;
    },
    ObjectByString: (o, s) => {
        if (s === "*") {
            return JSON.stringify(o);
        }
        s = s.replace(/\[(\w+)\]/g, '.$1'); // convert indexes to properties
        s = s.replace(/^\./, '');           // strip a leading dot
        var a = s.split('.');
        for (var i = 0, n = a.length; i < n; ++i) {
            var k = a[i];
            if (o[k] != null && o[k] != undefined && k in o) {
                o = o[k];
            } else {
                return;
            }
        }
        return o;
    },
    getParameterByName:(name, url = window.location.href) => {
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
    },
    createSelect: (list, nameValue, nameDisplay, idName,onChange = null) => {
        const select = document.querySelector(`#${idName}`);
        select.innerHTML = ""
        if (list) {
            list.forEach((item, index) => {
                option = document.createElement("option");
                option.value = item[nameValue];
                option.innerHTML = GeneralManage.ObjectByString(item, nameDisplay);
                select.appendChild(option);
            });
            if (onChange) {
                select.addEventListener('change', onChange);
            }
        } else {
            select.innerHTML = '<option>Không tìm thấy</option>'
        }
    },
    createTable: (list, listObjectKey, page, itemsPerPage, idName, onClickRow, stringsFormat = [], isStt = true) => {
        const divContainer = document.getElementById(idName);
        const tBody = divContainer.querySelector(`#${idName} table tbody`);
        tBody.innerHTML = ""
        if (list) {
            list.forEach((item, index) => {
                tr = tBody.insertRow(-1);
                tr.classList = `row-${item.id}`
                if (isStt) {
                    td = document.createElement("td");
                    td.classList = `text-center`
                    td.innerHTML = index + 1 + itemsPerPage * page;
                    tr.appendChild(td);
                }
                listObjectKey.forEach((key,index) => {
                    td = document.createElement("td");
                    td.innerHTML = stringsFormat && stringsFormat[index] ? stringsFormat[index].replace(/\[\d+\]/g, match => {
                        const index = parseInt(match.slice(1, -1));
                        return GeneralManage.ObjectByString(item, key);
                    }) : GeneralManage.ObjectByString(item, key);;
                    tr.appendChild(td);
                });
                if (onClickRow) {
                    tr.addEventListener('click', () => {
                        onClickRow(item)
                    });
                }
                tBody.appendChild(tr);
            });
        } else {
            tBody.innerHTML = '<tr><td>Không tìm thấy</td></tr>'
        }
        $('#' + idName + ' .table-responsive').height($(window).height() - 275);
    },
    createPaginationItem: (content, parent, onClickItem, page = null) => {
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
            GeneralManage.createPaginationItem(i + 1, pagination, () => { onClickItem(i) }, page);
        }
        if (page < no - 1) {
            GeneralManage.createPaginationItem('<span aria-hidden="true">&raquo;</span>', pagination, () => { onClickItem(page + 1) });
        }
    },
}
var Manager = {
    Login: (email) => {
        const url = `https://localhost:7124/api/Account/Login?gmail=${email}`;
        APIManager.GetAPI(url, onSuccess);

        function onSuccess(response) {
            if (response.code == "00") {
                console.log(response.data)
                const filterList = [...new Set(response.data.sidebars.map(JSON.stringify))].map(JSON.parse);
                localStorage.setItem('sidebars', JSON.stringify([...new Set(GeneralManage.buildNested(filterList.map((x) => ({ ...x, children: [] }))))]));
                localStorage.setItem('user', JSON.stringify(response.data));
                localStorage.setItem('term', JSON.stringify(response.data.currentTerm));
            }
        }
    },
    CurriculumManager: {
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/Curriculum/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Detail: (id, resolve) => {
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
    TermManager: {
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/Term/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Detail: (id, resolve) => {
            const url = `https://localhost:7124/api/Term/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Update: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Term/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Add: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Term/Add`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Delete: (id, resolve) => {
            const url = `https://localhost:7124/api/Term/Delete/${id}`;
            APIManager.DeleteAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        }
    },
    GradeDetailManager: {
        Export: (courseId,resolve) => {
            const url = `https://localhost:7124/api/GradeDetail/Export?courseId=${courseId}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/GradeDetail/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },
        UpdateAll: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/GradeDetail/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },
    },
    StudentCourseManager: {
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/StudentCourse/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Detail: (id, resolve) => {
            const url = `https://localhost:7124/api/StudentCourse/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Update: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/StudentCourse/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Add: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/StudentCourse/Add`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Delete: (id, resolve) => {
            const url = `https://localhost:7124/api/StudentCourse/Delete/${id}`;
            APIManager.DeleteAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        }
    },
    DepartmentManager: {
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/Department/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Detail: (id, resolve) => {
            const url = `https://localhost:7124/api/Department/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Update: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Department/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Add: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Department/Add`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Delete: (id, resolve) => {
            const url = `https://localhost:7124/api/Department/Delete/${id}`;
            APIManager.DeleteAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        }
    },
    SidebarManager: {
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/Sidebar/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Detail: (id, resolve) => {
            const url = `https://localhost:7124/api/Sidebar/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Update: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Sidebar/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Add: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Sidebar/Add`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Delete: (id, resolve) => {
            const url = `https://localhost:7124/api/Sidebar/Delete/${id}`;
            APIManager.DeleteAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        }
    },
    RoleManager: {
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/Role/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Detail: (id, resolve) => {
            const url = `https://localhost:7124/api/Role/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Update: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Role/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Add: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Role/Add`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Delete: (id, resolve) => {
            const url = `https://localhost:7124/api/Role/Delete/${id}`;
            APIManager.DeleteAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        }
    },
    SubjectManager: {
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/Subject/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Detail: (id, resolve) => {
            const url = `https://localhost:7124/api/Subject/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Update: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Subject/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Add: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Subject/Add`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Delete: (id, resolve) => {
            const url = `https://localhost:7124/api/Subject/Delete/${id}`;
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
        },
        Detail: (id, resolve) => {
            const url = `https://localhost:7124/api/Major/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Update: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Major/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Add: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Major/Add`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Delete: (id, resolve) => {
            const url = `https://localhost:7124/api/Major/Delete/${id}`;
            APIManager.DeleteAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        }
    },
    AccountManager: {
        CheckPassword: (url, resolve) => {
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/Account/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Detail: (id, resolve) => {
            const url = `https://localhost:7124/api/Account/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Update: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Account/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Add: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Account/Add`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Delete: (id, resolve) => {
            const url = `https://localhost:7124/api/Account/Delete/${id}`;
            APIManager.DeleteAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        }
    },
    AssessmentManager: {
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/Assessment/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Detail: (id, resolve) => {
            const url = `https://localhost:7124/api/Assessment/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Update: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Assessment/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Add: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Assessment/Add`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Delete: (id, resolve) => {
            const url = `https://localhost:7124/api/Assessment/Delete/${id}`;
            APIManager.DeleteAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        }
    },
    CategoryManager: {
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/Category/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Detail: (id, resolve) => {
            const url = `https://localhost:7124/api/Category/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Update: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Category/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Add: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Category/Add`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Delete: (id, resolve) => {
            const url = `https://localhost:7124/api/Category/Delete/${id}`;
            APIManager.DeleteAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        }
    },
    ClassManager: {
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/Class/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Detail: (id, resolve) => {
            const url = `https://localhost:7124/api/Class/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Update: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Class/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Add: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Class/Add`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Delete: (id, resolve) => {
            const url = `https://localhost:7124/api/Class/Delete/${id}`;
            APIManager.DeleteAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        }
    },
    CourseManager: {
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/Course/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Detail: (id, resolve) => {
            const url = `https://localhost:7124/api/Course/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Update: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Course/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Add: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Course/Add`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Delete: (id, resolve) => {
            const url = `https://localhost:7124/api/Course/Delete/${id}`;
            APIManager.DeleteAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        }
    },
    CurriculumDetailManager: {
        Detail: (id, resolve) => {
            const url = `https://localhost:7124/api/CurriculumDetail/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },
    },
    DepartmentManager: {
        GetAllList: (page, itemsPerPage, filter, resolve) => {
            const url = `https://localhost:7124/api/Department/List?$top=${itemsPerPage}&$skip=${page * itemsPerPage}${filter ? "&$filter=" + filter : ""}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Detail: (id, resolve) => {
            const url = `https://localhost:7124/api/Department/Detail/${id}`;
            APIManager.GetAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        },

        Update: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Department/Update`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Add: (objectValue, resolve) => {
            const url = `https://localhost:7124/api/Department/Add`;
            APIManager.PostAPI(url, objectValue, onSuccess)

            function onSuccess(response) {
                resolve(response)
            }
        },

        Delete: (id, resolve) => {
            const url = `https://localhost:7124/api/Department/Delete/${id}`;
            APIManager.DeleteAPI(url, onSuccess);

            function onSuccess(response) {
                resolve(response)
            }
        }
    }
}