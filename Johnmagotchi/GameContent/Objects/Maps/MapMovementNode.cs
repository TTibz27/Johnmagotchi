using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Logic;
using Johnmagotchi.GameContent.Units;
using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Johnmagotchi.GameContent.Objects.Maps
{
    public class MapMovementNode
    {
        public enum ParentNodeLocation {
            NORTH, EAST, SOUTH, WEST, ROOT
        }


        UnitObject selectedUnit;
        BattleMap currentMap;
        public int movesRemaining;
        public int x;
        public int y;
        public MapMovementNode(UnitObject selectedUnit, BattleMap currentMap, int moveRemaining, int x, int y) {

            this.selectedUnit = selectedUnit;
            this.currentMap = currentMap;
            this.movesRemaining = moveRemaining;
            this.x = x;
            this.y = y;
        }

        /// Passes a list by reference, recursively creates nodes to traverse tree and then assigns to list
        public void GetMoveLocations(ParentNodeLocation parent, ref List<Tuple<int, int>> list) {
         
            if (movesRemaining <= 0) return; // no more moves left, invalid
            if (x < 0 || x > currentMap.width || y < 0 || y > currentMap.height) return; // square is out of bounds, invalid

            //this square is valid, add to list
            list.Add(new Tuple<int, int>(x, y));
            // parent north means the unit movement is south, parent east means we moved west, etc 
            if (parent != ParentNodeLocation.NORTH)  // check north movement
            {
                MapTile destTile = currentMap.getTile(x, y - 1);
                if (destTile != null) {
                    new MapMovementNode(selectedUnit, currentMap, getMovesRemaining(destTile, x, y-1), x, y - 1).GetMoveLocations(ParentNodeLocation.SOUTH, ref list);
                }
            }
            if (parent != ParentNodeLocation.EAST) // check east movement
            {
                MapTile destTile = currentMap.getTile(x + 1, y);
                if (destTile != null)
                {
                    new MapMovementNode(selectedUnit, currentMap, getMovesRemaining(destTile, x+1 ,y), x + 1, y).GetMoveLocations(ParentNodeLocation.WEST, ref list);
                }
                //new MapMovementNode(ParentNodeLocation.SOUTH, selectedUnit, getMovesRemaining(), x + 1, y).GetLocations(ref list);
            }
            if (parent != ParentNodeLocation.SOUTH) // check south movement
            {
                MapTile destTile = currentMap.getTile(x, y + 1);
                if (destTile != null)
                {
                    new MapMovementNode(selectedUnit, currentMap, getMovesRemaining(destTile , x, y+1), x, y + 1).GetMoveLocations(ParentNodeLocation.NORTH, ref list);
                }
            }
            if (parent != ParentNodeLocation.WEST) // check west movement
            {
                MapTile destTile = currentMap.getTile(x - 1, y);
                if (destTile != null)
                {
                    new MapMovementNode(selectedUnit, currentMap, getMovesRemaining(destTile,x-1, y), x - 1, y).GetMoveLocations(ParentNodeLocation.EAST, ref list);
                }
            }
            
            // all recursions should be completed, prune duplicates
            if (parent == ParentNodeLocation.ROOT) {
                 list = RemoveDuplicates(list);
            }
            return ;
        }


        public void getDirectAttackLocations( ParentNodeLocation parent, ref List<Tuple<int, int>> list) {
            if (movesRemaining <= 0) return; // no more moves left, invalid
            if (x < 0 || x > currentMap.width || y < 0 || y > currentMap.height) return; // square is out of bounds, invalid

            //add valid attack squares to the  list
           // list.Add(new Tuple<int, int>(x, y));
            addAttackRangesToList( ref list);

            // parent north means the unit movement is south, parent east means we moved west, etc 
            if (parent != ParentNodeLocation.NORTH)  // check north movement
            {
                MapTile destTile = currentMap.getTile(x, y - 1);
                if (destTile != null)
                {
                    new MapMovementNode(selectedUnit, currentMap, getMovesRemaining(destTile, x, y - 1), x, y - 1).getDirectAttackLocations(ParentNodeLocation.SOUTH, ref list);
                }
            }
            if (parent != ParentNodeLocation.EAST) // check east movement
            {
                MapTile destTile = currentMap.getTile(x + 1, y);
                if (destTile != null)
                {
                    new MapMovementNode(selectedUnit, currentMap, getMovesRemaining(destTile, x + 1, y), x + 1, y).getDirectAttackLocations(ParentNodeLocation.WEST, ref list);
                }
                //new MapMovementNode(ParentNodeLocation.SOUTH, selectedUnit, getMovesRemaining(), x + 1, y).GetLocations(ref list);
            }
            if (parent != ParentNodeLocation.SOUTH) // check south movement
            {
                MapTile destTile = currentMap.getTile(x, y + 1);
                if (destTile != null)
                {
                    new MapMovementNode(selectedUnit, currentMap, getMovesRemaining(destTile, x, y + 1), x, y + 1).getDirectAttackLocations(ParentNodeLocation.NORTH, ref list);
                }
            }
            if (parent != ParentNodeLocation.WEST) // check west movement
            {
                MapTile destTile = currentMap.getTile(x - 1, y);
                if (destTile != null)
                {
                    new MapMovementNode(selectedUnit, currentMap, getMovesRemaining(destTile, x - 1, y), x - 1, y).getDirectAttackLocations(ParentNodeLocation.EAST, ref list);
                }
            }

            // all recursions should be completed, prune duplicates
            if (parent == ParentNodeLocation.ROOT)
            {
                list = RemoveDuplicates(list);
            }
            return;

        }

        public void getDirectAttacksNoMovement(ParentNodeLocation parent, ref List<Tuple<int, int>> list) 
        {
            addAttackRangesToList(ref list);
           
                list = RemoveDuplicates(list);
            
        }
        
        private int getMovesRemaining(MapTile destTile , int x, int y) {
            // check if occupied by another uint 
            UnitObject targetUnit = currentMap.getUnitAtLocation(x, y);
            if (targetUnit != null) {
                if (
                    (selectedUnit.team == UnitObject.UnitTeam.PLAYER && targetUnit.team == UnitObject.UnitTeam.ENEMY) ||
                    (selectedUnit.team == UnitObject.UnitTeam.ENEMY && (targetUnit.team == UnitObject.UnitTeam.PLAYER || targetUnit.team == UnitObject.UnitTeam.NPC))
                    )
                {
                    return 0;
                }              
            }

            // comare movement type to current tile type then return

          //  MovementCostTable.getMoveCost(selectedUnit.stats.movementType, destTile.Type);

            // note that the return value MUST be <= moves remaining, or else we risk infinite recursion
            return movesRemaining - MovementCostTable.getMoveCost(selectedUnit.stats.movementType, destTile.Type); 
        }

        private List<Tuple<int, int>> RemoveDuplicates( List<Tuple<int, int>> oldList)
        {
            List < Tuple<int, int> > newList = new List<Tuple<int, int>>();

            for (int i = 0; i < oldList.Count; i++)
            {
                bool isUnique = true;

                for (int j = 0; i < newList.Count; j++)
                {
                    if (oldList[i].Item1 == newList[j].Item1 && oldList[i].Item2 == newList[j].Item2)
                    {
                        isUnique = false;
                    }
                }

                if (isUnique)
                {
                    newList.Add(oldList[i]);
                    //TibzLog.Debug("added unique square");
                    //TibzLog.Debug(" x: " + oldList[i].Item1, " y: " + oldList[i].Item2);
                }
            }

            return newList;
        }

        private void addAttackRangesToList( ref List<Tuple<int, int>> list) {

            for (int i = selectedUnit.DirectAttackRangeMin; i <= selectedUnit.DirectAttackRangeMax; i++) {            
                getCircleRange(ref list, i);
            }
                
        }

        //gets one ring of an attack range at a time
        private void getCircleRange(ref List<Tuple<int, int>> list, int range) {
            for (int i = range; i > 0; i--) {
                int offset = range - i;
                addTileIfInBounds(ref list, x + offset, y + i);   // top to right
                addTileIfInBounds(ref list, x + i, y - offset);  //right to bot
                addTileIfInBounds(ref list, x - offset, y - i);  //bot to left
                addTileIfInBounds(ref list, x - i, y + offset);  //let to top
            }
        }

        private bool addTileIfInBounds(ref List<Tuple<int, int>> list, int newX, int newY) {
            if (newX < 0 || newX > currentMap.width || newY < 0 || newY > currentMap.height) {
              return false;
            }
            list.Add(new Tuple<int, int>(newX, newY));
            return true;
        }
    }
}
