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
                localStorage.setItem('sidebars', JSON.stringify([...new Set(GeneralManage.buildNested(response.data.sidebars.map((x) => ({ ...x, children: [] }))))]));
                localStorage.setItem('user', JSON.stringify(response.data));
                localStorage.setItem('info', JSON.stringify(googleUser));
                localStorage.setItem('authenticationToken', response.data.token);
                window.location.href='/'
            }
        }

        function onFailed(xhr, status, error) {
            console.error(error);
        }

    },
}