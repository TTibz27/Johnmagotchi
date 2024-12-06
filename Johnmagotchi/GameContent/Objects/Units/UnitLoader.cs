using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Johnmagotchi.Core.tools;
using System.Collections;
using System.Net.Http.Headers;

namespace Johnmagotchi.GameContent.Units
{
    public class UnitLoader
    {
        public static List<string> GetTest(){
            // MAC OS also prly Linux -  var path = Path.Combine(Directory.GetCurrentDirectory(), "GameContent/Objects/Units/UnitDefines.csv");

            System.Console.WriteLine("PATH: {0}", Directory.GetCurrentDirectory());
            var path =  "..\\..\\..\\GameContent\\Objects\\Units\\UnitDefines.csv";
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            System.Console.WriteLine("path: {0} ", path);
            using(var reader = new StreamReader(path))
            {
               
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    listA.Add(values[0]);
                    listB.Add(values[1]);
        
                }
            }
            return listA;
        }

        public static List<UnitObject> LoadBaseUnits(){
            List<UnitObject> unitList = new List<UnitObject>();
            System.Console.WriteLine("PATH: {0}", Directory.GetCurrentDirectory());
            var path = "..\\..\\..\\Data\\BaseUnitData.csv";
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            System.Console.WriteLine("path: {0} ", path);

            // these let us resolve which column is which
            int IdIndex = -1;
            int NameIndex = -1;
            int isUniqueIndex = -1;
            int HealthIndex = -1;
            int AttackIndex = -1;
            int DefenseIndex = -1;
            int SpeedIndex = -1;
            int MovementIndex = -1;
            
            bool AreColumnIndexesSet = false;

            using (var reader = new StreamReader(path))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (AreColumnIndexesSet == false)
                    {
                        //These strings MUST match /GameContent/Data/UnitDefines.CSV
                        // That file MUST have these in the first row.
                        for (int i = 0; i < values.Length; i++) {

                            if (values[i].Trim() == "ID") { IdIndex = i; }
                            if (values[i].Trim() == "NAME") { NameIndex = i; }
                            if (values[i].Trim() == "IS_UNIQUE") { isUniqueIndex = i; }
                            if (values[i].Trim() == "HEALTH") { HealthIndex = i; }
                            if (values[i].Trim() == "ATTACK") { AttackIndex = i; }
                            if (values[i].Trim() == "DEFENSE") { DefenseIndex = i; }
                            if (values[i].Trim() == "SPEED") { SpeedIndex = i; }
                            if (values[i].Trim() == "MOVEMENT") { MovementIndex = i; }

                        }
                        AreColumnIndexesSet = true;
                    }
                    else {
                        UnitObject unit = new UnitObject();
                        unit.id = Int32.Parse(values[IdIndex]);
                        unit.name = values[NameIndex];
                        unit.isUnique = ( values[isUniqueIndex].ToLower() == "true");
                        unit.stats.maxHealth = Int32.Parse(values[HealthIndex]);
                        unit.stats.attack = Int32.Parse(values[AttackIndex]);
                        unit.stats.defense = Int32.Parse(values[DefenseIndex]);
                        unit.stats.speed = Int32.Parse(values[SpeedIndex]);
                        unit.stats.movement = Int32.Parse(values[MovementIndex]);


                        switch(unit.id){ 
                            case 0:
                                // Add texture
                                break;
                            case 1:
                                //add texture
                                break;
                            case 2:
                                break;
                                // add texture
                            default:
                                break;
                        }
                        
                        unitList.Add(unit);
                    }             
                }
            }
            return unitList;
        }
    }
}