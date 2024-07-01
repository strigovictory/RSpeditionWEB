function dragstart_handler(ev) {
    // Добавить id целевого элемента в объект передачи данных
    ev.dataTransfer.setData("application/my-app", ev.target.id);
    ev.dataTransfer.effectAllowed = "move";
}
function dragover_handler(ev) {
    ev.preventDefault();
    ev.dataTransfer.dropEffect = "move"
}

var GLOBAL = {};
GLOBAL.DotNetReference = null;
GLOBAL.SetDotnetReference = function (pDotNetReference) {
    GLOBAL.DotNetReference = pDotNetReference;
};

function drop_handler(ev) {
    ev.preventDefault();
    // Получить id целевого элемента и добавить перемещаемый элемент в его DOM
    const sourceId = ev.dataTransfer.getData("application/my-app");
    var targetId = ev.target.id;

    if (ev.target.localName != "th")
        if (ev.target.offsetParent.localName == "th")
            targetId = ev.target.offsetParent.id;
        else if (ev.target.offsetParent.offsetParent.localName == "th")
            targetId = ev.target.offsetParent.offsetParent.id;

    GLOBAL.DotNetReference.invokeMethodAsync("MoveColumnToNewPosition", new String(sourceId), new String(targetId));
}

function drop_handler_on_group_by_zone(ev) {
    ev.preventDefault();
    // Получить id целевого элемента и добавить перемещаемый элемент в его DOM
    const sourceId = ev.dataTransfer.getData("application/my-app");
    
    GLOBAL.DotNetReference.invokeMethodAsync("HandleEventOnDropZone", new String(sourceId));
}


function initializeFileDropZone(dropZoneElement, inputFile) {
    // Add a class when the user drags a file over the drop zone
    function onDragHover(e) {
        e.preventDefault();
        e.stopPropagation();
        dropZoneElement.classList.add("hover");
    }

    function onDragLeave(e) {
        e.preventDefault();
        e.stopPropagation();
        dropZoneElement.classList.remove("hover");
    }

    // Handle the paste and drop events
    function onDrop(e) {
        e.preventDefault();
        e.stopPropagation();
        dropZoneElement.classList.remove("hover");

        // Set the files property of the input element and raise the change event
        inputFile.files = e.dataTransfer.files;
        const event = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(event);
    }

    function onPaste(e) {
        // Set the files property of the input element and raise the change event
        inputFile.files = e.clipboardData.files;
        const event = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(event);
    }

    // Register all events
    dropZoneElement.addEventListener("dragenter", onDragHover);
    dropZoneElement.addEventListener("dragover", onDragHover);
    dropZoneElement.addEventListener("dragleave", onDragLeave);
    dropZoneElement.addEventListener("drop", onDrop);
    dropZoneElement.addEventListener('paste', onPaste);
}

function initializeGroupByDropZone(dropZoneElement, destination) {
    // Add a class when the user drags a file over the drop zone
    function onDragHover(e) {
        e.preventDefault();
        e.stopPropagation();
        dropZoneElement.classList.add("hover");
    }

    function onDragLeave(e) {
        e.preventDefault();
        e.stopPropagation();
        dropZoneElement.classList.remove("hover");
    }

    // Handle the paste and drop events
    function onDrop(e) {
        e.preventDefault();
        dropZoneElement.classList.remove("hover");

        const event = new Event('change', { bubbles: true });
        destination.dispatchEvent(event);
    }

    // Register all events
    dropZoneElement.addEventListener("dragenter", onDragHover);
    dropZoneElement.addEventListener("dragover", onDragHover);
    dropZoneElement.addEventListener("dragleave", onDragLeave);
    dropZoneElement.addEventListener("drop", onDrop);
}
