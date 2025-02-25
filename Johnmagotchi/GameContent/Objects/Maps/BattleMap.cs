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
        public string serializedPlayerUnits { get; set; }
        public string serializedEnemyUnits{ get; set; }
        public string serializedNpcUnits { get; set; }


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

        public void Update()
        {
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

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    MapTileGrid[x,y].Update();
                }
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
        public void SerializeAll()
        {
            this.serializeMapTiles();
            this.SerializeUnitData();
            
        }

        public void SerializeUnitData() {
            List<string> player = new List<string>();
            List<string> enemy = new List<string>();
            List<string> npc = new List<string>();

            foreach (UnitObject unit in playerUnits) { 
            player.Add(unit.GetAsSerialized());
            }
            foreach (UnitObject unit in enemyUnits) {
                enemy.Add(unit.GetAsSerialized());
            }
            foreach (UnitObject unit in npcUnits)
            {
                npc.Add(unit.GetAsSerialized());
            }
            serializedPlayerUnits = JsonSerializer.Serialize(player);
            serializedEnemyUnits = JsonSerializer.Serialize(enemy);
            serializedNpcUnits = JsonSerializer.Serialize(npc);

            TibzLog.Debug("P:" + player.Count + " E:" + enemy.Count + " N:" + npc.Count);

        }
        public void DeserializeUnitData() {
            List<UnitObject> OutPlayerUnits = new List<UnitObject>();
            List<UnitObject> OutEnemyUnits = new List<UnitObject>();
            List<UnitObject> OutNpcUnits = new List<UnitObject>();

            List<string> players = JsonSerializer.Deserialize<List<string>>(serializedPlayerUnits);
            List<string> enemies = JsonSerializer.Deserialize<List<string>>(serializedEnemyUnits);
            List<string> npcs = JsonSerializer.Deserialize<List<string>>(serializedNpcUnits);

            foreach(string entry in players) {
                UnitObject newUnit = new UnitObject(entry);
                OutPlayerUnits.Add(newUnit); // makes new instance from serialized string
                newUnit.InitSprite(_screenManager, UnitObject.UnitTeam.PLAYER); 
            }
            foreach (string entry in enemies)
            {
                UnitObject newUnit = new UnitObject(entry);
                OutEnemyUnits.Add(newUnit); // makes new instance from serialized string
                newUnit.InitSprite(_screenManager, UnitObject.UnitTeam.ENEMY);
            }
            foreach (string entry in npcs)
            {
                UnitObject newUnit = new UnitObject(entry);
                OutNpcUnits.Add(newUnit); // makes new instance from serialized string
                newUnit.InitSprite(_screenManager,UnitObject.UnitTeam.NPC);
            }

            playerUnits = OutPlayerUnits;
            enemyUnits = OutEnemyUnits;
            npcUnits = OutNpcUnits;

            TibzLog.Debug("P:" + playerUnits.Count + " E:" + enemyUnits.Count + " N:" + npcUnits.Count);
        }

        public void AddPlayerUnit(UnitObject unitRef, int xpos, int ypos)
        {
            if (! CheckIfUnitCanBePlaced(unitRef, xpos, ypos))
            {
                return;
            }

            UnitObject unit = new UnitObject(unitRef);
            unit.SetShaderSet(UnitObject.SpriteShaderSets.PLAYER_NORMAL);
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
            }

            unit.InitSprite(_screenManager, UnitObject.UnitTeam.PLAYER);
        }
        public void AddEnemyUnit(UnitObject unitRef, int xpos, int ypos)
        {
            if (!CheckIfUnitCanBePlaced(unitRef, xpos, ypos))
            {
                return;
            }      
            UnitObject unit = new UnitObject(unitRef);
        
            unit.SetShaderSet(UnitObject.SpriteShaderSets.ENEMY_NORMAL);
            unit.xPos = xpos;
            unit.yPos = ypos;
            if (unit.isUnique == true)
            {
                //check then replace
                for (int i = 0; i < enemyUnits.Count; i++)
                {
                    if (enemyUnits[i].id == unit.id)
                    {
                        enemyUnits[i].xPos = xpos;
                        enemyUnits[i].yPos = ypos;
                        return; // dont add
                    }
                }
                enemyUnits.Add(unit); // else add
            }
            else
            {
                enemyUnits.Add(unit);
            }
            unit.InitSprite(_screenManager, UnitObject.UnitTeam.ENEMY);
        }

        public void AddNpcUnit(UnitObject unitRef, int xpos, int ypos) 
        {

            if (!CheckIfUnitCanBePlaced(unitRef, xpos, ypos))
            {
                return;
            }

            UnitObject unit = new UnitObject(unitRef);  
            unit.SetShaderSet(UnitObject.SpriteShaderSets.NPC_NORMAL);
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
            unit.InitSprite(_screenManager, UnitObject.UnitTeam.NPC);

            TibzLog.Debug("NPC obj count: {0}", npcUnits.Count);

        }

        public void deleteUnit(UnitObject unitRef) {

            int removePlayerIndex = -1;
            int removeEnemyIndex = -1;
            int removeNpcIndex = -1;

            for (int i = 0; i < playerUnits.Count; i++)
            {
                if (unitRef.internalID == playerUnits[i].internalID)
                {
                    removePlayerIndex = i;
                    break;
                }
            }
            for (int i = 0; i < enemyUnits.Count; i++)
            {
                if (unitRef.internalID == enemyUnits[i].internalID)
                {
                    removeEnemyIndex = i;
                    break;
                }
            }
            for (int i = 0; i < npcUnits.Count; i++)
            {
                if (unitRef.internalID == npcUnits[i].internalID)
                {
                    removeNpcIndex = i;
                    break;
                }
            }
            if (removePlayerIndex != -1) { playerUnits.RemoveAt(removePlayerIndex); }
            if (removeEnemyIndex != -1) { enemyUnits.RemoveAt(removeEnemyIndex); }
            if (removeNpcIndex != -1) { npcUnits.RemoveAt(removeNpcIndex); }
        }

        private bool CheckIfUnitCanBePlaced(UnitObject unit, int x, int y) {

            if (MapTileGrid[x,y].Type == TileType.SEA ) {  // add a && unit.Traversal type or whatever, so we check if this unit CAN stand on said tile.
                return false;
            }
            // else delete existing units if they overlap then return true

            return true;
        }


        public bool CheckIfAnyUnits(int x, int y) {
            return CheckIfPlayerUnits(x, y) || CheckIfEnemyUnits(x,y)|| CheckIfNpcUnits(x,y) ;
        }
        public bool CheckIfPlayerUnits(int CursorX, int CursorY)
        {
            foreach (UnitObject Unit in playerUnits) {
                if (Unit.xPos == CursorX && Unit.yPos == CursorY) { return true; }
            }
            return false;
        }
        public bool CheckIfEnemyUnits(int CursorX, int CursorY)
        {
            foreach (UnitObject Unit in enemyUnits)
            {
                if (Unit.xPos == CursorX && Unit.yPos == CursorY) { return true; }
            }
            return false;
        }
        public bool CheckIfNpcUnits(int CursorX, int CursorY)
        {
            foreach (UnitObject Unit in npcUnits)
            {
                if (Unit.xPos == CursorX && Unit.yPos == CursorY) { return true; }
            }
            return false;
        }
        public UnitObject getUnitAtLocation(int CursorX, int CursorY) {

            foreach (UnitObject Unit in playerUnits)
            {
                if (Unit.xPos == CursorX && Unit.yPos == CursorY) { return Unit; }
            }
            foreach (UnitObject Unit in enemyUnits)
            {
                if (Unit.xPos == CursorX && Unit.yPos == CursorY) { return Unit; }
            }
            foreach (UnitObject Unit in npcUnits)
            {
                if (Unit.xPos == CursorX && Unit.yPos == CursorY) { return Unit; }
            }
            return null;
        }

        public void moveUnit(UnitObject unitcopy, int x, int y)
        {
            foreach (UnitObject Unit in playerUnits)
            {
                if (Unit.internalID == unitcopy.internalID) 
                {
                   Unit.xPos = x;
                   Unit.yPos = y;
                }
            }
            foreach (UnitObject Unit in enemyUnits)
            {
                if (Unit.internalID == unitcopy.internalID) { }
            }
            foreach (UnitObject Unit in npcUnits)
            {
                if (Unit.internalID == unitcopy.internalID) { }
            }
        }

            public MapTile getTile(int x, int y) {
            if (x < 0 || y < 0) return null;
            if (x >= width || y >= height) return null;
            return MapTileGrid[x, y];
        }
        public void ChangeTileHighlight(int x, int y, TileHighlight highlightType)
        {
            MapTileGrid[x, y].SetTileHighlight(highlightType);
        }


        public BattleMap GetClone() { 
            BattleMap clone = new BattleMap(this.width, this.height);
            clone.Init(this._screenManager);
            this.SerializeAll();
       
            clone.serializedMapTiles = this.serializedMapTiles;
            clone.deserializeMapTiles();

            clone.serializedPlayerUnits = this.serializedPlayerUnits;
            clone.serializedNpcUnits = this.serializedNpcUnits;
            clone.serializedEnemyUnits = this.serializedEnemyUnits;
            clone.DeserializeUnitData();

            return clone;
        }

    }
}
