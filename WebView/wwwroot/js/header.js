$(document).ready(() => {
    const info = GeneralManage.GetLocalStorage('info'), user = GeneralManage.GetLocalStorage('user');
    if (info && user) {
        console.log(info);
        document.querySelectorAll('.user-image').forEach(x => {
            x.src = info.picture
        })
        document.querySelectorAll('.user-details-name').forEach(x => {
            x.innerHTML = user.name;
        })
        document.querySelectorAll('.user-details-email').forEach(x => {
            x.innerHTML = user.email;
        })
    } else {
        logout();
    }
})
function logout() {
    localStorage.clear();
    window.location.href = '/Login'
}