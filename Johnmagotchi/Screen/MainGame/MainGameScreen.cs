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

namespace Johnmagotchi.Screen.MainGame
{
    public class MainGameScreen : GameScreen
    {
        SpriteBatch testSprite;
        Texture2D texture;
        Texture2D topBar;
        TestJohn currentJohn; // TODO- Change to have John interface that matches the abstractJohn

        public override void Init()
        {
            testSprite = new SpriteBatch(screenManager.GraphicsDevice);

            texture = screenManager.contentRef.Load<Texture2D>("sidebarPlaceholder");
            topBar = screenManager.contentRef.Load<Texture2D>("TopBarPlaceholder");

            currentJohn = new TestJohn();
            currentJohn.Init(screenManager);

        }
        public override void Draw()
        {
            // Background
            testSprite.Begin();
            testSprite.Draw(texture, new Vector2(0, 0), Color.White);
            testSprite.Draw(topBar, new Vector2(350, 0), Color.White);
            Debug.WriteLine("draw");
            testSprite.End();

            //John playfield
            currentJohn.Draw();


        }
        public override void Update()
        {
            //throw new NotImplementedException();
        }
        public override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}
