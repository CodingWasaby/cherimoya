var currentMask;

var currentOptions;

function popup(title, options, html) {

    currentOptions = options;


    var hasButtons = false;

    var buttons = "";

    if (options.okButton) {
        buttons += "<input type='button' value='确定' onclick='javascript:onDialogOKButtonClick();'>";
        hasButtons = true;
    }

    if (options.cancelButton) {
        buttons += "<input type='button' value='取消' onclick='javascript:onDialogCancelButtonClick();'>";
        hasButtons = true;
    }

    if (hasButtons) {
        buttons = "<tr><td>" + buttons + "</td></tr>";
    }


    var dialog = document.createElement("div");

    var html = "<table style='background-color:#555599'><thead><tr><th width='90%'>" +
        "<div>" + title + "</div>" +
        "</th><th width='10%'></th></tr></thead>" +
        "<tbody><tr style='background-color:#ffffff'><td colspan='2'><div id='dialog-content'>" + html +
        "</div></td></tr>" + buttons + "</tbody></table>";

    var width = options.width + 8;
    var height = options.height + 35 + (hasButtons ? 21 : 0);

    $(dialog).html(html);
    $(dialog).css({
        'width': width + "px",
        'height': height + "px",
        'border': 'solid 1px black',
        'position': 'fixed',
        'background-color': '#ffffff',
        'left': '50%',
        'top': '50%',
        'z-index': 100000,
        'margin-top': -(height / 2) + "px",
        'margin-left': -(width / 2) + "px",
    });

    $("#dialog-content", dialog).css({
        'width': options.width + "px",
        'height': options.height + "px",
        'overflow-y': 'auto'
    });



    var mask = document.createElement("div");
    $(mask).css({
        'width': '100%',
        'height': '100%',
        'left': '0%',
        'top': '0%',
        'position': 'fixed',
        'overflow': 'hidden',
        'z-index': 100000,
        'background-color': 'rgba(0, 0, 0, 0.5)'
    });

    $(mask).append(dialog);

    currentMask = mask;

    $(document.body).append(currentMask);
    document.addEventListener('drop', function (e) {
        e.preventDefault();
    }, false);
    document.addEventListener('dragover', function (e) {
        e.preventDefault();
    }, false);
    document.getElementById("dialog-content").addEventListener("drop", function (e) {
        e.preventDefault();
        var file = e.dataTransfer.files[0];
        $("#select-file")[0].files = e.dataTransfer.files;
        //file.type; 文件类型
        //file.name;文件名
        //file.size; 文件大小 btye

    });
    document.getElementById("dialog-content").addEventListener("dragover", function (e) {
        e.preventDefault();
    });
}

function closePopup() {

    $(currentMask).remove();
    currentMask = null;
    currentOptions = null;
}

function onDialogCloseButtonClick() {
    closePopup();
}

function onDialogOKButtonClick() {
    console.log(currentOptions);
    if (currentOptions.ok) {
        currentOptions.ok();
    }
}

function onDialogCancelButtonClick() {
    if (currentOptions.cancel) {
        currentOptions.cancel();
    }
}