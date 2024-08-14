using System;
using Microsoft.Xna.Framework;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Objects
{
   
    public class MapTile
    {
        public TileType Type;
        public TileType NorthNeighborType;
        public TileType SouthNeighborType;
        public TileType EastNeighborType;
        public TileType WestNeighborType;

        public MapTile(){
         this.Type = TileType.GRASS;
        }

        public void ChangeType(TileType newType){
            this.Type = newType;
        }

        public void Draw(){

        }
    }
}