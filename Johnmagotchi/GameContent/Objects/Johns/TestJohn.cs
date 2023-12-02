using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Johnmagotchi.GameContent.Objects.Johns
{
    public class TestJohn : AbtractJohn
    {
        Texture2D headTexture;
        Texture2D bodyTexture;

        public TestJohn() :base() {
            
        }   

        public override void Init(ScreenManager screenManager) {
            this.spriteBatch = new SpriteBatch(screenManager.GraphicsDevice);
            this.headTexture = screenManager.contentRef.Load<Texture2D>("johnHeadPlaceholder");
            this.bodyTexture = screenManager.contentRef.Load<Texture2D>("johnBodyPlaceholder");

        }
        public override void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bodyTexture, new Vector2(xPosition, yPosition +32), Color.White);
            spriteBatch.Draw(headTexture, new Vector2(xPosition, yPosition), Color.White);
            spriteBatch.End();
        }
        public override void Update() { }   
        public override void Destroy() { }  
          

    }
}
