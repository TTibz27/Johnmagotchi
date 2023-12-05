using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;
using static Johnmagotchi.GameContent.Objects.Johns.AbstractJohn;

namespace Johnmagotchi.GameContent.Objects.Johns
{
    internal interface IAbstractJohn
    {
        public abstract void Init(ScreenManager screenManager);
        public abstract void Draw();
        public abstract void Update();
        public abstract void Destroy();

        public abstract string GetDisplayName();

        public abstract JohnStatus getStatus();
     
        public abstract void SetJohnState(JohnState state);
        public abstract JohnState GetJohnState();



        public abstract void SetMoveTo(int x);

        public abstract int GetXPosition();

        public abstract Vector2 getHoldingPosition();
    }
    
}
