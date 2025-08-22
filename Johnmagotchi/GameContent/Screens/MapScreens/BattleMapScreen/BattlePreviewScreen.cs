using Johnmagotchi.GameContent.Screens.Menu;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Screens.MapScreens.BattleMapScreen
{
    internal class BattlePreviewScreen : GameScreen
    {
        public delegate void confirmCallback(bool confirmed);

        protected GameScreen ParentGameScreen;
        protected SpriteBatch spriteBatch;
        public SpriteEffects currentSpriteEffects;

        protected int xPos;
        protected int yPos;

        protected int xDrawOffset;
        protected int yDrawOffset;

        protected int SelectedIndex;
        protected MenuOption[] CurrentOptions;

        protected Texture2D button;
        protected Texture2D buttonHighlight;
        protected SpriteFont kemco;
        protected SpriteFont pixsplit;


        confirmCallback callback;
        public BattlePreviewScreen(ref confirmCallback newCallback) {

            this.callback = newCallback;
        
        }
        public override void Destroy()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void Init()
        {
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            button = screenManager.contentRef.Load<Texture2D>("Map-UI/test-menu-item-64-28");
            buttonHighlight = screenManager.contentRef.Load<Texture2D>("Map-UI/test-menu-item-64-28-selected");
            //load fonts
            kemco = screenManager.contentRef.Load<SpriteFont>("Fonts/Kemco-20");
            pixsplit = screenManager.contentRef.Load<SpriteFont>("Fonts/PixelSplitter");
           
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
