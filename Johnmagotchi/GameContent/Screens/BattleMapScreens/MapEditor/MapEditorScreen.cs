using Johnmagotchi.GameContent.Objects;
using System.Collections.Generic;
using  Johnmagotchi.GameContent.Units;
using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Screens.Menu;
using Johnmagotchi.GameContent.Screens.BattleMapScreens.MapEditor;
using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System;

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
        private readonly bool AUTOLOAD_SLOT_0 = true;
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
                TibzLog.Debug(" Unit id: {0}, name: {1},  health: {2}, atk: {3}, def: {4}, spd {5}, unique: {6} ",
                    unit.id, unit.name, unit.stats.maxHealth, unit.stats.attack, unit.stats.defense,unit.stats.speed , unit.isUnique);
            }

            if (AUTOLOAD_SLOT_0) { 
                this.LoadMapAtSlot(0);
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

        public void SaveMap() {
            this.saveCurrentMap();
        }
        public void SaveMapAtSlot( int saveslot)
        {
            TibzLog.Debug("Save slot : " + saveslot);
            // write to GameContent/Data/SaveMapData/Map-<slot>.json
          var path = "..\\..\\..\\Data\\SaveMapData\\map-" +saveslot + ".json";
            File.WriteAllText(path, this.saveCurrentMap());
        }

        public void LoadMapAtSlot(int saveslot) {
            TibzLog.Debug("LoadMAp hit");
            try
            {
                var path = "..\\..\\..\\Data\\SaveMapData\\map-" + saveslot + ".json";
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(path);
                //We should only need to read the first line of text
                string line = sr.ReadLine();
                TibzLog.Debug(line);
                this.tempSave = line;
                this.loadCurrentMap();
                //if needed we do this to continue to read until you reach end of file
                //while (line != null)
                //{
                //    TibzLog.Debug(line);
                //    //Read the next line
                //    line = sr.ReadLine();
                //}
                //close the file
                sr.Close();

              
                
            }
            catch (Exception e)
            {
                TibzLog.Debug("File Read Error: \n" + e);
            }
        }
    }
}

 