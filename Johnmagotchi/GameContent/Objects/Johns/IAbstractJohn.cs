using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;
using static Johnmagotchi.GameContent.GlobalData;
using static Johnmagotchi.GameContent.Objects.Johns.AbstractJohn;

namespace Johnmagotchi.GameContent.Objects.Johns
{
    public interface IAbstractJohn
    {
        public abstract void Init(ScreenManager screenManager);
        public abstract void Draw();
        public abstract void DrawAt(Vector2 pos);
        public abstract void Update();
        public abstract void Destroy();

        public abstract string GetDisplayName();

        public abstract string GetDisplayNameFirst();
        public abstract string GetDisplayNameSecond();

        public abstract JohnStatus getStatus();
     
        public abstract void SetJohnState(JohnState state);
        public abstract JohnState GetJohnState();



        public abstract void SetMoveTo(int x);

        public abstract int GetXPosition();

        public abstract Vector2 getHoldingPosition();

        public abstract bool moveTo(int x);
        public abstract bool moveTo(int x, int y);

        public abstract int getInitialYPos ();

        public abstract string getDescription(int lineNumber);

        public abstract int getDescriptionHeight();
        public abstract float getDescriptionWeight();

        public abstract JohnList getJohnEnum();
        public abstract int getShopCost();


    }

}
