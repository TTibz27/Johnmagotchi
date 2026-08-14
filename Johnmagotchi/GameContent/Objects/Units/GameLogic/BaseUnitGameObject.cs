using Johnmagotchi.GameContent.Objects.Units.BattleObjects;
using Johnmagotchi.GameContent.Objects.Units.Loadouts;
using Johnmagotchi.GameContent.Objects.Units.Loadouts.PlayerClasses;
using Johnmagotchi.GameContent.Objects.Units.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;
using static Johnmagotchi.GameContent.Objects.Units.BattleObjects.UnitCurrentStatus;
using static Johnmagotchi.GameContent.Objects.Units.Loadouts.PlayerClasses.PlayerClass;

namespace Johnmagotchi.GameContent.Objects.Units.GameLogic
{
    internal abstract class BaseUnitGameObject
    {

            //defined stats
            public int internalID; // used to reference this object internally
            public int id { get; set; } // used to reference this unit as "Byleth" or "Marth" or whatever, you know?
            public string name { get; set; }

            private UnitStats stats;
           
            public PlayerClassType playerClassEnum { get; set; }

            // public statusEnum status {get;set;}
            public int xPos { set; get; }
            public int yPos { set; get; }


            // -------------------------------------------------------------------------------------------------------------



            //drawing and state management
            public SpriteShaderSets shaderSet;
            public Texture2D sprite;
            private Effect TurnEndedEffect;

            private SpriteBatch spriteBatch;
            private ScreenManager _screenManager;
            private bool IsInit;
            private bool hasStats;




            public enum SpriteShaderSets
            {
                PLAYER_NORMAL,
                ENEMY_NORMAL,
                NPC_NORMAL
            }


            public BaseUnitGameObject()
            {
                IsInit = false;
                hasStats = false;
                xPos = 0;
                yPos = 0;
            }

            public void SetStats(UnitStats newStats) 
            {
                stats = newStats;
                hasStats = true;
            }
            public string GetAsSerialized()
            {
                string json = JsonSerializer.Serialize(this);
                return json;
            }



            public void SetShaderSet(SpriteShaderSets inSet)
            {
                shaderSet = inSet;
            }

            public void InitSprite(ScreenManager screenManager)
            {
                _screenManager = screenManager;
                spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
                string path = getSpriteTexturePath();
                sprite = screenManager.contentRef.Load<Texture2D>(path);
                this.internalID = screenManager.getUnitId();
                TurnEndedEffect = screenManager.contentRef.Load<Effect>("Shaders/Units/UnitTurnEnded_S");
                IsInit = true;

            }

            public void ReinitSprite()
            {
                spriteBatch = new SpriteBatch(_screenManager.GraphicsDevice);
                string path = getSpriteTexturePath();
                sprite = _screenManager.contentRef.Load<Texture2D>(path);
                IsInit = true;
            }

            public void Update()
            {


            }

            public void DrawAt(int x, int y)
            {
                if (IsInit == false) return;
                if (hasStats == false) return;

                SpriteEffects effects = getSpriteShaderEffects(shaderSet);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);

                // all rectangles should do the scaling from world coordinates to screen coordinates
                Rectangle rect = _screenManager.GetScaledRectangle(x, y, MapTile.TILE_WIDTH_PX, MapTile.TILE_HEIGHT_PX);

                //TibzLog.Debug("Scaled xPos: {0}, yPos: {1}" , tileRect.X, tileRect.Y);
                // Texture2D currentTexture;

                if (stats.status.isTurnOver)
                {
                    //apply shader
                    TurnEndedEffect.CurrentTechnique.Passes[0].Apply();
                }

                spriteBatch.Draw(
                    sprite, rect, null, Color.White, 0, new Vector2(0, 0),
                    effects, 1);

                spriteBatch.End();
            }


            private SpriteEffects getSpriteShaderEffects(SpriteShaderSets shaderEnum)
            {

                return SpriteEffects.None;

            }
            public string getSpriteTexturePath()
            {
            UnitTeam team  = stats.status.team;
                if (team == UnitTeam.PLAYER)
                {
                    return "Units/unit-test";
                }
                if (team == UnitTeam.ENEMY)
                {
                    return "Units/unit-test-enemy";
                }
                if (team == UnitTeam.NPC)
                {
                    return "Units/unit-test-npc";
                }
                return "Units/unit-test";
            }
        }
    }
