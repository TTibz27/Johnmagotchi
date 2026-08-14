using Johnmagotchi.GameContent.Objects.Units.GameLogic;
using Johnmagotchi.GameContent.Objects.Units.Loadouts;
using Johnmagotchi.GameContent.Objects.Units.Stats;
using Johnmagotchi.GameContent.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Johnmagotchi.GameContent.Objects.Units
{
    internal class Unit : BaseUnitGameObject
    {

        internal UnitStats stats;

        public Unit() 
        {
            SetStats(new UnitStats());
        }
        public Unit(Unit template)
        {
            SetStats(template.GetStats());
        }
        public Unit(string serializeStatdData)
        {
            SetStats(LoadStatsFromSerialized(serializeStatdData));
        }

        public UnitStats GetStats() 
        {
            return stats;
        }
        public UnitStats LoadStatsFromSerialized(string data)
        {

            return new UnitStats();
        }
    }
}
