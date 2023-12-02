using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.GameContent.Objects
{
    public class JohnStatus
    {
        public int sleepy;
        public int hungry;
        public int bathroom;
        public int JPM;

        public JohnStatus() 
        { 
            sleepy = 200;
            hungry = 200;
            bathroom = 200;
            JPM = 5000;
        }

        public void UpdateStats ()
        {
            sleepy--;
            hungry--;
            bathroom--;

        }

    }
}
