using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Screens.BattleMapScreens.BattleMapScreen;
using Johnmagotchi.GameContent.Screens.Menu.MapEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.GameContent.Screens.Menu.BattleMap
{
    internal class BattleMapMenu : BaseMapMenu
    {
        private BattleMapScreen BattleMapInstance;
        public BattleMapMenu(BattleMapScreen parentScreen, int x, int y, int screenQuadrant) : base(parentScreen, x, y, screenQuadrant)
        {
            BattleMapInstance = parentScreen;
            CurrentOptions = new MenuOption[5];
            CurrentOptions[0] = new MenuOption(MenuOption.MenuOptionType.DEBUG_RETURN_TO_EDITOR);
            CurrentOptions[1] = new MenuOption(MenuOption.MenuOptionType.IMPORT_MAP);
            CurrentOptions[2] = new MenuOption(MenuOption.MenuOptionType.EXPORT_MAP);
            CurrentOptions[3] = new MenuOption(MenuOption.MenuOptionType.ENTER_BATTLE_SCREEN);
            CurrentOptions[4] = new MenuOption(MenuOption.MenuOptionType.EXIT);
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
          //  throw new NotImplementedException();
        }

        public override void ChildUpdate()
        {
            // throw new NotImplementedException();


            if (screenManager.inputs.editorInputs.confirm.isJustPressed)
            {
                TibzLog.Debug("Menu Selected Option Type :{0}", CurrentOptions[SelectedIndex]);
                int subX = xPos;
                int subY = yPos;
                if (screenQuadrant == 1)
                {
                    subX -= 800;
                    subY += 800;
                }
                else if (screenQuadrant == 2)
                {
                    subX += 800;
                    subY += 800;
                }
                else if (screenQuadrant == 3)
                {
                    subX += 800;
                    subY -= ((CurrentOptions.Length - 3) * 2800) - 800;
                }
                else
                {
                    subX -= 800;
                    subY -= ((CurrentOptions.Length - 3) * 2800) - 800;

                }

                if (CurrentOptions[SelectedIndex].OptionType == MenuOption.MenuOptionType.ENTER_BATTLE_SCREEN)
                {
                    //    screenManager.removeTopScreens(1); // remove this screen
                   // MapEditorInstance.SaveMap();
                 //   screenManager.addScreen(new BattleMapScreen(MapEditorInstance.GetMap()));
                }
                if (CurrentOptions[SelectedIndex].OptionType == MenuOption.MenuOptionType.CHOOSE_NEW_TOOL)
                {

                //    screenManager.addScreen(new ToolSelectMenu(this, subX, subY, screenQuadrant));
                }
                if (CurrentOptions[SelectedIndex].OptionType == MenuOption.MenuOptionType.EXPORT_MAP)
                {
                  //  screenManager.addScreen(new SaveSlotSelect(this, subX, subY, screenQuadrant, SaveSlotSelected));
                    // this would use the windows API to save, for now I am just going to use save slots bc I dont want to deal with it needing to be single threaded
                    //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    //saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                    //saveFileDialog1.Title = "Save an Image File";
                    //saveFileDialog1.ShowDialog();
                }
                if (CurrentOptions[SelectedIndex].OptionType == MenuOption.MenuOptionType.IMPORT_MAP)
                {
                  //  screenManager.addScreen(new SaveSlotSelect(this, subX, subY, screenQuadrant, LoadSlotSelected));
                }
            }


        }
     }
}
