using Johnmagotchi.GameContent.Objects.Johns;
using Johnmagotchi.Screen.MainGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.Inputs;
using TibzGame.Core.ScreenManager;
using static Johnmagotchi.GameContent.GlobalData;

namespace Johnmagotchi.Screen
{
    internal class ShopScreen : GameScreen
    {
        public MainGameScreen mainGameScreen;

        SpriteBatch spriteBatch;
        Texture2D background;
        Texture2D backButtonHover;
        Texture2D buyButtonSelected;
        Texture2D buyButtonPurchased;
        SpriteFont kemco;
        SpriteFont pixsplit;

        bool isBackHover;

        IAbstractJohn john1;
        IAbstractJohn john2;
        IAbstractJohn john3;

        IAbstractJohn selectedJohn;

        bool slot1Purchased;
        bool slot2Purchased;
        bool slot3Purchased;
        int slotSelected;

        int screenindex;


        public ShopScreen(MainGameScreen mainScreen)
        {
            isUpdatePriority = true;
            isDrawPriority = true;
            isBackHover = false;
            mainGameScreen = mainScreen;

            slot1Purchased = false;
            slot2Purchased = false;
            slot3Purchased = false;

            slotSelected = -1;
            screenindex = 0;
            selectedJohn = mainGameScreen.currentJohn;

        }
        public override void Init()
        {
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            background = screenManager.contentRef.Load<Texture2D>("ShopScreen");
            backButtonHover = screenManager.contentRef.Load<Texture2D>("UI/BackButtonHover");

            buyButtonSelected = screenManager.contentRef.Load<Texture2D>("UI/buyBarSelected");
            buyButtonPurchased = screenManager.contentRef.Load<Texture2D>("UI/buyBarPurchased");

            //load fonts
            kemco = screenManager.contentRef.Load<SpriteFont>("Fonts/Kemco-20");
            pixsplit = screenManager.contentRef.Load<SpriteFont>("Fonts/PixelSplitter");



            john1 = screenManager.gameData.GetJohnByEnum(JohnList.BaseJohn);
            john2 = screenManager.gameData.GetJohnByEnum(JohnList.AccountantJohn);
            john3 = screenManager.gameData.GetJohnByEnum(JohnList.GrizzlyJohn);
            // find selected selected
            if (john1.getJohnEnum() == mainGameScreen.currentJohn.getJohnEnum()) slotSelected = 1;
            else if (john2.getJohnEnum() == mainGameScreen.currentJohn.getJohnEnum()) slotSelected = 2;
            else if (john3.getJohnEnum() == mainGameScreen.currentJohn.getJohnEnum()) slotSelected = 3;
            else slotSelected = -1;


        }
        public override void Update()
        {
            MouseInput mouse = screenManager.inputs.mouseInput;

            if (mouse.x >= 1024 &&
                mouse.x < 1280 &&
                mouse.y <= 73 &&
                mouse.y > 0)
            {
                isBackHover = true;
                if (mouse.leftClick.isJustPressed)
                {
                    if(mainGameScreen.currentJohn.getJohnEnum() != selectedJohn.getJohnEnum()) mainGameScreen.currentJohn = selectedJohn;
                    screenManager.removeTopScreens(1);
                }
            }
            else isBackHover = false;

            /// this needs to change to only fire when change occurs
          /*  if (screenindex == 0)
            {
                john1 = screenManager.gameData.GetJohnByEnum(JohnList.BaseJohn);
                john2 = screenManager.gameData.GetJohnByEnum(JohnList.AccountantJohn);
                john3 = screenManager.gameData.GetJohnByEnum(JohnList.GrizzlyJohn);
            }

            if (screenindex == 1)
            {
                john1 = screenManager.gameData.GetJohnByEnum(JohnList.NerdJohn);
                john2 = screenManager.gameData.GetJohnByEnum(JohnList.ManagerJohn);
                john3 = screenManager.gameData.GetJohnByEnum(JohnList.Johntrepreneur);
            }

            if (screenindex == 2)
            {
         
                john1 = screenManager.gameData.GetJohnByEnum(JohnList.JohnUgly);
                john2 = screenManager.gameData.GetJohnByEnum(JohnList.JohnCena);
                john3 = screenManager.gameData.GetJohnByEnum(JohnList.PapaJohn);
            }*/

            slot1Purchased = screenManager.gameData.IsPurchased(john1);
            slot2Purchased = screenManager.gameData.IsPurchased(john2);
            slot3Purchased = screenManager.gameData.IsPurchased(john3);

            //97,561, - - - 416, 629
            bool isMouseButton1 = (mouse.x >97 && mouse.x < 416 && mouse.y >561 && mouse.y < 629) && mouse.leftClick.isJustPressed;
            bool isMouseButton2 = (mouse.x > 481 && mouse.x < 800 && mouse.y > 561 && mouse.y < 629) && mouse.leftClick.isJustPressed;
            bool isMouseButton3 = (mouse.x > 866 && mouse.x < 1184 && mouse.y > 561 && mouse.y < 629) && mouse.leftClick.isJustPressed;
            bool isPanLeft = (mouse.x > 0 && mouse.x < 61 && mouse.y > 258 && mouse.y < 513) && mouse.leftClick.isJustPressed;
            bool isPanRight = (mouse.x > 1216 && mouse.x < 1280 && mouse.y > 258 && mouse.y < 513) && mouse.leftClick.isJustPressed;


            // need to purchase
            if (isMouseButton1 && !slot1Purchased)
            {
                 if (john1.getShopCost() <= screenManager.gameData.johnPoints)
                {
                    Debug.WriteLine("slot 1");
                    screenManager.gameData.PurchaseJohn(john1);
                }
            }
            if (isMouseButton2 && !slot2Purchased)
            {
                if (john2.getShopCost() <= screenManager.gameData.johnPoints)
                {
                    Debug.WriteLine("slot 2");
                    screenManager.gameData.PurchaseJohn(john2);
                }
            }
            if (isMouseButton3 && !slot3Purchased)
            {
                if (john3.getShopCost() <= screenManager.gameData.johnPoints)
                {
                    Debug.WriteLine("slot3");
                    screenManager.gameData.PurchaseJohn(john3);
                }
            }
             // Reassign selected slot
            if (isMouseButton1 && slot1Purchased && slotSelected !=1)
            {
                selectedJohn = john1;
                slotSelected = 1;
            }
            if (isMouseButton2 && slot2Purchased && slotSelected != 2)
            {
                selectedJohn = john2;
                slotSelected = 2;
            }
            if (isMouseButton3 && slot3Purchased && slotSelected != 3)
            {
                selectedJohn = john3;
                slotSelected = 3;
            }

            if (isPanLeft || isPanRight) {

                Debug.WriteLine("Panning");
                if (isPanLeft) screenindex--;
                if (isPanRight) screenindex++;

                if (screenindex < 0) screenindex = 2;
                if (screenindex > 2) screenindex = 0;

                Debug.WriteLine(screenindex);
                if (screenindex == 0)
                {
                    john1 = screenManager.gameData.GetJohnByEnum(JohnList.BaseJohn);
                    john2 = screenManager.gameData.GetJohnByEnum(JohnList.AccountantJohn);
                    john3 = screenManager.gameData.GetJohnByEnum(JohnList.GrizzlyJohn);
                }

                if (screenindex == 1)
                {
                    john1 = screenManager.gameData.GetJohnByEnum(JohnList.NerdJohn);
                    john2 = screenManager.gameData.GetJohnByEnum(JohnList.SportsJohn);
                    john3 = screenManager.gameData.GetJohnByEnum(JohnList.Johntrepreneur);
                }

                if (screenindex == 2)
                {

                    john1 = screenManager.gameData.GetJohnByEnum(JohnList.JohnUgly);
                    john2 = screenManager.gameData.GetJohnByEnum(JohnList.JohnCena);
                    john3 = screenManager.gameData.GetJohnByEnum(JohnList.PapaJohn);
                }

                // new johns, adjust selected. this is like marginally quicker than doing this calc in the draw function, I guess, prly should have not had seperate variables for selection logic
                if (john1.getJohnEnum() == mainGameScreen.currentJohn.getJohnEnum()) slotSelected = 1;
                else if (john2.getJohnEnum() == mainGameScreen.currentJohn.getJohnEnum()) slotSelected = 2;
                else if (john3.getJohnEnum() == mainGameScreen.currentJohn.getJohnEnum()) slotSelected = 3;
                else slotSelected = -1;

            }

        }
        public override void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            //back button
            if (isBackHover) spriteBatch.Draw(backButtonHover, new Vector2(1023, 0), Color.White); // I messed up this position but honestly I think it works better



