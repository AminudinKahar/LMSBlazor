function PreventFormSubmissionWhenPressEnter(formId) {
    document.getElementById(`${formId}`).addEventListener("keydown", function (event) {

        // 13 = keyboard enter is pressed
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
}