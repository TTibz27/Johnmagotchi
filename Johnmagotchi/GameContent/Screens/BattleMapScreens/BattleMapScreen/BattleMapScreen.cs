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
        UnitObject selectedToMoveUnit;
        private List<Tuple<int, int>> validMoves;
        public BattleMapScreen(BattleMap map) : base(map)
        {
            selectedToMoveUnit = null;
        }
        public override void Destroy()
        {
            // throw new NotImplementedException();
        }

        public override void ChildInit()
        {
            TibzLog.Debug("Child init hit");
 
            
        }

        public override void ChildUpdate()
        {
            //   throw new NotImplementedException();

            if (screenManager.inputs.editorInputs.confirm.isJustPressed)
            {
                TibzLog.Debug("Select Pressed");

            
                UnitObject  newSelectedUnit = CurrentMap.getUnitAtLocation(cursorIndexX, cursorIndexY);
                if (newSelectedUnit != null && selectedToMoveUnit == null)
                {              
                    selectedToMoveUnit = newSelectedUnit;
                    validMoves = GetValidMoveCoordinates(selectedToMoveUnit, new Tuple<int, int>(cursorIndexX, cursorIndexY));

                    foreach (Tuple<int, int> tile in validMoves)
                    {
                        CurrentMap.ChangeTileHighlight(tile.Item1, tile.Item2, TileHighlight.MOVEMENT);
                    }

                }
                else if (selectedToMoveUnit != null) // we are currently moving
                {
                    if (newSelectedUnit == null)
                    { //  normal movement
                        this.selectedToMoveUnit = null;
                        if (validMoves != null)
                        {
                            foreach (Tuple<int, int> tile in validMoves)
                            {
                                CurrentMap.ChangeTileHighlight(tile.Item1, tile.Item2, TileHighlight.NONE);
                            }
                        }

                    }
                    else // new unit selected while attempting to move
                    {


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
                if (selectedToMoveUnit != null)
                {
                    this.selectedToMoveUnit = null;
                    if (validMoves != null)
                    {
                        foreach (Tuple<int, int> tile in validMoves)
                        {
                            CurrentMap.ChangeTileHighlight(tile.Item1, tile.Item2, TileHighlight.NONE);
                        }
                    }
                }
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

        protected override void TickCursorLeft()
        {
            if (cursorIndexX > 0) cursorIndexX--;
            else cursorIndexX = 0;
            // adjust screen after mouse movement
            tickLeftScroll();
        }
        protected override void TickCursorRight()
        {
            if (cursorIndexX < CurrentMap.width - 1) cursorIndexX++; // zero indexed so -1
            else cursorIndexX = CurrentMap.width - 1;
            // adjust screen after mouse movement
            tickRightScroll();
        }
        protected override void TickCursorUp()
        {
            if (cursorIndexY > 0) cursorIndexY--;
            else cursorIndexY = 0;
            // adjust screen after mouse movement
            tickUpScroll();
        }
        protected override void TickCursorDown()
        {
            if (cursorIndexY < CurrentMap.height - 1) cursorIndexY++; // zero indexed so -1
            else cursorIndexY = CurrentMap.height - 1;
            // adjust screen after mouse movement
            tickDownScroll();
        }
    }
}