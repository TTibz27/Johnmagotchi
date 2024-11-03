using Johnmagotchi.GameContent.Objects;
using System.Collections.Generic;
using  Johnmagotchi.GameContent.Units;
using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Screens.Menu;
using Johnmagotchi.GameContent.Screens.BattleMapScreens.MapEditor;

namespace Johnmagotchi.Screen.BattleMapScreens
{
    public class MapEditorScreen : BaseMapScreen
    {

        public enum EditorToolType
        {
            TILE_EDIT,
            UNIT_ADD,
            UNIT_DELETE
        }
        private enum UnitFactionType { 
            PLAYER,
            ENEMY,
            NPC
        }

        private TileType selectedTileType;
        private int selectedUnitIndex;
        private EditorToolType CurrentTool;
        private MapEditorInfoText InfoText;
        public List<UnitObject> AvailableUnits;
        private UnitFactionType UnitFaction; 


        public MapEditorScreen(){

            this.isUpdatePriority = true;
            this.InfoText = new MapEditorInfoText();
        }
        public MapEditorScreen(BattleMap existingMap){
            this.CurrentMap = existingMap;
        }

        public BattleMap GetMap() {
            return CurrentMap;        
        }

        public override void ChildInit()
        {
            InfoText.Init(screenManager);
            selectedTileType = TileType.GRASS;
            UnitLoader.GetTest();
            AvailableUnits = UnitLoader.LoadBaseUnits();
            selectedUnitIndex = 0;
            UnitFaction = UnitFactionType.PLAYER;
            foreach (UnitObject unit in AvailableUnits) {
                TibzLog.Debug(" Unit id: {0}, name: {1},  health: {2}, atk: {3}, def: {4}, spd {5}, unique: {6} ", unit.id, unit.name, unit.stats.health, unit.stats.attack, unit.stats.defense, unit.stats.speed , unit.isUnique);
                TibzLog.Debug(" INIT SPRITES HERE");

                unit.InitSprite(screenManager);
            }
        }
        public override void ChildUpdate() {
            if (CurrentTool == EditorToolType.TILE_EDIT)
            {
                if (screenManager.inputs.editorInputs.special1.isJustPressed)
                {
                    this.selectedTileType = TileType.GRASS;
                }
                if (screenManager.inputs.editorInputs.special2.isJustPressed)
                {
                    this.selectedTileType = TileType.SEA;
                }
                if (screenManager.inputs.editorInputs.confirm.isPressed)
                {
                    CurrentMap.ChangeTileType(this.cursorIndexX, this.cursorIndexY, this.selectedTileType);
                }
            }
// ----------------------------------------------------------------------------------------------------------------------
            if (CurrentTool == EditorToolType.UNIT_ADD)
            {
                if (screenManager.inputs.editorInputs.special1.isJustPressed)
                {
                    selectedUnitIndex ++;
                    if (selectedUnitIndex > AvailableUnits.Count -1) {
                        selectedUnitIndex = 0;
                    }
                }
                if (screenManager.inputs.editorInputs.special2.isJustPressed)
                {
                   selectedUnitIndex --;
                    if (selectedUnitIndex < 0)
                    {
                        selectedUnitIndex = AvailableUnits.Count - 1;
                    }
                }

                if (screenManager.inputs.editorInputs.special3.isJustPressed)
                {
                    UnitFaction++;
                    if (UnitFaction > UnitFactionType.NPC)
                    {
                        UnitFaction = UnitFactionType.PLAYER;
                    }
                }
                if (screenManager.inputs.editorInputs.special4.isJustPressed)
                {
                    UnitFaction--;
                    if (UnitFaction < UnitFactionType.PLAYER)
                    {
                        UnitFaction = UnitFactionType.NPC;
                    }
                }
                if (screenManager.inputs.editorInputs.confirm.isJustPressed)
                {
                    if (UnitFaction == UnitFactionType.PLAYER) 
                    {
                        CurrentMap.AddPlayerUnit(AvailableUnits[selectedUnitIndex], cursorIndexX, cursorIndexY);
                    }
                    else if (UnitFaction == UnitFactionType.ENEMY)
                    {
                        CurrentMap.AddEnemyUnit(AvailableUnits[selectedUnitIndex], cursorIndexX, cursorIndexY);
                    }
                   else  if (UnitFaction == UnitFactionType.NPC)
                    {
                        CurrentMap.AddNpcUnit(AvailableUnits[selectedUnitIndex], cursorIndexX, cursorIndexY);
                    }
                  //  CurrentMap.ChangeTileType(this.cursorIndexX, this.cursorIndexY, this.selectedTileType);
                }
            }
 // ----------------------------------------------------------------------------------------------------------------------

            if (screenManager.inputs.editorInputs.cancel.isJustPressed)
            {         
                this.screenManager.addScreen(
                    new MapEditorMenu(
                    this,
                    (MapTile.TILE_WIDTH_PX * cursorIndexX) + scrollOffsetX, 
                    (MapTile.TILE_HEIGHT_PX * cursorIndexY) + scrollOffsetY,
                    CursorQuadrant));
            }
            
        }

        public void UpdateEditorTool(EditorToolType tool) { 
            CurrentTool = tool;

            TibzLog.Debug("Tool Updated: {0}", CurrentTool);
        }

        public override void ChildDraw()
        {
            string tooltext = CurrentTool.ToString().Replace("_", " ");
            InfoText.Draw( CursorQuadrant,0,0, "Current Tool : " + tooltext);
            if (CurrentTool == EditorToolType.TILE_EDIT) {
                InfoText.Draw(CursorQuadrant, 15 , 30, "Current Tile : " + selectedTileType);
            }
            if (CurrentTool == EditorToolType.UNIT_ADD)
            {
                InfoText.Draw(CursorQuadrant, 15, 30, "Current Unit : " + AvailableUnits[selectedUnitIndex].name); 
                InfoText.Draw(CursorQuadrant, 15, 60, "Current Faction : " + UnitFaction);
            }
        }
    }
}

