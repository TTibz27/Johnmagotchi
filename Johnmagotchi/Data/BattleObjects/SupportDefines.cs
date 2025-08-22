using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Johnmagotchi.Data.BattleObjects.SkillDefines;

namespace Johnmagotchi.Data.BattleObjects
{
    public class SupportDefines
    {
        // A support is an action that isnt an attack. This can include things that aren't "friendly" such as "steal" or possibly status effects. FE Rods basically.
        public enum SupportType { 
            HEAL = 0,
            BOOST_STAT_ATTACK,
            BOOST_STAT_DEFENSE,
            BOOST_STAT_CRITICAL,
            BOOST_STAT_SPEED,
            BOOST_STAT_MOVE,
            STATUS_EFFECT,
            STEAL,
            WARP,
            RESCUE,
            UNLOCK,
        }

        public enum SupportTarget { 
            SELF,
            ALLY,
            ALLIES_IN_RANGE,
            ENEMY,
            ENEMIES_IN_RANGE
        }

        public static SupportType ParseSupportTypes(string enumString)
        {
            Enum.TryParse(enumString, out SupportType typeEnum);
            return typeEnum;
        }
        public static SupportTarget ParseSupportTargets(string enumString)
        {
            Enum.TryParse(enumString, out SupportTarget targetEnum);
            return targetEnum;
        }
    }
}
