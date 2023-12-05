using Microsoft.Xna.Framework;
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

        private int currentStatGain;
        private int hungerGainMax = 100;


        private int animHopWalkTimer;
        protected int hopwalkDuration = 28; //must be an even number or else it will cause issues 
        protected int hopwalkDelay;
        protected int baseHopwalkDelay =20;

        protected int MoveToXPosition = 400;

        

        public enum JohnState
        {
            Walking,
            MoveTo,
            Waiting,
            Eating,
            Sleeping,
            Poopin,
        }

        public JohnState johnState;
        public AbstractJohn()
        {
            status = new JohnStatus();
            width = 32 * 4;
            height = 64 * 4;
            xPosition = 815 - (width/2);
            yPosition = 385;

            hopwalkDelay = baseHopwalkDelay;
            animHopWalkTimer = 0;
            spriteRotation = 0;
            facingLeft = false;
            currentSpriteEffects = SpriteEffects.None;

            hungerTimer = 0;
            sleepTimer = 0;
            brTimer = 0;
            currentStatGain = 0;
            johnState = JohnState.Walking;
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

        protected bool moveTo(int x) {
            hopwalkDelay = 2;
            if (xPosition > x-5)
            {
                facingLeft = true;
                currentSpriteEffects = SpriteEffects.FlipHorizontally;
                // two hop walk ticks
                animateHopWalk();
                animateHopWalk();
                animateHopWalk();
                return false;
            }
            else if (xPosition > x+5) {
                facingLeft = false;
                currentSpriteEffects = SpriteEffects.None;
                animateHopWalk();
                animateHopWalk();
                animateHopWalk();
                return false;
            }
            else
            {
                if (animHopWalkTimer <= 0)
                //reset delay
                {
                    hopwalkDelay = baseHopwalkDelay;
                    facingLeft = false;
                    currentSpriteEffects = SpriteEffects.None;
                    return true;
                }
                else
                {
                    animateHopWalk();
                    animateHopWalk();
                    animateHopWalk();
                    return false;
                }
            }

        }


        public JohnStatus getStatus() {

            return status;
        }

        public void UpdateStatus() {
            hungerTimer++;
            sleepTimer++;
            brTimer++;

            if (johnState == JohnState.Walking) {
                currentStatGain = 0;
            }

            if (hungerTimer >= GetHungerDecay())
            {
                if(johnState != JohnState.Eating) status.hungry--;
                if (status.hungry < 0) { status.hungry = 0;}
                hungerTimer = 0;
            }
            if (sleepTimer >= GetSleepDecay()) 
            {
                if (status.sleepy < 0) { status.sleepy = 0; }
                status.sleepy--;
                sleepTimer = 0;
            }
            if (brTimer >= GetBathroomDecay()) {
                status.bathroom--;
                if (status.bathroom < 0) { status.bathroom = 0;} 
                brTimer = 0;
            }

            if (johnState == JohnState.Eating) {
                currentStatGain++; 
                if (currentStatGain < hungerGainMax) status.hungry++;
                if (status.hungry > 255) { status.hungry = 255; }

            }

        }

        public void SetJohnState(JohnState state)
        {
            this.johnState = state;
        }

        public JohnState GetJohnState() {
            return this.johnState;
        }

        public void SetMoveTo(int x) {
         this.MoveToXPosition = x;
        }

        public int GetXPosition() {

            return this.xPosition;
        }
        
        public virtual Vector2 getHoldingPosition()
        {
            float x = this.xPosition;
            if (facingLeft) x = x - (width / 2);
            else x = x + (width / 2);
            float y = this.yPosition + height/2;
            return new Vector2(x, y); // note that this is a center position, not a draw position, object being held must adjust
        }
    }


}
