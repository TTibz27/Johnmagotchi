using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Screens.BattleMapScreens.MapEditor
{
    internal class MapEditorInfoText
    {
        protected SpriteFont kemco;
        protected SpriteFont pixsplit;
        protected ScreenManager ScreenManager;
        protected SpriteBatch spriteBatch;

        public MapEditorInfoText() { 
           
        }
        public void Init(ScreenManager screenManager)
        {
            ScreenManager = screenManager;
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            kemco = ScreenManager.contentRef.Load<SpriteFont>("Fonts/Kemco-20");
            pixsplit = ScreenManager.contentRef.Load<SpriteFont>("Fonts/PixelSplitter");
        }
        public void Draw( int cursorQuadrant, int offsetX, int offsetY, string CurrentToolString)
        {

            Vector2 textRotationOrigin = kemco.MeasureString(CurrentToolString);
            textRotationOrigin.X = textRotationOrigin.X / 2;

            float x = textRotationOrigin.X - 40 ;
            int y = 40;
            y += offsetY;
            if (cursorQuadrant == 1 || cursorQuadrant ==2) {
                y = 700; // these values need to be adjust to match screen resolution
                y -= offsetY;
            }

            x += offsetX;
         
     
            Vector2 position = new Vector2(x,y);
         
           

            spriteBatch.Begin();

            // the hackiest white outline of all time.
            position = new Vector2(x+2, y+2);
            spriteBatch.DrawString(kemco, CurrentToolString, position, Color.White, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);
            position = new Vector2(x-2, y-2);
            spriteBatch.DrawString(kemco, CurrentToolString, position, Color.White, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);
            position = new Vector2(x+2, y-2);
            spriteBatch.DrawString(kemco, CurrentToolString, position, Color.White, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);
            position = new Vector2(x-2, y+2);
            spriteBatch.DrawString(kemco, CurrentToolString, position, Color.White, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);
            position = new Vector2(x+2, y);
            spriteBatch.DrawString(kemco, CurrentToolString, position, Color.White, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);
            position = new Vector2(x-2, y);
            spriteBatch.DrawString(kemco, CurrentToolString, position, Color.White, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);


            // the actual text
            position = new Vector2(x, y);
            spriteBatch.DrawString(kemco, CurrentToolString, position, Color.Black, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);


            spriteBatch.End();
        }
    }
}
