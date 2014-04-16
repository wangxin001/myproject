//=================================================================
//=================================================================
//=================================================================

// 指定的输入框是否为空
function CheckNull(v) {
    return document.getElementById(v).value.trim() == "";
}

// 指定的输入框是否大于长度
function CheckLengthMore(v, l) {
    // v.length >= l
    return document.getElementById(v).value.length >= l;
}

// 指定的输入框是否小于长度
function CheckLengthLess(v, l) {
    // v.length >= l
    return document.getElementById(v).value.length <= l;
}

// 制定的输入框输入的是否是数字
function CheckNumeric(v) {
    return document.getElementById(v).value.IsNumeric();
}

// 是否为整数
function CheckInteger(v) {
    return document.getElementById(v).value.IsInteger();
}

// 是否为正数
function CheckUnsigned(v) {
    return document.getElementById(v).value.IsUnsigned();
}

function CheckUnsignedInteger(v) {
    return document.getElementById(v).value.IsUnsignedInteger();
}

function CheckEmail(v) {
    return document.getElementById(v).value.isEmail();
}

function CheckDate(v) {
    return document.getElementById(v).value.isDate();
}

function CheckDateTime(v) {
    // yyyy-MM-dd HH:mm
    // yyyy-MM-dd HH:mm:ss

}

function CheckUserName(v) {

}

function CheckEqualIgnalCase(u, v) {
    return document.getElementById(u).value.toUpperCase() == document.getElementById(v).value.toUpperCase();
}

function CheckEqualMatchCase(u, v) {
    return document.getElementById(u).value == document.getElementById(v).value;
}

//=================================================================
//=================================================================
//=================================================================


//是否包含指定字符
String.prototype.IsContains = function(str) {
    return (this.indexOf(str) > -1);
}

//判断是否为空
String.prototype.IsEmpty = function() {
    var v = this.replace(/(^s+)|(s+$)/g, "");
    return v == "";
}

//判断是否为空
String.prototype.IsNULL = function() {
    return this == "";
}

//判断是否是数字
String.prototype.IsNumeric = function() {
    var newPar = /^(-|\+)?\d+(\.\d+)?$/;
    return newPar.test(this);
}

//判断是否是整数
String.prototype.IsInteger = function() {
    var newPar = /^(-|\+)?\d+$/;
    return newPar.test(this);
}

//判断是否是正数
String.prototype.IsUnsigned = function() {
    var newPar = /^\d+(\.\d+)?$/;
    return newPar.test(this);
}

//判断是否是正整数
String.prototype.IsUnsignedInteger = function() {
    var newPar = /^\d+$/;
    return newPar.test(this);
}

//合并多个空白为一个空白
String.prototype.resetBlank = function() {
    return this.replace(/s+/g, "");
}

//除去左边空白
String.prototype.LTrim = function() {
    return this.replace(/^s+/g, "");
}
//除去右边空白
String.prototype.RTrim = function() {
    return this.replace(/s+$/g, "");
}
//除去两边空白
String.prototype.trim = function() {
    return this.replace(/(^s+)|(s+$)/g, "");
}

// 从左截取指定长度的字串
String.prototype.left = function(n) {
    return this.slice(0, n);
}
// 从右截取指定长度的字串
String.prototype.right = function(n) {
    return this.slice(this.length - n);
}

// HTML编码
String.prototype.HTMLEncode = function() {
    var re = this;
    var q1 = [/x26/g, /x3C/g, /x3E/g, /x20/g];
    var q2 = ["&", "<", ">", " "];
    for (var i = 0; i < q1.length; i++)
        re = re.replace(q1[i], q2[i]);
    return re;
}

//获取Unicode
String.prototype.Unicode = function() {
    var tmpArr = [];
    for (var i = 0; i < this.length; i++) tmpArr.push("&#" + this.charCodeAt(i) + ";");
    return tmpArr.join("");
}
//指定位置插入字符串
String.prototype.Insert = function(index, str) {
    return this.substring(0, index) + str + this.substr(index);
}

String.prototype.isEmail = function() {
    var emailPat = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]+)+)$/;
    return emailPat.test(this);
}


String.prototype.isDate = function() {
    var reg = /^\d{4}-((0{0,1}[1-9]{1})|(1[0-2]{1}))-((0{0,1}[1-9]{1})|([1-2]{1}[0-9]{1})|(3[0-1]{1}))$/;
    var result = true;
    if (!reg.test(this))
        result = false;
    else {
        var arr_hd = this.split("-");
        var dateTmp;
        dateTmp = new Date(arr_hd[0], parseFloat(arr_hd[1]) - 1, parseFloat(arr_hd[2]));
        if (dateTmp.getFullYear() != parseFloat(arr_hd[0])
       || dateTmp.getMonth() != parseFloat(arr_hd[1]) - 1
        || dateTmp.getDate() != parseFloat(arr_hd[2])) {
            result = false
        }
    }
    return result;
}



/*
输入域合法性验证，随时补充
*/
function CheckValid(v) {

    for (var i = 0; i < v.length; i++) {
        var curField = v[i];

        switch (curField[0].toUpperCase()) {
            case "CheckNull".toUpperCase():
                if (CheckNull(curField[1])) {
                    alert(curField[2]);
                    return false;
                }
                break;
            case "CheckNumeric".toUpperCase():
                if (!CheckNumeric(curField[1])) {
                    alert(curField[2]);
                    return false;
                }
                break;
            case "CheckDate".toUpperCase():
                if (!CheckDate(curField[1])) {
                    alert(curField[2]);
                    return false;
                }
                break;
            default:
                return true;
        }
    }

    return true;
}
