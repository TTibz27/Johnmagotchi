using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Johnmagotchi.Data.BattleObjects.AttackDefines;

namespace Johnmagotchi.GameContent.Objects.Units.BattleObjects
{
    public class AttackObj
    {

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
