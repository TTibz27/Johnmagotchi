using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.GameContent.Objects.Units.Loadouts
{
    internal class LoadoutModule
    {
        // This is going to be each little square module in the character customizer

        enum ModuleType {
            ATTACK,
            ACTIVE_SUPPORT,
            PASSIVE_SUPPORT,
            STAT_BOOST,
            BATTLE_EFFECT
        }  

        public enum ModuleShape {
        SINGLE,
        DOUBLE,
        TRIPLE_STRAIGHT,
        TRIPLE_L,
        TETRI_O,
        TETRI_I,
        TETRI_S,
        TETRI_Z,
        TETRI_L,
        TETRI_J,
        TETRI_T,
        }

        public enum ShapeOrientation { 
        NORTH, SOUTH, WEST, EAST
        }

        public int activationCost;
        public int secondaryActivationCost;
        public string ModuleName;
        public ModuleShape Shape;
        public ShapeOrientation orientation;

        /*   
            RR - Armor Knight
            RB - Mercenary/Axe Fighter
            RY - Grappler
            RG - Big Shield Bro
            RP - Flamethrower Enthusiast

            BB - True Generic Helldiver
            BY - call out weakpoints
            BG - Arc Weapon Sprayer
            BP - Grenadier

            YY - Theif/Assassin
            YG - Behind Enemy Lines
            YP - Master of Traps

            GG - True Staff User
            GP - Aes Sedai

            PP - Elemental Mage

         */

    }
}
