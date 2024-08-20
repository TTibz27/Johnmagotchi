using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Objects
{
    public class BattleMap
    {
        public int width;
        public int height;

      
        public MapTile [,] MapTileGrid;
        private SpriteBatch _spriteBatch;
        private ScreenManager _screenManager;
        private SpriteEffects currentSpriteEffects;  
        public Boolean OutlineEnabled;

        Texture2D _outlineTexture;

        public BattleMap() 
        { 
           this.height = 12; // default min height (shooting for  360 x 640 res, 9x16 = 40x40 pixel grid | 10x18 = 36x36px grid | 11.25 x 20 = 32x32 px grid)
           this.width = 20;
           MapTileGrid = new MapTile[width,height];
        }
         public BattleMap(int width, int height) 
        { 
          this.height = height; // default min height (shooting for  360 x 640 res, 40x40 pixel grid)
           this.width = width;
           MapTileGrid = new MapTile[height,width];
      
        }

        public void Init(ScreenManager ScreenManager){
            this.OutlineEnabled = true;
            this._screenManager = ScreenManager;
            initArray();
            _spriteBatch = new SpriteBatch(_screenManager.GraphicsDevice);
            _outlineTexture = _screenManager.contentRef.Load<Texture2D>("Map-UI/outline-32");
        }

        public void initArray(){
            // init tiles
            for (int x =0; x < width; x++)
            {
                for (int y =0; y < height; y++)
                {
                    MapTileGrid[x,y] = new MapTile();
                    MapTileGrid[x,y].Init(_screenManager);
                }
            }
            //set neighbors
            for (int x =0; x < width; x++)
            {
                for (int y =0; y < height; y++)
                {
                    if (x -1 >= 0)  MapTileGrid[x,y].WestNeighborType = MapTileGrid[x-1,y].Type;
                    if (x+1 < height)  MapTileGrid[x,y].WestNeighborType = MapTileGrid[x+1,y].Type;
                    if (y -1 >= 0)  MapTileGrid[x,y].WestNeighborType = MapTileGrid[x,y-1].Type;
                    if (y+1 < height)  MapTileGrid[x,y].WestNeighborType = MapTileGrid[x,y+1].Type;
                }
            }
        }
        public void DrawMap(){
            // this should probably be changed to only draw visible tiles instead of every single tile in the future
            for (int x =0; x < width; x++)
            {
                for (int y =0; y < height; y++)
                {
                    int xLocation = (x * MapTile.TILE_WIDTH_PX);
                    int yLocation = (y * MapTile.TILE_HEIGHT_PX);
                     MapTileGrid[x,y].DrawAt(xLocation, yLocation);
                }
            }


// ADD IN an if statment, 'IF OUTLINE IS ENABLED'
            if (this.OutlineEnabled == true)
            {
                _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
                for (int x =0; x < width; x++)
                {
                    for (int y =0; y < height; y++)
                    {
                        int xLocation = (x * MapTile.TILE_WIDTH_PX);
                        int yLocation = (y * MapTile.TILE_HEIGHT_PX);
                        this.DrawOutlineSquare(xLocation, yLocation);
                    }
                }
                _spriteBatch.End();
            }
        }

        private void DrawOutlineSquare(int posX, int posY)
        {
            // all rectangles should do the scaling from world coordinates to screen coordinates
            Rectangle tileRect = _screenManager.GetScaledRectangle(posX, posY, MapTile.TILE_WIDTH_PX, MapTile.TILE_HEIGHT_PX);

            _spriteBatch.Draw(
                _outlineTexture, tileRect, null, Color.White, 0, new Vector2(0, 0),
                currentSpriteEffects, 1);
        }

        public void ChangeTileType(int x, int y, TileType newType)
        {
                MapTileGrid[x,y].ChangeType(newType);
                if(x > 0){MapTileGrid[x-1,y].EastNeighborType = newType;} //update left neighbor
                if(x < width -1){MapTileGrid[x +1,y].WestNeighborType = newType;} //update right neighbor
                if(y > 0){MapTileGrid[x,y-1].SouthNeighborType = newType;} //update top neighbor
                if(x < height -1){MapTileGrid[x ,y+1].NorthNeighborType = newType;} //update bottom neighbor
        }
    }
}
