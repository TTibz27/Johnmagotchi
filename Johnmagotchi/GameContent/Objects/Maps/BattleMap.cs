using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Objects
{
    public class BattleMap
    {
        public int width { get; set; }
        public int height{ get; set; }

        public string serializedMapTiles{ get; set; }

      
        private MapTile [,] MapTileGrid;

        private SpriteBatch _spriteBatch;
        private ScreenManager _screenManager;
        private SpriteEffects currentSpriteEffects;  
        public Boolean OutlineEnabled;

        Texture2D _outlineTexture;

        public BattleMap(){
            this.height = 10;
            this.width =10;
           MapTileGrid = new MapTile[width,height];
        }
         public BattleMap(int width, int height) 
        { 
          this.height = height; // default min height (shooting for  360 x 640 res, 40x40 pixel grid)
           this.width = width;
           MapTileGrid = new MapTile[width,height];
      
        }

        public void Init(ScreenManager ScreenManager){
            this.OutlineEnabled = true;
            this._screenManager = ScreenManager;
            initArray();
            _spriteBatch = new SpriteBatch(_screenManager.GraphicsDevice);
            _outlineTexture = _screenManager.contentRef.Load<Texture2D>("Map-UI/outline-32");
        }


        public void InitFromReload(ScreenManager ScreenManager){
            this.OutlineEnabled = true;
            this._screenManager = ScreenManager;
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
        public void DrawMap(int xOffset, int yOffset){
            // this should probably be changed to only draw visible tiles instead of every single tile in the future
            for (int x =0; x < width; x++)
            {
                for (int y =0; y < height; y++)
                {
                    int xLocation = (x * MapTile.TILE_WIDTH_PX) + xOffset;
                    int yLocation = (y * MapTile.TILE_HEIGHT_PX)+ yOffset;
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
                        int xLocation = (x * MapTile.TILE_WIDTH_PX) + xOffset;
                        int yLocation = (y * MapTile.TILE_HEIGHT_PX)+ yOffset;
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
                if(y < height -1){MapTileGrid[x ,y+1].NorthNeighborType = newType;} //update bottom neighbor
        }

        public void serializeMapTiles(){
            List<string> serializedRows = new List<string>();
            for (int row = 0; row < this.height; row ++){
               
                List<MapTile> rowData = new List<MapTile>();
                for (int column =0; column < this.width; column++){
                   rowData.Add(  MapTileGrid[column, row]); // grid is width, height
                }
                 serializedRows.Add( JsonSerializer.Serialize(rowData));
            }
            serializedMapTiles = JsonSerializer.Serialize(serializedRows);
        }

        public void deserializeMapTiles(){
            System.Console.WriteLine("Deserializing Map Tiles...");
            MapTileGrid = new  MapTile[width,height];
             List<string> serializedRows = JsonSerializer.Deserialize<List<string>>(serializedMapTiles);
             for (int row= 0; row < serializedRows.Count; row++){
                string currentRow = serializedRows[row];
                List<MapTile> rowData =  JsonSerializer.Deserialize<List<MapTile>>(currentRow);
                  for (int column =0; column < rowData.Count; column++){
                    MapTile tile = rowData[column];
                    MapTileGrid[column, row] = tile;
                    MapTileGrid[column, row].Init(_screenManager);
                  }
             }
           //  initArray();
              System.Console.WriteLine("Deserialition Completed!");
        }
    }
}
