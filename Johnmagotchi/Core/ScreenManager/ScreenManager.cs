using System;
using System.Collections.Generic;
using System.Text;
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
        public ScreenManager(Game game, ref GraphicsDeviceManager gfxRef, ref ContentManager contentManagerRef, ref InputManager inputManager)
            : base(game)
        {
            gfxDevRef = gfxRef;
            contentRef = contentManagerRef;
            inputs = inputManager;
        
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
            base.Update(gameTime);
           
            foreach (GameScreen screen in _screens)
            {
                screen.Update();
                screen.isTopScreen = false;
            }
            _screens.Peek().isTopScreen = true;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (GameScreen screen in _screens)
            {
                screen.Draw();

                if (screen.isDrawPriority)
                {
                    break;
                }
            }
        }

        public void addScreen(GameScreen newScreen) {
            newScreen.ScreenManager = this;
            newScreen.Init();
 
            this._screens.Push(newScreen);
        }

        public void setWindowSize(ushort width, ushort height) {
            //ushort[] widths = new ushort[] { 3840, 2560, 2560, 1920, 1366, 1280, 1280 };
            //ushort [] heights = new ushort[] { 2160, 1440, 1080, 1080, 768, 1024, 720 };

            gfxDevRef.PreferredBackBufferWidth = width;
            gfxDevRef.PreferredBackBufferHeight = height;

            // Apply the changes
            gfxDevRef.ApplyChanges();

            System.Console.WriteLine("New resolution: {0} x {1}", gfxDevRef.PreferredBackBufferWidth, gfxDevRef.PreferredBackBufferHeight);

        }
    }
}
