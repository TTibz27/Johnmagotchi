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
using Johnmagotchi.GameContent.Units;
using Johnmagotchi.Core.tools;
using System.Runtime.ExceptionServices;

namespace Johnmagotchi.GameContent.Objects
{
    public class BattleMap
    {
        public int width { get; set; }
        public int height{ get; set; }

        public string serializedMapTiles{ get; set; }

      
        private MapTile [,] MapTileGrid;

        private  List<UnitObject> playerUnits;
        private List<UnitObject> enemyUnits;
        private List<UnitObject> npcUnits;
        
        private SpriteBatch _spriteBatch;
        private ScreenManager _screenManager;
        private SpriteEffects currentSpriteEffects;  
        public Boolean OutlineEnabled;

        Texture2D _outlineTexture;

        public BattleMap(){
            buildMap(10,10);
        }
         public BattleMap(int width, int height) 
        { 
            buildMap(width,height);
        }

        public void buildMap(int w, int h){
           this.height = h; // default min height (shooting for  360 x 640 res, 40x40 pixel grid)
           this.width = w;
           MapTileGrid = new MapTile[width,height];
           this.playerUnits = new List<UnitObject>();
           this.npcUnits = new List<UnitObject>();
           this.enemyUnits = new List<UnitObject>();
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

        public void Update() {
            foreach (UnitObject unit in playerUnits)
            {
                unit.Update();
            }
            foreach (UnitObject unit in enemyUnits)
            {
                unit.Update();
            }
            foreach (UnitObject unit in npcUnits)
            {
                unit.Update();
            }
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
            // this could probably be changed to only draw visible tiles instead of every single tile in the future
            for (int x =0; x < width; x++)
            {
                for (int y =0; y < height; y++)
                {
                    int xLocation = (x * MapTile.TILE_WIDTH_PX) + xOffset;
                    int yLocation = (y * MapTile.TILE_HEIGHT_PX)+ yOffset;
                     MapTileGrid[x,y].DrawAt(xLocation, yLocation);
                   // TibzLog.Debug("drawing tile XPos: {0} , YPos: {1}", xLocation, yLocation );
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


        public void DrawUnits(int xOffset, int yOffset) {

            foreach(UnitObject unit in playerUnits) { 
                int xLocation = (unit.xPos * MapTile.TILE_WIDTH_PX) + xOffset;
                int yLocation = (unit.yPos * MapTile.TILE_HEIGHT_PX) + yOffset;
                unit.DrawAt( xLocation, yLocation);
            }
            foreach (UnitObject unit in enemyUnits)
            {
                int xLocation = (unit.xPos * MapTile.TILE_WIDTH_PX) + xOffset;
                int yLocation = (unit.yPos * MapTile.TILE_HEIGHT_PX) + yOffset;
                unit.DrawAt( xLocation, yLocation);
            }
            foreach (UnitObject unit in npcUnits)
            {
                int xLocation = (unit.xPos * MapTile.TILE_WIDTH_PX) + xOffset;
                int yLocation = (unit.yPos * MapTile.TILE_HEIGHT_PX) + yOffset;
                unit.DrawAt(xLocation, yLocation);
            }
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

        public void AddPlayerUnit(UnitObject unitRef, int xpos, int ypos)
        {
            UnitObject unit = new UnitObject(unitRef);

            unit.xPos = xpos;
            unit.yPos = ypos;
            if (unit.isUnique == true)
            {
                //check then replace
                for (int i = 0; i < playerUnits.Count; i++)
                {
                    if (playerUnits[i].id == unit.id )
                    {          
                        playerUnits[i].xPos = xpos;
                        playerUnits[i].yPos = ypos;
                        return; // dont add
                    }
                }
                playerUnits.Add(unit); // else add
            }
            else {
                playerUnits.Add(unit);
                TibzLog.Debug(playerUnits.Count);
            }

            unit.InitSprite(_screenManager);
            unit.setShaderSet(UnitObject.SpriteShaderSets.PLAYER_NORMAL);
        }
        public void AddEnemyUnit(UnitObject unitRef, int xpos, int ypos)
        {

            UnitObject unit = new UnitObject(unitRef);
            unit.xPos = xpos;
            unit.yPos = ypos;
            if (unit.isUnique == true)
            {
                //check then replace
                for (int i = 0; i < enemyUnits.Count; i++)
                {
                    if (enemyUnits[i].id == unit.id)
                    {
                        TibzLog.Debug("updating existing unique unit -  x:{0}, y: {1}", unit.xPos, unit.yPos);
                        enemyUnits[i].xPos = xpos;
                        enemyUnits[i].yPos = ypos;
                        return; // dont add
                    }
                }
                TibzLog.Debug("adding new unique unit -  x:{0}, y: {1}", unit.xPos, unit.yPos);
                enemyUnits.Add(unit); // else add
            }
            else
            {
                TibzLog.Debug("adding new non-unique unit -  x:{0}, y: {1}", unit.xPos, unit.yPos);
                enemyUnits.Add(unit);
            }
            unit.InitSprite(_screenManager);
            unit.setShaderSet(UnitObject.SpriteShaderSets.ENEMY_NORMAL);

            TibzLog.Debug("Enemy obj count: {0}", enemyUnits.Count);
        }
        public void AddNpcUnit(UnitObject unitRef, int xpos, int ypos) 
        {

            UnitObject unit = new UnitObject(unitRef);
            unit.xPos = xpos;
            unit.yPos = ypos;

            if (unit.isUnique == true)
            {
                //check then replace
                for (int i = 0; i < npcUnits.Count; i++)
                {
                    if (npcUnits[i].id == unit.id)
                    {
                        enemyUnits[i] = unit;
                        return; // dont add
                    }
                }
                npcUnits.Add(unit); // else add
            }
            else
            {
                npcUnits.Add(unit);
            }
            unit.InitSprite(_screenManager);
            unit.setShaderSet(UnitObject.SpriteShaderSets.NPC_NORMAL);
            TibzLog.Debug("NPC obj count: {0}", npcUnits.Count);

        }
    }
}
