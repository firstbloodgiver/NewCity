using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Enum
{

    public enum enumConditionType {大于=0,小于=1,等于=2,不等于=3 }

    public enum enumEffectType {
        增加 = 0,
        减少 = 1,
        赋值 = 2,
        场景转移 = 3,
        结束处理 = 4
    }

    public enum enumStoryStatus
    {
        测试 = 0,
        结束 = 1,
        进行中 = 2,
        隐藏 = 3
    }

    public enum enumCharacterType { 作家 = 1 };

    public enum HomeType {轮播 = 0,主体语 = 1,内容 = 2 }
}
