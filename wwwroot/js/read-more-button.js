$(document).ready(function () {
    $(".read-more-btn").click(function () {
        const textElement = $(this).prev(".card-text");
        textElement.toggleClass("expanded");
        $(this).text(textElement.hasClass("expanded") ? "Read Less" : "Read More");
    });
});
