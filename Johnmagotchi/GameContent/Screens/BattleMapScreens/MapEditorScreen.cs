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

namespace Johnmagotchi.Screen.BattleMapScreens
{
    public class MapEditorScreen : BaseMapScreen
    {
    
        private TileType selectedTileType;


        public MapEditorScreen(){

            this.isUpdatePriority = true;
        }
        public MapEditorScreen(BattleMap existingMap){
            this.CurrentMap = existingMap;
        }

        public override void ChildInit(){
            
                 selectedTileType = TileType.GRASS;
                 UnitLoader.GetTest();
        }
        public override void ChildUpdate() {
            if(screenManager.inputs.editorInputs.special1.isJustPressed){
                this.selectedTileType = TileType.GRASS;
            }
            if(screenManager.inputs.editorInputs.special2.isJustPressed){
                this.selectedTileType = TileType.SEA;
            }

            if(screenManager.inputs.editorInputs.cancel.isJustPressed){
                TibzLog.Debug("Cancel pressed");
            }
            if (screenManager.inputs.editorInputs.special5.isJustPressed)
            {
                int quadrant = 4;
               if( (MapTile.TILE_HEIGHT_PX * cursorIndexY) + scrollOffsetY < ScreenManager.GetScaledPixelScreenHeight() / 2) // top
                {
                    if ((MapTile.TILE_WIDTH_PX * cursorIndexX) + scrollOffsetX > ScreenManager.GetScaledPixelScreenWidth() /2) // right
                    {
                        quadrant = 1;
                    }
                    else // left
                    {
                        quadrant = 2;
                    }
                }
              
                else //bottom
                {
                    if ((MapTile.TILE_WIDTH_PX * cursorIndexX) + scrollOffsetX < ScreenManager.GetScaledPixelScreenWidth() / 2) // left
                    {
                        quadrant = 3;
                    }
                    else // right
                    {
                        quadrant = 4;
                    }
                }
                TibzLog.Debug("opening menu in Quadrant: {0}", quadrant);
                this.screenManager.addScreen(
                    new MapEditorMenu(
                    this,
                    (MapTile.TILE_WIDTH_PX * cursorIndexX) + scrollOffsetX, 
                    (MapTile.TILE_HEIGHT_PX * cursorIndexY) + scrollOffsetY,
                    quadrant));
            }

            // DRAW CURRENT
            if (screenManager.inputs.editorInputs.confirm.isPressed){
               CurrentMap.ChangeTileType(this.cursorIndexX, this.cursorIndexY, this.selectedTileType);
            }

        }
    }
}

