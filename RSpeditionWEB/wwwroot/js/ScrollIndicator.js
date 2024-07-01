// When the user scrolls the page, execute myFunction
function ScrollIndicatorActivate() {
    window.onscroll = function () { ScrollIndicatorStart() };
}

function ScrollIndicatorStart() {

    var winScroll = document.body.scrollTop || document.documentElement.scrollTop;
    var height = document.documentElement.scrollHeight - document.documentElement.clientHeight;
    if (height == 0)
    {
        height = 1;
    }
    var scrolled = (winScroll / height) * 100;
    document.getElementById("myBar").style.width = scrolled + "%";
}