using Johnmagotchi.Core.tools;
using Johnmagotchi.Screen.BattleMapScreens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using TibzGame.Core.ScreenManager;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Johnmagotchi.GameContent.Screens.Menu.MapEditor
{
    internal class SaveSlotSelect : BaseMapMenu
    {
        private MapEditorMenu Parent;
        private Action<int> Callback;
        public SaveSlotSelect(MapEditorMenu parentScreen, int x, int y, int screenQuadrant, Action<int> callback) : base(parentScreen, x, y, screenQuadrant)
        {
            Parent = parentScreen;
            Callback = callback;
            CurrentOptions = new MenuOption[5];
            CurrentOptions[0] = new MenuOption(MenuOption.MenuOptionType.SAVE_SLOT_0);
            CurrentOptions[1] = new MenuOption(MenuOption.MenuOptionType.SAVE_SLOT_1);
            CurrentOptions[2] = new MenuOption(MenuOption.MenuOptionType.SAVE_SLOT_2);
            CurrentOptions[3] = new MenuOption(MenuOption.MenuOptionType.SAVE_SLOT_3);
            CurrentOptions[4] = new MenuOption(MenuOption.MenuOptionType.SAVE_SLOT_4);
            //CurrentOptions[5] = new MenuOption(MenuOption.MenuOptionType.SAVE_SLOT_5);
            //CurrentOptions[6] = new MenuOption(MenuOption.MenuOptionType.SAVE_SLOT_6);
            //CurrentOptions[7] = new MenuOption(MenuOption.MenuOptionType.SAVE_SLOT_7);
            //CurrentOptions[8] = new MenuOption(MenuOption.MenuOptionType.SAVE_SLOT_8);
            //CurrentOptions[9] = new MenuOption(MenuOption.MenuOptionType.SAVE_SLOT_9);
            if (screenQuadrant == 1)
            {
                xDrawOffset = 7000;
                yDrawOffset = -2800 * (CurrentOptions.Length - 1) - 300;
            }
            else if (screenQuadrant == 2)
            {
                xDrawOffset = -3800;
                yDrawOffset = -2800 * (CurrentOptions.Length - 1) - 300;
            }
            else if (screenQuadrant == 3)
            {
                xDrawOffset = -3800;
                yDrawOffset = 0;
            }
            else
            {
                xDrawOffset = 7000;
                yDrawOffset = 0;
            }
        }

        public override void ChildInit()
        {
           
        }

        public override void ChildUpdate()
        {
            if (screenManager.inputs.editorInputs.confirm.isJustPressed)
            {
                TibzLog.Debug("Closing Menu with Option Type :{0}", CurrentOptions[SelectedIndex]);

                this.Callback(SelectedIndex); // index should just match 1 to 1 with slot
              //  Parent.SaveSlotSelected(SelectedIndex); // index should just match 1 to 1 with slot

                screenManager.removeTopScreens(2); // remove this screen & previous
            }   
        }
    }
}
