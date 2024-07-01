function openPage(ind, pageName, elmnt, color) {
    // Hide all elements with class="tabcontent" by default */
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Remove the background color of all tablinks/buttons
    tablinks = document.getElementsByClassName("tablink");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].style.backgroundColor = "";
    }
    // Шрифт активной вкладки черного цвета и  жирный
    tablinks[ind].style.color = "black";
    tablinks[ind].style.fontWeight = "bold";

    // Show the specific tab content
    document.getElementById(pageName).style.display = "block";

    // Add the specific color to the button used to open the tab content
    elmnt.style.backgroundColor = color;
}

function defaultOpenPage() {
    // Get the element with id="defaultOpen" and click on it
    document.getElementById("defaultOpen")?.click();
}

function someOpenPage(pageId) {
    // Get the element with id="defaultOpen" and click on it
    document.getElementById(pageId).click();
}

function someTabClick(tabRef) {
    tabRef.click();
}