function onSignIn(response) {
    var tokens = response.credential.split(".");
    var payload = JSON.parse(atob(tokens[1]));
    console.log(payload)
    LoginManager.Login(payload)
}
var LoginManager = {
    Login: (googleUser) => {
        const url = `https://localhost:7124/api/Account/Login?gmail=${googleUser.email}`;
        APIManager.GetAPI(url, onSuccess, onFailed);

        function onSuccess(response) {
            if (response.code == "00") {
                console.log(response.data);
                localStorage.setItem('sidebars', JSON.stringify([...new Set(buildNested(response.data.sidebars.map((x) => ({ ...x, children: [] }))))]));
                localStorage.setItem('user', JSON.stringify(response.data));
                localStorage.setItem('info', JSON.stringify(googleUser));
                localStorage.setItem('authenticationToken', response.data.token);
                window.location.href='/'
            }
        }

        function onFailed(xhr, status, error) {
            console.error(error);
        }
        function buildNested(arr, parentId = 0) {
            if (arr && arr.length > 0) {
                let result = [];
                const list = arr.filter((x) => (x.parent ? x.parent.id : 0) === parentId);
                console.log(list);
                if (list.length > 0) {
                    for (let item of list) {
                        let children = buildNested(arr, item.id);
                        if (children.length) {
                            item.children = children;
                        } else {
                            delete item.children;
                        }
                        delete item.parent;
                        result.push({ ...item, expand: false });
                    }
                }
                return result;
            }
            return [];
        }

    },
}