using Johnmagotchi.GameContent.Objects.Food;
using Johnmagotchi.GameContent.Objects.Johns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
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

        private static bool DEBUG_FONT_PRINT = false;

        int playbackSpeedModifier;


        private FoodObj food;

        IAbstractJohn currentJohn;


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

            currentJohn = new BaseJohn();
            currentJohn.Init(screenManager);
            playbackSpeedModifier = 4;

            food = null;

            /*   isFoodObjectReady= false;
               isFoodObjectSpawned= false; */

            displayNameOffset =(int) (karmatic.MeasureString(currentJohn.GetDisplayName()).X /2); // TODO -- add logic for multiple line length names. also this font should prly be changed, it doesn't read too well on backgrounds that have color.
          
        }
        public override void Draw()
        {

            //John playfield
            currentJohn.Draw();
            if (food!= null) food.Draw();


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
            spriteBatch.DrawString(kemco, "Games", new Vector2(450 + 186 * 3, 40), new Color(new Vector3(255, 255, 255)), 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(kemco, "Data", new Vector2(450 + 186 * 4, 40), new Color(new Vector3(255, 255, 255)), 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);


            //Print John Name
         //   Vector2 textRotationOrigin = new Vector2(50, 25); // karmatic.MeasureString(text) / 2;
                                                           // Places text in center of the screen
            Vector2 position = new(200 - displayNameOffset, 75);
            spriteBatch.DrawString(karmatic, currentJohn.GetDisplayName(), position, Color.Black, 0, textRotationOrigin, 2.0f, SpriteEffects.None, 0.5f);

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
            spriteBatch.DrawString(pixsplit, currentJohn.getStatus().JPM.ToString(), new Vector2(95, 560), Color.Black, 0, textRotationOrigin,1.0f, SpriteEffects.None, 0.5f);

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
                if (screenManager.inputs.mouseInput.y <= 50)
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
                                break;
                            case 2:
                                Debug.WriteLine("John");
                                break;
                            case 3:
                                Debug.WriteLine("Games");
                                break;
                            case 4:
                                Debug.WriteLine("Data");
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
        }

        public override void Destroy()
        {
            throw new NotImplementedException();
        }

        public void spawnFood(int FoodType) {
            // this.currentJohn.SetJohnState(JohnState.MoveTo);
            food = new FoodObj(screenManager, FoodType);



        }
    }
}
