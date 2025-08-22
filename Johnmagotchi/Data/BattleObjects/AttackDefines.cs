using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Johnmagotchi.Data.BattleObjects.SkillDefines;

namespace Johnmagotchi.Data.BattleObjects
{
    public class AttackDefines
    {
        public enum AttackTypeEnum
        {
            DIRECT = 0,
            INDIRECT = 1
        }

        public enum SpecialAttackPropertiesEnum
        {
            NONE = 0,
            BRAVE = 1,
            POISON = 2,
            KILLER = 3,
            ARMOR_BANE = 4
        }

        public static AttackTypeEnum ParseAttackTypes(string enumString)
        {
            Enum.TryParse(enumString, out AttackTypeEnum AttackType);
            return AttackType;
        }
        public static SpecialAttackPropertiesEnum ParseSpecialAttackProperties(string enumString)
        {
            Enum.TryParse(enumString, out SpecialAttackPropertiesEnum props);
            return props;
        }
    }
}
