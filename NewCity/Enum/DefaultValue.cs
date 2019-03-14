using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Enum
{
    public class DefaultValue
    {
        /// <summary>
        /// 创建人物故事卡
        /// </summary>
        public readonly Guid createCharacter = new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8");

        /// <summary>
        /// 默认地点系列
        /// </summary>
        public readonly Guid defaultlocation = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4");

        public int CheckCharacterType(Guid optionID) {
            switch (optionID.ToString())
            {
                case "6B29FC40-CA47-1067-B31D-00DD010662DA":
                    //作家
                    return 1;


            }
            return 0;
        }

        /// <summary>
        /// 新建角色后默认地点
        /// </summary>
        public Guid characterNextCard(int characterType)
        {
            
            switch (characterType)
            {
                case 1:
                    //作家
                    return new Guid("83409419-48E4-43EB-B23F-7FB664946152");
            }
            return Guid.Empty;
        }
    }
}
