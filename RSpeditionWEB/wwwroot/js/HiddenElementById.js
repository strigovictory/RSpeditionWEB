function hiddenElementById(itemID) {
    // Toggle visibility between none and '' 
    if ((document.getElementById(itemID).style.display == 'none')) {
        document.getElementById(itemID).style.display = 'block'
        event.preventDefault()
    } else {
        document.getElementById(itemID).style.display = 'none';
        event.preventDefault()
    }
}