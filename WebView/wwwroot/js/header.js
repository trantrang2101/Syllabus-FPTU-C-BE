$(document).ready(() => {
    const infoString = localStorage.getItem('info'), userString = localStorage.getItem('user');
    if (infoString && userString) {
        const info = JSON.parse(infoString), user = JSON.parse(userString);
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