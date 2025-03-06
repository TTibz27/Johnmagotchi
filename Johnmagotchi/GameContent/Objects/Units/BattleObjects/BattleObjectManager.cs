using Johnmagotchi.GameContent.Units;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.GameContent.Objects.Units.BattleObjects
{
    public class BattleObjectManager
    {
        public static List<AttackObj> Attacks;
        public static List<SkillObj> Skills;
        public static List<SupportObj> Supports;
            
        public BattleObjectManager() 
        { 
        
        
        }

        public void LoadBattleObjects() 
        {
            Attacks = LoadAttacks();
            Skills = LoadSkills();
            Supports = LoadSupports();
        }

        public List<AttackObj> LoadAttacks() 
        {
            List<AttackObj> AttackList = new List<AttackObj>();
            System.Console.WriteLine("PATH: {0}", Directory.GetCurrentDirectory());
            var path = "..\\..\\..\\Data\\BattleObjects\\AttackDefinitions.csv";
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
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
                        attack.AttackType = (AttackObj.AttackTypeEnum)Int32.Parse(values[AttackTypeIndex]);
                        attack.SpecialProperties =(AttackObj.SpecialAttackPropertiesEnum) Int32.Parse(values[SpecialPropertiesIndex]);

                                 
                    }
                }
            }
            return AttackList;

        }

        public List<SkillObj> LoadSkills()
        {
            List<SkillObj> SkillList = new List<SkillObj>();
            return SkillList;
        }

        public List<SupportObj> LoadSupports() 
        {
            List<SupportObj> SupportList = new List<SupportObj>();
            return SupportList;

        }
    }
}
