using System;
using System.Collections.Generic;
using System.Text;

namespace TibzGame.Core.Inputs
{
    public class ButtonPress
    {
        public bool isPressed = false;
        public bool isJustPressed = false;
        public bool isJustReleased = false;
        public UInt64 heldTime = 0;

        public void RegisterButtonPress(bool pressed) {
            bool lastState = isPressed;
            isPressed = pressed;

            if (lastState == isPressed) isJustReleased = isJustPressed = false;
            else if (isPressed) isJustPressed = true;
            else isJustReleased = true;

            if (! isPressed) heldTime = 0;
            else  heldTime++;
           
        }
    }
}
