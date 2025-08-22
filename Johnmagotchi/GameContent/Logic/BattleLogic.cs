using Johnmagotchi.GameContent.Objects.Units.BattleObjects;
using Johnmagotchi.GameContent.Units;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.GameContent.Logic
{
    public static class BattleLogic
    {
        private static double CriticalMultiplier = 1.5;
        public static Random Random = new Random();
        public static BattleResults RunBattle(ref UnitObject Attacker, ref UnitObject Defender, AttackObj attack, AttackObj counterAttack) {

            // these are debugs, should be parsed from attack + unit combos
            bool IsCounterAttackOccuring = true;
            bool IsDoubleAttackOccuring = false;
            bool IsDoubleCounterAttackOccuring = false;

            BattleResults results = new BattleResults(); // battle results will determine how the battles are animated

            // for each combat, we need to do each attack phase that occurs apply results here

            //Initial Attack
            HandleAttackPhase(ref Attacker, ref Defender, attack, ref results, false);

            //Enemy's counter attack
            if (IsCounterAttackOccuring)
            {
               HandleAttackPhase(ref Defender, ref Attacker, counterAttack, ref results, true);
            }
            // FE style double attack
            if (IsDoubleAttackOccuring) 
            {
               HandleAttackPhase(ref Attacker, ref Defender, attack, ref results, false);
            }
            // FE style Double Counter Attack
            if (IsDoubleCounterAttackOccuring) 
            {
               HandleAttackPhase(ref Defender, ref Attacker, counterAttack, ref results, true);
            }


            return results;
        }
        
        // this is each step 
        public static void HandleAttackPhase(
            ref UnitObject Attacker, ref UnitObject Defender, AttackObj attack,
            ref BattleResults results, bool isCounter) 
        {
            int hitcount = attack.BaseHitCount; // anything that adjusts how many times an attack happens PER PHASE should be adjusted here

            for (int i = 0; i < hitcount; i++) {
            
                int hitRoll = BattleLogic.Random.Next(100) + 1; // random int 1 - 100
                int hitThreshold = attack.BaseAccuracy + Attacker.stats.speed - Defender.stats.speed;

                // this should be adjusted, in FE games its usally something like......
                // ATTACKER = Hit + (Skill × 2) + (Luck / 2) + (Support bonus)+(Weapon triangle advantage/ disadvantage) +(S level bonus) 
                // DEFENDER = ((Attack speed × 2) +(Luck) + (Terrain bonus) +(Support bonus) +Tactician bonus) )
                // hit perentage = attacker - defender

                int critThreshold = 95;


                if (hitRoll < hitThreshold)  //handle misses
                {
                  
                    results.addResult(BattleResults.resultType.MISS, 0, isCounter);
                }
                else if (hitRoll >= critThreshold)   //Handle Crits
                {
                  
                    if (Defender.CurrentShieldCount > 1) Defender.CurrentShieldCount--; // as is, crits ignore shields but should still tick a shield down
                    if (Defender.CurrentShieldCount < 0) Defender.CurrentShieldCount = 0;
                    int BaseDamage = CalculateDamage(Attacker,Defender,attack);
                    int TotalCritDamage = (int) Math.Ceiling (BaseDamage * CriticalMultiplier);

                    //apply Damage to defending unit, then add to result report
                    Defender.CurrentHealth -= TotalCritDamage;
                    results.addResult(BattleResults.resultType.CRITICAL, TotalCritDamage, isCounter);

                }
                else if (Defender.CurrentShieldCount > 1) // Shielded
                {
                    Defender.CurrentShieldCount--;
                    results.addResult(BattleResults.resultType.SHIELDED,1, isCounter); // 1 siginfies 1 shield lost
                }
                else // normal attack
                { 
                    int BaseDamage = CalculateDamage(Attacker, Defender, attack);

                    //Apply damage + report to animation handler
                    Defender.CurrentHealth -= BaseDamage;
                    results.addResult(BattleResults.resultType.ATTACK, BaseDamage, isCounter);
                }
            }
            return;
        }

        private static int CalculateDamage( UnitObject Attacker, UnitObject Defender, AttackObj attack)
        {
            int damage = 0;

            damage += attack.BaseDamage;
            damage += Attacker.stats.attack;

            damage -= Defender.stats.defense;


            if (damage < 0) damage = 0;
            return damage ;
        }
    }
}
