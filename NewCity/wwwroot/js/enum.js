
var enumConditionType = [">", "<", "=", "≠"];

var enumEffectType = ["增加", "减少", "赋值","场景转移","结束"];

const emptyguid = "00000000-0000-0000-0000-000000000000";

//生成uuid
function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}
function guid() {
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}

//角色状态   行动力 幸运 速度 力量 智力 社会经验 社会地位 善恶值
var characterStatus = ["Healthy", "MaxHealthy", "MaxSanity","Sanity","ActionPoints", "Lucky", "Speed", "Strength", "Intelligence", "Experience", "Status", "Moral"] 
