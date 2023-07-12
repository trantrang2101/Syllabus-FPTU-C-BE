
var APIManager = {
    GetAPI: function (serviceUrl, successCallback, errorCallback) {
        $.ajax({
            type: "GET",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            },
            dataType: "json",
            success: successCallback,
            error: errorCallback
        });
    },
    PostAPI: function (serviceUrl, data, successCallback, errorCallback) {
        $.ajax({
            type: "POST",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            },
            contentType: "application/json",
            data: JSON.stringify(data),
            success: successCallback,
            error: errorCallback
        });
    },
    PutAPI: function (serviceUrl, data, successCallback, errorCallback) {
        $.ajax({
            type: "PUT",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            },
            contentType: "application/json",
            data: JSON.stringify(data),
            success: successCallback,
            error: errorCallback
        });
    },
    DeleteAPI: function (serviceUrl, successCallback, errorCallback) {
        $.ajax({
            type: "DELETE",
            url: serviceUrl,
            headers: {
                Authorization: 'Bearer ' + token
            },
            success: successCallback,
            error: errorCallback
        });
    }
};
