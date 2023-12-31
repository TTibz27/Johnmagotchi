﻿using Johnmagotchi.Screen.MainGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TibzGame.Core.Inputs;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi
{
    public class Johnmagotchi : Game
    {
        public GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        private ScreenManager _screenManager;
        public InputManager inputManager;
        public ContentManager contentRef;

        public MainGameScreen gameScrn;

        public Johnmagotchi()
        {
            contentRef = Content;
            graphics = new GraphicsDeviceManager(this);
            inputManager = new InputManager();
            _screenManager = new ScreenManager(this, ref graphics,  ref contentRef, ref inputManager); // pass a ref to gfx for screen size changes
           
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
           
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            Components.Add(_screenManager);
            _screenManager.setWindowSize(1280, 720);
            gameScrn = new MainGameScreen();
            _screenManager.addScreen(gameScrn);
          
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            inputManager.getInputs();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
