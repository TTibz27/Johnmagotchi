using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Objects.Johns
{
    public abstract class AbstractJohn : IAbstractJohn
    {
        public JohnStatus status;
        public int xPosition;
        public int yPosition;
        public SpriteBatch spriteBatch;
        public int width;
        public int height;
        public AbstractJohn()
        {
            status = new JohnStatus();
            xPosition = 585;
            yPosition = 385;
            width = 32 * 4;
            height = 64 * 4;
        }
        public abstract void Init(ScreenManager screenManager);
        public abstract void Draw();
        public abstract void Update();
        public abstract void Destroy();

        public abstract string GetDisplayName();

    }
}
