using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TibzGame.Core.Inputs;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Objects
{
   
    public class MapTile
    {
        public static readonly int TILE_HEIGHT_PX = 32;
        public static readonly int TILE_WIDTH_PX = 32;

        public TileType Type;
        public TileType NorthNeighborType;
        public TileType SouthNeighborType;
        public TileType EastNeighborType;
        public TileType WestNeighborType;


        private SpriteBatch spriteBatch;
        private ScreenManager _screenManager;

        public int width;
        public int height;
        public int shadowOffset;
        public bool facingLeft;
        public float spriteRotation;
        public SpriteEffects currentSpriteEffects;  
        Texture2D TileTexture;

        public MapTile()
        {
         this.Type = TileType.GRASS;
        }
        public void Init(ScreenManager screenManager)
        {
         
            _screenManager = screenManager;
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            TileTexture = screenManager.contentRef.Load<Texture2D>("Tiles/Grass/grass-1");
        }

        public void ChangeType(TileType newType)
        {
            this.Type = newType;
        }

        public void NullNeighbors()
        {
            this.NorthNeighborType = TileType.NULL;
            this.SouthNeighborType = TileType.NULL;
            this.WestNeighborType = TileType.NULL;
            this.EastNeighborType = TileType.NULL;
        }

        public void DrawAt(int posX, int posY)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            // all rectangles should do the scaling from world coordinates to screen coordinates
            Rectangle tileRect = _screenManager.GetScaledRectangle(posX, posY, TILE_WIDTH_PX , TILE_HEIGHT_PX); 
        
            
            spriteBatch.Draw(
                TileTexture, tileRect, null, Color.White, 0, new Vector2(0, 0),
                currentSpriteEffects, 1);
    
            spriteBatch.End();
        }
    }
}