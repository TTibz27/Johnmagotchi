using Johnmagotchi.GameContent.Objects;
using Johnmagotchi.GameContent.Objects.Food;
using Johnmagotchi.GameContent.Objects.Johns;
using Johnmagotchi.GameContent.Objects.Johns.Concrete;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.Inputs;
using TibzGame.Core.ScreenManager;
using static Johnmagotchi.GameContent.Objects.Johns.AbstractJohn;
using static System.Net.Mime.MediaTypeNames;

namespace Johnmagotchi.Screen.MainGame
{
    public class MainGameScreen : GameScreen
    {
        SpriteBatch spriteBatch;
        Texture2D leftBar;
        Texture2D topBar;
        Texture2D topBarButton;
        Texture2D topBarButtohHighlighted;
        Texture2D statusBarFull;
        Texture2D statusBarEmpty;


        SpriteFont karmatic;
        SpriteFont kemco;
        SpriteFont pixsplit;
        SpriteFont ariel;

        private int mouseHoverIndex;

        private int displayNameOffset;
        private int displayNameOffset2;

        private static bool DEBUG_FONT_PRINT = false;

        int playbackSpeedModifier;


        private FoodObj food;
        private BedProp bed;
        private BathroomDoorProp door;
        private int doorCloseWaitTimer;
        private static int DOOR_CLOSE_WAIT_MAX = 60;

        public IAbstractJohn currentJohn;


        public override void Init()
        {
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);

            //UI
            leftBar = screenManager.contentRef.Load<Texture2D>("UI/sidebar2");
            topBar = screenManager.contentRef.Load<Texture2D>("TopBarPlaceholder");
            topBarButton = screenManager.contentRef.Load<Texture2D>("UI/SingleButtonBG");
            topBarButtohHighlighted = screenManager.contentRef.Load<Texture2D>("UI/SingleButtonBGBright");
            statusBarFull = screenManager.contentRef.Load<Texture2D>("UI/HealthBarGreen");
            statusBarEmpty = screenManager.contentRef.Load<Texture2D>("UI/HealthBarRed");
            mouseHoverIndex = -1;

            //fonts
            karmatic = screenManager.contentRef.Load<SpriteFont>("Fonts/Karmatic-20");
            kemco = screenManager.contentRef.Load<SpriteFont>("Fonts/Kemco-20");
            pixsplit = screenManager.contentRef.Load<SpriteFont>("Fonts/PixelSplitter");
            ariel = screenManager.contentRef.Load<SpriteFont>("Fonts/Ariel-20");

            // currentJohn = new TestJohn();
            currentJohn = new BaseJohn();
            currentJohn.Init(screenManager);
            playbackSpeedModifier = 1;

            food = null;
            bed = null;
            door = null;
            doorCloseWaitTimer = 0;

            /*   isFoodObjectReady= false;
               isFoodObjectSpawned= false; */

