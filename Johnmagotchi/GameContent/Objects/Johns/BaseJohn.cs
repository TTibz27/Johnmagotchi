using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Objects.Johns
{
    internal class BaseJohn : AbstractJohn
    {

        Texture2D headTexture;
        Texture2D bodyTexture;
        Texture2D shadowTexture;

    
        public BaseJohn() : base()
        {
            shadowOffset = height +12 - (width/2);

        }

        public override void Init(ScreenManager screenManager)
        {
            this.spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            this.headTexture = screenManager.contentRef.Load<Texture2D>("BaseJohnHead");
            this.bodyTexture = screenManager.contentRef.Load<Texture2D>("BaseJohnBody");
            this.shadowTexture = screenManager.contentRef.Load<Texture2D>("ShadowRegular");
        }

        /*
        public override int GetHungerDecay()
        {
           return 1;
        }
        public override int GetBathroomDecay()
        {
           return 30;
        }

        public override int GetSleepDecay()
        {
            return 30;
        }
        */

        public override void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            Rectangle shadowRect = new Rectangle(xPosition,yPosition + shadowOffset, width, width/2);
            Rectangle bodyRect = new Rectangle(xPosition, yPosition + width, width, width);
            Rectangle headRect = new Rectangle(xPosition, yPosition, width, width);




            spriteBatch.Draw(shadowTexture, shadowRect, Color.White);
            spriteBatch.Draw(
                bodyTexture, bodyRect, null, Color.White, 0, new Vector2(0,0), 
                currentSpriteEffects,1);
            spriteBatch.Draw(
               headTexture, headRect, null, Color.White, 0, new Vector2(0, 0),
               currentSpriteEffects, 1);

           // spriteBatch.Draw(bodyTexture, bodyRect, Color.White);
           // spriteBatch.Draw(headTexture, headRect, Color.White);
            spriteBatch.End();
        }
        public override void Update() 
        {
            spriteRotation++;
            animateHopWalk();
            UpdateStatus();


        }
        public override void Destroy() { }

        public override string GetDisplayName()
        {
            return "John";
        }


    }
}
