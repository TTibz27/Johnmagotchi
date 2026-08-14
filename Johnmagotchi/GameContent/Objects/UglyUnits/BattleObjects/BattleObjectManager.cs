using Johnmagotchi.Core.tools;
using Johnmagotchi.Data.BattleObjects;
using Johnmagotchi.GameContent.Units;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Johnmagotchi.Data.BattleObjects.SupportDefines;

namespace Johnmagotchi.GameContent.Objects.Units.BattleObjects
{
    public class BattleObjectManager
    {

        public static List<AttackObj> AllAttacks;
        public static List<SupportObj> AllSupports;
        public static List<SkillObj> AllSkills;



        public static void LoadBattleObjects() 
        {
            AllAttacks = LoadAttacks();
            AllSupports = LoadSupports();
            AllSkills = LoadSkills();

            foreach (AttackObj atk in AllAttacks)
            {
                TibzLog.Debug(" ID: {0}, AttackName: {1},  Base Damamge: {2}, Base Hit Count: {3}, Base Speed: {4}," +
                    " Base Accuracy {5}, rangeMin: {6}, RangeMax: {7}, Attack Type: {8}, Special Properties {9} ",
                  atk.ID, atk.AttackName, atk.BaseDamage, atk.BaseHitCount, atk.BaseSpeed, atk.BaseAccuracy, atk.AttackRangeMin, atk.AttackRangeMax, atk.AttackType, atk.SpecialProperties);
            }

            foreach (SupportObj support in AllSupports)
            {
                TibzLog.Debug(" ID: {0}, Name: {1}, Type:{2}, Target: {3}, Val: {4}, Desc: {5}  ", support.ID, support.Name, support.type, support.target, support.Value, support.Description);
            }
            foreach (SkillObj skl in AllSkills)
            {
                TibzLog.Debug("ID: {0}, Name: {1}, Type: {2}, Val: {3}, Desc: {4} ", skl.ID, skl.SkillName, skl.SkillType, skl.Value, skl.Description);
            }

        }

