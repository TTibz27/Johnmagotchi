using System;
using System.Collections.Generic;
using System.Text;

namespace TibzGame.Core.Inputs
{
    public class InputMapMenu
    {
        public ButtonPress confirm;
        public ButtonPress cancel;
        public ButtonPress navLeft;
        public ButtonPress navRight;
        public ButtonPress navUp;
        public ButtonPress navDown;
        public InputMapMenu() {
            confirm = new ButtonPress();
            cancel = new ButtonPress();
            navLeft = new ButtonPress();
            navRight = new ButtonPress();
            navUp = new ButtonPress();
            navDown = new ButtonPress();
        }
    }
}
