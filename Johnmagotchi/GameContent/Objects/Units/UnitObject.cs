


namespace Johnmagotchi.GameContent.Units
{
    public class UnitObject{
        public UnitStatBlock stats;

        public int xPos;
        public int yPos;

        public UnitObject(){
            xPos = 0;
            yPos = 0;
            stats = new UnitStatBlock();
        }
        public UnitObject(int x, int y){
            xPos = x;
            yPos = y;
            stats = new UnitStatBlock();
        }
    }
}