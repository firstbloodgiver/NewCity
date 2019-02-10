using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Enum
{

    public enum enumConditionType {大于=0,小于=1,等于=2,不等于=3 }

    public enum enumEffectType { 增加 = 0, 减少 = 1, 赋值 = 2}

    public class EnumConditionType
    {
        public string[] enumConditionType = { ">", "<", "=", "≠" };
    }

    public class EnumEffectType
    {
        public string[] enumConditionType = { "增加", "减少", "赋值" };
    }
}
