using Johnmagotchi.GameContent.Objects.Johns;
using Johnmagotchi.Screen.MainGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.Inputs;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.Screen
{
    internal class DexScreen : GameScreen
    {
       
        SpriteBatch spriteBatch;
        Texture2D background;
        Texture2D buttonHover;
        IAbstractJohn john; 

        SpriteFont karmatic;
        SpriteFont kemco;
        SpriteFont pixsplit;
        SpriteFont ariel;


        public bool isBackHover;
        public DexScreen(IAbstractJohn currentJohn) {
            isUpdatePriority = true;
            isDrawPriority = true;
            john = currentJohn;


        }

        public override void Init()
        {

            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            background = screenManager.contentRef.Load<Texture2D>("UI/johnnydex");
            buttonHover = screenManager.contentRef.Load<Texture2D>("UI/BackButtonHover");

            //load fonts
            karmatic = screenManager.contentRef.Load<SpriteFont>("Fonts/Karmatic-20");
            kemco = screenManager.contentRef.Load<SpriteFont>("Fonts/Kemco-20");
            pixsplit = screenManager.contentRef.Load<SpriteFont>("Fonts/PixelSplitter");
            ariel = screenManager.contentRef.Load<SpriteFont>("Fonts/Ariel-20");
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
                    screenManager.removeTopScreens(1);
                }
            }
            else isBackHover = false;
        }

        public override void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            spriteBatch.Draw(background, new Vector2(0,0), Color.White);

            if (isBackHover)
            {
                spriteBatch.Draw(buttonHover, new Vector2(1023, 0), Color.White); // I messed up this position but honestly I think it works better

            }

            // Name
            Vector2 position = new(540 , 128);
            Vector2 textRotationOrigin = new Vector2(0,0);
             float scale = 648/ (karmatic.MeasureString(john.GetDisplayName()).X);
            if (scale > 2.0f) scale = 2.0f;
           
            spriteBatch.DrawString(karmatic, john.GetDisplayName(), position, Color.Black, 0, textRotationOrigin, scale, SpriteEffects.None, 0.5f);

            //Height Weight
             position = new(448 +12, 272);
            spriteBatch.DrawString(kemco, "Height: " + john.getDescriptionHeight() + " px",position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);

            position = new(448 + 12, 272 + 80);
            spriteBatch.DrawString(kemco, "Base Power : " + john.getStatus().baseJPM + " JPM", position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);

            // Description

            position = new(224, 500);
            spriteBatch.DrawString(ariel, john.getDescription(0), position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            position = new(224 , 538);
            spriteBatch.DrawString(ariel, john.getDescription(1), position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            position = new(224, 576);
            spriteBatch.DrawString(ariel, john.getDescription(2), position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);
            position = new(224, 614);
            spriteBatch.DrawString(ariel, john.getDescription(3), position, Color.Black, 0, textRotationOrigin, 1.0f, SpriteEffects.None, 0.5f);





            spriteBatch.End();

            john.DrawAt(new Vector2(160, 128));
        }

        public override void Destroy()
        {
            // I don't think anytthing special needs to happen on this screen.
          
        }

    }
}
