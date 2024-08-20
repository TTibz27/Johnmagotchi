using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TibzGame.Core.Inputs;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Objects
{
   
    public class MapCursor
    {
        private readonly int CURSOR_SIZE = 40;

        public int GridPositionX;
        public int GridPositionY;
    

        private SpriteBatch spriteBatch;
        private ScreenManager _screenManager;
      
        public SpriteEffects currentSpriteEffects;  
        Texture2D CursorTexture;

        public MapCursor()
        {
       
        }
        public void Init(ScreenManager screenManager)
        {
            _screenManager = screenManager;
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            CursorTexture = screenManager.contentRef.Load<Texture2D>("Map-UI/cursor");
        }

        public void Draw(int posX, int posY)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            // offset by 4
            Rectangle tileRect = _screenManager.GetScaledRectangle(posX -4, posY- 4, CURSOR_SIZE, CURSOR_SIZE); 
        
            
            spriteBatch.Draw(
                CursorTexture, tileRect, null, Color.White, 0, new Vector2(0, 0),
                currentSpriteEffects, 1);
    
            spriteBatch.End();
        }
    }
}