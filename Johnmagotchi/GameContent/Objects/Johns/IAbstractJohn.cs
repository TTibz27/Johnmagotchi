using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent.Objects.Johns
{
    internal interface IAbstractJohn
    {
        public abstract void Init(ScreenManager screenManager);
        public abstract void Draw();
        public abstract void Update();
        public abstract void Destroy();

        public abstract string GetDisplayName();
    }
    
}
