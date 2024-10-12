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
            EDITOR_TOOL_DELETE_UNIT
        }
        public string OptionLabel;
        public MenuOptionType OptionType;

        public MenuOption(MenuOptionType type) { 
        this.OptionType = type;
        this.OptionLabel = GetMenuString(type);
        }
        public static string GetMenuString(MenuOptionType type) {
            switch (type) {
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
                case MenuOptionType.EDITOR_TOOL_TERRAIN:
                    return "Tiles";
                case MenuOptionType.EDITOR_TOOL_PLACE_UNIT:
                    return "Units";
                case MenuOptionType.EDITOR_TOOL_DELETE_UNIT:
                    return "Delete";
                default:
                    return "";

            }
        }
    }
}
