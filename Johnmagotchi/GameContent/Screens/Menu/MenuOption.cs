using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.GameContent.Screens.Menu
{
    public class MenuOption
    {
        public enum MenuOptionType
        {
            //Editor Menu
            CHOOSE_NEW_TOOL,
            ENTER_BATTLE_SCREEN,
            IMPORT_MAP,
            EXPORT_MAP,
            EXIT,
            //Editor Tool Select
            EDITOR_TOOL_TERRAIN,
            EDITOR_TOOL_PLACE_UNIT,
            EDITOR_TOOL_DELETE_UNIT,
            //Save Slot
            SAVE_SLOT_0,
            SAVE_SLOT_1,
            SAVE_SLOT_2,
            SAVE_SLOT_3,
            SAVE_SLOT_4,
            SAVE_SLOT_5,
            SAVE_SLOT_6,
            SAVE_SLOT_7,
            SAVE_SLOT_8,
            SAVE_SLOT_9,
            //Battle Menu
            DEBUG_RETURN_TO_EDITOR

            // Unit End Movement
        }
        public string OptionLabel;
        public MenuOptionType OptionType;

        public MenuOption(MenuOptionType type) { 
        this.OptionType = type;
        this.OptionLabel = GetMenuString(type);
        }
        public static string GetMenuString(MenuOptionType type) {
            switch (type) {
                //Editor Menu
                case MenuOptionType.CHOOSE_NEW_TOOL:
                    return "Tools";
                case MenuOptionType.ENTER_BATTLE_SCREEN:
                    return "Battle";
                case MenuOptionType.IMPORT_MAP:
                    return "Load";
                case MenuOptionType.EXPORT_MAP:
                    return "Save";
                case MenuOptionType.EXIT:
                    return "Exit";
                //Editor tool select
                case MenuOptionType.EDITOR_TOOL_TERRAIN:
                    return "Tiles";
                case MenuOptionType.EDITOR_TOOL_PLACE_UNIT:
                    return "Units";
                case MenuOptionType.EDITOR_TOOL_DELETE_UNIT:
                    return "Delete";
                // Save Slots 
                case MenuOptionType.SAVE_SLOT_0:
                case MenuOptionType.SAVE_SLOT_1:
                case MenuOptionType.SAVE_SLOT_2:
                case MenuOptionType.SAVE_SLOT_3:
                case MenuOptionType.SAVE_SLOT_4:
                case MenuOptionType.SAVE_SLOT_5:
                case MenuOptionType.SAVE_SLOT_6:
                case MenuOptionType.SAVE_SLOT_7:
                case MenuOptionType.SAVE_SLOT_8:
                case MenuOptionType.SAVE_SLOT_9:
                    return "Slot " + (type - MenuOptionType.SAVE_SLOT_0 );
                case MenuOptionType.DEBUG_RETURN_TO_EDITOR:
                    return "Editor";


                default:
                    return "";

            }
        }
    }
}