            //shop interface
            //136/20
            Vector2 textRotationOrigin = new Vector2(0, 0);
            Vector2 position = new(144, 30);
            spriteBatch.DrawString(kemco, "JP:", position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            position = new(220, 24);
            spriteBatch.DrawString(pixsplit, "" + (int)screenManager.gameData.johnPoints, position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);



            // slot button states

            if (slot1Purchased)
            {
                spriteBatch.Draw(buyButtonPurchased, new Vector2(97, 561), Color.White);
                position = new(121, 585);  // add 20 to each
                spriteBatch.DrawString(kemco, " Purchased! ", position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
              
            }
            if (slot2Purchased)
            {
                spriteBatch.Draw(buyButtonPurchased, new Vector2(481, 561), Color.White);
                position = new(505, 585);   // add 20 to each
                spriteBatch.DrawString(kemco, " Purchased! ", position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }
            if (slot3Purchased)
            {
                spriteBatch.Draw(buyButtonPurchased, new Vector2(865, 561), Color.White);
                position = new(889, 585);  // add 20 to each
                spriteBatch.DrawString(kemco, " Purchased! ", position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }

            if (slotSelected == 1) 
            {
                spriteBatch.Draw(buyButtonSelected, new Vector2(97, 561), Color.White);
                position = new(121, 585);  // add 20 to each
                spriteBatch.DrawString(kemco, " Selected! ", position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }
            if (slotSelected == 2) 
            {
                spriteBatch.Draw(buyButtonSelected, new Vector2(481, 561), Color.White);
                position = new(505, 585);   // add 20 to each
                spriteBatch.DrawString(kemco, " Selected! ", position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }
            if (slotSelected == 3) 
            {
                spriteBatch.Draw(buyButtonSelected, new Vector2(865, 561), Color.White);
                position = new(889, 585);   // add 20 to each
                spriteBatch.DrawString(kemco, " Selected! ", position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }

            //slot button text
            //slot 1 = 101 565
            if (!slot1Purchased && slotSelected != 1) 
            {
                Color color = Color.Black;
                if (screenManager.gameData.johnPoints < john1.getShopCost()) color = Color.Red;
                position = new(121, 585);  // add 20 to each
                spriteBatch.DrawString(kemco, "Buy: ", position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
                position = new(121 + 100, 585 - 6);
                spriteBatch.DrawString(pixsplit, john1.getShopCost() + " JP", position, color, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }

            // slot 2 = 485 565
            if (!slot2Purchased && slotSelected != 2)
            {
                Color color = Color.Black;
                if (screenManager.gameData.johnPoints < john2.getShopCost()) color = Color.Red;
                position = new(505, 585);  // add 20 to each
                spriteBatch.DrawString(kemco, "Buy: ", position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
                position = new(505 + 100, 585 - 6);
                spriteBatch.DrawString(pixsplit, john2.getShopCost() + " JP", position, color, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }
            // slot3  = 869
            if (!slot3Purchased && slotSelected != 3)
            {
                Color color = Color.Black;
                if (screenManager.gameData.johnPoints < john3.getShopCost()) color = Color.Red;
                position = new(889, 585);  // add 20 to each
                spriteBatch.DrawString(kemco, "Buy: ", position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
                position = new(889 + 100, 585 - 6);
                spriteBatch.DrawString(pixsplit, john3.getShopCost() + " JP", position, color,  0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }


            // Name Labels
            position = new(192 +64, 232);
            textRotationOrigin = new Vector2(kemco.MeasureString(john1.GetDisplayName()).X / 2, 0);
            spriteBatch.DrawString(kemco, john1.GetDisplayName(), position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            position = new(576 + 64, 232);
            textRotationOrigin = new Vector2(kemco.MeasureString(john2.GetDisplayName()).X / 2, 0);
            spriteBatch.DrawString(kemco, john2.GetDisplayName(), position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            position = new(960 + 64, 232);
            textRotationOrigin = new Vector2(kemco.MeasureString(john3.GetDisplayName()).X / 2, 0);
            spriteBatch.DrawString(kemco, john3.GetDisplayName(), position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);



            spriteBatch.End();



            //Johns
            // 192 272
            position = new(192, 272);
            john1.DrawAt(position);
            position = new(576, 272);
            john2.DrawAt(position);
            position = new(960, 272);
            john3.DrawAt(position);

        }


        public override void Destroy()
        {
           
        }

    }
}
