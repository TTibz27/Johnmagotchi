


using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Objects;
using Johnmagotchi.GameContent.Objects.Units.PlayerClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using TibzGame.Core.ScreenManager;
using static Johnmagotchi.GameContent.Objects.Units.PlayerClasses.PlayerClass;

namespace Johnmagotchi.GameContent.Units
{
    public class UnitObject{
        //defined stats
        public int internalID; // used to reference this object internally
        public int id { get; set; } // used to reference this unit as "Byleth" or "Marth" or whatever, you know?
        public string name { get; set; }
        public UnitStatBlock stats { set; get; }
        public bool isUnique { set; get; }
        public UnitTeam team { set; get; }
        public PlayerClassType playerClassEnum { get; set; }
    
     // Stats for battle screens
        public int CurrentHealth { set; get; }
        public bool isTurnOver { set; get; }
     // public statusEnum status {get;set;}
        public int xPos { set; get; }
        public int yPos { set; get; }

        private PlayerClass playerClassInstance;

    //drawing and state management
        public SpriteShaderSets shaderSet;
        public Texture2D sprite;
        private Effect TurnEndedEffect;

        private SpriteBatch spriteBatch;
        private ScreenManager _screenManager;
        private bool IsInit;

    // DEBUG VARS 
        // this needs to be expanded for attacks/ items eventually, does not need saved atm
        public int DirectAttackRangeMin = 2;
        public int DirectAttackRangeMax = 4;
        public int IndirectAttackRangeMin;
        public int IndirectAttackRangeMax;


        public enum UnitTeam
        {
            PLAYER,
            ENEMY,
            NPC
        }
        public enum SpriteShaderSets { 
            PLAYER_NORMAL,
            ENEMY_NORMAL,
            NPC_NORMAL
        }


        public UnitObject(){
            IsInit = false;
            xPos = 0;
            yPos = 0;
            stats = new UnitStatBlock();
        //    playerClassInstance = new PlayerClass(playerClassEnum);

        }
        public UnitObject(int x, int y){
            IsInit = false;
            xPos = x;
            yPos = y;
            stats = new UnitStatBlock();
        //    playerClassInstance = new PlayerClass(playerClassEnum);

        }

        public UnitObject(UnitObject template) {
            IsInit = false;
            CopyFromTemplate(template);
            CurrentHealth = stats.maxHealth;
            isTurnOver = false;
            playerClassInstance = new PlayerClass(playerClassEnum);

        }

        public UnitObject(string serializedData)
        {
            IsInit = false;
            SetFromSerialized(serializedData);
           
        }

        public string GetAsSerialized()
        {
           return JsonSerializer.Serialize(this);
        }

        public void SetFromSerialized(string data)
        {
           UnitObject temp = JsonSerializer.Deserialize<UnitObject>(data);
            CopyFromTemplate(temp); // get base stats
            CurrentHealth = temp.CurrentHealth;// Update current stats
            isTurnOver = temp.isTurnOver;
            TibzLog.Debug("HP on deserialized: " + temp.CurrentHealth);
            return ;
        }


        public void setTeam(UnitTeam team)
        {
            this.team = team;
            this.ReinitSprite();
        }

        public void SetShaderSet(SpriteShaderSets inSet) {
            shaderSet = inSet;
        }

        public void InitSprite(ScreenManager screenManager, UnitTeam team) {
            _screenManager = screenManager;
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            this.team = team;
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

        public void Update() { 
        
        
        }

        public void CopyFromTemplate(UnitObject template)
        {
            this.id = template.id;
            this.name = template.name;
            this.stats = template.stats;
            this.xPos = template.xPos;
            this.yPos = template.yPos;
            this.isUnique = template.isUnique;
            this.playerClassEnum = template.playerClassEnum;
           
            //this.sprite = template.sprite;
        }

        public void DrawAt( int x, int y) {
            if (IsInit == false) return;

            SpriteEffects effects = getSpriteShaderEffects(shaderSet);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);

            // all rectangles should do the scaling from world coordinates to screen coordinates
            Rectangle rect = _screenManager.GetScaledRectangle(x, y, MapTile.TILE_WIDTH_PX, MapTile.TILE_HEIGHT_PX);

            //TibzLog.Debug("Scaled xPos: {0}, yPos: {1}" , tileRect.X, tileRect.Y);
            // Texture2D currentTexture;

            if (isTurnOver) {
                //apply shader
                TurnEndedEffect.CurrentTechnique.Passes[0].Apply();
            }

            spriteBatch.Draw(
                sprite, rect, null, Color.White, 0, new Vector2(0, 0),
                effects, 1);

            spriteBatch.End();
        }


        private SpriteEffects getSpriteShaderEffects(SpriteShaderSets shaderEnum) {

            return SpriteEffects.None;

        }
        public string getSpriteTexturePath()
        {
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