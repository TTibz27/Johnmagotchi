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
using TibzGame.Core.ScreenManager;
using static System.Net.Mime.MediaTypeNames;

namespace Johnmagotchi.Screen.MainGame
{
    public class MainGameScreen : GameScreen
    {
        SpriteBatch testSprite;
        Texture2D leftBar;
        Texture2D topBar;


        SpriteFont karmatic;
        SpriteFont kemco;
        SpriteFont phone;
        SpriteFont pixsplit;
        SpriteFont ariel;

        private static bool DEBUG_FONT_PRINT = true;

        IAbstractJohn currentJohn; 

        public override void Init()
        {
            testSprite = new SpriteBatch(screenManager.GraphicsDevice);

            leftBar = screenManager.contentRef.Load<Texture2D>("sidebarPlaceholder");
            topBar = screenManager.contentRef.Load<Texture2D>("TopBarPlaceholder");
            karmatic = screenManager.contentRef.Load<SpriteFont>("Fonts/Karmatic-20");
            kemco = screenManager.contentRef.Load<SpriteFont>("Fonts/Kemco-20");
            pixsplit = screenManager.contentRef.Load<SpriteFont>("Fonts/PixelSplitter");
            ariel = screenManager.contentRef.Load<SpriteFont>("Fonts/Ariel-20");

            currentJohn = new BaseJohn();
            currentJohn.Init(screenManager);

        }
        public override void Draw()
        {
            // Background
            testSprite.Begin();
            testSprite.Draw(leftBar, new Vector2(0, 0), Color.White);
            testSprite.Draw(topBar, new Vector2(350, 0), Color.White);
            Debug.WriteLine("draw");


            // Text 

            if (DEBUG_FONT_PRINT)
            {

                // Finds the center of the string in coordinates inside the text rectangle
                Vector2 textMiddlePoint = new Vector2(50,25); // karmatic.MeasureString(text) / 2;
                // Places text in center of the screen
                Vector2 position = new Vector2(1080 / 2, 720 / 2);
                testSprite.DrawString(karmatic, "Karmatic", position, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);
                position.Y += 50;
                testSprite.DrawString(kemco, "Kemco", position, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);
                position.Y += 50;
                testSprite.DrawString(pixsplit, "Pixel Splitter", position, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);
                position.Y += 50;
                testSprite.DrawString(ariel, "Ariel", position, Color.White, 0, textMiddlePoint, 1.0f, SpriteEffects.None, 0.5f);
                position.Y += 50;

            }

            testSprite.End();

            //John playfield
            currentJohn.Draw();


        }
        public override void Update()
        {
            //throw new NotImplementedException();
            currentJohn.Update();
        }
        public override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}
