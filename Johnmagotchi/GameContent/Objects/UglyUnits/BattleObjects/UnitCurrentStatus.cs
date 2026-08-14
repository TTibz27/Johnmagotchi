using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Johnmagotchi.GameContent.Objects.Units.BattleObjects
{
    public class UnitCurrentStatus
    {
        private readonly UnitStatBlock StatBlock;
        public int CurrentHealth { set; get; }
        public int CurrentShieldCount { set; get; }

        public bool isTurnOver {  set; get; }

        public UnitTeam team { set; get; }


        // This Constructor is used when init-ing a player object
        public UnitCurrentStatus(UnitStatBlock stats) {
            StatBlock = stats;
            CurrentHealth = StatBlock.MaxHealth;
            CurrentShieldCount = 0;
        }

        // This Constructor is used when deserializing a unit
        [JsonConstructor]
        public UnitCurrentStatus(int CurrentHealth, int CurrentShieldCount) {
            this.CurrentHealth = CurrentHealth;
            this.CurrentShieldCount = CurrentShieldCount;
        }

        public void setFromSerialized(UnitObject temp, string data) {

            UnitCurrentStatus parsedData = JsonSerializer.Deserialize<UnitCurrentStatus>(data);
            temp.currentStatus.CurrentHealth = parsedData.CurrentHealth;// Update current stats
            CurrentShieldCount = 0;
            TibzLog.Debug("HP on deserialized: " + parsedData.CurrentHealth);
        }
        public enum UnitTeam
        {
            PLAYER,
            ENEMY,
            NPC
        }
    }
}
