using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Objects;
using Johnmagotchi.GameContent.Screens.Menu;
using Johnmagotchi.GameContent.Screens.Menu.BattleMap;
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

namespace Johnmagotchi.Screen.BattleMapScreens
{
    internal class BattleMapScreen : BaseMapScreen
    {
        public BattleMapScreen(BattleMap map): base(map) { 

        }
        public override void Destroy()
        {
           // throw new NotImplementedException();
        }

        public override void ChildInit()
        {
            TibzLog.Debug("Child init hit");
           // throw new NotImplementedException();
        }

        public override void ChildUpdate()
        {
            //   throw new NotImplementedException();

            if (screenManager.inputs.editorInputs.confirm.isJustPressed)
            {
                TibzLog.Debug("Select Pressed");
            }

            if (screenManager.inputs.editorInputs.cancel.isJustPressed)
            {
                this.screenManager.addScreen(
                    new BattleMapMenu(
                    this,
                    (MapTile.TILE_WIDTH_PX * cursorIndexX) + scrollOffsetX,
                    (MapTile.TILE_HEIGHT_PX * cursorIndexY) + scrollOffsetY,
                    CursorQuadrant));
            }
        }

        public override void ChildDraw()
        {
           // throw new NotImplementedException();
        }
    }
}