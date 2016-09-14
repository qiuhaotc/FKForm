/*-------------------------------------------------------------配置代码 Begin-------------------------------------------------------------*/

/*
*   配置项
*/
var validateConfig = {
    showSingleMessage: true,
    autoForcus: false
};

var validateItemArray = [];
var errorItemsArray = [];
var validateAjaxItems = [];
var serverValidateErrorArray = [];

/*-------------------------------------------------------------配置代码 End-------------------------------------------------------------*/

/*-------------------------------------------------------------校验代码 Begin-------------------------------------------------------------*/

/*-----------------------------用户自定义校验方法 Begin-----------------------------*/
/*
*   用户自定义校验方法
*/
function customValidateMethod(obj) {
    var valueThis = GetValue(obj.ControlID);
    if (valueThis.length < 10) {
        return false;
    }
    else {
        return true;
    }
}
/*
*      自定义Ajax Form 校验
*/
function validateAjaxForm() {

    if (validateForm()) {
        var formData = $("#form1").serialize();

        $.post("", formData + "&Ajax=true", function (data) {
            if (data.length == 0) {
                ShowAlertBox("成功");
            }
            else {
                serverValidateErrorArray = data;
                showServerAlertInfo();
            }
        }, "json");
    }

    return false;
}

/*-----------------------------用户自定义校验方法 End-----------------------------*/

/*
    添加校验事件
*/
$(function () {
    for (var index = 0; index < validateItemArray.length; index++) {
        var itemThisArray = validateItemArray[index];

        (function (itemThisArray) {

            $("#" + itemThisArray[0].ControlID).change(function () {
                var itemIsValid = true;

                var validateErrorCount = 0;

                var hasCompare = false;
                var compareControlID = "";

                for (var itemColomnIndex = 0; itemColomnIndex < itemThisArray.length; itemColomnIndex++) {
                    var itemThis = itemThisArray[itemColomnIndex];

                    switch (itemThis.ValidateType) {
                        case "Regex":
                            itemIsValid = validateRegex(itemThis.ControlID, itemThis.RegexStr);
                            break;
                        case "Length":
                            itemIsValid = validateLength(itemThis.ControlID, itemThis.MinLength, itemThis.MaxLength);
                            break;
                        case "Range":
                            itemIsValid = validateRange(itemThis.ControlID, itemThis.RegexStr, itemThis.MinValue, itemThis.MaxValue);
                            break;
                        case "Ajax":
                            validateAjax(itemThis.ControlID, itemThis.ValidateURL, validateCallBack, itemThis);
                            itemIsValid = true;
                            break;
                        case "Compare":
                            itemIsValid = validateSame(itemThis.ControlID, itemThis.CompareControlID);
                            hasCompare = true;
                            compareControlID = itemThis.CompareControlID;
                            break;
                        case "Other":
                            itemIsValid = eval(itemThis.ValidateJavaScript + "(itemThis)");
                            break;
                    }

                    if (!itemIsValid) {
                        validateErrorCount++;
                        SetErrorItems(itemThis);
                    }
                    else {
                        clearErrorItems(itemThis);
                    }
                }

                if (validateErrorCount != 0) {
                    SetForcus(itemThisArray[0].ControlID);
                    ShowAlertBoxSingle(itemThisArray[0]);
                }
                else {
                    if (hasCompare) {
                        clearAlertInfoWithCompare(itemThisArray[0], compareControlID);
                    }
                    else {
                        clearAlertInfo(itemThisArray[0]);
                    }
                }
           
            });

        })(itemThisArray);
    }
});

/*
*  客户端校验表单
*/
function validateForm() {
    errorItemsArray = [];
    var isValid = true;
    var errorStr = "";
    var forcusControlID = "";
    for (var index = 0; index < validateItemArray.length; index++) {

        for (var colomnIndex = 0; colomnIndex < validateItemArray[index].length; colomnIndex++) {

            var itemThis = validateItemArray[index][colomnIndex];
            var itemIsValid = false;

            switch (itemThis.ValidateType) {
                case "Regex":
                    itemIsValid = validateRegex(itemThis.ControlID, itemThis.RegexStr);
                    break;
                case "Length":
                    itemIsValid = validateLength(itemThis.ControlID, itemThis.MinLength, itemThis.MaxLength);
                    break;
                case "Range":
                    itemIsValid = validateRange(itemThis.ControlID, itemThis.RegexStr, itemThis.MinValue, itemThis.MaxValue);
                    break;
                case "Ajax":
                    itemIsValid = validateAjaxNotAysnc(itemThis.ControlID, itemThis.ValidateURL);
                    break;
                case "Compare":
                    itemIsValid = validateSame(itemThis.ControlID, itemThis.CompareControlID);
                    break;
                case "Other":
                    itemIsValid = eval(itemThis.ValidateJavaScript + "(itemThis)");
                    break;
            }

            if (!itemIsValid) {
                var isShow = true;

                if (validateConfig.showSingleMessage) {
                    for (var errorIndex = 0; errorIndex < errorItemsArray.length; errorIndex++) {
                        if (errorItemsArray[errorIndex].ControlID == itemThis.ControlID) {
                            isShow = false;
                        }
                    }
                }

                //if (isShow) {
                //    errorStr += itemThis.ErrorStr + "\n";
                //}
                if (isValid) {
                    isValid = false;
                }
                if (forcusControlID == "") {
                    forcusControlID = itemThis.ControlID;
                }
                errorItemsArray[errorItemsArray.length] = { ControlID: itemThis.ControlID, IsShow: isShow, ErrorStr: itemThis.ErrorStr };
            }
        }
    }
    if (!isValid) {
        SetForcus(forcusControlID);
        for (var errorItemIndex = 0; errorItemIndex < errorItemsArray.length; errorItemIndex++) {
            ShowAlertBoxSingle(errorItemsArray[errorItemIndex]);
        }
        ShowAlertBox(errorStr);
    }
    else {
        clearErrorItems();
        clearAlertInfo();
    }
    return isValid;
}

