using Johnmagotchi.GameContent.Objects.Units.BattleObjects;
using Johnmagotchi.GameContent.Objects.Units.Loadouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Johnmagotchi.GameContent.Objects.Units.Loadouts.PlayerClasses.PlayerClass;

namespace Johnmagotchi.GameContent.Objects.Units.Stats
{
    internal class UnitStats
    {
        internal UnitStatBlock statblock;
        internal UnitCurrentStatus status;
        internal UnitLoadout loadout;
        internal PlayerClassType classId;

        public UnitStats() 
        {
            // Default Stats
            classId = PlayerClassType.CL_COMMONER;
            statblock = new UnitStatBlock();
            status = new UnitCurrentStatus(statblock);
            loadout = new UnitLoadout(classId);
        }

       // Deserializes Stats
        public UnitStats(string serializedStatData) { 
        
        
        
        
        }

        //Returns This Unit's stats as a serialized String.
        public string getAsSerialized() {

            return "";
        }
    
    }
}
