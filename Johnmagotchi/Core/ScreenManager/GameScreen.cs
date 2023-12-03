using System;
using System.Collections.Generic;
using System.Text;

namespace TibzGame.Core.ScreenManager
{
    public abstract class GameScreen
    {
        public abstract void Init();
        public abstract void Draw();
        public abstract void Update();

        public abstract void Destroy();



        public bool isUpdatePriority; // Blocks updates lower than this in the stack
        public bool isDrawPriority; // Blocks draws lower than this in the stack
        public bool isTopScreen; // only topmost screen should accept inputs

        public ScreenManager ScreenManager
        {
            get { return screenManager; }  
            internal set { screenManager = value; }
        }

        public ScreenManager screenManager;   
    }
}
