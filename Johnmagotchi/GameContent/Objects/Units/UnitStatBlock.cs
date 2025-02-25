

namespace Johnmagotchi.GameContent.Units
{
    public class UnitStatBlock{
        public int maxHealth {get; set;}
        public int attack {get; set;}
        public int defense {get; set;}
        public int speed {get; set;}
        public int movement { get; set;}

        public MovementType movementType {get; set;}

        public enum MovementType { 
               FOOT,
               WHEELS,
               HORSE,
               FLYING,
        }

        public void setMovementTypeFromFile(string inStr) {
            switch (inStr) {
                case "FOOT":
                    this.movementType = MovementType.FOOT; 
                    return;
                case "WHEELS":
                    this.movementType= MovementType.WHEELS; 
                    return;
                case "HORSE":
                    this.movementType = MovementType.HORSE; 
                    return;
                case "FLYING":
                case "FLYER":
                    this.movementType = MovementType.FLYING;
                    return;
            }


        }
    }


}