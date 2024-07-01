function GetSelectedValues(sel) {
    var results = [];
    var i;

    for (i = 0; i < sel.options.length; i++) {
        if (sel.options[i]) {
            sel.options[i].style.backgoundColor = "white";
            sel.options[i].style.color = "black";
            sel.options[i].style.fontWeight = "normal";
        }
    }

    for (i = 0; i < sel.options.length; i++) {
        if (sel.options[i].selected) {
            results[results.length] = parseInt(sel.options[i].value);
            sel.options[i].style.color = "black";
            sel.options[i].style.fontWeight = "bold";
        }
    }
    return results;
};

function GetSelectedStringValues(sel) {
    var results = [];
    var i;
    if (sel != null && sel.options != null)
    {
        for (i = 0; i < sel.options.length; i++) {
            if (sel.options[i]) {
                sel.options[i].style.backgroundColor = "white";
                sel.options[i].style.color = "black";
                sel.options[i].style.fontWeight = "normal";
            }
        }

        for (i = 0; i < sel.options.length; i++) {
            if (sel.options[i].selected) {
                results[results.length] = sel.options[i].value;
                sel.options[i].style.color = "black";
                sel.options[i].style.fontWeight = "bold";
            }
        }
    }
    return results;
};

// Функция для сброса в теге select всех option (установка во всех opt свойства selected = false)
function ResetAllSelectedValues(sel) {
    for (i = 0; i < sel.options.length; i++) {
        sel.options[i].selected = false;
        sel.options[i].style.fontWeight = "normal";
    }
};

// Функция для сброса в теге select всех option (установка во всех opt свойства selected = false)
function ResetAllSelectedValuesById(selectId) {
    var selectItem = document.getElementById(selectId);
    for (i = 0; i < selectItem?.options?.length ?? 0; i++) {
        selectItem.options[i].selected = false;
        selectItem.options[i].style.fontWeight = "normal";
    }
};

// Функция для сброса в теге select выбранного option (установка в выбранном opt свойства selected = false)
function ResetSelectedValues(sel, opt) {
    var array = [];

    for (i = 0; i < sel.options?.length ?? 0; i++) {
        array[array.length] = sel.options[i];
    }

    var finfOpt = array.find(_ => parseInt(_.value) == opt);
    var ind = array.indexOf(finfOpt);
    if (ind > -1)
    {
        sel.options[ind].selected = false;
        sel.options[ind].style.fontWeight = "normal";
    }
};

// Функция для сброса в теге select выбранного option (установка в выбранном opt свойства selected = false) - для value типа string
function ResetSelectedStringValues(sel, opt) {
    var array = [];

    for (i = 0; i < sel?.options?.length ?? 0; i++) {
        array[array.length] = sel.options[i];
    }

    var finfOpt = array.find(_ => _.value == opt);
    var ind = array.indexOf(finfOpt);
    if (ind > -1)
    {
        sel.options[ind].selected = false;
        sel.options[ind].style.fontWeight = "normal";
    }
};

// Функция для установки в теге select выбранного option (установка в выбранном opt свойства selected = true)
function SetSelectedValues(sel, opt) {
    var array = [];

    for (i = 0; i < sel.options.length; i++) {
        array[array.length] = sel.options[i];
    }

    var finfOpt = array.find(_ => parseInt(_.value) == opt);
    var ind = array.indexOf(finfOpt);
    sel.options[ind].selected = true;
    sel.options[ind].style.fontWeight = "bold";
};

// Функция для установки в теге select выбранного option - пеервого в коллекции
function SetFirstSelectedValues(sel) {
    var array = [];

    sel.options[0].selected = true;
};
