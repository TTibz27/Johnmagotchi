using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;
using static Johnmagotchi.GameContent.GlobalData;

namespace Johnmagotchi.GameContent.Objects.Johns.Concrete
{
    internal class SportsJohn : AbstractJohn
    {

        Texture2D headTexture;
        Texture2D bodyTexture;
        Texture2D shadowTexture;


        public SportsJohn() : base()
        {
            shadowOffset = height + 12 - width / 2;
            baseJPM = 3000;
            this.status.baseJPM = baseJPM;
        }

        public override void Init(ScreenManager screenManager)
        {
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            headTexture = screenManager.contentRef.Load<Texture2D>("Johns/sportsJohn");
            bodyTexture = screenManager.contentRef.Load<Texture2D>("BaseJohnBody");
            shadowTexture = screenManager.contentRef.Load<Texture2D>("ShadowRegular");
            gameData = screenManager.gameData;
        }

        // Drawing functions
        public override void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            Rectangle shadowRect = new Rectangle(xPosition, yPosition + shadowOffset, width, width / 2);
            Rectangle headRect = new Rectangle(xPosition, yPosition, width, height);

            spriteBatch.Draw(shadowTexture, shadowRect, Color.White);
            spriteBatch.Draw(
               headTexture, headRect, null, Color.White, 0, new Vector2(0, 0),
               currentSpriteEffects, 1);

            spriteBatch.End();
        }
        public override void DrawAt(Vector2 pos)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            int x = (int)pos.X;
            int y = (int)pos.Y;

            Rectangle headRect = new Rectangle(x, y, width, height);

            spriteBatch.Draw(
               headTexture, headRect, null, Color.White, 0, new Vector2(0, 0),
               currentSpriteEffects, 1);

            spriteBatch.End();
        }

        //DISPLAY NAMES
        public override string GetDisplayName()
        {
            return "Sports John";
        }

        public override string GetDisplayNameFirst()
        {
            return "Sports";
        }
        public override string GetDisplayNameSecond()
        {
            return "John";
        }

        // POKEDEX INFO
        public override string getDescription(int lineNumber)
        {
            if (lineNumber == 0) return "Sports John! Sports John! Sports John!";
            else if (lineNumber == 1) return "He goes to any sports game within 40 miles, just to chant. He is such a mega fan";
            else if (lineNumber == 2) return " that knows the box score for any Bears game in the last decade.";
            else return "Poor Guy.";
        }

        public override int getDescriptionHeight()
        {
            return 57;
        }


        ///SHOP INFO 
        public override int getShopCost()
        {
            return 16000;
        }
        public override JohnList getJohnEnum()
        {
            return JohnList.SportsJohn;
        }
    }
}
