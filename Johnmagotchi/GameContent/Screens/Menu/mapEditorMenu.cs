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
using Johnmagotchi.GameContent.Objects;
using System.Text.Json;
using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Screens.Menu;


namespace Johnmagotchi.Screen.BattleMapScreens
{
    public class MapEditorMenu : GameScreen
    {
        private MapEditorScreen mapEditor;
        private SpriteBatch spriteBatch;
        public SpriteEffects currentSpriteEffects;

        private int xPos;
        private int yPos;

        private int xDrawOffset;
        private int yDrawOffset;

        private MenuOption[] currentOptions;

        Texture2D button;
        Texture2D buttonHighlight;
        SpriteFont kemco;
        private int screenQuadrant; // 1 - top left, 2 top right, 3 bottom left, 4 bottom right. if right, submenus push out left. if bottom, start drawing from bottom up

        public MapEditorMenu(MapEditorScreen parentScreen, int x, int y, int screenQuadrant) {
            mapEditor = parentScreen;
            xPos = x;
            yPos = y;
            this.screenQuadrant = screenQuadrant;
            this.isUpdatePriority = true;
            this.currentOptions = new MenuOption[5];
            currentOptions[0] = new MenuOption(MenuOption.MenuOptionType.CHOOSE_NEW_TOOL);
            currentOptions[1] = new MenuOption(MenuOption.MenuOptionType.IMPORT_MAP);
            currentOptions[2] = new MenuOption(MenuOption.MenuOptionType.EXPORT_MAP);
            currentOptions[3] = new MenuOption(MenuOption.MenuOptionType.ENTER_BATTLE_SCREEN);
            currentOptions[4] = new MenuOption(MenuOption.MenuOptionType.EXIT);
            if (screenQuadrant == 1)
            {
                xDrawOffset = 7000;
                yDrawOffset =  (-2800 * (currentOptions.Length -1)) - 300; 
            }
            else if (screenQuadrant == 2) {
                xDrawOffset = -3800;
                yDrawOffset = (-2800 * (currentOptions.Length - 1)) - 300;
            }
            else if (screenQuadrant == 3) {
                xDrawOffset = -3800;
                yDrawOffset = 0;
            }
            else {
                xDrawOffset = 7000;
                yDrawOffset = 0;
            }

        }

        public override void Init()
        {
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            button = screenManager.contentRef.Load<Texture2D>("Map-UI/test-menu-item-64-28");
            buttonHighlight = screenManager.contentRef.Load<Texture2D>("Map-UI/test-menu-item-64-28-selected");
        }
        public override void Update()
        {
            if (screenManager.inputs.editorInputs.cancel.isJustPressed)
            {
                TibzLog.Debug("Removing screen");
                screenManager.removeTopScreens(1); // remove this screen
            }
            // throw new NotImplementedException();
        }

        public override void Draw(){
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            int yOffset = yDrawOffset;
            if (screenQuadrant >2) { // adjust y offset so it starts a full length higher up
            
            }
            for (int i = 0; i < currentOptions.Length; i++)
            {
                Rectangle tileRect = screenManager.GetScaledRectangle(xPos - xDrawOffset, yPos - yOffset, 6400, 2800);
                spriteBatch.Draw(
                             button, tileRect, null, Color.White, 0, new Vector2(0, 0),
                             currentSpriteEffects, 1);

                yOffset += 2800;
            }

            spriteBatch.End();
        }
        public override void Destroy() {

        }
    }
}