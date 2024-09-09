using System;
using System.Collections.Generic;
using System.Text;

namespace TibzGame.Core.Inputs
{
    public class InputMapMenu
    {
        public ButtonPress confirm;
        public ButtonPress cancel;
        public ButtonPress special1;
        public ButtonPress special2;
        public ButtonPress special3;
        public ButtonPress special4;
        public ButtonPress special5;
        public ButtonPress navLeft;
        public ButtonPress navRight;
        public ButtonPress navUp;
        public ButtonPress navDown;
        public InputMapMenu() {
            confirm = new ButtonPress();
            cancel = new ButtonPress();
            special1 = new ButtonPress();
            special2 = new ButtonPress();
            special3 = new ButtonPress();
            special4 = new ButtonPress();
            special5 = new ButtonPress();
            navLeft = new ButtonPress();
            navRight = new ButtonPress();
            navUp = new ButtonPress();
            navDown = new ButtonPress();
        }
    }
}
