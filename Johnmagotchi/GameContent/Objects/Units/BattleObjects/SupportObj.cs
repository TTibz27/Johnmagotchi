using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Johnmagotchi.Data.BattleObjects.SupportDefines;

namespace Johnmagotchi.GameContent.Objects.Units.BattleObjects
{
    public class SupportObj
    {

        //ID,SupportName,SupportTarget,SupportType,Value,Description
        public int ID;
        public string Name;
        public SupportTarget target;
        public SupportType type;
        public int Value;
        public string Description;
    }
}
