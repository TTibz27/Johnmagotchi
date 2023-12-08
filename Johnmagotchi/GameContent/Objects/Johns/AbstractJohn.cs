using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;
using static Johnmagotchi.GameContent.GlobalData;

namespace Johnmagotchi.GameContent.Objects.Johns
{
    public abstract class AbstractJohn : IAbstractJohn
    {
        public JohnStatus status;
        public int xPosition;
        public int initialYPosition; // this should realsitically be private and have methods that protect this initial state, oh well I guess
        public int yPosition;
        public SpriteBatch spriteBatch;
        public int width;
        public int height;
        public int shadowOffset;
        public bool facingLeft;
        public float spriteRotation;
        public SpriteEffects currentSpriteEffects;
        public GlobalData gameData;

        public int HUNGER_DECAY = 20;
        public int SLEEP_DECAY = 20;
        public int BR_DECAY = 20;

        private int hungerTimer;
        private int sleepTimer;
        private int brTimer;

        private int currentStatGain;
        private int hungerGainMax = 100;
        private int statGainTimer;
        private const int SLEEP_GAIN_RESET = 4;
        private const int BATHROOM_GAIN_RESET = 2;


        private int animHopWalkTimer;
        protected int hopwalkDuration = 28; //must be an even number or else it will cause issues 
        protected int hopwalkDelay;
        protected int baseHopwalkDelay =20;

        protected int MoveToXPosition = 400;
        public int baseJPM = 1000;

        

        public enum JohnState
        {
            Walking,
            MoveTo,
            Waiting,
            Eating,
            goToSleep,
            Sleeping,
            exitSleep,
            gotoBathroom,
            enterBathroomDoor,
            heyImPoopinHere,
            ExitBathroom
        }

        public JohnState johnState;
        public AbstractJohn()
        {
            status = new JohnStatus();
            status.baseJPM = 1000;
            width = 32 * 4;
            height = 64 * 4;
            xPosition = 815 - (width/2);
           
            yPosition = 385;
            initialYPosition = yPosition;

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
            statGainTimer = 0 ;
    }
        public abstract void Init(ScreenManager screenManager);
        public abstract void Draw();
        public abstract void DrawAt(Vector2 pos);
        public virtual void Update() {
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
        public virtual void Destroy() { 
        // I don't think we need to do this for anything....???
        }

        // Lower decay number = faster decay, probably not intuitive
        public virtual int GetHungerDecay() { return 60; }
        public virtual int GetSleepDecay() { return 60; }
        public virtual int GetBathroomDecay() { return 60; }

        public abstract string GetDisplayName();

        public abstract string GetDisplayNameFirst();
        public abstract string GetDisplayNameSecond();



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

        public bool moveTo(int x) {
            hopwalkDelay = 2;
            if (xPosition > x+5)
            {
                facingLeft = true;
                currentSpriteEffects = SpriteEffects.FlipHorizontally;
                // two hop walk ticks
                animateHopWalk();
                animateHopWalk();
                animateHopWalk();
                return (xPosition < x + 5);
                
            }
            else if (xPosition < x-5) {
                facingLeft = false;
                currentSpriteEffects = SpriteEffects.None;
                animateHopWalk();
                animateHopWalk();
                animateHopWalk();
                return (xPosition > x - 5);

            }
            else
            {
                    //yPosition=  initialYPosition;
                    hopwalkDelay = baseHopwalkDelay;
                    facingLeft = false;
                    currentSpriteEffects = SpriteEffects.None;
                    return true;
                  
            }

        }

        public bool moveTo(int x, int y) {
            bool isXGood = this.moveTo(x);
            bool isYGood = false;

            if (yPosition < y - 5)
                yPosition += 3;
            else if (yPosition > y + 5)
                yPosition -= 3;
            else isYGood = true;


            return (isXGood && isYGood);
        }


        public JohnStatus getStatus() {

            return status;
        }

        public void UpdateStatus()
        {
            hungerTimer++;
            sleepTimer++;
            brTimer++;

            if (johnState == JohnState.Walking)
            {
                currentStatGain = 0;
            }

            if (hungerTimer >= GetHungerDecay())
            {
                if (johnState != JohnState.Eating) status.hungry--;
                if (status.hungry < 0) { status.hungry = 0; }
                hungerTimer = 0;
            }
            if (sleepTimer >= GetSleepDecay())
            {
                if (johnState != JohnState.Sleeping) status.sleepy--;
                if (status.sleepy < 0) { status.sleepy = 0; }
                sleepTimer = 0;
            }
            if (brTimer >= GetBathroomDecay())
            {
                if (johnState != JohnState.heyImPoopinHere) status.bathroom--;
                if (status.bathroom < 0) { status.bathroom = 0; }
                brTimer = 0;
            }

            //Special states
            if (johnState == JohnState.Eating)
            {
                currentStatGain++;
                if (currentStatGain < hungerGainMax) status.hungry++;
                if (status.hungry > 255) { status.hungry = 255; }

            }
            if (johnState == JohnState.Sleeping)
            {
                statGainTimer++;

                if (statGainTimer > SLEEP_GAIN_RESET)
                {
                    status.sleepy++;
                    statGainTimer = 0;
                }

                if (status.sleepy >= 255)
                {
                    status.sleepy = 255;
                    this.johnState = JohnState.exitSleep;
                }

            }
            if (johnState == JohnState.heyImPoopinHere)
            {
                statGainTimer++;

                if (statGainTimer > BATHROOM_GAIN_RESET)
                {
                    status.bathroom++;
                    statGainTimer = 0;
                }

                if (status.bathroom >= 255)
                {
                    status.bathroom = 255;
                    this.johnState = JohnState.ExitBathroom;
                }
            }


            // JPM Calculations 
            float sleepModifier = 0f;
            float hungryModifier = 0f;
            float bathroomModifier = 0f;

            if (status.sleepy > 64) sleepModifier += 1f;
            if (status.sleepy > 128) sleepModifier += 0.5f;
            if (status.sleepy > 192) sleepModifier += 0.5f;

            if (status.hungry > 64) hungryModifier += 1f;
            if (status.hungry > 128) hungryModifier += 0.5f;
            if (status.hungry > 192) hungryModifier += 0.5f;

            if (status.bathroom > 64) bathroomModifier += 1f;
            if (status.bathroom > 128) bathroomModifier += 0.5f;
            if (status.bathroom > 192) bathroomModifier += 0.5f;



            status.currentJPM = baseJPM + (baseJPM * sleepModifier) + (baseJPM * hungryModifier) + (baseJPM * bathroomModifier);


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

        public int getInitialYPos()
        {
            return initialYPosition;
        }

        public void AddJohnPoints() {
            gameData.johnPoints += (status.currentJPM / (60 * 60)); // 60 frames per second * 60 sec per min 
        }

        public virtual string getDescription(int lineNumber) {

            return "";
        }

        public virtual int getDescriptionHeight()
        {
            return 64;
        }
        public virtual float getDescriptionWeight()
        {
            return 1.00f;
        }

        public abstract JohnList getJohnEnum();

        public abstract int getShopCost();
    }


}
