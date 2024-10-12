using Johnmagotchi.Core.tools;
using Johnmagotchi.Screen.BattleMapScreens;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;
using Microsoft.Xna.Framework;


namespace Johnmagotchi.GameContent.Screens.Menu
    {
        public abstract class BaseMapMenu : GameScreen
        {
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
           protected int screenQuadrant; // 1 - top left, 2 top right, 3 bottom left, 4 bottom right. if right, submenus push out left. if bottom, start drawing from bottom up

            public BaseMapMenu(GameScreen parentScreen, int x, int y, int screenQuadrant)
            {
                ParentGameScreen = parentScreen;
                xPos = x;
                yPos = y;
                this.screenQuadrant = screenQuadrant;
                isUpdatePriority = true;
                SelectedIndex = 0;
                
                // Child class needs to init: 
                // parent screen
                // CurrentOptions List
                // X & Y Draw Offsets
                
            }

            public override void Init()
            {
                spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
                button = screenManager.contentRef.Load<Texture2D>("Map-UI/test-menu-item-64-28");
                buttonHighlight = screenManager.contentRef.Load<Texture2D>("Map-UI/test-menu-item-64-28-selected");
                //load fonts
                kemco = screenManager.contentRef.Load<SpriteFont>("Fonts/Kemco-20");
                pixsplit = screenManager.contentRef.Load<SpriteFont>("Fonts/PixelSplitter");
                ChildInit();
            }

            public override void Update()
            {
                if (screenManager.inputs.editorInputs.cancel.isJustPressed)
                {
                    TibzLog.Debug("Removing screen");
                    screenManager.removeTopScreens(1); // remove this screen
                }

                if (screenManager.inputs.editorInputs.navUp.isJustPressed)
                {
                    SelectedIndex--;
                    if (SelectedIndex < 0) { SelectedIndex = CurrentOptions.Length - 1; }
                }
                if (screenManager.inputs.editorInputs.navDown.isJustPressed)
                {
                    SelectedIndex++;
                    if (SelectedIndex >= CurrentOptions.Length) { SelectedIndex = 0; }
                }
              
                //Child should implement what happens on confirm
                ChildUpdate();
            }

            public abstract void ChildInit();
            public abstract void ChildUpdate();
            public override void Draw()
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

                int yOffset = yDrawOffset;
                if (screenQuadrant > 2)
                { // adjust y offset so it starts a full length higher up

                }

                //------------------------- Draw boxes -----------------------------
                for (int i = CurrentOptions.Length; i > 0; i--)
                {
                    Texture2D buttonSprite = button;
                    if (i - 1 == SelectedIndex)
                    {
                        buttonSprite = buttonHighlight;
                    }
                    // Box
                    Rectangle tileRect = screenManager.GetScaledRectangle(xPos - xDrawOffset, yPos - yOffset, 6400, 2800);
                    spriteBatch.Draw(
                                 buttonSprite, tileRect, null, Color.White, 0, new Vector2(0, 0),
                                 currentSpriteEffects, 1);

                    // increase offset for next item
                    yOffset += 2800;
                }

                // ------------------------- Draw text -----------------------------
                yOffset = yDrawOffset;
                int textOffsetX = 1000;
                int textOffsetY = 1000;
                for (int j = CurrentOptions.Length; j > 0; j--)
                {
                    int i = j - 1;
                    //text
                    Vector2 position = new(screenManager.getScaledIntX(xPos - xDrawOffset + textOffsetX), screenManager.getScaledIntY(yPos - (yOffset - textOffsetY)));  // add 20 to each
                    Vector2 textRotationOrigin = new Vector2(0, 0);
                    spriteBatch.DrawString(kemco, CurrentOptions[i].OptionLabel, position, Color.Black, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);
                    // increase offset for next item
                    yOffset += 2800;
                }

                spriteBatch.End();
            }
            public override void Destroy()
            {

            }
        }
    }
