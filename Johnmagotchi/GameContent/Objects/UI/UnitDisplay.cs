using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Objects.UI
{
    public class UnitDisplay
    {
        private SpriteBatch spriteBatch;
        private ScreenManager _screenManager;
        public SpriteEffects currentSpriteEffects;
        Texture2D UnitDisplayTexture;
        Texture2D EnemyDisplayTexture;
        Texture2D NpcDisplayTexture;
        protected SpriteFont kemco;
        protected SpriteFont pixsplit;
        UnitObject HighlightedUnit;


        Boolean showDisplay;

        private readonly int DISPLAY_WIDTH = 192 * ScreenManager.BASE_ZOOM_LEVEL;
        private readonly int DISPLAY_HEIGHT = 96 * ScreenManager.BASE_ZOOM_LEVEL;

        public UnitDisplay() {
            showDisplay = false;
        }
        public void Init(ScreenManager screenManager)
        {
            _screenManager = screenManager;
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            UnitDisplayTexture = screenManager.contentRef.Load<Texture2D>("Map-UI/unit-display-blue-lighter");
            EnemyDisplayTexture = screenManager.contentRef.Load<Texture2D>("Map-UI/unit-display-red-lighter");
            NpcDisplayTexture = screenManager.contentRef.Load<Texture2D>("Map-UI/unit-display-green-lighter");
            kemco = screenManager.contentRef.Load<SpriteFont>("Fonts/Kemco-20");
            pixsplit = screenManager.contentRef.Load<SpriteFont>("Fonts/PixelSplitter");

        }

        public void Update(UnitObject unit) {
            if (unit == null) {
                showDisplay = false;
                return;
            }
            else {
                showDisplay = true;
                HighlightedUnit = unit;
              
            }
        }

        public void Draw(int cursorQuadrant) {

            if (showDisplay == false) { return; }
            if (HighlightedUnit == null) { return; }

            Texture2D texture = UnitDisplayTexture;
            if (HighlightedUnit.team == UnitObject.UnitTeam.ENEMY)
            {
                texture = EnemyDisplayTexture;
            }
            if (HighlightedUnit.team == UnitObject.UnitTeam.NPC)
            {
                texture = NpcDisplayTexture;
            }

            // offset by 4 X base zoom
            int posX = (int)Math.Round(.5 * (MapTile.TILE_WIDTH_PX));
            int posY = (int)Math.Round(.5 * (MapTile.TILE_HEIGHT_PX));

            if (cursorQuadrant == 2 || cursorQuadrant == 3)
            {
                posX = _screenManager.GetScaledPixelScreenWidth() - DISPLAY_WIDTH- posX;
            }
            //------------------DRAW BACKGROUND------------------------------------
            spriteBatch.Begin();
            Rectangle tileRect = _screenManager.GetScaledRectangle(posX, posY, DISPLAY_WIDTH, DISPLAY_HEIGHT);
            spriteBatch.Draw(
                texture, tileRect, null, Color.White, 0, new Vector2(0, 0),
                currentSpriteEffects, 1);
            spriteBatch.End();

            // -----------------DRAW TEXT -----------------------------------------
            posX += (int)Math.Round(.05 * DISPLAY_WIDTH);
            posY += (15 * ScreenManager.BASE_ZOOM_LEVEL);
            DrawText(posX, posY, HighlightedUnit.name);
            posY += (15 * ScreenManager.BASE_ZOOM_LEVEL);
            DrawText(posX, posY, HighlightedUnit.playerClassEnum.ToString());
            posX += (int)Math.Round(.05 * DISPLAY_WIDTH);
            posY += (15 * ScreenManager.BASE_ZOOM_LEVEL);
            string DisplayString = HighlightedUnit.CurrentHealth + " HP  - " + HighlightedUnit.stats.maxHealth + " MAX";
            DrawText(posX, posY, DisplayString);
        }

        public void DrawText(int x, int y ,string textString) {
            Vector2 textRotationOrigin = new Vector2(0, 0);
             //Vector2 textRotationOrigin = kemco.MeasureString(HighlightedUnit.name);
             //textRotationOrigin.X = textRotationOrigin.X / 2;

             x = _screenManager.getScaledIntX(x);
            y = _screenManager.getScaledIntX(y);
            Vector2 position = new Vector2(x, y);

            spriteBatch.Begin();

            // the hackiest white outline of all time.
            position = new Vector2(x + 2, y + 2);
            spriteBatch.DrawString(kemco, textString, position, Color.White, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);
            position = new Vector2(x - 2, y - 2);
            spriteBatch.DrawString(kemco, textString, position, Color.White, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);
            position = new Vector2(x + 2, y - 2);
            spriteBatch.DrawString(kemco, textString, position, Color.White, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);
            position = new Vector2(x - 2, y + 2);
            spriteBatch.DrawString(kemco, textString, position, Color.White, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);
            position = new Vector2(x + 2, y);
            spriteBatch.DrawString(kemco, textString, position, Color.White, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);
            position = new Vector2(x - 2, y);
            spriteBatch.DrawString(kemco, textString, position, Color.White, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);

            // the actual text
            position = new Vector2(x, y);
            spriteBatch.DrawString(kemco, textString, position, Color.Black, 0, textRotationOrigin, 00.75f, SpriteEffects.None, 1.0f);

            spriteBatch.End();
        }
    }
}
