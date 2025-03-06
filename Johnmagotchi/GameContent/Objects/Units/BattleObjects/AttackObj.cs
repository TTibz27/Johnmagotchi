using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.GameContent.Objects.Units.BattleObjects
{
    public class AttackObj
    {
        public enum AttackTypeEnum { 
            DIRECT = 0,
            INDIRECT = 1
        }

        public enum SpecialAttackPropertiesEnum { 
            NONE = 0,
            BRAVE = 1,
            POISON = 2,
            KILLER = 3,
            ARMOR_BANE = 4 
        }

        public int ID;
        public string AttackName;
        public int BaseDamage;
        public int BaseHitCount;
        public int BaseSpeed;
        public int BaseAccuracy;
        public int AttackRangeMin;
        public int AttackRangeMax;
        public AttackTypeEnum AttackType;
        public SpecialAttackPropertiesEnum SpecialProperties;

    }
}
