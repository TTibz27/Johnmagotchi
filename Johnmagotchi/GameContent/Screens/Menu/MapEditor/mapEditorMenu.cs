using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.Inputs;
using TibzGame.Core.ScreenManager;

using Johnmagotchi.GameContent.Objects;
using System.Text.Json;
using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Screens.Menu;
using Johnmagotchi.Screen.BattleMapScreens;
using Johnmagotchi.GameContent.Screens.Menu.MapEditor;

namespace Johnmagotchi.GameContent.Screens.Menu
{
    public class MapEditorMenu : BaseMapMenu
    {
        public MapEditorScreen mapEditor;

        public MapEditorMenu(MapEditorScreen parentScreen, int x, int y, int screenQuadrant) : base (parentScreen, x ,y ,screenQuadrant)
        {
            mapEditor = parentScreen;
            CurrentOptions = new MenuOption[5];
            CurrentOptions[0] = new MenuOption(MenuOption.MenuOptionType.CHOOSE_NEW_TOOL);
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
        }
        public override void ChildUpdate()
        {
            if (screenManager.inputs.editorInputs.confirm.isJustPressed)
            {
                TibzLog.Debug("Menu Selected Option Type :{0}", CurrentOptions[SelectedIndex]);
               
                if (CurrentOptions[SelectedIndex].OptionType == MenuOption.MenuOptionType.ENTER_BATTLE_SCREEN)
                {
                    screenManager.removeTopScreens(1); // remove this screen
                    screenManager.addScreen(new BattleMapScreen(mapEditor.GetMap()));
                }
                if (CurrentOptions[SelectedIndex].OptionType == MenuOption.MenuOptionType.CHOOSE_NEW_TOOL)
                {
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
                    screenManager.addScreen(new ToolSelectMenu(this, subX,subY, screenQuadrant));
                }
            }
        }
    }
}