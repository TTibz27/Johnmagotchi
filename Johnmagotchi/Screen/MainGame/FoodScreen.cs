using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.Inputs;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.Screen.MainGame
{
    internal class FoodScreen : GameScreen
    {

        MainGameScreen mainScreen;
        SpriteBatch spriteBatch;
        Texture2D panel;
        Texture2D button;
        Texture2D buttonHighlight;
        SpriteFont kemco;


        Texture2D BurgerTexture;
        Texture2D SalmonTexture;
        Texture2D TacosTexture;
        Texture2D PopcornTexture;
        Texture2D PizzaTexture;
        Texture2D CerealTexture;
        Texture2D SaladTexture;
        Texture2D CakeTexture;

        // These could all be consts
        int spriteScaler;
        int panelWidth;
        int panelHeight;
        int buttonWidth;
        int buttonHeight;
        int foodWidth;
        int foodHeight;
        int highlightedButtonIndex;

        public FoodScreen(MainGameScreen mainScreenRef) {
            mainScreen = mainScreenRef;
            spriteScaler = 3;
            panelWidth = 256 * spriteScaler;
            panelHeight = 160 * spriteScaler;
            buttonWidth = 60 * spriteScaler;
            buttonHeight = 60 * spriteScaler;
            foodWidth = 50 * spriteScaler;
            foodHeight = 38 * spriteScaler;

            highlightedButtonIndex = -1;
        }
      

        public override void Init()
        {
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            panel = screenManager.contentRef.Load<Texture2D>("UI/FoodGridTray");
            button = screenManager.contentRef.Load<Texture2D>("UI/FoodButton");
            buttonHighlight = screenManager.contentRef.Load<Texture2D>("UI/FoodButtonHighlighted");
            kemco = screenManager.contentRef.Load<SpriteFont>("Fonts/Kemco-20");

            BurgerTexture =  screenManager.contentRef.Load<Texture2D>("Foods/Burger");
            SalmonTexture = screenManager.contentRef.Load<Texture2D>("Foods/salmon.");
            TacosTexture = screenManager.contentRef.Load<Texture2D>("Foods/tacos");
            PopcornTexture = screenManager.contentRef.Load<Texture2D>("Foods/popcorn");
            PizzaTexture = screenManager.contentRef.Load<Texture2D>("Foods/Pizza");
            CerealTexture = screenManager.contentRef.Load<Texture2D>("Foods/cereal");
            SaladTexture = screenManager.contentRef.Load<Texture2D>("Foods/salad");
            CakeTexture = screenManager.contentRef.Load<Texture2D>("Foods/cake");

        }

        public override void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
           // spriteBatch.Draw(panel, new Vector2(300, 0), Color.White);
         
            Rectangle panelRect = new Rectangle(300, 0, panelWidth, panelHeight);
            spriteBatch.Draw(panel, panelRect, Color.White);

            // buttons
            int xPos = 300 + 9 * spriteScaler;
            Rectangle buttonRect0 = new (xPos, 16 * spriteScaler,  buttonWidth, buttonHeight);
            Vector2 label0 = new (xPos + 25, 16 * spriteScaler + 150);
            Rectangle buttonRect4 = new (xPos, 76 * spriteScaler, buttonWidth, buttonHeight);
            Vector2 label4 = new(xPos + 40, 76 * spriteScaler + 150);

            xPos += 60 * spriteScaler;
            Rectangle buttonRect1 = new(xPos, 16 * spriteScaler, buttonWidth, buttonHeight);
            Vector2 label1 = new(xPos + 25, 16 * spriteScaler + 150);
            Rectangle buttonRect5 = new (xPos, 76 * spriteScaler, buttonWidth, buttonHeight);
            Vector2 label5 = new(xPos + 25, 76 * spriteScaler + 150);

            xPos += 60 * spriteScaler;
            Rectangle buttonRect2 = new (xPos, 16 * spriteScaler, buttonWidth, buttonHeight);
            Vector2 label2 = new(xPos + 40, 16 * spriteScaler + 150);
            Rectangle buttonRect6 = new (xPos, 76 * spriteScaler, buttonWidth, buttonHeight);
            Vector2 label6 = new(xPos + 40, 76 * spriteScaler + 150);

            xPos += 60 * spriteScaler;
            Rectangle buttonRect3 = new (xPos, 16 * spriteScaler, buttonWidth, buttonHeight);
            Vector2 label3 = new(xPos + 15, 16 * spriteScaler + 150);
            Rectangle buttonRect7 = new (xPos, 76 * spriteScaler, buttonWidth, buttonHeight);
            Vector2 label7 = new(xPos + 55, 76 * spriteScaler + 150);

            //base buttons
            spriteBatch.Draw(button, buttonRect0, Color.White);
            spriteBatch.Draw(button, buttonRect1, Color.White);
            spriteBatch.Draw(button, buttonRect2, Color.White);
            spriteBatch.Draw(button, buttonRect3, Color.White);
            spriteBatch.Draw(button, buttonRect4, Color.White);
            spriteBatch.Draw(button, buttonRect5, Color.White);
            spriteBatch.Draw(button, buttonRect6, Color.White);
            spriteBatch.Draw(button, buttonRect7, Color.White);

            //highlighted buttons
           if(highlightedButtonIndex == 0) spriteBatch.Draw(buttonHighlight, buttonRect0, Color.White);
           if(highlightedButtonIndex == 1) spriteBatch.Draw(buttonHighlight, buttonRect1, Color.White);
           if(highlightedButtonIndex == 2) spriteBatch.Draw(buttonHighlight, buttonRect2, Color.White);
           if(highlightedButtonIndex == 3) spriteBatch.Draw(buttonHighlight, buttonRect3, Color.White);
           if(highlightedButtonIndex == 4) spriteBatch.Draw(buttonHighlight, buttonRect4, Color.White);
           if(highlightedButtonIndex == 5) spriteBatch.Draw(buttonHighlight, buttonRect5, Color.White);
           if(highlightedButtonIndex == 6) spriteBatch.Draw(buttonHighlight, buttonRect6, Color.White);
           if(highlightedButtonIndex == 7) spriteBatch.Draw(buttonHighlight, buttonRect7, Color.White);


            //Salmon, stir fry, burger, popcorn, cereal, tacos, and cheesecake
            //labels
            spriteBatch.DrawString(kemco, "Burger", label0, Color.Black);
            spriteBatch.DrawString(kemco, "Salmon", label1, Color.Black);
            spriteBatch.DrawString(kemco, "Tacos", label2, Color.Black);
            spriteBatch.DrawString(kemco, "Popcorn", label3, Color.Black);
            spriteBatch.DrawString(kemco, "Pizza", label4, Color.Black);
            spriteBatch.DrawString(kemco, "Cereal", label5, Color.Black);
            spriteBatch.DrawString(kemco, "Salad", label6, Color.Black);
            spriteBatch.DrawString(kemco, "Cake", label7, Color.Black);

            // food sprites
            xPos = 300 + 9 + 10 * spriteScaler;
            Rectangle foodRect0 = new(xPos, 21 * spriteScaler, foodWidth, foodHeight);
            Rectangle foodRect4 = new(xPos, 81 * spriteScaler, foodWidth, foodHeight);
            xPos += 60 * spriteScaler;
            Rectangle foodRect1 = new(xPos, 21 * spriteScaler, foodWidth, foodHeight);
            Rectangle foodRect5 = new(xPos, 81 * spriteScaler, foodWidth, foodHeight);
            xPos += 60 * spriteScaler;
            Rectangle foodRect2 = new(xPos, 21 * spriteScaler, foodWidth, foodHeight);
            Rectangle foodRect6 = new(xPos, 81 * spriteScaler, foodWidth, foodHeight);
            xPos += 60 * spriteScaler;
            Rectangle foodRect3 = new(xPos, 21 * spriteScaler, foodWidth, foodHeight);
            Rectangle foodRect7 = new(xPos, 81 * spriteScaler, foodWidth, foodHeight);

            // draw foods
            spriteBatch.Draw(BurgerTexture, foodRect0, Color.White);
            spriteBatch.Draw(SalmonTexture, foodRect1, Color.White);
            spriteBatch.Draw(TacosTexture, foodRect2, Color.White);
            spriteBatch.Draw(PopcornTexture, foodRect3, Color.White);
            spriteBatch.Draw(PizzaTexture, foodRect4, Color.White);
            spriteBatch.Draw(CerealTexture, foodRect5, Color.White);
            spriteBatch.Draw(SaladTexture, foodRect6, Color.White);
            spriteBatch.Draw(CakeTexture, foodRect7, Color.White);




            spriteBatch.End();
        }


        public override void Update()
        {
            //todo - add input controls, and global esc key to push exit prompt

            if (this.isTopScreen)
            {
                MouseInput mouse = screenManager.inputs.mouseInput;
                // Out of panel
               if ( mouse.x < 300 || mouse.x > 300+ panelWidth || mouse.y > panelHeight)
                {
                    highlightedButtonIndex = -1;
                    if (mouse.leftClick.isJustPressed) screenManager.removeTopScreens(1);
                }

                else
                {
                    // default
                    highlightedButtonIndex = -1;

                    //first row 
                    if (mouse.y >= 16 * spriteScaler && mouse.y <= 76 * spriteScaler) 
                    {
                        int xStart = 300 + 9 * spriteScaler;
                        int xEnd = xStart + 59 * spriteScaler;
                        for (int i = 0; i < 4; i++) {
                            if (mouse.x > xStart && mouse.x < xEnd) highlightedButtonIndex = i;
                            xStart  += 60 * spriteScaler;
                            xEnd += 60 * spriteScaler;
                        }

                    }
                    //second row
                    else if (mouse.y >= 76 * spriteScaler && mouse.y <= 136 * spriteScaler)
                    {
                        int xStart = 300 + 9 * spriteScaler;
                        int xEnd = xStart + 59 * spriteScaler;
                        for (int i = 0; i < 4; i++)
                        {
                            if (mouse.x > xStart && mouse.x < xEnd) highlightedButtonIndex = i + 4;
                            xStart += 60 * spriteScaler;
                            xEnd += 60 * spriteScaler;
                        }

                    }
                    
                    if (highlightedButtonIndex != -1 && mouse.leftClick.isJustPressed)
                    {
                        Debug.WriteLine("index clicked : " + highlightedButtonIndex);
                        mainScreen.spawnFood(highlightedButtonIndex);
                        screenManager.removeTopScreens(1);
                    }



                }

            }
        }
        public override void Destroy()
        {
            //TODO - Handle Destroying object
            //throw new NotImplementedException();
        }

    }
}
