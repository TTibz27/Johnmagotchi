using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using static Johnmagotchi.GameContent.GlobalData;

namespace Johnmagotchi.GameContent.Objects.Johns
{
    public class TestJohn : AbstractJohn
    {
        Texture2D headTexture;
        Texture2D bodyTexture;

        public TestJohn() :base() {
            
        }   

        public override void Init(ScreenManager screenManager) {

            this.spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            this.headTexture = screenManager.contentRef.Load<Texture2D>("johnHeadPlaceholder");
            this.bodyTexture = screenManager.contentRef.Load<Texture2D>("johnBodyPlaceholder");
            this.gameData = screenManager.gameData;

        }
        public override void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            spriteBatch.Draw(bodyTexture, new Vector2(xPosition, yPosition + 32), Color.White);
            spriteBatch.Draw(headTexture, new Vector2(xPosition, yPosition), Color.White);
            spriteBatch.End();

        }

         public override void DrawAt(Vector2 pos)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            int x = (int)pos.X;
            int y = (int)pos.Y;

            Rectangle bodyRect = new Rectangle(x, y + width, width, width);
            Rectangle headRect = new Rectangle(x, y, width, width);

            spriteBatch.Draw(
                bodyTexture, bodyRect, null, Color.White, 0, new Vector2(0, 0),
                currentSpriteEffects, 1);
            spriteBatch.Draw(
               headTexture, headRect, null, Color.White, 0, new Vector2(0, 0),
               currentSpriteEffects, 1);

            spriteBatch.End();

        }
        public override void Update() {

            UpdateStatus();
            AddJohnPoints();
            if (johnState == JohnState.Walking)
            {
                animateHopWalk();

            }
            else if (johnState == JohnState.MoveTo)
            {
                Debug.WriteLine("MoveTo");
                if (moveTo(MoveToXPosition))
                {
                    johnState = JohnState.Waiting;
                }
            }

            if (johnState == JohnState.Waiting)
            {
                Debug.WriteLine("waiting");
            }

        
        }
        public override void Destroy() { }

        public override string GetDisplayName()
        {
            return "Test John";
        }
        public override string GetDisplayNameFirst()
        {
            return "Test";
        }
        public override string GetDisplayNameSecond()
        {
            return "John";
        }


        public override string getDescription(int lineNumber)
        {
            if (lineNumber == 0) return "This is John,has been cursed";
            else if (lineNumber == 1) return "he is only a placeholder";
            else if (lineNumber == 2) return "if you see I screwed up ";
            else return "";
        }

        public override int getShopCost()
        {
            return 1000;
        }
        public override JohnList getJohnEnum()
        {
            return JohnList.AccountantJohn;
        }

    }
}
