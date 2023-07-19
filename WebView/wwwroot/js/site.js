// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.addEventListener('DOMContentLoaded', event => {
    feather.replace();

    const sidebar = document.querySelector('#sidebarToggle');
    if (sidebar) {
        sidebar.addEventListener('change', () => {
            localStorage.setItem(
                'sidebar-toggle',
                document.body.classList.contains('sidenav-toggled')
            );
        })
    }
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    const tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    const popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    const popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });

    const stickyNav = document.body.querySelector('#stickyNav');
    if (stickyNav) {
        new bootstrap.ScrollSpy(document.body, {
            target: '#stickyNav',
            offset: 82,
        });
    }

    // Add active state to sidbar nav links
    let activatedPath = window.location.pathname.match(/([\w-]+\.html)/, '$1');

    if (activatedPath) {
        activatedPath = activatedPath[0];
    } else {
        activatedPath = 'index.html';
    }

    const targetAnchors = document.body.querySelectorAll('[href="' + activatedPath + '"].nav-link');

    if (targetAnchors) {
        targetAnchors.forEach(targetAnchor => {
            let parentNode = targetAnchor.parentNode;
            while (parentNode !== null && parentNode !== document.documentElement) {
                if (parentNode.classList.contains('collapse')) {
                    parentNode.classList.add('show');
                    const parentNavLink = document.body.querySelector(
                        '[data-bs-target="#' + parentNode.id + '"]'
                    );
                    parentNavLink.classList.remove('collapsed');
                    parentNavLink.classList.add('active');
                }
                parentNode = parentNode.parentNode;
            }
            targetAnchor.classList.add('active');
        });
    }
    setTimeout(() => {
        feather.replace();
    });
    const toastTrigger = document.getElementById('liveToastBtn')
    const toastLiveExample = document.getElementById('liveToast')

    if (toastTrigger) {
        const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample)
        toastTrigger.addEventListener('click', () => {
            toastBootstrap.show()
        })
    }
});