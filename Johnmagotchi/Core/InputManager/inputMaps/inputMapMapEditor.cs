using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework.Input;

namespace TibzGame.Core.Inputs
{
    public class InputMapMapEditor
    {
        public ButtonPress confirm;
        public ButtonPress cancel;
        public ButtonPress special1;
        public ButtonPress special2;
        public ButtonPress special3;
        public ButtonPress special4;
        public ButtonPress special5;
         public ButtonPress saveButton;
        public ButtonPress loadButton;
        public ButtonPress navLeft;
        public ButtonPress navRight;
        public ButtonPress navUp;
        public ButtonPress navDown;
        public InputMapMapEditor() {
            confirm = new ButtonPress();
            cancel = new ButtonPress();
            special1 = new ButtonPress();
            special2 = new ButtonPress();
            special3 = new ButtonPress();
            special4 = new ButtonPress();
            special5 = new ButtonPress();

            saveButton = new ButtonPress();
            loadButton = new ButtonPress();

            navLeft = new ButtonPress();
            navRight = new ButtonPress();
            navUp = new ButtonPress();
            navDown = new ButtonPress();
        }

        public void registerButtons(KeyboardState kbState){
            confirm.RegisterButtonPress(kbState.IsKeyDown(Keys.Enter));
            cancel.RegisterButtonPress(kbState.IsKeyDown(Keys.Back)); 
            navUp.RegisterButtonPress(kbState.IsKeyDown(Keys.Up));
            navDown.RegisterButtonPress(kbState.IsKeyDown(Keys.Down));
            navLeft.RegisterButtonPress(kbState.IsKeyDown(Keys.Left));
            navRight.RegisterButtonPress(kbState.IsKeyDown(Keys.Right));
            
            special1.RegisterButtonPress(kbState.IsKeyDown(Keys.D1));
            special2.RegisterButtonPress(kbState.IsKeyDown(Keys.D2));
            special3.RegisterButtonPress(kbState.IsKeyDown(Keys.D3));
            special4.RegisterButtonPress(kbState.IsKeyDown(Keys.D4));
            special5.RegisterButtonPress(kbState.IsKeyDown(Keys.D5));
            saveButton.RegisterButtonPress(kbState.IsKeyDown(Keys.S));
            loadButton.RegisterButtonPress(kbState.IsKeyDown(Keys.L));
        }
    }
}
