using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TibzGame.Core.InputManager
{
    class InputMapGame
    {
        public ButtonPress action0;
        public ButtonPress action1;
        public ButtonPress action2;
        public ButtonPress action3;
        public ButtonPress action4;
        public ButtonPress action5;
        //public ButtonPress action6;

        public ButtonPress dpadUp;
        public ButtonPress dpadDown;
        public ButtonPress dpadLeft;
        public ButtonPress dpadRight;

        public ButtonPress pause;

   

        public Buttons[] buttonKeyMap;
        public InputMapGame() {
            // default button layout
            buttonKeyMap = new Buttons[6];
            buttonKeyMap[0] = Buttons.A;
            buttonKeyMap[1] = Buttons.B;
            buttonKeyMap[2] = Buttons.X;
            buttonKeyMap[3] = Buttons.Y;
            buttonKeyMap[4] = Buttons.RightShoulder;
            buttonKeyMap[5] = Buttons.LeftShoulder;

        }


  
    }
}
