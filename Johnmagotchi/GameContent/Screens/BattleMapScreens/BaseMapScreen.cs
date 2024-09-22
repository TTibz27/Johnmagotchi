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
using Johnmagotchi.GameContent.Objects;
using System.Text.Json;

namespace Johnmagotchi.Screen.BattleMapScreens
{
    public abstract class BaseMapScreen : GameScreen
    {
         SpriteBatch SpriteBatch;
        protected BattleMap CurrentMap;

        public MapCursor MapCursor;

        protected int cursorIndexX;

        protected int cursorIndexY;

        protected int scrollOffsetX;
        protected int scrollOffsetY;

        private readonly ulong CURSOR_HOLD_FRAMES = 25;
        private readonly int SCROLL_BOOST_TIME_FRAMES = 60;
        private readonly int CURSOR_SCROLL_NEXT_INC_FRAMES= 5;

        private readonly int SCROLL_TILES_FROM_EDGE_X = 3;
        private readonly int SCROLL_TILES_FROM_EDGE_Y = 2;

        private int scrollTimer ; // frames until click to next 
        private int scrollTotalDuration;

        private string tempSave;

        public Boolean ScreenScrollLock = false;


        public BaseMapScreen(){
            this.CurrentMap = new BattleMap(24,20);
            this.MapCursor = new MapCursor();
            saveCurrentMap();
        }
        public BaseMapScreen(BattleMap existingMap){
            this.CurrentMap = existingMap;
        }

        public override void Init()
        {
            scrollTimer =0;
            scrollTotalDuration =0;
            cursorIndexX = 2;
            cursorIndexY = 2;
            scrollOffsetX=0;
            scrollOffsetY=0;

            SpriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            this.CurrentMap.Init(screenManager);
            this.MapCursor.Init(screenManager);

            ChildInit();
        }
        public abstract void ChildInit();

        public override void Destroy()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
        
           // bool stopXScroll = false;
            //bool stopYScroll = false;
            if ((this.CurrentMap.width * MapTile.TILE_WIDTH_PX) < this.ScreenManager.GetScaledPixelScreenWidth()) { // center width
                scrollOffsetX = (this.ScreenManager.GetScaledPixelScreenWidth() - (this.CurrentMap.width * MapTile.TILE_WIDTH_PX))/2;
             //   stopXScroll = true;
            }
            if ((this.CurrentMap.height * MapTile.TILE_HEIGHT_PX) < this.ScreenManager.GetScaledPixelScreenHeight()){ // center height
                scrollOffsetY = (this.ScreenManager.GetScaledPixelScreenHeight() - (this.CurrentMap.height * MapTile.TILE_HEIGHT_PX))/2;
            //    stopYScroll = true;
            }

           
            this.CurrentMap.DrawMap(scrollOffsetX, scrollOffsetY);// should draw background
         
            //draw on top of background :
            //cursor
            this.MapCursor.Draw( (MapTile.TILE_WIDTH_PX *cursorIndexX) + scrollOffsetX , (MapTile.TILE_HEIGHT_PX * cursorIndexY) + scrollOffsetY);
            // units
            // ui overlay 
        }

