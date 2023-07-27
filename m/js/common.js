
function sendPhoneCode(txtID, btnID, validExits) {
    var phone = $("#" + txtID).attr("value");
    var mobile = /^(((13[0-9]{1})|(14[0-9]{1})|(15[0-9]{1})|(16[0-9]{1})|(17[0-9]{1})|(18[0-9]{1}))+\d{8})$/;
    if (!mobile.test(phone)) {
        alert("请正确填写您的手机号码");
        return;
    }
    $.ajax({
        url: "/ajax/send_phone_code.ashx?phone=" + phone + "&validExits=" + validExits,
        type: "POST",
        dataType: "text",
        error: function () {
            $("#" + btnID).removeAttr('disabled');
            $("#" + btnID).css("background", "url(/bootstrap/img/smail.jpg) no-repeat scroll 0 0 transparent");
        },
        success: function (html) {
            alert(html);
        },
        beforeSend: function () {
            $("#" + btnID).attr('disabled', 'disabled');
            $("#" + btnID).css("background", "url(/bootstrap/img/smail2.jpg) no-repeat scroll 0 0 transparent");
            i = 30;
            timer = setInterval("enablePhoneSend('" + btnID + "')", 1000);
        }
    });
}

function sendPayCode(btnID) {
   
    $.ajax({
        url: "/ajax/send_pay_code.ashx",
        type: "POST",
        dataType: "text",
        error: function () {
            $("#" + btnID).removeAttr('disabled');
            $("#" + btnID).css("background", "url(/bootstrap/img/smail.jpg) no-repeat scroll 0 0 transparent");
        },
        success: function (html) {
            alert(html);
        },
        beforeSend: function () {
            $("#" + btnID).attr('disabled', 'disabled');
            $("#" + btnID).css("background", "url(/bootstrap/img/smail2.jpg) no-repeat scroll 0 0 transparent");
            i = 30;
            timer = setInterval("enablePhoneSend('" + btnID + "')", 1000);
        }
    });
}

function sendResetPassCode(txtID,btnID) {
    var username = $("#" + txtID).attr("value");
    $.ajax({
        url: "/ajax/send_resetpass_code.ashx?username=" + username,
        type: "POST",
        dataType: "text",
        error: function () {
            $("#" + btnID).removeAttr('disabled');
            $("#" + btnID).css("background", "url(/bootstrap/img/smail.jpg) no-repeat scroll 0 0 transparent");
        },
        success: function (html) {
            alert(html);
        },
        beforeSend: function () {
            $("#" + btnID).attr('disabled', 'disabled');
            $("#" + btnID).css("background", "url(/bootstrap/img/smail2.jpg) no-repeat scroll 0 0 transparent");
            i = 30;
            timer = setInterval("enablePhoneSend('" + btnID + "')", 1000);
        }
    });
}

function enablePhoneSend(btnID) {
    if (i <= 0) {
        //$("#timego").html("");
        $("#" + btnID).removeAttr('disabled');
        $("#" + btnID).css("background", "url(/bootstrap/img/smail.jpg) no-repeat scroll 0 0 transparent");
        $("#" + btnID).attr("value", "获取手机验证码");
        clearInterval(timer);
    } else {
        i--;
        $("#" + btnID).attr("value", "获取手机验证码 " + i);
        //$("#timego").html("&nbsp;剩余:" + i + "秒");
    }
}

$(function () {
    
    var myImages = ["/bootstrap/img/loading.gif"];
    for (var i = 0; i <= myImages.length; i++) {
        var img = new Image();
        img.src = myImages[i];
    }
    /*
    var html = "<div style=\"display:none\" id=\"loadingImgDiv\"><img alt=\"\" src=\"/bootstrap/img/loading.gif\"/></div>";
    $(document.body).append(html);
    */
    
})

$(function () {
    $(".table tr:gt(0)").hover(
    function () { $(this).addClass("even") },
    function () { $(this).removeClass("even") })
});
