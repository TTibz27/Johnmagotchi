using Johnmagotchi.GameContent.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TibzGame.Core.Inputs;
using TibzGame.Core.ScreenManager;
using Johnmagotchi.Screen.BattleMapScreens;
using  Johnmagotchi.GameContent.Units;
using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Screens.Menu;
using Johnmagotchi.GameContent.Screens.BattleMapScreens.MapEditor;

namespace Johnmagotchi.Screen.BattleMapScreens
{
    public class MapEditorScreen : BaseMapScreen
    {

        public enum EditorToolType
        {
            TILE_EDIT,
            UNIT_ADD,
            UNIT_DELETE
        }

        private TileType selectedTileType;
        private EditorToolType CurrentTool;
        private MapEditorInfoText InfoText;


        public MapEditorScreen(){

            this.isUpdatePriority = true;
            this.InfoText = new MapEditorInfoText();
        }
        public MapEditorScreen(BattleMap existingMap){
            this.CurrentMap = existingMap;
        }

        public BattleMap GetMap() {
            return CurrentMap;        
        }

        public override void ChildInit(){
            InfoText.Init(screenManager);
            selectedTileType = TileType.GRASS;
            UnitLoader.GetTest();
        }
        public override void ChildUpdate() {
            if (CurrentTool == EditorToolType.TILE_EDIT)
            {
                if (screenManager.inputs.editorInputs.special1.isJustPressed)
                {
                    this.selectedTileType = TileType.GRASS;
                }
                if (screenManager.inputs.editorInputs.special2.isJustPressed)
                {
                    this.selectedTileType = TileType.SEA;
                }
                if (screenManager.inputs.editorInputs.confirm.isPressed)
                {
                    CurrentMap.ChangeTileType(this.cursorIndexX, this.cursorIndexY, this.selectedTileType);
                }
            }

            if (screenManager.inputs.editorInputs.cancel.isJustPressed)
            {         
                this.screenManager.addScreen(
                    new MapEditorMenu(
                    this,
                    (MapTile.TILE_WIDTH_PX * cursorIndexX) + scrollOffsetX, 
                    (MapTile.TILE_HEIGHT_PX * cursorIndexY) + scrollOffsetY,
                    CursorQuadrant));
            }
        }

        public void UpdateEditorTool(EditorToolType tool) { 
            CurrentTool = tool;

            TibzLog.Debug("Tool Updated: {0}", CurrentTool);
        }

        public override void ChildDraw()
        {
            InfoText.Draw(CursorQuadrant, cursorIndexX, cursorIndexY, "Testing the text : " + CurrentTool);
        }
    }
}

