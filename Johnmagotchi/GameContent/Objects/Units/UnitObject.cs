


using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Units
{
    public class UnitObject{
        public int id { get; set; }
        public string name { get; set; }
        public UnitStatBlock stats { set; get; }
        public int CurrentHealth { set; get; }
        public int xPos { set; get; }
        public int yPos { set; get; }

        public bool isUnique { set; get; }
        public UnitTeam team { set; get; }
        public SpriteShaderSets shaderSet;

        public Texture2D sprite;
        private SpriteBatch spriteBatch;
        private ScreenManager _screenManager;
        private bool IsInit;


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
        }
        public UnitObject(int x, int y){
            IsInit = false;
            xPos = x;
            yPos = y;
            stats = new UnitStatBlock();
        }

        public UnitObject(UnitObject template) {
            IsInit = false;
            CopyFromTemplate(template);
            CurrentHealth = stats.maxHealth;
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
            //this.sprite = template.sprite;
        }

        public void DrawAt( int x, int y) {
            if (IsInit == false) return;

            SpriteEffects effects = getSpriteShaderEffects(shaderSet);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            // all rectangles should do the scaling from world coordinates to screen coordinates
            Rectangle rect = _screenManager.GetScaledRectangle(x, y, MapTile.TILE_WIDTH_PX, MapTile.TILE_HEIGHT_PX);

            //TibzLog.Debug("Scaled xPos: {0}, yPos: {1}" , tileRect.X, tileRect.Y);
            // Texture2D currentTexture;

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


           // return "Units/unit-test";

            // this is placeholder till we get more graphics
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

            // shader sets should also be handled, but probably not here.

            // TODO - This is how it should look when its done, grab srite by ID and add color for shaders via the enum

            //switch (id) { 
            //case 0:
            //    return "Units/unit-test";
            //case 1:
            //    return "Units/unit-test-enemy";
            //  case 2:
            //    return "Units/unit-test-npc";
            ////case 3:
            ////    return "";
            ////case 4:
            ////    return "";
            ////case 5:
            ////    return "";
            ////case 6:
            ////    return "";
            //default:
            //    return "Units/unit-test";

        }
    }
}