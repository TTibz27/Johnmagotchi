using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Objects.Johns
{
    public abstract class AbstractJohn : IAbstractJohn
    {
        public JohnStatus status;
        public int xPosition;
        public int yPosition;
        public SpriteBatch spriteBatch;
        public int width;
        public int height;
        public int shadowOffset;
        public bool facingLeft;
        public float spriteRotation;
        public SpriteEffects currentSpriteEffects;

        private int animHopWalkTimer;
        protected int hopwalkDuration = 28; //must be an even number or else it will cause issues 
        protected int hopwalkDelay = 20;
        public AbstractJohn()
        {
            status = new JohnStatus();
            width = 32 * 4;
            height = 64 * 4;
            xPosition = 815 - (width/2);
            yPosition = 385;

            animHopWalkTimer = 0;
            spriteRotation = 0;
            facingLeft = false;
            currentSpriteEffects = SpriteEffects.None;
        }
        public abstract void Init(ScreenManager screenManager);
        public abstract void Draw();
        public abstract void Update();
        public abstract void Destroy();

        public abstract string GetDisplayName();

        protected void animateHopWalk()
        {
            animHopWalkTimer++;
            if (animHopWalkTimer >= 0) { 
                if (facingLeft) { xPosition--; }
                else xPosition++;

                if (animHopWalkTimer <= hopwalkDuration / 2) // going up
                {
                    yPosition--;
                    shadowOffset++;
                }
                else {
                    yPosition++;
                    shadowOffset --;
                }

            }

            if (animHopWalkTimer> hopwalkDuration)
            {
                animHopWalkTimer = 0 - hopwalkDelay;
            }

            // check if we are out of bounds
            int rightBound = 1280 - width;
            int leftBound = 350;

            if (xPosition > rightBound)
            {
                facingLeft = true;
                xPosition = rightBound;
                currentSpriteEffects = SpriteEffects.FlipHorizontally;
            }
            else if (xPosition < leftBound)
            {
                facingLeft = false;
                xPosition = leftBound;
                currentSpriteEffects = SpriteEffects.None;
            }

        }

    }
}
