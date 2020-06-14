function toLogin() {
    try {
        window.opener.location.href = "/index.html";
    }
    catch (e) {
        console.log(e);
        window.location.href = "/index.html";
    }
}

function toIndex() {
    window.location.href = "/Home/Index";
}

function reloadAllframes() {
    for (var i = 0; i < self.frames.length; i++) {
        self.frames[i].document.location.reload();
    }
}

function MessageShow(title, text, type, callback) {
    swal({
        title: title,
        text: text,
        type: type,
        confirmButtonColor: "#1ab394",
        confirmButtonText: "确定",
        closeOnConfirm: true,
    }, callback);
}

function innitPage(currentPage, totalPages, funcSearch, param1) {
    $('#pageLimit' + param1).bootstrapPaginator({
        currentPage: currentPage,
        totalPages: totalPages,
        size: "normal",
        bootstrapMajorVersion: 3,
        alignment: "right",
        numberOfPages: 5,
        itemTexts: function (type, page, current) {

            switch (type) {
                case "first": return "首页";
                case "prev": return "上一页";
                case "next": return "下一页";
                case "last": return "末页";
                case "page":
                    {
                        if (page === current) {
                            return "(" + page + ")";
                        }
                        return page;
                    }
            }
        },
        onPageClicked: function (event, originalEvent, type, page) {//给每个页眉绑定一个事件，其实就是ajax请求，其中page变量为当前点击的页上的数字。
            funcSearch(page);
        }
    });
}



