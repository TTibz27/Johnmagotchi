using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Objects.Food
{
    internal class FoodObj
    {
        SpriteBatch spriteBatch;
        public Texture2D texture;
        public ScreenManager scrnManger;

        const int FLOOR_PX = 650 - (35 * 3);

        public int xPos;
        public int yPos;

        public bool isInteractable;

        int foodEatenTimer;
        public FoodObj(ScreenManager screenManager, int foodindex) {

            isInteractable = true;
            scrnManger = screenManager;
            switch (foodindex)
            {
                case 0:
                    texture = scrnManger.contentRef.Load<Texture2D>("Foods/Burger");
                    break; 
                case 1:
                    texture = scrnManger.contentRef.Load<Texture2D>("Foods/salmon.");
                    break;
                case 2:
                    texture = scrnManger.contentRef.Load<Texture2D>("Foods/tacos");
                    break;
                case 3: 
                    texture = scrnManger.contentRef.Load<Texture2D>("Foods/popcorn");
                    break;
                case 4:
                    texture = scrnManger.contentRef.Load<Texture2D>("Foods/Pizza");
                    break;
                case 5:
                    texture = scrnManger.contentRef.Load<Texture2D>("Foods/cereal");
                    break;  
                case 6:
                    texture = scrnManger.contentRef.Load<Texture2D>("Foods/salad");
                    break;
                case 7:
                    texture = scrnManger.contentRef.Load<Texture2D>("Foods/cake");
                    break;
                    default: throw new ArgumentException();
            }
            spriteBatch = new SpriteBatch(scrnManger.GraphicsDevice);
            yPos = 100;
            xPos = 500;
            foodEatenTimer = 180;
        }
        public bool updateFalling() // return true when it hits the ground

        {
            if (isInteractable)
            {
                if (yPos < FLOOR_PX)
                {
                    yPos += 4;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else return false; // not sure if this matters if true or false.

        }


        public bool UpdateEaten() {  // return true when finished
            foodEatenTimer--;
         
            if (foodEatenTimer < 0) return true;
            else return false;
        }
        public void Draw() {
       
            Rectangle foodRect0 = new(xPos, yPos, 50 * 3, 38 * 3);

            //shrink food as it is eaten
            if (foodEatenTimer < 120)
            {
                foodRect0 = new(xPos, yPos, 50 * 2, 38 * 2);

            }

            if (foodEatenTimer < 60)
            {
                foodRect0 = new(xPos, yPos, 50 , 38 );

            }

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            spriteBatch.Draw(texture, foodRect0, Color.White);
            spriteBatch.End();
        }
    }
}
