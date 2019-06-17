
window.onkeydown = function (evt) {
    evt = (evt) ? evt : window.event;
    if (evt.keyCode) {
        if (evt.keyCode === 13) {
            $('#sumbit').click();
        }
    }
}

function loginSumbit() {
    if (validateInput()) {
        swal({
            title: "正在验证...",
            text: "",
            confirmButtonColor: "#1ab394",
            confirmButtonText: "确定",
        });
        $.ajax({
            url: "/Login/LoginSumbit",
            type: "POST",
            data: {
                email: $("#email").val(),
                password: $("#password").val()
            },
            traditional: true,
            success: function (result) {
                if (result === "true") {
                    toIndex();
                }
                else if (result === "overDate") {
                    MessageShow("登陆失败", "账号已过期，请联系管理员", "error");
                }
                else {
                    MessageShow("验证失败", "账号或密码错误", "error");
                }
            }
        });
    }
}

function toIndex() {
    window.location.href = "/Home/DashBoard";
}

function loginOut() {
    $.ajax({
        url: "/Login/LoginOut",
        type: "POST",
        data: {
            userID: $("#userid").val()
        },
        traditional: true,
        success: function (result) {
            toLogin();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            toLogin();
        }
    });
}

function sendMail_Rgister() {
    if (validateInput()) {

        var reg = new RegExp("^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$");
        if (!reg.test($("#email").val())) {
            swal({
                title: "验证未通过",
                text: "请填写正确邮箱地址",
                confirmButtonColor: "#1ab394",
                confirmButtonText: "确定"
            });
            return;
        }
        //swal({
        //    title: "正在发送邮件...",
        //    text: "",
        //    confirmButtonColor: "#1ab394",
        //    confirmButtonText: "确定",
        //});
        $.ajax({
            url: "/Login/SendRegisterMail",
            type: "POST",
            data: {
                email: $("#email").val()
            },
            traditional: true,
            success: function (result) {
                if (result !== "error") {
                    //MessageShow("验证成功", "请注意查收您的邮件。", "success", toLogin);
                    window.location.href = "/Login/RegisterInfo?e=" + result;
                }
                else {
                    MessageShow("验证失败", result, "error");
                }
            }
        });
    }
}

function sendMail_ForgetPass() {
    if (validateInput()) {
        swal({
            title: "正在发送邮件...",
            text: "",
            confirmButtonColor: "#1ab394",
            confirmButtonText: "确定",
        });
        $.ajax({
            url: "/Login/SendForgetMail",
            type: "POST",
            data: {
                email: $("#email").val()
            },
            traditional: true,
            success: function (result) {
                if (result === "true") {
                    MessageShow("发送成功", "请注意查收您的邮件。", "success", toLogin);
                }
                else {
                    MessageShow("发送失败", result, "error");
                }
            }
        });
    }
}

function RgisterSumbit() {
    var email = $("#email").val();
    var passWord = $("#password").val();
    var confirmPassword = $("#confirmPassword").val();
    var Name = $("#Name").val();
    var Company = $("#Company").val();
    var CellPhone = $("#CellPhone").val();
    var Tel = $("#Tel").val();

    if (validateInput()) {
        if (validateRegisteInfo()) {
            swal({
                title: "正在保存...",
                text: "",
                confirmButtonColor: "#1ab394",
                confirmButtonText: "确定",
            });
            $.ajax({
                url: "/Login/RgisterSumbit",
                type: "POST",
                data: {
                    email: email,
                    passWord: passWord,
                    Name: Name,
                    Company: Company,
                    TelPhone: Tel
                },
                traditional: true,
                success: function (result) {
                    if (result === "true") {
                        MessageShow("注册成功", "欢迎登陆UES。", "success", toIndex);
                    }
                    else {
                        MessageShow("注册失败", "请稍后重试。", "error");
                    }
                }
            });
        }
    }
}

function ResetpassSumbit() {
    var email = $("#email").val();
    var passWord = $("#password").val();
    var confirmPassword = $("#confirmPassword").val();

    if (validateInput()) {
        if (validateRegisteInfo()) {
            swal({
                title: "正在保存...",
                text: "",
                confirmButtonColor: "#1ab394",
                confirmButtonText: "确定",
            });
            $.ajax({
                url: "/Login/ResetPass",
                type: "POST",
                data: {
                    email: email,
                    passWord: passWord
                },
                traditional: true,
                success: function (result) {
                    if (result === "true") {
                        MessageShow("重置成功", "请重新登陆。", "success", toLogin);
                    }
                    else {
                        MessageShow("重置失败", "请稍后重试。", "error");
                    }
                }
            });
        }
    }
}

function validateRegisteInfo() {
    var email = $("#email").val();
    var passWord = $("#password").val();
    var confirmPassword = $("#confirmPassword").val();
    if (passWord != confirmPassword) {
        MessageShow("校验失败", "请确认密码输入一致", "error");
        return false;
    }
    if (passWord.length < 6) {
        MessageShow("校验失败", "密码长度需不小于6位", "error");
        return false;
    }
    return true;
}

function validateInput() {
    var doms = $("[required='required']");
    for (var i = 0; i < doms.length; i++) {
        doms[i].parentElement.setAttribute("class", "form-group");
        if (doms[i].value === "") {
            doms[i].parentElement.setAttribute("class", "form-group has-error");
            return false;
        }
    }
    return true;
}



