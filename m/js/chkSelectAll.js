function FormSelectAll(formID,EleName,e) //formID：目标复选框组所在的form表单的ID属性；Elename：目标复选框组共同的Name属性；e：用于标识是否全选的复选框自身，用户判断是“全选”还是“全不选”   
{   
    var Elements = document.getElementById(formID).elements; //获取目标复选框组所在的Form表单   
    for (var i = 0; i < Elements.length;i++)   
    {   
        if (Elements[i].type == "checkbox" && Elements[i].name.indexOf(EleName) >= 0)   //根据对象类型和对象的name属性判断是否为目标复选框   
        {
             Elements[i].checked = e.checked;   //根据用于控制的复选框的选中情况判断是否选中目标复选框   
        }   
    }   
}

function FormSelectAllEnable(formID, EleName, e) //formID：目标复选框组所在的form表单的ID属性；Elename：目标复选框组共同的Name属性；e：用于标识是否全选的复选框自身，用户判断是“全选”还是“全不选”   
{
    var Elements = document.getElementById(formID).elements; //获取目标复选框组所在的Form表单   
    for (var i = 0; i < Elements.length; i++) {
        if (Elements[i].type == "checkbox" && Elements[i].name.indexOf(EleName) >= 0)   //根据对象类型和对象的name属性判断是否为目标复选框   
        {
            if (!Elements[i].disabled)
                Elements[i].checked = e.checked;   //根据用于控制的复选框的选中情况判断是否选中目标复选框   
        }
    }
}


function FormSelectAllRadio(formID, EleName, e) //formID：目标复选框组所在的form表单的ID属性；Elename：目标复选框组共同的Name属性；e：用于标识是否全选的复选框自身，用户判断是“全选”还是“全不选”   
{
    var Elements = document.getElementById(formID).elements; //获取目标复选框组所在的Form表单   
    for (var i = 0; i < Elements.length; i++) {
        if (Elements[i].type == "radio" && Elements[i].value==EleName)   //根据对象类型和对象的name属性判断是否为目标复选框   
        {
            var eleID = Elements[i]
            Elements[i].checked = e.checked;   //根据用于控制的复选框的选中情况判断是否选中目标复选框   
        }
    }
}

function CloseWebPage(msg) {
    var boolColse = confirm(msg);
    if (!boolColse)
        return;

    CloseWebPageNotConfirm();
}
function CloseWebPageNotConfirm() {
    if (navigator.userAgent.indexOf("MSIE") > 0) {
        if (navigator.userAgent.indexOf("MSIE 6.0") > 0) {
            window.opener = null; window.close();
        }
        else {
            window.open('', '_top'); window.top.close();
        }
    }
    else if (navigator.userAgent.indexOf("Firefox") > 0) {
        window.location.href = 'about:blank ';
        //window.history.go(-2);  
    }
    else {
        window.opener = null;
        window.open('', '_self', '');
        window.close();
    }
}

