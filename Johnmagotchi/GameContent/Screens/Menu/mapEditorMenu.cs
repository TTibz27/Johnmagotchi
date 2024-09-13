namespace Johnmagotchi.Screen.BattleMapScreens
{
    public abstract class MapEditorMenu : GameScreen
    {
        private MapEditorScreen mapEditor;
        
        private int xPixScaled;
        private int yPixScaled;

        `1Texture2D button;
        Texture2D buttonHighlight;
        SpriteFont kemco;
        private int screenQuadrant; // 1 - top left, 2 top right, 3 bottom left, 4 bottom right. if right, submenus push out left. if bottom, start drawing from bottom up

        public MapEditorMenu(MapEditorScreen parentScreen, int xPixScaled, int yPixScaled, int screenQuadrant){
            mapEditor = parentScreen;
        }

        public override void Init(){

        }

        public override void Draw(){

        }
        public override void Destroy() {

        }
    }
}