        public override void Update()
        {
            //Single Button Presses
            if (screenManager.inputs.editorInputs.navLeft.isJustPressed){
              TickCursorLeft();
            }
            if (screenManager.inputs.editorInputs.navRight.isJustPressed){
               TickCursorRight();
            }
            if (screenManager.inputs.editorInputs.navUp.isJustPressed){
               TickCursorUp();
            }
            if (screenManager.inputs.editorInputs.navDown.isJustPressed){
                TickCursorDown();
            }

          

           if(screenManager.inputs.editorInputs.saveButton.isJustPressed){
                saveCurrentMap();
            }
            if(screenManager.inputs.editorInputs.loadButton.isJustPressed){
                loadCurrentMap();
            }


            //Button Holds
            if (screenManager.inputs.editorInputs.navLeft.heldTime > CURSOR_HOLD_FRAMES)
            {
                IncrementScrollTimers();
                if (scrollTimer > CURSOR_SCROLL_NEXT_INC_FRAMES)
                {
                    scrollTimer = 0;
                    TickCursorLeft();
                }
              
            }
            if (screenManager.inputs.editorInputs.navRight.heldTime > CURSOR_HOLD_FRAMES)
            {
                IncrementScrollTimers();
                if (scrollTimer > CURSOR_SCROLL_NEXT_INC_FRAMES)
                {
                    scrollTimer = 0;
                    TickCursorRight();
                }
            }
            if (screenManager.inputs.editorInputs.navUp.heldTime > CURSOR_HOLD_FRAMES)
            {
                IncrementScrollTimers();
                if (scrollTimer > CURSOR_SCROLL_NEXT_INC_FRAMES)
                {
                    scrollTimer = 0;
                   TickCursorUp();
                }
            } 
            if (screenManager.inputs.editorInputs.navDown.heldTime > CURSOR_HOLD_FRAMES)
            {
                IncrementScrollTimers();
                if (scrollTimer > CURSOR_SCROLL_NEXT_INC_FRAMES)
                {
                    scrollTimer = 0;
                  TickCursorDown();
                }
            }  
       


            
            if (ScreenManager.inputs.editorInputs.navLeft.isJustReleased|| // reset scroll timers on release
            ScreenManager.inputs.editorInputs.navRight.isJustReleased||
            ScreenManager.inputs.editorInputs.navUp.isJustReleased||
            ScreenManager.inputs.editorInputs.navDown.isJustReleased)
            {
                scrollTimer = 0;
                scrollTotalDuration = 0;
            }


            ChildUpdate();
            
          
        }

        public abstract void ChildUpdate();
        private void TickCursorLeft()
        {
            if (cursorIndexX > 0) cursorIndexX --;
            else cursorIndexX = 0;
                 // adjust screen after mouse movement
            tickLeftScroll();
        }
        private void TickCursorRight()
        {
            if (cursorIndexX < CurrentMap.width -1) cursorIndexX ++; // zero indexed so -1
            else cursorIndexX = CurrentMap.width -1;
                 // adjust screen after mouse movement
            tickRightScroll();
        }
        private void TickCursorUp()
        {
            if (cursorIndexY > 0) cursorIndexY --;
            else cursorIndexY = 0;
                 // adjust screen after mouse movement
             tickUpScroll();
        }
        private void TickCursorDown()
        {
            if (cursorIndexY < CurrentMap.height -1) cursorIndexY ++; // zero indexed so -1
            else cursorIndexY = CurrentMap.height -1;
                 // adjust screen after mouse movement
            tickDownScroll();
        }
        private void IncrementScrollTimers()
        {
                scrollTimer++;
                scrollTotalDuration++;
                if (scrollTotalDuration > SCROLL_BOOST_TIME_FRAMES){
                   scrollTimer ++; // double the speed of scroll if held for over 70 frames
                }
              
        }

