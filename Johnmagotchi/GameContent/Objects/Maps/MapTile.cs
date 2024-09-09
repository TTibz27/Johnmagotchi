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

        public TileType Type { get; set; }

        public TileSubType SubType { get; set; }
         
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
        Texture2D GrassTileTexture;
        Texture2D SeaTileTexture;


        public MapTile()
        {
         this.Type = TileType.GRASS;
         this.SubType = TileSubType.NONE;
        }
        public void Init(ScreenManager screenManager)
        {
         
            _screenManager = screenManager;
            spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            GrassTileTexture = screenManager.contentRef.Load<Texture2D>("Tiles/Grass/grass-1");
            SeaTileTexture= screenManager.contentRef.Load<Texture2D>("Tiles/Sea/ocean");
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
            
            Texture2D currentTexture;

            switch(this.Type){
                case TileType.GRASS:
                    currentTexture = GrassTileTexture;
                break;
                case TileType.SEA:
                    currentTexture = SeaTileTexture;
                break;
                default:
                    currentTexture = GrassTileTexture;
                break;
            }
          
            spriteBatch.Draw(
                currentTexture, tileRect, null, Color.White, 0, new Vector2(0, 0),
                currentSpriteEffects, 1);
    
            spriteBatch.End();
        }
    }
}