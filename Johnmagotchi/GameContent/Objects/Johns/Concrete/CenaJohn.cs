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
    internal class CenaJohn : AbstractJohn
    {

        Texture2D headTexture;
        Texture2D bodyTexture;
        Texture2D shadowTexture;


        public CenaJohn() : base()
        {
            shadowOffset = height + 12 - width / 2;
            baseJPM = 7000;
            this.status.baseJPM = baseJPM;
        }

        public override void Init(ScreenManager screenManager)
        {
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            headTexture = screenManager.contentRef.Load<Texture2D>("Johns/cena");
          
            shadowTexture = screenManager.contentRef.Load<Texture2D>("ShadowRegular");
            gameData = screenManager.gameData;
      
        }

        // Drawing functions
        public override void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            Rectangle shadowRect = new Rectangle(xPosition, yPosition + shadowOffset, width, width / 2);
            Rectangle bodyRect = new Rectangle(xPosition, yPosition + width, width, width);
            Rectangle headRect = new Rectangle(xPosition, yPosition, width, width);




            spriteBatch.Draw(shadowTexture, shadowRect, Color.White);
            /*spriteBatch.Draw(
                bodyTexture, bodyRect, null, Color.White, 0, new Vector2(0, 0),
                currentSpriteEffects, 1);
            spriteBatch.Draw(
               headTexture, headRect, null, Color.White, 0, new Vector2(0, 0),
               currentSpriteEffects, 1);*/

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
            return "John Cena";
        }

        public override string GetDisplayNameFirst()
        {
            return "John";
        }
        public override string GetDisplayNameSecond()
        {
            return "Cena";
        }

        // POKEDEX INFO
        public override string getDescription(int lineNumber)
        {
            if (lineNumber == 0) return "John Cena is a WWE superstar (tm).";
            else if (lineNumber == 1) return "I would say more about him, except I don't know much about him";
            else if (lineNumber == 2) return "outside of being a Wrestler, and the memes. Obviously.";
            else return "If I had the rights to his entrance song, I would play it on this screen right now.";
        }

        public override int getDescriptionHeight()
        {
            return 57;
        }


        ///SHOP INFO 
        public override int getShopCost()
        {
            return 50000;
        }
        public override JohnList getJohnEnum()
        {
            return JohnList.JohnCena;
        }
    }
}