/*
*  服务器端回发校验事件
*/
function showServerAlertInfo() {
    errorItemsArray = [];
    var forucsControlID = "";
   
    for (var serverErrorItemsIndex = 0; serverErrorItemsIndex < serverValidateErrorArray.length; serverErrorItemsIndex++) {
        var isShow = true;
        var itemThis = serverValidateErrorArray[serverErrorItemsIndex];

        if (validateConfig.showSingleMessage) {
            for (var errorIndex = 0; errorIndex < errorItemsArray.length; errorIndex++) {
                if (errorItemsArray[errorIndex].ControlID == itemThis.ControlID) {
                    isShow = false;
                }
            }
            if (forucsControlID == "") {
                forucsControlID = itemThis.ControlID;
            }
            errorItemsArray[errorItemsArray.length] = { ControlID: itemThis.ControlID, IsShow: isShow, ErrorStr: itemThis.ErrorStr };
        }
    }

    SetForcus(forucsControlID);
    for (var errorItemIndex = 0; errorItemIndex < errorItemsArray.length; errorItemIndex++) {
        ShowAlertBoxSingle(errorItemsArray[errorItemIndex]);
    }
    ShowAlertBox("");
}

/*
*  ajax校验
*/
function validateAjax(controlID, validateURL, callBackFunc,item) {
    var valueThis = GetValue(controlID);

    $.post(validateURL, { controlID: controlID, value: valueThis }, function (data) {
        callBackFunc(data.ControlID, data.ErrorStr, data.IsValid, item);
    }, "json");

    return false;
}

function validateCallBack(controlID, errorStr, itemIsValid, itemThis) {
    if (!itemIsValid) {
        SetForcus(itemThis.ControlID);
        ShowAlertBoxSingle(SetErrorItems(itemThis));
    }
    else {
        clearErrorItems(itemThis);
        clearAlertInfo(itemThis);
    }
}

function validateAjaxNotAysnc(controlID, validateURL) {
    var valueThis = GetValue(controlID);
    var isValid = false;

    $.ajax({
        type: "post",
        url: validateURL,
        cache: false,
        async: false,
        dataType: "json",
        data: { controlID: controlID, value: valueThis },
        success: function (result) {
            isValid = result.IsValid;
        }
    });

    return isValid;
}

/*
*  校验长度
*/
function validateLength(controlID, min, max) {
    var valueThis = GetValue(controlID);
    valueThis = valueThis.replace(/(^\s*)|(\s*$)/g, "");

    if (valueThis.length > max || valueThis.length < min) {
        return false;
    }
    else {
        return true;
    }
}

/*
 *   校验正则
*/
function validateRegex(controlID, regexStr) {
    var valueThis = GetValue(controlID);
    var reg = new RegExp(regexStr);

    if (reg.test(valueThis) != '') {
        return true;
    }
    else {
        return false;
    }
}

/*
*   校验数字范围
*/
function validateRange(controlID, regexStr, min, max) {
    var valueThis = GetValue(controlID);
    var reg = new RegExp(regexStr);

    if (reg.test(valueThis) != '') {
        if (valueThis > max || valueThis < min) {
            return false;
        }
        else {
            return true;
        }
    }
    else {
        return false;
    }
}

/*
*  校验值一致
*/
function validateSame(controlID,compareControlID) {
    var valueThis = GetValue(controlID);
    var valueCompare = GetValue(compareControlID);

    if(valueThis!=valueCompare)
    {
        return false;
    }
    else
    {
        return true;
    }
}

/*-------------------------------------------------------------校验代码 End-------------------------------------------------------------*/

/*-------------------------------------------------------------通用方法 Begin-------------------------------------------------------------*/

