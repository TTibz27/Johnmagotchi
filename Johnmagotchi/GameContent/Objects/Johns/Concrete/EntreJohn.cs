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
    internal class EntreJohn : AbstractJohn
    {

        Texture2D headTexture;
        Texture2D bodyTexture;
        Texture2D shadowTexture;


        public EntreJohn() : base()
        {
            shadowOffset = height + 12 - width / 2;
            baseJPM = 3500;
        }

        public override void Init(ScreenManager screenManager)
        {
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            headTexture = screenManager.contentRef.Load<Texture2D>("Johns/EntreJohn");
           
            shadowTexture = screenManager.contentRef.Load<Texture2D>("ShadowRegular");
            gameData = screenManager.gameData;
            this.status.baseJPM = baseJPM;
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
            return "Johntrepreneur";
        }

        public override string GetDisplayNameFirst()
        {
            return "Johntrepreneur";
        }
        public override string GetDisplayNameSecond()
        {
            return "";
        }

        // POKEDEX INFO
        public override string getDescription(int lineNumber)
        {
            if (lineNumber == 0) return "Some Johns work for the corporate machine. Not this John. He owns crypto.";
            else if (lineNumber == 1) return "He is a motivated self-starter. He is his own boss. He is making his dreams real. ";
            else if (lineNumber == 2) return "He wants you to know that YOU TOO can get in on the ground floor!";
            else return "Join his team, sell some essential oil, and make some passive income.";
        }

        public override int getDescriptionHeight()
        {
            return 57;
        }


        ///SHOP INFO 
        public override int getShopCost()
        {
            return 25000;
        }
        public override JohnList getJohnEnum()
        {
            return JohnList.Johntrepreneur;
        }
    }
}

