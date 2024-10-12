using Johnmagotchi.Core.tools;
using Johnmagotchi.Screen.BattleMapScreens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.GameContent.Screens.Menu.MapEditor
{
    internal class ToolSelectMenu : BaseMapMenu
    {
        private MapEditorMenu Parent;
        public ToolSelectMenu(MapEditorMenu parent, int x, int y , int q): base(parent.mapEditor, x,y,q) {
            Parent = parent;
            CurrentOptions = new MenuOption[3];
            CurrentOptions[0] = new MenuOption(MenuOption.MenuOptionType.EDITOR_TOOL_TERRAIN);
            CurrentOptions[1] = new MenuOption(MenuOption.MenuOptionType.EDITOR_TOOL_PLACE_UNIT);
            CurrentOptions[2] = new MenuOption(MenuOption.MenuOptionType.EDITOR_TOOL_DELETE_UNIT);
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
           // throw new NotImplementedException();
        }

        public override void ChildUpdate()
        {
            if (screenManager.inputs.editorInputs.confirm.isJustPressed)
            {
                TibzLog.Debug("Closing Menu with Option Type :{0}", CurrentOptions[SelectedIndex]);
                screenManager.removeTopScreens(2); // remove this screen and the previous menu screen
                if (CurrentOptions[SelectedIndex].OptionType == MenuOption.MenuOptionType.EDITOR_TOOL_TERRAIN)
                {
                    //screenManager.addScreen(new BattleMapScreen(mapEditor.GetMap()));1
                    Parent.mapEditor.UpdateEditorTool(MapEditorScreen.EditorToolType.TILE_EDIT);
                }
                if (CurrentOptions[SelectedIndex].OptionType == MenuOption.MenuOptionType.EDITOR_TOOL_PLACE_UNIT)
                {
                    Parent.mapEditor.UpdateEditorTool(MapEditorScreen.EditorToolType.UNIT_ADD);
                }
                if (CurrentOptions[SelectedIndex].OptionType == MenuOption.MenuOptionType.EDITOR_TOOL_DELETE_UNIT)
                {
                    Parent.mapEditor.UpdateEditorTool(MapEditorScreen.EditorToolType.UNIT_DELETE);
                }
            }
        }
    }
}
