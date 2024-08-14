using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Objects
{
    public class BattleMap
    {
        public int width;
        public int height;
      
        public MapTile [,] MapTileGrid;

        private ScreenManager ScreenManager;

        public BattleMap() 
        { 
           this.height = 9; // default min height (shooting for  360 x 640 res, 40x40 pixel grid)
           this.width = 16;
           MapTileGrid = new MapTile[width,height];
           initArray();
        }
         public BattleMap(int width, int height) 
        { 
          this.height = height; // default min height (shooting for  360 x 640 res, 40x40 pixel grid)
           this.width = width;
           MapTileGrid = new MapTile[height,width];
           initArray();
        }

        public void Init(ScreenManager ScreenManager){
            this.ScreenManager = ScreenManager;
        }

        public void initArray(){
            for (int x =0; x < height; x++){
                for (int y =0; y < height; y++){
                    MapTileGrid[x,y]= new MapTile();
                }
            }
        }
        public void DrawMap(){

        }

    }
}
