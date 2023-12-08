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
        public int baseJPM;
        public float currentJPM;

        public JohnStatus() 
        { 
            sleepy = 200;
            hungry = 200;
            bathroom = 200;
            baseJPM = 1000;
            currentJPM = 1000;
        }

        public void UpdateStats ()
        {
            sleepy--;
            hungry--;
            bathroom--;

      
        }

    }
}
