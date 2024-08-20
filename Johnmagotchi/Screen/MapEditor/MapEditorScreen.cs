using Johnmagotchi.GameContent.Objects;
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

namespace Johnmagotchi.Screen.MapEditor
{
    public class MapEditorScreen : GameScreen
    {
        SpriteBatch SpriteBatch;
        private BattleMap CurrentMap;

        public MapCursor MapCursor;

         private int cursorIndexX;

        private int cursorIndexY;

        private readonly ulong CURSOR_HOLD_FRAMES = 25;
        private readonly int SCROLL_BOOST_TIME_FRAMES = 60;
        private readonly int CURSOR_SCROLL_NEXT_INC_FRAMES= 5;

        private int scrollTimer ; // frames until click to next 
        private int scrollTotalDuration;





        public MapEditorScreen(){
            this.CurrentMap = new BattleMap();
            this.MapCursor = new MapCursor();
        }
        public MapEditorScreen(BattleMap existingMap){
            this.CurrentMap = existingMap;
        }

        public override void Init()
        {
            scrollTimer =0;
            scrollTotalDuration =0;
            cursorIndexX = 1;
            cursorIndexY = 1;
            SpriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            this.CurrentMap.Init(screenManager);
            this.MapCursor.Init(screenManager);
        }


        public override void Destroy()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            this.CurrentMap.DrawMap();// should draw background
            //draw on top of background :
            // units
            // ui overlay
            //cursor
            this.MapCursor.Draw(MapTile.TILE_WIDTH_PX *cursorIndexX , MapTile.TILE_HEIGHT_PX * cursorIndexY);
        }

        public override void Update()
        {
            //Single Button Presses
            if (screenManager.inputs.menuInputs.navLeft.isJustPressed){
              TickCursorLeft();
            }
            if (screenManager.inputs.menuInputs.navRight.isJustPressed){
               TickCursorRight();
            }
            if (screenManager.inputs.menuInputs.navUp.isJustPressed){
               TickCursorUp();
            }
            if (screenManager.inputs.menuInputs.navDown.isJustPressed){
                TickCursorDown();
            }

            //Button Holds
            if (screenManager.inputs.menuInputs.navLeft.heldTime > CURSOR_HOLD_FRAMES)
            {
                IncrementScrollTimers();
                if (scrollTimer > CURSOR_SCROLL_NEXT_INC_FRAMES)
                {
                    scrollTimer = 0;
                    TickCursorLeft();
                }
              
            }
            if (screenManager.inputs.menuInputs.navRight.heldTime > CURSOR_HOLD_FRAMES)
            {
                IncrementScrollTimers();
                if (scrollTimer > CURSOR_SCROLL_NEXT_INC_FRAMES)
                {
                    scrollTimer = 0;
                    TickCursorRight();
                }
            }
            if (screenManager.inputs.menuInputs.navUp.heldTime > CURSOR_HOLD_FRAMES)
            {
                IncrementScrollTimers();
                if (scrollTimer > CURSOR_SCROLL_NEXT_INC_FRAMES)
                {
                    scrollTimer = 0;
                   TickCursorUp();
                }
            } 
            if (screenManager.inputs.menuInputs.navDown.heldTime > CURSOR_HOLD_FRAMES)
            {
                IncrementScrollTimers();
                if (scrollTimer > CURSOR_SCROLL_NEXT_INC_FRAMES)
                {
                    scrollTimer = 0;
                  TickCursorDown();
                }
            }  
            
            if (ScreenManager.inputs.menuInputs.navLeft.isJustReleased|| // reset scroll timers on release
            ScreenManager.inputs.menuInputs.navRight.isJustReleased||
            ScreenManager.inputs.menuInputs.navUp.isJustReleased||
            ScreenManager.inputs.menuInputs.navDown.isJustReleased)
            {
                scrollTimer = 0;
                scrollTotalDuration = 0;
            }
          
        }
        private void TickCursorLeft()
        {
            if (cursorIndexX > 0) cursorIndexX --;
            else cursorIndexX = 0;
        }
        private void TickCursorRight()
        {
            if (cursorIndexX < CurrentMap.width -1) cursorIndexX ++; // zero indexed so -1
            else cursorIndexX = CurrentMap.width -1;
        }
        private void TickCursorUp()
        {
            if (cursorIndexY > 0) cursorIndexY --;
            else cursorIndexY = 0;
        }
        private void TickCursorDown()
        {
            if (cursorIndexY < CurrentMap.height -1) cursorIndexY ++; // zero indexed so -1
            else cursorIndexY = CurrentMap.height -1;
        }
        private void IncrementScrollTimers()
        {
                scrollTimer++;
                scrollTotalDuration++;
                if (scrollTotalDuration > SCROLL_BOOST_TIME_FRAMES){
                   scrollTimer ++; // double the speed of scroll if held for over 70 frames
                }
        }
    }
}