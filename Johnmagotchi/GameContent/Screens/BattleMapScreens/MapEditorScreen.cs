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

namespace Johnmagotchi.Screen.BattleMapScreens
{
    public class MapEditorScreen : BaseMapScreen
    {
    
        private TileType selectedTileType;


        public MapEditorScreen(){
        
        
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

        
            if(screenManager.inputs.editorInputs.special1.isJustPressed){
                this.selectedTileType = TileType.GRASS;
            }
            if(screenManager.inputs.editorInputs.special1.isJustPressed){
                this.selectedTileType = TileType.GRASS;
            }
            if(screenManager.inputs.editorInputs.cancel.isJustPressed){
               
            }

                // DRAW CURRENT
            if (screenManager.inputs.editorInputs.confirm.isPressed){
               CurrentMap.ChangeTileType(this.cursorIndexX, this.cursorIndexY, this.selectedTileType);
            }

        }
    }
}

