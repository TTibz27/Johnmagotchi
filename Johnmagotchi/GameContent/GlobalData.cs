using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent
{
    public class GlobalData
    {
        ScreenManager screenManager;
        public GlobalData(ScreenManager sm) {
            screenManager = sm;
        }
    }
}
