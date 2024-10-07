// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const button = document.querySelector(".tap-top");
const displayButton = () => {
    window.addEventListener("scroll", () => {
        if (window.scrollY > 100) {
            button.style.display = "block";
        } else {
            button.style.display = "none";
        }

    });
};

const scrollToTop = () => {
    button.addEventListener("click", () => {
        window.scroll({
            top: 0,
            left: 0,
            behavior: "smooth"
        });
        console.log(window.scrollY)
    })
}

displayButton();
scrollToTop();