        private void tickLeftScroll(){
            if (ScreenScrollLock){ return; }
            int currentOffsetTilesX = - scrollOffsetX / MapTile.TILE_WIDTH_PX; // since the offset scrolling left is negative we need to invert this sign
            int currentOffsetTilesY = scrollOffsetY / MapTile.TILE_HEIGHT_PX;
            int totalWidthTiles =  this.screenManager.GetScaledPixelScreenWidth() / MapTile.TILE_WIDTH_PX;
            int TilesFromLeftEdge =  cursorIndexX - currentOffsetTilesX;
              
               System.Console.WriteLine("Scroll offset px: {0}", scrollOffsetX);
           
            if (  TilesFromLeftEdge < SCROLL_TILES_FROM_EDGE_X)
            {
                scrollOffsetX += MapTile.TILE_WIDTH_PX; // scroll left
            }
            if (scrollOffsetX >= 0 ) // scroll limit
            {
                scrollOffsetX = 0;
            }
        }
        private void tickRightScroll(){
            if (ScreenScrollLock){ return; }
            int currentOffsetTilesX = - scrollOffsetX / MapTile.TILE_WIDTH_PX; // invert sign here
            int currentOffsetTilesY = scrollOffsetY / MapTile.TILE_HEIGHT_PX;
            int totalWidthTiles =  this.screenManager.GetScaledPixelScreenWidth() / MapTile.TILE_WIDTH_PX;
            int TilesFromLeftEdge = cursorIndexX - currentOffsetTilesX;
               System.Console.WriteLine("Scroll offset px: {0}", scrollOffsetX);

                System.Console.WriteLine("Scroll offset limit: {0}", -1* (CurrentMap.width * MapTile.TILE_WIDTH_PX - this.screenManager.GetScaledPixelScreenWidth()));

       
            if (  TilesFromLeftEdge >=  totalWidthTiles - SCROLL_TILES_FROM_EDGE_X)
            {
                scrollOffsetX -= MapTile.TILE_WIDTH_PX; // scroll right
            }
            if (scrollOffsetX <= -1* (CurrentMap.width * MapTile.TILE_WIDTH_PX - this.screenManager.GetScaledPixelScreenWidth()))
            {
                scrollOffsetX =  -1* (CurrentMap.width * MapTile.TILE_WIDTH_PX - this.screenManager.GetScaledPixelScreenWidth());
            }
        }
        private void tickUpScroll(){
            if (ScreenScrollLock){ return; }
            int currentOffsetTilesY = - scrollOffsetY / MapTile.TILE_HEIGHT_PX;
            int totalHeightTiles = this.screenManager.GetScaledPixelScreenHeight() / MapTile.TILE_HEIGHT_PX;
            int tilesFromTop =  cursorIndexY - currentOffsetTilesY;
            //System.Console.WriteLine("Scroll offset px: {0}", scrollOffsetY);
            System.Console.WriteLine("Scroll offset tiles: {0}", currentOffsetTilesY);
            System.Console.WriteLine("Tiles from top: {0}", tilesFromTop);
            if (  tilesFromTop < SCROLL_TILES_FROM_EDGE_Y){
                scrollOffsetY += MapTile.TILE_HEIGHT_PX; // scroll up
            }
             if (scrollOffsetY >= 0 )
            {
                scrollOffsetY = 0;
            }
        }
        private void tickDownScroll(){
            if (ScreenScrollLock){ return; }
            int currentOffsetTilesY = - scrollOffsetY / MapTile.TILE_HEIGHT_PX;
            int totalHeightTiles = this.screenManager.GetScaledPixelScreenHeight() / MapTile.TILE_HEIGHT_PX;
            int tilesFromTop =  cursorIndexY - currentOffsetTilesY;
           // System.Console.WriteLine("Scroll offset px: {0}", scrollOffsetY);
               System.Console.WriteLine("Scroll offset tiles: {0}", currentOffsetTilesY);
              System.Console.WriteLine("Tiles from top: {0}", tilesFromTop);

           
            if ( tilesFromTop >  totalHeightTiles  - SCROLL_TILES_FROM_EDGE_Y){
                scrollOffsetY -= MapTile.TILE_HEIGHT_PX; // scroll up
            }
             if (scrollOffsetY <= -1* (CurrentMap.height * MapTile.TILE_HEIGHT_PX - this.screenManager.GetScaledPixelScreenHeight()))
            {
                scrollOffsetY =  -1* (CurrentMap.height * MapTile.TILE_HEIGHT_PX - this.screenManager.GetScaledPixelScreenHeight());
            }  
        }




        public void saveCurrentMap(){  
            System.Console.WriteLine("Saving....");
            CurrentMap.serializeMapTiles();
            tempSave = JsonSerializer.Serialize(CurrentMap);
          // System.Console.WriteLine("save json: {0}", tempSave);
        }

        public void loadCurrentMap(){
            if (tempSave.Length > 10){
                System.Console.WriteLine("Loading....");
                this.CurrentMap = JsonSerializer.Deserialize<BattleMap>(tempSave);
                this.CurrentMap.InitFromReload(screenManager);
                this.CurrentMap.deserializeMapTiles();
            }
             else{
                  System.Console.WriteLine("valid save state not found");
             }
           
        }
    }
}