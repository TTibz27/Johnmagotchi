using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TibzGame.Core.Inputs
{
    public class InputManager
    {
        // classes that have actions should just read from these during their update() to get the latest state of all input devices.
        public InputMapMenu menuInputs;
        public InputMapGame[] gameInputs;

        public InputMapMapEditor editorInputs;
        public MouseInput mouseInput; 

        public Keys[] actionKeyMappings;
        public InputManager() {

            menuInputs = new InputMapMenu();
            gameInputs = new InputMapGame[8]; // effectively this will count as 8 "profiles" for controllers.
            mouseInput = new MouseInput();
            editorInputs = new InputMapMapEditor();

        }

        // getInputs() polls and populates input info for all controllers + keyboard.
        // should be called every update() from the main game class.
        public void getInputs() {

            handleMouseInput();

            handleKeyboardInput(-1);

        }

    
        private void handleKeyboardInput(short playerindex) {

            //Default Keyboard Menu Inputs
            KeyboardState kbState = Keyboard.GetState();
            menuInputs.confirm.RegisterButtonPress(kbState.IsKeyDown(Keys.Enter));
            menuInputs.cancel.RegisterButtonPress(kbState.IsKeyDown(Keys.Escape)); 
            menuInputs.navUp.RegisterButtonPress(kbState.IsKeyDown(Keys.Up));
            menuInputs.navDown.RegisterButtonPress(kbState.IsKeyDown(Keys.Down));
            menuInputs.navLeft.RegisterButtonPress(kbState.IsKeyDown(Keys.Left));
            menuInputs.navRight.RegisterButtonPress(kbState.IsKeyDown(Keys.Right));
            
            menuInputs.special1.RegisterButtonPress(kbState.IsKeyDown(Keys.D1));
            menuInputs.special2.RegisterButtonPress(kbState.IsKeyDown(Keys.D2));
            menuInputs.special3.RegisterButtonPress(kbState.IsKeyDown(Keys.D3));
            menuInputs.special4.RegisterButtonPress(kbState.IsKeyDown(Keys.D4));
            menuInputs.special5.RegisterButtonPress(kbState.IsKeyDown(Keys.D5));


            // in update function just check bool for pressed state, for example:
           // menuInputs.confirm.isPressed

            editorInputs.registerButtons(kbState);

        }


        private void handleMouseInput() // no controller port needed 
        {

            MouseState mouseState = Mouse.GetState();
            mouseInput.leftClick.RegisterButtonPress(mouseState.LeftButton == ButtonState.Pressed);
            mouseInput.rightClick.RegisterButtonPress(mouseState.RightButton == ButtonState.Pressed);
            mouseInput.middleClick.RegisterButtonPress(mouseState.MiddleButton == ButtonState.Pressed);
            mouseInput.x = mouseState.X;
            mouseInput.y = mouseState.Y;
            mouseInput.scrollWheelValue = mouseState.ScrollWheelValue;

           // Debug.WriteLine("MOUSE X: "+ mouseInput.x + "  MOUSE Y: " + mouseInput.y);

        }
    }
}
