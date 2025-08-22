using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.GameContent.Logic
{
    public class BattleResults
    {
        /* 
         This class is basically a summary of the "math" that happened in the battle routines
         that will *eventually* be displayed in the battle animation. 
         
         */
        public enum resultType
        {
            MISS,
            ATTACK,
            CRITICAL,
            SHIELDED,
            SPECIAL,
        }

        public List<Tuple<resultType, int, bool>> results;

        public void addResult(resultType result, int val, bool isCounter) {
            results.Add(new Tuple<resultType,int, bool>(result, val, isCounter));
        }
    }
}
