var Grid = (function () {

    function Grid(row, column, id) {

        this.rowCount = row;
        this.columnCount = column;

        this.id = id;


        this.container = $("#" + id)[0];

        this.presenter = document.createElement("div");
        this.container.appendChild(this.presenter);

        init.call(this);
    }

    function init() {

        var self = this;

        this.newRowButton = document.createElement("input");
        this.newRowButton.type = "button";
        this.newRowButton.value = "+";
        this.presenter.appendChild(this.newRowButton);
        $(this.newRowButton).on("click", function () {
            onNewRowButtonClick.call(self);
        });

        this.newColumnButton = document.createElement("input");
        this.newColumnButton.type = "button";
        this.newColumnButton.value = "+";
        this.presenter.appendChild(this.newColumnButton);
        $(this.newColumnButton).on("click", function () {
            onNewColumnButtonClick.call(self)
        });

        for (var column = 0; column <= this.columnCount - 1; column++) {
            addColumnHeader.call(this, column);
        }

        for (var row = 0; row <= this.rowCount - 1; row++) {
            addRow.call(this, row);
        }

        layout.call(this);
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

        $(this.newRowButton).css({
            position: "absolute",
            left: "0px",
            top: (25 + 25 * this.rowCount) + "px"
        });

        $(this.newColumnButton).css({
            position: "absolute",
            left: (25 + 90 * this.columnCount) + "px",
            top: "0px"
        });


        var contentWidth = 50 + 90 * this.columnCount;
        var contentHeight = 50 + 25 * this.rowCount;

        $(this.presenter).css({
            overflow: "auto",
            position: "absolute",
            width: contentWidth + "px",
            height: contentHeight + "px"
        });


        var viewWidth = Math.min(600, contentWidth) + 25;
        var viewHeight = Math.min(500, contentHeight) + 25;

        $(this.presenter).css({
            position: "relative",
            width: viewWidth + "px",
            height: viewHeight + "px"
        });
    }

    function addRow(row) {

        var rowHeader = createRowHeader(row);
        this.presenter.appendChild(rowHeader);

        for (var column = 0; column <= this.columnCount - 1; column++) {
            var cell = createCell.call(this, row, column);
            this.presenter.appendChild(cell);
        }
    }

    function addColumnHeader(column) {

        var columnHeader = createColumnHeader(column);
        this.presenter.appendChild(columnHeader);
    }

    function addColumn(column) {

        for (var row = 0; row <= this.rowCount - 1; row++) {
            var cell = createCell.call(this, row, column);
            this.presenter.appendChild(cell);
        }
    }


    function createRowHeader(row) {

        var top = 25 + row * 25;

        var header = document.createElement("div");
        $(header).html("<div style='width:25x;height:25px;position:absolute;left:0px;top:" + top + "px;'>" + (row + 1) + "</div>");

        return header;
    }

    function createColumnHeader(column) {

        var left = 25 + column * 90;

        var header = document.createElement("div");
        $(header).html("<div style='width:90x;height:25px;position:absolute;left:" + left + "px;top:0px;'>" + (column + 1) + "</div>");

        return header;
    }

    function createCell(row, column) {

        var left = 25 + column * 90;
        var top = 25 + row * 25;

        var cell = document.createElement("div");
        $(cell).html("<input class='cell' type='text' data-row='" + row + "' data-column='" + column + "' style='width:90px;height:25px;position:absolute;left:" + left + "px;top:" + top + "px;'/>");

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

            var index = row * this.rowCount + column;

            if (index <= cells.length - 1) {
                cellViews[i].value = cells[index];
            }
        }
    };

    Grid.prototype.getMatrixString = function () {

        var row = this.rowCount - 1;
        var column = this.columnCount - 1;

        for (var i = 0; i <= this.rowCount - 1; i++) {
            if (isRowClear.call(this, i)) {
                row = i - 1;
                break;
            }
        }

        for (var i = 0; i <= this.columnCount - 1; i++) {
            if (isColumnClear.call(this, i)) {
                column = i - 1;
                break;
            }
        }


        if (row == -1 || column == -1) {
            return "";
        }


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
