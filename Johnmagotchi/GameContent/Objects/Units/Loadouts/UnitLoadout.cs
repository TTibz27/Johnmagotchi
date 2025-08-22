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
        // this should effectively be a layer between the character's stats/ablities and the battle system.

        public int playerClassID { get; set; }
        public string ModuleIDsCSV { get; set;} // csv list of all Aptitude Grid :TM: modules by ID
        public string AvailibleAttackIDsCSV { get; set; } // csv list of all attacks from class + Modules
        public string AvailaibeSupportIDsCSV { get; set; } // csv list of all supports from class + Modules
        public string ActiveSkillsIDsCSV { get; set; } // csv list of all Skills from class + modules

        public PlayerClasses.PlayerClass PlayerClassRef;

       // These get instatiated from the csv save of the loadout
       public List<LoadoutModule> Modules; 
       public List<AttackObj> AvailableAttacks;
       public List<SupportObj> AvailableSupports;
       public List<SkillObj> ActiveSkills;

       public UnitLoadout() {
            AvailableAttacks = new List<AttackObj>();
            AvailableSupports = new List<SupportObj>();
            ActiveSkills = new List<SkillObj>();
       } 


        public void loadAllFromCSV() {

            int[] atkIds = Array.ConvertAll(AvailibleAttackIDsCSV.Split(','), int.Parse);
            int[] sprtIds = Array.ConvertAll(AvailaibeSupportIDsCSV.Split(','), int.Parse);
            int[] sklIds = Array.ConvertAll(ActiveSkillsIDsCSV.Split(','), int.Parse);

            foreach (var attackID in atkIds)
            {
                foreach (var attackobj in BattleObjectManager.AllAttacks) {
                    if (attackID == attackobj.ID) { 
                        AvailableAttacks.Add(attackobj);
                    }
                }   
            }
            foreach (var supportID in sprtIds)
            {
                foreach (var supportobj in BattleObjectManager.AllSupports)
                {
                    if (supportID == supportobj.ID)
                    {
                        AvailableSupports.Add(supportobj);
                    }
                }
            }
            foreach (var skillID in sklIds)
            {
                foreach (var skillobj in BattleObjectManager.AllSkills)
                {
                    if (skillID == skillobj.ID)
                    {
                        ActiveSkills.Add(skillobj);
                    }
                }
            }

        }
        public void saveAllToCSV() {

            AvailibleAttackIDsCSV = String.Join(",", AvailableAttacks.Select(atk => atk.ID).ToArray());
            AvailaibeSupportIDsCSV = String.Join(",", AvailableSupports.Select(spt => spt.ID).ToArray());
            ActiveSkillsIDsCSV = String.Join(",", ActiveSkills.Select(skill => skill.ID).ToArray());
        }
    }
}
