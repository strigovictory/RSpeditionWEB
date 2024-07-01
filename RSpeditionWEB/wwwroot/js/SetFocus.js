function SetFocus (element) {
    if (element instanceof HTMLElement) {
        element.focus();
    }
};

function SetFocusById(elementId) {
    var element = document.getElementById(elementId);
    element.focus();
};

function OnClickById(elementId) {
    var element = document.getElementById(elementId);
    element.click();
};