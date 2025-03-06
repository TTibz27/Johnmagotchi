using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Objects.Units.BattleObjects;
using Johnmagotchi.GameContent.Objects.Units.Loadouts;
using Johnmagotchi.GameContent.Units;
using System.Collections.Generic;
using System.Linq;

namespace Johnmagotchi.GameContent.Objects.Units.PlayerClasses
{
    public class PlayerClass
    {
        // Class contains a loadout, and default attacks/supports/skills
        enum ModuleColor
        {
            RED,
            BLUE,
            YELLOW,
            GREEN,
            PURPLE,
            COLORLESS
        }
        
        public enum PlayerClassType
        {
            CL_COMMONER = 0,

            RR_KNIGHT, //Red 1
            RB_MERC,
            RY_GRAPPLER,
            RG_SHIELD,
            RP_PYRO,
            BB_HECK_DIVER, //Blue 6
            BY_SPECIALIST,
            BG_ORORO_MUNROE,
            BP_GRENADIER,
            YY_ASSASSIN, //Yellow 10
            YG_SPY,
            YP_SURVIVALIST,
            GG_HEALER, //Green 13
            GP_MOIRAINE_HERSELF,
            PP_ELEMENTALIST, //Purple 15
          
        }

        UnitLoadout UnitLoadout { get; set; }
        PlayerClassType ClassType { get; set; }


        ModuleColor PrimaryColor;
        ModuleColor SecondaryColor;
        
        List<AttackObj> AvailableAttacks; // default + loadout 
        List<SupportObj> AvailableSupports;  // default + loadout 
        List<SkillObj> ActiveSkills;  // default + loadout 

        UnitStatBlock StatBoosts; // This is a unit stat block that will be added? to the base stats of a unit.

        List<int> defaultAttackIds;
        List<int> defaultSupportIds;
        List<int> defaultSkillIds;
      
        

        public PlayerClass(PlayerClassType type) {
            ClassType = type;

            AvailableAttacks = new List<AttackObj>();
            AvailableSupports = new List<SupportObj>();
            ActiveSkills = new List<SkillObj>();
            StatBoosts = new UnitStatBlock();

            InitClassSpecificFields();
              
        }
        public void SetAndInitLoadout( UnitLoadout loadout) { 
            UnitLoadout = loadout;
            AvailableAttacks.Clear();
            AvailableSupports.Clear();
            ActiveSkills.Clear();
            this.InitClassSpecificFields(); // grab attacks again
            AvailableAttacks = AvailableAttacks.Concat(loadout.AvailableAttacks).ToList();
            AvailableSupports = AvailableSupports.Concat(loadout.AvailableSupports).ToList();
            ActiveSkills = ActiveSkills.Concat(loadout.ActiveSkills).ToList();         
        }

        private void InitClassSpecificFields() {

            switch (ClassType) {
                case PlayerClassType.RR_KNIGHT:                
                    PrimaryColor = ModuleColor.RED; SecondaryColor = ModuleColor.RED;
                    break;
                case PlayerClassType.RB_MERC:
                    PrimaryColor = ModuleColor.RED; SecondaryColor = ModuleColor.BLUE;
                    break;
                case PlayerClassType.RY_GRAPPLER:
                    PrimaryColor = ModuleColor.RED; SecondaryColor = ModuleColor.YELLOW;
                    break;
                case PlayerClassType.RG_SHIELD:
                    PrimaryColor = ModuleColor.RED; SecondaryColor = ModuleColor.GREEN;
                    break;
                case PlayerClassType.RP_PYRO:
                    PrimaryColor = ModuleColor.RED; SecondaryColor = ModuleColor.PURPLE;
                    break;
                case PlayerClassType.BB_HECK_DIVER: 
                        PrimaryColor = ModuleColor.BLUE; SecondaryColor = ModuleColor.BLUE;
                    break;
                case PlayerClassType.BY_SPECIALIST: 
                        PrimaryColor = ModuleColor.BLUE; SecondaryColor = ModuleColor.YELLOW;
                    break;
                case PlayerClassType.BG_ORORO_MUNROE: 
                        PrimaryColor = ModuleColor.BLUE; SecondaryColor = ModuleColor.GREEN;
                    break;
                case PlayerClassType.BP_GRENADIER: 
                        PrimaryColor = ModuleColor.BLUE; SecondaryColor = ModuleColor.PURPLE;
                    break;
                case PlayerClassType.YY_ASSASSIN: 
                        PrimaryColor = ModuleColor.YELLOW; SecondaryColor = ModuleColor.YELLOW;
                    break;
                case PlayerClassType.YG_SPY: 
                        PrimaryColor = ModuleColor.YELLOW; SecondaryColor = ModuleColor.GREEN;
                    break;
                case PlayerClassType.YP_SURVIVALIST: 
                        PrimaryColor = ModuleColor.YELLOW; SecondaryColor = ModuleColor.PURPLE;
                    break;
                case PlayerClassType.GG_HEALER: 
                        PrimaryColor = ModuleColor.GREEN; SecondaryColor = ModuleColor.GREEN;
                    break;
                case PlayerClassType.GP_MOIRAINE_HERSELF: 
                        PrimaryColor = ModuleColor.GREEN; SecondaryColor = ModuleColor.PURPLE;
                    break;
                case PlayerClassType.PP_ELEMENTALIST: 
                        PrimaryColor = ModuleColor.PURPLE; SecondaryColor = ModuleColor.PURPLE;
                    break;
                case PlayerClassType.CL_COMMONER:
                    TibzLog.Debug("Commoner built");
                    PrimaryColor = ModuleColor.COLORLESS; SecondaryColor = ModuleColor.COLORLESS;
                    break;


            }
        }
        public static PlayerClassType GetClassFromShortHand(string shorthand) {
            shorthand = shorthand.ToLower();
            switch (shorthand) {
                case "rr":
                    return PlayerClassType.RR_KNIGHT;
                case "rb":
                    return PlayerClassType.RB_MERC;
                case "ry":
                    return PlayerClassType.RY_GRAPPLER;
                case "rg":
                    return PlayerClassType.RG_SHIELD;
                case "rp":
                    return PlayerClassType.RP_PYRO;
                case "bb":
                    return PlayerClassType.BB_HECK_DIVER;
                case "by":
                    return PlayerClassType.BY_SPECIALIST;
                case "bg":
                    return PlayerClassType.BG_ORORO_MUNROE;
                case "bp":
                    return PlayerClassType.BP_GRENADIER;
                case "yy":
                    return PlayerClassType.YY_ASSASSIN;
                case "yg":
                    return PlayerClassType.YG_SPY;
                case "yp":
                    return PlayerClassType.YP_SURVIVALIST;
                case "gg":
                    return PlayerClassType.GG_HEALER;
                case "gp":
                    return PlayerClassType.GP_MOIRAINE_HERSELF;
                case "pp":
                    return PlayerClassType.PP_ELEMENTALIST;
                default:
                    return PlayerClassType.CL_COMMONER;
            }

        }
    }
  
}

