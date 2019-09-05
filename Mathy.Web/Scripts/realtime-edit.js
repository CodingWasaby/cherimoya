var state;

var prevData;

function init() {

    var variableTemplate = "<tr id='{id}'>";
    variableTemplate += "<td width='10%'><input class='name' type='text' style='display: block; width: 100%;' /></td>";
    variableTemplate += "<td width='10%'>";
    variableTemplate += "<select class='type'>";
    variableTemplate += "<option selected>数值</option>";
    variableTemplate += "<option >字符串</option>";
    variableTemplate += "<option >矩阵</option>";
    variableTemplate += "<option >向量</option>";
    variableTemplate += "<option >数组</option>";
    variableTemplate += "</select>";
    variableTemplate += "<div class='type-size'>长度：<input type='text' /></div>";
    variableTemplate += "<div class='type-column-names'>列标题：<input type='text' /></div>";
    variableTemplate += "<div class='type-row-header-width'>行标头宽度：<input type='text' /></div>";
    variableTemplate += "<div class='type-row-name'>行名称：<input type='text' /></div>";
    variableTemplate += "</td>";
    variableTemplate += "<td width='70%'><input class='desc' type='text' style='display: block; width: 100%;' /></td>";
    variableTemplate += "<td width='10%'>";
    variableTemplate += "<a class='delete' href='javascript:deleteRow(0, &apos;{id}&apos;);'>删除</a>&nbsp;";
    variableTemplate += "<a class='moveUp' href='javascript:moveRow(0, {index}, -1);'>上移</a>&nbsp;";
    variableTemplate += "<a class='moveDown' href='javascript:moveRow(0, {index}, 1);'>下移</a>";
    variableTemplate += "</td>";
    variableTemplate += "</tr>";

    var expressionTemplate = "<tr id='{id}'>";
    expressionTemplate += "<td width='10%'><input class='title' type='text' style='display: block; width: 100%;' /></td>";
    expressionTemplate += "<td width='10%'><input class='desc' type='text' style='display: block; width: 100%;' /></td>";
    expressionTemplate += "<td width='35%' height='90px'><textarea class='expression' style='display: block; width: 100%; height: 100%;' /></td>";
    expressionTemplate += "<td width='35%' height='90px'><textarea class='condition' style='display: block; width: 100%; height: 100%;' /></td>";
    expressionTemplate += "<td width='10%'>";
    expressionTemplate += "<a class='delete' href='javascript:deleteRow(1, &apos;{id}&apos;);'>删除</a>&nbsp;";
    expressionTemplate += "<a class='moveUp' href='javascript:moveRow(1, {index}, -1);'>上移</a>&nbsp;";
    expressionTemplate += "<a class='moveDown' href='javascript:moveRow(1, {index}, 1);'>下移</a>";
    expressionTemplate += "</td>";
    expressionTemplate += "</tr>";

    state = [
        {
            count: parseInt($("#variableCount").val()),
            template: variableTemplate,
            id: "variable",
            listID: "variableList",
            addCallback: function (row) {
                $(".type", row).change(onSelectDataType);
                showOptions($(".type", row)[0]);
            }
        },
        {
            count: parseInt($("#expressionCount").val()),
            template: expressionTemplate,
            id: "expression",
            listID: "expressionList",
            addCallback: null
        }
    ];

    prevData = null;


    $(".type").change(onSelectDataType);

    var selects = $(".type");
    for (var i = 0; i <= selects.length - 1; i++) {
        showOptions(selects[i]);
    }
}

function onSelectDataType(e) {
    showOptions(e.currentTarget);
}

function showOptions(row) {

    var p = row.parentNode;
    var option0 = $(".type-size", p);
    var option1 = $(".type-column-names", p);
    var option2 = $(".type-row-header-width", p);
    var option3 = $(".type-row-name", p);

    var s = $(row).find(":selected").text();

    if (s === "矩阵") {
        option0.hide();
        option1.show();
        option2.show();
        option3.show();
    }
    else if (s === "向量") {
        option0.show();
        option1.show();
        option2.show();
        option3.show();
    }
    else {
        option0.hide();
        option1.hide();
        option2.hide();
        option3.hide();
    }
}

function add(stateIndex) {

    var id = state[stateIndex].id + state[stateIndex].count;
    $("#" + state[stateIndex].listID).append(state[stateIndex].template.replace(/{id}/g, id).replace(/{index}/g, state[stateIndex].count));
    state[stateIndex].count++;

    if (state[stateIndex].addCallback !== null) {
        var rows = $("tr", $("#" + state[stateIndex].listID));
        state[stateIndex].addCallback(rows[rows.length - 1]);
    }
}

function deleteRow(stateIndex, id) {

    $("#" + id).remove();
    state[stateIndex].count--;

    renameRows(stateIndex);
}