            displayNameOffset =(int) (karmatic.MeasureString(currentJohn.GetDisplayNameFirst()).X /2); // TODO -- add logic for multiple line length names. also this font should prly be changed, it doesn't read too well on backgrounds that have color.
            displayNameOffset2 = (int)(karmatic.MeasureString(currentJohn.GetDisplayNameSecond()).X / 2); // leaving  that above todo, how much time did I think I had lol lmao
        }
        public override void Draw()
        {

            //John playfield
            if (bed != null) bed.DrawBackLayer();

            if (door != null)
            {
                if (this.currentJohn.GetJohnState() == JohnState.enterBathroomDoor ||
                    this.currentJohn.GetJohnState() == JohnState.ExitBathroom) door.DrawOpenDoor();
                else door.DrawClosedDoor();
                
            }

            currentJohn.Draw();

            if (this.currentJohn.GetJohnState() == JohnState.heyImPoopinHere) door.DrawClosedDoor(); // this will hide John
            if (food != null) food.Draw();
            if (bed != null) bed.DrawFrontLayer();


            // Background

            //Side & Top Bars
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            spriteBatch.Draw(leftBar, new Vector2(0, 0), Color.White);

            spriteBatch.Draw(topBar, new Vector2(350, 0), Color.White); ///185 width

            spriteBatch.Draw(topBarButton, new Vector2(350, 0), Color.White);
            spriteBatch.Draw(topBarButton, new Vector2(350 +186, 0), Color.White);
            spriteBatch.Draw(topBarButton, new Vector2(350 + 186 * 2,0), Color.White);
            spriteBatch.Draw(topBarButton, new Vector2(350 + 186 * 3, 0), Color.White);
            spriteBatch.Draw(topBarButton, new Vector2(350 + 186 * 4, 0), Color.White);

            //highlight if appropriate
            if (currentJohn.GetJohnState() == JohnState.Walking && food ==null) // don't imply selections can be made if john is busy
            {
                if (mouseHoverIndex == 0) spriteBatch.Draw(topBarButtohHighlighted, new Vector2(350, 0), Color.White);
                if (mouseHoverIndex == 1) spriteBatch.Draw(topBarButtohHighlighted, new Vector2(350 + 186, 0), Color.White);
                if (mouseHoverIndex == 2) spriteBatch.Draw(topBarButtohHighlighted, new Vector2(350 + 186 * 2, 0), Color.White);
                if (mouseHoverIndex == 3) spriteBatch.Draw(topBarButtohHighlighted, new Vector2(350 + 186 * 3, 0), Color.White);
                if (mouseHoverIndex == 4) spriteBatch.Draw(topBarButtohHighlighted, new Vector2(350 + 186 * 4, 0), Color.White);
            }

            Vector2 textRotationOrigin = new Vector2(50, 25);
            // labels
            spriteBatch.DrawString(kemco, "Food", new Vector2(450, 40),  new Color( new Vector3(255,255,255)), 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(kemco, "Sleep", new Vector2(450 +186, 40), new Color(new Vector3(255, 255, 255)), 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(kemco, "John", new Vector2(450 + 186 *2, 40), new Color(new Vector3(255, 255, 255)), 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(kemco, "Shop", new Vector2(450 + 186 * 3, 40), new Color(new Vector3(255, 255, 255)), 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(kemco, "Data", new Vector2(450 + 186 * 4, 40), new Color(new Vector3(255, 255, 255)), 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);


            //Print John Name
         //   Vector2 textRotationOrigin = new Vector2(50, 25); // karmatic.MeasureString(text) / 2;
                                                           // Places text in center of the screen
            Vector2 position = new(35, 35);
            textRotationOrigin = new Vector2(0, 0);
            float scale = 224/ (karmatic.MeasureString(currentJohn.GetDisplayNameFirst()).X);
            if (scale > 2.0f) scale = 2.0f;
            if (scale < 2.0f) {position.Y = position.Y + scale * 10; }
            spriteBatch.DrawString(karmatic, currentJohn.GetDisplayNameFirst(), position, Color.Black, 0, textRotationOrigin, scale, SpriteEffects.None, 0.5f);
            Vector2 position2 = new(35, 90);
            spriteBatch.DrawString(karmatic, currentJohn.GetDisplayNameSecond(), position2, Color.Black, 0, textRotationOrigin, 2.0f, SpriteEffects.None, 0.5f);

            textRotationOrigin = new Vector2(50, 25);

            //Status Bars

            spriteBatch.DrawString(kemco, "Hunger", new Vector2(85, 200), Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(statusBarEmpty, new Vector2(35, 200), Color.White);
            spriteBatch.Draw(statusBarFull, new Vector2(35, 200), new Rectangle(0,0,currentJohn.getStatus().hungry,30) ,Color.White);

            spriteBatch.DrawString(kemco, "Sleep", new Vector2(85, 300), Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(statusBarEmpty, new Vector2(35, 300), Color.White);
            spriteBatch.Draw(statusBarFull, new Vector2(35, 300), new Rectangle(0, 0, currentJohn.getStatus().sleepy, 30), Color.White);

            spriteBatch.DrawString(kemco, "Bathroom", new Vector2(85, 400), Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.Draw(statusBarEmpty, new Vector2(35, 400), Color.White);
            spriteBatch.Draw(statusBarFull, new Vector2(35, 400), new Rectangle(0, 0, currentJohn.getStatus().bathroom, 30), Color.White);

            spriteBatch.DrawString(kemco, "JPM:", new Vector2(85, 500), Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(kemco, "(Johns Per Minute)", new Vector2(80, 520), Color.Black, 0, textRotationOrigin, 0.75f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(pixsplit, currentJohn.getStatus().currentJPM.ToString(), new Vector2(95, 560), Color.Black, 0, textRotationOrigin,1.0f, SpriteEffects.None, 0.5f);

            spriteBatch.DrawString(kemco, "John Points:", new Vector2(85, 620), Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(pixsplit,( (Int64)screenManager.gameData.johnPoints).ToString(), new Vector2(95, 660), Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);

            

            // DebugText 

            if (DEBUG_FONT_PRINT)
            {

                // Finds the center of the string in coordinates inside the text rectangle
                 textRotationOrigin = new Vector2(50,25); // karmatic.MeasureString(text) / 2;
                // Places text in center of the screen
                 position = new Vector2(1080 / 2, 720 / 2);
                spriteBatch.DrawString(karmatic, "Karmatic", position, Color.White, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
                position.Y += 50;
                spriteBatch.DrawString(kemco, "Kemco", position, Color.White, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
                position.Y += 50;
                spriteBatch.DrawString(pixsplit, "Pixel Splitter", position, Color.White, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
                position.Y += 50;
                spriteBatch.DrawString(ariel, "Ariel", position, Color.White, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
                position.Y += 50;

            }

            spriteBatch.End();

        }
        public override void Update()
        {
            //throw new NotImplementedException();
            for (int i = 0; i < playbackSpeedModifier; i++)
            {
                currentJohn.Update();
            }
       

            if (this.isTopScreen)
            {
                if (screenManager.inputs.mouseInput.y <= 50 && screenManager.inputs.mouseInput.y > 0)
                {
                    if (screenManager.inputs.mouseInput.x >= 350)
                    {
                        mouseHoverIndex = (int)Math.Floor((screenManager.inputs.mouseInput.x - 350) / 186.0f);
                    }
                    else mouseHoverIndex = -1;
                }
                else { mouseHoverIndex = -1; }


                if (screenManager.inputs.mouseInput.leftClick.isJustPressed && mouseHoverIndex >= 0)
                {
                    Debug.WriteLine("click registered");
                    if (currentJohn.GetJohnState() == JohnState.Walking && food == null) // lock out screen transitions if currentJohn is busy or food is null
                    {
                        switch (mouseHoverIndex)
                        {
                            case 0:
                                Debug.WriteLine("Food");

                                screenManager.addScreen(new FoodScreen(this));

                                break;
                            case 1:
                                Debug.WriteLine("Sleep");
                                currentJohn.SetJohnState(JohnState.goToSleep);
                                break;
                            case 2:
                                Debug.WriteLine("John");
                                if (door == null)
                                currentJohn.SetJohnState(JohnState.gotoBathroom);
                                door = new BathroomDoorProp(screenManager);
                                break;
                            case 3:
                                Debug.WriteLine("Shop");
                                screenManager.addScreen(new ShopScreen(this));
                                break;
                            case 4:
                                Debug.WriteLine("Data");
                                screenManager.addScreen(new DexScreen(currentJohn));
                                break;
                            default:
                                Debug.WriteLine("Unhandled index clicked");
                                break;
                        }
                    }
                }
            }

            if (food != null) {
                          
                //will cause food to fall and then execute inside when food hits ground
                if (food.updateFalling())
                {
                    if (currentJohn.GetJohnState() == JohnState.Walking) {
                        currentJohn.SetMoveTo(food.xPos + (25 * 3));
                        currentJohn.SetJohnState(JohnState.MoveTo);
                    }
                      

                    if (currentJohn.GetJohnState() == JohnState.Waiting) // should only happen after moveto completes... we'll see I guess 
                    {
                        Debug.WriteLine("waiting");
                        currentJohn.SetJohnState(JohnState.Eating);
                        food.isInteractable = false;
                        Vector2 holdPos = currentJohn.getHoldingPosition();

                        food.xPos = (int)holdPos.X;
                        food.yPos = (int)holdPos.Y;
                    }
                }

                // State checks
                if (currentJohn.GetJohnState() == JohnState.Eating)
                {
                    Debug.WriteLine("eating");
                    if (food.UpdateEaten())
                    {
                        food = null;
                        currentJohn.SetJohnState(JohnState.Walking);
                    }
                }
               
            }

            if (currentJohn.GetJohnState() == JohnState.goToSleep ||
                   currentJohn.GetJohnState() == JohnState.Sleeping ||
                   currentJohn.GetJohnState() == JohnState.exitSleep)
            {
                this.HandleSleepStates();
            }


            if (currentJohn.GetJohnState() == JohnState.gotoBathroom ||
                currentJohn.GetJohnState() == JohnState.enterBathroomDoor ||
                   currentJohn.GetJohnState() == JohnState.heyImPoopinHere ||
                   currentJohn.GetJohnState() == JohnState.ExitBathroom)
            {
                this.HandleBathroomStates();
            }

            if (door!= null && currentJohn.GetJohnState() == JohnState.Walking)
            {

                if (doorCloseWaitTimer >= DOOR_CLOSE_WAIT_MAX)
                {
                    doorCloseWaitTimer = 0;

                    door = null;
                }
                else
                {
                    doorCloseWaitTimer++;
                }
            }


            // debug / cheat speed up 
            KeyboardState kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(Keys.D1)) playbackSpeedModifier = 1;
            if (kbState.IsKeyDown(Keys.D2)) playbackSpeedModifier = 2;
            if (kbState.IsKeyDown(Keys.D3)) playbackSpeedModifier = 4;
            if (kbState.IsKeyDown(Keys.D4)) playbackSpeedModifier = 8;

        }

        public override void Destroy()
        {
            throw new NotImplementedException();
        }

        public void spawnFood(int FoodType) {
            // this.currentJohn.SetJohnState(JohnState.MoveTo);
            food = new FoodObj(screenManager, FoodType);

        }


        public void HandleSleepStates() {
            if (currentJohn.GetJohnState() == JohnState.goToSleep)
            {
                bed = new BedProp(screenManager);
                if (currentJohn.moveTo(bed.JohnTargetXPos))
                {
                    currentJohn.SetJohnState(JohnState.Sleeping);
                }
            }
            if (currentJohn.GetJohnState ()==JohnState.Sleeping)
            {
                //this state transition is handled by currentJohn.sus()
            }
            if (currentJohn.GetJohnState() == JohnState.exitSleep)
            {
                if (currentJohn.moveTo(500))
                {
                    bed = null;
                    currentJohn.SetJohnState (JohnState.Walking);
                }
            }


        }

        public void HandleBathroomStates() {
            if (currentJohn.GetJohnState() == JohnState.gotoBathroom)
            {
                if (currentJohn.moveTo(door.JohnTargetXPosOpeningDoor))
                {
                    this.currentJohn.SetJohnState(JohnState.enterBathroomDoor);
                   
                }
            }
            if (currentJohn.GetJohnState() == JohnState.enterBathroomDoor)
            {
                if (this.currentJohn.moveTo(door.JohnTargetXPosClosingDoor, door.JohnTargetYPosClosingDoor))
                {
                    currentJohn.SetJohnState(JohnState.heyImPoopinHere);
                }
            }
            if (currentJohn.GetJohnState() == JohnState.heyImPoopinHere)
            {
                //this state transition is handled by currentJohn.UpdateStatus()
            }
            if (currentJohn.GetJohnState() == JohnState.ExitBathroom)
            {

                if (currentJohn.moveTo(door.JohnTargetXPosOpeningDoor, currentJohn.getInitialYPos()))
                {
                    currentJohn.SetJohnState(JohnState.Walking);
                  
                }
            }

        }
    }
}
