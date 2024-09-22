using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.GameContent.Screens.Menu
{
    internal class MenuOption
    {
        public enum MenuOptionType
        {
            CHOOSE_NEW_TOOL,
            ENTER_BATTLE_SCREEN,
            IMPORT_MAP,
            EXPORT_MAP,
            EXIT
        }
        public string OptionLabel;
        public MenuOptionType OptionType;

        public MenuOption(MenuOptionType type) { 
        this.OptionType = type;
        
        }
        public string GetMenuString(MenuOptionType type) {
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
                default:
                    return "";

            }
        }
    }
}