        public static List<AttackObj> LoadAttacks() 
        {
            List<AttackObj> AttackList = new List<AttackObj>();
            System.Console.WriteLine("PATH: {0}", Directory.GetCurrentDirectory());
            var path = "..\\..\\..\\Data\\BattleObjects\\AttackDefinitions.csv";
            System.Console.WriteLine("path: {0} ", path);

            // these let us resolve which column is which
            int IDIndex = -1;
            int AttackNameIndex = -1;
            int BaseDamageIndex = -1;
            int BaseHitCountIndex = -1;
            int BaseSpeedIndex = -1;
            int BaseAccuracyIndex = -1;
            int AttackRangeMinIndex = -1;
            int AttackRangeMaxIndex = -1;
            int AttackTypeIndex = -1;
            int SpecialPropertiesIndex = -1;

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
                        for (int i = 0; i < values.Length; i++)
                        {

                            if (values[i].Trim() == "ID") { IDIndex = i; }
                            if (values[i].Trim() == "AttackName") { AttackNameIndex = i; }
                            if (values[i].Trim() == "BaseDamage") { BaseDamageIndex = i; }
                            if (values[i].Trim() == "BaseHitCount") { BaseHitCountIndex = i; }
                            if (values[i].Trim() == "BaseSpeed") { BaseSpeedIndex = i; }
                            if (values[i].Trim() == "BaseAccuracy") { BaseAccuracyIndex = i; }
                            if (values[i].Trim() == "AttackRangeMin") { AttackRangeMinIndex = i; }
                            if (values[i].Trim() == "AttackRangeMax") { AttackRangeMaxIndex = i; }
                            if (values[i].Trim() == "AttackType") { AttackTypeIndex = i; }
                            if (values[i].Trim() == "SpecialProperties") { SpecialPropertiesIndex = i; }

                        }
                        AreColumnIndexesSet = true;
                    }
                    else
                    {
                        AttackObj attack = new AttackObj();

                        attack.ID = Int32.Parse(values[IDIndex]);
                        attack.AttackName = values[AttackNameIndex];
                        attack.BaseDamage = Int32.Parse(values[BaseDamageIndex]);
                        attack.BaseHitCount = Int32.Parse(values[BaseHitCountIndex]);
                        attack.BaseSpeed = Int32.Parse(values[BaseSpeedIndex]);
                        attack.BaseAccuracy = Int32.Parse(values[BaseAccuracyIndex]);
                        attack.AttackRangeMin = Int32.Parse(values[AttackRangeMinIndex]);
                        attack.AttackRangeMax = Int32.Parse(values[AttackRangeMaxIndex]);
                        attack.AttackType = AttackDefines.ParseAttackTypes(values[AttackTypeIndex]);
                        attack.SpecialProperties = AttackDefines.ParseSpecialAttackProperties(values[SpecialPropertiesIndex]);

                       AttackList.Add(attack);
                    }
                }
            }
            return AttackList;

        }

        public static List<SkillObj> LoadSkills()
        {
            List<SkillObj> SkillList = new List<SkillObj>();

            System.Console.WriteLine("PATH: {0}", Directory.GetCurrentDirectory());
            var path = "..\\..\\..\\Data\\BattleObjects\\SkillDefinitions.csv";
            System.Console.WriteLine("path: {0} ", path);

           // ID,SkillName,SkillType,Value,Description
            // these let us resolve which column is which
            int IDIndex = -1;
            int SkillNameIndex = -1;
            int SkillTypeIndex = -1;
            int ValueIndex = -1;
            int DescriptionIndex = -1;


            bool AreColumnIndexesSet = false;

            using (var reader = new StreamReader(path))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (AreColumnIndexesSet == false)
                    {
                        //These strings MUST match /GameContent/Data/SkillDefinitions.CSV
                        // That file MUST have these in the first row.
                        for (int i = 0; i < values.Length; i++)
                        {
                            //ID,SkillName,SkillType,Value,Description
                            if (values[i].Trim() == "ID") { IDIndex = i; }
                            if (values[i].Trim() == "SkillName") { SkillNameIndex = i; }
                            if (values[i].Trim() == "SkillType") { SkillTypeIndex = i; }
                            if (values[i].Trim() == "Value") { ValueIndex = i; }
                            if (values[i].Trim() == "Description") { DescriptionIndex = i; }
                        

                        }
                        AreColumnIndexesSet = true;
                    }
                    else
                    {
                        SkillObj skill = new SkillObj();

                        skill.ID = Int32.Parse(values[IDIndex]);
                        skill.SkillName = values[SkillNameIndex];
                        skill.SkillType = SkillDefines.ParseSkillTypes(values[SkillTypeIndex]);
                        skill.Value = Int32.Parse(values[ValueIndex]);
                        skill.Description = values[DescriptionIndex];
                        
                        SkillList.Add(skill);
                    }
                }
            }
            return SkillList;
        }

        public static List<SupportObj> LoadSupports() 
        {
            List<SupportObj> SupportList = new List<SupportObj>();


            System.Console.WriteLine("PATH: {0}", Directory.GetCurrentDirectory());
            var path = "..\\..\\..\\Data\\BattleObjects\\SupportDefinitions.csv";
            System.Console.WriteLine("path: {0} ", path);

            //ID,SupportName,SupportTarget,SupportType,Value,Description
            // these let us resolve which column is which
            int IDIndex = -1;
            int SupportlNameIndex = -1;
            int SupportTargetIndex = -1;
            int SupportTypeIndex = -1;
            int ValueIndex = -1;
            int DescriptionIndex = -1;


            bool AreColumnIndexesSet = false;

            using (var reader = new StreamReader(path))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (AreColumnIndexesSet == false)
                    {
                        //These strings MUST match /GameContent/Data/SkillDefinitions.CSV
                        // That file MUST have these in the first row.
                        for (int i = 0; i < values.Length; i++)
                        {
                            
                            //ID,SupportName,SupportTarget,SupportType,Value,Description
                            if (values[i].Trim() == "ID") { IDIndex = i; }
                            if (values[i].Trim() == "SupportName") { SupportlNameIndex = i; }
                            if (values[i].Trim() == "SupportTarget") { SupportTargetIndex = i; }
                            if (values[i].Trim() == "SupportType") { SupportTypeIndex = i; }
                            if (values[i].Trim() == "Value") { ValueIndex = i; }
                            if (values[i].Trim() == "Description") { DescriptionIndex = i; }


                        }
                        AreColumnIndexesSet = true;
                    }
                    else
                    {
                        SupportObj support = new SupportObj();

                        support.ID = Int32.Parse(values[IDIndex]);
                        support.Name = values[SupportlNameIndex];
                        support.type = SupportDefines.ParseSupportTypes(values[SupportTypeIndex]);
                        support.target = SupportDefines.ParseSupportTargets(values[SupportTargetIndex]);
                        support.Value = Int32.Parse(values[ValueIndex]);
                        support.Description = values[DescriptionIndex];

                        SupportList.Add(support);
                    }
                }
            }


            return SupportList;

        }
    }
}
