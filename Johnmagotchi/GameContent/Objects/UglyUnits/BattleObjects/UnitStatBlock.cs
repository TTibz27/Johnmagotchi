namespace Johnmagotchi.GameContent.Objects.Units.BattleObjects
{
    public class UnitStatBlock{

        public string Name { get; set; }
        public int MaxHealth {get; set;}
        public int Attack {get; set;}
        public int Defense {get; set;}
        public int Speed {get; set;}
        public int Movement { get; set;}

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
                    movementType = MovementType.FOOT; 
                    return;
                case "WHEELS":
                    movementType= MovementType.WHEELS; 
                    return;
                case "HORSE":
                    movementType = MovementType.HORSE; 
                    return;
                case "FLYING":
                case "FLYER":
                    movementType = MovementType.FLYING;
                    return;
            }


        }
    }


}