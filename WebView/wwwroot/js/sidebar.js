$(document).ready(() => {
    const sidebar = document.getElementById('item0')
    if (sidebar) {
        const menu = GeneralManage.GetLocalStorage('sidebars');
        if (menu) {
            for (let i = 0; i < menu.length; i++) {
                const entity = menu[i];
                const header = document.createElement('div');
                header.innerHTML = entity.name;
                header.classList = 'sidenav-menu-heading';
                if (entity.children)
                    createChildSidebar(entity.children, '0' + i, header);
                sidebar.appendChild(header);
            }
        }
    }
});

function createChildSidebar(quickmenu, parent, currentNode) {
    for (let i = 0; i < quickmenu.length; i++) {
        const item = quickmenu[i];
        if (item.children && item.children.length > 0) {
            const a = sidebar.createElement('a');
            a.setAttribute('class', 'nav-link collapsed');
            a.setAttribute('href', 'javascript:void(0)');
            a.setAttribute('data-bs-toggle', 'collapse');
            a.setAttribute('aria-expanded', 'false');
            a.setAttribute('data-bs-target', `#${parent}${i+''}`);
            a.setAttribute('aria-controls', `${parent}${i+''}`);
            if (item.icon) {
                const div = a.createElement('div');
                div.classList = 'nav-link-icon';
                div.innerHTML = `<i class="${item.icon}"></i>`;
                a.appendChild(div);
            }
            const name = document.createElement('span');
            name.innerHTML = item.name;
            a.appendChild(name);
            currentNode.appendChild(a)
            const div = document.createElement('div');
            div.setAttribute('class', 'collapsed');
            div.setAttribute('id', `${parent}${i+''}`);
            div.setAttribute('data-bs-parent', `#${parent}`);

            const nav = div.createElement('nav');
            nav.classList = 'sidenav-menu-nested nav accordion'
            nav.setAttribute('id', `accordionSidenavPagesMenu`);
            div.appendChild(nav);
            createChildSidebar(item.children, parent + (i + ''), div)
            currentNode.appendChild(div);
        } else {
            const a = document.createElement('a');
            a.href = item.url;
            a.classList = 'nav-link';
            if (item.icon) {
                const div = document.createElement('div');
                div.classList = 'nav-link-icon';
                div.innerHTML = `<i class="${item.icon}"></i>`;
                a.appendChild(div);
            }
            const name = document.createElement('span');
            name.innerHTML = item.name;
            a.appendChild(name);
            currentNode.appendChild(a);
        }
    }
}