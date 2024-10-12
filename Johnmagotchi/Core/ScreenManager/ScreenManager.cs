using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Reflection.Metadata;
using System.Text;
using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TibzGame.Core.Inputs;

namespace TibzGame.Core.ScreenManager
{
     public class ScreenManager: DrawableGameComponent
    {
        private Stack<GameScreen> _screens = new Stack<GameScreen>();
        public GraphicsDeviceManager gfxDevRef;
        public ContentManager contentRef;
        public InputManager inputs;
        public GameScreen[] newScreenBuffer;
        public int removeScreenCount;
        public GlobalData gameData;



        //private const int SCALED_PIXELS_WIDTH = 640 * 100; // 640
        //private const int SCALED_PIXELS_HEIGHT = 360 * 100; // 360
        public static readonly int BASE_WORLD_UNIT_HEIGHT = 360;
        public static readonly int BASE_ZOOM_LEVEL = 100;

        public int CURRRENT_ZOOM_LEVEL = 100; // bigger number zooms out more.

        private int SCALED_PIXELS_HEIGHT;//= 360 * 100; // 360
        private int SCALED_PIXELS_WIDTH;// = 640 * 100;
    
        public ScreenManager(Game game, ref GraphicsDeviceManager gfxRef, ref ContentManager contentManagerRef, ref InputManager inputManager)
            : base(game)
        {         
            gfxDevRef = gfxRef;
            contentRef = contentManagerRef;
            inputs = inputManager;

           
            newScreenBuffer = new GameScreen[0];
            removeScreenCount = 0;
            gameData  = new GlobalData(this);// todo = this would be where we load in existing save game data


        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            // Update Screens
            base.Update(gameTime);
            bool updateBreak = false;
            foreach (GameScreen screen in _screens)
            {
                if (!updateBreak)
                {
                    screen.Update();

                }
                if (screen.isUpdatePriority) updateBreak = true;

            }

            //Remove Screens

            if (removeScreenCount > 0) 
            {
                for (int i = 0; i < removeScreenCount; i++)
                {
                    _screens.Pop().Destroy();
                }
                removeScreenCount = 0;

                if (newScreenBuffer.Length == 0) _screens.Peek().isTopScreen = true;
            }

            // Add new screens
            if (newScreenBuffer.Length > 0)
            {
                // since we are adding to a stack, we expect FILO
                // looping backwards will push to the stack in that same expected order if multiple screens are added at once
                for( int i =  newScreenBuffer.Length -1; i >=0; i--) 
                {
                    GameScreen newScreen = newScreenBuffer[i];
                    this._screens.Push(newScreen); 
                }
                //clear buffer
                this.newScreenBuffer = new GameScreen[0];

                //set top screen
                foreach( GameScreen scrn in _screens) {
                    scrn.isTopScreen = false;
                }
                _screens.Peek().isTopScreen = true;

            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            //we want to draw screens from lowest to highest in the main stack, so we use this stack to reverse drawing order
            Stack<GameScreen> DrawStack = new Stack<GameScreen>();
            foreach (GameScreen screen in _screens)
            {
                DrawStack.Push(screen);
                if (screen.isDrawPriority)
                {
                    break;
                }
            }

            foreach (GameScreen screen in DrawStack) 
            {
                screen.Draw();
            }
        }

        public void addScreen(GameScreen newScreen) {
            newScreenBuffer = new GameScreen[1];
            newScreenBuffer[0] = newScreen;
            newScreen.ScreenManager = this;
            newScreen.Init();
 
           
        }
        public void removeTopScreens(int screenCount) 
        {
            removeScreenCount = screenCount;
        }

        public void setWindowSize(ushort width, ushort height) 
        {
            //ushort[] widths = new ushort[] { 3840, 2560, 2560, 1920, 1366, 1280, 1280 };
            //ushort [] heights = new ushort[] { 2160, 1440, 1080, 1080, 768, 1024, 720 };

            gfxDevRef.PreferredBackBufferWidth = width;
            gfxDevRef.PreferredBackBufferHeight = height;

            // Apply the changes
            TibzLog.Debug("New resolution: {0} x {1}", gfxDevRef.PreferredBackBufferWidth, gfxDevRef.PreferredBackBufferHeight);

            gfxDevRef.ApplyChanges();

            RescaleWorldUnits();
            
        }


        public void setFullScreen(bool enabled) 
        {
            gfxDevRef.IsFullScreen = enabled;
            gfxDevRef.ApplyChanges();
            RescaleWorldUnits();
        }

        // 360 x 640 res
        public double GetScreenScaleX(){
            return  gfxDevRef.PreferredBackBufferWidth / (float)  SCALED_PIXELS_WIDTH;
        }

        public double GetScreenScaleY(){
            return gfxDevRef.PreferredBackBufferHeight/  (float) SCALED_PIXELS_HEIGHT;
        }

        public int getScaledIntX(int x) {
            return (int)(x * GetScreenScaleX());
        }
        public int getScaledIntY(int y) {
            return (int)(y * GetScreenScaleY());
        }    

        public Rectangle GetScaledRectangle(int posX, int posY, int width, int height){

                double scaleX = (float) gfxDevRef.PreferredBackBufferWidth / (float)  SCALED_PIXELS_WIDTH;
                double scaleY = (float) gfxDevRef.PreferredBackBufferHeight/  (float) SCALED_PIXELS_HEIGHT; 
                return new Rectangle(
                    Convert.ToInt32(Math.Floor(posX * scaleX)), //Round down the next tile position to reduce where gaps occur 
                    Convert.ToInt32(Math.Floor( posY * scaleY)),
                    Convert.ToInt32(Math.Ceiling(width * scaleX)), // Round up tile size to reduce gaps in tile map
                    Convert.ToInt32(Math.Ceiling(height * scaleY))
                );
        }

        public int GetScaledPixelScreenWidth(){
            return SCALED_PIXELS_WIDTH;
        }
              public int GetScaledPixelScreenHeight(){
            return SCALED_PIXELS_HEIGHT;
        }
        private void RescaleWorldUnits() {
            float heightWidthRatio = (float)gfxDevRef.PreferredBackBufferWidth / (float)gfxDevRef.PreferredBackBufferHeight;
            SCALED_PIXELS_HEIGHT = BASE_WORLD_UNIT_HEIGHT * CURRRENT_ZOOM_LEVEL; // 360
            SCALED_PIXELS_WIDTH = Convert.ToInt32(SCALED_PIXELS_HEIGHT * heightWidthRatio);
            TibzLog.Debug("Scaled Height: {0}, Scaled Width {1}", SCALED_PIXELS_HEIGHT, SCALED_PIXELS_WIDTH);
        }
    }
}
