using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
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

        public int HUNGER_DECAY = 20;
        public int SLEEP_DECAY = 20;
        public int BR_DECAY = 20;

        private int hungerTimer;
        private int sleepTimer;
        private int brTimer;


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

            hungerTimer = 0;
            sleepTimer = 0;
            brTimer = 0;
        }
        public abstract void Init(ScreenManager screenManager);
        public abstract void Draw();
        public abstract void Update();
        public abstract void Destroy();

        // Lower decay number = faster decay, probably not intuitive
        public virtual int GetHungerDecay() { return 60; }
        public virtual int GetSleepDecay() { return 60; }
        public virtual int GetBathroomDecay() { return 60; }

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
        public JohnStatus getStatus() {

            return status;
        }

        public void UpdateStatus() {
            hungerTimer++;
            sleepTimer++;
            brTimer++;

            if (hungerTimer >= GetHungerDecay())
            {
                status.hungry--;
                hungerTimer = 0;
            }
            if (sleepTimer >= GetSleepDecay()) 
            {
                status.sleepy--;
                sleepTimer = 0;
            }
            if (brTimer >= GetBathroomDecay()) {
                status.bathroom--;
                brTimer = 0;
            }

        }

    }
}
