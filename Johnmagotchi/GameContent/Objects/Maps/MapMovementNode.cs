using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
                    new MapMovementNode(selectedUnit, currentMap, getMovesRemaining(destTile), x, y - 1).GetMoveLocations(ParentNodeLocation.SOUTH, ref list);
                }
            }
            if (parent != ParentNodeLocation.EAST) // check east movement
            {
                MapTile destTile = currentMap.getTile(x + 1, y);
                if (destTile != null)
                {
                    new MapMovementNode(selectedUnit, currentMap, getMovesRemaining(destTile), x + 1, y).GetMoveLocations(ParentNodeLocation.WEST, ref list);
                }
                //new MapMovementNode(ParentNodeLocation.SOUTH, selectedUnit, getMovesRemaining(), x + 1, y).GetLocations(ref list);
            }
            if (parent != ParentNodeLocation.SOUTH) // check south movement
            {
                MapTile destTile = currentMap.getTile(x, y + 1);
                if (destTile != null)
                {
                    new MapMovementNode(selectedUnit, currentMap, getMovesRemaining(destTile), x, y + 1).GetMoveLocations(ParentNodeLocation.NORTH, ref list);
                }
            }
            if (parent != ParentNodeLocation.WEST) // check west movement
            {
                MapTile destTile = currentMap.getTile(x - 1, y);
                if (destTile != null)
                {
                    new MapMovementNode(selectedUnit, currentMap, getMovesRemaining(destTile), x - 1, y).GetMoveLocations(ParentNodeLocation.EAST, ref list);
                }
            }
            
            // all recursions should be completed, prune duplicates
            if (parent == ParentNodeLocation.ROOT) {
                 list = RemoveDuplicates(list);
            }
            return ;
        }

        private int getMovesRemaining(MapTile destTile) {
            // comare movement type to current tile type then return

            // note that the return value MUST be <= moves remaining, or else we risk infinite recursion
            return movesRemaining - 1;
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
    }

}
