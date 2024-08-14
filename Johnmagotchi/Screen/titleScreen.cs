using Johnmagotchi.Screen.MainGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.Screen
{
    internal class titleScreen : GameScreen
    {
        public override void Init()
        {

        }
    

        public override void Draw()
        {
            throw new NotImplementedException();
        }

       

        public override void Update()
        {
           if ( screenManager.inputs.mouseInput.y > 0 &&
                screenManager.inputs.mouseInput.y < 1280 &&
                screenManager.inputs.mouseInput.x > 0 && 
                screenManager.inputs.mouseInput.x < 720 &&
                screenManager.inputs.mouseInput.leftClick.isJustPressed)
            {
                // screenManager.addScreen(new MainGameScreen());
            }
        }

        public override void Destroy()
        {

        }
    }
}
