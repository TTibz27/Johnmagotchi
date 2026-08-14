using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Logic;
using Johnmagotchi.GameContent.Objects;
using Johnmagotchi.GameContent.Objects.Maps;
using Johnmagotchi.GameContent.Screens.Menu.BattleMap;
using Johnmagotchi.GameContent.Units;
using Johnmagotchi.Screen.BattleMapScreens;
using System;
using System.Collections.Generic;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Screens.BattleMapScreens.BattleMapScreen
{
    internal class BattleMapScreen : BaseMapScreen
    {
        enum SelectionState
        {
            MOVE,
            CHOOSE_ACTION,
            END_ACTION,
        }

        UnitObject selectedToMoveUnit;
        UnitObject attackedUnit;
        private Tuple<int, int> originalLocation;
        private List<Tuple<int, int>> validMoves;
        private List<Tuple<int, int>> validAttacks;
        private int initInputDelay = 40; // frames till inputs work
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

            // Wait until delay ends to start input
            if (initInputDelay > 0)
            {
                initInputDelay--;
                return;
            }


            // Handle inputs
            if (screenManager.inputs.editorInputs.confirm.isJustPressed)
            {
                TibzLog.Debug("Select Pressed");


                UnitObject newSelectedUnit = CurrentMap.getUnitAtLocation(cursorIndexX, cursorIndexY);
                TibzLog.Debug(newSelectedUnit.UnitLoadout.AvailableAttacks);
                if (newSelectedUnit != null && newSelectedUnit.isTurnOver == false && selectedToMoveUnit == null)
                {
                    selectedToMoveUnit = newSelectedUnit;
                    this.originalLocation = new Tuple<int, int>(cursorIndexX, cursorIndexY);

                    ShowAllMoveHighlights();

                }
                else if (selectedToMoveUnit != null) // unit is selected, movement not confirmed
                {
                    if (newSelectedUnit == null) // space is not occupied with another unit
                    {
                        //  normal movement                 
                        if (validMoves != null)
                        {
                            bool isMoveValid = false;
                            foreach (Tuple<int, int> tile in validMoves)
                            {
                                if (tile.Item1 == cursorIndexX && tile.Item2 == cursorIndexY)
                                {
                                    isMoveValid = true;
                                }
                            }
                            if (isMoveValid)
                            {
                                //clear highlights and move
                                clearValidTiles();
                                CurrentMap.moveUnit(this.selectedToMoveUnit, cursorIndexX, cursorIndexY);

                                ShowDirectAttackHighlights();
                                // This menu directly calls: 
                                //AfterMoveWait(), AfterMoveAttack(), AfterMoveWait()
                                TibzLog.Debug(selectedToMoveUnit.UnitLoadout.AvailableAttacks);
                                screenManager.addScreen(
                                  new AfterMovementMenu(
                                  this,
                                  MapTile.TILE_WIDTH_PX * cursorIndexX + scrollOffsetX,
                                   MapTile.TILE_HEIGHT_PX * cursorIndexY + scrollOffsetY,
                                  CursorQuadrant, true));

                            }
                            else
                            {
                                // Move is not valid, probably play a sound effect or something
                            }


                        }

                    }
                    else if (selectedToMoveUnit.internalID == newSelectedUnit.internalID)
                    {
                        clearValidTiles();
                        CurrentMap.moveUnit(this.selectedToMoveUnit, cursorIndexX, cursorIndexY);
                        // The same unit has been selected 
                        ShowDirectAttackHighlights();
                        // This menu calls: 
                        //AfterMoveWait(), AfterMoveAttack(), AfterMoveWait()
                        screenManager.addScreen(
                          new AfterMovementMenu(
                          this,
                          MapTile.TILE_WIDTH_PX * cursorIndexX + scrollOffsetX,
                           MapTile.TILE_HEIGHT_PX * cursorIndexY + scrollOffsetY,
                          CursorQuadrant, true));

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

                    if (validMoves != null)
                    {
                        clearValidTiles();
                    }

                    this.selectedToMoveUnit = null;

                }
            }
        }

        public override void ChildDraw()
        {
            // throw new NotImplementedException();
        }

        public List<Tuple<int, int>> GetValidMoveCoordinates(UnitObject unit, Tuple<int, int> coords)
        {
            List<Tuple<int, int>> validTiles = new List<Tuple<int, int>>();
            int x = coords.Item1;
            int y = coords.Item2;

            int range = unit.stats.Movement;

            // tree traversal time
            TibzLog.Debug(" - BM screen - ");
            TibzLog.Debug(unit.stats.movementType);
            MapMovementNode rootNode = new MapMovementNode(unit, CurrentMap, range, x, y);
            rootNode.GetMoveLocations(MapMovementNode.ParentNodeLocation.ROOT, ref validTiles);

            return validTiles;
        }

        public List<Tuple<int, int>> GetValidAttackHighlights(UnitObject unit, Tuple<int, int> coords)
        {
            List<Tuple<int, int>> validTiles = new List<Tuple<int, int>>();
            int x = coords.Item1;
            int y = coords.Item2;
            int range = unit.stats.Movement;
            MapMovementNode rootNode = new MapMovementNode(unit, CurrentMap, range, x, y);
            rootNode.getDirectAttackLocations(MapMovementNode.ParentNodeLocation.ROOT, ref validTiles);

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



        private void finishMovement()
        {

            this.selectedToMoveUnit.isTurnOver = true;
            this.selectedToMoveUnit = null;
        }

        public void AfterMoveWait()
        {
            ClearDirectAttackHighlights();
            finishMovement();
        }
        public void AfterMoveAttack()
        {
            ClearDirectAttackHighlights();
            finishMovement();
            MapScreens.BattleMapScreen.BattlePreviewScreen.confirmCallback attackPreviewCallback = delegate (bool confirmed) { AttackPreviewConfirmed(confirmed); };
            screenManager.addScreen(new MapScreens.BattleMapScreen.BattlePreviewScreen(ref attackPreviewCallback) );

        }

        public void AttackPreviewConfirmed(bool isConfirmed) {
            if (isConfirmed)
            {
             //   BattleLogic.RunBattle(selectedToMoveUnit,attackedUnit, selectedAttack, defenderAttack);
                BattleLogic.RunBattle(ref selectedToMoveUnit, ref attackedUnit, null, null);
                screenManager.addScreen(new MapScreens.BattleMapScreen.BattleAnimationScreen());
            }
         
        }


        public void AfterMoveItem()
        {
            ClearDirectAttackHighlights();
            finishMovement();

        }

        public void AfterMoveCancelled()
        {
            CurrentMap.moveUnit(this.selectedToMoveUnit, this.originalLocation.Item1, this.originalLocation.Item2);
            ShowAllMoveHighlights();
        }

        private void ShowAllMoveHighlights()
        {
            validAttacks = GetValidAttackHighlights(selectedToMoveUnit, new Tuple<int, int>(originalLocation.Item1, originalLocation.Item2));
            foreach (Tuple<int, int> tile in validAttacks)
            {
                CurrentMap.ChangeTileHighlight(tile.Item1, tile.Item2, TileHighlight.ATTACK);
            }


            validMoves = GetValidMoveCoordinates(selectedToMoveUnit, new Tuple<int, int>(originalLocation.Item1, originalLocation.Item2));
            foreach (Tuple<int, int> tile in validMoves)
            {
                CurrentMap.ChangeTileHighlight(tile.Item1, tile.Item2, TileHighlight.MOVEMENT);
            }
        }
        private void clearValidTiles()
        {

            foreach (Tuple<int, int> tile in validMoves)
            {
                CurrentMap.ChangeTileHighlight(tile.Item1, tile.Item2, TileHighlight.NONE);
            }

            foreach (Tuple<int, int> tile in validAttacks)
            {
                CurrentMap.ChangeTileHighlight(tile.Item1, tile.Item2, TileHighlight.NONE);
            }
            validMoves = null;
            validAttacks = null;
        }

        private void ShowDirectAttackHighlights()
        {
            List<Tuple<int, int>> tiles = new List<Tuple<int, int>>();
            MapMovementNode rootNode = new MapMovementNode(this.selectedToMoveUnit, CurrentMap, 0, this.selectedToMoveUnit.xPos, this.selectedToMoveUnit.yPos);
            rootNode.getDirectAttacksNoMovement(MapMovementNode.ParentNodeLocation.ROOT, ref tiles);
            foreach (Tuple<int, int> tile in tiles)
            {
                CurrentMap.ChangeTileHighlight(tile.Item1, tile.Item2, TileHighlight.ATTACK);
            }
        }
        private void ClearDirectAttackHighlights()
        {
            List<Tuple<int, int>> tiles = new List<Tuple<int, int>>();
            MapMovementNode rootNode = new MapMovementNode(this.selectedToMoveUnit, CurrentMap, 0, this.selectedToMoveUnit.xPos, this.selectedToMoveUnit.yPos);
            rootNode.getDirectAttacksNoMovement(MapMovementNode.ParentNodeLocation.ROOT, ref tiles);
            foreach (Tuple<int, int> tile in tiles)
            {
                CurrentMap.ChangeTileHighlight(tile.Item1, tile.Item2, TileHighlight.NONE);
            }

        }
    }
}