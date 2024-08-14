using Johnmagotchi.GameContent.Objects;
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

namespace Johnmagotchi.Screen.MapEditor
{
    public class MapEditorScreen : GameScreen
    {
        SpriteBatch SpriteBatch;
        private BattleMap CurrentMap;


        public MapEditorScreen(){
            this.CurrentMap = new BattleMap();
        }
        public MapEditorScreen(BattleMap existingMap){
            this.CurrentMap = existingMap;
        }

        public override void Init()
        {
            SpriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
        }


        public override void Destroy()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            this.CurrentMap.DrawMap();// should draw background
            //draw on top of background :
            // units
            // ui overlay
            //cursor
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}