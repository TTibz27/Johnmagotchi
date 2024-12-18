using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Objects;
using Johnmagotchi.GameContent.Objects.Maps;
using Johnmagotchi.GameContent.Screens.Menu;
using Johnmagotchi.GameContent.Screens.Menu.BattleMap;
using Johnmagotchi.GameContent.Units;
using Johnmagotchi.Screen.BattleMapScreens;
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

namespace Johnmagotchi.GameContent.Screens.BattleMapScreens.BattleMapScreen
{
    internal class BattleMapScreen : BaseMapScreen
    {
        public BattleMapScreen(BattleMap map) : base(map)
        {

        }
        public override void Destroy()
        {
            // throw new NotImplementedException();
        }

        public override void ChildInit()
        {
            TibzLog.Debug("Child init hit");
            // throw new NotImplementedException();
        }

        public override void ChildUpdate()
        {
            //   throw new NotImplementedException();

            if (screenManager.inputs.editorInputs.confirm.isJustPressed)
            {
                TibzLog.Debug("Select Pressed");


                UnitObject selectedUnit = CurrentMap.getUnitAtLocation(cursorIndexX, cursorIndexY);
                if (selectedUnit != null)
                {
                    List<Tuple<int, int>> vaildMoves =  GetValidMoveCoordinates(selectedUnit, new Tuple<int, int>(cursorIndexX, cursorIndexY));
                    
                    foreach (Tuple<int,int> tile in vaildMoves)
                    {
                        CurrentMap.ChangeTileHighlight(tile.Item1, tile.Item2, TileHighlight.MOVEMENT);                  
                    }

                }
                else
                {
                    screenManager.addScreen(
                    new BattleMapMenu(
                    this,
                    MapTile.TILE_WIDTH_PX * cursorIndexX + scrollOffsetX,
                     MapTile.TILE_HEIGHT_PX * cursorIndexY + scrollOffsetY,
                    CursorQuadrant));
                }
            }

            if (screenManager.inputs.editorInputs.cancel.isJustPressed)
            {

            }
        }

        public override void ChildDraw()
        {
            // throw new NotImplementedException();
        }

        public List<Tuple<int, int>> GetValidMoveCoordinates(UnitObject unit, Tuple<int,int> coords) {
            List<Tuple<int, int>> validTiles = new List<Tuple<int, int>>();
            int x = coords.Item1;
            int y = coords.Item2;

            int range = unit.stats.movement;

            // tree traversal time
            MapMovementNode rootNode = new MapMovementNode( unit, CurrentMap, range, x, y);
            rootNode.GetMoveLocations(MapMovementNode.ParentNodeLocation.ROOT,ref validTiles);
            
            return validTiles;      
        }
    }
}