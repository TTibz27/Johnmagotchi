using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TibzGame.Core.InputManager
{
    class MouseInput
    {
        public ButtonPress leftClick;
        public ButtonPress rightClick;
        public ButtonPress middleClick;
        public int x;
        public int y;
        public int scrollWheelValue;

        private int prevScrollWheelValue;

        public MouseInput() {
            x = y = prevScrollWheelValue = scrollWheelValue = 0; // set all ints to 0
            leftClick = new ButtonPress();
            rightClick = new ButtonPress();
            middleClick = new ButtonPress();
        
        }
        public int ScrollWheelMoving() /// returns 1 (positive) if up, -1 if Down, and 0 if not moving
        {
            MouseState state = Mouse.GetState();
            this.scrollWheelValue = state.ScrollWheelValue;

            int returnVal = 0;
            if (prevScrollWheelValue > scrollWheelValue)
            {
                returnVal = 1;
            }
            else if (scrollWheelValue > prevScrollWheelValue)
            {
                returnVal = - 1;
            }

            this.prevScrollWheelValue = this.scrollWheelValue;
            return returnVal;
        }

        
    }
}
