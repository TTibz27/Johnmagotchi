using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Johnmagotchi.Core.tools;

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

                    TibzLog.Debug("Values: {0} , {1}", values[0], values[1]);
        
                }
            }
            return listA;
        }

        public static List<UnitObject> LoadBaseUnits(){
            List<UnitObject> unitList = new List<UnitObject>();


            return unitList;
        }
    }
}