function PreventFormSubmissionWhenPressEnter(formId) {
    document.getElementById(`${formId}`).addEventListener("keydown", function (event) {

        // 13 = keyboard enter is pressed
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
}

$(document).ready(function () {
    $(".sidebar-link").on("click", function (e) {
        $(".sidebar-item").removeClass("selected");
        $(this).addClass("active");
    });
});
