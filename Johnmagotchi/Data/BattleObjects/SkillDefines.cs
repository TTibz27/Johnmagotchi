using Johnmagotchi.GameContent.Objects.Units.BattleObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.Data.BattleObjects
{
    public static class SkillDefines
    {
        public enum SkillTypes
        {
            ATK_BOOST,
            SPD_BOOST,
            DEF_BOOST,
            MOV_BOOST,
            UNIQUE
        }

        public static int GetAttackBoost(List<SkillObj> skillList)
        {
            int boost = 0;
            foreach (var skill in skillList)
            {
                if (skill.SkillType == SkillTypes.ATK_BOOST) {  boost+= skill.Value; }
             
            }
            return boost;
        }

        public static SkillTypes ParseSkillTypes(string enumString) {
            Enum.TryParse(enumString, out SkillTypes skillEnum);
            return skillEnum;
        }
    }
}
