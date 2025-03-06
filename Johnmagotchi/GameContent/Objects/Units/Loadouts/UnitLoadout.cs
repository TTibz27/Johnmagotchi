using Johnmagotchi.GameContent.Objects.Units.BattleObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.GameContent.Objects.Units.Loadouts
{
    public class UnitLoadout
    {
        // this class is going to be a summary of both the player's class and thier currently added loadout modules.
        // this should effectively be an API between the character's stats/ablities and the battle system.
       private PlayerClasses.PlayerClass  PlayerClassRef;
       internal  List<LoadoutModule> Modules;
       public List<AttackObj> AvailableAttacks;
       public List<SupportObj> AvailableSupports;
       public List<SkillObj> ActiveSkills;
     
    }
}
