using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Objects
{
    internal class BedProp
    {
        SpriteBatch spriteBatch;
        public Texture2D texture;
        public Texture2D frontTexture;
        public int xPos, yPos;
        public int JohnTargetXPos;
       

        public BedProp(ScreenManager screenManager) {
            yPos = 100 + 225;
            xPos = 500 + 320;
            JohnTargetXPos = xPos + 128 + 30 ;

            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            texture = screenManager.contentRef.Load<Texture2D>("props/bedwhole");
            frontTexture = screenManager.contentRef.Load<Texture2D>("props/bedfront");

        }
        public void DrawBackLayer() {
            Rectangle foodRect0 = new(xPos, yPos, 128 * 3, 128 * 3);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            spriteBatch.Draw(texture, foodRect0, Color.White);
            spriteBatch.End();
        }
        public void DrawFrontLayer() {
            Rectangle foodRect0 = new(xPos, yPos, 128 * 3, 128 * 3);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            spriteBatch.Draw(frontTexture, foodRect0, Color.White);
            spriteBatch.End();

        }
    }
}
