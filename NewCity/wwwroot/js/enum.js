
var enumConditionType = [">", "<", "=", "≠"];

var enumEffectType = ["增加", "减少", "赋值"];

const emptyguid = "00000000-0000-0000-0000-000000000000";

//生成uuid
function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}
function guid() {
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}

