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
    internal class BathroomDoorProp
    {
        SpriteBatch spriteBatch;
        public Texture2D doorCloseTexture;
        public Texture2D doorOpenTexture;
        public Texture2D backdropTexture;
        public int xPos, yPos;
        public int JohnTargetXPosClosingDoor;
        public int JohnTargetXPosOpeningDoor;
        public int JohnTargetYPosClosingDoor;


        public BathroomDoorProp (ScreenManager screenManager)
        {
            yPos = 100 + 125;
            xPos = 500 + 320;
            JohnTargetXPosClosingDoor = xPos + 128 + 30;
            JohnTargetXPosOpeningDoor = JohnTargetXPosClosingDoor - 80;
            JohnTargetYPosClosingDoor = yPos + 50;

            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            doorCloseTexture = screenManager.contentRef.Load<Texture2D>("props/bathroomdoorclosed");
            doorOpenTexture = screenManager.contentRef.Load<Texture2D>("props/bathroomdooropen");
            backdropTexture = screenManager.contentRef.Load<Texture2D>("props/bathroomRealBG");

        }
        public void DrawClosedDoor()
        {
            Rectangle doorRect = new(xPos, yPos, 128 * 3, 128 * 3);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            spriteBatch.Draw(doorCloseTexture, doorRect, Color.White);
            spriteBatch.End();
        }
       
        public void DrawOpenDoor()
        {
            Rectangle door = new(xPos, yPos, 128 * 3, 128 * 3);

            int srcWidth = 224;
            int srcHeight = 311;

            //52 x 96 = dest width unscaled
            Rectangle sourceClip = new Rectangle(0, 0, srcWidth,srcHeight);
            Rectangle DestBackdrop = new Rectangle(xPos + (58*3) , yPos + (16*3), 52 *3 , 96 *3);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            spriteBatch.Draw(backdropTexture, DestBackdrop, sourceClip, Color.White);
            spriteBatch.Draw(doorOpenTexture, door, Color.White);
            spriteBatch.End();

        }
    }
}
