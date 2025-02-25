using Johnmagotchi.GameContent.Objects.Units.BattleObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.GameContent.Objects.Units.Loadouts
{
    internal class UnitLoadout
    {
        // this class is going to be a summary of both the player's class and thier currently added loadout modules.
        // this should effectively be an API between the character's stats/ablities and the battle system.
        PlayerClasses.PlayerClass PlayerClass;
        List<LoadoutModule> Modules;
        List<AttackObj> AvailableAttacks;
        List<SupportObj> AvailableSupports;
        List<SkillObj> ActiveSkills;
     
        
    }
}
