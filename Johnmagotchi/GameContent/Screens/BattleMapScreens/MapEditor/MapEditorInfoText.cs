using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Screens.BattleMapScreens.MapEditor
{
    internal class MapEditorInfoText
    {
        protected SpriteFont kemco;
        protected SpriteFont pixsplit;
        protected ScreenManager ScreenManager;
        public MapEditorInfoText() { 
           
        }
        public void Init(ScreenManager screenManager)
        {
            ScreenManager = screenManager;
            kemco = ScreenManager.contentRef.Load<SpriteFont>("Fonts/Kemco-20");
            pixsplit = ScreenManager.contentRef.Load<SpriteFont>("Fonts/PixelSplitter");
        }
        public void Draw(int screenQuadrant, int x, int y, string CurrentToolString)
        {
        }
    }
}
