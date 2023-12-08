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
    internal class NerdJohn : AbstractJohn
    {

        Texture2D headTexture;
        Texture2D bodyTexture;
        Texture2D shadowTexture;


        public NerdJohn() : base()
        {
            shadowOffset = height + 12 - width / 2;
            baseJPM = 2500;
        }

        public override void Init(ScreenManager screenManager)
        {
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            headTexture = screenManager.contentRef.Load<Texture2D>("BaseJohnHead");
            bodyTexture = screenManager.contentRef.Load<Texture2D>("BaseJohnBody");
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
            spriteBatch.Draw(
                bodyTexture, bodyRect, null, Color.White, 0, new Vector2(0, 0),
                currentSpriteEffects, 1);
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

        //DISPLAY NAMES
        public override string GetDisplayName()
        {
            return "Nerd John";
        }

        public override string GetDisplayNameFirst()
        {
            return "Nerd";
        }
        public override string GetDisplayNameSecond()
        {
            return "John";
        }

        // POKEDEX INFO
        public override string getDescription(int lineNumber)
        {
            if (lineNumber == 0) return "A true classic model of John, Nerd John excels where other";
            else if (lineNumber == 1) return "Johns falter. Dungeons and Dragons? check. MTG? Check. ";
            else if (lineNumber == 2) return "Fighting games? you do not want to see his combos or hold that mix.";
            else return "He has a math degree, a calculator watch, and so many indoor hobbies.";
        }

        public override int getDescriptionHeight()
        {
            return 57;
        }


        ///SHOP INFO 
        public override int getShopCost()
        {
            return 12000;
        }
        public override JohnList getJohnEnum()
        {
            return JohnList.NerdJohn;
        }
    }
}