function moveRow(stateIndex, index, d) {

    var listID = state[stateIndex].listID;
    var count = state[stateIndex].count;

    if (d === -1 && index > 0) {
        var from = $("#" + listID + " tr")[index];
        var to = $("#" + listID + " tr")[index - 1];
        $("#" + listID)[0].removeChild(from);
        $(from).insertBefore(to);
    }
    else if (d === 1 && index < count - 1) {
        var from1 = $("#" + listID + " tr")[index];
        var to1 = $("#" + listID + " tr")[index + 1];
        $("#" + listID)[0].removeChild(from1);
        $(from).insertAfter(to1);
    }

    renameRows(stateIndex);
}


function renameRows(stateIndex) {

    var s = state[stateIndex];

    var rows = $("#" + s.listID + " tr");

    for (var i = 0; i <= rows.length - 1; i++) {
        var id = s.id + i;
        rows[i].setAttribute("id", id);
        $(".delete", rows[i]).attr("href", "javascript:deleteRow(" + stateIndex + ",'" + id + "');");
        $(".moveUp", rows[i]).attr("href", "javascript:moveRow(" + stateIndex + "," + i + ", -1);");
        $(".moveDown", rows[i]).attr("href", "javascript:moveRow(" + stateIndex + "," + i + ", 1);");
    }

    if (rows.length > 0) {
        $(rows[0]).remove(".moveUp");
        $(rows[rows.length - 1]).remove(".moveDown");
    }
}

function getPlanData(des) {

    var data = {};

    data["ID"] = $("#planID").val();
    data["EditID"] = $("#editID").val();
    data["Title"] = $("#titleTextBox").val();
    data["Description"] = des;     // $("#descriptionTextBox").val();
    data["Author"] = $("#authorTextBox").val();
    data["PlanType"] = $("#planType").find(":selected").val();
    data["PlanCategory"] = $("#category").find(":selected").val();
    data["Variables"] = [];
    data["Expressions"] = [];



    var variables = $("#variableList tr");

    for (var i = 0; i <= variables.length - 1; i++) {

        var variable = {};
        data.Variables.push(variable);
        variable["Name"] = $(".name", variables[i]).val();
        variable["Type"] = $(".type", variables[i]).find(":selected").text();
        variable["Description"] = $(".desc", variables[i]).val();

        if (variable["Type"] === "矩阵") {
            variable["Style"] = {
                ColumnNames: $(".type-column-names input", variables[i]).val(),
                RowHeaderWidth: $(".type-row-header-width input", variables[i]).val(),
                RowName: $(".type-row-name input", variables[i]).val()
            };
        }
        else if (variable["Type"] === "向量") {
            variable["Style"] = {
                Size: $(".type-size input", variables[i]).val(),
                ColumnNames: $(".type-column-names input", variables[i]).val(),
                RowHeaderWidth: $(".type-row-header-width input", variables[i]).val(),
                RowName: $(".type-row-name input", variables[i]).val()
            };
        }
    }


    var expressions = $("#expressionList tr");

    for (var i = 0; i <= expressions.length - 1; i++) {
        var expression = {};
        data.Expressions.push(expression);
        expression["Title"] = $(".title", expressions[i]).val();
        expression["Description"] = $(".desc", expressions[i]).val();
        expression["Expression"] = $(".expression", expressions[i]).val();
        expression["Condition"] = $(".condition", expressions[i]).val();
    }

    //console.log(data);
    return data;

}

function blink(id) {
    $("#" + id).css("color", "#ff0000");
    blinkTime(id, 0);
}

function blinkTime(id, time) {

    $("#" + id).css("color", "#ffffff");
    setTimeout(function () {
        $("#" + id).css("color", "#ff0000");
        setTimeout(function () {
            if (time < 2) {
                blinkTime(id, time + 1);
            }
        }, 200);
    }, 200);
}

var EventUtil = {
    addHandler: function (element, type, handler) {
        if (element.addEventListener) {
            element.addEventListener(type, handler, false);
        } else if (element.attachEvent) {
            element.attachEvent("on" + type, handler);
        } else {
            element["on" + type] = handler;
        }
    },
    removeHandler: function (element, type, handler) {
        if (element.removeEventListener) {
            element.removeEventListener(type, handler, false);
        } else if (element.detachEvent) {
            element.detachEvent("on" + type, handler);
        } else {
            element["on" + type] = null;
        }
    },

    getEvent: function (event) {
        return event ? event : window.event;
    },
    getTarget: function (event) {
        return event.target || event.srcElement;
    },
    preventDefault: function (event) {
        if (event.preventDefault) {
            event.preventDefault();
        } else {
            event.returnValue = false;
        }
    },
    stopPropagation: function (event) {
        if (event.stopPropagation) {
            event.stopPropagation();
        } else {
            event.cancelBubbles = true;
        }
    },
    getRelatedTarget: function (event) {
        if (event.relatedTarger) {
            return event.relatedTarget;
        } else if (event.toElement) {
            return event.toElement;
        } else if (event.fromElement) {
            return event.fromElement;
        } else { return null; }

    }

};