/*
*   获取控件值
*/
function GetValue(controlID) {
    var control = document.getElementById(controlID);
    if (control == null || control == undefined) {
        return '';
    }
    else {
        return control.value;
    }
}

/*
*   显示单个错误提示信息
*/
function ShowAlertBoxSingle(item) {

    for (var errorItemsIndex = 0; errorItemsIndex < errorItemsArray.length; errorItemsIndex++) {
        if (item.ControlID == errorItemsArray[errorItemsIndex].ControlID) {
            $("#" + item.ControlID + "_alert").remove();
            $("#" + item.ControlID).after("<span id='" + item.ControlID + "_alert'>" + errorItemsArray[errorItemsIndex].ErrorStr + "</span>");
            break;
        }
    }
}

/*
*   显示提示信息
*/
function ShowAlertBox(msg) {
    //alert(msg);
    var stringThis = '<pre class="brush: js;" style="width: 100%">' + msg+"\nvar errorItemsArray=[";//+ '</pre>';
    for (var i = 0; i < errorItemsArray.length; i++) {
        if (i == 0) {
            stringThis += "\n{ControlID:'" + errorItemsArray[i].ControlID + "',ErrorStr:'" + errorItemsArray[i].ErrorStr + ",',IsShow:" + errorItemsArray[i].IsShow + "}";
        }
        else {
            stringThis += "\n,{ControlID:'" + errorItemsArray[i].ControlID + "',ErrorStr:'" + errorItemsArray[i].ErrorStr + ",',IsShow:" + errorItemsArray[i].IsShow + "}";
        }
    }
    stringThis += '];\nvar showSingleMessage = ' + validateConfig.showSingleMessage + ';\nvar autoForcus = ' + validateConfig.autoForcus + ';</pre>';

    $("#message").html(stringThis);

    SyntaxHighlighter.highlight();
}

/*
*   设置控件聚焦
*/
function SetForcus(controlID) {
    if (validateConfig.autoForcus) {
        document.getElementById(controlID).focus();
    }
}

/*
*  去除校验提示信息
*/
function clearAlertInfo(item) {
    if (typeof (item) == "undefined") {
        $("#message").html("");
    }
    else {
        $("#" + item.ControlID + "_alert").html("");
    }
}

function clearAlertInfoWithCompare(item, compareControlID) {
    if (typeof (item) == "undefined") {
        $("#message").html("");
    }
    else {
        $("#" + item.ControlID + "_alert").html("");
        $("#" + compareControlID + "_alert").html("");
    }
}

/*
 *  去除错误项目
*/
function clearErrorItems(item) {
    if (typeof (item) == "undefined") {
        errorItemsArray = [];
    }
    else {
        if(item.ValidateType=="Compare")
        {
            for (var errorItemIndex = 0; errorItemIndex < errorItemsArray.length; errorItemIndex++) {
                if ((errorItemsArray[errorItemIndex].ControlID == item.ControlID||errorItemsArray[errorItemIndex].ControlID==item.CompareControlID) && errorItemsArray[errorItemIndex].ErrorStr == item.ErrorStr) {
                    errorItemsArray.remove(errorItemIndex);
                }
            }
        }
        else
        {
            for (var errorItemIndex = 0; errorItemIndex < errorItemsArray.length; errorItemIndex++) {
                if (errorItemsArray[errorItemIndex].ControlID == item.ControlID && errorItemsArray[errorItemIndex].ErrorStr == item.ErrorStr) {
                    errorItemsArray.remove(errorItemIndex);
                }
            }
        }
    }
}

/*
*  设置错误项目
*/
function SetErrorItems(item) {
    var hasValue = false;
    var isShow = true;
    var hasValueitem = null;
    var errorItems = null;
    for (var errorItemIndex = 0; errorItemIndex < errorItemsArray.length; errorItemIndex++) {
        if (errorItemsArray[errorItemIndex].ControlID == item.ControlID && errorItemsArray[errorItemIndex].ErrorStr == item.ErrorStr) {
            hasValue = true;
            hasValueitem = errorItemsArray[errorItemIndex];
            errorItems = hasValueitem;
            break;
        }
    }
    if (!hasValue) {
        errorItemsArray[errorItemsArray.length] = errorItems={ ControlID: item.ControlID, IsShow: isShow, ErrorStr: item.ErrorStr };
    }
    else if (isShow == true&&hasValue!=null) {
        hasValueitem.IsShow = true;
    }

    return errorItems;
}

/* 
*  方法:Array.remove(dx) 通过遍历,重构数组 
*  功能:删除数组元素. 
*  参数:dx删除元素的下标. 
*/
Array.prototype.remove = function (dx) {
    if (isNaN(dx) || dx > this.length) { return false; }
    for (var i = 0, n = 0; i < this.length; i++) {
        if (this[i] != this[dx]) {
            this[n++] = this[i]
        }
    }
    this.length -= 1
}

/*-------------------------------------------------------------通用方法 End-------------------------------------------------------------*/