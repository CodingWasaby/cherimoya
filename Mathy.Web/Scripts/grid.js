var Grid = (function () {

    function Grid(row, column, id, options) {

        this.rowCount = row;
        this.columnCount = column;

        this.id = id;

        this.allowEditRow = options.allowEditRow;
        this.allowEditColumn = options.allowEditColumn;
        this.rowHeaderWidth = options.rowHeaderWidth;
        this.rowName = options.rowName;
        if (this.rowName != null && this.rowName.indexOf(",") != -1) {
            this.rowNames = this.rowName.split(",")
        }
        this.columnNames = options.columnNames;


        this.container = $("#" + id)[0];

        this.presenter = document.createElement("div");
        this.container.appendChild(this.presenter);

        init.call(this);
    }

    function init() {

        var self = this;

        if (this.allowEditRow) {
            this.newRowButton = document.createElement("input");
            this.newRowButton.type = "button";
            this.newRowButton.value = "+";
            this.presenter.appendChild(this.newRowButton);
            $(this.newRowButton).on("click", function () {
                onNewRowButtonClick.call(self);
            });
        }

        if (this.allowEditColumn) {
            this.newColumnButton = document.createElement("input");
            this.newColumnButton.type = "button";
            this.newColumnButton.value = "+";
            this.presenter.appendChild(this.newColumnButton);
            $(this.newColumnButton).on("click", function () {
                onNewColumnButtonClick.call(self)
            });
        }

        for (var column = 0; column <= this.columnCount - 1; column++) {
            addColumnHeader.call(this, column);
        }

        for (var row = 0; row <= this.rowCount - 1; row++) {
            addRow.call(this, row);
        }

        layout.call(this);


        if (self.rowCount == 1) {
            $("[type=button]", $("#row0", self.presenter)).hide();
        }

        if (self.columnCount == 1) {
            $("[type=button]", $("#column0", self.presenter)).hide();
        }
    }

    function onNewRowButtonClick() {

        this.rowCount++;
        addRow.call(this, this.rowCount - 1);
        layout.call(this);
    }

    function onNewColumnButtonClick() {

        this.columnCount++;
        addColumnHeader.call(this, this.columnCount - 1);
        addColumn.call(this, this.columnCount - 1);
        layout.call(this);
    }


    function layout() {

        if (this.allowEditRow) {
            $(this.newRowButton).css({
                position: "absolute",
                left: "0px",
                top: (25 + 25 * this.rowCount) + "px"
            });
        }

        if (this.allowEditColumn) {
            $(this.newColumnButton).css({
                position: "absolute",
                left: (60 + 90 * this.columnCount) + "px",
                top: "0px"
            });
        }


        var contentWidth = 40 + 90 * this.columnCount + this.rowHeaderWidth;;
        var contentHeight = 50 + 25 * this.rowCount;

        /*
        $(this.presenter).css({
            overflow: "auto",
            position: "absolute",
            width: contentWidth + "px",
            height: contentHeight + "px"
        });
        */

        var addRowButtonHeight = this.allowEditRow ? 25 : 0;
        var addColumnButtonWidth = this.allowEditColumn ? 25 : 0;

        var viewWidth = Math.min(600, contentWidth) + addColumnButtonWidth;
        var viewHeight = Math.min(500, contentHeight) + addRowButtonHeight;

        $(this.presenter).css({
            overflow: "auto",
            position: "relative",
            width: viewWidth + "px",
            height: viewHeight + "px"
        });
    }

    function layoutControls() {

        for (var i = 0; i <= this.rowCount - 1; i++) {
            var top = 25 + i * 25;
            $("#row" + i, this.presenter).animate({
                top: top + "px"
            }, 200);
        }

        for (var i = 0; i <= this.columnCount - 1; i++) {
            var left = 10 + i * 90 + this.rowHeaderWidth;
            $("#column" + i, this.presenter).animate({
                left: left + "px"
            }, 200);
        }

        for (var i = 0; i <= this.rowCount - 1; i++) {
            for (var j = 0; j <= this.columnCount - 1; j++) {

                var left = 10 + j * 90 + this.rowHeaderWidth;
                var top = 25 + i * 25;

                $("[data-row=" + i + "][data-column=" + j + "]", this.presenter).animate({
                    left: left + "px",
                    top: top + "px"
                }, 200);
            }
        }
    }


    function addRow(row) {

        if (this.rowCount == 2) {
            $("[type=button]", $("#row0", this.presenter)).show();
        }


        var rowHeader = createRowHeader.call(this, row);
        this.presenter.appendChild(rowHeader);

        for (var column = 0; column <= this.columnCount - 1; column++) {
            var cell = createCell.call(this, row, column);
            this.presenter.appendChild(cell);
        }
    }

    function addColumnHeader(column) {

        var columnHeader = createColumnHeader.call(this, column);
        this.presenter.appendChild(columnHeader);
    }

    function addColumn(column) {

        if (this.columnCount == 2) {
            $("[type=button]", $("#column0", this.presenter)).show();
        }


        for (var row = 0; row <= this.rowCount - 1; row++) {
            var cell = createCell.call(this, row, column);
            this.presenter.appendChild(cell);
        }
    }


    function getRowName(row) {

        if (this.rowName != null && this.rowName.length > 0) {
            if (this.rowNames != undefined && row <= this.rowNames.length - 1) {
                return this.rowNames[row];
            }
            else {
                return this.rowName.replace("{0}", row + 1);
            }
        }
        else {
            return row + 1;
        }
    }

    function createRowHeader(row) {

        var top = 25 + row * 25;

        var rowHeaderWidth = this.rowHeaderWidth == 0 ? 50 : this.rowHeaderWidth;

        var removeButton = "";
        if (this.allowEditRow) {
            removeButton = "<input data-row-index=" + row + " type='button' value='-' style='width:25px;position:absolute;left:" + (rowHeaderWidth - 25) + "px;top:0px;'>";
        }


        var label = "<div data-part='rowLabel'>" + getRowName.call(this, row) + "</div>";

        var header = document.createElement("div");
        $(header).html(
            "<div id='row" + row + "' style='width:" + rowHeaderWidth + "x;height:25px;position:absolute;left:0px;top:" + top + "px;'>" +
            removeButton +
            label +
            "</div>");


        var self = this;

        $("input[type=button]", header).on("click", function () {
            removeRow.call(self, this);
        });

        return header;
    }

    function createColumnHeader(column) {

        var left = 10 + column * 90 + this.rowHeaderWidth;

        var removeButton = "";
        if (this.allowEditColumn) {
            removeButton = "<input data-column-index=" + column + " type='button' value='-' style='width:25px;position:absolute;left:0px;top:0px;' onclick='javascript:removeColumn(" + column + ")'>";
        }
        

        var labelLeft = this.allowEditColumn ? 35 : 5;

        var label = "<div data-part='columnLabel' style='position:absolute;left:" + labelLeft + "px;top:0px;'>" + getColumnName.call(this, column) + "</div>";

        var header = document.createElement("div");
        $(header).html(
            "<div id='column" + column + "' style='width:90x;height:25px;position:absolute;left:" + left + "px;top:0px;'>" +
            removeButton +
            label +
            "</div>");


        var self = this;

        $("input[type=button]", header).on("click", function () {
            removeColumn.call(self, this);
        });

        return header;
    }

    function removeRow(button) {

        if (this.rowCount == 1) {
            return;
        }


        var rowIndex = parseInt($(button).attr("data-row-index"));

        $("[data-row=" + rowIndex + "]", this.presenter).remove();
        $("#row" + rowIndex, this.presenter).remove();


        for (var i = rowIndex + 1; i <= this.rowCount - 1; i++) {

            $("[data-row=" + i + "]", this.presenter).attr("data-row", i - 1);

            var row = $("#row" + i, this.presenter);

            row.attr("id", "row" + (i - 1));
            $("[type=button]", row).attr("data-row-index", i - 1);
            $("[data-part=rowLabel]", row).html(getRowName.call(this, i - 1));
        }

        this.rowCount--;


        var self = this;

        setTimeout(function () {

            layoutControls.call(self);

            setTimeout(function () {

                layout.call(self);

                if (self.rowCount == 1) {
                    $("[type=button]", $("#row0", self.presenter)).hide();
                }

            }, 200);

        }, 200);
    }

    function getColumnName(column) {
        return column <= this.columnNames.length - 1 ? this.columnNames[column] : (column + 1);
    }

    function removeColumn(button) {

        if (this.columnCount == 1) {
            return;
        }


        var columnIndex = parseInt($(button).attr("data-column-index"));

        $("[data-column=" + columnIndex + "]", this.presenter).remove();
        $("#column" + columnIndex, this.presenter).remove();


        for (var i = columnIndex + 1; i <= this.columnCount - 1; i++) {

            $("[data-column=" + i + "]", this.presenter).attr("data-column", i - 1);

            var column = $("#column" + i, this.presenter);

            column.attr("id", "column" + (i - 1));
            $("[type=button]", column).attr("data-column-index", i - 1);
            $("[data-part=columnLabel]", column).html(getColumnName.call(this, i - 1));
        }

        this.columnCount--;


        var self = this;

        setTimeout(function () {

            layoutControls.call(self);

            setTimeout(function () {

                layout.call(self);

                if (self.columnCount == 1) {
                    $("[type=button]", $("#column0", self.presenter)).hide();
                }

            }, 200);

        }, 200);
    }

    function createCell(row, column) {

        var left = 10 + column * 90 + this.rowHeaderWidth;
        var top = 25 + row * 25;

        var textBox = "<input class='cell' type='text' data-row='" +
            row +
            "' data-column='" +
            column +
            "' style='width:90px;height:25px;position:absolute;left:" +
            left +
            "px;top:" +
            top +
            "px;'/>";

        var cell = document.createElement("div");
        $(cell).html(textBox);

        return cell;
    }

    function getCellText(row, column) {

        return $("[data-row='" + row + "'][data-column='" + column + "']", this.presenter).val();
    }

    function isRowClear(row) {

        for (var i = 0; i <= this.columnCount - 1; i++) {
            if (getCellText.call(this, row, i).length > 0) {
                return false;
            }
        }


        return true;
    }

    function isColumnClear(column) {

        for (var i = 0; i <= this.rowCount - 1; i++) {
            if (getCellText.call(this, i, column).length > 0) {
                return false;
            }
        }


        return true;
    }

    Grid.prototype.setCells = function (cells) {

        var cellViews = $(".cell", this.container);

        for (var i = 0; i <= cellViews.length - 1; i++) {

            var row = parseInt($(cellViews[i]).data("row"));
            var column = parseInt($(cellViews[i]).data("column"));

            var index = row * this.columnCount + column;

            if (index <= cells.length - 1) {
                cellViews[i].value = cells[index];
            }
        }
    };

    Grid.prototype.getMatrixString = function () {

        var row = this.rowCount - 1;
        var column = this.columnCount - 1;

        var s = "[";

        for (var i = 0; i <= row; i++) {

            for (var j = 0; j <= column; j++) {
                var cellText = getCellText.call(this, i, j);
                s += (cellText.length == 0 ? "0" : cellText);
                s += j < column ? "," : "";
            }

            s += i < row ? ";" : "]";
        }


        return s;
    };

    return Grid;
})();
