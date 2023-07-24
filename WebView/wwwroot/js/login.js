function onSignIn(response) {
    var tokens = response.credential.split(".");
    var payload = JSON.parse(atob(tokens[1]));
    LoginManager.Login(payload)
}
var LoginManager = {
    Login: (googleUser) => {
        const url = `https://localhost:7124/api/Account/Login?gmail=${googleUser.email}`;
        APIManager.GetAPI(url, onSuccess);

        function onSuccess(response) {
            if (response.code == "00") {
                console.log(response.data)
                const filterList = [...new Set(response.data.sidebars.map(JSON.stringify))].map(JSON.parse);
                localStorage.setItem('sidebars', JSON.stringify([...new Set(GeneralManage.buildNested(filterList.map((x) => ({ ...x, children: [] }))))]));
                localStorage.setItem('user', JSON.stringify(response.data));
                localStorage.setItem('info', JSON.stringify(googleUser));
                localStorage.setItem('authenticationToken', response.data.token);
                localStorage.setItem('term', JSON.stringify(response.data.currentTerm));
                window.location.href = '/'
            }
        }
    },
}